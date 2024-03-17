Shader "UI/Gradient"
{
    Properties
    {
        _TopColor ("Top Color", Color) = (1, 1, 1, 1)
        _BottomColor ("Bottom Color", Color) = (0, 0, 0, 1)
        _Direction ("Direction", Vector) = (0, 1, 0, 0)
    }
    SubShader
    {
        Tags { "Queue"="Overlay" "IgnoreProjector"="True" "RenderType"="Transparent" }
        LOD 100

        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        ZWrite Off

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
                fixed4 vertex : SV_POSITION;
            };

            fixed4 _TopColor;
            fixed4 _BottomColor;
            float4 _Direction;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            // Simple Gradient Shader learping _BottomColor and _TopColor
            fixed4 frag (v2f i) : SV_Target
            {
                float gradientFactor = dot(i.uv, _Direction);
                fixed4 gradientColor = lerp(_BottomColor, _TopColor, gradientFactor);

                return gradientColor;
            }
            ENDCG
        }
    }
}