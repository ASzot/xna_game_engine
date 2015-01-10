//-----------------------------------------------------------------------------
// LPPMainEffect.fx
//
// Jorge Adriano Luna 2011
// http://jcoluna.wordpress.com
//
// It uses some code from Nomal Mapping Sample found at
// http://create.msdn.com/en-US/education/catalog/sample/normal_mapping
// and also code from here
// http://aras-p.info/texts/CompactNormalStorage.html
//-----------------------------------------------------------------------------


//-----------------------------------------
// Parameters
//-----------------------------------------
float4x4 World;
float4x4 WorldView;
float4x4 View;
float4x4 Projection;
float4x4 WorldViewProjection;
float4x4 LightViewProj; //used when rendering to shadow map

bool UseDiffuseMat = false;
bool UseSpecularMat = false;
float4 DiffuseMat;
float3 SpecularMat;

float4x4 TexTransform;

float FarClip;
float2 LightBufferPixelSize;

//as we used a 0.1f scale when rendering to light buffer,
//revert it back here.
const static float LightBufferScaleInv = 10.0f;

float4 AmbientColor;

bool Clipping = false;
float4 ClipPlane;

float2 ParallaxScaleBias;

//we should use one of these 4 defines to compute ambient color:
//NO_AMBIENT -- default, you don't need to define it
//AMBIENT_COLOR --we use an external variable to modulate the diffuse color
//AMBIENT_CUBEMAP --we use a cubemap to encode ambient light information

#define AMBIENT_COLOR

float AlphaReference;

//-----------------------------------------
// Textures
//-----------------------------------------
texture DiffuseMap;
sampler diffuseMapSampler = sampler_state
{
	Texture = (DiffuseMap);
	MAGFILTER = LINEAR;
	MINFILTER = LINEAR;
	MIPFILTER = LINEAR;
	AddressU = Wrap;
	AddressV = Wrap;
};

texture SpecularMap;
sampler specularMapSampler = sampler_state
{
	Texture = (SpecularMap);
	MAGFILTER = LINEAR;
	MINFILTER = LINEAR;
	MIPFILTER = LINEAR;
	AddressU = Wrap;
	AddressV = Wrap;
};

texture NormalMap;
sampler normalMapSampler = sampler_state
{
	Texture = (NormalMap);
	MAGFILTER = LINEAR;
	MINFILTER = LINEAR;
	MIPFILTER = LINEAR;
	AddressU = Wrap;
	AddressV = Wrap;
};

texture EmissiveMap;
sampler emissiveMapSampler = sampler_state
{
	Texture = (EmissiveMap);
	MAGFILTER = LINEAR;
	MINFILTER = LINEAR;
	MIPFILTER = LINEAR;
	AddressU = Wrap;
	AddressV = Wrap;
};

texture LightBuffer;
sampler2D lightSampler = sampler_state
{
	Texture = <LightBuffer>;
	MipFilter = POINT;
	MagFilter = POINT;
	MinFilter = POINT;
	AddressU = Clamp;
	AddressV = Clamp;
};


texture LightSpecularBuffer;
sampler2D lightSpecularSampler = sampler_state
{
	Texture = <LightSpecularBuffer>;
	MipFilter = NONE;
	MagFilter = POINT;
	MinFilter = POINT;
	AddressU = Clamp;
	AddressV = Clamp;
};



#ifdef DUAL_LAYER
texture SecondDiffuseMap;
sampler secondDiffuseMapSampler = sampler_state
{
	Texture = (SecondDiffuseMap);
	MAGFILTER = LINEAR;
	MINFILTER = LINEAR;
	MIPFILTER = LINEAR;
	AddressU = Wrap;
	AddressV = Wrap;
};


texture SecondSpecularMap;
sampler secondSpecularMapSampler = sampler_state
{
	Texture = (SecondSpecularMap);
	MAGFILTER = LINEAR;
	MINFILTER = LINEAR;
	MIPFILTER = LINEAR;
	AddressU = Wrap;
	AddressV = Wrap;
};


texture SecondNormalMap;
sampler secondNormalMapSampler = sampler_state
{
	Texture = (SecondNormalMap);
	MAGFILTER = LINEAR;
	MINFILTER = LINEAR;
	MIPFILTER = LINEAR;
	AddressU = Wrap;
	AddressV = Wrap;
};
#endif

#ifdef AMBIENT_CUBEMAP

