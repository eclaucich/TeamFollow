using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelTablero : MonoBehaviour
{
    [SerializeField] private GameObject panel_crearJugada = null;

    public void MostrarPanelPrincipal()
    {
        CanvasController.instance.overlayPanel.SetNombrePanel("", AppController.Idiomas.Español);
        CanvasController.instance.overlayPanel.SetNombrePanel("", AppController.Idiomas.Ingles);

        panel_crearJugada.SetActive(true);

        CanvasController.instance.botonDespliegueMenu.SetActive(false);

        Screen.orientation = ScreenOrientation.Landscape;
        //Screen.SetResolution(1080, 1920, false);

        CanvasController.instance.AgregarPanelAnterior(CanvasController.Paneles.MisEquipos);
    }
}
