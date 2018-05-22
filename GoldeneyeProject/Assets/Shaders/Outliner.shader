Shader "Custom/Outliner"
{
	Properties
	{
		_MeshColor("Mesh Color", Color) = (0.5, 0.5, 0.5, 1)
		_MainTex ("Texture", 2D) = "white" {}
		_OutlineColor("Outline Color", Color) = (0, 0, 0, 1)
		_OutlineWidth("Outline Width", Range(1.0, 5.0)) = 1.01
		_PulseSpeed("Pulse Speed", Range(0.0, 5.0)) = 1.0
	}

	CGINCLUDE
	#include "UnityCG.cginc"

	struct appdata
	{
		float4 vertex : POSITION;
		float2 normal : NORMAL;
	};

	struct v2f
	{
		float4 pos : POSITION;
		float2 normal : NORMAL;
	};

	float4 _OutlineColor;
	float _OutlineWidth;
	float _PulseSpeed;

	v2f vert(appdata v)
	{
		v.vertex.xyz *= _OutlineWidth;

		v2f o;
		o.pos = UnityObjectToClipPos(v.vertex);
		return o;
	}

	ENDCG

	SubShader
	{
		Tags { "Queue"="Transparent" }

		// For rendering the outline.
		Pass
		{
			ZWrite Off

			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag

			half4 frag(v2f i) : COLOR
			{
				float4 col = _OutlineColor * sin(_Time.y * _PulseSpeed);

				if (col.x < 0)
				{
					col *= (-1, -1, -1, -1);
				}

				return col;
			}

			ENDCG
		}

		// For rendering the object.
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
