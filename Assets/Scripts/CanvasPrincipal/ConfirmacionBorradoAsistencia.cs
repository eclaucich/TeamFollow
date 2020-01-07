using UnityEngine;
using UnityEngine.UI;

public class ConfirmacionBorradoAsistencia : MensajeDesplegable
{
    private string nombreAsistencia;
    private GameObject botonAsistenciaFocus;
    [SerializeField] protected Text textoConfirmacion = null;

    public void Activar(GameObject botonAsistencia, string nombreAsistencia_)
    {
        nombreAsistencia = nombreAsistencia_;
        textoConfirmacion.text = "Borrar Asistencia \"" + nombreAsistencia_ + "\"?";
        botonAsistenciaFocus = botonAsistencia;
        ToggleDesplegar();
    }

    public void BorrarAsistencia()
    {
        GetComponentInParent<PanelHistorialPlanillas>().BorrarPlanilla(botonAsistenciaFocus, nombreAsistencia);
        Cerrar();
    }
}
