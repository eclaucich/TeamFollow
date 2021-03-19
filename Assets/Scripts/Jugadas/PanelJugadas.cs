using System.Collections;
using UnityEngine;

public class PanelJugadas : Panel {

    [SerializeField] private GameObject panel_principal = null;
    [SerializeField] private GameObject panel_crearJugada = null;
    [SerializeField] private PanelEdicion panelEdicion = null;

    public void MostrarPanelPrincipal()
    {
        SetPublicity();

        AppController.instance.pantallaCarga.Activar();

        CanvasController.instance.overlayPanel.gameObject.SetActive(false);
        CanvasController.instance.botonDespliegueMenu.SetActive(false);
        
        panel_principal.SetActive(false);
        panel_crearJugada.SetActive(false);

        StartCoroutine(EsperarPantallaCarga());

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
