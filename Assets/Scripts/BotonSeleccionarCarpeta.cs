using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotonSeleccionarCarpeta : MonoBehaviour
{
    [SerializeField] private Text nombreCarpeta = null;
    private CarpetaJugada carpeta;

    public void SetCarpeta(CarpetaJugada _carpeta)
    {
        carpeta = _carpeta;
        nombreCarpeta.text = carpeta.GetNombre();
    }

    public void Deseleccionar()
    {
        GetComponent<BotonNormal>().SetColorActivado();
    }

    public void Seleccionar()
    {
        Debug.Log("SELECCIONADO");
        GetComponent<BotonNormal>().SetColorSeleccionado();
    }

    public CarpetaJugada GetCarpeta()
    {
        return carpeta;
    }
}
