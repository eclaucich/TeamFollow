using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Threading;
using System;
using System.IO.IsolatedStorage;

public class PanelEstadisticasGlobalesEquipo : Panel {

    //[SerializeField] private Text nombreEquipoText = null;
    [SerializeField] private GameObject botonEstadisticaPrefab = null;
    [SerializeField] private GameObject botonVerResultado = null;
    [SerializeField] private Transform transformParent = null;

    [SerializeField] private GraficaResumen graficaResumen = null;
    [SerializeField] private EstadisticasGlobalesEquipo estadisticasGlobales = null;
    [SerializeField] private GameObject estadisticaPrefab = null;
    [SerializeField] private ConfirmacionBorradoPartido confirmacionBorradoPartido = null;

    [SerializeField] private GameObject seccionResultado = null;
    [SerializeField] private ResultadoNormal resultadoNormal = null;
    [SerializeField] private ResultadoSets resultadoSets = null;
    [SerializeField] private TextLanguage resultadoText = null;

    protected Dictionary<string, int> diccEstadisticas;

    private Estadisticas estadisticas;
    private Equipo equipo;

    //private bool tipoEstadisticasPartido = true;                                       //true="Partido", false="Practica"
 
    private Transform parentTransform;
    private List<GameObject> listaPrefabsTextos;

    private Partido partidoFocus;

    [SerializeField] private GameObject botonBorrar;

    private void Start()
    {
        diccEstadisticas = new Dictionary<string, int>();
    }


    public void SetPanelEstadisticasGlobalesEquipo(/*BotonPartido _botonPartido, */Estadisticas _estadisticas)
    {
        botonVerResultado.SetActive(false);
        resultadoNormal.gameObject.SetActive(false);
        resultadoSets.gameObject.SetActive(false);

        equipo = AppController.instance.equipoActual;
        if (equipo == null) Debug.Log("EQUIPO NULO");

        CanvasController.instance.overlayPanel.SetNombrePanel(equipo.GetNombre() + ": ESTADISTICAS GLOBALES", AppController.Idiomas.Español);
        CanvasController.instance.overlayPanel.SetNombrePanel(equipo.GetNombre() + ": GLOBAL STATISTICS", AppController.Idiomas.Ingles);

        /*if (_botonPartido == null) */
        botonBorrar.SetActive(false);// else botonBorrar.SetActive(true);
        //botonFocus = _botonPartido;

        CanvasController.instance.AgregarPanelAnterior(CanvasController.Paneles.EstadisticasGlobalesEquipo);

        if (listaPrefabsTextos == null) listaPrefabsTextos = new List<GameObject>();


        //nombreEquipoText.text = equipo.GetNombre();

        estadisticas = _estadisticas;

        //Debug.Log("ESTADISTICAS: " + _estadisticas.GetDictionary() == null);

        /*diccEstadisticas = new Dictionary<string, int>();
        diccEstadisticas = estadisticas.GetDictionary();

        Debug.Log("DICC ESTADISTICAS: " + diccEstadisticas == null);
        */
        seccionResultado.SetActive(false);
        parentTransform = estadisticasGlobales.SetPanelEstadisticas();

        BorrarPrefabs();
        CrearPrefabs();
    }

