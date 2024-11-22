Shader "Custom/TwoColorGradient"
{
    Properties
    {
        _Transparency ("Transparency", Range(0, 1)) = 1.0 // Transparency slider (1 is fully opaque, 0 is fully transparent)
        _Color2 ("Color 2", Color) = (0, 0, 1, 1) // Blue by default
        _Color1 ("Color 1", Color) = (1, 0, 0, 1) // Red by default
    }

    SubShader
    {
        Tags { "Queue" = "Overlay" "RenderType" = "Transparent" }

        Pass
        {
            // Define vertex and fragment shaders
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
                float4 pos : POSITION;
                float2 uv : TEXCOORD0;
            };

            // Shader properties
            float4 _Color1;
            float4 _Color2;
            float _Transparency;

            // Vertex shader: Calculate vertex positions and pass UV coordinates
            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            // Fragment shader: Calculate final color with transparency
            float4 frag(v2f i) : SV_Target
            {
                // Interpolate between _Color1 and _Color2 based on the UV.x
                float4 color = lerp(_Color1, _Color2, i.uv.x);

                // Apply transparency by modifying the alpha channel
                color.a *= _Transparency;

                return color;
            }

            ENDCG

            // Define the blending mode for transparency
            Blend SrcAlpha OneMinusSrcAlpha
        }

        
    }

    // Fallback shader if this one cannot be used
    FallBack "Diffuse"
}