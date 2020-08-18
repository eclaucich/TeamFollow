using System;
using System.Collections.Generic;

[System.Serializable]
public class SaveDataPartido
{
    public string nombre;
    public DateTime fecha;
    public List<SaveDataEvento> eventos;

    public SaveDataPartido(Partido partido)
    {
        nombre = partido.GetNombre();
        fecha = partido.GetFecha();

        eventos = new List<SaveDataEvento>();
        foreach (var evento in partido.GetEventos())
        {
            eventos.Add(new SaveDataEvento(evento));
        }
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