    public void SetPanelEstadisticasGlobalesEquipo(Partido _partido, bool fromGrafica=false)
    {
        botonVerResultado.SetActive(true);

        partidoFocus = _partido;
        ResultadoEntradaDatos.Resultado _tipoResultado;

        if (partidoFocus.GetTipoResultadoPartido() == Partido.TipoResultadoPartido.Normal)
        {
            resultadoSets.gameObject.SetActive(false);
            resultadoNormal.gameObject.SetActive(true);

            ResultadoNormal _res = (ResultadoNormal)partidoFocus.GetResultadoEntradaDato();
            Debug.Log(_res == null);
            resultadoNormal.CopyDataFrom(_res);
            resultadoNormal.DisableEdition();
            _tipoResultado = resultadoNormal.GetResultado();
        }
        else
        {
            resultadoSets.gameObject.SetActive(true);
            resultadoNormal.gameObject.SetActive(false);

            ResultadoSets _res = (ResultadoSets)partidoFocus.GetResultadoEntradaDato();
            List<SetPrefab> _lista = _res.GetListaSets();

            resultadoSets.BorrarPrefabs();
            foreach (var set in _lista)
            {
                resultadoSets.AgregarSet(set);
            }
            resultadoSets.DisableEdition();
            _tipoResultado = resultadoSets.GetResultado();
        }
        if (_tipoResultado == ResultadoEntradaDatos.Resultado.Victoria)
        {
            resultadoText.SetText("Victoria", AppController.Idiomas.Español);
            resultadoText.SetText("Victory", AppController.Idiomas.Ingles);
        }
        else if (_tipoResultado == ResultadoEntradaDatos.Resultado.Derrota)
        {
            resultadoText.SetText("Derrota", AppController.Idiomas.Español);
            resultadoText.SetText("Loss", AppController.Idiomas.Ingles);
        }
        else
        {
            resultadoText.SetText("Empate", AppController.Idiomas.Español);
            resultadoText.SetText("Tie", AppController.Idiomas.Ingles);
        }


        CanvasController.instance.overlayPanel.SetNombrePanel("PARTIDO: " + _partido.GetNombre(), AppController.Idiomas.Español);
        CanvasController.instance.overlayPanel.SetNombrePanel("MATCH: " + _partido.GetNombre(), AppController.Idiomas.Ingles);

        seccionResultado.SetActive(true);
        botonBorrar.SetActive(true);

        Screen.orientation = ScreenOrientation.Portrait;

        if (!fromGrafica) CanvasController.instance.AgregarPanelAnterior(CanvasController.Paneles.EstadisticasGlobalesEquipo);

        if (listaPrefabsTextos == null) listaPrefabsTextos = new List<GameObject>();

        estadisticas = _partido.GetEstadisticas();

        parentTransform = estadisticasGlobales.SetPanelEstadisticas();

        BorrarPrefabs();
        CrearPrefabs();
    }

    virtual public void CrearPrefabs()
    {
        EstadisticaDeporte estDeporte = estadisticas.GetEstadisticaDeporte();

        Array listaTipoEstadisticas = estDeporte.GetEstadisticas();

        for (int i = 0; i < estDeporte.GetSize(); i++) //este cantidad categorias en realidad devuelve la cantidad que haya en el enum de estadisticas
        {
            if (estadisticas.Find(estDeporte.GetValueAtIndex(i))[0]==1)
            {
                GameObject botonEstadisticaGO = Instantiate(botonEstadisticaPrefab, transformParent, false);
                botonEstadisticaGO.SetActive(true);
                BotonEstadistica botonEstadistica = botonEstadisticaGO.GetComponent<BotonEstadistica>();

                string statsNameEspañol = EstadisticasDeporteDisplay.GetStatisticsName((EstadisticaDeporte.Estadisticas)listaTipoEstadisticas.GetValue(i), AppController.Idiomas.Español)[0]; //estDeporte.GetStatisticsName(i, AppController.Idiomas.Español)[0];
                string statsNameIngles = EstadisticasDeporteDisplay.GetStatisticsName((EstadisticaDeporte.Estadisticas)listaTipoEstadisticas.GetValue(i), AppController.Idiomas.Ingles)[0];

                botonEstadistica.SetTextInLanguage(statsNameEspañol, AppController.Idiomas.Español);
                botonEstadistica.SetTextInLanguage(statsNameIngles, AppController.Idiomas.Ingles);
                botonEstadistica.SetValorEstadistica(estadisticas.Find(statsNameEspañol.Replace(" ", string.Empty))[1].ToString());
                listaPrefabsTextos.Add(botonEstadisticaGO);
            }
        }
        /*for (int i = 0; i < estadisticas.GetCantidadCategorias(); i++)
        {            
            GameObject botonEstadisticaGO = Instantiate(botonEstadisticaPrefab, transformParent, false);
            botonEstadisticaGO.SetActive(true);
            BotonEstadistica botonEstadistica = botonEstadisticaGO.GetComponent<BotonEstadistica>();
            botonEstadistica.SetNombreEstadistica(estadisticas.GetKeyAtIndex(i));
            botonEstadistica.SetValorEstadistica(estadisticas.GetValueAtIndex(i).ToString());
            listaPrefabsTextos.Add(botonEstadisticaGO);
        }*/
    }

    public void VerResumenPartido()
    {
        graficaResumen.SetGraficaResumen(partidoFocus);
        CanvasController.instance.AgregarPanelAnterior(CanvasController.Paneles.EstadisticasGlobalesEquipo);
    }

    public void BorrarPrefabs()
    {
        for (int i = 0; i < listaPrefabsTextos.Count; i++)
        {
            Destroy(listaPrefabsTextos[i]);
        }
        listaPrefabsTextos.Clear();
    }

    public void ActivarPanelConfirmacionBorrado()
    {
        confirmacionBorradoPartido.ActivarPanel(partidoFocus);
    }
}
