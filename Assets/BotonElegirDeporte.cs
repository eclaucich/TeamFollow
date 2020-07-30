using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotonElegirDeporte : MonoBehaviour
{
    [SerializeField] private Deportes.DeporteEnum deporte;
    [SerializeField] private PanelNuevoEquipo panelNuevoEquipo = null;
    [SerializeField] private Image imagenFondo = null;

    private void FixedUpdate()
    {
        if (deporte == panelNuevoEquipo.GetDeporteActual())
            imagenFondo.color = AppController.instance.colorTheme.botonSeleccionado;
        else
            imagenFondo.color = AppController.instance.colorTheme.botonActivado;
    }
    public void CambiarDeporteActual()
    {
        panelNuevoEquipo.CambiarDeporteElegido(deporte);
    }
}
