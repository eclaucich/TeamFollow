using UnityEngine;
using UnityEngine.UI;

public class HerramientaColor : Herramienta
{
    private Color color;
    [SerializeField] private PanelOpcionesHerramienta panelOpciones = null;
    //[SerializeField] private Image imagenFicha = null;
    [SerializeField] private PanelCrearJugadas panelCrearJugadas = null;

    public void SetColorActual()
    {
        color = GetComponent<Image>().color;
        panelCrearJugadas.SetColorActual(color);
        /*imagenFicha.color = color;
        panelOpciones.Cerrar();*/
    }

    public override void Usar()
    {
        throw new System.NotImplementedException();
    }
}
