[System.Serializable]
public class SaveDataEquipo {

    public string nombreEquipo;
    public Deportes.DeporteEnum deporte;

    public SaveDataEquipo(Equipo equipo)
    {
        nombreEquipo = equipo.GetNombre();
        deporte = equipo.GetDeporte();
    }

    public string GetNombre()
    {
        return nombreEquipo;
    }

    public Deportes.DeporteEnum GetDeporte()
    {
        return deporte;
    }
}
