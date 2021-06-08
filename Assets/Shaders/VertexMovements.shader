Shader "Unlit/VertexMovement"
{
    Properties
    {
        _HorizontalOffsetMin("Horizontal Offset Min",Float) = 0
        _HorizontalOffsetMax("Horizontal Offset Max",Float) = 5
        _Color("Color", Color) = (1,1,1,1)
        _MainTex("Texture2D", 2D) = "white" {}
        _SecTex("Secondary Texture2D",2D) = "white"{}
        _Speed("Speed", Float) = 1
        _MovementSpeed("Movement Speed", Float) = 1
    }
        SubShader
        {
            Tags { "RenderType" = "Opaque" }
            Blend SrcAlpha OneMinusSrcAlpha
            Pass
            {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag

                #include "UnityCG.cginc"

                struct appdata
                {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                };

                struct v2f
                {
                    float2 uv : TEXCOORD0;
                    float4 vertex : SV_POSITION;
                };

                float4 _HorizontalOffsetMin;
                float _HorizontalOffsetMax;
                half4 _Color;
                sampler2D _MainTex;
                float4 _MainTex_ST;
                sampler2D _SecTex;
                float4 _SecTex_ST;
                float _Speed;
                float _MovementSpeed;

                v2f vert(appdata v)
                {
                    v2f o;
                    v.vertex.y += lerp(_HorizontalOffsetMin, _HorizontalOffsetMax, abs(fmod(_Time.a * _MovementSpeed, 2.0) - 1.0));
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    fixed4 col = lerp(tex2D(_MainTex, i.uv), tex2D(_SecTex, i.uv), abs(fmod(_Time.a * _Speed, 2.0) - 1.0));
                    col *= _Color;
                    return col;
                }
                ENDCG
            }
        }
}