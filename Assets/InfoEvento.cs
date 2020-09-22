using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoEvento : MonoBehaviour
{
    [SerializeField] private Text nombreJugadorText = null;
    [SerializeField] private Text periodoText = null;
    [SerializeField] private Text tiempoText = null;
    [SerializeField] private Text tipoEstadisticaText = null;


    public void SetInfoEvento(string _nombreJugador, string _periodo, string _tiempo, string _tipoEstadistica)
    {
        Debug.Log("SETTED: " + _tipoEstadistica + " " + _nombreJugador);
        nombreJugadorText.text = _nombreJugador;
        periodoText.text = _periodo;
        tiempoText.text = _tiempo;
        tipoEstadisticaText.text = _tipoEstadistica;
    }
}
