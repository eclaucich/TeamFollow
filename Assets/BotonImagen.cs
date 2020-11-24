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
    private Image imagen = null;

    private ImagenBiblioteca _jugadaFocus;

    private void Start()
    {
        imagen = GetComponent<Image>();
        inputfield.onEndEdit.AddListener(VerificarEdicionNombreJugada);
        imagen.color = new Color(1f, 1f, 1f, 0f); //transparente
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

    public bool VerificarNombreJugadasCarpeta(CarpetaJugada _carpeta)
    {
        return _carpeta.BuscarJugada(_jugadaFocus.GetNombre()) == null;
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

    public void SetNewName(string _newName)
    {
        nombreImagenText.text = _newName;
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

    public void SetCarpeta(CarpetaJugada _carpeta)
    {
        _jugadaFocus.SetCarpetaActual(_carpeta);
    }

    public CarpetaJugada GetCarpeta()
    {
        return _jugadaFocus.GetCarpetaActual();
    }

    public void ActivarSeleccion()
    {
        imagen.color = AppController.instance.colorTheme.botonSeleccionado;
    }

    public void DesactivarSeleccion()
    {
        imagen.color = new Color(1f, 1f, 1f, 0f); //transparente
    }
}
