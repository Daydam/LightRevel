// Upgrade NOTE: upgraded instancing buffer 'LightReceiver' to new syntax.

// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "LightReceiver"
{
	Properties
	{
		[HideInInspector] __dirty( "", Int ) = 1
		_RequiredColor("RequiredColor", Color) = (0,0,0,0)
		_EmissionMap("Emission Map", 2D) = "white" {}
		_Activated("Activated", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma multi_compile_instancing
		#pragma surface surf Unlit keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _EmissionMap;
		uniform float4 _EmissionMap_ST;

		UNITY_INSTANCING_BUFFER_START(LightReceiver)
			UNITY_DEFINE_INSTANCED_PROP(float4, _RequiredColor)
#define _RequiredColor_arr LightReceiver
			UNITY_DEFINE_INSTANCED_PROP(float, _Activated)
#define _Activated_arr LightReceiver
		UNITY_INSTANCING_BUFFER_END(LightReceiver)

		inline fixed4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return fixed4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float2 uv_EmissionMap = i.uv_texcoord * _EmissionMap_ST.xy + _EmissionMap_ST.zw;
			float4 _RequiredColor_Instance = UNITY_ACCESS_INSTANCED_PROP(_RequiredColor_arr, _RequiredColor);
			float _Activated_Instance = UNITY_ACCESS_INSTANCED_PROP(_Activated_arr, _Activated);
			o.Emission = ( tex2D( _EmissionMap, uv_EmissionMap ) * _RequiredColor_Instance * _Activated_Instance ).rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=13101
82;121;999;542;1348.681;460.4079;1.671174;True;False
Node;AmplifyShaderEditor.ColorNode;1;-658.4866,-87.73618;Float;False;InstancedProperty;_RequiredColor;RequiredColor;0;0;0,0,0,0;0;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;4;-608.3503,107.7914;Float;False;InstancedProperty;_Activated;Activated;2;0;0;0;0;0;1;FLOAT
Node;AmplifyShaderEditor.SamplerNode;2;-688.5676,-281.5922;Float;True;Property;_EmissionMap;Emission Map;1;0;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;3;-274.1159,-84.39368;Float;False;3;3;0;COLOR;0.0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0,0,0,0;False;1;COLOR
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;6;0,0;Float;False;True;2;Float;ASEMaterialInspector;0;0;Unlit;LightReceiver;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;0;False;0;0;Opaque;0.5;True;True;0;False;Opaque;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;False;0;4;10;25;False;0.5;True;0;Zero;Zero;0;Zero;Zero;Add;Add;0;False;0;0,0,0,0;VertexOffset;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;0;14;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;OBJECT;0.0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;3;0;2;0
WireConnection;3;1;1;0
WireConnection;3;2;4;0
WireConnection;6;2;3;0
ASEEND*/
//CHKSM=9A9F0DA130F2397B4E7D5C2868705571C2785C02