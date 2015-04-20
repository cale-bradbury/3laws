Shader "Cale/Kalidescope" {
Properties {
	_MainTex ("Base (RGB)", 2D) = "white" {}
	_Angle ("Angle", float) = 1
	_BaseAngle("BaseAngle",float) = 1
	_SpinAngle("SpinAngle",float) = 0
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

uniform sampler2D _MainTex;
uniform float4 _MainTex_ST;
uniform float _Angle;
uniform float _BaseAngle;
uniform float _SpinAngle;

fixed4 frag (v2f_img i) : COLOR
{
	float2 uv = i.uv;
	uv-=0.5;
	float a = atan2(uv.y,uv.x)+_SpinAngle;
	float d = length(uv);
	a = mod(a,_Angle*2);
	a = abs(a-_Angle)+_BaseAngle;
	uv.x = sin(a)*d;
	uv.y = cos(a)*d;	
	return tex2D(_MainTex, (uv+0.5)*_MainTex_ST.xy+_MainTex_ST.zw);
}
ENDCG

	}
}

Fallback off

}