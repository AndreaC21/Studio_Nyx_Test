Shader "Unlit/StripShader"
{
    Properties
    {
        _MainTex ("Albedo Texture",2D) = "white" {}
        _Color1("Color", Color) = (0,0,0,1)
        _Color2("Color", Color) = (1,1,1,1)
        _Tiling("Tiling", Range(1, 500)) = 10
    }
    SubShader
    {
        Tags { "Queue" = "Transparent" "RenderType"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            int _Tiling;
            fixed4 _Color1;
            fixed4 _Color2;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float pos = i.uv.y * _Tiling;
                float value = floor(frac(pos) + 0.5);
                return lerp(_Color1,_Color2,value);
            }

            ENDCG
        }
    }
}
