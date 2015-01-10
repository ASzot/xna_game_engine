sampler BloomSampler : register(s0);
sampler BaseSampler : register(s1);

float BloomIntensity;
float BaseIntensity;

float BloomSaturation;
float BaseSaturation;


// Allows the saturation of the color to be changed.
float4 AdjustSaturation(float4 color, float saturation)
{
    // The constants 0.3, 0.59, and 0.11 are chosen because the
    // human eye is more sensitive to green light, and less to blue.
    float grey = dot(color, float3(0.3, 0.59, 0.11));

	// Interpolate between that constant color and the saturation value.
    return lerp(grey, color, saturation);
}

struct PSInput
{
	float2 TexCoord : TEXCOORD0;
};

float4 PS(PSInput pin) : COLOR0
{
    // Get both the bloom and the base image colors.
    float4 bloom = tex2D(BloomSampler, pin.TexCoord);
    float4 base = tex2D(BaseSampler, pin.TexCoord);
    
    // Adjust color saturation and intensity.
    bloom = AdjustSaturation(bloom, BloomSaturation) * BloomIntensity;
    base = AdjustSaturation(base, BaseSaturation) * BaseIntensity;
    
    // Darken down the base image in areas where there is a lot of bloom,
    // to prevent things looking excessively burned-out.
    base *= (1 - saturate(bloom));
    
    // Combine images.
    return base + bloom;
}


technique BloomCombine
{
    pass Pass1
    {
        PixelShader = compile ps_2_0 PS();
    }
}