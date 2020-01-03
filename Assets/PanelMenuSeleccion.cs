using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelMenuSeleccion : MensajeDesplegable
{
    [SerializeField] private GameObject botonDespliegueMenu = null;

    override public void ToggleDesplegar()
    {
        base.ToggleDesplegar();
        botonDespliegueMenu.SetActive(!botonDespliegueMenu.activeSelf);
    }
}
