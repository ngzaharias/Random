Shader "Custom/MonumentShader" 
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}

		_ColourX("Colour X", Color) = (1,0,0,0)
		_ColourY("Colour Y", Color) = (0,1,0,0)
		_ColourZ("Colour Z", Color) = (0,0,1,0)

		_RimColour("Rim Colour", Color) = (0,0,0,0)

		_LightMultiplier("Light Multiplier", Color) = (1,1,1,0)
		_LightAdd("Light Add", Color) = (0,0,0,0)

		//_ShadowTint("Shadow Tint", Color) = (0,0,0,0)
		//_ShadowBoost("Shadow Boost", Float) = 1.0

		_FogColor("Fog Color", Color) = (1,1,1,0)
		_FogMinHeight("Fog Min Height", Float) = 0.0
		_FogMaxHeight("Fog Max Height", Float) = 1.0
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque" }
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
				fixed2 uv : TEXCOORD0;
				fixed3 normal : NORMAL;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				fixed2 uv : TEXCOORD0;
				fixed3 normal : TEXCOORD1;
				float3 position : TEXCOORD2;
			};

			struct Input
			{
				float2 uv_MainTex;
				float3 worldPos;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;

			fixed4 _ColourX;
			fixed4 _ColourY;
			fixed4 _ColourZ;

			fixed4 _RimColour;

			fixed4 _LightMultiplier;
			fixed4 _LightAdd;

			fixed4 _ShadowTint;
			float _ShadowBoost;

			fixed4 _FogColor;
			float _FogMaxHeight;
			float _FogMinHeight;

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.normal = UnityObjectToWorldNormal(v.normal);
				o.position = mul(unity_ObjectToWorld, v.vertex).xyz;
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				fixed4 colour = fixed4(0.0f, 0.0f, 0.0f, 1.0f);
				colour = lerp(colour, _ColourX, abs(i.normal.x));
				colour = lerp(colour, _ColourY, abs(i.normal.y));
				colour = lerp(colour, _ColourZ, abs(i.normal.z));

				colour.rgb *= _LightMultiplier.rgb;
				colour.rgb += _LightAdd.rgb;

				float lerpValue = clamp((i.position.y - _FogMinHeight) / (_FogMaxHeight - _FogMinHeight), 0, 1);
				colour.rgb = lerp(_FogColor.rgb, colour.rgb, lerpValue);

				return tex2D(_MainTex, i.uv) * colour;
			}

			ENDCG
		}
	}
}