using UnityEngine;

public class DarkTheme : ColorTheme
{
    [SerializeField] private Color detalle1;
    [SerializeField] private Color detalle2;
    [SerializeField] private Color detalle3;
    [SerializeField] private Color detalle4;

    public Color GetColorDetalle1()
    {
        return detalle1;
    }

    public Color GetColorDetalle2()
    {
        return detalle2;
    }

    public Color GetColorDetalle3()
    {
        return detalle4;
    }

    public Color GetColorDetalle4()
    {
        return detalle4;
    }
}
