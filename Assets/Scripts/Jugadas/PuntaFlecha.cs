using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuntaFlecha : MonoBehaviour
{
    public void SetMaterialColor(Color _color)
    {
        foreach(Transform child in transform)
        {
            child.gameObject.GetComponent<MeshRenderer>().material = ElegirMaterial(_color);
        }
    }

    private Material ElegirMaterial(Color _color)
    {
        if(_color.r == 1 && _color.g == 0 && _color.b == 0)
        {
            return PanelCrearJugada.instance.materialRojo;
        }
        else if(_color.r == 0 && _color.g == 0 && _color.b == 0)
        {
            return PanelCrearJugada.instance.materialNegro;
        }
        else if(_color.r == 0 && _color.b == 1)
        {
            return PanelCrearJugada.instance.materialAzul;
        }
        else if(_color.r == 1 && _color.g == 1 && _color.b == 0)
        {
            return PanelCrearJugada.instance.materialAmarillo;
        }

        return PanelCrearJugada.instance.materialNegro;
    }
}
