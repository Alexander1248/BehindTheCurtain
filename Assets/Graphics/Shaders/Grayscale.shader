Shader "Game/Grayscale"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_Grayscale("Grayscale", Float) = 0
	}

		SubShader
		{
			Cull Off ZWrite Off ZTest Always

			Pass
			{
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#include "UnityCG.cginc"

				struct appdata
				{
					float4 vertex : POSITION;
					float2 uv : TEXCOORD0;
				};

				struct v2f
				{
					float2 uv : TEXCOORD0;
					float4 vertex : SV_POSITION;
				};

				v2f vert(appdata v)
				{
					v2f o;
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.uv = v.uv;
					return o;
				}

				sampler2D _MainTex;
				float _Grayscale;

				fixed4 frag(v2f i) : SV_TARGET
				{
					fixed4 col = tex2D(_MainTex, i.uv);
					float brightness = 0.2126 * col.r + 0.7152 * col.g + 0.0722 * col.b;
					fixed4 gray = fixed4(brightness, brightness, brightness, 1);
					return col + (gray - col) * _Grayscale;
				}
				ENDCG
			}
		}
}
