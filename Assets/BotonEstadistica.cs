using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotonEstadistica : MonoBehaviour
{
    [SerializeField] private Text nombreEstadisticaText = null;
    [SerializeField] private Text valorEstadisticaText = null;

    public void SetNombreEstadistica(string _nombre)
    {
        nombreEstadisticaText.text = _nombre;
    }

    public void SetValorEstadistica(string _valor)
    {
        valorEstadisticaText.text = _valor;
    }
}
