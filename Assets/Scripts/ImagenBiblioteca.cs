using UnityEngine;

public class ImagenBiblioteca
{
    private string nombre;
    private byte[] bytes;

    private Texture2D texture;

    public ImagenBiblioteca(byte[] bytes_, string nombre_)
    {
        nombre = nombre_;
        bytes = bytes_;
        Debug.Log("W: " + AppController.instance.resWidth);
        Debug.Log("H: " + AppController.instance.resHeight);

        texture = new Texture2D(AppController.instance.resWidth, AppController.instance.resHeight, TextureFormat.RGB24, false);
        texture.LoadImage(bytes);
    }

    public string GetNombre()
    {
        return nombre;
    }

    public Texture2D GetTexture()
    {
        return texture;
    }
}
