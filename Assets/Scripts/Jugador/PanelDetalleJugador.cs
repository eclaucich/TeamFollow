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
    
    private bool tipoEstadisticasPartido = true;

    protected List<GameObject> listaPrefabsTextos;

    [SerializeField] private EstadisticasJugador panelEstadisticas = null;
    [SerializeField] private GameObject botonBorrar = null;

    private BotonPartido botonFocus;

    public void SetDetallesJugador(BotonPartido botonPartido, string nombreJugador, Estadisticas _estadisticas)                     //Setear el panel
    {
        botonFocus = botonPartido;

        if (botonFocus == null) botonBorrar.SetActive(false); else botonBorrar.SetActive(true); 

        if (listaPrefabsTextos == null) listaPrefabsTextos = new List<GameObject>();
        jugador = AppController.instance.GetEquipoActual().BuscarPorNombre(nombreJugador);

        estadisticas = _estadisticas;

        parentTransform = panelEstadisticas.Set(botonPartido);
        BorrarPrefabs();
        CrearPrefabs();
    }

    public void SetDetallesEstadisticas()
    {
        if (tipoEstadisticasPartido)
        {
            estadisticas = jugador.GetEstadisticasPartido();
            tipoEstadisticasText.text = "Partido";
        }
        else
        {
            estadisticas = jugador.GetEstadisticasPractica();
            tipoEstadisticasText.text = "Practica";
        }

        parentTransform = panelEstadisticas.Set(null);

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
            //Debug.Log(estadisticas.GetKeyAtIndex(i) + "  " + estadisticas.GetValueAtIndex(i).ToString());
            /*GameObject estadisticaGO = Instantiate(estadisticaPrefab, parentTransform, false);
            Text[] textos = estadisticaGO.GetComponentsInChildren<Text>();
            textos[0].text = estadisticas.GetKeyAtIndex(i);
            textos[1].text = estadisticas.GetValueAtIndex(i).ToString();

            listaPrefabsTextos.Add(estadisticaGO);*/

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
        tipoEstadisticasPartido = !tipoEstadisticasPartido;
        SetDetallesEstadisticas();
    }

    public void ActivarPanelConfirmacionBorradoPartido()
    {
        confirmacionBorradoPartido.ActivarPanel(botonFocus);
    }
}
