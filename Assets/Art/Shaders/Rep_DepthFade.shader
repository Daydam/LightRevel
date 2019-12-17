// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Unlit/Rep_DepthFade"
{
	Properties
	{
		[HideInInspector] __dirty( "", Int ) = 1
		_PlayerPos("PlayerPos", Vector) = (0,0,0,0)
		_MinDistanceView("MinDistanceView", Float) = 5
		_Color("Color", Color) = (0.08514194,0.5367647,0,0)
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Unlit keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float3 worldPos;
		};

		uniform float4 _Color;
		uniform float3 _PlayerPos;
		uniform float _MinDistanceView;

		inline fixed4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return fixed4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float3 ase_worldPos = i.worldPos;
			float clampResult6 = clamp( ( distance( _PlayerPos , ase_worldPos ) / _MinDistanceView ) , 0.0 , 0.0 );
			o.Emission = ( _Color * ( 1.0 - saturate( clampResult6 ) ) ).rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=13101
7;29;1232;618;2585.544;715.4313;2.691193;True;False
Node;AmplifyShaderEditor.WorldPosInputsNode;12;-1331.797,230.5598;Float;False;0;4;FLOAT3;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.Vector3Node;1;-1292.443,72.25117;Float;False;Property;_PlayerPos;PlayerPos;0;0;0,0,0;0;4;FLOAT3;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;5;-1043.696,336.109;Float;False;Property;_MinDistanceView;MinDistanceView;1;0;5;0;0;0;1;FLOAT
Node;AmplifyShaderEditor.DistanceOpNode;2;-1049.696,183.1091;Float;False;2;0;FLOAT3;0.0;False;1;FLOAT3;0,0,0;False;1;FLOAT
Node;AmplifyShaderEditor.SimpleDivideOpNode;4;-832.6967,279.1091;Float;False;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.ClampOpNode;6;-666.1696,270.8566;Float;False;3;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.SaturateNode;14;-392.2237,188.8094;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.ColorNode;10;-178.8172,-71.89822;Float;False;Property;_Color;Color;2;0;0.08514194,0.5367647,0,0;0;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.OneMinusNode;7;-80.82501,152.3203;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;8;120.1416,62.6329;Float;False;2;2;0;COLOR;0.0;False;1;FLOAT;0.0,0,0,0;False;1;COLOR
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;532.5226,7.0465;Float;False;True;2;Float;ASEMaterialInspector;0;0;Unlit;Unlit/Rep_DepthFade;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;0;False;0;0;Opaque;0.5;True;True;0;False;Opaque;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;False;0;4;10;25;False;0.5;True;0;Zero;Zero;0;Zero;Zero;Add;Add;0;False;0;0,0,0,0;VertexOffset;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;0;14;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;OBJECT;0.0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
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
//CHKSM=07E065D25BC65C047C8320317B6CFB2F0FD0E9DF