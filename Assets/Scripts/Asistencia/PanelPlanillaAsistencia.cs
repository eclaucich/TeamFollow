using UnityEngine;
using System.Collections.Generic;

public class PanelPlanillaAsistencia : MonoBehaviour {

    [SerializeField] private GameObject panel_principal = null;
    [SerializeField] private GameObject panel_nuevaPlanilla = null;
    [SerializeField] private GameObject panel_historialPlanillas = null;
    [SerializeField] private GameObject panel_planilla = null;

    private List<GameObject> listaPaneles;

    private void Awake()
    {
        listaPaneles = new List<GameObject>();
        listaPaneles.Add(panel_principal);
        listaPaneles.Add(panel_nuevaPlanilla);
        listaPaneles.Add(panel_historialPlanillas);
        listaPaneles.Add(panel_planilla);
    }

    /*public void MostrarPanelPrincipal()
    {
        ActivarPanel(0);

        CanvasController.instance.AgregarPanelAnterior(CanvasController.Paneles.DetalleEquipoPrincipal);
    }*/

    public void MostrarPanelNuevaPlanilla()
    {
        ActivarPanel(1);

        panel_nuevaPlanilla.GetComponent<PanelNuevaPlanilla>().SetPanelNuevaPlanilla();

        CanvasController.instance.AgregarPanelAnterior(CanvasController.Paneles.HistorialPlanilla);
    }

    public void MostrarPanelHistorialPlanillas()
    {
        ActivarPanel(2);

        panel_historialPlanillas.GetComponent<PanelHistorialPlanillas>().SetPanelHistorialPlanillas();

        CanvasController.instance.AgregarPanelAnterior(CanvasController.Paneles.DetalleEquipoPrincipal);
    }

    public void MostrarPanelPlanilla(BotonHistorialAsistencia botonHistorialAsistencia)
    {
        ActivarPanel(3);

        panel_planilla.GetComponent<PanelPlanilla>().SetPanelPlanilla(botonHistorialAsistencia);

        CanvasController.instance.AgregarPanelAnterior(CanvasController.Paneles.HistorialPlanilla);
    }

    public void ActivarPanel(int index)
    {
        foreach (var panel in listaPaneles) panel.SetActive(false);

        listaPaneles[index].SetActive(true);
    }
}
