﻿Shader "Unlit/ProgressBar"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _AddTex("Additive Texture", 2D) = "white"{}
        _Color("Color", Color) = (1,1,1,1)
        _Speed("Speed", Range(-1,1)) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 color : COLOR;
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 color : COLOR;
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            sampler2D _AddTex;
            float4 _MainTex_ST;
            float4 _Color;
            float _Speed;

            v2f vert (appdata v)
            {
                v2f o;
                o.color = v.color;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                fixed2 uvs = fixed2(i.uv.x * 2 + (_Time.y * _Speed), i.uv.y);
                fixed4 addtex = tex2D(_AddTex, uvs) * _Color;
                return fixed4(col.rgb + (addtex.rgb * _Color.a), col.a) * i.color;  
            }
            ENDCG
        }
    }
}
