// very high skill issue

Shader "Unlit/ToonShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Brightness("Brightness", Range(0, 1)) = 0.3
        _Strength("Strength", Range(0, 1)) = 0.5
        _Color("Color", COLOR) = (1, 1, 1, 1)
        _Detail("Detail", Range(0, 1)) = 0.3

        _OutlineColor ("Color", Color) = (1, 0, 0, 1)
        _Size ("Size", Float) = 1.05
    }
    SubShader
    {
        Tags { "RenderType" = "Opaque" }
        LOD 100

        //Pass for Toon shader
        Pass
        {

            Name "Toon"

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                half3 worldNormal : NORMAL;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _Brightness;
            float _Strength;
            float4 _Color;
            float _Detail;

            float _Size;

            float Toon(float3 normal, float3 lightDir) {
                float NdotL = max(0.0, dot(normalize(normal), normalize(lightDir)));

                return floor(NdotL / _Detail);
            }

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                col *= Toon(i.worldNormal, _WorldSpaceLightPos0.xyz) * _Strength * _Color + _Brightness;

                float y = 0;
                y += step(_Size, i.uv.x);
                y -= step(1. - _Size, i.uv.x);
                y += step(_Size, i.uv.y);
                y -= step(1. - _Size, i.uv.y);
                return col * y;
            }
            ENDCG
        }

        // first pass turns off second pass or idk, anyway it doesn't work even if it would be properly made
        // Maybe i am simply dumb XD
        /*
        Pass
        {

            Name "Outline"

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 3.0
            #include "UnityCG.cginc"

            fixed4 _OutlineColor;

            sampler2D _MainTex;
            float4 _MainTex_ST;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                half3 worldNormal : NORMAL;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float y = 0.;
                y += step(_Size, i.uv.x);
                y -= step(1. - _Size, i.uv.x);


                return _OutlineColor * y;
            }

            ENDCG
        }
        */
        
        // also tried to use scale outline as second pass but it doesn't work as well :/

        /*
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
        */
    }
}