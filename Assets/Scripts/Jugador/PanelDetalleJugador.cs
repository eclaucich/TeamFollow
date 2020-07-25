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
            estadisticas = jugador.GetEstadisticasPartido();
            tipoEstadisticasText.text = "Partido";
        }
        else
        {
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
        for (int i = 0; i < estadisticas.GetCantidadCategorias(); i++)
        {
            GameObject botonEstadisticaGO = Instantiate(botonEstadisticaPrefab, transformParent, false);
            botonEstadisticaGO.SetActive(true);

            BotonEstadistica botonEstadistica = botonEstadisticaGO.GetComponent<BotonEstadistica>();
            botonEstadistica.SetNombreEstadistica(estadisticas.GetKeyAtIndex(i));
            botonEstadistica.SetValorEstadistica(estadisticas.GetValueAtIndex(i).ToString());

            listaPrefabsTextos.Add(botonEstadisticaGO);
        }
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
