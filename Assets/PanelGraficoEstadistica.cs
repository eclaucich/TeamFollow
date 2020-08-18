using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelGraficoEstadistica : MonoBehaviour
{
    [SerializeField] private PanelJugadores panelJugadores = null;
    [SerializeField] private PanelPartidosEquipo panelPartidosEquipo= null;

    [SerializeField] private TextLanguage ejeXText = null;
    [SerializeField] private Text ejeYText = null;
    [SerializeField] private TextLanguage titleText = null;

    [SerializeField] private GameObject graficasHorizontales = null;
    [SerializeField] private GameObject graficasVerticales = null;

    [SerializeField] private GraficaEstadistica graficaFuncionHorizontal = null;
    [SerializeField] private GraficaHistograma graficaHistogramaHorizontal = null;
    [SerializeField] private GraficaEstadistica graficaFuncionVertical = null;
    [SerializeField] private GraficaHistograma graficaHistogramaVertical = null;

    private bool vertical;
    private bool _datosJugador;

    private Dictionary<Partido, int> datosGraficaPartidos;
    private Dictionary<Jugador, int> datosGraficaJugadores;

    public enum Graficas
    {
        FuncionHorizontal = 0,
        HistogramaHorizontal = 1,
        FuncionVertical = 2,
        HistogramaVertical = 3
    };

    public void ActivarPanel()
    {
        Debug.Log("GRAFICAS");
        gameObject.SetActive(true);
        CanvasController.instance.botonDespliegueMenu.SetActive(false);
        CanvasController.instance.overlayPanel.gameObject.SetActive(false);
    }

    public void DesactivarPanel()
    {
        CanvasController.instance.overlayPanel.gameObject.SetActive(true);
        CanvasController.instance.botonDespliegueMenu.SetActive(true);
        gameObject.SetActive(false);
        CanvasController.instance.AgregarPanelAnterior(CanvasController.Paneles.Graficas);
    }

    public void SetPanel(string nombreEstadistica, bool isPartido, bool datosJugador) //datosJugador=true => estadisticas gobales del jugadoractual, datosJugador=false => estadisticas globales del equipo actual
    {
        _datosJugador = datosJugador;

        ///COSAS DEL GUI
        ActivarPanel();

        ejeXText.SetText("Partido", AppController.Idiomas.Español);
        ejeXText.SetText("Match", AppController.Idiomas.Ingles);

        ejeYText.text = nombreEstadistica;

        if (isPartido)
        {
            titleText.SetText("PARTIDOS", AppController.Idiomas.Español);
            titleText.SetText("MATCHES", AppController.Idiomas.Ingles);
        }
        else
        {
            titleText.SetText("PRACTICAS", AppController.Idiomas.Español);
            titleText.SetText("PRACTICES", AppController.Idiomas.Ingles);
        }


        ///OBTENER LOS DATOS PARA GRAFICAR
        if (datosJugador)
        {
            if (isPartido)
                datosGraficaPartidos = ObtenerDatosGraficaPartidos(nombreEstadistica, AppController.instance.jugadorActual.GetPartidos());
            else
                datosGraficaPartidos = ObtenerDatosGraficaPartidos(nombreEstadistica, AppController.instance.jugadorActual.GetPracticas());
        }
        else
        {
            if (isPartido)
            {
                datosGraficaPartidos = ObtenerDatosGraficaPartidos(nombreEstadistica, AppController.instance.equipoActual.GetPartidos());
                datosGraficaJugadores = ObtenerDatosGraficaJugadores(nombreEstadistica, AppController.instance.equipoActual.GetJugadores(), isPartido);
            }
            else
            {
                datosGraficaPartidos = ObtenerDatosGraficaPartidos(nombreEstadistica, AppController.instance.equipoActual.GetPracticas());
                datosGraficaJugadores = ObtenerDatosGraficaJugadores(nombreEstadistica, AppController.instance.equipoActual.GetJugadores(), isPartido);
            }
        }


        ///GRAFICAR TODAS LAS GRAFICAS
        graficaFuncionHorizontal.Graficar(datosGraficaPartidos);
        if (datosJugador) 
            graficaHistogramaHorizontal.Graficar(datosGraficaPartidos);
        else
            graficaHistogramaHorizontal.Graficar(datosGraficaJugadores);

        graficaFuncionVertical.Graficar(datosGraficaPartidos);
        if (datosJugador)
            graficaHistogramaVertical.Graficar(datosGraficaPartidos);
        else
            graficaHistogramaVertical.Graficar(datosGraficaJugadores);


        ///ESTADO INICIAL DE LAS GRAFICAS (CUAL SE MUESTRA POR PRIMERO)
        //EMPIEZO CON LAS VERTICALES
        vertical = true;
        Screen.orientation = ScreenOrientation.Portrait;
        graficasVerticales.SetActive(true);
        graficasHorizontales.SetActive(false);

        //EMPIEZO CON LA GRAFICA "FUNCION" EN TANTO VERTICAL COMO HORIZONTAL
        graficaHistogramaHorizontal.gameObject.SetActive(false);
        graficaFuncionHorizontal.gameObject.SetActive(true);
        graficaFuncionVertical.gameObject.SetActive(true);
        graficaHistogramaVertical.gameObject.SetActive(false);
        //PONGO LOS PREFABS DE LA FUNCION VERTICAL
        graficaFuncionVertical.BorrarPrefabs();
        graficaFuncionVertical.CrearPrefabs(datosGraficaPartidos, false);
    }

    public void ToggleVerticalHorizontal()
    {
        vertical = !vertical;
        if (vertical)
        {
            Screen.orientation = ScreenOrientation.Portrait;

            graficasVerticales.SetActive(true);
            graficasHorizontales.SetActive(false);

            ToggleGrafica();
        }
        else
        {
            Screen.orientation = ScreenOrientation.Landscape;

            graficasVerticales.SetActive(false);
            graficasHorizontales.SetActive(true);

            ToggleGrafica();
        }
    }

    public void ToggleGrafica()
    {
        if (!vertical)
        {
            graficaFuncionHorizontal.gameObject.SetActive(!graficaFuncionHorizontal.gameObject.activeSelf);
            graficaHistogramaHorizontal.gameObject.SetActive(!graficaHistogramaHorizontal.gameObject.activeSelf);

            if(graficaHistogramaHorizontal.gameObject.activeSelf)
            {
                ejeXText.SetText("JUGADOR", AppController.Idiomas.Español);
                ejeXText.SetText("PLAYER", AppController.Idiomas.Ingles);
            }
            else
            {
                ejeXText.SetText("PARTIDO", AppController.Idiomas.Español);
                ejeXText.SetText("MATCH", AppController.Idiomas.Ingles);
            }
        }
        else
        {
            graficaFuncionVertical.gameObject.SetActive(!graficaFuncionVertical.gameObject.activeSelf);
            graficaHistogramaVertical.gameObject.SetActive(!graficaHistogramaVertical.gameObject.activeSelf);
            if (graficaFuncionVertical.gameObject.activeSelf)
            {
                graficaFuncionVertical.BorrarPrefabs();
                graficaFuncionVertical.CrearPrefabs(datosGraficaPartidos, false);

                titleText.SetText("PARTIDOS", AppController.Idiomas.Español);
                titleText.SetText("MATCHES", AppController.Idiomas.Ingles);
            }
            else
            {
                graficaHistogramaVertical.BorrarPrefabs();
                if (_datosJugador)
                {
                    graficaHistogramaVertical.CrearPrefabs(datosGraficaPartidos, false);

                    titleText.SetText("PARTIDOS", AppController.Idiomas.Español);
                    titleText.SetText("MATCHES", AppController.Idiomas.Ingles);
                }
                else
                {
                    graficaHistogramaVertical.CrearPrefabs(datosGraficaJugadores, true);

                    titleText.SetText("JUGADORES", AppController.Idiomas.Español);
                    titleText.SetText("PLAYERS", AppController.Idiomas.Ingles);
                }
            }
        }
    }

    private Dictionary<Partido, int> ObtenerDatosGraficaPartidos(string nombreEstadistica, List<Partido> partidos)
    {
        Dictionary<Partido, int> datos = new Dictionary<Partido, int>();

        foreach (var partido in partidos)
        {
            Estadisticas estPartido = partido.GetEstadisticas();
            int[] r = estPartido.Find(nombreEstadistica);
            if (r[0] == 1)
            {
                //string key = partido.GetFecha().ToString();
                if (datos.ContainsKey(partido)) datos[partido] += r[1];
                else datos[partido] = r[1];
            }
        }

        return datos;
    }

    private Dictionary<Jugador, int> ObtenerDatosGraficaJugadores(string nombreEstadistica, List<Jugador> jugadores, bool isPartido)
    {
        Dictionary<Jugador, int> datos = new Dictionary<Jugador, int>();
        Debug.Log("Cantidad jugadores: " + jugadores.Count);
        foreach (var jugador in jugadores)
        {
            Estadisticas estGlobales = isPartido ? jugador.GetEstadisticasPartido() : jugador.GetEstadisticasPractica();
            int[] r = estGlobales.Find(nombreEstadistica);
            if (r[0] == 1)
            {
                //string key = partido.GetFecha().ToString();
                datos[jugador] = r[1];
            }
        }
        Debug.Log("datos cantidad: " + datos.Count);
        return datos;
    }

    public void VerDato(Jugador _jugador)
    {
        DesactivarPanel();
        panelJugadores.MostrarPanelPartidos(_jugador);
    }

    public void VerDato(Partido _partido)
    {
        DesactivarPanel();
        panelPartidosEquipo.MostrarPanelDetallePartido(_partido, true);
    }
}
