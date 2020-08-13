using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotonDatoGrafica : MonoBehaviour
{
    [SerializeField] private Text nombreDato = null;
    [SerializeField] private Text valorDato = null;

    [SerializeField] private PanelGraficoEstadistica panelGraficoEstadistica = null;

    private bool isDatoJugador = false;
    private Partido _partidoFocus;
    private Jugador _jugadorFocus;

    public void SetDato<T>(T key, int value, bool _isDatoJugador)
    {
        if (_isDatoJugador)
        {
            _jugadorFocus = ((Jugador)Convert.ChangeType(key, typeof(T)));
            nombreDato.text = _jugadorFocus.GetNombre();
        }
        else
        {
            _partidoFocus = ((Partido)Convert.ChangeType(key, typeof(T)));
            nombreDato.text = _partidoFocus.GetNombre();
        }
        valorDato.text = value.ToString();

        isDatoJugador = _isDatoJugador;
    }

    public void VerDato()
    {
        if (isDatoJugador)
        {
            Debug.Log("DATO JUGADOR");
            panelGraficoEstadistica.VerDato(_jugadorFocus);
        }
        else
            panelGraficoEstadistica.VerDato(_partidoFocus);
    }
}
