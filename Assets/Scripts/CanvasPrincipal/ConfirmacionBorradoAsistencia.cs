using UnityEngine;
using UnityEngine.UI;

public class ConfirmacionBorradoAsistencia : MensajeDesplegable
{
    private BotonHistorialAsistencia botonAsistenciaFocus;
    [SerializeField] protected Text textoConfirmacion = null;
    [SerializeField] private PanelHistorialPlanillas panelHistorialAsistencias = null;

    public void Activar(BotonHistorialAsistencia botonFocus)
    {
        botonAsistenciaFocus = botonFocus;
        textoConfirmacion.text = "Borrar Asistencia \"" + botonFocus.GetDisplayNombre() + "\"?";
        ToggleDesplegar();
    }

    public void BorrarAsistencia()
    {
        ToggleDesplegar();
        AppController.instance.equipoActual.BorrarAsistencia(botonAsistenciaFocus.GetNombre());
        panelHistorialAsistencias.BorrarPlanilla(botonAsistenciaFocus);
        CanvasController.instance.MostrarPanelAnterior();
    }
}
