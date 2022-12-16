Shader "Unlit/InteractArea"
{
    Properties
    {
        [NoScaleOffset] _Alpha ("Alpha", 2D) = "white" {}
        [NoScaleOffset] _Effect ("Effect", 2D) = "white" {}
    }
    SubShader
    {
        // Set additive blending mode
        Blend One One

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            float _Density;

            v2f vert (float4 pos : POSITION, float2 uv : TEXCOORD0)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(pos);
                o.uv = uv;
                return o;
            }

            sampler2D _Effect;
            sampler2D _Alpha;

            fixed4 frag (v2f i) : SV_Target
            {
                // First texture UV
                float2 fx_uv = i.uv;
                //fx_uv.x *= 1.0;
                fx_uv.y *= 4.0;
                fx_uv.y -= _Time.y * 0.1;

                // Second texture UV
                float2 fx_uv2 = i.uv;
                fx_uv2.x *= 0.5;
                fx_uv2.y *= 2.0;
                fx_uv2.x -= _Time.y * 0.05;
                fx_uv2.y -= _Time.y * 0.15;

                // Mix first and second texture,
                // then blend with alpha texture
                return (tex2D(_Effect, fx_uv)
                      + tex2D(_Effect, fx_uv2))
                      * tex2D(_Alpha, i.uv);
            }
            ENDCG
        }
    }
}