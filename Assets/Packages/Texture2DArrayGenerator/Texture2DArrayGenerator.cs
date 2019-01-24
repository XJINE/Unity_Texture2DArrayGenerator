using System.Collections.Generic;
using UnityEngine;

public static class Texture2DArrayGenerator
{
    public static Texture2DArray Generate(IList<Texture2D> textures, TextureFormat format)
    {
        // NOTE:
        // Maybe Texture2DArray doesn't support mipChain in Unity.2018.2.8f1.

        // NOTE:
        // format needs to be ARGB32, RGBA32, RGB24, R8, Alpha8 or one of float formats.

        Texture2DArray texture2DArray = new Texture2DArray(textures[0].width,
                                                           textures[0].height,
                                                           textures.Count,
                                                           format,
                                                           false);
        for (int i = 0; i < textures.Count; i++)
        {
            // NOTE:
            // It is able to make a Texture2DArray with "Graphics.CopyTexture()".
            // However, it has a problem which is able to make Texture2DArray in Editor
            // without enabling read-write settings of texture.
            // And then, it causes some wrong result in build app.
            // So we should make a Texture2DArray with "SetPixels()".
            // 
            // Graphics.CopyTexture(textures[i], 0, 0, texture2DArray, i, 0);

            texture2DArray.SetPixels(textures[i].GetPixels(), i);
        }

        texture2DArray.Apply();

        return texture2DArray;
    }
}