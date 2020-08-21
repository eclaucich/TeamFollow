using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PanelInfoJugador : Panel
{
    [SerializeField] private Transform parentTransform = null;
    [SerializeField] private GameObject prefabInputInfo = null;
    [SerializeField] private GameObject prefabInputInfoEspecial = null;
    [SerializeField] private GameObject prefabInputFecha = null;

    [SerializeField] private ScrollRect scrollRect = null;
    [SerializeField] private FlechasScroll flechasScroll = null;

    [SerializeField] private GameObject botonGuardarCambios = null;
    [SerializeField] private GameObject textEditando = null;

    [SerializeField] private MensajeError mensajeError = null;

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

    void Awake()
    {
        //listaPrefabs = new List<InputPrefab>();
        inputsString = new List<InputPrefab>();
        inputsInt = new List<InputPrefab>();
        inputsEspecial = new List<InputPrefab>();
        inputsObligatorios = new List<InputPrefab>();
    }

    private void Start()
    { 
        panelJugadores = GameObject.Find("PanelJugadores").GetComponent<PanelJugadores>();

        prefabHeight = prefabInputInfo.GetComponent<RectTransform>().rect.height;
    }

    private void FixedUpdate()
    {
        flechasScroll.Actualizar(scrollRect, cantMinima, inputsString.Count+inputsEspecial.Count+inputsObligatorios.Count+1);//el +1 es el de inputfecha
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

        BorrarPrefabs();
        CrearPrefabs();

        if (prefabHeight == 0) prefabHeight = prefabInputInfo.GetComponent<RectTransform>().rect.height;
        cantMinima = (int)(scrollRect.GetComponent<RectTransform>().rect.height / (prefabHeight + parentTransform.GetComponent<VerticalLayoutGroup>().spacing));
    }

    private void CrearPrefabs()
    {
        //if(listaPrefabs == null) return;

        foreach (var info in infoJugador.GetInfoObligatoria())
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

        InputPrefabFecha IPGO = Instantiate(prefabInputFecha, parentTransform).GetComponent<InputPrefabFecha>();
        IPGO.gameObject.SetActive(true);
        IPGO.SetNombreCategoria("Fecha Nacimiento");
        IPGO.SetText("Fecha Nacimiento".ToUpper(), AppController.Idiomas.Español);
        IPGO.SetText("Date of Birth".ToUpper(), AppController.Idiomas.Ingles);
        IPGO.SetValorCategoria(infoJugador.GetFechaNac().ToShortDateString());
        IPGO.HabilitarInput(false);
        //listaPrefabs.Add(IPGO);
        inputFecha = IPGO;
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

        jugadorFocus.Editar(ij);

        HabilitarEdicion(false);
    }
}
