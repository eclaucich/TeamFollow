using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

/// <summary>
/// 
/// Panel que muestra todos los detalles de un jugador
/// 
/// </summary>

public class PanelDetalleJugador : Panel{

    //Campos a mostrar
    [SerializeField] private Text nombreJugadorText = null;
    [SerializeField] private GameObject estadisticaPrefab = null;

    [SerializeField] private Text tipoEstadisticasText = null;

    protected Transform parentTransform;

    protected Estadisticas estadisticas;
    protected Jugador jugador;                                                                //Jugador enfocado
    
    private bool tipoEstadisticasPartido = true;

    protected List<GameObject> listaPrefabsTextos;

    [SerializeField] private EstadisticasJugador panelEstadisticas = null;


    public void SetDetallesJugador(string nombreJugador, Estadisticas _estadisticas)                     //Setear el panel
    {
        if (listaPrefabsTextos == null) listaPrefabsTextos = new List<GameObject>();
        jugador = AppController.instance.GetEquipoActual().BuscarPorNombre(nombreJugador);
        nombreJugadorText.text = nombreJugador;

        estadisticas = _estadisticas;

        parentTransform = panelEstadisticas.Set();
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

        parentTransform = panelEstadisticas.Set();

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
            GameObject estadisticaGO = Instantiate(estadisticaPrefab, parentTransform, false);
            Text[] textos = estadisticaGO.GetComponentsInChildren<Text>();
            textos[0].text = estadisticas.GetKeyAtIndex(i);
            textos[1].text = estadisticas.GetValueAtIndex(i).ToString();

            listaPrefabsTextos.Add(estadisticaGO);
        }
    }


    public void CambiarTipoEstadisticas()
    {
        tipoEstadisticasPartido = !tipoEstadisticasPartido;
        SetDetallesEstadisticas();
    }
}
