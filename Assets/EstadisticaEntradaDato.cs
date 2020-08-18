using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EstadisticaEntradaDato : MonoBehaviour
{
    [SerializeField] private Text inicialEstadisticaText = null;
    [SerializeField] private Text nombreEstadisticaText = null;
    [SerializeField] private Text valorEstadisticaText = null;
    [SerializeField] private GameObject goNombreEstadistica = null;

    [SerializeField] private SeccionEstadisticas seccionEstadisticas = null;

    private EstadisticaDeporte.Estadisticas tipoEstadistica;
    private int valorEstadistica;

    public void Initiate(EstadisticaDeporte.Estadisticas _tipoEstadistica, string _nombre, string _inicial)
    {
        goNombreEstadistica.SetActive(false);

        tipoEstadistica = _tipoEstadistica;
        nombreEstadisticaText.text = _nombre;
        inicialEstadisticaText.text = _inicial;
        valorEstadistica = 0;
        valorEstadisticaText.text = valorEstadistica.ToString();
    }

    public void AgregarEstadisticasJugador()
    {
        valorEstadistica++;
        valorEstadisticaText.text = valorEstadistica.ToString();
        seccionEstadisticas.AgregarEstadisticasJugadorFocus(tipoEstadistica, nombreEstadisticaText.text, 1);
    }

    public void EliminarEstadisticasJugador()
    {
        valorEstadistica--;
        valorEstadisticaText.text = valorEstadistica.ToString();
        seccionEstadisticas.AgregarEstadisticasJugadorFocus(tipoEstadistica, nombreEstadisticaText.text, -1);
    }

    public void SetValor(JugadorEntradaDato _jed)
    {
        valorEstadistica = _jed.GetValorEstadistica(nombreEstadisticaText.text);
        valorEstadisticaText.text = valorEstadistica.ToString();
    }

    public void OnPress()
    {
        goNombreEstadistica.SetActive(true);
    }

    public void OnRelease()
    {
        goNombreEstadistica.SetActive(false);
    }
}
