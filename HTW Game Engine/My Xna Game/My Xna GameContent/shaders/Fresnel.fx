float4x4 WorldViewProjection;
float4x4 World;
float4x4 WorldView;

float TotalTime;

struct VertexShaderInput
{
    float4 Position : POSITION0;
	float3 Normal : NORMAL;
};

struct VertexShaderOutput
{
    float4 Position : POSITION0;
	float3 Normal	: TEXCOORD0;
	float3 ViewVector		:TEXCOORD1;
};

VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
    VertexShaderOutput output;

    output.Position = mul(input.Position, WorldViewProjection);	
    output.Normal = normalize(mul(float4(input.Normal,0),WorldView).xyz); 
    output.ViewVector =  normalize(mul(input.Position, WorldView).xyz);	
	
	return output;
}

float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0
{	
	float3 normal = normalize(input.Normal);
	float3 viewVec = -normalize(input.ViewVector);
	float fresnel = 1 - saturate(dot(normal,viewVec));

	//pulse the fresnel power, using the TotalTime variable
	fresnel = pow(fresnel,4 + cos(TotalTime*20));
    return fresnel;
}

technique FresnelSimple
{
    pass Pass1
    {
		CullMode = CCW;
		ZENABLE = True;
		ZFUNC = LESSEQUAL;
		ZWRITEENABLE = False;		
		DESTBLEND = ONE;
		DESTBLENDALPHA = ONE;
		SRCBLEND = ONE;
		SRCBLENDALPHA = ONE;
		
        VertexShader = compile vs_2_0 VertexShaderFunction();
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}