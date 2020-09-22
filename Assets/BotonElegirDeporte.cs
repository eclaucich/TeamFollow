using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotonElegirDeporte : MonoBehaviour
{
    [SerializeField] private Deportes.DeporteEnum deporte;
    [SerializeField] private PanelNuevoEquipo panelNuevoEquipo = null;
    [SerializeField] private Image imagenFondo = null;
    [SerializeField] private Image iconoDeporte = null;

    private void FixedUpdate()
    {
        if (deporte == panelNuevoEquipo.GetDeporteActual())
        {
            imagenFondo.color = AppController.instance.colorTheme.botonSeleccionado;
            iconoDeporte.color = AppController.instance.colorTheme.icon;
        }
        else
        {
            imagenFondo.color = AppController.instance.colorTheme.secundario;
            iconoDeporte.color = AppController.instance.colorTheme.detalle1;
        }
    }
    public void CambiarDeporteActual()
    {
        panelNuevoEquipo.CambiarDeporteElegido(deporte);
    }
}
