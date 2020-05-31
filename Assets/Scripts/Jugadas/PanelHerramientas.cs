using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelHerramientas : MonoBehaviour
{
    [SerializeField] private PanelCrearJugadas panelCrearJugadas = null;

    public PanelCrearJugadas GetPanelCrearJugadas()
    {
        return panelCrearJugadas;
    }

    public void ToogleActive()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    public bool isActive()
    {
        return gameObject.activeSelf;
    }
}
