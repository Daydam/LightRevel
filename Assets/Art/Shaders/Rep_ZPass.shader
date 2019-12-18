// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Unlit/Rep_ZPass"
{
	Properties
	{
		[HideInInspector] __dirty( "", Int ) = 1
		_MaskClipValue( "Mask Clip Value", Float ) = 0.5
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Geometry+0" "IgnoreProjector" = "True" "IsEmissive" = "true" "GameAsset"="Stage" }
		Cull Back
		Blend SrcAlpha DstAlpha , SrcAlpha DstAlpha
		BlendOp Add , Add
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Unlit keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float3 worldPos;
		};

		uniform float3 PlayerPos;
		uniform float _MaskClipValue = 0.5;

		inline fixed4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return fixed4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float3 ase_worldPos = i.worldPos;
			float clampResult21 = clamp( ( distance( PlayerPos , ase_worldPos ) / 1.5 ) , 0.0 , 1.0 );
			float temp_output_24_0 = ( 1.0 - saturate( clampResult21 ) );
			o.Emission = ( float4(0,1.5,0.01034474,0) * temp_output_24_0 ).rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=13101
111;35;1230;617;1377.228;338.7469;1.642849;True;False
Node;AmplifyShaderEditor.WorldPosInputsNode;17;-1289.105,187.0364;Float;False;0;4;FLOAT3;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.Vector3Node;16;-1249.751,28.72777;Float;False;Global;PlayerPos;PlayerPos;0;0;0,0,0;0;4;FLOAT3;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.DistanceOpNode;19;-1007.003,139.5857;Float;False;2;0;FLOAT3;0.0;False;1;FLOAT3;0,0,0;False;1;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;18;-1001.003,292.5858;Float;False;Constant;_Float1;Float 1;1;0;1.5;0;0;0;1;FLOAT
Node;AmplifyShaderEditor.SimpleDivideOpNode;20;-790.004,235.5858;Float;True;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.ClampOpNode;21;-548.4767,200.3331;Float;False;3;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;1.0;False;1;FLOAT
Node;AmplifyShaderEditor.SaturateNode;22;-349.5303,145.2859;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.ColorNode;23;-235.2774,-136.0785;Float;False;Constant;_Color1;Color 1;2;1;[HDR];0,1.5,0.01034474,0;0;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.OneMinusNode;24;-137.2852,88.13995;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;15;-200.9491,318.3933;Float;False;Property;_Opacity;Opacity;1;0;0;0;1;0;1;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;26;157.193,282.2502;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;25;63.68142,-1.547455;Float;False;2;2;0;COLOR;0.0;False;1;FLOAT;0.0,0,0,0;False;1;COLOR
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;532.5226,7.0465;Float;False;True;2;Float;ASEMaterialInspector;0;0;Unlit;Unlit/Rep_ZPass;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;Back;0;0;False;0;0;Custom;0.5;True;True;0;True;Transparent;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;False;0;4;10;25;False;0.5;True;1;SrcAlpha;DstAlpha;1;SrcAlpha;DstAlpha;Add;Add;0;False;0;0,0,0,0;VertexOffset;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;1;GameAsset=Stage;14;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;OBJECT;0.0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;19;0;16;0
WireConnection;19;1;17;0
WireConnection;20;0;19;0
WireConnection;20;1;18;0
WireConnection;21;0;20;0
WireConnection;22;0;21;0
WireConnection;24;0;22;0
WireConnection;26;0;24;0
WireConnection;26;1;15;0
WireConnection;25;0;23;0
WireConnection;25;1;24;0
WireConnection;0;2;25;0
ASEEND*/
//CHKSM=CA7FE2EFBBAFBFE2E60555EC0112082478C293AD