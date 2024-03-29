Shader "Custom/Get Lucky 01" {
	Properties {
		_Float0 ("Float 0", Float) = 0
		_Color ("Color", Vector) = (0,0,0,0)
		_Texture ("Texture", 2D) = "white" {}
		_Shhading ("Shhading", Float) = 0
		_Shhading2 ("Shhading 2", Float) = 0
		_Shhadingcolor ("Shhading color", Vector) = (0,0,0,0)
		_Shhading2color ("Shhading 2 color", Vector) = (0,0,0,0)
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