[System.Serializable]
public class SaveDataPlanilla {

    public string nombrePlanilla;
    public string aliasPlanilla;
    public string nombreJugador;
    public bool asistencia;
    public string observacion;

    public SaveDataPlanilla(DetalleAsistencia detalleAsistencia, string nombrePlanilla_, string aliasPlanilla_)
    {
        nombrePlanilla = nombrePlanilla_;
        aliasPlanilla = aliasPlanilla_;
        nombreJugador = detalleAsistencia.GetNombre();
        asistencia = detalleAsistencia.GetAsistencia();
        observacion = detalleAsistencia.GetObservacion();
    }

    public string GetNombrePlanilla()
    {
        return nombrePlanilla;
    }

    public string GetAliasPlanilla()
    {
        return aliasPlanilla;
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
