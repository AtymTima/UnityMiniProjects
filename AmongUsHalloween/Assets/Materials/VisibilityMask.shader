﻿Shader "Custom/VisibilityMask"
{
    Properties{}

    SubShader{

        Tags {
            "RenderType" = "Opaque"
        }

        Pass {
            ZWrite Off
        }
    }
}
