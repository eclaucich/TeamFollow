using UnityEngine;
using UnityEngine.UI;

public class BotonImagen : MonoBehaviour
{
    [SerializeField] private Text nombreImagenText = null;
    [SerializeField] private Button botonVerImagen = null;

    private Sprite sprite = null;

    public void SetNombreBoton(string nombre_)
    {
        nombreImagenText.text = nombre_;
    }

    public void SetImagenPreview(Texture2D textura_)
    {
        if(sprite == null)
            sprite = Sprite.Create(textura_, new Rect(0, 0, 1210, 720), new Vector2(0f, 0f));

        botonVerImagen.image.sprite = sprite;
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
}
