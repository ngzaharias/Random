Shader "Custom/XRay_SolidColour"
{
	Properties
	{
		_Color("Main Color", Color) = (1,1,1,1)
	}

	SubShader
	{
		Stencil
		{
			Ref 0
			Comp NotEqual
		}

		Tags
		{
			"Queue" = "Transparent"
			"RenderType" = "Transparent"
			"XRay" = "SolidColour"
		}

		ZWrite Off
		ZTest Greater

		Pass
		{

			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				return o;
			}

			float4 _Color;

			fixed4 frag (v2f i) : SV_Target
			{
				return _Color;
			}

			ENDCG
		}
	}
}
