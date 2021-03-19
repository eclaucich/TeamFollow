using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PanelInfoJugador : Panel
{
    [SerializeField] private Transform parentTransform = null;
    [SerializeField] private GameObject prefabInputInfo = null;
    [SerializeField] private GameObject prefabInputInfoEspecial = null;
    [SerializeField] private GameObject prefabInputFecha = null;
    
    [SerializeField] private Text nombreJugadorText = null;
    [SerializeField] private Image imagenJugador = null;

    [SerializeField] private ScrollRect scrollRect = null;
    [SerializeField] private FlechasScroll flechasScroll = null;

    [SerializeField] private GameObject botonGuardarCambios = null;
    [SerializeField] private GameObject textEditando = null;

    [SerializeField] private MensajeError mensajeError = null;

    [SerializeField] private Sprite defaultSpriteJugador = null;

    private InfoJugador infoJugador;
    private Jugador jugadorFocus;

    //private List<InputPrefab> listaPrefabs = null;
    private List<InputPrefab> inputsString;
    private List<InputPrefab> inputsInt;
    private List<InputPrefab> inputsEspecial;
    private List<InputPrefab> inputsObligatorios;
    private InputPrefabFecha inputFecha;

    private string nombreActual;
    private string numCamisetaActual;

    private PanelJugadores panelJugadores;

    private int cantMinima;
    private float prefabHeight;

    private bool editando = false;

    private List<Color> coloresBotones;

    private string pathImagenJugador = null;

    void Awake()
    {
        //listaPrefabs = new List<InputPrefab>();
        inputsString = new List<InputPrefab>();
        inputsInt = new List<InputPrefab>();
        inputsEspecial = new List<InputPrefab>();
        inputsObligatorios = new List<InputPrefab>();
        coloresBotones = new List<Color>();
    }

    private void Start()
    { 
        panelJugadores = GameObject.Find("PanelJugadores").GetComponent<PanelJugadores>();

        prefabHeight = prefabInputInfo.GetComponent<RectTransform>().rect.height;
    }

    private void Update() 
    {
        if(editando && Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleEditar();
        }    
    }

    private void FixedUpdate()
    {
        int cantPrefabs = inputsString.Count + inputsEspecial.Count + inputsObligatorios.Count + inputsInt.Count + 1;
        flechasScroll.Actualizar(scrollRect, cantMinima, cantPrefabs);//el +1 es el de inputfecha
    }

    public void MostrarPanelPartidos()
    {
        CanvasController.instance.AgregarPanelAnterior(CanvasController.Paneles.JugadoresInfo);
        panelJugadores.MostrarPanelPartidos();
    }

    public void SetearPanelInfoJugador(Jugador jugador)
    {
        CanvasController.instance.overlayPanel.SetNombrePanel("JUGADOR: " + jugador.GetNombre(), AppController.Idiomas.Español);
        CanvasController.instance.overlayPanel.SetNombrePanel("PLAYER: " + jugador.GetNombre(), AppController.Idiomas.Ingles);

        textEditando.SetActive(false);
        botonGuardarCambios.SetActive(false);

        infoJugador = jugador.GetInfoJugador();
        jugadorFocus = jugador;

        coloresBotones.Clear();
        coloresBotones.Add(AppController.instance.colorTheme.detalle5);
        coloresBotones.Add(AppController.instance.colorTheme.detalle3);

        BorrarPrefabs();
        CrearPrefabs();

        if (prefabHeight == 0) prefabHeight = prefabInputInfo.GetComponent<RectTransform>().rect.height;
        cantMinima = (int)(scrollRect.GetComponent<RectTransform>().rect.height / (prefabHeight + parentTransform.GetComponent<VerticalLayoutGroup>().spacing + parentTransform.GetComponent<VerticalLayoutGroup>().padding.top));
        Debug.Log("CANT MINIMA: " + cantMinima);


        imagenJugador.GetComponentInParent<Button>().enabled = false;
        imagenJugador.sprite = defaultSpriteJugador;
        if (infoJugador.pathImagenJugador != null)
        {
            Texture2D texture = NativeGallery.LoadImageAtPath(infoJugador.pathImagenJugador, 1000, markTextureNonReadable: false);
            if (texture == null)
            {
                Debug.Log("Couldn't load texture from " + infoJugador.pathImagenJugador);
                return;
            }
            Sprite sprite = Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), new Vector2(.5f, .5f), 100f);
            if (sprite == null)
            {
                Debug.Log("Error creating sprite");
                return;
            }
            imagenJugador.sprite = sprite;
        }
        else
        {
            Debug.Log("PATH NULO");
        }

        Debug.Log("PATH INFO: " + infoJugador.pathImagenJugador);
    }

    private void CrearPrefabs()
    {
        //if(listaPrefabs == null) return;

        //int idxColor = 0;

        foreach (var info in infoJugador.GetInfoObligatoria())
        {  
            if(info.Key.ToUpper() == "NOMBRE")
            {
                nombreJugadorText.text = info.Value.ToString();
            }
            else
            {
                InputPrefab IPgo = Instantiate(prefabInputInfo, parentTransform).GetComponent<InputPrefab>();
                IPgo.gameObject.SetActive(true);
                IPgo.SetNombreCategoria(info.Key.ToString());
                IPgo.SetText(info.Key.ToString().ToUpper(), AppController.Idiomas.Español);
                IPgo.SetText(infoJugador.GetKeyInLaguage(info.Key.ToString(), AppController.Idiomas.Ingles), AppController.Idiomas.Ingles);
                IPgo.SetPlaceholder(info.Value.ToString());
                IPgo.HabilitarInput(false);
                inputsObligatorios.Add(IPgo);
                IPgo.SetKeyboardType(TouchScreenKeyboardType.Default);
                nombreActual = info.Value.ToString();
            }
            //IPgo.SetColor(coloresBotones[idxColor % coloresBotones.Count]);
            //idxColor++;
        }

        InputPrefabFecha IPGO = Instantiate(prefabInputFecha, parentTransform).GetComponent<InputPrefabFecha>();
        IPGO.gameObject.SetActive(true);
        IPGO.SetNombreCategoria("Fecha Nacimiento");
        IPGO.SetText("Fecha Nacimiento".ToUpper(), AppController.Idiomas.Español);
        IPGO.SetText("Date of Birth".ToUpper(), AppController.Idiomas.Ingles);
        IPGO.SetValorCategoria(infoJugador.GetFechaNac().ToShortDateString());
        IPGO.HabilitarInput(false);
        //listaPrefabs.Add(IPGO);
        inputFecha = IPGO;

        //IPGO.SetColor(coloresBotones[idxColor % coloresBotones.Count]);
        //idxColor++;
        //GO.transform.GetChild(0).GetComponent<Text>().text = "Fecha Nacimiento";
        //listaPrefabs.Add(GO);

        foreach (var info in infoJugador.GetInfoString())
        {
            InputPrefab IPgo = Instantiate(prefabInputInfo, parentTransform).GetComponent<InputPrefab>();
            IPgo.gameObject.SetActive(true);
            IPgo.SetNombreCategoria(info.Key.ToString());
            IPgo.SetText(info.Key.ToString().ToUpper(), AppController.Idiomas.Español);
            IPgo.SetText(infoJugador.GetKeyInLaguage(info.Key.ToString(), AppController.Idiomas.Ingles), AppController.Idiomas.Ingles);
            IPgo.SetPlaceholder(info.Value.ToString());
            IPgo.HabilitarInput(false);
            IPgo.SetKeyboardType(TouchScreenKeyboardType.Default);
            inputsString.Add(IPgo);

            //IPgo.SetColor(coloresBotones[idxColor % coloresBotones.Count]);
            //idxColor++;
        }

        foreach (var info in infoJugador.GetInfoInt())
        {
            InputPrefab IPgo = Instantiate(prefabInputInfo, parentTransform).GetComponent<InputPrefab>();
            IPgo.gameObject.SetActive(true);
            IPgo.SetNombreCategoria(info.Key.ToUpper());
            IPgo.SetText(info.Key.ToUpper(), AppController.Idiomas.Español);
            IPgo.SetText(infoJugador.GetKeyInLaguage(info.Key.ToString(), AppController.Idiomas.Ingles), AppController.Idiomas.Ingles);
            IPgo.SetPlaceholder(info.Value.ToString());
            IPgo.HabilitarInput(false);
            IPgo.SetKeyboardType(TouchScreenKeyboardType.NumberPad);
            inputsInt.Add(IPgo);

            if (IPgo.GetNombreCategoria() == "NUMERO CAMISETA")
                numCamisetaActual = info.Value.ToString();

            //IPgo.SetColor(coloresBotones[idxColor % coloresBotones.Count]);
            //idxColor++;
        }

        foreach (var info in infoJugador.GetInfoEspecial())
        {
            InputPrefabEspecial IPgo = Instantiate(prefabInputInfoEspecial, parentTransform).GetComponent<InputPrefabEspecial>();
            IPgo.gameObject.SetActive(true);
            IPgo.SetNombreCategoria(info.Key.ToString());
            IPgo.SetText(info.Key.ToString().ToUpper(), AppController.Idiomas.Español);
            IPgo.SetText(infoJugador.GetKeyInLaguage(info.Key.ToString(), AppController.Idiomas.Ingles), AppController.Idiomas.Ingles);
            IPgo.SetValor(infoJugador.GetSpecialValueInLanguage(info.Value.ToString(), AppController.Idiomas.Español), AppController.Idiomas.Español);
            IPgo.SetValor(infoJugador.GetSpecialValueInLanguage(info.Value.ToString(), AppController.Idiomas.Ingles), AppController.Idiomas.Ingles);
            IPgo.HabilitarInput(false);
            inputsEspecial.Add(IPgo);

            //IPgo.SetColor(coloresBotones[idxColor % coloresBotones.Count]);
            //idxColor++;
        }
    }

    private void BorrarPrefabs()
    {
        foreach (var input in inputsObligatorios)
            Destroy(input.gameObject);
        foreach (var input in inputsString)
            Destroy(input.gameObject);
        foreach (var input in inputsInt)
            Destroy(input.gameObject);
        foreach (var input in inputsEspecial)
            Destroy(input.gameObject);
        if(inputFecha != null) 
            Destroy(inputFecha.gameObject);

        inputsObligatorios.Clear();
        inputsString.Clear();
        inputsInt.Clear();
        inputsEspecial.Clear();
    }

    public void ToggleEditar()
    {
        editando = !editando;
        HabilitarEdicion(editando);
    }

    public void HabilitarEdicion(bool _aux)
    {
        CanvasController.instance.retrocesoPausado = _aux;

        foreach (var input in inputsObligatorios)
            input.HabilitarInput(_aux);
        foreach (var input in inputsString)
            input.HabilitarInput(_aux);
        foreach (var input in inputsInt)
            input.HabilitarInput(_aux);
        foreach (var input in inputsEspecial)
            input.HabilitarInput(_aux);
        inputFecha.HabilitarInput(_aux);

        textEditando.SetActive(_aux);
        botonGuardarCambios.SetActive(_aux);

        imagenJugador.GetComponentInParent<Button>().enabled = _aux;
    }

    public void ConfirmarEdicion()
    {
        InfoJugador ij = new InfoJugador();

        foreach (var input in inputsObligatorios)
        {
            if (input.GetValorCategoria() == "")
            {
                mensajeError.SetText("Completar campos obligatorios (*)".ToUpper(), AppController.Idiomas.Español);
                mensajeError.SetText("Complete required fields (*)".ToUpper(), AppController.Idiomas.Ingles);
                mensajeError.Activar();
                return;
            }
            ij.SetInfoObligatoria(input);
        }

        //Reviasr si existe el nombre (hacer una función de comporbación de nombres general en appcontroller
        if (nombreActual!=ij.GetNombre() && AppController.instance.equipoActual.BuscarPorNombre(ij.GetNombre()) != null || ij.GetNombre() == "" || ij.GetNombre() == " ")
        {
            mensajeError.SetText("Nombre inválido/existente!".ToUpper(), AppController.Idiomas.Español);
            mensajeError.SetText("Invalid/Existing name!".ToUpper(), AppController.Idiomas.Ingles);
            mensajeError.Activar();
            return;
        }


        foreach (var input in inputsInt)
            ij.SetInfoInt(input);

        int numCamisetaActualInt = -1;
        int.TryParse(numCamisetaActual, out numCamisetaActualInt);

        Debug.Log("NUM ACTUAL: " + numCamisetaActualInt);

        int numCamiseta = -1;
        if (int.TryParse(ij.GetNumeroCamiseta(), out numCamiseta))
        {
            Debug.Log("NUEVO NUM: " + numCamiseta);

            if (numCamiseta != numCamisetaActualInt && (numCamiseta < 0 || !AppController.instance.equipoActual.VerficarNumeroCamiseta(ij.GetNumeroCamiseta())))
            {
                mensajeError.SetText("NUMERO DE CAMISETA EN USO", AppController.Idiomas.Español);
                mensajeError.SetText("SHIR NUMBER IN USE", AppController.Idiomas.Ingles);
                mensajeError.Activar();
                return;
            }
        }

        foreach (var input in inputsString)
            ij.SetInfoString(input);

        foreach (var input in inputsEspecial)
            ij.SetInfoEspecial(input);

        ij.SetFechaNac(inputFecha.GetFechaPlaceholder());

        if(pathImagenJugador!=null)
            ij.pathImagenJugador = pathImagenJugador;
        else
            ij.pathImagenJugador = null;

        jugadorFocus.Editar(ij);

        HabilitarEdicion(false);
    }

    public void PickImage(int maxSize = 10000)
    {
        NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((path) => 
        {
            Debug.Log("Image path: " + path);
            if (path != null)
            {
                // Create Texture from selected image
                Texture2D texture = NativeGallery.LoadImageAtPath(path, maxSize);
                if (texture == null)
                {
                    Debug.Log("Couldn't load texture from " + path);
                    return;
                }

                Sprite sprite = Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), new Vector2(.5f,.5f), 100f);
                if(sprite == null)
                    Debug.Log("Error creating sprite");
                imagenJugador.sprite = sprite;

                pathImagenJugador = path;
            }
        }, "Seleccionar imagen", "image/*" );

        Debug.Log( "Permission result: " + permission );
    }
}
