//-----------------------------------------------------------------------------
// SkinnedEffect.fx
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

#include "Macros.fxh"

#define SKINNED_EFFECT_MAX_BONES   72


DECLARE_TEXTURE(Texture, 0);


BEGIN_CONSTANTS

    float4 DiffuseColor                         _vs(c0)  _ps(c1)  _cb(c0);
    float3 EmissiveColor                        _vs(c1)  _ps(c2)  _cb(c1);
    float3 SpecularColor                        _vs(c2)  _ps(c3)  _cb(c2);
    float  SpecularPower                        _vs(c3)  _ps(c4)  _cb(c2.w);

    float3 DirLight0Direction                   _vs(c4)  _ps(c5)  _cb(c3);
    float3 DirLight0DiffuseColor                _vs(c5)  _ps(c6)  _cb(c4);
    float3 DirLight0SpecularColor               _vs(c6)  _ps(c7)  _cb(c5);

    float3 DirLight1Direction                   _vs(c7)  _ps(c8)  _cb(c6);
    float3 DirLight1DiffuseColor                _vs(c8)  _ps(c9)  _cb(c7);
    float3 DirLight1SpecularColor               _vs(c9)  _ps(c10) _cb(c8);

    float3 DirLight2Direction                   _vs(c10) _ps(c11) _cb(c9);
    float3 DirLight2DiffuseColor                _vs(c11) _ps(c12) _cb(c10);
    float3 DirLight2SpecularColor               _vs(c12) _ps(c13) _cb(c11);

    float3 EyePosition                          _vs(c13) _ps(c14) _cb(c12);

    float3 FogColor                                      _ps(c0)  _cb(c13);
    float4 FogVector                            _vs(c14)          _cb(c14);

    float4x4 World                              _vs(c19)          _cb(c15);
    float3x3 WorldInverseTranspose              _vs(c23)          _cb(c19);
    
    float4x3 Bones[SKINNED_EFFECT_MAX_BONES]    _vs(c26)          _cb(c22);

MATRIX_CONSTANTS

    float4x4 WorldViewProj                      _vs(c15)          _cb(c0);

END_CONSTANTS


#include "Structures.fxh"
#include "Common.fxh"

void Skin(inout VSInputNmTxWeights vin, uniform int boneCount)
{
    float4x3 skinning = 0;

    [unroll]
    for (int i = 0; i < boneCount; i++)
    {
        skinning += Bones[vin.Indices[i]] * vin.Weights[i];
    }

    vin.Position.xyz = mul(vin.Position, skinning);
    vin.Normal = mul(vin.Normal, (float3x3)skinning);
}


