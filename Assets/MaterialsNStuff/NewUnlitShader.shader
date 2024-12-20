Shader "Unlit/NewUnlitShader"
{
    Properties
    {
        _TopColor("Top Gradient Color: ", Color) = (1,1,1,1)
        _BottomColor("Bottom Gradient Color: ", Color) = (0,0,0,1)
        _Transparency("Transparency", Range(0, 1)) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 color : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            fixed4 _TopColor;
            fixed4 _BottomColor;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.color = lerp(_BottomColor, _TopColor, v.uv.y);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
               float4 color = i.color;


               //color.a *= _Transparency;
               return color;
            }
            ENDCG
        }
    }
}
