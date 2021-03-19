using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;

public class PanelPrincipal : Panel 
{

    private PanelMisEquipos panelMisEquipos;

    [SerializeField] private GameObject prefabBotonEquipo = null;
    [SerializeField] private Text adviceText = null;
    [SerializeField] private Transform seccionEquiposTransform = null;

    [SerializeField] private ScrollRect scrollRect = null;
    [SerializeField] private FlechasScroll flechasScroll = null;

    [SerializeField] private MensajeError mensajeCambioFavorito = null;

    [SerializeField] private RectTransform barraInferiorRect = null;

    [Space]
    [Header("Seleccion Multiple")]
    [SerializeField] private ConfirmacionBorradoSeleccionMultiple confirmacionBorradoSeleccionMultiple = null;
    [SerializeField] private GameObject botonBorrarSeleccionMultiple = null;
    private bool seleccionMultipleActivada = false;

    [Space]
    [Header("Buscador")]
    [SerializeField] private Buscador buscador = null;

    private List<GameObject> listaPrefabsBoton;
    private List<BotonEquipo> botonesEquipo;

    private float prefabHeight;
    private int cantMinima;
    
    private BannerView bannerView;

    private void Awake()
    {
        panelMisEquipos = GetComponentInParent<PanelMisEquipos>();
        listaPrefabsBoton = new List<GameObject>();
        botonesEquipo = new List<BotonEquipo>();

        ActivarYDesactivarAdviceText();
        prefabHeight = prefabBotonEquipo.GetComponent<RectTransform>().rect.height;
    }

    private void Start() {
        mensajeCambioFavorito.SetText("NUEVO EQUIPO FAVORITO ELEGIDO", AppController.Idiomas.Español);
        mensajeCambioFavorito.SetText("NEW FAVOURITE TEAM SETTED", AppController.Idiomas.Ingles);

        botonBorrarSeleccionMultiple.SetActive(false);
    }

