using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PanelNuevoJugador : Panel
{
    //[SerializeField] private Text nombreJugadorText = null;
    //[SerializeField] private InputField inputPeso = null;
    //[SerializeField] private InputField inputAltura = null;
    [SerializeField] private Transform parentTransform = null;
    [SerializeField] private GameObject prefabInputInfo = null;
    [SerializeField] private GameObject prefabInputInfoEspecial = null;
    [SerializeField] private GameObject prefabInputFecha = null;
    [SerializeField] private Image imagenJugador = null;
    
    [SerializeField] private MensajeError mensajeError = null;

    [SerializeField] private ScrollRect scrollRect = null;
    [SerializeField] private FlechasScroll flechasScroll = null;

    [SerializeField] private Sprite defaultSpriteJugador = null;

    //private InfoJugador infoJugador;

    //private List<GameObject> listaPrefabs;
    private List<InputPrefab> inputsString;
    private List<InputPrefab> inputsInt;
    private List<InputPrefab> inputsEspecial;
    private List<InputPrefab> inputsObligatorios;
    private InputPrefabFecha inputFecha;
    private InfoJugador infoJugador;

    private int cantMinima;
    private float prefabHeight;

    private List<Color> coloresBotones;

    private string pathImagenJugador = null;

    private void Start()
    {
        coloresBotones = new List<Color>();

        InfoJugador infoJugadorAux = new InfoJugador();

        prefabHeight = prefabInputInfo.GetComponent<RectTransform>().rect.height;

        coloresBotones.Clear();
        coloresBotones.Add(AppController.instance.colorTheme.detalle5);
        coloresBotones.Add(AppController.instance.colorTheme.detalle3);      

        pathImagenJugador = null;
        imagenJugador.sprite = defaultSpriteJugador;

        SetInputs();
    }


    private void SetInputs()
    {
        infoJugador = new InfoJugador();

        inputsString = new List<InputPrefab>();
        inputsInt = new List<InputPrefab>();
        inputsEspecial = new List<InputPrefab>();
        inputsObligatorios = new List<InputPrefab>();

        //int idxColor = 0;

        foreach (var info in infoJugador.GetInfoObligatoria())
        {
            GameObject go = Instantiate(prefabInputInfo, parentTransform);
            go.gameObject.SetActive(true);
            InputPrefab IPgo = go.GetComponent<InputPrefab>();
            IPgo.SetNombreCategoria(info.Key.ToUpper());
            IPgo.SetText(info.Key, AppController.Idiomas.Español);
            IPgo.SetText(infoJugador.GetKeyInLaguage(info.Key, AppController.Idiomas.Ingles), AppController.Idiomas.Ingles);
            IPgo.SetCampoObligatorio(true);
            IPgo.SetKeyboardType(TouchScreenKeyboardType.Default);
            inputsObligatorios.Add(IPgo);

            //IPgo.SetColor(coloresBotones[idxColor % coloresBotones.Count]);
            //idxColor++;
        }

        GameObject GO = Instantiate(prefabInputFecha, parentTransform);
        GO.SetActive(true);
        inputFecha = GO.GetComponent<InputPrefabFecha>();
        inputFecha.SetCampoObligatorio(true);
        inputFecha.SetNombreCategoria("Fecha Nacimiento".ToUpper());
        inputFecha.SetText("Fecha Nacimiento".ToUpper(), AppController.Idiomas.Español);
        inputFecha.SetText("Date of birth".ToUpper(), AppController.Idiomas.Ingles);
        inputFecha.ResetValor();

        //inputFecha.SetColor(coloresBotones[idxColor % coloresBotones.Count]);
        //idxColor++;

        foreach (var info in infoJugador.GetInfoString())
        {
            GameObject go = Instantiate(prefabInputInfo, parentTransform);
            go.SetActive(true);
            InputPrefab IPgo = go.GetComponent<InputPrefab>();
            IPgo.SetNombreCategoria(info.Key.ToUpper());
            IPgo.SetText(info.Key, AppController.Idiomas.Español);
            IPgo.SetText(infoJugador.GetKeyInLaguage(info.Key, AppController.Idiomas.Ingles), AppController.Idiomas.Ingles);
            IPgo.SetKeyboardType(TouchScreenKeyboardType.Default);
            inputsString.Add(IPgo);

            //IPgo.SetColor(coloresBotones[idxColor % coloresBotones.Count]);
            //idxColor++;
        }

        foreach (var info in infoJugador.GetInfoInt())
        {
            Debug.Log("INFO INT");
            GameObject go = Instantiate(prefabInputInfo, parentTransform);
            go.SetActive(true);
            InputPrefab IPgo = go.GetComponent<InputPrefab>();
            IPgo.SetNombreCategoria(info.Key.ToUpper());
            IPgo.SetText(info.Key, AppController.Idiomas.Español);
            IPgo.SetText(infoJugador.GetKeyInLaguage(info.Key, AppController.Idiomas.Ingles), AppController.Idiomas.Ingles);
            IPgo.SetKeyboardType(TouchScreenKeyboardType.NumberPad);
            inputsInt.Add(IPgo);

            //IPgo.SetColor(coloresBotones[idxColor % coloresBotones.Count]);
            //idxColor++;
        }

        foreach (var info in infoJugador.GetInfoEspecial())
        {
            GameObject go = Instantiate(prefabInputInfoEspecial, parentTransform);
            go.SetActive(true);
            InputPrefabEspecial IPgo = go.GetComponent<InputPrefabEspecial>();
            IPgo.SetNombreCategoria(info.Key.ToUpper());
            IPgo.SetText(info.Key, AppController.Idiomas.Español);
            IPgo.SetText(infoJugador.GetKeyInLaguage(info.Key, AppController.Idiomas.Ingles), AppController.Idiomas.Ingles);
            inputsEspecial.Add(IPgo);

            //IPgo.SetColor(coloresBotones[idxColor % coloresBotones.Count]);
            //idxColor++;
        }

        pathImagenJugador = null;
        imagenJugador.sprite = defaultSpriteJugador;
    }

    public void SetPanel()
    {
        CanvasController.instance.retrocesoPausado = false;
        
        CanvasController.instance.overlayPanel.SetNombrePanel("NUEVO JUGADOR", AppController.Idiomas.Español);
        CanvasController.instance.overlayPanel.SetNombrePanel("NEW PLAYER", AppController.Idiomas.Ingles);

        if (inputsObligatorios != null)
            foreach (var input in inputsObligatorios)
                input.ResetValor();
        if (inputsString != null)
            foreach (var input in inputsString)
                input.ResetValor();
        if (inputsInt != null)
            foreach (var input in inputsInt)
                input.ResetValor();
        if (inputsEspecial != null)
            foreach (var input in inputsEspecial)
                input.ResetValor();
        if (inputFecha != null)
            inputFecha.ResetValor();

        pathImagenJugador = null;
        imagenJugador.sprite = defaultSpriteJugador;

        if(prefabHeight == 0) prefabHeight = prefabInputInfo.GetComponent<RectTransform>().rect.height;
        cantMinima = (int)(scrollRect.GetComponent<RectTransform>().rect.height / (prefabHeight + parentTransform.GetComponent<VerticalLayoutGroup>().spacing + parentTransform.GetComponent<VerticalLayoutGroup>().padding.top));
    }

    private void FixedUpdate()
    {
        if (prefabHeight == 0) prefabHeight = prefabInputInfo.GetComponent<RectTransform>().rect.height;
        flechasScroll.Actualizar(scrollRect, cantMinima, inputsString.Count + inputsEspecial.Count + inputsObligatorios.Count + inputsInt.Count + 1);
    }

    public void GuardarNuevoJugador()
    {
        Equipo equipoActual = AppController.instance.equipoActual;
        
        InfoJugador ij = new InfoJugador();
        
        if (!inputFecha.IsDateValid())
        {
            mensajeError.SetText("Completar campos obligatorios (*)".ToUpper(), AppController.Idiomas.Español);
            mensajeError.SetText("Complete required fields (*)".ToUpper(), AppController.Idiomas.Ingles);
            mensajeError.Activar();
            return;
        }

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
        if (equipoActual.BuscarPorNombre(ij.GetNombre()) != null || !AppController.instance.VerificarNombre(ij.GetNombre()))
        {
            mensajeError.SetText("Nombre inválido/existente!".ToUpper(), AppController.Idiomas.Español);
            mensajeError.SetText("Invalid/Existing name!".ToUpper(), AppController.Idiomas.Ingles);
            mensajeError.Activar();
            return;
        }

        foreach (var input in inputsInt)
            ij.SetInfoInt(input);

        int numCamiseta = -1;
        if(int.TryParse(ij.GetNumeroCamiseta(), out numCamiseta))
        {
            if(numCamiseta < 0 || !equipoActual.VerficarNumeroCamiseta(ij.GetNumeroCamiseta()))
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

        ij.SetFechaNac(inputFecha.GetFecha());

        //GUARDAR IMAGEN DE JUGADOR
        if(pathImagenJugador!=null)
            ij.pathImagenJugador = pathImagenJugador;
        else
            ij.pathImagenJugador = null;

        Debug.Log("PATH: "+ pathImagenJugador);
        equipoActual.NuevoJugador(ij);

        CanvasController.instance.MostrarPanelAnterior();
    }

    public void PickImage(int maxSize = 10000)
    {
        NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((path) => 
        {
            Debug.Log("Image path: " + path);
            if (path != null)
            {
                // Create Texture from selected image
                Texture2D texture = NativeGallery.LoadImageAtPath(path, maxSize, markTextureNonReadable:false);
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
