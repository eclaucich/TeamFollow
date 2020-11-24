using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotonSeleccionarCarpeta : MonoBehaviour
{
    [SerializeField] private Text nombreCarpeta = null;
    private CarpetaJugada carpeta;
    private Image imagen;

    private void Start()
    {
        imagen = GetComponent<Image>();
    }

    public void SetCarpeta(CarpetaJugada _carpeta)
    {
        carpeta = _carpeta;
        nombreCarpeta.text = carpeta.GetNombre();
    }

    public void Deseleccionar()
    {
        if (imagen == null)
            return;
        imagen.color = AppController.instance.colorTheme.detalle2;
    }

    public void Seleccionar()
    {
        if(imagen == null)
            return;
        imagen.color = AppController.instance.colorTheme.botonSeleccionado;
    }

    public CarpetaJugada GetCarpeta()
    {
        return carpeta;
    }
}