texture AmbientCubeMap;
samplerCUBE ambientCubemapSampler = sampler_state
{
	Texture = <AmbientCubeMap>;
	MinFilter=LINEAR;
	MagFilter=LINEAR;
	MipFilter=LINEAR;
	AddressU = WRAP;
	AddressV = WRAP;
};
#endif
//-------------------------------
// Helper functions
//-------------------------------
half2 EncodeNormal (half3 n)
{
	float kScale = 1.7777;
	float2 enc;
	enc = n.xy / (n.z+1);
	enc /= kScale;
	enc = enc * 0.5 + 0.5;
	return enc;
}

float2 PostProjectionSpaceToScreenSpace(float4 pos)
{
	float2 screenPos = pos.xy / pos.w;
	return (0.5f * (float2(screenPos.x, -screenPos.y) + 1));
}

half3 NormalMapToSpaceNormal(half3 normalMap, float3 normal, float3 binormal, float3 tangent)
{
	normalMap = normalMap * 2 - 1;
	normalMap = half3(normal * normalMap.z + normalMap.x * tangent - normalMap.y * binormal);
	return normalMap;
}	


//-------------------------------
// Shaders
//-------------------------------

#ifdef SKINNED_MESH

#define MaxBones 60
float4x4 Bones[MaxBones];

#endif

struct VertexShaderInput
{
    float4 Position : POSITION0;
    float2 TexCoord : TEXCOORD0;
    float3 Normal	: NORMAL0;
	float3 Binormal  : BINORMAL0;
	float3 Tangent  : TANGENT;
#ifdef SKINNED_MESH
    float4 BoneIndices : BLENDINDICES0;
    float4 BoneWeights : BLENDWEIGHT0;
#endif
#ifdef DUAL_LAYER
    float4 Color   : COLOR0;		
#endif
};


struct VertexShaderOutput
{
    float4 Position			: POSITION0;
    float3 TexCoord			: TEXCOORD0;
    float Depth				: TEXCOORD1;
	
    float3 Normal			: TEXCOORD2;
    float3 Tangent			: TEXCOORD3;
    float3 Binormal			: TEXCOORD4; 
	float4 ClipDistances	: TEXCOORD5; 
	//float3 LightDir			: TEXCOORD6;
};

struct PixelShaderInput
{
    float4 Position			: POSITION0;
    float3 TexCoord			: TEXCOORD0;
    float Depth				: TEXCOORD1;
	
    float3 Normal			: TEXCOORD2;
    float3 Tangent			: TEXCOORD3;
    float3 Binormal			: TEXCOORD4;
	float4 ClipDistances	: TEXCOORD5; 	
	//float3 LightDir			: TEXCOORD6;
	
	//we need this to detect back bacing triangles
	float Face : VFACE;
};
VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
    VertexShaderOutput output = (VertexShaderOutput)0;

	
#ifdef SKINNED_MESH
	// Blend between the weighted bone matrices.
    float4x4 skinTransform = 0;
    
    skinTransform += Bones[input.BoneIndices.x] * input.BoneWeights.x;
    skinTransform += Bones[input.BoneIndices.y] * input.BoneWeights.y;
    skinTransform += Bones[input.BoneIndices.z] * input.BoneWeights.z;
    skinTransform += Bones[input.BoneIndices.w] * input.BoneWeights.w;
	
	float4 skinPos = mul(input.Position, skinTransform);
	float3 skinNormal = mul(input.Normal, skinTransform);
	float3 skinTangent = mul(input.Tangent, skinTransform);
	float3 skinBinormal = mul(input.Binormal, skinTransform);
	
	float3 viewSpacePos = mul(skinPos, WorldView);
    output.Position = mul(skinPos, WorldViewProjection);
	
    output.TexCoord.xy = mul(float4(input.TexCoord, 0.0, 1.0), TexTransform).xy; //pass the texture coordinates further
	
	//we output our normals/tangents/binormals in viewspace
    output.Normal   = mul(skinNormal,WorldView); 
	output.Tangent  = mul(skinTangent,WorldView); 
	output.Binormal = mul(skinBinormal,WorldView); 
	// No parallax mapping with skinning.
	//output.LightDir = float3(0, 0, 0);
#else

	float3 viewSpacePos = mul(input.Position, WorldView);
    output.Position = mul(input.Position, WorldViewProjection);
    output.TexCoord.xy = mul(float4(input.TexCoord, 0.0, 1.0), TexTransform).xy; //pass the texture coordinates further

	//we output our normals/tangents/binormals in viewspace
	output.Normal = normalize(mul(input.Normal,WorldView)); 
	output.Tangent =  normalize(mul(input.Tangent,WorldView)); 
	output.Binormal =  normalize(mul(input.Binormal,WorldView)); 
	
	//float3x3 tbn = (float3x3)0;
	//tbn[0] = normalize(mul(input.Binormal, World));
	//tbn[1] = normalize(mul(input.Tangent, World));
	//tbn[2] = normalize(mul(input.Normal, World));
	//output.LightDir = 
