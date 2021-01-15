using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ObjetoEdicion : MonoBehaviour, IPointerClickHandler, IDragHandler
{
    [SerializeField] private bool mover = true;
    [SerializeField] private bool borrar = true;
    private bool escalado = false;
    private bool moviendo = false;
    private bool panelHerramientasWasOpened = false;

    private PanelEdicion panelEdicion;
    private PanelHerramientas panelHerramientas;

    private void Start()
    {
        panelEdicion = GameObject.Find("PanelEdicion").GetComponent<PanelEdicion>();
        panelHerramientas = panelEdicion.GetPanelHerramientas();
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
            Debug.Log("MOVIENDO FICHA");
            if (GetComponentInParent<PanelCrearJugadas>().GetHerramientaActual().GetNombre() == "Seleccionar")
            {
                Debug.Log("HERRAMIENTA SELECCION");
                if(panelHerramientas.isActive())
                {
                    Debug.Log("PANEL ABIERTO -> LO CIERRO");
                    panelHerramientas.ToogleActive();
                    moviendo = true;
                }
                Vector3 mPos = Input.mousePosition;
                mPos.z = 2f;
                Vector3 goPos = Camera.main.ScreenToWorldPoint(mPos);
                transform.position = goPos;
                if(!escalado)
                {
                    transform.localScale *= 1.3f;
                    escalado = true;
                }
                GetComponentInParent<PanelCrearJugadas>().SetObjetoActual(this.gameObject);
            }
        }
    }
    public void OnMouseUp()
    {
        if (moviendo)
        {
            Debug.Log("FICHA SOLTADA");
            moviendo = false;
            transform.localScale = new Vector3(1f,1f,1f);
            escalado = false;
            panelHerramientas.ToogleActive();
        }
    }
}
