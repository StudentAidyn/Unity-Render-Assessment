Shader "Post Processing/Sh_OutlineShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                //float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                //float3 normal : TEXCOORD1;
            };

            //sampler2D _CameraDepthTexture;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                UNITY_TRANSFER_DEPTH(o.depth);
                //o.uv = v.uv;
                return o;
            }
            //check Normal directions and then Depth pass directions



            //sampler2D _MainTex;

            // fixed4 frag (v2f i) : SV_Target
            // {
            //     fixed4 col = tex2D(_MainTex, i.uv);
            //     float depth = SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, 0);

            //     // just invert the colors
            //     //col.rgb = 1 - col.rgb;
            //     return fixed4(depth, 0, 0, 1);
            // }

            half4 frag(v2f i) : SV_Target {
                UNITY_OUTPUT_DEPTH(i.depth);
            }
            // https://docs.unity3d.com/2020.1/Documentation/Manual/SL-CameraDepthTexture.html
            ENDCG
        }
    }
}
