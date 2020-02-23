using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotonBase : BotonSoftball
{
    [SerializeField] private GameObject opcionesBase = null;

    public void ActivarOpcionesBase()
    {
        opcionesBase.SetActive(true);
    }

    public override void OcultarOpciones()
    {
        base.OcultarOpciones();
        opcionesBase.SetActive(false);
    }
}
