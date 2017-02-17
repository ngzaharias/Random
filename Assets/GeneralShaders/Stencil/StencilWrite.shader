Shader "Custom/StencilWrite" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		Stencil
		{
			Ref 1
			Comp Always
			Pass Replace
			ZFail Keep
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
