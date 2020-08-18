using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour {

    public static CanvasController instance = null;

    [HideInInspector] public bool retrocesoPausado = false;

    //GUI
    [SerializeField] public OverlayPanel overlayPanel = null;
    [SerializeField] public GameObject botonDespliegueMenu = null;
    [SerializeField] private MensajeDesplegable menuSeleccion = null;

    //PANELES PRINCIPALES
    [SerializeField] private GameObject panelMiPerfil = null;
    [SerializeField] private PanelMisEquipos panelMisEquipos = null;
    [SerializeField] private PanelJugadas panelJugadas = null;
    [SerializeField] private PanelDetalleEquipo panelDetalleEquipo = null;
    [SerializeField] private PanelEntradaDatos panelEntradaDatos = null;
    [SerializeField] private PanelJugadores panelJugadores = null;
    [SerializeField] private PanelPlanillaAsistencia panelPlanillaAsistencia = null;
    [SerializeField] private PanelGraficoEstadistica panelGraficas = null;
    [SerializeField] private PanelBiblioteca panelBiblioteca = null;

    //PANELES AUXILIARES
    [SerializeField] private ConfirmacionBorradoJugador confirmacionBorradoJugador = null;
    [SerializeField] private EleccionBorradoEstadisticas eleccionBorradoEstadisticas = null;
    [SerializeField] private ConfirmacionBorradoEquipo confirmacionBorradoEquipo = null;
    [SerializeField] private ConfirmacionBorradoAsistencia confirmacionBorradoAsistencia = null;
    [SerializeField] private ConfirmacionBorradoPartido confirmacionBorradoPartidoJugador = null;
    [SerializeField] private ConfirmacionBorradoPartido confirmacionBorradoPartidoEquipo = null;

    public List<int> escenas;
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

        Graficas,

        BibliotecaPrincipal
    }

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(instance);

        escenas = new List<int>();
        
        listaPaneles = new List<GameObject>();
        listaPaneles.Add(panelMiPerfil);
        listaPaneles.Add(panelMisEquipos.gameObject);
        listaPaneles.Add(panelJugadas.gameObject);
        listaPaneles.Add(panelBiblioteca.gameObject);

        AbrirMisEquipos();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MostrarPanelAnterior();
        }
    }

    #region Abrir paneles principales
    public void AbrirMiPerfil()
    {
        ActivarPanel(0);

        menuSeleccion.Cerrar();
    }

    public void AbrirMisEquipos()
    {
        ActivarPanel(1);  

        panelMisEquipos.MostrarPanelPrincipal();

        if (menuSeleccion != null) menuSeleccion.Cerrar();

        botonDespliegueMenu.SetActive(true);
    }

    public void AbrirJugadas()
    {
        ActivarPanel(2);

        panelJugadas.MostrarPanelPrincipal();

        menuSeleccion.Cerrar();   
    }

    public void AbrirBiblioteca()
    {
        ActivarPanel(3);

        menuSeleccion.Cerrar();

        panelBiblioteca.MostrarPanelPrincipal();
    }
    private void ActivarPanel(int index)
    {
        foreach (var panel in listaPaneles) 
            panel.SetActive(false);

        listaPaneles[index].SetActive(true);
    }
    #endregion

    #region Control de el retroceder pantalla
    public void MostrarPanelAnterior()
    {
        if (menuSeleccion.isDesplegado())
        {
            menuSeleccion.Cerrar();
            return;
        }
        else if (confirmacionBorradoPartidoEquipo.isDesplegado())
        {
            confirmacionBorradoPartidoEquipo.Cerrar();
            return;
        }
        else if (confirmacionBorradoPartidoJugador.isDesplegado())
        {
            confirmacionBorradoPartidoJugador.Cerrar();
            return;
        }
        else if (confirmacionBorradoEquipo.isDesplegado())
        {
            confirmacionBorradoEquipo.Cerrar();
            return;
        }
        else if (confirmacionBorradoJugador.isDesplegado())
        {
            confirmacionBorradoJugador.Cerrar();
            return;
        }
        else if (eleccionBorradoEstadisticas.isDesplegado())
        {
            eleccionBorradoEstadisticas.Cerrar();
            return;
        }
        else if (confirmacionBorradoAsistencia.isDesplegado())
        {
            confirmacionBorradoAsistencia.Cerrar();
            return;
        }
        if (panelGraficas.gameObject.activeSelf)
            panelGraficas.gameObject.SetActive(false);

        if (!retrocesoPausado && escenas.Count != 0)
        {
            int codigoPanel = escenas[escenas.Count-1];

            switch (codigoPanel)
            {
                case (int)Paneles.Salir: Application.Quit();
                    break;

                case (int)Paneles.MisEquiposPrincipal: panelMisEquipos.MostrarPanelPrincipal();
                    break;

                case (int)Paneles.DetalleEquipoPrincipal: panelDetalleEquipo.MostrarPanelJugadores();//panelDetalleEquipo.MostrarPanelPrincipal();
                    break;

                case (int)Paneles.EstadisticasGlobalesEquipo: panelDetalleEquipo.MostrarPanelEstadisticasGlobalesEquipo();
                    break;

                case (int)Paneles.SeleccionEstadisticas: panelEntradaDatos.MostrarPanelSeleccionEstadisticas();
                    break;

                case (int)Paneles.JugadoresPrincipal: panelJugadores.MostrarPanelPrincipal(AppController.instance.equipoActual);
                    break;

                case (int)Paneles.JugadoresInfo: panelJugadores.MostrarPanelInfoJugador();
                    break;

                case (int)Paneles.JugadoresDetalle: panelJugadores.MostrarPanelPartidos();
                    break;

                case (int)Paneles.HistorialPlanilla: panelPlanillaAsistencia.MostrarPanelHistorialPlanillas();
                    break;

                case (int)Paneles.MisEquipos: { botonDespliegueMenu.SetActive(true); AbrirMisEquipos(); }
                    break;

                case (int)Paneles.JugadoresPartidos: panelJugadores.MostrarPanelPartidos();
                    break;

                case (int)Paneles.Graficas: panelGraficas.ActivarPanel();
                    break;

                case (int)Paneles.BibliotecaPrincipal: AbrirBiblioteca();
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
    #endregion
}