using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ObjetoEdicion : MonoBehaviour, IPointerClickHandler, IDragHandler
{
    [SerializeField] private bool mover = true;
    [SerializeField] private bool borrar = true;

    private bool moviendo = false;
    private PanelEdicion panelEdicion;
    private bool panelHerramientasCerrado = false;

    private void Start()
    {
        panelEdicion = GameObject.Find("PanelEdicion").GetComponent<PanelEdicion>();
    }

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
                if(!panelHerramientasCerrado)
                {
                    panelEdicion.SetSwipe(false);
                    panelEdicion.CerrarPanelHerramientas();
                    panelHerramientasCerrado = true;
                    moviendo = true;
                }
                Vector3 mPos = Input.mousePosition;
                mPos.z = 2f;
                Vector3 goPos = Camera.main.ScreenToWorldPoint(mPos);
                transform.position = goPos;
                GetComponentInParent<PanelCrearJugadas>().SetObjetoActual(this.gameObject);
            }
        }
    }
    public void OnMouseUp()
    {
        if (moviendo)
        {
           // Debug.Log("SOLTADO");
            moviendo = false;
            panelEdicion.SetSwipe(true);
            panelHerramientasCerrado = false;
            panelEdicion.AbrirPanelHerramientas();
        }
    }
}