#endif

#ifdef DUAL_LAYER
    output.TexCoord.z = input.Color.r;	
#endif
	
	output.ClipDistances = dot(input.Position, ClipPlane);	
	
	output.Depth = viewSpacePos.z; //pass depth.

    return output;
}
//render to our 2 render targets, normal and depth 
struct PixelShaderOutput
{
    float4 Normal : COLOR0;
    float4 Depth : COLOR1;
};

PixelShaderOutput PixelShaderFunction(PixelShaderInput input, uniform bool alphaMask, uniform bool useParallax)
{
	PixelShaderOutput output = (PixelShaderOutput)1;   

	//if (useParallax)
	//{
	//	float height = tex2D(heightMapSampler, input.TexCoord);

	//	height = height * scaleBias.x + scaleBias.y;
	//	input.TexCoord = input.TexCoord + (height * 
	//}

	//if we are using alpha mask, we need to read the diffuse map	
	if (alphaMask)
	{
		half4 diffuseMap = tex2D(diffuseMapSampler, input.TexCoord);
		clip(diffuseMap.a - AlphaReference);	
	}
	
#ifdef DUAL_LAYER
	float transition = tex2D(diffuseMapSampler, input.TexCoord).a;
	transition = (1.3f*input.TexCoord.z - 0.15f) - transition;		
	transition = saturate(transition * 5);
#endif
	//read from our normal map
	half4 normalMap = tex2D(normalMapSampler, input.TexCoord);
		
#ifdef DUAL_LAYER
	normalMap= normalMap * transition + tex2D(secondNormalMapSampler, input.TexCoord) * (1 - transition);
#endif

	half3 normalViewSpace = NormalMapToSpaceNormal(normalMap.xyz, input.Normal, input.Binormal, input.Tangent);
    
	//if we are using alpha mask, we need to invert the normal if its a back face
	if (alphaMask)	
		normalViewSpace = normalViewSpace * sign(input.Face);

	output.Normal.rg =  EncodeNormal (normalize(normalViewSpace));	//our encoder output in RG channels
	output.Normal.b = normalMap.a;			//our specular power goes into B channel
	output.Normal.a = 1;					//not used
	output.Depth.x = -input.Depth/ FarClip;		//output Depth in linear space, [0..1]

	if (Clipping)
		clip(input.ClipDistances);
	
	return output;
}


struct ReconstructVertexShaderInput
{
    float4 Position  : POSITION0;
    float2 TexCoord  : TEXCOORD0;
#ifdef AMBIENT_CUBEMAP
	float3 Normal	 : NORMAL0;
#endif
#ifdef SKINNED_MESH
    float4 BoneIndices : BLENDINDICES0;
    float4 BoneWeights : BLENDWEIGHT0;
#endif
#ifdef DUAL_LAYER
    float4 Color   : COLOR0;		
#endif
};




struct ReconstructVertexShaderOutput
{
    float4 Position				: POSITION0;
    float3 TexCoord				: TEXCOORD0;
	float4 TexCoordScreenSpace	: TEXCOORD1;
	float4 ClipDistances		: TEXCOORD2;
#ifdef AMBIENT_CUBEMAP
	float3 Normal				: TEXCOORD3;
#endif
};

ReconstructVertexShaderOutput ReconstructVertexShaderFunction(ReconstructVertexShaderInput input)
{
    ReconstructVertexShaderOutput output=(ReconstructVertexShaderOutput)0;

#ifdef SKINNED_MESH
	// Blend between the weighted bone matrices.
    float4x4 skinTransform = 0;
    
    skinTransform += Bones[input.BoneIndices.x] * input.BoneWeights.x;
    skinTransform += Bones[input.BoneIndices.y] * input.BoneWeights.y;
    skinTransform += Bones[input.BoneIndices.z] * input.BoneWeights.z;
    skinTransform += Bones[input.BoneIndices.w] * input.BoneWeights.w;

	float4 skinPos = mul(input.Position, skinTransform);

    output.Position = mul(skinPos, WorldViewProjection);
	#ifdef AMBIENT_CUBEMAP
	float3 skinNormal = mul(input.Normal, skinTransform);
	output.Normal = normalize(mul(skinNormal, World)); 
	#endif

#else

    output.Position = mul(input.Position, WorldViewProjection);

	#ifdef AMBIENT_CUBEMAP
	output.Normal = normalize(mul(input.Normal,World)); 
	#endif
#endif

    output.TexCoord.xy = mul(float4(input.TexCoord, 0.0, 1.0), TexTransform).xy; //pass the texture coordinates further
	output.TexCoordScreenSpace = output.Position;
	
#ifdef DUAL_LAYER
    output.TexCoord.z = input.Color.r;	
#endif

	output.ClipDistances = dot(input.Position, ClipPlane);

    return output;
}

