// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Agora VR/Gradient_3Color" {
// Properties {
//      [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
//      _Color ("Left Color", Color) = (1,1,1,1)
//      _Color2 ("Right Color", Color) = (1,1,1,1)
// }
// SubShader {
//      Tags {"Queue"="Transparent"  "IgnoreProjector"="True"}
//      LOD 100
//      ZWrite Off
//      Pass {
//           Blend SrcAlpha OneMinusSrcAlpha
//          CGPROGRAM
//          #pragma vertex vert
//          #pragma fragment frag
//          #include "UnityCG.cginc"
//          fixed4 _Color;
//          fixed4 _Color2;
//          struct v2f {
//              float4 pos : SV_POSITION;
//              fixed4 col : COLOR;
//          };
//          v2f vert (appdata_full v)
//          {
//              v2f o;
//              o.pos = UnityObjectToClipPos (v.vertex);
//              o.col = lerp(_Color,_Color2, v.texcoord.x );
//              return o;
//          }

//          float4 frag (v2f i) : COLOR {
//              float4 c = i.col;
//              return c;
//          }
//              ENDCG
//          }
//      }

     Properties {
         [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
         _ColorTop ("Top Color", Color) = (1,1,1,1)
         _ColorMid ("Mid Color", Color) = (1,1,1,1)
         _ColorBot ("Bot Color", Color) = (1,1,1,1)
         _Middle ("Middle", Range(0.001, 0.999)) = 1
     }

     SubShader {
         Tags {"Queue"="Transparent"  "IgnoreProjector"="True"}
         LOD 100

         ZWrite Off

         Pass {
          Blend SrcAlpha OneMinusSrcAlpha
         CGPROGRAM
         #pragma vertex vert
         #pragma fragment frag
         #include "UnityCG.cginc"

         fixed4 _ColorTop;
         fixed4 _ColorMid;
         fixed4 _ColorBot;
         float  _Middle;

         struct v2f {
             float4 pos : SV_POSITION;
             float4 texcoord : TEXCOORD0;
         };

         v2f vert (appdata_full v) {
             v2f o;
             o.pos = UnityObjectToClipPos (v.vertex);
             o.texcoord = v.texcoord;
             return o;
         }

         fixed4 frag (v2f i) : COLOR {
             fixed4 c = lerp(_ColorBot, _ColorMid, i.texcoord.x / _Middle) * step(i.texcoord.x, _Middle);
              c += lerp(_ColorMid, _ColorTop, (i.texcoord.x - _Middle) / (1 - _Middle)) * (1 - step(i.texcoord.x, _Middle));
             return c;
         }
         ENDCG
         }
     }
}