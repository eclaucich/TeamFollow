[System.Serializable]
public class SaveDataPlanilla {

    public string nombrePlanilla;
    public string nombreJugador;
    public bool asistencia;
    public string observacion;

    public SaveDataPlanilla(DetalleAsistencia detalleAsistencia, string nombrePlanilla_)
    {
        nombrePlanilla = nombrePlanilla_;
        nombreJugador = detalleAsistencia.GetNombre();
        asistencia = detalleAsistencia.GetAsistencia();
        observacion = detalleAsistencia.GetObservacion();
    }

    public string GetNombrePlanilla()
    {
        return nombrePlanilla;
    }

    public string GetNombreJugador()
    {
        return nombreJugador;
    }

    public bool GetAsistencia()
    {
        return asistencia;
    }

    public string GetObservacion()
    {
        return observacion;
    }
}
