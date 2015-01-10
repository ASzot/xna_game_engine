float4x4 World;
float4x4 View;
float4x4 Projection;
float4x4 TexTransform;

texture WaterBumpMap;
texture RefractionMap;

float WaveLength;
float WaveHeight;

float4 WaterColor;
float WaterColorFactor;

float3 CamPos;

float Time;
float TransSpeed;
float3 TransDir;

sampler RefractionSampler = 
sampler_state 
{ 
	texture = <RefractionMap>; 
	magfilter = LINEAR; 
	minfilter = LINEAR; 
	mipfilter=LINEAR; 
	AddressU = clamp; 
	AddressV = clamp;
};

sampler WaterBumpMapSampler = 
sampler_state 
{ 
	texture = <WaterBumpMap>; 
	magfilter = LINEAR; 
	minfilter = LINEAR; 
	mipfilter=LINEAR; 
	AddressU = mirror; 
	AddressV = mirror;
};

struct VertexShaderInput
{
    float4 Position					: POSITION;
	float2 TexCoord					: TEXCOORD;
};

struct VertexShaderOutput
{
    float4 Position                 : POSITION;
    float2 BumpMapSamplingPos       : TEXCOORD2;

    float4 RefractionMapSamplingPos : TEXCOORD3;
    float4 WorldPos					: TEXCOORD4;
};

VertexShaderOutput VertexShaderFunction(VertexShaderInput vin)
{
    VertexShaderOutput vout = (VertexShaderOutput)0;

	vin.TexCoord = mul(float4(vin.TexCoord, 0.0, 1.0), TexTransform).xy;

    float4x4 preViewProjection = mul (View, Projection);
    float4x4 preWorldViewProjection = mul (World, preViewProjection);

    vout.Position = mul(vin.Position, preWorldViewProjection);
    vout.BumpMapSamplingPos = vin.TexCoord / WaveLength;
    vout.RefractionMapSamplingPos = mul(vin.Position, preWorldViewProjection);
    vout.WorldPos = mul(vin.Position, World);

    float3 transDir = normalize(TransDir);    
    float3 perpDir = cross(TransDir, float3(0, 1, 0));
    float ydot = dot(vin.TexCoord, TransDir.xz);
    float xdot = dot(vin.TexCoord, perpDir.xz);
    float2 moveVector = float2(xdot, ydot);
    moveVector.y += Time * TransSpeed;
	  
    vout.BumpMapSamplingPos = moveVector / WaveLength;  
	

    return vout;
}

float4 PixelShaderFunction(VertexShaderOutput pin) : COLOR0
{      
    float4 bumpColor = tex2D(WaterBumpMapSampler, pin.BumpMapSamplingPos);
    float2 perturbation = WaveHeight * (bumpColor.rg - 0.5f) * 2.0f;
	
    float2 projectedRefrTexCoords;
    projectedRefrTexCoords.x = pin.RefractionMapSamplingPos.x / pin.RefractionMapSamplingPos.w / 2.0f + 0.5f;
    projectedRefrTexCoords.y = -pin.RefractionMapSamplingPos.y / pin.RefractionMapSamplingPos.w / 2.0f + 0.5f;    
    float2 perturbatedRefrTexCoords = projectedRefrTexCoords + perturbation;    
    float4 refractiveColor = tex2D(RefractionSampler, perturbatedRefrTexCoords);
    
    float4 finalColor = lerp(refractiveColor, WaterColor, WaterColorFactor);
    
	return finalColor;
}

technique PlanarWaterTechnique
{
    pass Pass1
    {
        VertexShader = compile vs_3_0 VertexShaderFunction();
        PixelShader = compile ps_3_0 PixelShaderFunction();
    }
}
