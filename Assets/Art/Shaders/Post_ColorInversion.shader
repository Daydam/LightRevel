// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Post_ColorInversion"
{
	Properties
	{
		_MainTex ( "Screen", 2D ) = "black" {}
		_MainTex("MainTex", 2D) = "white" {}
		_Noise("Noise", 2D) = "white" {}
		_CloudSpeed("CloudSpeed", Vector) = (1,1,0,0)
		_InsanityLevel("Insanity Level", Range( 0 , 1)) = 0.02041683
	}

	SubShader
	{
		Tags{  }
		
		ZTest Always Cull Off ZWrite Off
		


		Pass
		{ 
			CGPROGRAM 

			#pragma vertex vert_img_custom 
			#pragma fragment frag
			#pragma target 3.0
			#include "UnityCG.cginc"
			#include "UnityShaderVariables.cginc"


			struct appdata_img_custom
			{
				float4 vertex : POSITION;
				half2 texcoord : TEXCOORD0;
			};

			struct v2f_img_custom
			{
				float4 pos : SV_POSITION;
				half2 uv   : TEXCOORD0;
				half2 stereoUV : TEXCOORD2;
		#if UNITY_UV_STARTS_AT_TOP
				half4 uv2 : TEXCOORD1;
				half4 stereoUV2 : TEXCOORD3;
		#endif
			};

			uniform sampler2D _MainTex;
			uniform half4 _MainTex_TexelSize;
			uniform half4 _MainTex_ST;
			
			uniform sampler2D _Noise;
			uniform float2 _CloudSpeed;
			uniform float _InsanityLevel;

			v2f_img_custom vert_img_custom ( appdata_img_custom v  )
			{
				v2f_img_custom o;

				o.pos = UnityObjectToClipPos ( v.vertex );
				o.uv = float4( v.texcoord.xy, 1, 1 );

				#ifdef UNITY_HALF_TEXEL_OFFSET
						o.uv.y += _MainTex_TexelSize.y;
				#endif

				#if UNITY_UV_STARTS_AT_TOP
					o.uv2 = float4( v.texcoord.xy, 1, 1 );
					o.stereoUV2 = UnityStereoScreenSpaceUVAdjust ( o.uv2, _MainTex_ST );

					if ( _MainTex_TexelSize.y < 0.0 )
						o.uv.y = 1.0 - o.uv.y;
				#endif
				o.stereoUV = UnityStereoScreenSpaceUVAdjust ( o.uv, _MainTex_ST );
				return o;
			}

			half4 frag ( v2f_img_custom i ) : SV_Target
			{
				#ifdef UNITY_UV_STARTS_AT_TOP
					half2 uv = i.uv2;
					half2 stereoUV = i.stereoUV2;
				#else
					half2 uv = input.uv;
					half2 stereoUV = i.stereoUV;
				#endif	
				
				half4 finalColor;

				// ase common template code
				float2 uv_MainTex = i.uv.xy*_MainTex_ST.xy + _MainTex_ST.zw;
				float4 tex2DNode1 = tex2D( _MainTex, uv_MainTex );
				float mulTime80 = _Time.y * 0.1;
				float4 appendResult81 = (float4(mulTime80 , mulTime80 , 0.0 , 0.0));
				float2 uv82 = i.uv.xy*float2( 1,1 ) + ( float4( _CloudSpeed, 0.0 , 0.0 ) * appendResult81 ).xy;
				float4 lerpResult79 = lerp( tex2DNode1 , ( tex2DNode1 * _SinTime * tex2D( _Noise, uv82 ).r ) , _InsanityLevel);

				finalColor = lerpResult79;

				return finalColor;
			} 
			ENDCG 
		}
	}
}
/*ASEBEGIN
Version=13101
-40;128;1232;618;886.576;-85.71098;1.302403;True;False
Node;AmplifyShaderEditor.SimpleTimeNode;80;-1288.59,732.1814;Float;False;1;0;FLOAT;0.1;False;1;FLOAT
Node;AmplifyShaderEditor.Vector2Node;83;-1092.258,553.5352;Float;False;Property;_CloudSpeed;CloudSpeed;3;0;1,1;0;3;FLOAT2;FLOAT;FLOAT
Node;AmplifyShaderEditor.DynamicAppendNode;81;-1054.181,745.8092;Float;False;FLOAT4;4;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;0.0;False;3;FLOAT;0.0;False;1;FLOAT4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;84;-873.8559,653.6352;Float;False;2;2;0;FLOAT2;0.0;False;1;FLOAT4;0.0,0;False;1;FLOAT4
Node;AmplifyShaderEditor.TextureCoordinatesNode;82;-642.8873,580.2932;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SamplerNode;1;-389.2329,89.74391;Float;True;Property;_MainTex;MainTex;0;0;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SinTimeNode;71;-142.1642,362.5481;Float;False;0;5;FLOAT4;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SamplerNode;78;-261.4981,538.5547;Float;True;Property;_Noise;Noise;3;0;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;FLOAT4;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;70;168.3732,430.834;Float;False;3;3;0;COLOR;0.0;False;1;FLOAT4;0.0,0,0,0;False;2;FLOAT;0,0,0,0;False;1;COLOR
Node;AmplifyShaderEditor.RangedFloatNode;8;-265.1889,763.1471;Float;False;Property;_InsanityLevel;Insanity Level;1;0;0.02041683;0;1;0;1;FLOAT
Node;AmplifyShaderEditor.LerpOp;79;390.9283,407.1512;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0.0;False;2;FLOAT;0.0;False;1;COLOR
Node;AmplifyShaderEditor.TemplateMasterNode;0;675.7435,384.9321;Float;False;True;2;Float;ASEMaterialInspector;0;1;Post_ColorInversion;c71b220b631b6344493ea3cf87110c93;1;0;FLOAT4;0,0,0,0;False;0
WireConnection;81;0;80;0
WireConnection;81;1;80;0
WireConnection;84;0;83;0
WireConnection;84;1;81;0
WireConnection;82;1;84;0
WireConnection;78;1;82;0
WireConnection;70;0;1;0
WireConnection;70;1;71;0
WireConnection;70;2;78;1
WireConnection;79;0;1;0
WireConnection;79;1;70;0
WireConnection;79;2;8;0
WireConnection;0;0;79;0
ASEEND*/
//CHKSM=BCC7CF6207DEBAB3DD4AB714FEA20B23514815FC