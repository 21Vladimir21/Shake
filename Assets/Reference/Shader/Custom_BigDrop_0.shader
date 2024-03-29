Shader "Custom/BigDrop" {
	Properties {
		_TextureMatCap ("TextureMatCap", 2D) = "white" {}
		_Color ("Color", Vector) = (1,1,1,1)
		_MatcapIntensity ("MatcapIntensity", Range(0, 1)) = 0.5
		_Frequency ("Frequency", Float) = 1
		_Amplitude ("Amplitude", Float) = 0.1
		[HideInInspector] __dirty ("", Float) = 1
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		fixed4 _Color;
		struct Input
		{
			float2 uv_MainTex;
		};
		
		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			o.Albedo = _Color.rgb;
			o.Alpha = _Color.a;
		}
		ENDCG
	}
	Fallback "Diffuse"
	//CustomEditor "ASEMaterialInspector"
}