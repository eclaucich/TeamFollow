using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointGraph : MonoBehaviour
{
    [SerializeField] private PanelGraficoEstadistica panelGraficoEstadistica = null;
    [SerializeField] private PanelPartidosEquipo panelPartidosEquipo = null;
    [SerializeField] private Text valorText = null;
    private Partido partidoPadre;

    public void SetPoint(float coordX, float coordY, int valor)
    {
        transform.localPosition = new Vector3(coordX, coordY, -10);
        valorText.text = valor.ToString();
    }

    public void SetPartidoPadre(Partido _partido)
    {
        partidoPadre = _partido;
    }

    public void MostrarPartidoPadre()
    {
        panelGraficoEstadistica.gameObject.SetActive(false);
        panelPartidosEquipo.MostrarPanelDetallePartido(partidoPadre);
    }
}
