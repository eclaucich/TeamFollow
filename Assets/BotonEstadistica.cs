using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotonEstadistica : MonoBehaviour
{
    [SerializeField] private PanelPartidosEquipo panelPartidosEquipo = null;
    [SerializeField] private PanelDetalleEquipo panelDetalleEquipo = null;
    [SerializeField] private PanelDetalleJugador panelDetalleJugador = null;

    [SerializeField] private Text nombreEstadisticaText = null;
    [SerializeField] private Text valorEstadisticaText = null;

    [SerializeField] private bool desdeEquipo = true; //En los botones que estan en la parte de las estadisticas del equipo estrue, en los de la parte de los jugadores es false

    public void SetNombreEstadistica(string _nombre)
    {
        nombreEstadisticaText.text = _nombre;
    }

    public void SetValorEstadistica(string _valor)
    {
        valorEstadisticaText.text = _valor;
    }

    public void MostrarPanelGraficoEstadistica()
    {
        if (desdeEquipo)
        {
            bool isPartido = panelPartidosEquipo.GetIsPartido();

            panelDetalleEquipo.MostrarPanelGraficoEstadistica(nombreEstadisticaText.text, isPartido, false);
        }
        else
        {
            bool isPartido = panelDetalleJugador.GetIsPartido();

            panelDetalleEquipo.MostrarPanelGraficoEstadistica(nombreEstadisticaText.text, isPartido, true);
        }
    }
}
