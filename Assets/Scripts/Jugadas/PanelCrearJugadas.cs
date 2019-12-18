using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelCrearJugadas : MonoBehaviour
{
    [SerializeField] public Material materialRojo = null;
    [SerializeField] public Material materialNegro = null;
    [SerializeField] public Material materialAzul = null;
    [SerializeField] public Material materialAmarillo = null;

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
    }
}
