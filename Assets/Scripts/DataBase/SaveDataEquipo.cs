
[System.Serializable]
public class SaveDataEquipo {

    public string nombreEquipo;
    public string deporte;

    public SaveDataEquipo(Equipo equipo)
    {
        nombreEquipo = equipo.GetNombre();
        deporte = equipo.GetDeporte();
    }


    public string GetNombre()
    {
        return nombreEquipo;
    }

    public string GetDeporte()
    {
        return deporte;
    }
}
