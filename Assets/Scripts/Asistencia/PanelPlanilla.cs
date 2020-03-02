using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelPlanilla : PanelAsistencia {

    [SerializeField] private Text nombrePlanillaText = null;
    [SerializeField] private PanelHistorialPlanillas panelHistorialPlanillas = null;
    [SerializeField] private ConfirmacionBorradoAsistencia confirmacionBorradoAsistencia = null;

    private BotonHistorialAsistencia botonFocus;

    public void SetPanelPlanilla(BotonHistorialAsistencia botonFocus_)
    {
        botonFocus = botonFocus_;

        detalles = AppController.instance.GetEquipoActual().GetPlanillaWithName(botonFocus.GetNombre()).GetDetalles();
        cantidadHojas = Mathf.CeilToInt(detalles.Count / 13f);

        base.SetPanelPlanilla();

        AppController.instance.overlayPanel.SetNombrePanel(botonFocus_.GetDisplayNombre());

        CrearPrefabsHoja();

        if (botonFocus_.GetAlias() != "") nombrePlanillaText.text = botonFocus_.GetFecha();
        else nombrePlanillaText.text = "";
    }

    public void BorrarAsistencia()
    {
        confirmacionBorradoAsistencia.Activar(botonFocus);
    }
}
