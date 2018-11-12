// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "EmitterParticles"
{
	Properties
	{
		[HideInInspector] __dirty( "", Int ) = 1
		_EmissionPattern("Emission Pattern", 2D) = "white" {}
		_MaskClipValue( "Mask Clip Value", Float ) = 0.5
		_MaxRadius("MaxRadius", Float) = 5
		_WaveForm("WaveForm", 2D) = "white" {}
		_WaveColor("Wave Color", Color) = (1,0,0,0)
		_SpotlightRadius("SpotlightRadius", Float) = 1
		_SpotlightPosition("SpotlightPosition", Vector) = (0,0,0,0)
		_SpotlightColor("SpotlightColor", Color) = (1,0,0,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Overlay+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Back
		Blend One One , One One
		BlendOp Add , Add
		CGINCLUDE
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		struct Input
		{
			float2 uv_texcoord;
			float3 worldPos;
		};

		uniform sampler2D _EmissionPattern;
		uniform float4 _EmissionPattern_ST;
		uniform sampler2D _WaveForm;
		uniform float3 Position;
		uniform float Radius;
		uniform float4 _WaveColor;
		uniform float _MaxRadius;
		uniform float3 _SpotlightPosition;
		uniform float _SpotlightRadius;
		uniform float4 _SpotlightColor;
		uniform float _MaskClipValue = 0.5;

		inline fixed4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return fixed4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float2 uv_EmissionPattern = i.uv_texcoord * _EmissionPattern_ST.xy + _EmissionPattern_ST.zw;
			float4 tex2DNode45 = tex2D( _EmissionPattern, uv_EmissionPattern );
			float3 ase_worldPos = i.worldPos;
			float temp_output_2_0 = distance( Position , ase_worldPos );
			float2 temp_cast_0 = (( 1.0 - saturate( ( temp_output_2_0 / Radius ) ) )).xx;
			o.Emission = ( tex2DNode45 * saturate( ( saturate( ( ( tex2D( _WaveForm, temp_cast_0 ) * _WaveColor ) * ( 1.0 - saturate( ( temp_output_2_0 / _MaxRadius ) ) ) ) ) + saturate( ( ( 1.0 - saturate( ( distance( _SpotlightPosition , ase_worldPos ) / _SpotlightRadius ) ) ) * _SpotlightColor ) ) ) ) ).xyz;
			o.Alpha = tex2DNode45.a;
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Unlit keepalpha fullforwardshadows 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			# include "HLSLSupport.cginc"
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			sampler3D _DitherMaskLOD;
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float3 worldPos : TEXCOORD6;
				float4 tSpace0 : TEXCOORD1;
				float4 tSpace1 : TEXCOORD2;
				float4 tSpace2 : TEXCOORD3;
				float4 texcoords01 : TEXCOORD4;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
				o.texcoords01 = float4( v.texcoord.xy, v.texcoord1.xy );
				o.worldPos = worldPos;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				return o;
			}
			fixed4 frag( v2f IN
			#if !defined( CAN_SKIP_VPOS )
			, UNITY_VPOS_TYPE vpos : VPOS
			#endif
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				Input surfIN;
				UNITY_INITIALIZE_OUTPUT( Input, surfIN );
				surfIN.uv_texcoord.xy = IN.texcoords01.xy;
				float3 worldPos = IN.worldPos;
				fixed3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				surfIN.worldPos = worldPos;
				SurfaceOutput o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutput, o )
				surf( surfIN, o );
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				half alphaRef = tex3D( _DitherMaskLOD, float3( vpos.xy * 0.25, o.Alpha * 0.9375 ) ).a;
				clip( alphaRef - 0.01 );
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=13101
7;29;1216;581;3113.587;1683.849;5.595114;True;False
Node;AmplifyShaderEditor.CommentaryNode;43;-2783.606,-357.8428;Float;False;2091.278;765.2473;LightWave;16;13;1;14;2;3;5;4;6;7;28;8;16;15;9;12;10;LightWave;1,1,1,1;0;0
Node;AmplifyShaderEditor.WorldPosInputsNode;1;-2714.705,-39.98495;Float;False;0;4;FLOAT3;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.Vector3Node;13;-2733.606,-307.8428;Float;False;Global;Position;Position;1;0;0,0,0;0;4;FLOAT3;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.DistanceOpNode;2;-2432.598,-172.8271;Float;True;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0.0,0;False;1;FLOAT
Node;AmplifyShaderEditor.CommentaryNode;42;-2350.873,533.9825;Float;False;1661.379;735.7211;Spotlight;10;29;30;31;32;33;34;36;35;38;40;Spotlight;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;14;-2206.455,-109.6638;Float;False;Global;Radius;Radius;1;0;1;0;0;0;1;FLOAT
Node;AmplifyShaderEditor.Vector3Node;29;-2300.873,583.9826;Float;False;Property;_SpotlightPosition;SpotlightPosition;6;0;0,0,0;0;4;FLOAT3;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.WorldPosInputsNode;30;-2281.972,851.8406;Float;False;0;4;FLOAT3;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SimpleDivideOpNode;3;-1998.979,-168.7973;Float;False;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.DistanceOpNode;32;-1999.865,718.9985;Float;True;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0.0,0;False;1;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;31;-2016.747,995.0028;Float;False;Property;_SpotlightRadius;SpotlightRadius;5;0;1;0;0;0;1;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;4;-2379.762,292.4044;Float;False;Property;_MaxRadius;MaxRadius;2;0;5;0;0;0;1;FLOAT
Node;AmplifyShaderEditor.SaturateNode;5;-1849.141,-168.1938;Float;True;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.OneMinusNode;7;-1660.381,-168.1469;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.SimpleDivideOpNode;6;-2163.816,242.861;Float;False;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.SimpleDivideOpNode;33;-1740.629,838.0483;Float;False;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.SaturateNode;8;-2003.069,278.1342;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.SaturateNode;34;-1563.519,791.5099;Float;True;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.SamplerNode;28;-1482.89,-216.7598;Float;True;Property;_WaveForm;WaveForm;3;0;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;FLOAT4;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.ColorNode;16;-1593.35,16.05951;Float;False;Property;_WaveColor;Wave Color;4;0;1,0,0,0;0;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.OneMinusNode;35;-1342.034,726.1074;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.ColorNode;36;-1341.4,874.5375;Float;False;Property;_SpotlightColor;SpotlightColor;7;0;1,0,0,0;0;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.OneMinusNode;9;-1816.518,278.1811;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;15;-1178.225,-42.85925;Float;False;2;2;0;FLOAT4;0.0;False;1;COLOR;0.0,0,0,0;False;1;FLOAT4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;12;-1121.4,146.6583;Float;False;2;2;0;FLOAT4;0.0;False;1;FLOAT;0.0,0,0,0;False;1;FLOAT4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;38;-1048.798,774.1292;Float;False;2;2;0;FLOAT;0.0;False;1;COLOR;0.0;False;1;COLOR
Node;AmplifyShaderEditor.SaturateNode;40;-864.4931,629.8017;Float;False;1;0;COLOR;0.0;False;1;COLOR
Node;AmplifyShaderEditor.SaturateNode;10;-867.3282,207.887;Float;False;1;0;FLOAT4;0.0;False;1;FLOAT4
Node;AmplifyShaderEditor.SimpleAddOpNode;41;-518.1595,443.5968;Float;False;2;2;0;FLOAT4;0.0,0,0,0;False;1;COLOR;0.0,0,0,0;False;1;FLOAT4
Node;AmplifyShaderEditor.SamplerNode;45;-563.752,-190.4315;Float;True;Property;_EmissionPattern;Emission Pattern;0;0;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;FLOAT4;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SaturateNode;44;-243.7476,264.1767;Float;False;1;0;FLOAT4;0.0;False;1;FLOAT4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;46;-133.8499,30.10266;Float;False;2;2;0;FLOAT4;0.0,0,0,0;False;1;FLOAT4;0.0;False;1;FLOAT4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;2;Float;ASEMaterialInspector;0;0;Unlit;EmitterParticles;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;Back;0;0;False;0;0;Custom;0.5;True;True;0;True;Transparent;Overlay;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;False;0;4;10;25;False;0.5;True;4;One;One;4;One;One;Add;Add;0;False;0;0,0,0,0;VertexOffset;False;Cylindrical;False;Relative;0;;1;-1;-1;-1;0;0;14;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;OBJECT;0.0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
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
WireConnection;46;0;45;0
WireConnection;46;1;44;0
WireConnection;0;2;46;0
WireConnection;0;9;45;4
ASEEND*/
//CHKSM=CD7B34620B31CE07249E1EA242FAC204B60CDA82