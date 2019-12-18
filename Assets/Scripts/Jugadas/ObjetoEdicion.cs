using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ObjetoEdicion : MonoBehaviour, IPointerClickHandler, IDragHandler
{
    [SerializeField] private bool mover = true;
    [SerializeField] private bool borrar = true;

    public void OnPointerClick(PointerEventData eventData) 
    {
        if (borrar)
        {
            if (GetComponentInParent<PanelCrearJugadas>().GetHerramientaActual().GetNombre() == "Seleccionar")
            {
                GetComponentInParent<PanelCrearJugadas>().SetObjetoActual(null);
            }
            else if (GetComponentInParent<PanelCrearJugadas>().GetHerramientaActual().GetNombre() == "Borrar")
            {
                Destroy(this.gameObject);
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (mover)
        {
            if (GetComponentInParent<PanelCrearJugadas>().GetHerramientaActual().GetNombre() == "Seleccionar")
            {
                transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f);
                GetComponentInParent<PanelCrearJugadas>().SetObjetoActual(this.gameObject);
            }
        }
    }

}
