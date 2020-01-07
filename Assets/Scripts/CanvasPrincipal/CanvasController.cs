using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// Controla el "Menú de Selección" de las secciones principales
/// Cuando se elige una sección, se activa y se desactivan todas las demás
/// 
/// </summary>

public class CanvasController : MonoBehaviour {

    public static CanvasController instance = null;

    [HideInInspector] public bool retrocesoPausado = false;                     //Permite activar o desactivar el retroceso de página. Algunos paneles lo desactivan para poder usar el retroceso de página internamente, y luego lo vuelven a activar

    [SerializeField] public GameObject botonDespliegueMenu = null;
    [SerializeField] private GameObject panel_miPerfil = null;                             //Panel de MI PERFIL
    [SerializeField] private GameObject panel_misEquipos = null;                           //Panel de MIS EQUIPOS
    [SerializeField] private GameObject panel_jugadas = null;                              //Panel de JUGADAS
    [SerializeField] private GameObject panel_detalle_equipo = null;                       //Panel de los DETALLES de  un EQUIPO
    [SerializeField] private GameObject panel_entrada_datos = null;                        //Panel de las ENTRADAS DE DATOS de un EQUIPO
    [SerializeField] private PanelJugadores panel_jugadores = null;
    [SerializeField] private PanelPlanillaAsistencia panel_planillas_asistencias = null;
    [SerializeField] private GameObject panel_tablero = null;
    [SerializeField] private GameObject panel_biblioteca = null;

    [SerializeField] private GameObject panel_menuSeleccion = null;
    private MensajeDesplegable menuSeleccion;

    [SerializeField] private ConfirmacionBorradoJugador confirmacionBorradoJugador = null;
    [SerializeField] private ConfirmacionBorradoEquipo confirmacionBorradoEquipo = null;

    public List<int> escenas;
    private PanelMisEquipos panelMisEquipos;
    private PanelDetalleEquipo panelDetalleEquipo;
    private PanelEntradaDatos panelEntradaDatos;

    private List<GameObject> listaPaneles;
    
    public enum Paneles
    {
        Salir,

        MisEquipos,
        MisEquiposPrincipal,
        MisEquiposNuevoEquipo,
        DetalleEquipoPrincipal,
        EstadisticasGlobalesEquipo,
        SeleccionEstadisticas,

        JugadoresPrincipal,
        JugadoresInfo,
        JugadoresNuevoJugador,
        JugadoresPartidos,
        JugadoresDetalle,

        PlanillaAsistenciaPrincipal,
        HistorialPlanilla,

        Biblioteca
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }

        menuSeleccion = panel_menuSeleccion.GetComponent<MensajeDesplegable>(); 
                                                      
        escenas = new List<int>();
        panelMisEquipos = panel_misEquipos.GetComponent<PanelMisEquipos>();
        panelDetalleEquipo = panel_detalle_equipo.GetComponent<PanelDetalleEquipo>();
        panelEntradaDatos = panel_entrada_datos.GetComponent<PanelEntradaDatos>();

        listaPaneles = new List<GameObject>();
        listaPaneles.Add(panel_miPerfil);
        listaPaneles.Add(panel_misEquipos);
        listaPaneles.Add(panel_jugadas);
        listaPaneles.Add(panel_tablero);
        listaPaneles.Add(panel_biblioteca);

        AbrirMisEquipos();  
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MostrarPanelAnterior();
        }
    }

    public void AbrirMiPerfil()                                                     //Activa el panel MI PERFIL y descativa los demás. Oculta el menú de selección
    {
        ActivarPanel(0);

        menuSeleccion.Cerrar();
    }

    public void AbrirMisEquipos()                                                   //Activa el panel MIS EQUIPOS y descativa los demás. Oculta el menú de selección
    {
        ActivarPanel(1);  

        panel_misEquipos.GetComponent<PanelMisEquipos>().MostrarPanelPrincipal();

        if (panel_menuSeleccion != null) menuSeleccion.Cerrar();

        botonDespliegueMenu.SetActive(true);
    }

    public void AbrirJugadas()                                                      //Activa el panel JUGADAS y descativa los demás. Oculta el menú de selección
    {
        ActivarPanel(2);

        panel_jugadas.GetComponent<PanelJugadas>().MostrarPanelPrincipal();

        menuSeleccion.Cerrar();   
    }

    public void AbrirTablero()
    {
        ActivarPanel(3);

        menuSeleccion.Cerrar();

        panel_tablero.GetComponent<PanelTablero>().MostrarPanelPrincipal();
    }

    public void AbrirBiblioteca()
    {
        ActivarPanel(4);

        menuSeleccion.Cerrar();

        panel_biblioteca.GetComponent<PanelBiblioteca>().MostrarPanelPrincipal();
    }


    public void MostrarPanelAnterior()
    {
        if (menuSeleccion.isDesplegado())
        {
            menuSeleccion.Cerrar();
            return;
        }
        if (confirmacionBorradoEquipo.isDesplegado())
        {
            confirmacionBorradoEquipo.Cerrar();
            return;
        }
        if (confirmacionBorradoJugador.isDesplegado())
        {
            confirmacionBorradoJugador.Cerrar();
            return;
        }

        if (!retrocesoPausado && escenas.Count != 0)
        {
            int codigoPanel = escenas[escenas.Count-1];

            switch (codigoPanel)
            {
                case (int)Paneles.Salir: Application.Quit();
                    break;

                case (int)Paneles.MisEquiposPrincipal: panelMisEquipos.MostrarPanelPrincipal();
                    break;

                case (int)Paneles.DetalleEquipoPrincipal: panelDetalleEquipo.MostrarPanelPrincipal();
                    break;

                case (int)Paneles.EstadisticasGlobalesEquipo: panelDetalleEquipo.MostrarPanelEstadisticasGlobalesEquipo();
                    break;

                case (int)Paneles.SeleccionEstadisticas: panelEntradaDatos.MostrarPanelSeleccionEstadisticas();
                    break;

                case (int)Paneles.JugadoresPrincipal: panel_jugadores.MostrarPanelPrincipal(AppController.instance.equipoActual);
                    break;

                case (int)Paneles.JugadoresInfo: panel_jugadores.MostrarPanelInfoJugador();
                    break;

                case (int)Paneles.PlanillaAsistenciaPrincipal: panel_planillas_asistencias.MostrarPanelPrincipal();
                    break;

                case (int)Paneles.HistorialPlanilla: panel_planillas_asistencias.MostrarPanelHistorialPlanillas();
                    break;

                case (int)Paneles.MisEquipos: { botonDespliegueMenu.SetActive(true); AbrirMisEquipos(); }
                    break;

                case (int)Paneles.JugadoresPartidos: panel_jugadores.MostrarPanelPartidos();
                    break;
            }

            escenas.Remove(codigoPanel);
        }
    }

    public void AgregarPanelAnterior(Paneles panel)
    {
        int index = (int)panel;

        if (escenas.Contains(index))
        {
            escenas.Remove(index);
        }

        escenas.Add(index);
    }

    private void ActivarPanel(int index)
    {
        foreach (var panel in listaPaneles) panel.SetActive(false);

        listaPaneles[index].SetActive(true);
    }
}