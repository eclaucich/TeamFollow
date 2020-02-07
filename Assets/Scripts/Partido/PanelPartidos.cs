using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelPartidos : Panel
{
    private Jugador jugadorFocus;

    [SerializeField] private GameObject partidoprefab = null;

    [SerializeField] private Image imagenPartido = null;
    [SerializeField] private Image imagenPractica = null;
    [SerializeField] private Color colorSeleccionado = new Color();
    [SerializeField] private Color colorNoSeleccionado = new Color();

    private List<GameObject> listaPartidosPrefabs;
    private List<Partido> listaPartidos;

    private Transform parentTransform;

    private bool isPartido = true;

    private void Awake()
    {
        parentTransform = GameObject.Find("Partidos").transform;
        listaPartidosPrefabs = new List<GameObject>();
    }

    public void SetearPanelPartidos(string nombreJugador)
    {
        jugadorFocus = AppController.instance.equipoActual.BuscarPorNombre(nombreJugador);

        if (listaPartidosPrefabs == null) listaPartidosPrefabs = new List<GameObject>();

        MostrarPartidos();
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
            go.transform.GetChild(0).GetComponentInChildren<Text>().text = partido.GetNombre();
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
        listaPartidos = jugadorFocus.GetPartidos();
        ResetPrefabs();
    }

    public void MostrarPracticas()
    {
        isPartido = false;
        imagenPartido.color = colorNoSeleccionado;
        imagenPractica.color = colorSeleccionado;
        listaPartidos = jugadorFocus.GetPracticas();
        ResetPrefabs();
    }

    public void MostrarEstadisticasGlobales()
    {
        Estadisticas estadisticas = isPartido ? jugadorFocus.GetEstadisticasPartido() : jugadorFocus.GetEstadisticasPractica();

        GetComponentInParent<PanelJugadores>().MostrarPanelDetalleJugador(null, jugadorFocus.GetNombre(), estadisticas);
    }



    public void BorrarPartido(BotonPartido botonPartido)
    {
        string nombrePartido = botonPartido.GetComponentInChildren<Text>().text;

        List<Partido> partidos = isPartido ? jugadorFocus.GetPartidos() : jugadorFocus.GetPracticas();

        Partido partidoFocus = jugadorFocus.BuscarPartido(isPartido, nombrePartido);

        if (partidoFocus == null) return;

        //Borrar las estadisticas del partido del jugador y del equipo
        if (isPartido)
        {
            jugadorFocus.BorrarEstadisticaPartido(partidoFocus);
            AppController.instance.equipoActual.BorrarEstadisticaPartido(partidoFocus);
        }
        else
        {
            jugadorFocus.BorrarEstadisticaPractica(partidoFocus);
            AppController.instance.equipoActual.BorrarEstadisticaPractica(partidoFocus);
        }


        //Borrar el archivo guardado del partido
        SaveSystem.BorrarPartido(isPartido, partidoFocus, jugadorFocus, AppController.instance.equipoActual);

        //Eliminar el prefab
        Destroy(botonPartido.transform.parent.gameObject);
        listaPartidosPrefabs.Remove(botonPartido.transform.parent.gameObject);

        //Eliminar el partido de la lista de partidos
        listaPartidos.Remove(partidoFocus);
    }



    public bool IsPartido()
    {
        return isPartido;
    }
}
