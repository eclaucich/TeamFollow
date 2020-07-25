using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelPartidos : Panel
{
    private Jugador jugadorFocus;

    [SerializeField] private GameObject partidoprefab = null;

    [SerializeField] private BotonNormal botonSeleccionarPartidos = null;
    [SerializeField] private BotonNormal botonSeleccionarPracticas = null;
    [SerializeField] private BotonNormal botonVerEstadisticasGlobales = null;

    [SerializeField] private GameObject warningTextPartidos = null;
    [SerializeField] private GameObject warningTextPracticas = null;

    [SerializeField] private FlechasScroll flechasScroll = null;
    [SerializeField] private ScrollRect scrollRect = null;


    private List<GameObject> listaPartidosPrefabs;
    private List<Partido> listaPartidos;

    private Transform parentTransform;

    private bool isPartido = true;

    private float prefabHeight;
    private int cantMinima;

    private void Awake()
    {
        parentTransform = GameObject.Find("Partidos").transform;
        listaPartidosPrefabs = new List<GameObject>();
        prefabHeight = partidoprefab.GetComponent<RectTransform>().rect.height;
    }

    private void FixedUpdate()
    {
        flechasScroll.Actualizar(scrollRect, cantMinima, listaPartidosPrefabs.Count);
    }

    public void SetearPanelPartidos(string nombreJugador)
    {
        jugadorFocus = AppController.instance.jugadorActual;//equipoActual.BuscarPorNombre(nombreJugador);

        if (listaPartidosPrefabs == null) listaPartidosPrefabs = new List<GameObject>();

        Estadisticas estadisticas = isPartido ? jugadorFocus.GetEstadisticasPartido() : jugadorFocus.GetEstadisticasPractica();

        /*Image imagen = botonVerEstadisticasGlobales.GetComponent<Image>();

        if (estadisticas.isEmpty())
        {
            imagen.color = new Color(imagen.color.r, imagen.color.g, imagen.color.b, 0.25f);
            botonVerEstadisticasGlobales.enabled = false;
        }
        else
        {
            imagen.color = new Color(imagen.color.r, imagen.color.g, imagen.color.b, 255f);
            botonVerEstadisticasGlobales.enabled = true;
        }*/

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

        cantMinima = (int)(scrollRect.GetComponent<RectTransform>().rect.height / (prefabHeight + parentTransform.GetComponent<VerticalLayoutGroup>().spacing));
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

        //imagenPartido.color = colorSeleccionado;
        //imagenPractica.color = colorNoSeleccionado;

        listaPartidos = jugadorFocus.GetPartidos();

        if (listaPartidos.Count == 0)
        {
            warningTextPartidos.SetActive(true);
            botonVerEstadisticasGlobales.Desactivar();
            //botonVerEstadisticasGlobales.enabled = false;
            //Color color = botonVerEstadisticasGlobales.GetComponent<Image>().color;
            //botonVerEstadisticasGlobales.GetComponent<Image>().color = new Color(color.r, color.g, color.b, 0.2f);
        }
        else
        {
            warningTextPartidos.SetActive(false);
            botonVerEstadisticasGlobales.Activar();
           // botonVerEstadisticasGlobales.enabled = true;
           // Color color = botonVerEstadisticasGlobales.GetComponent<Image>().color;
            //botonVerEstadisticasGlobales.GetComponent<Image>().color = new Color(color.r, color.g, color.b, 1f);
        }

        warningTextPracticas.SetActive(false);

        botonSeleccionarPartidos.SetColorDesactivado();
        botonSeleccionarPracticas.SetColorActivado();

        ResetPrefabs();
    }

    public void MostrarPracticas()
    {
        isPartido = false;

        //imagenPartido.color = colorNoSeleccionado;
        //imagenPractica.color = colorSeleccionado;

        listaPartidos = jugadorFocus.GetPracticas();

        if (listaPartidos.Count == 0)
        {
            warningTextPracticas.SetActive(true);
            botonVerEstadisticasGlobales.Desactivar();
            //botonVerEstadisticasGlobales.enabled = false;
            //Color color = botonVerEstadisticasGlobales.GetComponent<Image>().color;
            //botonVerEstadisticasGlobales.GetComponent<Image>().color = new Color(color.r, color.g, color.b, 0.2f);
        }
        else
        {
            warningTextPracticas.SetActive(false);
            botonVerEstadisticasGlobales.Activar();
            //botonVerEstadisticasGlobales.enabled = true;
            //Color color = botonVerEstadisticasGlobales.GetComponent<Image>().color;
            //botonVerEstadisticasGlobales.GetComponent<Image>().color = new Color(color.r, color.g, color.b, 1f);
        }

        warningTextPartidos.SetActive(false);

        botonSeleccionarPracticas.SetColorDesactivado();
        botonSeleccionarPartidos.SetColorActivado();

        ResetPrefabs();
    }

    public void MostrarEstadisticasGlobales()
    {
        Estadisticas estadisticas = isPartido ? jugadorFocus.GetEstadisticasPartido() : jugadorFocus.GetEstadisticasPractica();

        GetComponentInParent<PanelJugadores>().MostrarPanelDetalleJugador(null, jugadorFocus.GetNombre(), estadisticas);
    }



    public void BorrarPartido(Partido _partido)
    {
        string nombrePartido = _partido.GetNombre();

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
        //Destroy(botonPartido.transform.parent.gameObject);
        //listaPartidosPrefabs.Remove(botonPartido.transform.parent.gameObject);

        //Eliminar el partido de la lista de partidos
        listaPartidos.Remove(partidoFocus);

        ResetPrefabs();
        
    }

    public bool IsPartido()
    {
        return isPartido;
    }
}
