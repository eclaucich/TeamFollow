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

        texture = new Texture2D(1210, 720, TextureFormat.RGB24, false);
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
