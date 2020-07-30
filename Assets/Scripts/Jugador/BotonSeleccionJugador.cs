﻿using UnityEngine;
using UnityEngine.UI;

public class BotonSeleccionJugador : MonoBehaviour
{
    private bool seleccionado = false;

    public void ToggleSeleccionado()
    {
        seleccionado = !seleccionado;

        if (seleccionado) GetComponent<BotonNormal>().SetColorSeleccionado();
        else GetComponent<BotonNormal>().SetColorActivado();
    }

    public bool isSeleccionado()
    {
        return seleccionado;
    }
}
