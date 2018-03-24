Shader "Custom/NewSurfaceShader" {
	Properties {
		_MainTex("Texture", 2D) = "white" {}
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
		};

		void surf (Input IN, inout SurfaceOutputStandard o) {
			float a = tex2D(_MainTex, IN.uv_MainTex).a;
			o.color = float3(a,a,a);
			o.a = a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
