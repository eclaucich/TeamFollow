using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class Herramienta : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] protected Button botonSeleccion = null;
    [SerializeField] private PanelOpcionesHerramienta panelOpciones = null;
    [SerializeField] private bool hasOptions = true;
    [SerializeField] private GameObject fondo = null;

    protected string nombre;

    private bool longClick = false;


    public void OnPointerClick(PointerEventData eventData)
    {
        longClick = false; 
    }

    public void SetHerramientaActual()
    {
        PanelHerramientas panelHerramientas;
        panelHerramientas = GetComponentInParent<PanelHerramientas>();

        AndroidManager.HapticFeedback();

        if (!longClick)
        {
            panelHerramientas.GetPanelCrearJugadas().ChangeHerramientaActualFondo(false);
            panelHerramientas.GetPanelCrearJugadas().SetHerramientaActual(this);
            ChangeFondo(true);

            GameObject.Find("PanelEdicion").GetComponent<PanelEdicion>().CerrarPanelOpcionesActual();

            //panelHerramientas.GetComponent<MensajeDesplegable>().ToggleDesplegar();
        }
    }

    public void LongClickedFunction()
    {
        AndroidManager.HapticFeedback();
        longClick = true;
        panelOpciones.ToggleDesplegar();
    }

    public string GetNombre()
    {
        return nombre;
    }

    abstract public void Usar();

    virtual public void DejarDeUsar() { }

   /* public Material ElegirMaterial(Color _color)
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
    }*/

    public void ChangeFondo(bool seleccionar)
    {
        if (fondo == null)
        {
            Debug.Log("NULL");
            return;
        }
        if (seleccionar) fondo.GetComponent<Image>().color = AppController.instance.colorTheme.botonDesactivado;//new Color32(67, 92, 67, 255);
        else fondo.GetComponent<Image>().color = AppController.instance.colorTheme.detalle3;//new Color32(137, 183, 137, 1);
    }
}
