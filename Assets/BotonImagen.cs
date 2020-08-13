using UnityEngine;
using UnityEngine.UI;

public class BotonImagen : MonoBehaviour
{
    [SerializeField] private Text nombreImagenText = null;
    [SerializeField] private Image categoriaImagen = null;
    //[SerializeField] private Button botonVerImagen = null;

    [SerializeField] private Sprite attackIcon = null;
    [SerializeField] private Sprite defenseIcon = null;

    [SerializeField] private InputField inputfield = null;
    [SerializeField] private PanelPrincipalBiblioteca panelPrincipalBiblioteca = null;

    private Sprite sprite = null;

    private ImagenBiblioteca _jugadaFocus;

    private void Start()
    {
        inputfield.onEndEdit.AddListener(VerificarEdicionNombreJugada);
    }

    private void VerificarEdicionNombreJugada(string _nuevoNombre)
    {
        if (_nuevoNombre != nombreImagenText.text)
        {
            CarpetaJugada _carpeta = _jugadaFocus.GetCarpetaActual();

            if (_carpeta.ExistsJugada(_nuevoNombre.ToUpper()))
            {
                Debug.Log("NOMBRE EXISTENTE: " + _nuevoNombre);
                panelPrincipalBiblioteca.ActivarMensajeError();
                return;
            }
            else
            {
                Debug.Log("NOMBRE CAMBIADO");
                panelPrincipalBiblioteca.ActivarMensajeCambioNombreExitoso();
                SaveSystem.EditarJugada(nombreImagenText.text, _nuevoNombre.ToUpper(), _jugadaFocus.GetCarpetaActual());
                //panelPrincipalBiblioteca.ResetPrefabs();
                nombreImagenText.text = _nuevoNombre.ToUpper();
            }
        }
    }

    public void BorrarJugadaFocus()
    {
        CarpetaJugada _carpeta = _jugadaFocus.GetCarpetaActual();
        _carpeta.BorrarJugada(_jugadaFocus);
    }

    public void SetJugadaFocus(ImagenBiblioteca _jugada)
    {
        _jugadaFocus = _jugada;
        SetNombreBoton(_jugada.GetNombre());
        SetImagenPreview(_jugada.GetTexture());
        SetCategoria(_jugada.GetCategoria());
    }

    private void SetNombreBoton(string nombre_)
    {
        nombreImagenText.text = nombre_;
    }

    private void SetCategoria(string categoria_)
    {
        if (categoria_ == "ataque")
            categoriaImagen.sprite = attackIcon;
        else if (categoria_ == "defensa")
            categoriaImagen.sprite = defenseIcon;
        else
            categoriaImagen.gameObject.SetActive(false);
    }

    private void SetImagenPreview(Texture2D textura_)
    {
        if (sprite == null)
        {
            if(AppController.instance.resHeight > AppController.instance.resWidth)
                sprite = Sprite.Create(textura_, new Rect(0, 0, AppController.instance.resHeight, AppController.instance.resWidth), new Vector2(0f, 0f));
            else
                sprite = Sprite.Create(textura_, new Rect(0, 0, AppController.instance.resWidth, AppController.instance.resHeight), new Vector2(0f, 0f));
        }

        //botonVerImagen.image.sprite = sprite;
    }

    public Sprite GetSprite()
    {
        if(sprite == null)
        {
            Debug.Log("Sprite nula");
            return null;
        }

        return sprite;
    }

    public string GetNombre()
    {
        return nombreImagenText.text;
    }
}
