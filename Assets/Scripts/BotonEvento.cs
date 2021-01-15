using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BotonEvento : MonoBehaviour
{
    private string nombreJugadorText;
    private string periodText;
    private string tiempoText;
    private string tipoEventoText;

    [SerializeField] private GraficaResumen graficaResumen = null;
    [SerializeField] private InfoEvento infoEvento = null;
    [SerializeField] private Image icono = null;
    [SerializeField] private Color colorBase;
    [SerializeField] private Color colorSeleccionado;

    private Evento eventoFocus;

    private void Start() 
    {
        icono.color = colorBase;
    }

    public void SetEventoFocus(Evento _evento)
    {
        eventoFocus = _evento;
        nombreJugadorText = eventoFocus.GetAutor().GetNombre();
        periodText = _evento.GetPeriod().ToString() + "°";
        SetTimeText(eventoFocus.GetTiempo());
        tipoEventoText = eventoFocus.GetNombreTipo();
    }

    public void SetInfoEvento()
    {
        infoEvento.SetInfoEvento(nombreJugadorText, periodText, tiempoText, tipoEventoText);
        icono.color = colorSeleccionado;
        graficaResumen.SetEventoFocus(this);
    }

    private void SetTimeText(float _time)
    {
        int sec = (int)_time % 60;
        int min = (int)Mathf.Floor(_time / 60f);

        if (min < 10)
            tiempoText = "0";
        tiempoText += min + " : ";

        if (sec < 10)
            tiempoText += "0";
        tiempoText += sec;
    }

    public void SetColorBase()
    {
        icono.color = colorBase;
    }
}
