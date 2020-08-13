using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelBiblioteca : MonoBehaviour
{
    
    [SerializeField] private GameObject panel_principal = null;
    [SerializeField] private GameObject panel_imagen = null;

    public void MostrarPanelPrincipal()
    {
        AppController.instance.overlayPanel.SetNombrePanel("BIBLIOTECA JUGADAS", AppController.Idiomas.Español);
        AppController.instance.overlayPanel.SetNombrePanel("STRATEGIES LIBRARY", AppController.Idiomas.Ingles);

        panel_principal.SetActive(true);
        panel_imagen.SetActive(false);

        panel_principal.GetComponent<PanelPrincipalBiblioteca>().SetPanePrincipal();

        CanvasController.instance.AgregarPanelAnterior(CanvasController.Paneles.MisEquipos);
    }

    public void MostrarPanelImagen(BotonImagen botonFocus_)
    {
        AppController.instance.overlayPanel.SetNombrePanel("", AppController.Idiomas.Español);
        AppController.instance.overlayPanel.SetNombrePanel("", AppController.Idiomas.Ingles);

        panel_principal.SetActive(false);
        panel_imagen.SetActive(true);

        panel_imagen.GetComponent<PanelImagen>().SetPanel(botonFocus_);

        CanvasController.instance.AgregarPanelAnterior(CanvasController.Paneles.BibliotecaPrincipal);
    }


}
