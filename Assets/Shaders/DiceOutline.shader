Shader "Custom/DiceOutline"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _OutlineTex ("Outline Texture (RGB)", 2D) = "white" {}
        _OutlineColor ("Outline Colour", Color) = (1,1,1,1)
        _OutlineThickness("Outline Thickness", Range(1, 10)) = 1.1
    }
    SubShader
    {
        Tags { "Queue"="Transparent" }
        LOD 200

        //Outline pass, scaled mesh rendered behind object
        Pass{
            Zwrite Off
            CGPROGRAM            
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            sampler2D _OutlineTex;
            fixed4 _OutlineColor;
            float _OutlineThickness;

            //object -> vertex data
            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };


            struct v2f {
                float4 position : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            v2f vert(appdata v) {
                
                v.vertex.xyz *= _OutlineThickness;
                v2f o;
                o.position = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            };

            fixed4 frag(v2f i) : SV_TARGET{
                fixed4 col = tex2D(_OutlineTex, i.uv);
                col *= _OutlineColor;
                return col;
            };

            ENDCG
        }
        //Outline pass, extruded normals to remove forshortening in first outline pass
        Pass{
            Zwrite Off
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            sampler2D _OutlineTex;
            fixed4 _OutlineColor;
            float _OutlineThickness;

            //object -> vertex data
            struct appdata {
                float4 vertex : POSITION;
                float4 normal : NORMAL;
                float2 uv : TEXCOORD0;
            };


            struct v2f {
                float4 position : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            v2f vert(appdata v) {
                v2f o;
                //float3 normal = normalize(v.normal);
                //float3 outlineOffset = normal * _OutlineThickness;
                //float3 position = v.vertex + outlineOffset;
                //o.position = UnityObjectToClipPos(position);
                
                //v.vertex.xyz += v.normal * (1-_OutlineTickness);
                //o.position = UnityObjectToClipPos(v.vertex);
                
                //float4 clipPosition = UnityObjectToClipPos(v.vertex);
                //float3 clipNormal = mul((float3x3) UNITY_MATRIX_VP, mul((float3x3) UNITY_MATRIX_M, v.normal));
                //float2 offset = normalize(clipNormal.xy) * (_OutlineThickness) * clipPosition.w;
                //clipPosition.xyz += normalize(clipNormal) * (_OutlineThickness);
                //clipPosition.xy += offset;
                //o.position = clipPosition;
                o.position = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            };

            fixed4 frag(v2f i) : SV_TARGET{
                fixed4 col = tex2D(_OutlineTex, i.uv);
                col *= _OutlineColor;
                return col;
            };

            ENDCG
        }


        //Object Pass
        Pass{
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            sampler2D _MainTex;
            fixed4 _Color;

            //object -> vertex data
            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };


            struct v2f {
                float4 position : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            v2f vert(appdata v) {
                v2f o;
                o.position = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            };

            fixed4 frag(v2f i) : SV_TARGET{
                fixed4 col = tex2D(_MainTex, i.uv);
                col *= _Color;
                return col;
            };

            ENDCG
            }
    }
    FallBack "Diffuse"
}
