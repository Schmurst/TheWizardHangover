// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Wizard/UnlitVert"
{
	Properties
	{
		_Brightness("Brightness", Range( 0 , 10)) = 1
		_HueShift("HueShift", Color) = (0,0,0,0)
		_Saturation("Saturation", Float) = 0
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Lambert keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float4 vertexColor : COLOR;
		};

		uniform float4 _HueShift;
		uniform float _Brightness;
		uniform float _Saturation;

		void surf( Input i , inout SurfaceOutput o )
		{
			float4 temp_output_3_0 = ( i.vertexColor * _Brightness );
			float4 lerpResult11 = lerp( ( _HueShift * temp_output_3_0 ) , _HueShift , 0.0);
			float4 lerpResult16 = lerp( ( _HueShift + temp_output_3_0 ) , _HueShift , 0.0);
			float4 lerpResult25 = lerp( lerpResult11 , lerpResult16 , _HueShift.a);
			float3 desaturateInitialColor34 = lerpResult25.rgb;
			float desaturateDot34 = dot( desaturateInitialColor34, float3( 0.299, 0.587, 0.114 ));
			float3 desaturateVar34 = lerp( desaturateInitialColor34, desaturateDot34.xxx, ( 1.0 - _Saturation ) );
			o.Emission = desaturateVar34;
			o.Specular = 0.0;
			o.Gloss = 0.0;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=15600
-151;155;1813;974;-158.9206;253.1376;1;True;True
Node;AmplifyShaderEditor.RangedFloatNode;4;-612.1988,135.8694;Float;False;Property;_Brightness;Brightness;0;0;Create;True;0;0;False;0;1;2;0;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.VertexColorNode;5;-626.3986,-103.1304;Float;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;1;-73.17463,-136.2231;Float;False;Property;_HueShift;HueShift;1;0;Create;True;0;0;False;0;0,0,0,0;0.3799998,0.3799998,0.3799998,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;3;-279.4127,-101.1653;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;7;222.0453,158.7096;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;15;204.3033,-350.1826;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;35;515.0424,383.6552;Float;False;Property;_Saturation;Saturation;4;0;Create;True;0;0;False;0;0;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;16;489.6036,-439.4825;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;11;501.9459,135.1098;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.OneMinusNode;36;674.9206,340.8624;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;25;760.5796,-214.4962;Float;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;29;-190.9207,-694.196;Float;False;Property;_Float3;Float 3;3;0;Create;True;0;0;False;0;4;4;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;33;1046.747,-245.5863;Float;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;22;1340.782,-52.89567;Float;False;Constant;_Float1;Float 1;3;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;32;493.8793,-918.1954;Float;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;31;291.9794,-1129.895;Float;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;30;65.07928,-913.3958;Float;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PosVertexDataNode;26;-434.1206,-1010.995;Float;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DesaturateOpNode;34;855.5613,182.9329;Float;False;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;27;-443.7209,-737.3961;Float;False;Property;_Float2;Float 2;2;0;Create;True;0;0;False;0;0.25;0.25;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;28;-168.5208,-945.3961;Float;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;20;1277.581,90.50435;Float;False;Constant;_Float0;Float 0;3;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;24;1583.299,-194.4;Float;False;True;2;Float;ASEMaterialInspector;0;0;Lambert;Wizard/UnlitVert;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;3;0;5;0
WireConnection;3;1;4;0
WireConnection;7;0;1;0
WireConnection;7;1;3;0
WireConnection;15;0;1;0
WireConnection;15;1;3;0
WireConnection;16;0;15;0
WireConnection;16;1;1;0
WireConnection;11;0;7;0
WireConnection;11;1;1;0
WireConnection;36;0;35;0
WireConnection;25;0;11;0
WireConnection;25;1;16;0
WireConnection;25;2;1;4
WireConnection;33;0;25;0
WireConnection;32;0;30;0
WireConnection;31;0;30;0
WireConnection;30;0;28;0
WireConnection;30;1;29;0
WireConnection;34;0;25;0
WireConnection;34;1;36;0
WireConnection;28;0;26;2
WireConnection;28;1;27;0
WireConnection;24;2;34;0
WireConnection;24;3;22;0
WireConnection;24;4;20;0
ASEEND*/
//CHKSM=66EF820034D5A1DB545E932E8DC2C812EFAE41B7