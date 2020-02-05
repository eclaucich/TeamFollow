using UnityEngine;
using UnityEngine.UI;

public class HerramientaColor : Herramienta
{
    [SerializeField] private Color color;
    [SerializeField] private Image imagenFicha = null;

    public void SetColorActual()
    {
        PanelHerramientas panelHerramientas = GetComponentInParent<PanelHerramientas>();
        panelHerramientas.GetPanelCrearJugadas().SetColorActual(color);
        imagenFicha.color = color;
    }

    public override void Usar()
    {
        throw new System.NotImplementedException();
    }
}
