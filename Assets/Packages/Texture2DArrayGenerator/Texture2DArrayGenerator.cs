using System.Collections.Generic;
using UnityEngine;

public static class Texture2DArrayGenerator
{
    public static Texture2DArray Generate(IList<Texture2D> textures, TextureFormat format, bool mipChain)
    {
        // NOTE:
        // There is no-way to check the texture is compressed or not.
        // (TextureFormat is useful to check it, but its hard work.)

        var texture2DArray = new Texture2DArray(textures[0].width,
                                                textures[0].height,
                                                textures.Count,
                                                format,
                                                mipChain);

        for (var i = 0; i < textures.Count; i++)
        {
            // NOTE:
            // "Graphics.CopyTexture"
            // - It is able to ignore Texture.Read/Write settings.
            // - It is able to generate compressed Texture2DArray when the textures have same compressed setting.
            //    - Such Texture2DArray is unable to have MipMap.
            // 
            // "Set/GetPixels"
            // - It is able to ignore format, compress and any other settings.
            // - Textures are must be set "Read/Write" true.
            // - It is unable to generate compressed Texture2DArray.
            // 
            // In general, the size of Texture2DArray with compressed textures is smaller
            // than Texture2DArray with mipmap.

            Graphics.CopyTexture(textures[i], 0, 0, texture2DArray, i, 0);
            // texture2DArray.SetPixels(textures[i].GetPixels(), i);
        }

        texture2DArray.Apply();

        return texture2DArray;
    }
}