//-----------------------------------------------------------------------------
// LightingLPP.fx
//
// Jorge Adriano Luna 2011
// http://jcoluna.wordpress.com
//
// It uses some code from here:
// http://aras-p.info/texts/CompactNormalStorage.html
// http://mynameismjp.wordpress.com/2009/03/10/reconstructing-position-from-depth/
//-----------------------------------------------------------------------------


//-----------------------------------------
// Parameters
//-----------------------------------------
float4 LightColor;
float3 LightDir;
float3 LightPosition;
float InvLightRadiusSqr;
float3 FrustumCorners[4];
float2 GBufferPixelSize;
float FarClip;
float4x4 WorldViewProjection;
float2 TanAspect;
float4x4 CameraTransform;

float2 ShadowMapPixelSize;
float2 ShadowMapSize;
float4x4 MatLightViewProjSpot;
float DepthBias;

float SpotAngle;
float InvSpotAngle;
float SpotExponent;


//Cascade shadow maps parameters
static const int NUM_SPLITS = 3;
float4x4	MatLightViewProj [NUM_SPLITS];
float2		ClipPlanes[NUM_SPLITS];
float3		CascadeDistances;

//we use this to avoid clamping our results into [0..1]. 
//this way, we can fake a [0..10] range, since we are using a
//floating point buffer
const static float LightBufferScale = 0.1f;

//-----------------------------------------
// Textures
//-----------------------------------------
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

texture NormalBuffer;
sampler2D normalSampler = sampler_state
{
	Texture = <NormalBuffer>;
	MipFilter = NONE;
	MagFilter = LINEAR;
	MinFilter = LINEAR;
	AddressU = Clamp;
	AddressV = Clamp;
};


texture ShadowMap;
sampler ShadowMapSampler = sampler_state
{
    Texture = <ShadowMap>;
	MipFilter = POINT;
	MagFilter = POINT;
	MinFilter = POINT;
	AddressU = Clamp;
	AddressV = Clamp;
};
//-------------------------------
// Helper functions
//-------------------------------
half3 DecodeNormal (half4 enc)
{
	float kScale = 1.7777;
	float3 nn = enc.xyz*float3(2*kScale,2*kScale,0) + float3(-kScale,-kScale,1);
	float g = 2.0 / dot(nn.xyz,nn.xyz);
	float3 n;
	n.xy = g*nn.xy;
	n.z = g-1;
	return n;
}

float3 GetFrustumRay(in float2 texCoord)
{
	float index = texCoord.x + (texCoord.y * 2);
	return FrustumCorners[index];
}

float2 PostProjectionSpaceToScreenSpace(float4 pos)
{
	float2 screenPos = pos.xy / pos.w;
	return (0.5f * (float2(screenPos.x, -screenPos.y) + 1));
}


float ComputeAttenuation(float3 lDir)
{
	return 1 - saturate(dot(lDir,lDir)*InvLightRadiusSqr);
}



//-------------------------------
// Shaders
//-------------------------------

struct PixelShaderOutput
{
    float4 Diffuse : COLOR0;
    float4 Specular : COLOR1;
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
	float3 FrustumRay : TEXCOORD1;
};

struct VertexShaderOutputMeshBased
{
    float4 Position : POSITION0;
	float4 TexCoordScreenSpace : TEXCOORD0;
};


VertexShaderOutput PointLightVS(VertexShaderInput input)
{
    VertexShaderOutput output = (VertexShaderOutput)0;
	
	output.Position = input.Position;
	output.TexCoord = input.Position.xy*0.5f + float2(0.5f,0.5f); 
	output.TexCoord.y = 1 - output.TexCoord.y;	
	output.TexCoord += GBufferPixelSize;
	output.FrustumRay = GetFrustumRay(input.TexCoord);
	return output;
}

VertexShaderOutputMeshBased PointLightMeshVS(VertexShaderInput input)
{
    VertexShaderOutputMeshBased output = (VertexShaderOutputMeshBased)0;	
    output.Position = mul(input.Position, WorldViewProjection);

	//we will compute our texture coords based on pixel position further
	output.TexCoordScreenSpace = output.Position;
	return output;
}

