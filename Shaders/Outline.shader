Shader "Custom/Outline"
{
    Properties
    {
        _Color ("Color", Color) = (1, 0, 0, 1)
        _Size ("Size", Float) = 1.05
    }
    SubShader
    {
        Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }
        LOD 100

        Cull Front // Equivalent to `cull_front` in Godot
        Blend SrcAlpha OneMinusSrcAlpha // Equivalent to `blend_mix`
        ZWrite Off

        Pass
        {
            Name "Outline"
            Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }
            LOD 100

            Cull Front // Equivalent to `cull_front` in Godot
            Blend SrcAlpha OneMinusSrcAlpha // Equivalent to `blend_mix`
            ZWrite Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 3.0
            #include "UnityCG.cginc"

            fixed4 _Color;
            float _Size;

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                v.vertex.xyz *= _Size; // Scale vertex by size
                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return _Color;
            }
            ENDCG
        }
    }
    FallBack "Unlit/Transparent"
}
