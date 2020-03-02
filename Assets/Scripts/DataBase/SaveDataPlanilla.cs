[System.Serializable]
public class SaveDataPlanilla {

    public string nombrePlanilla;
    public string aliasPlanilla;
    public string nombreJugador;
    public int asistencia;

    public SaveDataPlanilla(DetalleAsistencia detalleAsistencia, string nombrePlanilla_, string aliasPlanilla_)
    {
        nombrePlanilla = nombrePlanilla_;
        aliasPlanilla = aliasPlanilla_;
        nombreJugador = detalleAsistencia.GetNombre();
        asistencia = detalleAsistencia.GetAsistencia();
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

    public int GetAsistencia()
    {
        return asistencia;
    }
}
