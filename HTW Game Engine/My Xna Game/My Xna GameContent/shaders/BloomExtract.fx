// Pixel shader extracts the brighter areas of an image.
// The first step in the bloom post processing effect.

sampler TextureSampler : register(s0);

float BloomThreshold;

struct PSInput
{
	float2 TexCoord : TEXCOORD0;
};

float4 PS(PSInput pin) : COLOR0
{
    // Look up the original image color.
    float4 c = tex2D(TextureSampler, pin.TexCoord);

    // Adjust it to keep only values brighter than the specified threshold.
    return saturate((c - BloomThreshold) / (1 - BloomThreshold));
}


technique BloomExtract
{
    pass Pass1
    {
        PixelShader = compile ps_2_0 PS();
    }
}
