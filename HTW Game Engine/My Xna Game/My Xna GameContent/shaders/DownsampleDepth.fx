//-----------------------------------------------------------------------------
// DownsampleDepth.fx
//
// Jorge Adriano Luna 2011
// http://jcoluna.wordpress.com
//-----------------------------------------------------------------------------


float2 PixelSize;
float2 HalfPixel;

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


struct VertexShaderInput
{
    float4 Position : POSITION0;
	float2 TexCoord : TEXCOORD0;
};


struct VertexShaderOutput
{
    float4 Position : POSITION0;
	float2 TexCoord : TEXCOORD0;
};


VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
    VertexShaderOutput output = (VertexShaderOutput)0;
	
	output.Position = input.Position;
	output.TexCoord.xy = input.TexCoord + HalfPixel;
	return output;
}
float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0
{
    float depth0 = tex2D(depthSampler,input.TexCoord).r;
    float depth1 = tex2D(depthSampler,input.TexCoord + float2(PixelSize.x,0)).r;
    float depth2 = tex2D(depthSampler,input.TexCoord + float2(0,PixelSize.y)).r;
    float depth3 = tex2D(depthSampler,input.TexCoord + PixelSize).r;

	return max(max(depth0,depth1),max(depth2,depth3));
}

technique DownSample
{
    pass Pass1
    {
        VertexShader = compile vs_2_0 VertexShaderFunction();
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}
