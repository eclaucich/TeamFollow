using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Threading;

public class PanelEstadisticasGlobalesEquipo : Panel {

    //[SerializeField] private Text nombreEquipoText = null;
    [SerializeField] private GameObject botonEstadisticaPrefab = null;
    [SerializeField] private Transform transformParent = null;

    [SerializeField] private EstadisticasGlobalesEquipo estadisticasGlobales = null;
    [SerializeField] private GameObject estadisticaPrefab = null;
    [SerializeField] private ConfirmacionBorradoPartido confirmacionBorradoPartido = null;

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
        resultadoNormal.gameObject.SetActive(false);
        resultadoSets.gameObject.SetActive(false);

        equipo = AppController.instance.equipoActual;
        if (equipo == null) Debug.Log("EQUIPO NULO");

        AppController.instance.overlayPanel.SetNombrePanel(equipo.GetNombre() + ": ESTADISTICAS GLOBALES", AppController.Idiomas.Español);
        AppController.instance.overlayPanel.SetNombrePanel(equipo.GetNombre() + ": GLOBAL STATISTICS", AppController.Idiomas.Ingles);

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

        parentTransform = estadisticasGlobales.SetPanelEstadisticas();

        BorrarPrefabs();
        CrearPrefabs();
    }

    public void SetPanelEstadisticasGlobalesEquipo(Partido _partido, bool fromGrafica=false)
    {
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


        AppController.instance.overlayPanel.SetNombrePanel("PARTIDO: " + _partido.GetNombre(), AppController.Idiomas.Español);
        AppController.instance.overlayPanel.SetNombrePanel("MATCH: " + _partido.GetNombre(), AppController.Idiomas.Ingles);

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

        foreach (var est in estadisticas.GetDictionary())
        {
            Debug.Log(est.Key + ": " + est.Value);
        }

        for (int i = 0; i < estDeporte.GetSize(); i++) //este cantidad categorias en realidad devuelve la cantidad que haya en el enum de estadisticas
        {
            if (estadisticas.Find(estDeporte.GetValueAtIndex(i))[0]==1)
            {
                GameObject botonEstadisticaGO = Instantiate(botonEstadisticaPrefab, transformParent, false);
                botonEstadisticaGO.SetActive(true);
                BotonEstadistica botonEstadistica = botonEstadisticaGO.GetComponent<BotonEstadistica>();

                string statsName = estDeporte.GetStatisticsName(i, AppController.Idiomas.Español)[0];

                botonEstadistica.SetTextInLanguage(statsName, AppController.Idiomas.Español);
                botonEstadistica.SetTextInLanguage(estDeporte.GetStatisticsName(i, AppController.Idiomas.Ingles)[0], AppController.Idiomas.Ingles);
                botonEstadistica.SetValorEstadistica(estadisticas.Find(statsName.Replace(" ", string.Empty))[1].ToString());
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