PixelShaderOutput PointLightPS(VertexShaderOutput input)
{
	PixelShaderOutput output = (PixelShaderOutput)0;
	//read the depth value
	float depthValue = tex2D(depthSampler, input.TexCoord).r;
	
	//if depth value == 1, we can assume its a background value, so skip it
	clip(-depthValue + 0.9999f);

    // Reconstruct position from the depth value, making use of the ray pointing towards the far clip plane
    float3 pos = input.FrustumRay * depthValue;	
	
	//light direction from current pixel to current light
	float3 lDir = LightPosition - pos;

	//compute attenuation, 1 - saturate(d2/r2)
	float atten = ComputeAttenuation(lDir);
	
	// Convert normal back with the decoding function
	float4 normalMap = tex2D(normalSampler, input.TexCoord);
	float3 normal = DecodeNormal(normalMap);
			
	lDir = normalize(lDir);

	// N dot L lighting term, attenuated
	float nl = saturate(dot(normal, lDir))*atten;
	
	//reject pixels outside our radius or that are not facing the light
	clip(nl -0.00001);

	//As our position is relative to camera position, we dont need to use (ViewPosition - pos) here
	float3 camDir = normalize(pos);
	
	//scale by our constant
	nl*= LightBufferScale;

	// Calculate specular term
	float3 h = normalize(reflect(lDir, normal));
	float spec = nl*pow(saturate(dot(camDir, h)), normalMap.b*100);
	

	output.Diffuse.rgb = LightColor * nl;
	output.Specular.rgb = (LightColor.a*spec)* LightColor.rgb;
	
	//output light
	return output;
}


PixelShaderOutput PointLightMeshPS(VertexShaderOutputMeshBased input)
{
	PixelShaderOutput output = (PixelShaderOutput)0;

	//as we are using a sphere mesh, we need to recompute each pixel position into texture space coords
	float2 screenPos = PostProjectionSpaceToScreenSpace(input.TexCoordScreenSpace) + GBufferPixelSize;
	//read the depth value
	float depthValue = tex2D(depthSampler, screenPos).r;
	
	//if depth value == 1, we can assume its a background value, so skip it
	//we need this only if we are using back-face culling on our light volumes. Otherwise, our z-buffer
	//will reject this pixel anyway
	
	//if depth value == 1, we can assume its a background value, so skip it
	clip(-depthValue + 0.9999f);
	
    // Reconstruct position from the depth value, the FOV, aspect and pixel position
	depthValue*=FarClip;
	//convert screenPos to [-1..1] range
	float3 pos = float3(TanAspect*(screenPos*2 - 1)*depthValue, -depthValue);
	
	//light direction from current pixel to current light
	float3 lDir = LightPosition - pos;

	//compute attenuation, 1 - saturate(d2/r2)
	float atten = ComputeAttenuation(lDir);
	
	// Convert normal back with the decoding function
	float4 normalMap = tex2D(normalSampler, screenPos);
	float3 normal = DecodeNormal(normalMap);
			
	lDir = normalize(lDir);

	// N dot L lighting term, attenuated
	float nl = saturate(dot(normal, lDir))*atten;

	//reject pixels outside our radius or that are not facing the light
	clip(nl -0.00001f);

	//As our position is relative to camera position, we dont need to use (ViewPosition - pos) here
	float3 camDir = normalize(pos);
	
	//scale by our constant
	nl*= LightBufferScale;

	// Calculate specular term
	float3 h = normalize(reflect(lDir, normal));
	float spec = nl*pow(saturate(dot(camDir, h)), normalMap.b*100);	
	
	output.Diffuse.rgb = LightColor * nl;
	output.Specular.rgb = (LightColor.a*spec)* LightColor.rgb;

	//output light
	return output;
}


