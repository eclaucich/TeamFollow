using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelJugadas : MonoBehaviour {

    [SerializeField] private GameObject panel_principal = null;
    [SerializeField] private GameObject panel_crearJugada = null;
    [SerializeField] private PanelEdicion panelEdicion = null;

    public void MostrarPanelPrincipal()
    {
        CanvasController.instance.overlayPanel.SetNombrePanel("", AppController.Idiomas.Español);
        CanvasController.instance.overlayPanel.SetNombrePanel("", AppController.Idiomas.Ingles);

        panel_principal.SetActive(false);
        panel_crearJugada.SetActive(false);

        CanvasController.instance.botonDespliegueMenu.SetActive(false);

        AppController.instance.pantallaCarga.Activar();
        StartCoroutine(EsperarPantallaCarga());

        //Screen.orientation = ScreenOrientation.Landscape;
        //Screen.SetResolution(1280, 720, false);

        CanvasController.instance.AgregarPanelAnterior(CanvasController.Paneles.MisEquipos);
    }

    private IEnumerator EsperarPantallaCarga()
    {
        Debug.Log("CARGANDO:  " + AppController.instance.pantallaCarga.IsLoading());
        yield return new WaitUntil(() => !AppController.instance.pantallaCarga.IsLoading());
        Debug.Log("TERMINO CARGAR: " + AppController.instance.pantallaCarga.IsLoading());
        panel_crearJugada.SetActive(true);
        panelEdicion.SetPanelEdicion();
    }

    public void MostrarPanelCrearJugada()
    {
        panel_principal.SetActive(false);
        panel_crearJugada.SetActive(true);
    }
}
