using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelCrearTablero : PanelCrearJugadas
{
    public static PanelCrearTablero instance = null;

    private void Start()
    {
        if (instance != null)
        {
            Destroy(instance);
        }

        instance = this;
    }
}