PixelShaderOutput SpotLightMeshPS(VertexShaderOutputMeshBased input)
{
	PixelShaderOutput output = (PixelShaderOutput)0;

	//as we are using a sphere mesh, we need to recompute each pixel position into texture space coords
	float2 screenPos = PostProjectionSpaceToScreenSpace(input.TexCoordScreenSpace) + GBufferPixelSize;
	//read the depth value
	float depthValue = tex2D(depthSampler, screenPos).r;
	
	//if depth value == 1, we can assume its a background value, so skip it
	//we need this only if we are using back-face culling on our light volumes. Otherwise, our z-buffer
	//will reject this pixel anyway
	
	//if depth value == 1, we can assume its a background value, so skip it
	clip(-depthValue + 0.9999f);

    // Reconstruct position from the depth value, the FOV, aspect and pixel position
	depthValue*=FarClip;
	//convert screenPos to [-1..1] range
	float3 pos = float3(TanAspect*(screenPos*2 - 1)*depthValue, -depthValue);
	
	//light direction from current pixel to current light
	float3 lDir = LightPosition - pos;

	//compute attenuation, 1 - saturate(d2/r2)
	float atten = ComputeAttenuation(lDir);
	
	// Convert normal back with the decoding function
	float4 normalMap = tex2D(normalSampler, screenPos);
	float3 normal = DecodeNormal(normalMap);
			
	lDir = normalize(lDir);

	// N dot L lighting term, attenuated
	float nl = saturate(dot(normal, lDir))*atten;

	//spot light cone
	half spotAtten = min(1,max(0,dot(lDir,LightDir) - SpotAngle)*SpotExponent);
	nl*=spotAtten;
	//reject pixels outside our radius or that are not facing the light
	clip(nl -0.00001f);

	//As our position is relative to camera position, we dont need to use (ViewPosition - pos) here
	float3 camDir = normalize(pos);
		
	//scale by our constant
	nl*= LightBufferScale;

	// Calculate specular term
	float3 h = normalize(reflect(lDir, normal));
	float spec = nl*pow(saturate(dot(camDir, h)), normalMap.b*100);
	
	output.Diffuse.rgb = LightColor * nl;
	output.Specular.rgb = (LightColor.a*spec)* LightColor.rgb;

	//output light
	return output;
}

////////////////////////////////////////
// Helper to compute shadow attenuation
////////////////////////////////////////
float ComputeShadow(float nl, float2 shadowTexCoord, float ourdepth)
{
	// Get the current depth stored in the shadow map
	float shadowdepth = tex2D(ShadowMapSampler, shadowTexCoord).r;    
    
	// Check to see if this pixel is in front or behind the value in the shadow map
	nl = shadowdepth < ourdepth? 0 : nl;
	
	return nl;
}

/////////////////////////////////////////////////
// Helper to compute shadow attenuation with PCF
/////////////////////////////////////////////////
float ComputeShadow4Samples(float nl, float2 shadowTexCoord, float ourdepth)
{
	// Get the current depth stored in the shadow map
	float samples[4];	
	samples[0] = tex2D(ShadowMapSampler, shadowTexCoord ).r < ourdepth ? 0 : 1;
	samples[1] = tex2D(ShadowMapSampler, shadowTexCoord + float2( 0,2)*ShadowMapPixelSize).r < ourdepth ? 0 : 1;
	samples[2] = tex2D(ShadowMapSampler, shadowTexCoord + float2( 2,0)*ShadowMapPixelSize).r < ourdepth ? 0 : 1;
	samples[3] = tex2D(ShadowMapSampler, shadowTexCoord + float2( 2,2)*ShadowMapPixelSize).r < ourdepth ? 0 : 1;
    
	// Determine the lerp amounts           
	float2 lerps = frac(shadowTexCoord*ShadowMapSize);
	// lerp between the shadow values to calculate our light amount
	half shadow = lerp(lerp(samples[0], samples[1], lerps.y), lerp( samples[2], samples[3], lerps.y), lerps.x);							  
				
	return nl*shadow;
}


