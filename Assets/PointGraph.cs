using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointGraph : MonoBehaviour
{
    [SerializeField] private PanelGraficoEstadistica panelGraficoEstadistica = null;
    [SerializeField] private PanelJugadores panelJugadores = null;
    [SerializeField] private PanelPartidosEquipo panelPartidosEquipo = null;
    [SerializeField] private Text valorText = null;
    private Partido partidoPadre;

    public void SetPoint(float coordX, float coordY, int valor)
    {
        transform.localPosition = new Vector3(coordX, coordY, -30f);
        valorText.text = valor.ToString();
    }

    public void SetPartidoPadre(Partido _partido)
    {
        partidoPadre = _partido;
    }

    public void MostrarPartidoPadre()
    {
        CanvasController.instance.AgregarPanelAnterior(CanvasController.Paneles.Graficas);
        CanvasController.instance.botonDespliegueMenu.SetActive(true);
        AppController.instance.overlayPanel.gameObject.SetActive(true);
        panelPartidosEquipo.MostrarPanelDetallePartido(partidoPadre, true);
        panelGraficoEstadistica.gameObject.SetActive(false);
        panelJugadores.gameObject.SetActive(false);
    }
}
