Shader "Custom/ProgresBar_Girl_2" {
	Properties {
		_Cutoff ("Mask Clip Value", Float) = 0.5
		_Color ("Color", Vector) = (1,1,1,1)
		_ShadingColor ("ShadingColor", Vector) = (0,0,0,1)
		_MainTex ("MainTex", 2D) = "white" {}
		_Emission ("Emission", Range(0, 1)) = 0
		_Matcap_Intensity ("Matcap_Intensity", Range(0, 1)) = 0
		_Matcap ("Matcap", 2D) = "white" {}
		_OutlineWidth ("OutlineWidth", Float) = 0
		_OutlineColor ("OutlineColor", Vector) = (0,0,0,0)
		_NotFilled ("NotFilled", Vector) = (0,0,0,0)
		_NotFilledOutline ("NotFilledOutline", Vector) = (0,0,0,0)
		_BlendMax ("BlendMax", Float) = 0
		_BlendMin ("BlendMin", Float) = 0
		_Blend ("Blend", Range(0, 1)) = 0.5
		[HideInInspector] _texcoord ("", 2D) = "white" {}
		[HideInInspector] __dirty ("", Float) = 1
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		sampler2D _MainTex;
		fixed4 _Color;
		struct Input
		{
			float2 uv_MainTex;
		};
		
		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
	Fallback "Diffuse"
	//CustomEditor "ASEMaterialInspector"
}