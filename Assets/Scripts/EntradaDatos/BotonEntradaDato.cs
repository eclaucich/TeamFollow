using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotonEntradaDato : MonoBehaviour
{
    private Text textoEntrada;
    private int cant;

    private bool holding = false;

    private void Awake()
    {
        textoEntrada = GetComponentInChildren<Text>();
        textoEntrada.text = "0";
        cant = 0;
    }

    public void SumarEntrada()
    {
        if (!holding)
        {
            cant++;
            textoEntrada.text = cant.ToString();
        }
        else holding = false;
    }

    public void RestarEntrada()
    {
        holding = true;
        cant--;
        if (cant < 0) cant = 0;
        textoEntrada.text = cant.ToString();
    }

    public int GetCantidad()
    {
        return cant;
    }
}
