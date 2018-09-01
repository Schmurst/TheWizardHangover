// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Wizard/PallettePicker"
{
	Properties
	{
		_Brightness("Brightness", Range( 0 , 10)) = 1
		_Pallette("Pallette", 2D) = "white" {}
		[IntRange]_StartFrame("StartFrame", Range( 0 , 36)) = 0
		[Toggle]_Shadow("Shadow", Float) = 0
		[Toggle]_Highlight("Highlight", Float) = 0
		[Toggle]_DoubleShadow("DoubleShadow", Float) = 0
		[Toggle]_UsePallette("UsePallette", Float) = 1
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
				float4 ase_color : COLOR;
				float4 ase_texcoord : TEXCOORD0;
			};
			
			struct v2f
			{
				float4 vertex : SV_POSITION;
				UNITY_VERTEX_OUTPUT_STEREO
				float4 ase_color : COLOR;
				float4 ase_texcoord : TEXCOORD0;
			};

			uniform float _UsePallette;
			uniform float _Brightness;
			uniform float _Highlight;
			uniform float _DoubleShadow;
			uniform float _Shadow;
			uniform sampler2D _Pallette;
			uniform float _StartFrame;
			
			v2f vert ( appdata v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				o.ase_color = v.ase_color;
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
				float4 temp_output_34_0 = ( i.ase_color * _Brightness );
				float2 uv3 = i.ase_texcoord.xy * float2( 1,1 ) + float2( 0,0 );
				float temp_output_4_0_g1 = 6.0;
				float temp_output_5_0_g1 = 6.0;
				float2 appendResult7_g1 = (float2(temp_output_4_0_g1 , temp_output_5_0_g1));
				float totalFrames39_g1 = ( temp_output_4_0_g1 * temp_output_5_0_g1 );
				float2 appendResult8_g1 = (float2(totalFrames39_g1 , temp_output_5_0_g1));
				float mulTime5 = _Time.y * 0.0;
				float clampResult42_g1 = clamp( _StartFrame , 0.0001 , ( totalFrames39_g1 - 1.0 ) );
				float temp_output_35_0_g1 = frac( ( ( mulTime5 + clampResult42_g1 ) / totalFrames39_g1 ) );
				float2 appendResult29_g1 = (float2(temp_output_35_0_g1 , ( 1.0 - temp_output_35_0_g1 )));
				float2 temp_output_15_0_g1 = ( ( uv3 / appendResult7_g1 ) + ( floor( ( appendResult8_g1 * appendResult29_g1 ) ) / appendResult7_g1 ) );
				float4 tex2DNode1 = tex2D( _Pallette, temp_output_15_0_g1 );
				float4 Pallete43 = lerp(lerp(lerp(tex2DNode1,( 0.93 * tex2DNode1 ),_Shadow),( 0.93 * lerp(tex2DNode1,( 0.93 * tex2DNode1 ),_Shadow) ),_DoubleShadow),( tex2DNode1 * 1.08 ),_Highlight);
				
				
				finalColor = lerp(temp_output_34_0,Pallete43,_UsePallette);
				return finalColor;
			}
			ENDCG
		}
	}
	CustomEditor "ASEMaterialInspector"
	
	
}
/*ASEBEGIN
Version=15600
113;116;1813;962;2366.959;1743.412;1.72977;True;True
Node;AmplifyShaderEditor.RangedFloatNode;21;-2244.429,624.42;Float;False;Constant;_Rows;Rows;7;0;Create;True;0;0;False;0;6;6;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;3;-2330.287,378.0808;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;6;-2345.549,772.5775;Float;False;Property;_StartFrame;StartFrame;3;1;[IntRange];Create;True;0;0;False;0;0;3;0;36;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;20;-2247.218,530.1915;Float;False;Constant;_Columns;Columns;8;0;Create;True;0;0;False;0;6;6;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;5;-2268.521,887.978;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;4;-1940.523,593.374;Float;True;Flipbook;-1;;1;53c2488c220f6564ca6c90721ee16673;2,71,1,68,0;8;51;SAMPLER2D;0.0;False;13;FLOAT2;0,0;False;4;FLOAT;4;False;5;FLOAT;6;False;24;FLOAT;0;False;2;FLOAT;0;False;55;FLOAT;0;False;70;FLOAT;0;False;5;COLOR;53;FLOAT2;0;FLOAT;47;FLOAT;48;FLOAT;62
Node;AmplifyShaderEditor.SamplerNode;1;-1470.173,183.3768;Float;True;Property;_Pallette;Pallette;1;0;Create;True;0;0;False;0;4738a02e1228e514d98b2c94964599b0;4738a02e1228e514d98b2c94964599b0;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;7;-1357.844,-73.82631;Float;True;Constant;_ShadowAmt;ShadowAmt;1;0;Create;True;0;0;False;0;0.93;0.89;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;16;-1045.455,73.26575;Float;True;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;29;-929.8888,-138.7032;Float;True;Constant;_Float2;Float 2;5;0;Create;True;0;0;False;0;0.93;0.78;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ToggleSwitchNode;18;-739.7996,164.0764;Float;False;Property;_Shadow;Shadow;4;0;Create;True;0;0;False;0;0;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;30;-589.8759,-105.8715;Float;True;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;8;-1426.219,461.3944;Float;True;Constant;_HighlightAmt;HighlightAmt;3;0;Create;True;0;0;False;0;1.08;1.14;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;26;-1048.672,346.0544;Float;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ToggleSwitchNode;28;-471.0929,163.991;Float;False;Property;_DoubleShadow;DoubleShadow;6;0;Create;True;0;0;False;0;0;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.VertexColorNode;32;-1286.626,-1031.204;Float;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ToggleSwitchNode;25;-640.6001,337.2652;Float;False;Property;_Highlight;Highlight;5;0;Create;True;0;0;False;0;0;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;31;-1272.426,-792.2047;Float;False;Property;_Brightness;Brightness;0;0;Create;True;0;0;False;0;1;1;0;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;34;-939.64,-1029.239;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;44;-61.81761,-2.712271;Float;False;43;Pallete;0;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;43;-350.1052,345.6389;Float;False;Pallete;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;45;-915.683,-705.5505;Float;False;-1;;0;1;OBJECT;0
Node;AmplifyShaderEditor.ToggleSwitchNode;47;-706.3807,-570.6288;Float;False;Property;_ToggleSwitch0;Toggle Switch0;7;0;Create;True;0;0;False;0;1;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ToggleSwitchNode;41;199.8969,-90.69302;Float;False;Property;_UsePallette;UsePallette;8;0;Create;True;0;0;False;0;1;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;48;-953.7377,-1413.027;Float;False;HueShift;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;49;-696.0016,-1393.999;Float;False;-1;;0;1;OBJECT;0
Node;AmplifyShaderEditor.ColorNode;33;-1234.051,-1303.667;Float;False;Property;_HueShift;HueShift;2;0;Create;True;0;0;False;0;0,0,0,0;1,1,1,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;46;-939.8995,-537.763;Float;False;-1;;0;1;OBJECT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;35;-438.1816,-769.3644;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;36;-455.9237,-1278.257;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;39;100.3527,-1142.57;Float;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;37;-170.6234,-1367.557;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;38;-158.2812,-792.9642;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;19;552.1006,-147.4934;Float;False;True;2;Float;ASEMaterialInspector;0;1;Wizard/PallettePicker;0770190933193b94aaa3065e307002fa;0;0;Unlit;2;True;0;1;False;-1;0;False;-1;0;1;False;-1;0;False;-1;True;0;False;-1;0;False;-1;True;0;False;-1;True;True;True;True;True;0;False;-1;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;True;1;False;-1;True;3;False;-1;True;True;0;False;-1;0;False;-1;True;1;RenderType=Opaque;True;2;0;False;False;False;False;False;False;False;False;False;False;0;;0;0;Standard;0;2;0;FLOAT4;0,0,0,0;False;1;FLOAT3;0,0,0;False;0
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
WireConnection;26;0;1;0
WireConnection;26;1;8;0
WireConnection;28;0;18;0
WireConnection;28;1;30;0
WireConnection;25;0;28;0
WireConnection;25;1;26;0
WireConnection;34;0;32;0
WireConnection;34;1;31;0
WireConnection;43;0;25;0
WireConnection;41;0;34;0
WireConnection;41;1;44;0
WireConnection;48;0;33;0
WireConnection;35;0;33;0
WireConnection;35;1;34;0
WireConnection;36;0;33;0
WireConnection;36;1;34;0
WireConnection;39;0;38;0
WireConnection;39;1;37;0
WireConnection;39;2;33;4
WireConnection;37;0;36;0
WireConnection;37;1;33;0
WireConnection;38;0;35;0
WireConnection;38;1;33;0
WireConnection;19;0;41;0
ASEEND*/
//CHKSM=3EB8254123B2C8E9374BDEFD6767D4CDDE4AED12