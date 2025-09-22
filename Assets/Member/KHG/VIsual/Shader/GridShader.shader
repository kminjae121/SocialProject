Shader "Custom/WorldGridOverlay"
{
    Properties
    {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _BaseColor ("Base Color", Color) = (1,1,1,1)
        _GridColor ("Grid Color", Color) = (0,0,0,1)
        _GridSize ("Grid Size", Float) = 1.0
        _LineThickness ("Line Thickness (world units)", Float) = 0.02
        _Feather ("Feather (world units)", Float) = 0.005
        _GridOpacity ("Grid Opacity", Range(0,1)) = 1.0
        _BlendMode ("Blend Mode (0=Overlay,1=Replace)", Range(0,1)) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _BaseColor;
            float4 _GridColor;
            float _GridSize;
            float _LineThickness;
            float _Feather;
            float _GridOpacity;
            float _BlendMode;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 pos : SV_POSITION;
                float3 worldPos : TEXCOORD1;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                return o;
            }

            float GridLineAlpha(float coord, float size, float thickness, float feather)
            {
                float scaled = coord / size;       // 스케일 조정
                float f = frac(scaled);            // [0,1)
                float dist = min(f, 1.0 - f);      // 가장 가까운 라인까지 거리
                float half = (thickness / size) * 0.5;
                return 1.0 - smoothstep(half - feather, half + feather, dist);
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 baseCol = tex2D(_MainTex, i.uv) * _BaseColor;

                float wx = i.worldPos.x;
                float wz = i.worldPos.z;

                float ax = GridLineAlpha(wx, _GridSize, _LineThickness, _Feather);
                float az = GridLineAlpha(wz, _GridSize, _LineThickness, _Feather);

                float a = saturate(max(ax, az) * _GridOpacity);

                fixed4 gcol = _GridColor;
                gcol.a = a;

                fixed4 outCol;
                if (_BlendMode < 0.5)
                    outCol = lerp(baseCol, gcol, gcol.a);   // Overlay
                else
                    outCol = lerp(baseCol, gcol, step(0.001, gcol.a)); // Replace

                return outCol;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
