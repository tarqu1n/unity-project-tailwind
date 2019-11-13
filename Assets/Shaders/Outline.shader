Shader "Custom/TransparentTintOutline"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_TintColour("Tint Colour", Color) = (1,1,1,1)
		_Opacity("Opacity", Range(0, 1)) = 1
				
		_OutlineColor("Outline Colour", Color) = (0,0,0,1)
		_OutlineWidth("Outline With", Range(0, 1)) = 0.03
    }

    SubShader
    {
        Tags { "Queue"="Geometry" } // rendering order queue tag (as its now transparent order matters) 
        LOD 100

		Blend SrcAlpha OneMinusSrcAlpha // how do we want to blend the pixels at the end

		Pass
        {
            CGPROGRAM
            #pragma vertex vert // fnc definition
            #pragma fragment frag // fnc definition

            #include "UnityCG.cginc" // unity helper funcs for rendering

            struct appdata // object that contains
            {
                // float4 is packed array (contains 4 floating point numbers (x, y, z, w)).
				float4 vertex : POSITION; //  We're telling the program this is a position
				float3 normal: NORMAL;
            };

            struct v2f
            {
                float4 pos : POSITION;
				float4 color : COLOR;
				float3 normal : NORMAL;
            };
		
			float _OutlineWidth;
			float4 _OutlineColor;
			float _Opacity;

            v2f vert (appdata v)
            {
                v.vertex.xyz *= 1 + _OutlineWidth; // extrude normals by outline width
				
				v2f o; // vert to frag struct called o. this is what we're going to return

				o.pos = UnityObjectToClipPos(v.vertex);
				o.color = _OutlineColor;

                return o; // pass o to frag
            }

            half4 frag (v2f i) : COLOR // takes v2f struct from vert. SV_target is frame buffer for screen and is the output target
            {
                i.color.a = _Opacity;
				return i.color;
            }
            ENDCG
        }
    }
}
