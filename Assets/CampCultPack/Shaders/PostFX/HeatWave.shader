Shader "Shader/HeatWave" {
Properties {
	_MainTex ("Base (RGB)", 2D) = "white" {}
	_DepthMap ("Depth Map", 2D) = "white" {}
	_Strength("Strength",float)=0
	_Phase("Phase",float)=0
	_Freq ("Freq",float)=1
	_Taps("Taps",float) = 3
}

SubShader {
	Pass {
		ZTest Always Cull Off ZWrite Off
		Fog { Mode off }
				
CGPROGRAM
#pragma vertex vert_img
#pragma fragment frag
#pragma fragmentoption ARB_precision_hint_fastest 
#pragma target 3.0
#include "UnityCG.cginc"
#define PI 3.14158

uniform sampler2D _MainTex;
uniform sampler2D _DepthMap;
uniform float _Strength;
uniform float _Phase;
uniform float _Freq;
uniform float _Taps;

fixed4 frag (v2f_img i) : COLOR
{
	float t = PI*2/_Taps;
	float4 c = 0.0;
	for(int j = 0; j<floor(_Taps);j++){
		c+=tex2Dlod(_MainTex,float4(i.uv.x+sin(i.uv.y*_Freq+_Phase*_Time.y+j*t)*_Strength,i.uv.y,0,1));
	}
	return c/_Taps;
}
ENDCG

	}
}

Fallback off

}