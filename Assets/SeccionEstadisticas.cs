using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class SeccionEstadisticas : MonoBehaviour
{
    [SerializeField] private RelojEntradaDatos relojEntradaDatos = null;
    [SerializeField] private Text nombreJugadorText = null;
    [SerializeField] private GameObject prefabEstadistica = null;
    [SerializeField] private Transform parentPrefabs = null;

    private JugadorEntradaDato jugadorEntradaDatoFocus;

    private List<EstadisticaEntradaDato> listaEstadisticaEntradaDatos;
    private List<Evento> eventos;

    private void Start()
    {
        listaEstadisticaEntradaDatos = new List<EstadisticaEntradaDato>();
        eventos = new List<Evento>();
    }

    public void SetSeccionEstadisticas(List<EstadisticaDeporte.Estadisticas> _estadisticas, List<string> _nombres, List<string> _iniciales)
    {
        nombreJugadorText.text = "";

        for (int i=0; i<_estadisticas.Count; i++)
        {
            if (_estadisticas[i] >= 0)
            {
                GameObject go = Instantiate(prefabEstadistica, parentPrefabs, false);
                go.SetActive(true);
                EstadisticaEntradaDato eed = go.GetComponent<EstadisticaEntradaDato>();
                eed.Initiate(_estadisticas[i], _nombres[i], _iniciales[i]);
                listaEstadisticaEntradaDatos.Add(eed);
            }
        }
    }

    public void SetJugadorEntradaDatoFocus(JugadorEntradaDato _jugadorEntradaDato)
    {
        jugadorEntradaDatoFocus = _jugadorEntradaDato;
        nombreJugadorText.text = _jugadorEntradaDato.GetNombreJugador();
        SetPrefabsEstadisticaEntradaDato();
    }

    private void SetPrefabsEstadisticaEntradaDato()
    {
        foreach (var eed in listaEstadisticaEntradaDatos)
        {
            eed.SetValor(jugadorEntradaDatoFocus);
        }
    }

    public void AgregarEstadisticasJugadorFocus(EstadisticaDeporte.Estadisticas _tipoEstadistica, string _categoria, int cant)
    {
        jugadorEntradaDatoFocus.AgregarEstadistica(_categoria, cant);

        if(cant>=0)
            eventos.Add(new Evento(_tipoEstadistica, jugadorEntradaDatoFocus.GetJugador(), relojEntradaDatos.GetCurrentPeriod(), relojEntradaDatos.GetCurrentTime()));
        else
        {
            Evento _evento = null;
            foreach (var evento in eventos)
            {
                if (evento.GetTipoEstadistica() == _tipoEstadistica && evento.GetAutor()==jugadorEntradaDatoFocus.GetJugador())
                    _evento = evento;
            }
            if(_evento != null)
            {
                eventos.Remove(_evento);
            }
        }
    }

    public void AgregarEventoCambioJugador(Jugador _jugador, bool _meterJugador)
    {
        if (_meterJugador)
            eventos.Add(new Evento(EstadisticaDeporte.Estadisticas.MeterJugador, _jugador, relojEntradaDatos.GetCurrentPeriod(), relojEntradaDatos.GetCurrentTime()));
        else
            eventos.Add(new Evento(EstadisticaDeporte.Estadisticas.SacarJugador, _jugador, relojEntradaDatos.GetCurrentPeriod(), relojEntradaDatos.GetCurrentTime()));
    }

    public List<Evento> GetListaEventos()
    {
        return eventos;
    }
}
