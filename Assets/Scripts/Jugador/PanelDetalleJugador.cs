using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

/// <summary>
/// 
/// Panel que muestra todos los detalles de un jugador
/// 
/// </summary>

public class PanelDetalleJugador : Panel{

    [SerializeField] private GraficaResumen graficaResumen = null;

    [SerializeField] private GameObject botonVerResumen = null;
    [SerializeField] private GameObject estadisticaPrefab = null;
    [SerializeField] private GameObject botonEstadisticaPrefab = null;
    [SerializeField] private Transform transformParent = null;

    [SerializeField] private ConfirmacionBorradoPartido confirmacionBorradoPartido = null;

    protected Transform parentTransform;

    protected Estadisticas estadisticas;
    protected Jugador jugador;                                                               
    
    private bool isPartido = true;

    protected List<GameObject> listaPrefabsTextos;

    [SerializeField] private EstadisticasJugador panelEstadisticas = null;
    [SerializeField] private GameObject botonBorrar = null;

    [SerializeField] private GameObject seccionResultado = null;
    [SerializeField] private ResultadoNormal resultadoNormal = null;
    [SerializeField] private ResultadoSets resultadoSets = null;
    [SerializeField] private TextLanguage resultadoText = null;
    [SerializeField] private Text posicionText = null;

    private Partido partidoFocus;

    private List<Color> coloresBotones;

    private void Awake()
    {
        coloresBotones = new List<Color>();
        coloresBotones.Clear();
        coloresBotones.Add(AppController.instance.colorTheme.detalle5);
        coloresBotones.Add(AppController.instance.colorTheme.detalle3);
    }

