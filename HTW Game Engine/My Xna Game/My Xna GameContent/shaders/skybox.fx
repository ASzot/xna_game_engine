float4x4 WorldViewProjection;
float4x4 World;
float4x4 WorldView;



texture DiffuseMap;
sampler diffuseMapSampler = sampler_state
{
	Texture = (DiffuseMap);
	MAGFILTER = LINEAR;
	MINFILTER = LINEAR;
	MIPFILTER = LINEAR;
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
    float2 TexCoord	: TEXCOORD0;
};

VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
    VertexShaderOutput output;

	//handle the position as direction
	float4 hPos = float4(input.Position.xyz,0);
 
	//we should set z and w to the same value, so we will have the skybox at the far plane 
    output.Position = mul(hPos, WorldViewProjection).xyww;
	output.TexCoord = input.TexCoord;

	return output;
}

float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0
{	
	return tex2D(diffuseMapSampler, input.TexCoord);
}

technique Skybox
{
    pass Pass1
    {
		CullMode = None;
		ZENABLE = True;
		ZFUNC = LESSEQUAL;
		ZWRITEENABLE = False;		
		
        VertexShader = compile vs_2_0 VertexShaderFunction();
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}