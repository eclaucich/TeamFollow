using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelPartidosEquipo : Panel
{
    private Equipo equipoFocus;

    [SerializeField] private GameObject panel_partidos = null;
    [SerializeField] private GameObject panel_detalle_partido = null;

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

        prefabHeight = partidoprefab.GetComponent<RectTransform>().rect.height;
    }

    private void FixedUpdate()
    {
        flechasScroll.Actualizar(scrollRectEquipos, cantMinima, listaPartidosPrefabs.Count);
    }

    public void SetearPanelPartidos()
    {
        ActivarPanel(0);

        Screen.orientation = ScreenOrientation.Portrait;
        AppController.instance.overlayPanel.gameObject.SetActive(true);

        equipoFocus = AppController.instance.equipoActual;

        AppController.instance.overlayPanel.SetNombrePanel(equipoFocus.GetNombre() + ": ESTADISTICAS GLOBALES", AppController.Idiomas.Español);
        AppController.instance.overlayPanel.SetNombrePanel(equipoFocus.GetNombre() + ": GLOBAL STATISTICS", AppController.Idiomas.Ingles);

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
            go.GetComponentInChildren<Text>().text = partido.GetNombre();
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


    public void SetPartidoFocus(BotonPartido botonpartido)
    {
        string nombrePartido = botonpartido.GetComponentInChildren<Text>().text;

        List<Partido> partidos = isPartido ? equipoFocus.GetPartidos() : equipoFocus.GetPracticas();

        foreach (var partido in partidos)
        {
            if (partido.GetNombre() == nombrePartido)
            {
                MostrarPanelDetallePartido(partido);// (botonpartido, partido.GetEstadisticas());
                return;
            }
        }
    }

    public void BorrarPartido(Partido _partido)
    {
        string nombrePartido = _partido.GetNombre();

        equipoFocus.BorrarPartido(isPartido, nombrePartido);

        foreach (var jugador in equipoFocus.GetJugadores()) 
        {
            jugador.BorrarPartido(isPartido, nombrePartido);
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
}