    public void SetDetallesJugador(Partido _partido, string nombreJugador, Estadisticas _estadisticas)
    {
        partidoFocus = _partido;
        ResultadoEntradaDatos.Resultado _tipoResultado;

        if (_partido != null)
        {
            if (partidoFocus.GetTipoResultadoPartido() == Partido.TipoResultadoPartido.Normal)
            {
                resultadoSets.gameObject.SetActive(false);
                resultadoNormal.gameObject.SetActive(true);

                ResultadoNormal _res = (ResultadoNormal)partidoFocus.GetResultadoEntradaDato();

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

            if (isPartido)
            {
                CanvasController.instance.overlayPanel.SetNombrePanel("PARTIDO: " + partidoFocus.GetNombre(), AppController.Idiomas.Español);
                CanvasController.instance.overlayPanel.SetNombrePanel("MATCH: " + partidoFocus.GetNombre(), AppController.Idiomas.Ingles);
            }
            else
            {
                CanvasController.instance.overlayPanel.SetNombrePanel("PRACTICA: " + partidoFocus.GetNombre(), AppController.Idiomas.Español);
                CanvasController.instance.overlayPanel.SetNombrePanel("PRACTICE: " + partidoFocus.GetNombre(), AppController.Idiomas.Ingles);
            }
        }

        if (_partido == null)
        {
            botonVerResumen.SetActive(false);
            botonBorrar.SetActive(false);
            seccionResultado.SetActive(false);
        }
        else
        {
            botonVerResumen.SetActive(true);
            botonBorrar.SetActive(true);
            seccionResultado.SetActive(true);
        }

        if (listaPrefabsTextos == null) listaPrefabsTextos = new List<GameObject>();
        jugador = AppController.instance.equipoActual.BuscarPorNombre(nombreJugador);

        estadisticas = _estadisticas;

        parentTransform = panelEstadisticas.GetPanelEstadisticaTransform();

        if (partidoFocus == null)
            posicionText.text = string.Empty;
        else
            posicionText.text = partidoFocus.GetPosicion();

        BorrarPrefabs();
        CrearPrefabs();
    }

    public void SetDetallesEstadisticas()
    {
        if (isPartido)
        {
            CanvasController.instance.overlayPanel.SetNombrePanel("PARTIDO: " + partidoFocus.GetNombre(), AppController.Idiomas.Español);
            CanvasController.instance.overlayPanel.SetNombrePanel("MATCH: " + partidoFocus.GetNombre(), AppController.Idiomas.Ingles);

            estadisticas = jugador.GetEstadisticasPartido();
        }
        else
        {
            CanvasController.instance.overlayPanel.SetNombrePanel("PRACTICA: " + partidoFocus.GetNombre(), AppController.Idiomas.Español);
            CanvasController.instance.overlayPanel.SetNombrePanel("PRACTICE: " + partidoFocus.GetNombre(), AppController.Idiomas.Ingles);

            estadisticas = jugador.GetEstadisticasPractica();
        }

        parentTransform = panelEstadisticas.GetPanelEstadisticaTransform();

        BorrarPrefabs();
        CrearPrefabs();
    }


    public void VerGraficaResumen()
    {
        graficaResumen.SetGraficaResumen(partidoFocus, jugador);
    }

    public void BorrarPrefabs()
    {
        for (int i = 0; i < listaPrefabsTextos.Count; i++)
        {
            Destroy(listaPrefabsTextos[i]);
        }
        listaPrefabsTextos.Clear();
    }

    virtual public void CrearPrefabs()
    {
        EstadisticaDeporte estDeporte = estadisticas.GetEstadisticaDeporte();
        Array listaTipoEstadisticas = estDeporte.GetEstadisticas();

        int idxColor = 0;

        for (int i = 0; i < estDeporte.GetSize(); i++) //este cantidad categorias en realidad devuelve la cantidad que haya en el enum de estadisticas
        {
            if (estadisticas.Find(estDeporte.GetValueAtIndex(i))[0] == 1)
            {
                GameObject botonEstadisticaGO = Instantiate(botonEstadisticaPrefab, transformParent, false);
                botonEstadisticaGO.SetActive(true);
                BotonEstadistica botonEstadistica = botonEstadisticaGO.GetComponent<BotonEstadistica>();

                string statsNameEspañol = EstadisticasDeporteDisplay.GetStatisticsName((EstadisticaDeporte.Estadisticas)listaTipoEstadisticas.GetValue(i), AppController.Idiomas.Español)[0]; //estDeporte.GetStatisticsName(i, AppController.Idiomas.Español)[0];
                string statsNameIngles = EstadisticasDeporteDisplay.GetStatisticsName((EstadisticaDeporte.Estadisticas)listaTipoEstadisticas.GetValue(i), AppController.Idiomas.Ingles)[0];

                botonEstadistica.SetTextInLanguage(statsNameEspañol, AppController.Idiomas.Español);
                botonEstadistica.SetTextInLanguage(statsNameIngles, AppController.Idiomas.Ingles);
                botonEstadistica.SetValorEstadistica(estadisticas.Find(statsNameEspañol.Replace(" ", string.Empty))[1].ToString());
                botonEstadistica.SetColor(coloresBotones[idxColor % coloresBotones.Count]);
                listaPrefabsTextos.Add(botonEstadisticaGO);
                idxColor++;
            }
        }
        /*
        for (int i = 0; i < estadisticas.GetCantidadCategorias(); i++)
        {
            GameObject botonEstadisticaGO = Instantiate(botonEstadisticaPrefab, transformParent, false);
            botonEstadisticaGO.SetActive(true);

            BotonEstadistica botonEstadistica = botonEstadisticaGO.GetComponent<BotonEstadistica>();
            botonEstadistica.SetNombreEstadistica(estadisticas.GetKeyAtIndex(i));
            botonEstadistica.SetValorEstadistica(estadisticas.GetValueAtIndex(i).ToString());

            listaPrefabsTextos.Add(botonEstadisticaGO);
        }*/
    }

    public void CambiarTipoEstadisticas()
    {
        isPartido = !isPartido;
        SetDetallesEstadisticas();
    }

    public void ActivarPanelConfirmacionBorradoPartido()
    {
        confirmacionBorradoPartido.ActivarPanel(partidoFocus);
    }

    public bool GetIsPartido()
    {
        return isPartido;
    }
}
