using UnityEngine;

public class ConvertTexture2D
{
    public static Texture2D GetTexture2D(string path)
    {
        try
        {
            var rawData = System.IO.File.ReadAllBytes(path);
            Texture2D texture = new Texture2D(1, 1);
            texture.LoadImage(rawData);
            return texture;
        }
        catch (System.Exception e)
        {
            Debug.Log(e);
            return null;
        }
    }

    public static Sprite GetSprite(Texture2D texture)
    {
        try
        {
            Sprite sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), Vector2.one);
            return sprite;
        }
        catch (System.Exception e)
        {
            Debug.Log(e);
            return null;
        }
    }
}
