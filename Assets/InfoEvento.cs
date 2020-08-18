using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoEvento : MonoBehaviour
{
    [SerializeField] private Text nombreJugadorText = null;
    [SerializeField] private Text tiempoText = null;
    [SerializeField] private Text tipoEstadisticaText = null;

    private bool infoSetted;

    private void Start()
    {
        gameObject.SetActive(false);
        infoSetted = false;
    }

    public void SetInfoEvento(string _nombreJugador, string _tiempo, string _tipoEstadistica)
    {
        if (!infoSetted)
        {
            nombreJugadorText.text = _nombreJugador;
            tiempoText.text = _tiempo;
            tipoEstadisticaText.text = _tipoEstadistica;

            infoSetted = true;
        }
    }

    public void OnMouseUp()
    {
        infoSetted = false;
        gameObject.SetActive(false);
    }
}
