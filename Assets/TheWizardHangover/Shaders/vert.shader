Shader "Unlit/Vert"
{
	Properties
	{
		_ColourOverride ("Colour Override", Color) = (0,0,0,0)
	}

	SubShader
	{
		Tags { "RenderType"="Opaque" }
		pass
		{
			Cull Back
			ZTest LEqual
			Tags { "LightMode"="ForwardBase" }

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fwdbase
			#include "UnityCG.cginc"
			#include "AutoLight.cginc"
			#include "Lighting.cginc"

			uniform float4 _ColourOverride;

			struct v2f
			{
				float4 pos : SV_POSITION;
				float3 normal : NORMAL;
				float3 col : COLOR;
				float3 lightDir : TEXCOORD0;
				LIGHTING_COORDS(3,4)
			};

			v2f vert(appdata_full v)
			{
				v2f vout;
				vout.pos = UnityObjectToClipPos(v.vertex);
				vout.lightDir = normalize(ObjSpaceLightDir(v.vertex));
				vout.normal = normalize(v.normal).xyz;
				float4 col = v.color;
				if(_ColourOverride.w) col = _ColourOverride;
				vout.col = col * _LightColor0.xyz;
				vout.col = pow(vout.col, 1/2.2f);
				TRANSFER_VERTEX_TO_FRAGMENT(vout);
				return vout;
			}

			float4 frag(v2f i): COLOR
			{
				float3 l = normalize(i.lightDir);
				float3 n = normalize(i.normal);
				float attenuation = LIGHT_ATTENUATION(i);
				float diffuseTerm = saturate(dot(l, n)*0.1f) * attenuation;
				diffuseTerm = min(pow(min(diffuseTerm*10, 1.0f), 1.0f) + 0.8f, 1.0f);
				return float4(i.col*diffuseTerm,1);
			}

			ENDCG
		}
	}
	FallBack "Diffuse"
}