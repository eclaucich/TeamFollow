using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelPartidosEquipo : Panel
{
    private Equipo equipoFocus;

    [SerializeField] private GameObject panel_partidos = null;
    [SerializeField] private GameObject panel_detalle_partido = null;
    [SerializeField] private GameObject panelGraficaResumen = null;

    private List<GameObject> listaPaneles;

    [SerializeField] private GameObject partidoprefab = null;

    /*[SerializeField] private Image imagenPartido = null;
    [SerializeField] private Image imagenPractica = null;
    [SerializeField] private Color colorSeleccionado = new Color();
    [SerializeField] private Color colorNoSeleccionado = new Color();*/
    [SerializeField] private BotonNormal botonSeleccionarPartido = null;
    [SerializeField] private BotonNormal botonSeleccionarPractica = null;

    //[SerializeField] private Image botonVerEstadisticasGlobales = null;
    [SerializeField] private BotonNormal botonVerEstadisticasGlobales = null;
    [SerializeField] private GameObject warningTextPartidos = null;
    [SerializeField] private GameObject warningTextPracticas = null;

    [SerializeField] private ScrollRect scrollRectEquipos = null;
    [SerializeField] private FlechasScroll flechasScroll = null;

    [SerializeField] private Transform parentTransform = null;

    [Space]
    [Header("Seleccion Multiple")]
    [SerializeField] private ConfirmacionBorradoSeleccionMultiple confirmacionBorradoSeleccionMultiple = null;
    [SerializeField] private GameObject botonBorrarSeleccionMultiple = null;
    private bool seleccionMultipleActivada = false;


    [Space]
    [Header("Buscador")]
    [SerializeField] private Buscador buscador = null;


    private List<GameObject> listaPartidosPrefabs;
    private List<Partido> listaPartidos;

    private bool isPartido = true;

    private Estadisticas estadisticasGlobalesEquipo;

    private int cantMinima;
    private float prefabHeight;

    private void Awake()
    {
        listaPartidosPrefabs = new List<GameObject>();

        listaPaneles = new List<GameObject>();
        listaPaneles.Add(panel_partidos);
        listaPaneles.Add(panel_detalle_partido);
        listaPaneles.Add(panelGraficaResumen);

        prefabHeight = partidoprefab.GetComponent<RectTransform>().rect.height;
    }

    private void FixedUpdate()
    {
        flechasScroll.Actualizar(scrollRectEquipos, cantMinima, listaPartidosPrefabs.Count);
    }

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.Escape) && seleccionMultipleActivada)
            SetSeleccionMultiple(false); 

        //no sería lo mas ótimo ésto
        if(seleccionMultipleActivada)
            CanvasController.instance.retrocesoPausado = true;
    }

    public void SetearPanelPartidos()
    {
        ActivarPanel(0);

        Screen.orientation = ScreenOrientation.Portrait;
        CanvasController.instance.retrocesoPausado = false;
        CanvasController.instance.overlayPanel.gameObject.SetActive(true);

        buscador.SetBuscador(false);
        SetPublicity();

        equipoFocus = AppController.instance.equipoActual;

        CanvasController.instance.AgregarPanelAnterior(CanvasController.Paneles.DetalleEquipoPrincipal);

        if (listaPartidosPrefabs == null) listaPartidosPrefabs = new List<GameObject>();

        MostrarPartidos();
    }

    public void MostrarPanelDetallePartido(BotonPartido botonPartidoFocus, Estadisticas _estadisticas)
    {
        ActivarPanel(1);

        panel_detalle_partido.GetComponent<PanelEstadisticasGlobalesEquipo>().SetPanelEstadisticasGlobalesEquipo(_estadisticas);
    }

    public void MostrarPanelDetallePartido(Partido _partido, bool fromGrafica=false)
    {
        if (!gameObject.activeSelf) gameObject.SetActive(true);
        ActivarPanel(1);
        panel_detalle_partido.GetComponent<PanelEstadisticasGlobalesEquipo>().SetPanelEstadisticasGlobalesEquipo(_partido, fromGrafica);
    }

    private void ResetPrefabs()
    {
        BorrarPrefabs();
        CrearPrefabs();
    }

    private void CrearPrefabs()
    {
        if (listaPartidosPrefabs == null) return;
        foreach (var partido in listaPartidos)
        {
            GameObject go = Instantiate(partidoprefab, parentTransform, false);
            go.SetActive(true);
            go.GetComponent<BotonPartido>().SetPartidoFocus(partido);
            listaPartidosPrefabs.Add(go);
        }

        cantMinima = (int)(scrollRectEquipos.GetComponent<RectTransform>().rect.height / (prefabHeight + parentTransform.GetComponent<VerticalLayoutGroup>().spacing));
    }

    private void BorrarPrefabs()
    {
        if (listaPartidosPrefabs == null) return;
        foreach (var GO in listaPartidosPrefabs)
        {
            Destroy(GO);
        }
        listaPartidosPrefabs.Clear();
    }

    public void MostrarPartidos()
    {
        isPartido = true;

        CanvasController.instance.overlayPanel.SetNombrePanel(equipoFocus.GetNombre() + ": PARTIDOS", AppController.Idiomas.Español);
        CanvasController.instance.overlayPanel.SetNombrePanel(equipoFocus.GetNombre() + ": MATCHES", AppController.Idiomas.Ingles);
        
        listaPartidos = equipoFocus.GetPartidos();

        Estadisticas estadisticasPartido = equipoFocus.GetEstadisticasPartido();

        if (listaPartidos.Count == 0)
            warningTextPartidos.SetActive(true);
        else 
            warningTextPartidos.SetActive(false);

        warningTextPracticas.SetActive(false);

        if (estadisticasPartido.isEmpty())
            botonVerEstadisticasGlobales.Desactivar();
        else
            botonVerEstadisticasGlobales.Activar();

        botonSeleccionarPartido.SetColorSeleccionado();
        botonSeleccionarPractica.SetColorActivado();

        ResetPrefabs();
    }

    public void MostrarPracticas()
    {
        isPartido = false;

        CanvasController.instance.overlayPanel.SetNombrePanel(equipoFocus.GetNombre() + ": PRACTICAS", AppController.Idiomas.Español);
        CanvasController.instance.overlayPanel.SetNombrePanel(equipoFocus.GetNombre() + ": PRACTICES", AppController.Idiomas.Ingles);

        listaPartidos = equipoFocus.GetPracticas();

        Estadisticas estadisticasPracticas = equipoFocus.GetEstadisticasPractica();

        if (listaPartidos.Count == 0)
            warningTextPracticas.SetActive(true);
        else
            warningTextPracticas.SetActive(false);
        warningTextPartidos.SetActive(false);

        if (estadisticasPracticas.isEmpty())
            botonVerEstadisticasGlobales.Desactivar();
        else
            botonVerEstadisticasGlobales.Activar();

        botonSeleccionarPartido.SetColorActivado();
        botonSeleccionarPractica.SetColorSeleccionado();

        ResetPrefabs();
    }

    public void MostrarEstadisticasGlobales()
    {
        //MostrarPanelDetallePartido(null, estadisticasGlobalesEquipo);
        ActivarPanel(1);

        Estadisticas estEquipo = isPartido ? equipoFocus.GetEstadisticasPartido() : equipoFocus.GetEstadisticasPractica();

        panel_detalle_partido.GetComponent<PanelEstadisticasGlobalesEquipo>().SetPanelEstadisticasGlobalesEquipo(estEquipo);
    }

    public void BorrarPartido(Partido _partido)
    {
        string nombrePartido = _partido.GetNombre();

        equipoFocus.BorrarPartido(isPartido, nombrePartido.ToUpper());

        foreach (var jugador in equipoFocus.GetJugadores()) 
        {
            jugador.BorrarPartido(isPartido, nombrePartido.ToUpper());
        }

        //listaPartidosPrefabs.Remove(botonPartido.transform.parent.gameObject);
        //Destroy(botonPartido.transform.parent.gameObject);
        listaPartidos.Remove(_partido);
        ResetPrefabs();
    }


    private void ActivarPanel(int index)
    {
        foreach (var panel in listaPaneles) panel.SetActive(false);

        listaPaneles[index].SetActive(true);
    }

    public bool GetIsPartido()
    {
        return isPartido;
    }

    #region Seleccion Multiple
    public void SetSeleccionMultiple(bool active)
    {
        seleccionMultipleActivada = active;

        botonBorrarSeleccionMultiple.SetActive(seleccionMultipleActivada);
        CanvasController.instance.retrocesoPausado = seleccionMultipleActivada;

        foreach (var go in listaPartidosPrefabs)
        {
            go.GetComponent<BotonPartido>().SetSeleccionMultiple(seleccionMultipleActivada);
        }
    }

    public void ActivarBorradoSeleccionMultiple()
    {
        List<BotonPartido> botones = new List<BotonPartido>();

        foreach (var boton in listaPartidosPrefabs)
        {
            if(boton.GetComponent<BotonPartido>().IsSelected())
                botones.Add(boton.GetComponent<BotonPartido>());
        }

        confirmacionBorradoSeleccionMultiple.Activar(botones, false);
    }

    #endregion


    #region Buscador
    public void ActualizarBusqueda(Text filterText)
    {
        string filter = filterText.text;

        int cantResultados = 0;

        foreach (var boton in listaPartidosPrefabs)
        {
            if (!boton.GetComponent<BotonPartido>().GetPartido().GetNombre().Contains(filter.ToUpper()))
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
        foreach (var boton in listaPartidosPrefabs)
        {
            boton.SetActive(true);
        }
    }

    #endregion
}
