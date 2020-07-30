using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

/// <summary>
/// 
/// Panel que muestra todos los detalles de un jugador
/// 
/// </summary>

public class PanelDetalleJugador : Panel{

    [SerializeField] private GameObject estadisticaPrefab = null;
    [SerializeField] private GameObject botonEstadisticaPrefab = null;
    [SerializeField] private Transform transformParent = null;

    [SerializeField] private Text tipoEstadisticasText = null;

    [SerializeField] private ConfirmacionBorradoPartido confirmacionBorradoPartido = null;

    protected Transform parentTransform;

    protected Estadisticas estadisticas;
    protected Jugador jugador;                                                               
    
    private bool isPartido = true;

    protected List<GameObject> listaPrefabsTextos;

    [SerializeField] private EstadisticasJugador panelEstadisticas = null;
    [SerializeField] private GameObject botonBorrar = null;

    private Partido partidoFocus;

    public void SetDetallesJugador(Partido _partido, string nombreJugador, Estadisticas _estadisticas)
    {
        partidoFocus = _partido;

        if (_partido != null)
        {
            if (isPartido)
            {
                AppController.instance.overlayPanel.SetNombrePanel("PARTIDO: " + partidoFocus.GetNombre(), AppController.Idiomas.Español);
                AppController.instance.overlayPanel.SetNombrePanel("MATCH: " + partidoFocus.GetNombre(), AppController.Idiomas.Ingles);
            }
            else
            {
                AppController.instance.overlayPanel.SetNombrePanel("PRACTICA: " + partidoFocus.GetNombre(), AppController.Idiomas.Español);
                AppController.instance.overlayPanel.SetNombrePanel("PRACTICE: " + partidoFocus.GetNombre(), AppController.Idiomas.Ingles);
            }
        }

        if (_partido == null) botonBorrar.SetActive(false); else botonBorrar.SetActive(true); 

        if (listaPrefabsTextos == null) listaPrefabsTextos = new List<GameObject>();
        jugador = AppController.instance.GetEquipoActual().BuscarPorNombre(nombreJugador);

        estadisticas = _estadisticas;

        parentTransform = panelEstadisticas.GetPanelEstadisticaTransform();

        BorrarPrefabs();
        CrearPrefabs();
    }

    public void SetDetallesEstadisticas()
    {
        if (isPartido)
        {
            AppController.instance.overlayPanel.SetNombrePanel("PARTIDO: " + partidoFocus.GetNombre(), AppController.Idiomas.Español);
            AppController.instance.overlayPanel.SetNombrePanel("MATCH: " + partidoFocus.GetNombre(), AppController.Idiomas.Ingles);

            estadisticas = jugador.GetEstadisticasPartido();
            tipoEstadisticasText.text = "Partido";
        }
        else
        {
            AppController.instance.overlayPanel.SetNombrePanel("PRACTICA: " + partidoFocus.GetNombre(), AppController.Idiomas.Español);
            AppController.instance.overlayPanel.SetNombrePanel("PRACTICE: " + partidoFocus.GetNombre(), AppController.Idiomas.Ingles);

            estadisticas = jugador.GetEstadisticasPractica();
            tipoEstadisticasText.text = "Practica";
        }

        parentTransform = panelEstadisticas.GetPanelEstadisticaTransform();

        BorrarPrefabs();
        CrearPrefabs();
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

        for (int i = 0; i < estDeporte.GetSize(); i++) //este cantidad categorias en realidad devuelve la cantidad que haya en el enum de estadisticas
        {
            if (estadisticas.Find(estDeporte.GetValueAtIndex(i))[0] == 1)
            {
                GameObject botonEstadisticaGO = Instantiate(botonEstadisticaPrefab, transformParent, false);
                botonEstadisticaGO.SetActive(true);
                BotonEstadistica botonEstadistica = botonEstadisticaGO.GetComponent<BotonEstadistica>();

                string statsName = estDeporte.GetStatisticsName(i, AppController.Idiomas.Español)[0];

                botonEstadistica.SetTextInLanguage(statsName, AppController.Idiomas.Español);
                botonEstadistica.SetTextInLanguage(estDeporte.GetStatisticsName(i, AppController.Idiomas.Ingles)[0], AppController.Idiomas.Ingles);
                botonEstadistica.SetValorEstadistica(estadisticas.GetValueAtIndex(i).ToString());
                listaPrefabsTextos.Add(botonEstadisticaGO);
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
