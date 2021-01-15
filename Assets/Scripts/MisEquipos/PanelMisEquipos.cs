using UnityEngine;
using System.Collections.Generic;

/// <summary>
///
/// Panel que controla la sección de MIS EQUIPOS
/// 
/// Tiene como hijos tres paneles, correspondientes a sub-secciones.
///     La creación de un nuevo equipo
///     La pantalla principal que muestra la lista de equipos
///     La visualización de los detalles de un equipo
/// 
/// </summary>

public class PanelMisEquipos : MonoBehaviour {

    [SerializeField] private GameObject panel_principal = null;                                                                 //Panel principal de la sección mis equipos
    [SerializeField] private GameObject panel_nuevoEquipo = null;                                                              //Panel de la creación de un nuevo equipo                                                         
    [SerializeField] private GameObject panel_detalleEquipo = null;                                                            //Panel que muestra los detalles de un equipo

    private List<GameObject> listaPaneles;                                                          


    private void Start()                                                                                                //Al comenzar se muestra el panel principal de la sección
    {
        listaPaneles = new List<GameObject>();
        listaPaneles.Add(panel_principal);
        listaPaneles.Add(panel_nuevoEquipo);
        listaPaneles.Add(panel_detalleEquipo);

        //MostrarPanelPrincipal();
    }


    public void MostrarPanelPrincipal()                                                                                 //Muestra el panel PRINCIPAL. Desactiva los demás.
    {
        //ActivarPanel(0);
        panel_principal.SetActive(true);
        panel_nuevoEquipo.SetActive(false);
        panel_detalleEquipo.SetActive(false);

        Screen.orientation = ScreenOrientation.Portrait;

        panel_principal.GetComponent<PanelPrincipal>().SetearPanelPrincipal();

        CanvasController.instance.AgregarPanelAnterior(CanvasController.Paneles.Salir);
    }

    public void MostrarPanelNuevoEquipo()                                                                               //Muestra el panel de NUEVO EQUIPO. Desactiva los demás.
    {
        //ActivarPanel(1);
        panel_principal.SetActive(false);
        panel_nuevoEquipo.SetActive(true);
        panel_detalleEquipo.SetActive(false);

        CanvasController.instance.AgregarPanelAnterior(CanvasController.Paneles.MisEquiposPrincipal);

        //panel_nuevoEquipo.GetComponent<PanelNuevoEquipo>().SetPanel();
        panel_nuevoEquipo.GetComponent<PanelNuevoEquipov2>().SetPanel();
    }

    public void MostrarPanelDetalleEquipo(string nombreEquipo_, GameObject botonEquipo_)                                                         //Muestra el panel de DETALLE EQUIPO. Desactiva los demás.
    {
        //ActivarPanel(2);
        panel_principal.SetActive(false);
        panel_nuevoEquipo.SetActive(false);
        panel_detalleEquipo.SetActive(true);

        panel_detalleEquipo.GetComponent<PanelDetalleEquipo>().SetPanelPrincipal(nombreEquipo_, botonEquipo_);
    }


    private void ActivarPanel(int index)
    {
        foreach (var panel in listaPaneles) panel.SetActive(false);

        listaPaneles[index].SetActive(true);
    }
}
