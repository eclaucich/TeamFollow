using System;

[System.Serializable]
public class SaveDataPartido
{
    public string nombre;
    public DateTime fecha;

    public SaveDataPartido(Partido partido)
    {
        nombre = partido.GetNombre();
        fecha = partido.GetFecha();
    }

    public string GetNombre()
    {
        return nombre;
    }

    public DateTime GetFecha()
    {
        return fecha;
    }
}
