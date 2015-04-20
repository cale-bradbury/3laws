Shader "CampCult/Wireframe" {
    Properties {
	    _Color("Color",Color)=(1.0,1.0,1.0,1.0)
	    _FaceColor("FaceColor",Color)=(0.0,0.0,0.0,0.0)
	 	_EdgeSize("EdgeSize",Range (0.0, 1.0))=0.1
    }
    Category{
		Blend SrcAlpha OneMinusSrcAlpha
		Tags {"Queue" = "Transparent"}
	    SubShader {
	        Pass {   
	    		//Lighting Off       
	    		//Cull Back  
	            CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma target 3.0
				#include "UnityCG.cginc"
				float4 _Color;
				float4 _FaceColor;
				float _EdgeSize;
				
				struct appdata {
					float4 vertex : POSITION;
					float4 tex:TEXCOORD0;
				};
				struct v2f {
					float4 pos : POSITION;	
					float4 vert: TEXCOORD0;	
					//float4 world;	
				};
				v2f vert(appdata v) {
					v2f o;
					o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
					o.vert = v.tex;//v.vertex;
					//o.world = mul(_Object2World, v.vertex);
					return o;
				};
				half4 frag(v2f i) : COLOR {
				float s = min(i.vert.x,min(i.vert.y,min(1.0-i.vert.x,1.0-i.vert.y)));
					return lerp(_Color,_FaceColor,step(_EdgeSize,s));
				};
				ENDCG
	        }
	    }
    }
    Fallback "VertexLit"
}