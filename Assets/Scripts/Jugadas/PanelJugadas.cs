using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelJugadas : MonoBehaviour {

    [SerializeField] private GameObject panel_principal = null;
    [SerializeField] private GameObject panel_crearJugada = null;

    public void MostrarPanelPrincipal()
    {
        AppController.instance.overlayPanel.SetNombrePanel("");

        panel_principal.SetActive(false);
        panel_crearJugada.SetActive(true);

        CanvasController.instance.botonDespliegueMenu.SetActive(false);

        Screen.orientation = ScreenOrientation.Landscape;
        //Screen.SetResolution(1280, 720, false);

        CanvasController.instance.AgregarPanelAnterior(CanvasController.Paneles.MisEquipos);
    }

    public void MostrarPanelCrearJugada()
    {
        panel_principal.SetActive(false);
        panel_crearJugada.SetActive(true);
    }
}
