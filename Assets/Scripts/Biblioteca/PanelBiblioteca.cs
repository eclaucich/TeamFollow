using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelBiblioteca : MonoBehaviour
{
    
    [SerializeField] private GameObject panel_principal = null;
    [SerializeField] private GameObject panel_imagen = null;

    public void MostrarPanelPrincipal()
    {
        panel_principal.SetActive(true);
        panel_imagen.SetActive(false);

        CanvasController.instance.AgregarPanelAnterior(CanvasController.Paneles.MisEquipos);
    }

    public void MostrarPanelImagen()
    {
        panel_principal.SetActive(false);
        panel_imagen.SetActive(true);

        CanvasController.instance.AgregarPanelAnterior(CanvasController.Paneles.Biblioteca);
    }


}
