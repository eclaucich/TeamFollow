using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelJugadores : MonoBehaviour {

    [SerializeField] private GameObject panel_principal = null;        //0
    [SerializeField] private GameObject panel_detalleJugador = null;   //1
    [SerializeField] private GameObject panel_partidos = null;         //2
    [SerializeField] private GameObject panel_nuevoJugador = null;     //3
    [SerializeField] private GameObject panel_infoJugador = null;      //4

    private List<GameObject> listaPaneles;

    private string nombreJugadorFocus;
    private Estadisticas estadisticasFocus;

    private void Awake()
    {
        listaPaneles = new List<GameObject>();
        listaPaneles.Add(panel_principal);
        listaPaneles.Add(panel_detalleJugador);
        listaPaneles.Add(panel_partidos);
        listaPaneles.Add(panel_nuevoJugador);
        listaPaneles.Add(panel_infoJugador);
    }



    public void MostrarPanelPrincipal(Equipo equipo)
    {
        ActivarPanel(0);

        panel_principal.GetComponent<PanelJugadoresPrincipal>().SetPanelJugadores(equipo);
    }


    public void MostrarPanelDetalleJugador(Partido _partido, string _nombreJugador = null, Estadisticas _estadisticas = null)
    {
        ActivarPanel(1);

        CanvasController.instance.AgregarPanelAnterior(CanvasController.Paneles.JugadoresPartidos);

        string nombre = _nombreJugador != null ? _nombreJugador : nombreJugadorFocus;
        Estadisticas estadisticas = _estadisticas != null ? _estadisticas : estadisticasFocus;

        panel_detalleJugador.GetComponent<PanelDetalleJugador>().SetDetallesJugador(_partido, nombre, estadisticas);
    }


    public void MostrarPanelPartidos()
    {
        ActivarPanel(2);

        //CanvasController.instance.AgregarPanelAnterior();

        panel_partidos.GetComponent<PanelPartidos>().SetearPanelPartidos(nombreJugadorFocus);
    }


    public void MostarPanelNuevoJugador()
    {
        ActivarPanel(3);

        panel_nuevoJugador.GetComponent<PanelNuevoJugador>().SetPanel();

        CanvasController.instance.AgregarPanelAnterior(CanvasController.Paneles.JugadoresPrincipal);
    }


    public void MostrarPanelInfoJugador()
    {
        ActivarPanel(4);

        Jugador jugActual = AppController.instance.jugadorActual;
        nombreJugadorFocus = jugActual.GetNombre();

        AppController.instance.overlayPanel.SetNombrePanel(nombreJugadorFocus + ": ESTADISTICAS", AppController.Idiomas.Español);
        AppController.instance.overlayPanel.SetNombrePanel(nombreJugadorFocus + ": STATISTICS", AppController.Idiomas.Ingles);

        CanvasController.instance.AgregarPanelAnterior(CanvasController.Paneles.JugadoresPrincipal);

        panel_infoJugador.GetComponent<PanelInfoJugador>().SetearPanelInfoJugador(jugActual);  
    }
     

    public void SetEstadsiticasFocus(Estadisticas estadisticas)
    {
        estadisticasFocus = estadisticas;
    }


    public void SetPartidoFocus(BotonPartido botonpartido)
    {
        Debug.Log("SetPartidoFocus");
        string nombrePartido = botonpartido.GetComponentInChildren<Text>().text;

        Jugador jugadorFocus = AppController.instance.jugadorActual;

        bool isPartido = panel_partidos.GetComponent<PanelPartidos>().IsPartido();

        List<Partido> partidos = isPartido ? jugadorFocus.GetPartidos() : jugadorFocus.GetPracticas();

        foreach (var partido in partidos)
        {
            if (partido.GetNombre() == nombrePartido)
            {
                estadisticasFocus = partido.GetEstadisticas();
                MostrarPanelDetalleJugador(partido);// botonPartido);
                return;
            }
        }

        estadisticasFocus = null;
    }


    private void ActivarPanel(int index)
    {
        foreach (var panel in listaPaneles) panel.SetActive(false);

        listaPaneles[index].SetActive(true);
    }
}
