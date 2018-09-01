﻿/*
Shader "Unlit/glass"
{
	Properties
	{
		_Color ("Colour", Color) = (0,0,0,0.1)
	}
	SubShader
	{
		Tags { "Queue"="Transparent" "RenderType"="Transparent" }

		Blend SrcAlpha OneMinusSrcAlpha

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			uniform float4 _Color;

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

			v2f vert (appdata_base v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.texcoord;
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				return _Color;
			}
			ENDCG
		}
	}
}
*/
/*
//Calculate the squared length of a vector
float length2(vec2 p)
{
	return dot(p, p);
}

//Generate some noise to scatter points.
float noise(vec2 p)
{
	return fract(sin(fract(sin(p.x) * (43.13311)) + p.y) * 31.0011);
}

float worley(vec2 p)
{
	//Set our distance to infinity
	float d = 1e30;
	//For the 9 surrounding grid points
	for(int xo = -1; xo <= 1; ++xo)
	{
		for(int yo = -1; yo <= 1; ++yo)
		{
			//Floor our vec2 and add an offset to create our point
			vec2 tp = floor(p) + vec2(xo, yo);
			//Calculate the minimum distance for this grid point
			//Mix in the noise value too!
			d = min(d, length2(p - tp - noise(tp)));
		}
	}
	return 3.0*exp(-2.0*abs(0.5*d - 1.0));
}

float fworley(vec2 p)
{
	//Stack noise layers 
	return sqrt(sqrt(sqrt(
		worley(p*5.0 + 0.05*iGlobalTime) *
		sqrt(worley(p * 50.0 + 0.12 + -0.1*iGlobalTime)) *
		sqrt(sqrt(worley(p * -10.0 + 0.03*iGlobalTime))))));
}

void mainImage(out vec4 fragColor, in vec2 fragCoord)
{
	vec2 uv = fragCoord.xy / iResolution.xy;
	//Calculate an intensity
	float t = fworley(uv * iResolution.xy / 1500.0);
	//Add some gradient
	t*=exp(-length2(abs(0.7*uv - 1.0)));
	//Make it blue!
	fragColor = vec4(t * vec3(0.1, 1.1*t, pow(t, 0.5-t)), 1.0);
}
*/
Shader "Unlit/glass"
{
	Properties
	{
		_Color ("Colour", Color) = (0,0,0,0.1)
	}
	SubShader
	{
		Tags { "Queue"="Transparent" "RenderType"="Transparent" }

		Blend SrcAlpha OneMinusSrcAlpha

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			uniform float4 _Color;

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

			v2f vert (appdata_base v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.texcoord * 10;
				return o;
			}

			fixed4 frag (v2f i) : SV_Target
			{
				return float4(0.1f,0.5f,1.0f,0.5f);
			}
			ENDCG
		}
	}
}
