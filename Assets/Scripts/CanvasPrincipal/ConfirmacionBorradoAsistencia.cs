using UnityEngine;
using UnityEngine.UI;

public class ConfirmacionBorradoAsistencia : MensajeDesplegable
{
    private BotonHistorialAsistencia botonAsistenciaFocus;
    [SerializeField] private PanelHistorialPlanillas panelHistorialAsistencias = null;

    public void Activar(BotonHistorialAsistencia botonFocus)
    {
        botonAsistenciaFocus = botonFocus;
        //textoConfirmacion.text = "Borrar Asistencia \"" + botonFocus.GetDisplayNombre() + "\"?";
        text.SetText("Borrar Asistencia \"" + botonFocus.GetDisplayNombre() + "\"?".ToUpper(), AppController.Idiomas.Español);
        text.SetText("Delete assitence form \"" + botonFocus.GetDisplayNombre() + "\"?".ToUpper(), AppController.Idiomas.Ingles);
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
