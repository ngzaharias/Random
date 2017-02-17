// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Simplified Diffuse shader. Differences from regular Diffuse one:
// - no Main Color
// - fully supports only 1 directional light. Other lights can affect it, but it will be per-vertex/SH.

Shader "CustomMobile/Fresnel" 
{
	Properties 
	{
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_Colour("Color", Color) = (1,1,1,1)
		_RimColour("RimColor", Color) = (1,1,1,1)
		_RimSize("RimSize", float) = 1.0
		_RimPower("RimPower", float) = 1.0
	}

	SubShader 
	{
		Tags { "RenderType"="Opaque" }
		LOD 150

		CGPROGRAM
		#pragma surface surf Lambert noforwardadd finalcolor:fresnel

		sampler2D _MainTex;
		fixed4 _Colour;
		fixed4 _RimColour;
		float _RimSize;
		float _RimPower;

		struct Input 
		{
			fixed2 uv_MainTex;
			float3 viewDir;
			float3 worldNormal;
		};

		void surf (Input IN, inout SurfaceOutput o) 
		{
			float4 c = tex2D(_MainTex, IN.uv_MainTex);
			o.Albedo = c * _Colour;
			o.Alpha = c.a;
		}

		void fresnel(Input IN, SurfaceOutput o, inout fixed4 color)
		{
			float3 cam = _WorldSpaceCameraPos;
			float3 pos = unity_ObjectToWorld[3].xyz;
			float dist = length(pos - cam) / 10.0;
			dist = max(_RimSize, dist);

			float3 N = IN.worldNormal;
			float D = dot(normalize(IN.viewDir), N);
			float rim = saturate(dist - D);

			color += _RimColour * pow(rim, _RimPower);
		}

		ENDCG
	}

	Fallback "Mobile/VertexLit"
}
