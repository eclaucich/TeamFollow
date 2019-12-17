using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstadisticasGlobalesFutbol : PanelEstadisticasGlobalesEquipo
{
    public override void Start()
    {
        diccEstadisticas = new Dictionary<string, int>();
    }

}