float4 ReconstructPixelShaderFunction(ReconstructVertexShaderOutput input, uniform bool alphaMask):COLOR0
{
	PixelShaderOutput output = (PixelShaderOutput)1;   
	// Find the screen space texture coordinate and offset it
	float2 screenPos = PostProjectionSpaceToScreenSpace(input.TexCoordScreenSpace) + LightBufferPixelSize;

	//read from our diffuse, specular and emissive map.
	half4 diffuseMap;
	half3 specularMap;
	if (UseDiffuseMat)
	{
		diffuseMap = DiffuseMat;
	}
	else 
	{
		diffuseMap = tex2D(diffuseMapSampler, input.TexCoord);
	}
	if (UseSpecularMat)
	{
		specularMap = SpecularMat;
	}
	else
	{
		tex2D(specularMapSampler, input.TexCoord);
	}
	
	if (alphaMask)
		clip(diffuseMap.a - AlphaReference);
	
	half3 emissiveMap = tex2D(emissiveMapSampler, input.TexCoord).rgb;
	
#ifdef DUAL_LAYER
	float transition = (1.3f * input.TexCoord.z - 0.15f) - diffuseMap.a;		
	transition = saturate(transition * 5);
	diffuseMap.rgb = diffuseMap.rgb * transition + tex2D(secondDiffuseMapSampler, input.TexCoord).rgb * (1 - transition);
	specularMap = specularMap.rgb * transition + tex2D(secondSpecularMapSampler, input.TexCoord).rgb *(1 - transition);
#endif
	
	// We need this magical constant.
	float4 lightColor =  tex2D(lightSampler, screenPos) * LightBufferScaleInv;

	// Specular Intensity is different different texture.
	float4 specular =  tex2D(lightSpecularSampler, screenPos) * LightBufferScaleInv;
	
	float4 finalColor = float4(diffuseMap * lightColor.rgb + specular * specularMap + emissiveMap,1);

#ifdef AMBIENT_COLOR
	//add a small constant to avoid dark areas
	finalColor.rgb+= diffuseMap * AmbientColor.rgb;
#elif defined(AMBIENT_CUBEMAP)
	//fetch ambient cubemap using vertex normal. Mabye you will want to use the per-pixel normal. In this case,
	//you should fetch the normal buffer as we do with the lightBuffer, and recompute the global-space normal
	half3 ambientCubemapColor = texCUBE(ambientCubemapSampler, input.Normal);
	finalColor.rgb += AmbientColor.rgb * ambientCubemapColor.rgb * diffuseMap.rgb;
#endif

	if (Clipping)
		clip(input.ClipDistances);

	return finalColor;
}


struct ShadowMapVertexShaderInput
{
    float4 Position : POSITION0;	
	//if we have alpha mask, we need to use the tex coord
#ifdef SKINNED_MESH
    float4 BoneIndices : BLENDINDICES0;
    float4 BoneWeights : BLENDWEIGHT0;
#endif

};

struct ShadowMapVertexShaderOutput
{
    float4 Position : POSITION0;
	float2 Depth : TEXCOORD0;
};



ShadowMapVertexShaderOutput OutputShadowVertexShaderFunction(ShadowMapVertexShaderInput input)
{
    ShadowMapVertexShaderOutput output = (ShadowMapVertexShaderOutput)0;
	
#ifdef SKINNED_MESH
	// Blend between the weighted bone matrices.
    float4x4 skinTransform = 0;
    
    skinTransform += Bones[input.BoneIndices.x] * input.BoneWeights.x;
    skinTransform += Bones[input.BoneIndices.y] * input.BoneWeights.y;
    skinTransform += Bones[input.BoneIndices.z] * input.BoneWeights.z;
    skinTransform += Bones[input.BoneIndices.w] * input.BoneWeights.w;

	float4 skinPos = mul(input.Position, skinTransform);
    float4 clipPos = mul(skinPos, mul(World, LightViewProj));
#else
    float4 clipPos = mul(input.Position, mul(World, LightViewProj));
#endif
	//clamp to the near plane
	clipPos.z = max(clipPos.z,0);
	
	output.Position = clipPos;
	output.Depth = output.Position.zw;
	
    return output;
}

