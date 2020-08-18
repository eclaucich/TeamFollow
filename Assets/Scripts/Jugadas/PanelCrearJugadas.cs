using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelCrearJugadas : MonoBehaviour
{
    [SerializeField] private Image imagenBotonFicha = null;
    [SerializeField] protected PanelEdicion panelEdicion = null;

    protected GameObject objectoActual;
    protected Herramienta herramientaActual;
    protected Color colorActual;

    public void SetHerramientaActual(Herramienta herramienta_)
    {
        herramientaActual = herramienta_;
    }

    public Herramienta GetHerramientaActual()
    {
        return herramientaActual;
    }

    public void UsarHerramientaActual()
    {
        herramientaActual.Usar();
    }

    public void SetObjetoActual(GameObject _object)
    {
        objectoActual = _object;
    }

    public GameObject GetObjetoActual()
    {
        return objectoActual;
    }

    public Color GetColorActual()
    {
        return colorActual;
    }

    public void SetColorActual(Color _color)
    {
        colorActual = _color;
        imagenBotonFicha.color = colorActual;
        panelEdicion.CerrarPanelOpcionesActual();
    }

    public void ChangeHerramientaActualFondo(bool seleccionar)
    {
        if (herramientaActual)
        {
            herramientaActual.ChangeFondo(seleccionar);
        }
    }
}
