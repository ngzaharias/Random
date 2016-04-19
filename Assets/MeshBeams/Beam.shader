Shader "Custom/Unlit/Beam"
{
	Properties
	{
		_Colour("Colour", Color) = (1,1,1,1)
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
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

			float4 _Colour;
			
			v2f vert (appdata v)
			{
				float4 vert = v.vertex;
				vert.y += sin( (vert.x + _Time.y) * 30.0f ) / 8.0f;

				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, vert);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				return _Colour;
			}
			ENDCG
		}
	}
}
