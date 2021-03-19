using UnityEngine;
using UnityEngine.UI;

public class FichaJugada : ObjetoEdicion
{
    [SerializeField] private Image fillImage = null;
    [SerializeField] private Image outlineImage = null;

    private PanelCrearJugadas panelCrearJugadas;

    public void SetColorActual(Color colorActual)
    {
        outlineImage.color = new Color(colorActual.r, colorActual.g, colorActual.b, 255f);
        Debug.Log("SET COLOR: " + colorActual);
    }

    public Sprite GetDisplayImage()
    {
        return fillImage.sprite;
    }
}
