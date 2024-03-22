Shader "Game/Pixelation"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_PixelSize("Pixel Size", Int) = 4
		_PixelationType("Pixelation Type", Range(0, 1)) = 0
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
				int _PixelSize;
				float _PixelationType;

				fixed4 frag(v2f i) : SV_TARGET
				{
					float2 scale = float2(1, _ScreenParams.y / _ScreenParams.x) * _PixelSize;

					// Hard
					float2 cord = (floor(i.uv * scale) + 0.5) / scale;
					fixed4 hard = tex2D(_MainTex, cord);
					
					// Soft
					fixed4 color = fixed4(0, 0, 0, 0);
					for (float y = 0; y <= 1; y += 0.5)
						for (float x = 0; x <= 1; x += 0.5)
							color += tex2D(_MainTex, (floor(i.uv * scale) + float2(x, y)) / scale);
					fixed4 soft = color / 9;
					
					return hard * (1 - _PixelationType) + soft * _PixelationType;
				}
				ENDCG
			}
		}
}
