using UnityEngine;

public class ImagenBiblioteca
{
    private string nombre;
    private byte[] bytes;
    private string categoria;

    private Texture2D texture;

    private CarpetaJugada carpetaActual;

    public ImagenBiblioteca(byte[] bytes_, string nombre_, string categoria_, CarpetaJugada _carpeta)
    {
        nombre = nombre_;
        bytes = bytes_;
        categoria = categoria_;

        carpetaActual = _carpeta;

        //Debug.Log("W: " + AppController.instance.resWidth);
        //Debug.Log("H: " + AppController.instance.resHeight);

        texture = new Texture2D(AppController.instance.resWidth, AppController.instance.resHeight, TextureFormat.RGB24, false);
        texture.LoadImage(bytes);
    }

    public void SetNombre(string _nombre)
    {
        nombre = _nombre;
    }
    public string GetNombre()
    {
        return nombre;
    }

    public Texture2D GetTexture()
    {
        return texture;
    }

    public string GetCategoria()
    {
        return categoria;
    }

    public void SetCarpetaActual(CarpetaJugada _carpeta)
    {
        carpetaActual = _carpeta;
    }
    public CarpetaJugada GetCarpetaActual()
    {
        return carpetaActual;
    }
}