float4 OutputShadowPixelShaderFunction(ShadowMapVertexShaderOutput input) : COLOR0
{

    float depth = input.Depth.x / input.Depth.y;	
    return float4(depth, 1, 1, 1); 
}



struct ShadowMapVertexShaderAlphaInput
{
    float4 Position : POSITION0;	
	//if we have alpha mask, we need to use the tex coord
    float2 TexCoord  : TEXCOORD0;
#ifdef SKINNED_MESH
    float4 BoneIndices : BLENDINDICES0;
    float4 BoneWeights : BLENDWEIGHT0;
#endif

};

struct ShadowMapVertexShaderAlphaOutput
{
    float4 Position : POSITION0;
	float2 Depth : TEXCOORD0;
    float2 TexCoord  : TEXCOORD1;
};

ShadowMapVertexShaderAlphaOutput OutputShadowVertexShaderAlphaMaskFunction(ShadowMapVertexShaderAlphaInput input)
{
    ShadowMapVertexShaderAlphaOutput output = (ShadowMapVertexShaderAlphaOutput)0;
	
#ifdef SKINNED_MESH
	// Blend between the weighted bone matrices.
    float4x4 skinTransform = 0;
    
    skinTransform += Bones[input.BoneIndices.x] * input.BoneWeights.x;
    skinTransform += Bones[input.BoneIndices.y] * input.BoneWeights.y;
    skinTransform += Bones[input.BoneIndices.z] * input.BoneWeights.z;
    skinTransform += Bones[input.BoneIndices.w] * input.BoneWeights.w;

	float4 skinPos = mul(input.Position, skinTransform);
    float4 clipPos = mul(skinPos, mul(World, LightViewProj));
#else
    float4 clipPos = mul(input.Position, mul(World, LightViewProj));
#endif
	//clamp to the near plane
	clipPos.z = max(clipPos.z,0);
	
	output.Position = clipPos;
	output.Depth = output.Position.zw;
	
    output.TexCoord = input.TexCoord; //pass the texture coordinates further	

    return output;
}

float4 OutputShadowPixelShaderAlphaMaskFunction(ShadowMapVertexShaderAlphaOutput input) : COLOR0
{
	//read our diffuse
	half4 diffuseMap = tex2D(diffuseMapSampler, input.TexCoord);
	clip(diffuseMap.a - AlphaReference);

    float depth = input.Depth.x / input.Depth.y;	
    return float4(depth, 1, 1, 1); 
}




technique RenderToGBuffer
{
    pass RenderToGBufferPass
    {
		CullMode = CCW;

        VertexShader = compile vs_3_0 VertexShaderFunction();
        PixelShader = compile ps_3_0 PixelShaderFunction(false, false);
    }
}

technique ReconstructShading
{
	pass ReconstructShadingPass
    {
		CullMode = CCW;

        VertexShader = compile vs_3_0 ReconstructVertexShaderFunction();
        PixelShader = compile ps_3_0 ReconstructPixelShaderFunction(false);
    }
}

technique OutputShadow
{
	pass OutputShadowPass
	{		
		CullMode = CCW;

        VertexShader = compile vs_3_0 OutputShadowVertexShaderFunction();
        PixelShader = compile ps_3_0 OutputShadowPixelShaderFunction();
	}
}

technique RenderToGBufferAlpha
{
	pass RenderToGBufferPass
	{
		CullMode = None;

		VertexShader = compile vs_3_0 VertexShaderFunction();
		PixelShader = compile ps_3_0 PixelShaderFunction(true, false);
	}
}

technique ReconstructShadingAlpha
{
	pass ReconstructShadingPass
    {
		CullMode = None;

        VertexShader = compile vs_3_0 ReconstructVertexShaderFunction();
        PixelShader = compile ps_3_0 ReconstructPixelShaderFunction(true);
    }
}

technique OutputShadowAlpha
{
	pass OutputShadowPass
	{		
		CullMode = None;

        VertexShader = compile vs_3_0 OutputShadowVertexShaderAlphaMaskFunction();
        PixelShader = compile ps_3_0 OutputShadowPixelShaderAlphaMaskFunction();
	}
}

//technique RenderToGBufferParallax
//{
//	pass RenderToGBufferPass
//    {
//	#ifdef ALPHA_MASKED	
//		CullMode = None;
//	#else
//		CullMode = CCW;
//	#endif

//        VertexShader = compile vs_3_0 VertexShaderFunction();
//        PixelShader = compile ps_3_0 PixelShaderFunction(true);
//    }
//}