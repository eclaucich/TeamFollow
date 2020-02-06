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

    [SerializeField] private Image imagenPartido = null;
    [SerializeField] private Image imagenPractica = null;
    [SerializeField] private Color colorSeleccionado = new Color();
    [SerializeField] private Color colorNoSeleccionado = new Color();

    [SerializeField] private Button botonVerEstadisticasGlobales = null;
    [SerializeField] private GameObject warningTextPartidos = null;
    [SerializeField] private GameObject warningTextPracticas = null;

    private List<GameObject> listaPartidosPrefabs;
    private List<Partido> listaPartidos;

    private Transform parentTransform;

    private bool isPartido = true;

    private void Awake()
    {
        parentTransform = GameObject.Find("PartidosEquipo").transform;
        listaPartidosPrefabs = new List<GameObject>();

        listaPaneles = new List<GameObject>();
        listaPaneles.Add(panel_partidos);
        listaPaneles.Add(panel_detalle_partido);
    }

    public void SetearPanelPartidos()
    {
        ActivarPanel(0);

        CanvasController.instance.AgregarPanelAnterior(CanvasController.Paneles.DetalleEquipoPrincipal);

        equipoFocus = AppController.instance.equipoActual;

        if (listaPartidosPrefabs == null) listaPartidosPrefabs = new List<GameObject>();

        MostrarPartidos();
    }

    public void MostrarPanelDetallePartido(Estadisticas _estadisticas)
    {
        ActivarPanel(1);

        panel_detalle_partido.GetComponent<PanelEstadisticasGlobalesEquipo>().SetPanelEstadisticasGlobalesEquipo(_estadisticas);
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
        imagenPartido.color = colorSeleccionado;
        imagenPractica.color = colorNoSeleccionado;
        listaPartidos = equipoFocus.GetPartidos();

        if (listaPartidos.Count == 0)
            warningTextPartidos.SetActive(true);
        else 
            warningTextPartidos.SetActive(false);

        warningTextPracticas.SetActive(false);

        if (equipoFocus.GetEstadisticasPartido().GetDictionary().Count == 0)
            botonVerEstadisticasGlobales.enabled = false;
        else
            botonVerEstadisticasGlobales.enabled = true;

        ResetPrefabs();
    }

    public void MostrarPracticas()
    {
        isPartido = false;
        imagenPartido.color = colorNoSeleccionado;
        imagenPractica.color = colorSeleccionado;
        listaPartidos = equipoFocus.GetPracticas();

        if (listaPartidos.Count == 0)
            warningTextPracticas.SetActive(true);
        else
            warningTextPracticas.SetActive(false);
        warningTextPartidos.SetActive(false);

        if (equipoFocus.GetEstadisticasPractica().GetDictionary().Count == 0)
        {
            botonVerEstadisticasGlobales.enabled = false;
        }
        else
            botonVerEstadisticasGlobales.enabled = true;

        ResetPrefabs();
    }

    public void MostrarEstadisticasGlobales()
    {
        Estadisticas estadisticas = isPartido ? equipoFocus.GetEstadisticasPartido() : equipoFocus.GetEstadisticasPractica();

        MostrarPanelDetallePartido(estadisticas);
    }



    public void SetPartidoFocus(BotonPartido botonpartido)
    {
        string nombrePartido = botonpartido.GetComponentInChildren<Text>().text;

        List<Partido> partidos = isPartido ? equipoFocus.GetPartidos() : equipoFocus.GetPracticas();

        foreach (var partido in partidos)
        {
            if (partido.GetNombre() == nombrePartido)
            {
                MostrarPanelDetallePartido(partido.GetEstadisticas());
                return;
            }
        }
    }

    public void BorrarPartido(BotonPartido botonPartido)
    {
        string nombrePartido = botonPartido.GetComponentInChildren<Text>().text;

        equipoFocus.BorrarPartido(isPartido, nombrePartido);

        foreach (var jugador in equipoFocus.GetJugadores()) 
        {
            jugador.BorrarPartido(isPartido, nombrePartido);
        }

        Destroy(botonPartido.transform.parent.gameObject);
        listaPartidosPrefabs.Remove(botonPartido.transform.parent.gameObject);
    }


    private void ActivarPanel(int index)
    {
        foreach (var panel in listaPaneles) panel.SetActive(false);

        listaPaneles[index].SetActive(true);
    }
}
