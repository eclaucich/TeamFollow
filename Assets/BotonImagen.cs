using UnityEngine;
using UnityEngine.UI;

public class BotonImagen : MonoBehaviour
{
    [SerializeField] private Text nombreImagenText = null;
    //[SerializeField] private Button botonVerImagen = null;

    private Sprite sprite = null;

    public void SetNombreBoton(string nombre_)
    {
        nombreImagenText.text = nombre_;
    }

    public void SetImagenPreview(Texture2D textura_)
    {
        if (sprite == null)
        {
            if(AppController.instance.resHeight > AppController.instance.resWidth)
                sprite = Sprite.Create(textura_, new Rect(0, 0, AppController.instance.resHeight - 70, AppController.instance.resWidth), new Vector2(0f, 0f));
            else
                sprite = Sprite.Create(textura_, new Rect(0, 0, AppController.instance.resWidth - 70, AppController.instance.resHeight), new Vector2(0f, 0f));
        }

        //botonVerImagen.image.sprite = sprite;
    }

    public Sprite GetSprite()
    {
        if(sprite == null)
        {
            Debug.Log("Tratando de regresar Sprite nula!");
            return null;
        }

        return sprite;
    }

    public string GetNombre()
    {
        return nombreImagenText.text;
    }
}
