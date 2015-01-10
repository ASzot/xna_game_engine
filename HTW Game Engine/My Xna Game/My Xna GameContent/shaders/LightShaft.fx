//-----------------------------------------------------------------------------
// LightPrePass.cs
//
// Jorge Adriano Luna 2012
// http://jcoluna.wordpress.com
//-----------------------------------------------------------------------------


/////////////////////////////////
// Parameters
/////////////////////////////////

float2 PixelSize;
float2 HalfPixel;

float2 LightCenter;

float Saturation;
float4 LinearColorBalance;
float Contrast;
float LinearExposure;
float Blend;

float Scale = 4;
float Intensity = 1;
float Spread = 0.005;
float Decay = 0.5f;
float4 ShaftTint = float4(1,1,1,1);
float TextureAspectRatio = 0.5625f;

// Floating point buffer, scaled down /4
texture DepthBuffer;
sampler2D depthSampler = sampler_state
{
	Texture = <DepthBuffer>;
	MipFilter = NONE;
	MagFilter = POINT;
	MinFilter = POINT;
	AddressU = Clamp;
	AddressV = Clamp;
};

texture ColorBuffer;
sampler2D colorSampler = sampler_state
{
	Texture = <ColorBuffer>;
	MipFilter = NONE;
	MagFilter = POINT;
	MinFilter = POINT;
	AddressU = Clamp;
	AddressV = Clamp;
};


texture ShaftBuffer;
sampler2D shaftSampler = sampler_state
{
	Texture = <ShaftBuffer>;
	MipFilter = NONE;
	MagFilter = LINEAR;
	MinFilter = LINEAR;
	AddressU = Clamp;
	AddressV = Clamp;
};
///////////////////////////////
// Helpers
///////////////////////////////

/////////////////////////////////////////
// Vertex declarations
/////////////////////////////////////////
struct VertexShaderInput
{
    float4 Position : POSITION0;
	float2 TexCoord : TEXCOORD0;
};


struct VertexShaderOutput
{
    float4 Position : POSITION0;
	float4 TexCoord : TEXCOORD0;
	
};


VertexShaderOutput VertexShaderConvertRGB(VertexShaderInput input)
{
    VertexShaderOutput output = (VertexShaderOutput)0;	
	output.Position = input.Position;
	output.TexCoord.xy = input.TexCoord + HalfPixel;
	return output;
}

float4 PixelShaderConvertRGB(VertexShaderOutput input) : COLOR0
{
	// we use the depth buffer as a mask: the background values (closer to 1)
	// have more weight in the shaft texture. We could use some parameters
	// to better control the depth, like scale-bias the value at our taste
	float depth = tex2D(depthSampler,input.TexCoord).r;

	// linear filtering
	float3 color = tex2D(colorSampler,input.TexCoord).rgb;
	color += tex2D(colorSampler,input.TexCoord + float2(0,PixelSize.y)).rgb;
	color += tex2D(colorSampler,input.TexCoord + float2(PixelSize.x,0)).rgb;
	color += tex2D(colorSampler,input.TexCoord + PixelSize).rgb;
	
	return float4(color*0.25f*Scale ,depth);
}



#define SAMPLE_COUNT 40
#define INV_SAMPLE_COUNT 0.025f
 

VertexShaderOutput VertexShaderBlur(VertexShaderInput input)
{
    VertexShaderOutput output = (VertexShaderOutput)0;	
	output.Position = input.Position;
	output.TexCoord.xy = input.TexCoord + HalfPixel;
	output.TexCoord.zw = input.TexCoord*2 - 1;
	return output;
}

float4 PixelShaderBlur(VertexShaderOutput input) : COLOR0
{
	float4 color = 0;
	   
	// Do a radial blur, with the center as the light's center in screen space
	float2 blurDirection = (LightCenter.xy - input.TexCoord.zw);
	// As our screen is not always a square, we should compensate to avoid blurring
	// too much in the shorter dimension
	blurDirection.y *= TextureAspectRatio; 
	
	float2 texCoord = input.TexCoord ;
	blurDirection *=(Spread*INV_SAMPLE_COUNT);
#ifdef XBOX	
	[unroll(SAMPLE_COUNT)]
#endif	
	for (int s = 0; s < SAMPLE_COUNT; ++s)
	{
#ifdef XBOX		
		[flatten]
#endif	
		float weight = 1.0f - s*Decay;
		color += tex2D(shaftSampler,texCoord) * (weight );
		
		texCoord += blurDirection;
	} 

	return float4(color.rgb*(INV_SAMPLE_COUNT* Intensity), color.a);	
}


technique ConvertDepth  //0
{
    pass Pass1
    {
        VertexShader = compile vs_3_0 VertexShaderConvertRGB();
        PixelShader = compile ps_3_0 PixelShaderConvertRGB();
    }
}

technique Blur	//1
{
    pass Pass1
    {
        VertexShader = compile vs_3_0 VertexShaderBlur();
        PixelShader = compile ps_3_0 PixelShaderBlur();
    }
}

// see http://filmicgames.com/archives/75 for details
#define FILMIC_FAST_CORRECTION
float3 ToneMap(float3 LinearColor)
{	
	// Exposure
	LinearColor = LinearColor * LinearExposure;
   
	// Contrast
	float LinRef = 0.18;
	LinearColor = LinRef + Contrast * ( LinearColor - LinRef );   
  
    // Color Balance
	LinearColor *= LinearColorBalance;

	// Desaturation
    float3 greyColor = dot( LinearColor, float3( .30, .59, .11 ) );
    LinearColor = greyColor + Saturation * ( LinearColor - greyColor );
    LinearColor = max( 0, LinearColor );
	
#ifdef FILMIC_FAST_CORRECTION
    float3 x = max(0,LinearColor-0.004);
	LinearColor = (x*(6.2*x+.5))/(x*(6.2*x+1.7)+0.06);
#else
	LinearColor.rgb = pow(LinearColor,0.4545f);
#endif

	return LinearColor;
}

VertexShaderOutput VertexShaderFinalMix(VertexShaderInput input)
{
    VertexShaderOutput output = (VertexShaderOutput)0;
	
	output.Position = input.Position;
	output.TexCoord.xy = input.TexCoord + HalfPixel;
	return output;
}

float4 PixelShaderFinalMix(VertexShaderOutput input) : COLOR0
{
	half4 shaft = tex2D(shaftSampler , input.TexCoord);
	shaft.rgb *= ShaftTint;

	half3 LinearColor = tex2D(colorSampler, input.TexCoord).rgb;
	
	//the mask is stored in alplha
	float fadeShaft = shaft.a * Blend;
	
	return float4(ToneMap(LinearColor + shaft.rgb * fadeShaft) ,1);	
} 

technique FinalMix // 2
{
    pass Pass1
    {
        VertexShader = compile vs_3_0 VertexShaderFinalMix();
        PixelShader = compile ps_3_0 PixelShaderFinalMix();
    }
}