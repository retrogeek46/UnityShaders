// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Unlit/OutlineShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Color("Main Color", Color) = (0.5,0.5,0.5,1)
		_OutlineColor("Outline color", Color) = (0,0,0,1)
		_OutlineWidth("Outline width", Range(0.0, 2.0)) = 1.01
	}

	CGINCLUDE
	#include "UnityCG.cginc"

	struct appdata {
		float4 vertex : POSITION;
		float3 normal : NORMAL;
		float2 tex : TEXCOORD0;
	};

	struct v2f {
		float4 pos : POSITION;
		float3 normal : NORMAL;
	};

	float _OutlineWidth;
	float4 _OutlineColor;	
	sampler2D _MainTex;	
	float4 _Color;
	float _SampleValues[64];

	v2f vert(appdata v) 
	{
		v.vertex.xyz *= _OutlineWidth;

		float step = 0.03125;

		float3 castToWorld = mul(unity_ObjectToWorld, v.vertex);

		for(float i = 0; i < 64; i++) {
			if(castToWorld.y > i && castToWorld.y < i + step) {
				if(castToWorld.x > 0) {
					v.vertex.x -= _SampleValues[i] / 2;
				} else {
					v.vertex.x += _SampleValues[i] / 2;
				}
			}
		}

		v2f o;
		o.pos = UnityObjectToClipPos(v.vertex);
		return o;
	}

	ENDCG

	SubShader
	{
		Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
		LOD 100

		Pass	// outline
		{
			Zwrite Off
			Cull Back

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			//float4 _OutlineColor;
			//float _outlineWidth;

			//v2f vert(appdata v) {
			//	appdata original = v;
			//	v.vertex.xyz += _OutlineWidth;

			//	v2f o;
			//	o.pos = UnityObjectToClipPos(v.vertex);
			//	return o;
			//}

			half4 frag(v2f i): COLOR
			{
				return _OutlineColor;
			}
			ENDCG
		}

		Pass 
		{
			ZWrite On

			Material
			{
				Diffuse[_Color]
				Ambient[_Color]
			}

			Lighting On

			SetTexture[_MainTex]
			{
				ConstantColor[_Color]
			}

			SetTexture[_MainTex]
			{
				Combine previous * primary DOUBLE
			}
		}
	}
}
