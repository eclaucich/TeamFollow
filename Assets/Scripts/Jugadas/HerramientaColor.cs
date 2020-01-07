using UnityEngine;
using UnityEngine.UI;

public class HerramientaColor : Herramienta
{
    [SerializeField] private Color color;

    public void SetColorActual()
    {
        GetComponentInParent<PanelHerramientas>().GetPanelCrearJugadas().SetColorActual(color);
    }

    public override void Usar()
    {
        throw new System.NotImplementedException();
    }
}
