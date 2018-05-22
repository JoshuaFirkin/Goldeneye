Shader "Custom/AlwaysVisible"
{
	Properties
	{
		_MeshColor("Mesh Color", Color) = (0.5, 0.5, 0.5, 1)
		_MainTex ("Texture", 2D) = "white" {}
		_Color("Visibility Color", Color) = (0,0,0,0)
	}

	SubShader
	{
		Tags { "Queue"="Transparent" }
		LOD 100

		Pass
		{
			Cull Off
			ZWrite Off

			ZTest Always

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag		
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
			};

			float4 _Color;


			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);

				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				return _Color;
			}

			ENDCG
		}


		Pass
		{
			ZWrite On

			Material
			{
				Diffuse[_MeshColor]
				Ambient[_MeshColor]
			}

			Lighting On

			SetTexture[_MainTex]
			{
				ConstantColor[_MeshColor]
			}

			SetTexture[_MainTex]
			{
				Combine previous * primary DOUBLE
			}
		}
	}
}