    private void FixedUpdate()
    {
        flechasScroll.Actualizar(scrollRect, cantMinima, listaPrefabsBoton.Count);
    }

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.Escape) && seleccionMultipleActivada)
            ToggleSeleccionMultiple();     

        //no sería lo mas ótimo ésto
        if(seleccionMultipleActivada)
            CanvasController.instance.retrocesoPausado = true;
    }

    public void SetearPanelPrincipal()
    {
        CanvasController.instance.overlayPanel.gameObject.SetActive(true);
        
        CanvasController.instance.retrocesoPausado = false;
        seleccionMultipleActivada = false;

        buscador.SetBuscador(false);

        /*if(AppController.instance.appStarted)
        {
            Debug.Log("APP STARTED");
            SetPublicity();
        }*/
        SetPublicity();

        //CanvasController.instance.botonDespliegueMenu.SetActive(true);
        cantMinima = (int)Mathf.Ceil(scrollRect.GetComponent<RectTransform>().rect.height / (prefabHeight + seccionEquiposTransform.GetComponent<VerticalLayoutGroup>().spacing));

        CanvasController.instance.overlayPanel.SetNombrePanel("MIS EQUIPOS", AppController.Idiomas.Español);
        CanvasController.instance.overlayPanel.SetNombrePanel("MY TEAMS", AppController.Idiomas.Ingles);

        if (gameObject.activeSelf)
        {
            BorrarPrefabs();
            CrearPrefabs();
        }

        AppController.instance.equipoActual = null;

        ActivarYDesactivarAdviceText();
    }

    public void BorrarPrefabs()
    {
        foreach (GameObject prefab in listaPrefabsBoton)
        {
            Destroy(prefab);
        }
        listaPrefabsBoton.Clear();
        botonesEquipo.Clear();
    }

    public void CrearPrefabs()                                                                //Instancia el prefab del botón
    {
        foreach (Equipo equipo in AppController.instance.equipos)
        {
            GameObject botonEquipoGO = Instantiate(prefabBotonEquipo.gameObject, seccionEquiposTransform, false);
            botonEquipoGO.SetActive(true);

            botonEquipoGO.GetComponentInChildren<Button>().GetComponentInChildren<Text>().text = equipo.GetNombre();
            botonEquipoGO.GetComponent<BotonEquipo>().SetSpriteBotonEquipo(equipo);

            if(AppController.instance.equipoFavorito != null)
            {
                if (AppController.instance.equipoFavorito == equipo.GetNombre())
                    botonEquipoGO.GetComponent<BotonEquipo>().SetComoFavorito();
            }

            listaPrefabsBoton.Add(botonEquipoGO);
            botonesEquipo.Add(botonEquipoGO.GetComponent<BotonEquipo>());
        }

        cantMinima = (int)(scrollRect.GetComponent<RectTransform>().rect.height / (prefabHeight + seccionEquiposTransform.GetComponent<VerticalLayoutGroup>().spacing));
        flechasScroll.Actualizar(scrollRect, cantMinima, listaPrefabsBoton.Count);
    }

    public void CrearNuevoEquipo()                                                             //Función que se llama al apretar el botón NUEVO EQUIPO. Se muestra el panel para crear un nuevo equipo.
    {
        panelMisEquipos.MostrarPanelNuevoEquipo();
    }

    public void ActivarYDesactivarAdviceText()
    {
        if (listaPrefabsBoton.Count == 0)
        {
            adviceText.gameObject.SetActive(true);
        }
        else
        {
            adviceText.gameObject.SetActive(false);
        }
    }

    public void BorrarBotonEquipo(GameObject botonEquipo)
    {
        Destroy(botonEquipo);
        listaPrefabsBoton.Remove(botonEquipo);

        //Debug.Log("BORRADO");
    }

    public void ResetFavouriteTeams()
    {
        foreach (var go in listaPrefabsBoton)
        {
            BotonEquipo _botonEquipo = go.GetComponent<BotonEquipo>();
            if (_botonEquipo.GetEquipoFocus().GetNombre() != AppController.instance.equipoFavorito)
                _botonEquipo.DesactivarFavorito();
        }
    }

    public void MensajeFavorito(){
        mensajeCambioFavorito.Activar();
    }

    #region Seleccion Multiple

    public void ToggleSeleccionMultiple()
    {
        seleccionMultipleActivada = !seleccionMultipleActivada;

        botonBorrarSeleccionMultiple.SetActive(seleccionMultipleActivada);
        CanvasController.instance.retrocesoPausado = seleccionMultipleActivada;

        foreach (var go in listaPrefabsBoton)
        {
            go.GetComponent<BotonEquipo>().SetSeleccionMultiple(seleccionMultipleActivada);
        }
    }

    public void ActivarBorradoSeleccionMultiple()
    {
        List<BotonEquipo> botones = new List<BotonEquipo>();

        foreach (var boton in listaPrefabsBoton)
        {
            if(boton.GetComponent<BotonEquipo>().IsSelected())
                botones.Add(boton.GetComponent<BotonEquipo>());
        }

        confirmacionBorradoSeleccionMultiple.Activar(botones);
    }

    #endregion

    #region Buscador

    public void ActualizarBusqueda(Text filterText)
    {
        string filter = filterText.text;

        int cantResultados = 0;

        foreach (var boton in listaPrefabsBoton)
        {
            if(!boton.GetComponent<BotonEquipo>().GetEquipoFocus().GetNombre().Contains(filter.ToUpper()))
                boton.SetActive(false);
            else
            {
                boton.SetActive(true);
                cantResultados++;
            }
        }

        buscador.SetCantidadResultados(cantResultados);
    }

    public void CerrarFiltrado()
    {
        foreach (var boton in listaPrefabsBoton)
        {
            boton.SetActive(true);
        }
    }

    #endregion
}