// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Unlit/Rep_DepthFade"
{
	Properties
	{
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true" "GameAsset"="Stage" }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Unlit keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float3 worldPos;
		};

		uniform float3 PlayerPos;

		inline fixed4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return fixed4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float3 ase_worldPos = i.worldPos;
			float clampResult6 = clamp( ( distance( PlayerPos , ase_worldPos ) / 1.5 ) , 0.0 , 1.0 );
			o.Emission = ( float4(1.5,0.501,0,0) * ( 1.0 - saturate( clampResult6 ) ) ).rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=13101
68;85;1232;618;1922.77;289.7604;1.942849;True;False
Node;AmplifyShaderEditor.Vector3Node;1;-1193.291,92.90813;Float;False;Global;PlayerPos;PlayerPos;0;0;0,0,0;0;4;FLOAT3;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.WorldPosInputsNode;12;-1232.645,251.2167;Float;False;0;4;FLOAT3;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;5;-944.5432,356.7661;Float;False;Constant;_MinDistanceView;MinDistanceView;1;0;1.5;0;0;0;1;FLOAT
Node;AmplifyShaderEditor.DistanceOpNode;2;-950.5432,203.766;Float;False;2;0;FLOAT3;0.0;False;1;FLOAT3;0,0,0;False;1;FLOAT
Node;AmplifyShaderEditor.SimpleDivideOpNode;4;-733.5438,299.7661;Float;True;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.ClampOpNode;6;-492.0165,264.5135;Float;False;3;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;1.0;False;1;FLOAT
Node;AmplifyShaderEditor.SaturateNode;14;-293.0702,209.4663;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.ColorNode;10;-178.8172,-71.89822;Float;False;Constant;_Color;Color;2;1;[HDR];1.5,0.501,0,0;0;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.OneMinusNode;7;-80.82501,152.3203;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;8;120.1416,62.6329;Float;False;2;2;0;COLOR;0.0;False;1;FLOAT;0.0,0,0,0;False;1;COLOR
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;532.5226,7.0465;Float;False;True;2;Float;ASEMaterialInspector;0;0;Unlit;Unlit/Rep_DepthFade;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;0;False;0;0;Opaque;0.5;True;True;0;False;Opaque;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;False;0;4;10;25;False;0.5;True;0;Zero;Zero;0;Zero;Zero;Add;Add;0;False;0;0,0,0,0;VertexOffset;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;1;GameAsset=Stage;14;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;OBJECT;0.0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;2;0;1;0
WireConnection;2;1;12;0
WireConnection;4;0;2;0
WireConnection;4;1;5;0
WireConnection;6;0;4;0
WireConnection;14;0;6;0
WireConnection;7;0;14;0
WireConnection;8;0;10;0
WireConnection;8;1;7;0
WireConnection;0;2;8;0
ASEEND*/
//CHKSM=306A7DF7C00CC60DE38B26361FCC1D4BD60C54AE