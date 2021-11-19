using UnityEngine;

public class Sample : MonoBehaviour
{
    public Texture2D[] textures;

    private void Start()
    {
        Texture2DArrayGenerator.Generate(textures, TextureFormat.RGB24, true);
    }
}