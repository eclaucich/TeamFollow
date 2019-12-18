using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class Herramienta : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] protected Button botonSeleccion = null;
    [SerializeField] private PanelOpcionesHerramienta panelOpciones = null;
    [SerializeField] private bool hasOptions = true;

    protected string nombre;

    private bool longClick = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        longClick = false; 
    }

    public void SetHerramientaActual()
    {
        if (!longClick)
        {
            GetComponentInParent<PanelHerramientas>().GetPanelCrearJugadas().SetHerramientaActual(this);
            if (hasOptions && panelOpciones.isDesplegado())
            {
                panelOpciones.Cerrar();
            }
        }
    }

    public void LongClickedFunction()
    {
        longClick = true;
        panelOpciones.ToggleDesplegar();
    }

    public string GetNombre()
    {
        return nombre;
    }

    abstract public void Usar();

    virtual public void DejarDeUsar() { }

    public Material ElegirMaterial(Color _color)
    {
        if (_color.r == 1 && _color.g == 0 && _color.b == 0)
        {
            return GetComponentInParent<PanelHerramientas>().GetPanelCrearJugadas().materialRojo;
        }
        else if (_color.r == 0 && _color.g == 0 && _color.b == 0)
        {
            return GetComponentInParent<PanelHerramientas>().GetPanelCrearJugadas().materialNegro;
        }
        else if (_color.r == 0 && _color.b == 1)
        {
            return GetComponentInParent<PanelHerramientas>().GetPanelCrearJugadas().materialAzul;
        }
        else if (_color.r == 1 && _color.g == 1 && _color.b == 0)
        {
            return GetComponentInParent<PanelHerramientas>().GetPanelCrearJugadas().materialAmarillo;
        }

        return GetComponentInParent<PanelHerramientas>().GetPanelCrearJugadas().materialNegro;
    }
}
