using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EntradaDatosGenerico : EntradaDatos
{
    [SerializeField] private SeleccionListaJugadores seleccionListaJugadores = null;
    [SerializeField] private GameObject overlayEntradaDatos = null;
    [SerializeField] private RelojEntradaDatos relojEntradaDatos = null;

    [SerializeField] private SeccionBanca seccionBanca = null;
    [SerializeField] private SeccionCancha seccionCancha = null;
    [SerializeField] private SeccionEstadisticas seccionEstadisticas = null;

    [SerializeField] private MensajeDesplegable panelConfirmacionGuardado = null;
    [SerializeField] private MensajeError mensajeErrorGuardado = null;
    [SerializeField] private Text nombrePartidoText = null;
    [SerializeField] private Image imagenSeleccion = null;
    [SerializeField] private Sprite botonGuardarPartidoSprite = null;
    [SerializeField] private Sprite botonGuardarPracticaSprite = null;

    [SerializeField] private ResultadoNormal resultadoNormal = null;
    [SerializeField] private ResultadoSets resultadoSets = null;

    [SerializeField] private Toggle toggleResultado = null;
    [SerializeField] private GameObject overlayInputsResultado = null;
    private bool insertarResultado;

    private List<Jugador> jugadoresSeleccionados;
    private List<EstadisticaDeporte.Estadisticas> listaEstadisticas;
    private List<string> listaNombres;
    private List<string> listaIniciales;

    private bool isPartido;

    private void Start()
    {
        listaEstadisticas = PanelSeleccionEstadisticas.instance.GetListaEstadisticas();
        listaNombres = PanelSeleccionEstadisticas.instance.GetListaNombreEstadisticas();
        listaIniciales = PanelSeleccionEstadisticas.instance.GetListaInicialesEstadisticas();
    }

    private void Update()
    {
        if (!AppController.instance.pantallaCarga.gameObject.activeSelf)
            CanvasController.instance.retrocesoPausado = true;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("PAUSED UPD?: " + CanvasController.instance.retrocesoPausado);

            if (seleccionListaJugadores.gameObject.activeSelf)
            {
                CanvasController.instance.retrocesoPausado = false;
                CanvasController.instance.AgregarPanelAnterior(CanvasController.Paneles.SeleccionEstadisticas);
                CanvasController.instance.MostrarPanelAnterior();
                Destroy(gameObject);
                return;
            }
            else
            {
                panelConfirmacionGuardado.ToggleDesplegar();
            }
        }
        if (panelConfirmacionGuardado.isDesplegado())
            relojEntradaDatos.paused = true;
    }

    public override void Display(bool _isPartido)
    {
        isPartido = _isPartido;

        seleccionListaJugadores.SetearListaJugadores();

        Deportes.DeporteEnum deporteActual = AppController.instance.equipoActual.GetDeporte();

        switch (deporteActual)
        {
            //NORMAL CON PENALES
            case Deportes.DeporteEnum.Futbol:
            case Deportes.DeporteEnum.HockeyCesped:
            case Deportes.DeporteEnum.HockeyPatines:
            case Deportes.DeporteEnum.Handball:
                resultadoNormal.gameObject.SetActive(true);
                resultadoSets.gameObject.SetActive(false);
                resultadoNormal.ActivateEdition();
                Debug.Log("NORMAL CON PENALES");
                break;

            //NORMAL SIN PENALES
            case Deportes.DeporteEnum.Basket:
            case Deportes.DeporteEnum.Rugby:
            case Deportes.DeporteEnum.Softball:
                resultadoNormal.gameObject.SetActive(true);
                resultadoSets.gameObject.SetActive(false);
                resultadoNormal.ActivateEdition(false); //sin penales
                Debug.Log("NORMAL SIN PENALES");
                break;

            //SETS CON TIEBREAK
            case Deportes.DeporteEnum.Padel:
            case Deportes.DeporteEnum.Tenis:
            case Deportes.DeporteEnum.Voley:
                resultadoNormal.gameObject.SetActive(false);
                resultadoSets.gameObject.SetActive(true);
                resultadoSets.ActivateEdition();
                resultadoSets.AgregarSet();
                Debug.Log("SETS CON TIEBREAK");
                break;

            default:
                Debug.LogError("ERROR CON EL DEPORTE ACTUAL");
                break;
        }

        relojEntradaDatos.Initiate();
        GuardarComoPartido();
    }

    public override void TerminarSeleccionJugadores(List<Jugador> _listaJugadores, int cantSeleccionados)
    {
        //controlar que cantSeleccionados sea al menos 1. En caso contrario mostar mensaje de error y no continuar

        AppController.instance.pantallaCarga.Activar();
        Debug.Log("PANTALLA DE CARGA");

        SetPublicity();

        seleccionListaJugadores.gameObject.SetActive(false);
        overlayEntradaDatos.SetActive(false);

        jugadoresSeleccionados = _listaJugadores;

        //Screen.orientation = ScreenOrientation.Landscape;

        CanvasController.instance.overlayPanel.gameObject.SetActive(false);
        CanvasController.instance.botonDespliegueMenu.SetActive(false);

        Debug.Log("CANT EST SEL: " + listaEstadisticas.Count);

        seccionBanca.SetSeccionBanca(jugadoresSeleccionados);
        seccionCancha.SetSeccionCancha();
        seccionEstadisticas.SetSeccionEstadisticas(listaEstadisticas, listaNombres, listaIniciales);

        //AppController.instance.pantallaCarga.Desactivar();

        seccionEstadisticas.SetJugadorEntradaDatoFocus(seccionBanca.GetJugadorEntradaDatoInicial());
    }

    #region Guardado de la entrada de datos
    public void GuardarComoPartido()
    {
        isPartido = true;
        imagenSeleccion.sprite = botonGuardarPartidoSprite;
        toggleResultado.gameObject.SetActive(false);
        insertarResultado = true;
        overlayInputsResultado.SetActive(false);
    }

    public void GuardarComoPractica()
    {
        isPartido = false;
        imagenSeleccion.sprite = botonGuardarPracticaSprite;
        toggleResultado.gameObject.SetActive(true);
        toggleResultado.isOn = true;
        insertarResultado = true;
        overlayInputsResultado.SetActive(false);
    }

    public void ToggleInsertarResultado()
    {
        insertarResultado = !insertarResultado;
        overlayInputsResultado.SetActive(!insertarResultado);
    }

    override public void GuardarEntradaDatos()
    {
        string tipoEntradaDatos = isPartido ? "Partido" : "Practica";

        Debug.Log("Guardando como: " + tipoEntradaDatos);

        mensajeErrorGuardado.gameObject.SetActive(true);

        string nombrePartido = nombrePartidoText.text.ToUpper();
        Equipo equipo = AppController.instance.equipoActual;

        if (nombrePartido == "" || !AppController.instance.VerificarNombre(nombrePartido))
        {
            mensajeErrorGuardado.SetText("Nombre inválido".ToUpper(), AppController.Idiomas.Español);
            mensajeErrorGuardado.SetText("Invalid name".ToUpper(), AppController.Idiomas.Ingles);
            mensajeErrorGuardado.Activar();
            Debug.Log("Nombre inválido");
            return;
        }
        else if (equipo.ContienePartido(tipoEntradaDatos, nombrePartido))
        {
            mensajeErrorGuardado.SetText("Nombre existente".ToUpper(), AppController.Idiomas.Español);
            mensajeErrorGuardado.SetText("Existing name".ToUpper(), AppController.Idiomas.Ingles);
            mensajeErrorGuardado.Activar();
            Debug.Log("Nombre existente");
            return;
        }

        Deportes.DeporteEnum deporteActual = equipo.GetDeporte();
        Estadisticas estEquipo = new Estadisticas(deporteActual);
        DateTime fecha = DateTime.Now;
        int cantPeriodos = relojEntradaDatos.GetCurrentPeriod();

        Partido _partido = new Partido(nombrePartido, estEquipo, fecha, isPartido, cantPeriodos);
        List<Evento> _eventos = seccionEstadisticas.GetListaEventos();
        _partido.SetListaEventos(_eventos);

        if (deporteActual == Deportes.DeporteEnum.Softball || deporteActual == Deportes.DeporteEnum.Futbol || deporteActual == Deportes.DeporteEnum.Basket || deporteActual == Deportes.DeporteEnum.HockeyCesped || deporteActual == Deportes.DeporteEnum.HockeyPatines || deporteActual == Deportes.DeporteEnum.Rugby || deporteActual == Deportes.DeporteEnum.Handball)
        {
            if (insertarResultado && !resultadoNormal.VerificarInputs())
            {
                mensajeErrorGuardado.SetText("Completar campos de resultado".ToUpper(), AppController.Idiomas.Español);
                mensajeErrorGuardado.SetText("Complete results fields".ToUpper(), AppController.Idiomas.Ingles);
                mensajeErrorGuardado.Activar();
                return;
            }
            resultadoNormal.SetResultado();
            Debug.Log("RES: " + resultadoNormal.GetResultado());
            Debug.Log("PROP: " + resultadoNormal.GetResultadoPropio());
            Debug.Log("CONT: " + resultadoNormal.GetResultadoContrario());
            Debug.Log("PEN PROP: " + resultadoNormal.GetResultadoPenalesPropio());
            Debug.Log("PEN CONT: " + resultadoNormal.GetResultadoPenalesContrario());
            _partido.AgregarResultadoEntradaDatos(resultadoNormal, Partido.TipoResultadoPartido.Normal);
            seccionBanca.GuardarEntradaDato(listaNombres, nombrePartido, tipoEntradaDatos, fecha, resultadoNormal, _eventos, Partido.TipoResultadoPartido.Normal, cantPeriodos);
        }
        else
        {
            if (insertarResultado && !resultadoSets.VerificarInputs())
            {
                mensajeErrorGuardado.SetText("Completar campos de resultado".ToUpper(), AppController.Idiomas.Español);
                mensajeErrorGuardado.SetText("Complete results fields".ToUpper(), AppController.Idiomas.Ingles);
                mensajeErrorGuardado.Activar();
                return;
            }
            resultadoSets.SetResultado();
            _partido.AgregarResultadoEntradaDatos(resultadoSets, Partido.TipoResultadoPartido.Sets);
            seccionBanca.GuardarEntradaDato(listaNombres, nombrePartido, tipoEntradaDatos, fecha, resultadoSets, _eventos, Partido.TipoResultadoPartido.Sets, cantPeriodos);
        }


        //PARA LOS JUGADORES
        seccionBanca.SetFechaEntradaDato(fecha); //para cada jugadorEntradaDato, estadistica.setfecha(fecha)
        
        //PARA EL EQUIPO
        estEquipo.SetFecha(fecha);
        seccionBanca.AgregarEstadisticasEquipo(estEquipo);
        equipo.GuardarEntradaDato(tipoEntradaDatos, estEquipo, _partido);



        //CanvasController.instance.escenas.Add(1);
        CanvasController.instance.retrocesoPausado = false;
        CanvasController.instance.MostrarPanelAnterior();
        CanvasController.instance.overlayPanel.gameObject.SetActive(true);
        CanvasController.instance.botonDespliegueMenu.SetActive(true);
        Screen.orientation = ScreenOrientation.Portrait;
        Destroy(gameObject);
    }

    public override void DescartarDatos()
    {
        base.DescartarDatos();
        CanvasController.instance.botonDespliegueMenu.SetActive(true);
        CanvasController.instance.overlayPanel.gameObject.SetActive(true);
        Destroy(gameObject);
    }

    public override void CancelarGuardado()
    {
        panelConfirmacionGuardado.Cerrar();
    }
    #endregion
}