//////////////////////////////////////////////////////
// Pixel shader to compute spot lights with shadows
//////////////////////////////////////////////////////
PixelShaderOutput SpotLightMeshShadowPS(VertexShaderOutputMeshBased input)
{

	PixelShaderOutput output = (PixelShaderOutput)0;

	//as we are using a sphere mesh, we need to recompute each pixel position into texture space coords
	float2 screenPos = PostProjectionSpaceToScreenSpace(input.TexCoordScreenSpace) + GBufferPixelSize;
	//read the depth value
	float depthValue = tex2D(depthSampler, screenPos).r;
	
	//if depth value == 1, we can assume its a background value, so skip it
	//we need this only if we are using back-face culling on our light volumes. Otherwise, our z-buffer
	//will reject this pixel anyway
	
	//if depth value == 1, we can assume its a background value, so skip it
	clip(-depthValue + 0.9999f);

    // Reconstruct position from the depth value, the FOV, aspect and pixel position
	depthValue*=FarClip;
	//convert screenPos to [-1..1] range
	float3 pos = float3(TanAspect*(screenPos*2 - 1)*depthValue, -depthValue);
	
	//light direction from current pixel to current light
	float3 lDir = LightPosition - pos;

	//compute attenuation, 1 - saturate(d2/r2)
	float atten = ComputeAttenuation(lDir);
	
	// Convert normal back with the decoding function
	float4 normalMap = tex2D(normalSampler, screenPos);
	float3 normal = DecodeNormal(normalMap);
			
	lDir = normalize(lDir);

	// N dot L lighting term, attenuated
	float nl = saturate(dot(normal, lDir))*atten;

	//spot light cone
	half spotAtten = min(1,max(0,dot(lDir,LightDir) - SpotAngle)*SpotExponent);
	nl*=spotAtten;
	//reject pixels outside our radius or that are not facing the light
	clip(nl -0.00001f);


	//compute shadow attenuation	
	float4 lightPosition = mul(mul(float4(pos,1),CameraTransform), MatLightViewProjSpot);

	// Find the position in the shadow map for this pixel
	float2 shadowTexCoord = 0.5 * lightPosition.xy / 
							lightPosition.w + float2( 0.5, 0.5 );
	shadowTexCoord.y = 1.0f - shadowTexCoord.y;
	//offset by the texel size
	shadowTexCoord += ShadowMapPixelSize;
	
	// Calculate the current pixel depth
	// The bias is used to prevent floating point errors 
	float ourdepth = (lightPosition.z / lightPosition.w) - DepthBias;
	
	nl = ComputeShadow4Samples(nl, shadowTexCoord, ourdepth);
		
	float4 finalColor;
	//As our position is relative to camera position, we dont need to use (ViewPosition - pos) here
	float3 camDir = normalize(pos);
	
	//scale by our constant
	nl*= LightBufferScale;

	// Calculate specular term
	float3 h = normalize(reflect(lDir, normal));
	float spec = nl*pow(saturate(dot(camDir, h)), normalMap.b*100);
	
	output.Diffuse.rgb = LightColor * nl;
	output.Specular.rgb = (LightColor.a*spec)* LightColor.rgb;

	//output light
	return output;
}
VertexShaderOutput DirectionalLightVS(VertexShaderInput input)
{
    VertexShaderOutput output = (VertexShaderOutput)0;
	
	output.Position = input.Position;
	output.TexCoord = input.TexCoord +GBufferPixelSize;
	output.FrustumRay = GetFrustumRay(input.TexCoord);
	return output;
}


PixelShaderOutput DirectionalLightPS(VertexShaderOutput input)
{
	PixelShaderOutput output = (PixelShaderOutput)0;

	// If we want the WorldPosition, we have to multiply by the world camera matrix
	float depthValue = tex2D(depthSampler, input.TexCoord).r;
	
	//if depth value == 1, we can assume its a background value, so skip it
	clip(-depthValue + 0.9999f);

    float3 pos = input.FrustumRay * depthValue;
	
	// Convert normal back with the decoding function
	float4 normalMap = tex2D(normalSampler,  input.TexCoord);
	float3 normal = DecodeNormal(normalMap);

	float nl = saturate(dot(normal, LightDir));
	
	clip(nl - 0.00001f);
	
	//As our position is relative to camera position, we dont need to use (ViewPosition - pos) here
	float3 camDir = normalize(pos);
		
	//scale by our constant
	nl*= LightBufferScale;

	// Calculate specular term
	float3 h = normalize(reflect(LightDir, normal)); 
	float spec = nl*pow(saturate(dot(camDir, h)), normalMap.b*100);
	
	output.Diffuse.rgb = LightColor * nl;
	output.Specular.rgb = (LightColor.a*spec)* LightColor.rgb;

	//output light
	return output;
}

