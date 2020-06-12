using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 
/// GameObject padre que tiene como hijos todos los paneles relacionados con los detalles de cada equipo
/// 
/// </summary>

public class PanelDetalleEquipo : MonoBehaviour {

    [SerializeField] private GameObject panel_principal = null;                                                    //Panel principal donde se pueden seleccionar los otros paneles
    [SerializeField] private GameObject panel_jugadores = null;                                                    //Panel de lista Jugadores
    [SerializeField] private GameObject panel_entradaDatos = null;                                                 //Panel para ingresar datos
    [SerializeField] private GameObject panel_estadisticasGlobalesEquipo = null;                                   //Panel para ver las estadisticas del equipo
    [SerializeField] private GameObject panel_planillaAsistencias = null;

    private List<GameObject> listaPaneles;

    private Equipo equipo;                                                                                  //Equipo enfocado

    private void Start()
    {
        listaPaneles = new List<GameObject>();
        listaPaneles.Add(panel_principal);
        listaPaneles.Add(panel_estadisticasGlobalesEquipo);
        listaPaneles.Add(panel_jugadores);
        listaPaneles.Add(panel_entradaDatos);
        listaPaneles.Add(panel_planillaAsistencias);
    }


    public void SetPanelPrincipal(string nombreEquipo_, GameObject botonEquipo_)                                                    //Se setea el panel principal. Se setea el equipo enfocado.
    {
        equipo = AppController.instance.equipos[AppController.instance.BuscarPorNombre(nombreEquipo_)];
        AppController.instance.SetEquipoActual(equipo);

        panel_principal.GetComponent<PanelPrincipalDetalleEquipo>().SetBotonEquipoFocus(botonEquipo_);
        MostrarPanelJugadores();
    }

    //Por alguna razon poner ActivarPanel(0) no anda acá
    public void MostrarPanelPrincipal()                                                                     //Se activa el panel principal, y se desactivan los otros
    {
        panel_principal.SetActive(true);
        panel_estadisticasGlobalesEquipo.SetActive(false);
        panel_jugadores.SetActive(false);
        panel_entradaDatos.SetActive(false);
        panel_planillaAsistencias.SetActive(false);

        CanvasController.instance.botonDespliegueMenu.SetActive(true);

        CanvasController.instance.AgregarPanelAnterior(CanvasController.Paneles.MisEquiposPrincipal);

        panel_principal.GetComponent<PanelPrincipalDetalleEquipo>().SetBotonesDisponibles();
    }

    public void MostrarPanelEstadisticasGlobalesEquipo()                                                   //Se activa el panel de estadisticas, y se desactivan los otros. A la vez se setean el panel
    {
        ActivarPanel(1);

        panel_estadisticasGlobalesEquipo.GetComponent<PanelPartidosEquipo>().SetearPanelPartidos();

        CanvasController.instance.AgregarPanelAnterior(CanvasController.Paneles.DetalleEquipoPrincipal);
    }

    public void MostrarPanelJugadores()                                                                    //Se activa el panel de jugadores, y se desactivan los otros
    {
        //ActivarPanel(2);

        panel_principal.SetActive(false);
        panel_estadisticasGlobalesEquipo.SetActive(false);
        panel_jugadores.SetActive(true);
        panel_entradaDatos.SetActive(false);
        panel_planillaAsistencias.SetActive(false);

        CanvasController.instance.botonDespliegueMenu.SetActive(true);

        CanvasController.instance.AgregarPanelAnterior(CanvasController.Paneles.MisEquiposPrincipal);


        panel_jugadores.GetComponent<PanelJugadores>().MostrarPanelPrincipal(equipo);

        //CanvasController.instance.AgregarPanelAnterior(CanvasController.Paneles.DetalleEquipoPrincipal);
    }

    public void MostrarPanelEntradaDatos()                                                                //Se activa el panel de entrada de datos, y se desactivan los otros
    {
        ActivarPanel(3);

        panel_entradaDatos.GetComponent<PanelEntradaDatos>().MostrarPanelSeleccionEstadisticas();
    }

    public void MostrarPanelPlanillaAsistencias()                                                                //Se activa el panel de entrada de datos, y se desactivan los otros
    {
        ActivarPanel(4);

        panel_planillaAsistencias.GetComponent<PanelPlanillaAsistencia>().MostrarPanelHistorialPlanillas();  
    }


    private void ActivarPanel(int index)
    {
        foreach (var panel in listaPaneles) panel.SetActive(false);

        listaPaneles[index].SetActive(true);
    }
}
