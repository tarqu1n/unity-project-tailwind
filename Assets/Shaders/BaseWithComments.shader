Shader "Unlit/Hologram"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert // fnc definition
            #pragma fragment frag // fnc definition
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc" // unity helper funcs for rendering

            struct appdata // object that contains
            {
                // float4 is packed array (contains 4 floating point numbers (x, y, z, w)).
				float4 vertex : POSITION; //  We're telling the program this is a position
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION; // were telling the program this is a screen space position
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;


            v2f vert (appdata v)
            {
                v2f o; // vert to frag struct called o. this is what we're going to return
                o.vertex = UnityObjectToClipPos(v.vertex); // vertex in local space goes from localspace -> world space -> view space -> clip space -> screen space
                o.uv = TRANSFORM_TEX(v.uv, _MainTex); // take date from the uv and the main texture
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o; // pass o to frag
            }

            fixed4 frag (v2f i) : SV_Target // takes v2f struct from vert. SV_target is frame buffer for screen and is the output target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv); // colour can be either coords x,y,z,w or r,g,b,a
				// text2D reads colour from the main texture at the position on the ui based on the v2f structed i
                
				// apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
