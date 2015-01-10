// Code derived from the following source: http://jcoluna.wordpress.com

struct VSInput
{
    float3 Position : POSITION0;
};

struct PSInput
{
    float4 Position : POSITION0;
};

PSInput VS(VSInput vin)
{
    PSInput vout;

	// Just output position in world space.
    vout.Position = float4(vin.Position, 1);

    return vout;
}

// We are outputing a structure which renders to the targets of the two set render targets.
struct PSOut
{
    float4 Normal : COLOR0;
    float4 Depth : COLOR1;
};

PSOut PS(PSInput pin)
{
    PSOut pout;
	// This will generate a (0,0,-1) normal
    pout.Normal = float4(0.5, 0.5, 0, 0);   
    
	// Ouput the max depth.
    pout.Depth = 1.0f;


    return pout;
}

technique Technique1
{
    pass Pass1
    {
        VertexShader = compile vs_2_0 VS();
        PixelShader = compile ps_2_0 PS();
    }
}