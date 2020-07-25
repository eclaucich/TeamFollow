using System;
using System.Diagnostics;

public class Partido
{
    private string nombre;
    private Estadisticas estadisticas;
    private DateTime fecha;

    public Partido(string _nombre, Estadisticas _estadisticas, DateTime _fecha)
    {
        nombre = _nombre;
        estadisticas = _estadisticas;
        fecha = _fecha;
    }

    public Partido(SaveDataPartido dataPartido, SaveDataEstadisticas dataEstadisticas)
    {
        nombre = dataPartido.GetNombre();
        estadisticas = new Estadisticas(dataEstadisticas);
        fecha = dataPartido.GetFecha();
    }

    public string GetNombre()
    {
        return nombre;
    }

    public Estadisticas GetEstadisticas()
    {
        return estadisticas;
    }

    public DateTime GetFecha()
    {
        return fecha;
    }
    public void BorrarEstadistica(Estadisticas estadisticas_)
    {
        estadisticas.BorrarEstadisticas(estadisticas_);
    }
}
