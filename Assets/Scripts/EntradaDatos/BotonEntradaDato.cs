using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotonEntradaDato : MonoBehaviour
{
    private Text textoEntrada;
    private int cant;

    private void Awake()
    {
        textoEntrada = GetComponentInChildren<Text>();
        textoEntrada.text = "0";
        cant = 0;
    }

    public void SumarEntrada()
    {
        cant++;
        textoEntrada.text = cant.ToString();
    }

    public void RestarEntrada()
    {
        cant--;
        if (cant < 0) cant = 0;
        textoEntrada.text = cant.ToString();
    }

    public int GetCantidad()
    {
        return cant;
    }
}
