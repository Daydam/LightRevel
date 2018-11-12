// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "LightEmitter"
{
	Properties
	{
		[HideInInspector] __dirty( "", Int ) = 1
		_MaxRadius("MaxRadius", Float) = 5
		_WaveForm("WaveForm", 2D) = "white" {}
		_WaveColor("Wave Color", Color) = (1,0,0,0)
		_SpotlightRadius("SpotlightRadius", Float) = 1
		_SpotlightPosition("SpotlightPosition", Vector) = (0,0,0,0)
		_SpotlightColor("SpotlightColor", Color) = (1,0,0,0)
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Unlit keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float3 worldPos;
		};

		uniform sampler2D _WaveForm;
		uniform float3 Position;
		uniform float Radius;
		uniform float4 _WaveColor;
		uniform float _MaxRadius;
		uniform float3 _SpotlightPosition;
		uniform float _SpotlightRadius;
		uniform float4 _SpotlightColor;

		inline fixed4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return fixed4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float3 ase_worldPos = i.worldPos;
			float temp_output_2_0 = distance( Position , ase_worldPos );
			float2 temp_cast_0 = (( 1.0 - saturate( ( temp_output_2_0 / Radius ) ) )).xx;
			o.Emission = saturate( ( saturate( ( ( tex2D( _WaveForm, temp_cast_0 ) * _WaveColor ) * ( 1.0 - saturate( ( temp_output_2_0 / _MaxRadius ) ) ) ) ) + saturate( ( ( 1.0 - saturate( ( distance( _SpotlightPosition , ase_worldPos ) / _SpotlightRadius ) ) ) * _SpotlightColor ) ) ) ).xyz;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=13101
7;29;1216;581;3344.808;443.2517;2.013477;True;False
Node;AmplifyShaderEditor.CommentaryNode;43;-2783.606,-357.8428;Float;False;2091.278;765.2473;LightWave;16;13;1;14;2;3;5;4;6;7;28;8;16;15;9;12;10;LightWave;1,1,1,1;0;0
Node;AmplifyShaderEditor.WorldPosInputsNode;1;-2714.705,-39.98495;Float;False;0;4;FLOAT3;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.Vector3Node;13;-2733.606,-307.8428;Float;False;Global;Position;Position;1;0;0,0,0;0;4;FLOAT3;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.DistanceOpNode;2;-2432.598,-172.8271;Float;True;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0.0,0;False;1;FLOAT
Node;AmplifyShaderEditor.CommentaryNode;42;-2350.873,533.9825;Float;False;1661.379;735.7211;Spotlight;10;29;30;31;32;33;34;36;35;38;40;Spotlight;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;14;-2206.455,-109.6638;Float;False;Global;Radius;Radius;1;0;1;0;0;0;1;FLOAT
Node;AmplifyShaderEditor.Vector3Node;29;-2300.873,583.9826;Float;False;Property;_SpotlightPosition;SpotlightPosition;4;0;0,0,0;0;4;FLOAT3;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.WorldPosInputsNode;30;-2281.972,851.8406;Float;False;0;4;FLOAT3;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SimpleDivideOpNode;3;-1998.979,-168.7973;Float;False;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.DistanceOpNode;32;-1999.865,718.9985;Float;True;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0.0,0;False;1;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;31;-2016.747,995.0028;Float;False;Property;_SpotlightRadius;SpotlightRadius;3;0;1;0;0;0;1;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;4;-2379.762,292.4044;Float;False;Property;_MaxRadius;MaxRadius;0;0;5;0;0;0;1;FLOAT
Node;AmplifyShaderEditor.SaturateNode;5;-1849.141,-168.1938;Float;True;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.OneMinusNode;7;-1660.381,-168.1469;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.SimpleDivideOpNode;6;-2163.816,242.861;Float;False;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.SimpleDivideOpNode;33;-1740.629,838.0483;Float;False;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.SaturateNode;8;-2003.069,278.1342;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.SaturateNode;34;-1563.519,791.5099;Float;True;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.SamplerNode;28;-1482.89,-216.7598;Float;True;Property;_WaveForm;WaveForm;1;0;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;FLOAT4;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.ColorNode;16;-1593.35,16.05951;Float;False;Property;_WaveColor;Wave Color;2;0;1,0,0,0;0;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.OneMinusNode;35;-1342.034,726.1074;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.ColorNode;36;-1341.4,874.5375;Float;False;Property;_SpotlightColor;SpotlightColor;5;0;1,0,0,0;0;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.OneMinusNode;9;-1816.518,278.1811;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;15;-1178.225,-42.85925;Float;False;2;2;0;FLOAT4;0.0;False;1;COLOR;0.0,0,0,0;False;1;FLOAT4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;12;-1121.4,146.6583;Float;False;2;2;0;FLOAT4;0.0;False;1;FLOAT;0.0,0,0,0;False;1;FLOAT4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;38;-1048.798,774.1292;Float;False;2;2;0;FLOAT;0.0;False;1;COLOR;0.0;False;1;COLOR
Node;AmplifyShaderEditor.SaturateNode;40;-864.4931,629.8017;Float;False;1;0;COLOR;0.0;False;1;COLOR
Node;AmplifyShaderEditor.SaturateNode;10;-867.3282,207.887;Float;False;1;0;FLOAT4;0.0;False;1;FLOAT4
Node;AmplifyShaderEditor.SimpleAddOpNode;41;-518.1595,443.5968;Float;False;2;2;0;FLOAT4;0.0,0,0,0;False;1;COLOR;0.0,0,0,0;False;1;FLOAT4
Node;AmplifyShaderEditor.SaturateNode;44;-243.7476,264.1767;Float;False;1;0;FLOAT4;0.0;False;1;FLOAT4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;2;Float;ASEMaterialInspector;0;0;Unlit;LightEmitter;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;Back;0;0;False;0;0;Opaque;0.5;True;True;0;False;Opaque;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;False;0;4;10;25;False;0.5;True;0;One;OneMinusSrcAlpha;0;One;One;Add;Add;0;False;0;0,0,0,0;VertexOffset;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;0;14;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;OBJECT;0.0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;2;0;13;0
WireConnection;2;1;1;0
WireConnection;3;0;2;0
WireConnection;3;1;14;0
WireConnection;32;0;29;0
WireConnection;32;1;30;0
WireConnection;5;0;3;0
WireConnection;7;0;5;0
WireConnection;6;0;2;0
WireConnection;6;1;4;0
WireConnection;33;0;32;0
WireConnection;33;1;31;0
WireConnection;8;0;6;0
WireConnection;34;0;33;0
WireConnection;28;1;7;0
WireConnection;35;0;34;0
WireConnection;9;0;8;0
WireConnection;15;0;28;0
WireConnection;15;1;16;0
WireConnection;12;0;15;0
WireConnection;12;1;9;0
WireConnection;38;0;35;0
WireConnection;38;1;36;0
WireConnection;40;0;38;0
WireConnection;10;0;12;0
WireConnection;41;0;10;0
WireConnection;41;1;40;0
WireConnection;44;0;41;0
WireConnection;0;2;44;0
ASEEND*/
//CHKSM=553CB31E96BA54FCE0A37AD9906A7F6D95CC409A