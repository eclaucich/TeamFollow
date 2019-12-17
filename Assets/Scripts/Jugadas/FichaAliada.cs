using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FichaAliada : ObjetoEdicion
{

    public void Start()
    {
        Physics.queriesHitTriggers = enabled;
    }

}
