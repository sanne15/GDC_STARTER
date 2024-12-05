Shader "Unlit/blurshader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _BlurSize ("Blur Size", Float) = 0.005
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _BlurSize;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                float4 col = 0;
                float2 offset = _BlurSize * float2(1.0, 0.0);
                col += tex2D(_MainTex, i.uv - offset);
                col += tex2D(_MainTex, i.uv + offset);
                offset = _BlurSize * float2(0.0, 1.0);
                col += tex2D(_MainTex, i.uv - offset);
                col += tex2D(_MainTex, i.uv + offset);
                col *= 0.25;
                return col;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
