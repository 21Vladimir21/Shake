Shader "Anzu/Video LED Playback Shader" {
	Properties {
		_ShouldFlipX ("Should Flip X", Float) = 0
		_ShouldFlipY ("Should Flip Y", Float) = 0
		_ShouldSwitchRB ("Should switch R/B", Float) = 0
		_HorizonalLeds ("Horizontal Resolution", Float) = 80
		_VerticalLeds ("Vertical Resolution", Float) = 60
		_Color ("Glow Color", Vector) = (1,1,1,1)
		_Glow ("Glow Intensity", Range(0, 3)) = 1
		_LuminanceSteps ("Luma Steps", Float) = 12
		_Brightness ("Brightness", Float) = 0
		_Contrast ("Contrast", Float) = 1
		_MainTex ("Base (RGB)", 2D) = "black" {}
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
}