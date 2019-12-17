using UnityEngine;
using UnityEngine.UI;

public class HerramientaColor : Herramienta
{
    public void SetColorActual()
    {
        GetComponentInParent<PanelHerramientas>().GetPanelCrearJugadas().SetColorActual(GetComponent<Image>().color);
    }

    public override void Usar()
    {
        throw new System.NotImplementedException();
    }
}
