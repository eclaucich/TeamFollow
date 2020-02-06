using UnityEngine;
using UnityEngine.UI;

public class HerramientaColor : Herramienta
{
    private Color color;
    [SerializeField] private Image imagenBoton = null;
    [SerializeField] private Image imagenFicha = null;

    public void SetColorActual()
    {
        color = GetComponent<Image>().color;
        PanelHerramientas panelHerramientas = GetComponentInParent<PanelHerramientas>();
        panelHerramientas.GetPanelCrearJugadas().SetColorActual(color);
        imagenFicha.color = color;
        imagenBoton.color = color;
    }

    public override void Usar()
    {
        throw new System.NotImplementedException();
    }
}
