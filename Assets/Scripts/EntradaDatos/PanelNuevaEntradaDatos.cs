﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelNuevaEntradaDatos : EntradaDatos
{
    [SerializeField] private GameObject mensajeError = null;

    //[SerializeField] private GameObject seccionNombrePartido = null;
    [SerializeField] private Text nombrePartidoText = null;

    [SerializeField] private MensajeDesplegable panelConfirmacionGuardado = null;

    [SerializeField] private SeleccionListaJugadores panelSeleccionJugadores = null;

    //[SerializeField] private GameObject entradaDatosJugadorPrefab = null;
    [SerializeField] private GameObject textPrefab = null;
    [SerializeField] private GameObject columnaPrefab = null;
    [SerializeField] private GameObject columnaNombresPrefab = null;
    [SerializeField] private GameObject botonPrefab = null;

    [SerializeField] private RectTransform estadisticasTransform = null;

    private Equipo equipo;
    private List<Jugador> jugadores;

    private List<GameObject> listaEntradaDatosPrefab;
    private List<EntradaDatosJugador> listaEntradaDatos;
    private List<GameObject> columnas;

    [SerializeField] private Transform parentColumna = null;

    private PanelEntradaDatos panelEntradaDatos;

    private bool isPartido = true;

    private List<string> listaEstadisticas;
    private List<string> listaIniciales;

    private void Awake()
    {
        jugadores = new List<Jugador>();
        listaEntradaDatosPrefab = new List<GameObject>();
        listaEntradaDatos = new List<EntradaDatosJugador>();
        panelEntradaDatos = GetComponentInParent<PanelEntradaDatos>();   
        listaEstadisticas = PanelSeleccionEstadisticas.instance.GetListaEstadisticas();
        listaIniciales = PanelSeleccionEstadisticas.instance.GetListaInicialesEstadisticas();

        //gameObject.GetComponent<RawImage>().texture = AppController.instance.GetComponent<Test>().myGradient.GetTexture(1280);
    }

    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Escape))
        {
            panelConfirmacionGuardado.ToggleDesplegar();
            //seccionNombrePartido.SetActive(!seccionNombrePartido.activeSelf);
        }*/

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (panelSeleccionJugadores.gameObject.activeSelf)
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
    }

    public override void Display(bool _isPartido)
    {
        isPartido = _isPartido;

        gameObject.GetComponent<RawImage>().texture = AppController.instance.GetTextureActual();

        //parentColumna = transform;

        columnas = new List<GameObject>();
        equipo = AppController.instance.GetEquipoActual();
        //jugadores = equipo.GetJugadores();

        //CanvasController.instance.botonDespliegueMenu.SetActive(false);

        //seccionNombrePartido.SetActive(false);

        panelSeleccionJugadores.SetearListaJugadores(false);
        AppController.instance.overlayPanel.SetNombrePanel("JUGADORES");

        mensajeError.SetActive(false); 
    }

    public override void TerminarSeleccionJugadores(List<Jugador> listaJugadores)
    {
        Screen.orientation = ScreenOrientation.Landscape;

        CanvasController.instance.botonDespliegueMenu.SetActive(false);

        jugadores = listaJugadores;
        panelSeleccionJugadores.gameObject.SetActive(false);
        AppController.instance.overlayPanel.SetNombrePanel("");

        AppController.instance.ChangeTexture(-1);
        gameObject.GetComponent<RawImage>().texture = AppController.instance.GetTextureActual();

        BorrarPrefabs();
        CrearColumnas();  
    }

    public void CrearColumnas()
    {
        GameObject columnaNombresGO = Instantiate(columnaNombresPrefab, transform, false);
        columnas.Add(columnaNombresGO);

        //columnaNombresGO.transform.position = new Vector2(100f, 360f);

        GameObject textGO = Instantiate(textPrefab, columnaNombresGO.transform, false);
        textGO.GetComponent<Text>().text = "Nombre";

        for (int j = 0; j < jugadores.Count; j++)
        {
            GameObject textJugadorGO = Instantiate(textPrefab, columnaNombresGO.transform, false);
            textJugadorGO.GetComponent<Text>().text = jugadores[j].GetNombre();
        }

        for (int i = 0; i < listaEstadisticas.Count; i++)
        {
            GameObject columnaGO = Instantiate(columnaPrefab, parentColumna, false);
            columnas.Add(columnaGO);

            GameObject textCategoriaGO = Instantiate(textPrefab, columnaGO.transform, false);
            textCategoriaGO.GetComponent<Text>().text = listaIniciales[i];//listaEstadisticas[i];

            for (int j = 0; j < jugadores.Count; j++)
            {
                GameObject botonGO = Instantiate(botonPrefab, columnaGO.transform, false);
            }
        }
    
        estadisticasTransform.sizeDelta = new Vector2(columnas.Count*120, 720);

        if(estadisticasTransform.sizeDelta.x < 1080)
        {
            estadisticasTransform.sizeDelta = new Vector2(1080, 720);
        }

        panelConfirmacionGuardado.transform.SetAsLastSibling();
    }

    public void BorrarPrefabs()
    {
        //Borrar columnas
        if (columnas == null) Debug.Log("null");
        foreach (var columna in columnas)
        {
            Destroy(columna);
        }
        columnas.Clear();
    }

    override public void GuardarEntradaDatos()
    {
        string tipoEntradaDatos = isPartido ? "Partido" : "Practica";

        if (nombrePartidoText.text == "")
        {
            mensajeError.SetActive(true);
            mensajeError.GetComponentInChildren<Text>().text = "Nombre Inválido!";
            return;
        }
        else if(equipo.ContienePartido(tipoEntradaDatos, nombrePartidoText.text))
        {
            mensajeError.SetActive(true);
            mensajeError.GetComponentInChildren<Text>().text = "Nombre Existente!";
            return;
        }

        Estadisticas estEquipo = new Estadisticas();

        for (int i = 0; i < jugadores.Count; i++)
        {
            Estadisticas estadistica = new Estadisticas();

            for (int j = 0; j < listaEstadisticas.Count; j++)
            {
                Button[] botones = columnas[j + 1].GetComponentsInChildren<Button>();
                estadistica.AgregarEstadisticas(listaEstadisticas[j], botones[i].GetComponent<BotonEntradaDato>().GetCantidad()); 
            }

            estEquipo.AgregarEstadisticas(estadistica);
            //jugadores[i].SetEstadisticas(estadistica, tipoEntradaDatos);
            //jugadores[i].AgregarPartido(new Partido(nombrePartidoText.text, estadistica), tipoEntradaDatos);
            jugadores[i].GuardarEntradaDato(tipoEntradaDatos, estadistica, new Partido(nombrePartidoText.text, estadistica));
        }

        //equipo.SetEstadisticas(estEquipo, tipoEntradaDatos);
        //equipo.AgregarPartido(new Partido(nombrePartidoText.text, estEquipo), tipoEntradaDatos);
        equipo.GuardarEntradaDato(tipoEntradaDatos, estEquipo, new Partido(nombrePartidoText.text, estEquipo));

        CanvasController.instance.escenas.Add(1);
        CanvasController.instance.retrocesoPausado = false;
        CanvasController.instance.MostrarPanelAnterior();
        Destroy(gameObject);
        CanvasController.instance.botonDespliegueMenu.SetActive(true);
    }

    public override void DescartarDatos()
    {
        base.DescartarDatos();
        CanvasController.instance.botonDespliegueMenu.SetActive(true);
        Destroy(gameObject);
    }

    public override void CancelarGuardado()
    {
        panelConfirmacionGuardado.Cerrar();
    }
}
