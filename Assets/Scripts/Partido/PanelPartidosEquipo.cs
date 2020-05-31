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

    private List<GameObject> listaPartidosPrefabs;
    private List<Partido> listaPartidos;

    private Transform parentTransform;

    private bool isPartido = true;

    private Estadisticas estadisticasGlobalesEquipo;

    private void Awake()
    {
        parentTransform = GameObject.Find("PartidosEquipo").transform;
        listaPartidosPrefabs = new List<GameObject>();

        listaPaneles = new List<GameObject>();
        listaPaneles.Add(panel_partidos);
        listaPaneles.Add(panel_detalle_partido);
    }

    private void FixedUpdate()
    {
        /*if (parentTransform.childCount < 9)
        {
            scrollRectEquipos.enabled = false;
            flechaAbajo.SetActive(false);
            flechaArriba.SetActive(false);
        }
        else
        {
            scrollRectEquipos.enabled = true;

            if (scrollRectEquipos.verticalNormalizedPosition > .95f) flechaArriba.SetActive(false); else flechaArriba.SetActive(true);
            if (scrollRectEquipos.verticalNormalizedPosition < 0.05f) flechaAbajo.SetActive(false); else flechaAbajo.SetActive(true);
        }*/
        flechasScroll.Actualizar(scrollRectEquipos, 7, parentTransform.childCount);
    }

    public void SetearPanelPartidos()
    {
        ActivarPanel(0);

        CanvasController.instance.AgregarPanelAnterior(CanvasController.Paneles.DetalleEquipoPrincipal);

        equipoFocus = AppController.instance.equipoActual;

        if (listaPartidosPrefabs == null) listaPartidosPrefabs = new List<GameObject>();

        MostrarPartidos();
    }

    public void MostrarPanelDetallePartido(BotonPartido botonPartidoFocus, Estadisticas _estadisticas)
    {
        ActivarPanel(1);

        panel_detalle_partido.GetComponent<PanelEstadisticasGlobalesEquipo>().SetPanelEstadisticasGlobalesEquipo(botonPartidoFocus, _estadisticas);
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


        botonSeleccionarPartido.SetColorDesactivado();
        botonSeleccionarPractica.SetColorActivado();

        if (estadisticasPartido.isEmpty())
            botonVerEstadisticasGlobales.Desactivar();
        else
            botonVerEstadisticasGlobales.Activar();

        if (listaPartidos.Count == 0)
            warningTextPartidos.SetActive(true);
        else 
            warningTextPartidos.SetActive(false);

        warningTextPracticas.SetActive(false);

        ResetPrefabs();
    }

    public void MostrarPracticas()
    {
        isPartido = false;

        listaPartidos = equipoFocus.GetPracticas();

        Estadisticas estadisticasPracticas = equipoFocus.GetEstadisticasPractica();

        botonSeleccionarPartido.SetColorActivado();
        botonSeleccionarPractica.SetColorDesactivado();

        if (estadisticasPracticas.isEmpty())
            botonVerEstadisticasGlobales.Desactivar();
        else
            botonVerEstadisticasGlobales.Activar();

        if (listaPartidos.Count == 0)
            warningTextPracticas.SetActive(true);
        else
            warningTextPracticas.SetActive(false);
        warningTextPartidos.SetActive(false);

        ResetPrefabs();
    }

    public void MostrarEstadisticasGlobales()
    {
        //MostrarPanelDetallePartido(null, estadisticasGlobalesEquipo);
        ActivarPanel(1);

        Estadisticas estEquipo = isPartido ? equipoFocus.GetEstadisticasPartido() : equipoFocus.GetEstadisticasPractica();


        panel_detalle_partido.GetComponent<PanelEstadisticasGlobalesEquipo>().SetPanelEstadisticasGlobalesEquipo(null, estEquipo);
    }



    public void SetPartidoFocus(BotonPartido botonpartido)
    {
        string nombrePartido = botonpartido.GetComponentInChildren<Text>().text;

        List<Partido> partidos = isPartido ? equipoFocus.GetPartidos() : equipoFocus.GetPracticas();

        foreach (var partido in partidos)
        {
            if (partido.GetNombre() == nombrePartido)
            {
                MostrarPanelDetallePartido(botonpartido, partido.GetEstadisticas());
                return;
            }
        }
    }

    public void BorrarPartido(BotonPartido botonPartido)
    {
        if (botonPartido == null) Debug.Log("NULL PARTIDO");

        string nombrePartido = botonPartido.GetComponentInChildren<Text>().text;

        equipoFocus.BorrarPartido(isPartido, nombrePartido);

        foreach (var jugador in equipoFocus.GetJugadores()) 
        {
            jugador.BorrarPartido(isPartido, nombrePartido);
        }

        listaPartidosPrefabs.Remove(botonPartido.transform.parent.gameObject);
        Destroy(botonPartido.transform.parent.gameObject);
    }


    private void ActivarPanel(int index)
    {
        foreach (var panel in listaPaneles) panel.SetActive(false);

        listaPaneles[index].SetActive(true);
    }
}
