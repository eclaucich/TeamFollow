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

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.Escape) && seleccionMultipleActivada)
        {
            CanvasController.instance.retrocesoPausado = false;
            SetSeleccionMultiple(false);
        }    
    }

    public void SetearPanelPartidos(Jugador _jugador)
    {
        CanvasController.instance.retrocesoPausado = false;
        
        CanvasController.instance.overlayPanel.gameObject.SetActive(true);
        buscador.SetBuscador(false);
        SetPublicity();

        if (_jugador == null)
            jugadorFocus = AppController.instance.jugadorActual;//equipoActual.BuscarPorNombre(nombreJugador);
        else
            jugadorFocus = _jugador;

        if (listaPartidosPrefabs == null) listaPartidosPrefabs = new List<GameObject>();

        Estadisticas estadisticas = isPartido ? jugadorFocus.GetEstadisticasPartido() : jugadorFocus.GetEstadisticasPractica();

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
            //go.transform.GetChild(0).GetComponentInChildren<Text>().text = partido.GetNombre();
            go.GetComponent<BotonPartido>().SetPartidoFocus(partido);
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
        Debug.Log("PARTIDOS");
        isPartido = true;

        CanvasController.instance.overlayPanel.SetNombrePanel("PARTIDOS DE: " + AppController.instance.jugadorActual.GetNombre(), AppController.Idiomas.Español);
        CanvasController.instance.overlayPanel.SetNombrePanel("MATCHES OF: " + AppController.instance.jugadorActual.GetNombre(), AppController.Idiomas.Ingles);
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

        botonSeleccionarPartidos.SetColorSeleccionado();
        botonSeleccionarPracticas.SetColorActivado();

        ResetPrefabs();
    }

    public void MostrarPracticas()
    {
        isPartido = false;

        CanvasController.instance.overlayPanel.SetNombrePanel("PRACTICAS DE: " + jugadorFocus.GetNombre(), AppController.Idiomas.Español);
        CanvasController.instance.overlayPanel.SetNombrePanel("PRACTICES OF: " + jugadorFocus.GetNombre(), AppController.Idiomas.Ingles);

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

        botonSeleccionarPracticas.SetColorSeleccionado();
        botonSeleccionarPartidos.SetColorActivado();

        ResetPrefabs();
    }

    public void MostrarEstadisticasGlobales()
    {
        Estadisticas estadisticas = isPartido ? jugadorFocus.GetEstadisticasPartido() : jugadorFocus.GetEstadisticasPractica();

        CanvasController.instance.overlayPanel.SetNombrePanel("ESTADISTICAS GLOBALES DE JUGADOR: " + jugadorFocus.GetNombre(), AppController.Idiomas.Español);
        CanvasController.instance.overlayPanel.SetNombrePanel("GLOBAL STATISTICS OF: " + jugadorFocus.GetNombre(), AppController.Idiomas.Ingles);

        GetComponentInParent<PanelJugadores>().MostrarPanelDetalleJugador(null, jugadorFocus.GetNombre(), estadisticas);
    }



    public void BorrarPartido(Partido _partido)
    {
        string nombrePartido = _partido.GetNombre().ToUpper();

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

        confirmacionBorradoSeleccionMultiple.Activar(botones, true);
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
