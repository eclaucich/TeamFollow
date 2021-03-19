using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelJugadoresPrincipal : Panel {

    [SerializeField] private GameObject botonJugadorPrefab = null;
    private List<Jugador> jugadores;
    private Equipo equipo;
    private List<GameObject> listaBotonJugador;
    
    [SerializeField] private GameObject botonNuevoJugador = null;
    [SerializeField] private Text adviceText = null;

    [SerializeField] private ScrollRect scrollRect = null;
    [SerializeField] private FlechasScroll flechasScroll = null;

    private Transform parentTransform;

    [SerializeField] private ConfirmacionBorradoJugador panelConfirmacionBorrado = null;
    private GameObject botonJugadorFocus;

    [SerializeField] private InputField inputfield;
    [SerializeField] private MensajeError mensajeErrorNuevoNombre = null;
    [SerializeField] private MensajeError mensajeCambioNombreExitoso = null;
    [SerializeField] private MensajeError mensajeCambioFavorito = null;

    [Space]
    [Header("Seleccion Multiple")]
    [SerializeField] private GameObject botonBorrarEquipo = null;
    [SerializeField] private GameObject botonBorrarSeleccionMultiple = null;
    [SerializeField] private GameObject botonEditarNombreEquipo = null;
    [SerializeField] private ConfirmacionBorradoSeleccionMultiple panelConfirmacionBorradoMultiple = null;

    [Space]
    [SerializeField] private Buscador buscador = null;

    private float prefabHeight;
    private int cantMinima;

    private bool seleccionMultipleActivada = false;

    private void Awake()
    {
        jugadores = new List<Jugador>();
        listaBotonJugador = new List<GameObject>();
        parentTransform = GameObject.Find("ListaJugadores").transform;

        ActivarYDesactivarAdviceText();

        prefabHeight = botonJugadorPrefab.GetComponent<RectTransform>().rect.height; 
    }

    private void Start()
    {
        mensajeErrorNuevoNombre.SetText("NOMBRE EXISTENTE", AppController.Idiomas.Español);
        mensajeErrorNuevoNombre.SetText("EXISTING NAME", AppController.Idiomas.Ingles);

        mensajeCambioNombreExitoso.SetText("NOMBRE CAMBIADO EXITOSAMENTE", AppController.Idiomas.Español);
        mensajeCambioNombreExitoso.SetText("NAME SUCCESSFULLY CHANGED", AppController.Idiomas.Ingles);

        mensajeCambioFavorito.SetText("NUEVO JUGADOR FAVORITO ELEGIDO", AppController.Idiomas.Español);
        mensajeCambioFavorito.SetText("NEW FAVOURITE PLAYER SETTED", AppController.Idiomas.Ingles);

        inputfield.onEndEdit.AddListener(VerificarEdicionNombreEquipo);
    }

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.Escape) && seleccionMultipleActivada)
        {
            ToggleSeleccionMultiple();
        }    
        
        //no sería lo mas ótimo ésto
        if(seleccionMultipleActivada)
            CanvasController.instance.retrocesoPausado = true;
    }

    private void FixedUpdate()
    {
        flechasScroll.Actualizar(scrollRect, cantMinima, listaBotonJugador.Count);
    }

    private void VerificarEdicionNombreEquipo(string _nombreNuevo)
    {
        if(!inputfield.wasCanceled && _nombreNuevo != equipo.GetNombre())
        {
            if (AppController.instance.BuscarEquipoPorNombre(_nombreNuevo) != null)
            {
                Debug.Log("NOMBRE EXISTENTE: " + _nombreNuevo);
                mensajeErrorNuevoNombre.Activar();
                return;
            }
            else
            {
                Debug.Log("NOMBRE CAMBIADO");
                mensajeCambioNombreExitoso.Activar();
                SaveSystem.EditarEquipo(equipo, _nombreNuevo.ToUpper());
                CanvasController.instance.overlayPanel.SetNombrePanel("EQUIPO: " + equipo.GetNombre(), AppController.Idiomas.Español);
                CanvasController.instance.overlayPanel.SetNombrePanel("TEAM: " + equipo.GetNombre(), AppController.Idiomas.Ingles);
            }
        }
    }

    public void SetPanelJugadores(Equipo equipo_)
    {
        seleccionMultipleActivada = false;

        CanvasController.instance.retrocesoPausado = false;

        buscador.SetBuscador(false);
        botonBorrarEquipo.SetActive(true);
        botonBorrarSeleccionMultiple.SetActive(false);
        botonEditarNombreEquipo.SetActive(true);


        equipo = equipo_;

        CanvasController.instance.overlayPanel.SetNombrePanel("EQUIPO: " + equipo.GetNombre(), AppController.Idiomas.Español);
        CanvasController.instance.overlayPanel.SetNombrePanel("TEAM: " + equipo.GetNombre(), AppController.Idiomas.Ingles);

        jugadores = equipo.GetJugadores();
        DetallarJugadores();

        botonNuevoJugador.SetActive(true);
        ActivarYDesactivarAdviceText();
    }


    private void DetallarJugadores()
    {
        BorrarDetalles();
        foreach (var jugador in jugadores)
            AgregarNuevoDetalle(jugador);
    }


    private void AgregarNuevoDetalle(Jugador nuevoJugador)
    {
        GameObject botonJugadorGO = Instantiate(botonJugadorPrefab, parentTransform, false);
        botonJugadorGO.SetActive(true);
        //botonJugadorGO.GetComponentInChildren<Text>().text = nuevoJugador.GetNombre();  ESTO ESTA MAL HACERLO ACA, LA LINEA DE ABAJO ES MAS CORRECTA
        botonJugadorGO.GetComponent<BotonJugador>().SetJugadorFocus(nuevoJugador);

        Jugador _jugadorFavorito = AppController.instance.equipoActual.GetJugadorFavorito();

        if (_jugadorFavorito != null)
        {
            if (_jugadorFavorito == nuevoJugador)
                botonJugadorGO.GetComponent<BotonJugador>().SetComoFavorito();
        }

        listaBotonJugador.Add(botonJugadorGO);

        cantMinima = (int)(scrollRect.GetComponent<RectTransform>().rect.height / (prefabHeight + parentTransform.GetComponent<VerticalLayoutGroup>().spacing + parentTransform.GetComponent<VerticalLayoutGroup>().padding.top + parentTransform.GetComponent<VerticalLayoutGroup>().padding.bottom));
    }

    public void ResetFavouritePlayers()
    {
        foreach (var go in listaBotonJugador)
        {
            BotonJugador _botonEquipo = go.GetComponent<BotonJugador>();
            if (_botonEquipo.GetJugadorFocus() != AppController.instance.equipoActual.GetJugadorFavorito())
                _botonEquipo.DesactivarFavorito();
        }
    }

    public void MensajeFavorito(){
        mensajeCambioFavorito.Activar();
    }

    private void BorrarDetalles()
    {
        for (int i = 0; i < listaBotonJugador.Count; i++)
            Destroy(listaBotonJugador[i]);

        listaBotonJugador.Clear();
    }



    public void BorrarBotonJugador(GameObject botonJugador)
    {
        Destroy(botonJugador);
        listaBotonJugador.Remove(botonJugador);
        ActivarYDesactivarAdviceText();
    }


    private void ActivarYDesactivarAdviceText()
    {
        if (listaBotonJugador.Count == 0)
        {
            adviceText.gameObject.SetActive(true);
        }
        else
        {
            adviceText.gameObject.SetActive(false);
        }
    }

    public void SetBotonJugadorFocus(GameObject botonJugador_)
    {
        botonJugadorFocus = botonJugador_;
    }

    public BotonJugador GetBotonJugadorFocus()
    {
        return botonJugadorFocus.GetComponent<BotonJugador>();
    }


    public void AbrirPanelConfirmacionBorrado()
    {
        panelConfirmacionBorrado.Activar(AppController.instance.jugadorActual.GetNombre(), botonJugadorFocus);
    }

    public ConfirmacionBorradoJugador GetPanelConfirmacionBorrado()
    {
        return panelConfirmacionBorrado;
    }

    #region Seleccion multiple

    public void ToggleSeleccionMultiple()
    {
        bool estadoSiguiente = !seleccionMultipleActivada;
        
        CanvasController.instance.retrocesoPausado = estadoSiguiente;
        seleccionMultipleActivada = estadoSiguiente;

        botonBorrarEquipo.SetActive(!estadoSiguiente);
        botonBorrarSeleccionMultiple.SetActive(estadoSiguiente);
        botonEditarNombreEquipo.SetActive(!estadoSiguiente);

        foreach (var go in listaBotonJugador)
        {
            go.GetComponent<BotonJugador>().SetSeleccionMultiple(estadoSiguiente);
        }
    }

    public void ActivarPanelBorradoSeleccionMultiple()
    {
        List<BotonJugador> listaSeleccionMultiple = new List<BotonJugador>();
        foreach (var go in listaBotonJugador)
        {
            if(go.GetComponent<BotonJugador>().IsSelected())
                listaSeleccionMultiple.Add(go.GetComponent<BotonJugador>());
        }
        panelConfirmacionBorradoMultiple.Activar(listaSeleccionMultiple);
    }

    #endregion

        #region Buscador

    public void ActualizarBusqueda(Text filterText)
    {
        string filter = filterText.text;

        int  cantResultados = 0;

        foreach (var boton in listaBotonJugador)
        {
            if(!boton.GetComponent<BotonJugador>().GetNombre().Contains(filter.ToUpper()))
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
        foreach (var boton in listaBotonJugador)
        {
            boton.SetActive(true);
        }
    }

    #endregion
}
