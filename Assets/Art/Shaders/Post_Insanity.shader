// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Hidden/Post_Insanity"
{
	Properties
	{
		_MainTex ( "Screen", 2D ) = "black" {}
		_MainTex("MainTex", 2D) = "white" {}
		_Offset("Offset", Float) = 0
		_Perlin("Perlin", 2D) = "white" {}
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
			
			uniform float _Offset;
			uniform sampler2D _Perlin;
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
				float temp_output_44_0 = ( _Offset * _SinTime.w );
				float2 temp_cast_0 = (temp_output_44_0).xx;
				float2 uv42 = i.uv.xy*float2( 1,1 ) + temp_cast_0;
				float4 appendResult51 = (float4(( temp_output_44_0 * -1.0 ) , temp_output_44_0 , 0.0 , 0.0));
				float2 uv48 = i.uv.xy*float2( 1,1 ) + appendResult51.xy;
				float mulTime58 = _Time.y * 0.1;
				float4 appendResult60 = (float4(mulTime58 , 0.0 , 0.0 , 0.0));
				float2 uv57 = i.uv.xy*float2( 1,1 ) + appendResult60.xy;
				float4 lerpResult6 = lerp( tex2D( _MainTex, uv_MainTex ) , saturate( ( saturate( ( ( tex2D( _MainTex, uv_MainTex ) + tex2D( _MainTex, uv42 ) + tex2D( _MainTex, uv48 ) ) * float4(0.03921569,0.03921569,0.03921569,0) ) ) + ( tex2D( _Perlin, uv57 ) * 0.005 ) ) ) , _InsanityLevel);

				finalColor = lerpResult6;

				return finalColor;
			} 
			ENDCG 
		}
	}
}
/*ASEBEGIN
Version=13101
47;87;1232;618;3315.514;348.5674;3.571816;True;False
Node;AmplifyShaderEditor.CommentaryNode;53;-2631.067,-297.6482;Float;False;1918.943;745.4238;;14;39;41;46;43;44;47;51;48;42;49;30;38;34;40;Offset and coloring;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;43;-2581.065,54.92798;Float;False;Property;_Offset;Offset;2;0;0;0;0;0;1;FLOAT
Node;AmplifyShaderEditor.SinTimeNode;46;-2581.067,140.0919;Float;False;0;5;FLOAT4;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;44;-2371.616,98.06052;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;47;-2299.653,241.3245;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;-1.0;False;1;FLOAT
Node;AmplifyShaderEditor.DynamicAppendNode;51;-2099.791,229.3291;Float;False;FLOAT4;4;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;0.0;False;3;FLOAT;0.0;False;1;FLOAT4
Node;AmplifyShaderEditor.TextureCoordinatesNode;48;-1932.967,229.329;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.TextureCoordinatesNode;42;-1945.263,10.36526;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SimpleTimeNode;58;-1824.948,577.2027;Float;False;1;0;FLOAT;0.1;False;1;FLOAT
Node;AmplifyShaderEditor.SamplerNode;49;-1702.703,-247.6482;Float;True;Property;_TextureSample1;Texture Sample 1;3;0;None;True;0;False;white;Auto;False;Instance;1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SamplerNode;38;-1693.839,205.3517;Float;True;Property;_TextureSample2;Texture Sample 2;2;0;None;True;0;False;white;Auto;False;Instance;1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.DynamicAppendNode;60;-1590.54,590.8305;Float;False;FLOAT4;4;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;0.0;False;3;FLOAT;0.0;False;1;FLOAT4
Node;AmplifyShaderEditor.SamplerNode;30;-1702.666,-34.54372;Float;True;Property;_TextureSample0;Texture Sample 0;2;0;None;True;0;False;white;Auto;False;Instance;1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.ColorNode;40;-1334.957,238.0499;Float;False;Constant;_Color3;Color 3;2;0;0.03921569,0.03921569,0.03921569,0;0;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SimpleAddOpNode;34;-1262.369,100.575;Float;False;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR
Node;AmplifyShaderEditor.TextureCoordinatesNode;57;-1410.646,525.4141;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;39;-1106.855,95.67682;Float;False;2;2;0;COLOR;0.0;False;1;COLOR;0,0,0,0;False;1;COLOR
Node;AmplifyShaderEditor.SamplerNode;56;-1069.938,577.2018;Float;True;Property;_Perlin;Perlin;3;0;Assets/Art/Images/Perlin noise.png;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;FLOAT4;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;61;-979.9918,800.7068;Float;False;Constant;_PerlinPresence;Perlin Presence;4;0;0.005;0;0;0;1;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;62;-767.3889,596.2819;Float;True;2;2;0;FLOAT4;0.0;False;1;FLOAT;0,0,0,0;False;1;FLOAT4
Node;AmplifyShaderEditor.SaturateNode;41;-887.1234,81.22437;Float;False;1;0;COLOR;0.0;False;1;COLOR
Node;AmplifyShaderEditor.SimpleAddOpNode;54;-471.2679,459.517;Float;True;2;2;0;COLOR;0.0;False;1;FLOAT4;0,0,0,0;False;1;COLOR
Node;AmplifyShaderEditor.SamplerNode;1;-81.55075,-91.1516;Float;True;Property;_MainTex;MainTex;0;0;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SaturateNode;63;-278.642,392.8895;Float;False;1;0;COLOR;0.0;False;1;COLOR
Node;AmplifyShaderEditor.RangedFloatNode;8;-68.83826,768.4863;Float;False;Property;_InsanityLevel;Insanity Level;1;0;0.02041683;0;1;0;1;FLOAT
Node;AmplifyShaderEditor.LerpOp;6;279.3506,226.5154;Float;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0.0;False;1;COLOR
Node;AmplifyShaderEditor.TemplateMasterNode;0;561.9337,342.6599;Float;False;True;2;Float;ASEMaterialInspector;0;1;Hidden/Post_Insanity;c71b220b631b6344493ea3cf87110c93;1;0;FLOAT4;0,0,0,0;False;0
WireConnection;44;0;43;0
WireConnection;44;1;46;4
WireConnection;47;0;44;0
WireConnection;51;0;47;0
WireConnection;51;1;44;0
WireConnection;48;1;51;0
WireConnection;42;1;44;0
WireConnection;38;1;48;0
WireConnection;60;0;58;0
WireConnection;30;1;42;0
WireConnection;34;0;49;0
WireConnection;34;1;30;0
WireConnection;34;2;38;0
WireConnection;57;1;60;0
WireConnection;39;0;34;0
WireConnection;39;1;40;0
WireConnection;56;1;57;0
WireConnection;62;0;56;0
WireConnection;62;1;61;0
WireConnection;41;0;39;0
WireConnection;54;0;41;0
WireConnection;54;1;62;0
WireConnection;63;0;54;0
WireConnection;6;0;1;0
WireConnection;6;1;63;0
WireConnection;6;2;8;0
WireConnection;0;0;6;0
ASEEND*/
//CHKSM=FC823CE776550C5E5CD3DEE9EE1A89F64A041FAF