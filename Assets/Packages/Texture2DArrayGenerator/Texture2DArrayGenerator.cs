using System.Collections.Generic;
using UnityEngine;

public static class Texture2DArrayGenerator
{
    public static Texture2DArray Generate(IList<Texture2D> textures)
    {
        // NOTE:
        // Maybe Texture2DArray doesn't support mipChain in Unity.2018.2.8f1.

        Texture2DArray texture2DArray = new Texture2DArray(textures[0].width,
                                                           textures[0].height,
                                                           textures.Count,
                                                           textures[0].format,
                                                           false);
        for (int i = 0; i < textures.Count; i++)
        {
            Graphics.CopyTexture(textures[i], 0, 0, texture2DArray, i, 0);
        }

        texture2DArray.Apply();

        return texture2DArray;
    }
}