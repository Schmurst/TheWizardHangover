// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Wizard/PallettePicker"
{
	Properties
	{
		_Pallette("Pallette", 2D) = "white" {}
		_ShadowAmt("ShadowAmt", Float) = 0.93
		_HighlightAmt("HighlightAmt", Float) = 1.08
		_Float2("Float 2", Float) = 0.93
		[IntRange]_StartFrame("StartFrame", Range( 0 , 36)) = 0
		[Toggle]_Shadow("Shadow", Float) = 1
		[Toggle]_Highlight("Highlight", Float) = 0
		_Rows("Rows", Float) = 6
		_Columns("Columns", Float) = 6
		[Toggle]_DoubleShadow("DoubleShadow", Float) = 1
	}
	
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100
		CGINCLUDE
		#pragma target 3.0
		ENDCG
		Blend Off
		Cull Back
		ColorMask RGBA
		ZWrite On
		ZTest LEqual
		Offset 0 , 0
		
		

		Pass
		{
			Name "Unlit"
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			#include "UnityShaderVariables.cginc"


			struct appdata
			{
				float4 vertex : POSITION;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				float4 ase_texcoord : TEXCOORD0;
			};
			
			struct v2f
			{
				float4 vertex : SV_POSITION;
				UNITY_VERTEX_OUTPUT_STEREO
				float4 ase_texcoord : TEXCOORD0;
			};

			uniform float _Highlight;
			uniform float _DoubleShadow;
			uniform float _Shadow;
			uniform sampler2D _Pallette;
			uniform float _Columns;
			uniform float _Rows;
			uniform float _StartFrame;
			uniform float _ShadowAmt;
			uniform float _Float2;
			uniform float _HighlightAmt;
			
			v2f vert ( appdata v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				o.ase_texcoord.xy = v.ase_texcoord.xy;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord.zw = 0;
				
				v.vertex.xyz +=  float3(0,0,0) ;
				o.vertex = UnityObjectToClipPos(v.vertex);
				return o;
			}
			
			fixed4 frag (v2f i ) : SV_Target
			{
				fixed4 finalColor;
				float2 uv3 = i.ase_texcoord.xy * float2( 1,1 ) + float2( 0,0 );
				float temp_output_4_0_g1 = _Columns;
				float temp_output_5_0_g1 = _Rows;
				float2 appendResult7_g1 = (float2(temp_output_4_0_g1 , temp_output_5_0_g1));
				float totalFrames39_g1 = ( temp_output_4_0_g1 * temp_output_5_0_g1 );
				float2 appendResult8_g1 = (float2(totalFrames39_g1 , temp_output_5_0_g1));
				float mulTime5 = _Time.y * 0.0;
				float clampResult42_g1 = clamp( _StartFrame , 0.0001 , ( totalFrames39_g1 - 1.0 ) );
				float temp_output_35_0_g1 = frac( ( ( mulTime5 + clampResult42_g1 ) / totalFrames39_g1 ) );
				float2 appendResult29_g1 = (float2(temp_output_35_0_g1 , ( 1.0 - temp_output_35_0_g1 )));
				float2 temp_output_15_0_g1 = ( ( uv3 / appendResult7_g1 ) + ( floor( ( appendResult8_g1 * appendResult29_g1 ) ) / appendResult7_g1 ) );
				float4 tex2DNode1 = tex2D( _Pallette, temp_output_15_0_g1 );
				
				
				finalColor = lerp(lerp(lerp(tex2DNode1,( _ShadowAmt * tex2DNode1 ),_Shadow),( _Float2 * lerp(tex2DNode1,( _ShadowAmt * tex2DNode1 ),_Shadow) ),_DoubleShadow),( tex2DNode1 * _HighlightAmt ),_Highlight);
				return finalColor;
			}
			ENDCG
		}
	}
	CustomEditor "ASEMaterialInspector"
	
	
}
/*ASEBEGIN
Version=15600
100;29;1813;1044;1442.346;708.9097;1.255607;True;True
Node;AmplifyShaderEditor.TextureCoordinatesNode;3;-1768.236,69.50358;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;20;-1685.167,221.6143;Float;False;Property;_Columns;Columns;8;0;Create;True;0;0;False;0;6;6;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;6;-1783.498,464.0004;Float;False;Property;_StartFrame;StartFrame;4;1;[IntRange];Create;True;0;0;False;0;0;12;0;36;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;21;-1682.378,315.8429;Float;False;Property;_Rows;Rows;7;0;Create;True;0;0;False;0;6;6;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;5;-1706.47,579.401;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;4;-1378.472,284.7969;Float;True;Flipbook;-1;;1;53c2488c220f6564ca6c90721ee16673;2,71,1,68,0;8;51;SAMPLER2D;0.0;False;13;FLOAT2;0,0;False;4;FLOAT;4;False;5;FLOAT;6;False;24;FLOAT;0;False;2;FLOAT;0;False;55;FLOAT;0;False;70;FLOAT;0;False;5;COLOR;53;FLOAT2;0;FLOAT;47;FLOAT;48;FLOAT;62
Node;AmplifyShaderEditor.SamplerNode;1;-924.6528,-152.7519;Float;True;Property;_Pallette;Pallette;0;0;Create;True;0;0;False;0;4738a02e1228e514d98b2c94964599b0;4738a02e1228e514d98b2c94964599b0;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;7;-812.3242,-408.6994;Float;True;Property;_ShadowAmt;ShadowAmt;1;0;Create;True;0;0;False;0;0.93;0.92;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;16;-499.9346,-262.8629;Float;True;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ToggleSwitchNode;18;-194.2792,-172.0523;Float;False;Property;_Shadow;Shadow;5;0;Create;True;0;0;False;0;1;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;29;-356.7452,-587.8365;Float;True;Property;_Float2;Float 2;3;0;Create;True;0;0;False;0;0.93;0.93;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;30;-44.35559,-442.0001;Float;True;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;8;-787.5754,123.9382;Float;True;Property;_HighlightAmt;HighlightAmt;2;0;Create;True;0;0;False;0;1.08;1.08;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ToggleSwitchNode;28;74.42749,-172.1377;Float;False;Property;_DoubleShadow;DoubleShadow;9;0;Create;True;0;0;False;0;1;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;26;-503.1519,9.92547;Float;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ToggleSwitchNode;25;-95.07977,1.136284;Float;False;Property;_Highlight;Highlight;6;0;Create;True;0;0;False;0;0;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;23;-2195.711,125.4411;Float;False;Constant;_Float1;Float 1;5;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;24;-1989.791,-57.87749;Float;False;Constant;_Vector0;Vector 0;5;0;Create;True;0;0;False;0;-1.53,-0.33;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.RangedFloatNode;22;-2183.155,-44.0658;Float;False;Constant;_Float0;Float 0;5;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;19;307.2573,-114.8477;Float;False;True;2;Float;ASEMaterialInspector;0;1;Wizard/PallettePicker;0770190933193b94aaa3065e307002fa;0;0;Unlit;2;True;0;1;False;-1;0;False;-1;0;1;False;-1;0;False;-1;True;0;False;-1;0;False;-1;True;0;False;-1;True;True;True;True;True;0;False;-1;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;True;1;False;-1;True;3;False;-1;True;True;0;False;-1;0;False;-1;True;1;RenderType=Opaque;True;2;0;False;False;False;False;False;False;False;False;False;False;0;;0;0;Standard;0;2;0;FLOAT4;0,0,0,0;False;1;FLOAT3;0,0,0;False;0
WireConnection;4;13;3;0
WireConnection;4;4;20;0
WireConnection;4;5;21;0
WireConnection;4;24;6;0
WireConnection;4;2;5;0
WireConnection;1;1;4;0
WireConnection;16;0;7;0
WireConnection;16;1;1;0
WireConnection;18;0;1;0
WireConnection;18;1;16;0
WireConnection;30;0;29;0
WireConnection;30;1;18;0
WireConnection;28;0;18;0
WireConnection;28;1;30;0
WireConnection;26;0;1;0
WireConnection;26;1;8;0
WireConnection;25;0;28;0
WireConnection;25;1;26;0
WireConnection;19;0;25;0
ASEEND*/
//CHKSM=086EA210847FB139021ADAAA0599D6F8AC28F842