PixelShaderOutput DirectionalLightShadowPS(VertexShaderOutput input)
{
	PixelShaderOutput output = (PixelShaderOutput)0;

	// If we want the WorldPosition, we have to multiply by the world camera matrix
	float depthValue = tex2D(depthSampler, input.TexCoord).r;
	
	//if depth value == 1, we can assume its a background value, so skip it
	clip(-depthValue + 0.9999f);

    float3 pos = input.FrustumRay * depthValue;
	
	// Convert normal back with the decoding function
	float4 normalMap = tex2D(normalSampler,  input.TexCoord);
	float3 normal = DecodeNormal(normalMap);

	float nl = saturate(dot(normal, LightDir));
	
	float spec = 0;

	clip(nl - 0.00001f);

	{
		// Figure out which split this pixel belongs to, based on view-space depth.
		float3 weights = ( pos.z < CascadeDistances );
		weights.xy -= weights.yz;


		float4x4 lightViewProj = MatLightViewProj[0]*weights.x + MatLightViewProj[1]*weights.y + MatLightViewProj[2]*weights.z;		

		//remember that we need to find the correct cascade into our cascade atlas
		float fOffset = weights.y*0.33333f + weights.z*0.666666f;


		// Find the position of this pixel in light space
		float4 lightingPosition = mul(mul(float4(pos,1),CameraTransform), lightViewProj);
    
		// Find the position in the shadow map for this pixel
		float2 shadowTexCoord = 0.5 * lightingPosition.xy / 
								lightingPosition.w + float2( 0.5, 0.5 );

		shadowTexCoord.x = shadowTexCoord.x *0.3333333f + fOffset;
		shadowTexCoord.y = 1.0f - shadowTexCoord.y;
		shadowTexCoord += ShadowMapPixelSize;

		// Calculate the current pixel depth
		// The bias is used to prevent floating point errors that occur when
		// the pixel of the occluder is being drawn
		float ourdepth = (lightingPosition.z / lightingPosition.w) - DepthBias;

		//the pixel can be outside of shadow distance, so skip it in that case
		float shadowSkip = ClipPlanes[2].y > pos.z;
		nl = nl*shadowSkip + ComputeShadow4Samples(nl, shadowTexCoord, ourdepth)*(1-shadowSkip);   
		
	}

	//As our position is relative to camera position, we dont need to use (ViewPosition - pos) here
	float3 camDir = normalize(pos);
	float3 h = normalize(reflect(LightDir, normal)); 
	
	//scale by our constant
	nl*= LightBufferScale;
	spec = nl*pow(saturate(dot(camDir, h)), normalMap.b*100);
	
	output.Diffuse.rgb = LightColor * nl;
	output.Specular.rgb = (LightColor.a*spec)* LightColor.rgb;

	//output light
	return output;
}

//tech 0
technique PointTechnique
{
    pass PointLight
    {
        VertexShader = compile vs_2_0 PointLightVS();
        PixelShader = compile ps_2_0 PointLightPS();
    }
}
//tech 1
technique PointMeshTechnique
{
    pass PointLight
    {
        VertexShader = compile vs_2_0 PointLightMeshVS();
        PixelShader = compile ps_2_0 PointLightMeshPS();
    }
}

//tech 2
technique DirectionalTechnique
{
    pass DirectionalLight
    {
        VertexShader = compile vs_2_0 DirectionalLightVS();
        PixelShader = compile ps_2_0 DirectionalLightPS();
    }
}

//tech 3
technique SpotMeshTechnique
{
    pass SpotLight
    {
        VertexShader = compile vs_2_0 PointLightMeshVS();
        PixelShader = compile ps_2_0 SpotLightMeshPS();
    }
}


//tech 4
technique SpotMeshShadowTechnique
{
    pass SpotLightShadow
    {
        VertexShader = compile vs_3_0 PointLightMeshVS();
        PixelShader = compile ps_3_0 SpotLightMeshShadowPS();
    }
}


//tech 5
technique DirectionalShadowTechnique
{
    pass DirectionalShadowLight
    {
        VertexShader = compile vs_3_0 DirectionalLightVS();
        PixelShader = compile ps_3_0 DirectionalLightShadowPS();
    }
}