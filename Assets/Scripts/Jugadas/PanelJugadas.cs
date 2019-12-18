using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelJugadas : MonoBehaviour {

    [SerializeField] private GameObject panel_principal = null;
    [SerializeField] private GameObject panel_crearJugada = null;

    public void MostrarPanelPrincipal()
    {
        panel_principal.SetActive(false);
        panel_crearJugada.SetActive(true);

        CanvasController.instance.botonDespliegueMenu.SetActive(false);

        Screen.orientation = ScreenOrientation.Landscape;
        //Screen.SetResolution(1080, 1920, false);

        CanvasController.instance.AgregarPanelAnterior(CanvasController.Paneles.MisEquipos);
    }

    public void MostrarPanelCrearJugada()
    {
        panel_principal.SetActive(false);
        panel_crearJugada.SetActive(true);
    }
}
