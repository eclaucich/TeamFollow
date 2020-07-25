using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelGraficoEstadistica : MonoBehaviour
{
    [SerializeField] private Text ejeXText= null;
    [SerializeField] private Text ejeYText = null;

    [SerializeField] private GraficaEstadistica graficaFuncion = null;
    [SerializeField] private GraficaHistograma graficaHistograma = null;

    public enum Graficas
    {
        Funcion = 0,
        Histograma = 1,
        Torta = 2
    };

    public void SetPanel(string nombreEstadistica, bool isPartido, bool datosJugador) //datosJugador=true => estadisticas gobales del jugadoractual, datosJugador=false => estadisticas globales del equipo actual
    {
        ejeXText.text = "Partido";
        ejeYText.text = nombreEstadistica;

        Dictionary<Partido, int> datosGraficaPartidos;
        Dictionary<Jugador, int> datosGraficaJugadores;

        /*if (datosJugador)
        {
            if (partido)
                datosGraficaJugadores = ObtenerDatosGraficaJugadores(nombreEstadistica, AppController.instance.jugadorActual.GetPartidos());
            else
                datosGraficaJugadores = ObtenerDatosGraficaJugadores(nombreEstadistica, AppController.instance.jugadorActual.GetPracticas());
        }
        else*/
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

        graficaFuncion.Graficar(datosGraficaPartidos);
        graficaHistograma.Graficar(datosGraficaJugadores);

        ActivarGrafica(Graficas.Funcion);
    }

    public void ToggleGrafica()
    {
        graficaFuncion.gameObject.SetActive(!graficaFuncion.gameObject.activeSelf);
        graficaHistograma.gameObject.SetActive(!graficaHistograma.gameObject.activeSelf);
    }
    public void ActivarGrafica(Graficas grafica)
    {
        graficaFuncion.gameObject.SetActive(false);
        graficaHistograma.gameObject.SetActive(false);

        if (grafica == Graficas.Funcion)
            graficaFuncion.gameObject.SetActive(true);
        else if (grafica == Graficas.Histograma)
            graficaHistograma.gameObject.SetActive(true);
    }
    private Dictionary<Partido, int> ObtenerDatosGraficaPartidos(string nombreEstadistica, List<Partido> partidos)
    {
        Dictionary<Partido, int> datos = new Dictionary<Partido, int>();

        foreach (var partido in partidos)
        {
            Estadisticas estPartido = partido.GetEstadisticas();
            int[] r = estPartido.Find(nombreEstadistica);
            if (r[0]==1)
            {
                //string key = partido.GetFecha().ToString();
                if(datos.ContainsKey(partido)) datos[partido] += r[1];
                else                           datos[partido] = r[1];
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
}
