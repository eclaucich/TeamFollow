using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BotonEvento : MonoBehaviour
{
    [SerializeField] private Image imagen = null;
    [SerializeField] private Text nombreJugadorText = null;
    [SerializeField] private Text periodText = null;
    [SerializeField] private Text tiempoText = null;
    [SerializeField] private Text tipoEventoText = null;

    [SerializeField] private InfoEvento infoEvento = null;

    private Evento eventoFocus;

    public void SetEventoFocus(Evento _evento)
    {
        eventoFocus = _evento;
        imagen.sprite = eventoFocus.GetSprite();
        nombreJugadorText.text = eventoFocus.GetAutor().GetNombre();
        periodText.text = _evento.GetPeriod().ToString() + "°";
        SetTimeText(eventoFocus.GetTiempo());
        tipoEventoText.text = eventoFocus.GetNombreTipo();
    }

    public void SetInfoEvento()
    {
        infoEvento.SetInfoEvento(nombreJugadorText.text, tiempoText.text, tipoEventoText.text);
    }

    private void SetTimeText(float _time)
    {
        int sec = (int)_time % 60;
        int min = (int)Mathf.Floor(_time / 60f);

        if (min < 10)
            tiempoText.text = "0";
        tiempoText.text += min + " : ";

        if (sec < 10)
            tiempoText.text += "0";
        tiempoText.text += sec;
    }
}
