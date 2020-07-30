using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeccionGuardarJugada : MonoBehaviour
{
    [SerializeField] private BotonNormal botonCategoriaAtaque = null;
    [SerializeField] private BotonNormal botonCategoriaDefensa = null;
    [SerializeField] private BotonNormal botonSinCategoria = null;

    private void Start()
    {
        SetCategoriaActual(0);
    }

    public void SetCategoriaActual(int i)
    {
        botonSinCategoria.SetColorActivado();
        botonCategoriaAtaque.SetColorActivado();
        botonCategoriaDefensa.SetColorActivado();

        if (i == 0) botonSinCategoria.SetColorSeleccionado();
        else if (i == 1) botonCategoriaAtaque.SetColorSeleccionado();
        else if (i == 2) botonCategoriaDefensa.SetColorSeleccionado();
    }

}
