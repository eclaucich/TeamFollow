using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeccionBanca : MonoBehaviour
{
    [SerializeField] private GameObject prefabJugador = null;
    [SerializeField] private Transform parentPrefabs = null;
    [SerializeField] private Image contorno = null;

    [SerializeField] private Toggle toggleNombres = null;
    [SerializeField] private Toggle toggleNumeros = null;
    [SerializeField] private Toggle togglePosiciones = null;

    private List<JugadorEntradaDato> listaJugadorEntradaDato;

    private void Start()
    {
        listaJugadorEntradaDato = new List<JugadorEntradaDato>();
    }

    public void SetSeccionBanca(List<Jugador> jugadores)
    {
        contorno.gameObject.SetActive(false);

        foreach (var jugador in jugadores)
        {
            GameObject go = Instantiate(prefabJugador, parentPrefabs, false);
            go.SetActive(true);
            JugadorEntradaDato jed = go.GetComponent<JugadorEntradaDato>();
            jed.SetJugadorFocus(jugador);
            listaJugadorEntradaDato.Add(jed);
        }
    }

    public JugadorEntradaDato GetJugadorEntradaDatoInicial()
    {
        return listaJugadorEntradaDato[0];
    }

    public void SetFechaEntradaDato(DateTime _fecha)
    {
        foreach (var jed in listaJugadorEntradaDato)
        {
            jed.SetFechaEntradaDato(_fecha);
        }
    }

    public void GuardarEntradaDato(List<string> _categorias, string _nombrePartido, string _tipoEntradaDatos, DateTime _fecha, ResultadoEntradaDatos _res, List<Evento> _eventos, Partido.TipoResultadoPartido _tipoResultado, int _cantPeriodos)
    {
        foreach (var jed in listaJugadorEntradaDato)
        {
            jed.GuardarEntradaDato(_categorias, _nombrePartido, _tipoEntradaDatos, _fecha, _res, _eventos, _tipoResultado, _cantPeriodos);
        }
    }

    public void AgregarEstadisticasEquipo(Estadisticas _estEquipo)
    {
        foreach (var jed in listaJugadorEntradaDato)
        {
            jed.AgregarEstadisticasEquipo(_estEquipo);
        }
    }

    public void SetActiveContorno(bool _aux)
    {
        contorno.gameObject.SetActive(_aux);
    }

    public void ToggleNombresJugadores()
    {
        foreach (var jed in listaJugadorEntradaDato)
        {
            jed.ToggleNombre(toggleNombres.isOn);
        }
    }

    public void ToggleNumerosJugadores()
    {
        foreach (var jed in listaJugadorEntradaDato)
        {
            jed.ToggleNumero(toggleNumeros.isOn);
        }
    }

    public void TogglePosicionesJugadores()
    {
        foreach (var jed in listaJugadorEntradaDato)
        {
            jed.TogglePosicion(togglePosiciones.isOn);
        }
    }
}
