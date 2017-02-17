Shader "Custom/StencilRead" 
{
	Properties 
	{
		_Color ("Color", Color) = (1,1,1,1)
	}
	SubShader 
	{
		Tags 
		{ 
			"Queue" = "Transparent"
			"RenderType"="Transparent" 
		}
		LOD 200
		ZWrite Off
		ZTest Less
		
		Stencil
		{
			Ref 0
			Comp NotEqual
		}

		CGPROGRAM
		#pragma surface surf Lambert

		struct Input {
			float2 empty;
		};

		fixed4 _Color;

		void surf (Input IN, inout SurfaceOutput o)
		{
			fixed4 c = _Color;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
