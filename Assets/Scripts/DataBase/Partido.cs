using System;
using System.Collections.Generic;
using System.Diagnostics;

public class Partido
{
    private string nombre;
    private Estadisticas estadisticas;
    private DateTime fecha;
    private List<Evento> eventos;
    private string posicion;
    private bool isPartido;

    public enum TipoResultadoPartido
    {
        Normal,
        Sets
    }
    private TipoResultadoPartido tipoResultado;
    private ResultadoEntradaDatos resultadoPartido;

    public Partido(string _nombre, Estadisticas _estadisticas, DateTime _fecha, bool _isPartido)
    {
        nombre = _nombre;
        estadisticas = _estadisticas;
        fecha = _fecha;
        posicion = string.Empty;
        isPartido = _isPartido;
    }

    public Partido(SaveDataPartido dataPartido, SaveDataEstadisticas dataEstadisticas, Equipo _equipo)
    {
        nombre = dataPartido.GetNombre();
        estadisticas = new Estadisticas(dataEstadisticas);
        fecha = dataPartido.GetFecha();
        posicion = string.Empty;
        isPartido = dataPartido.isPartido;

        eventos = new List<Evento>();
        foreach (var evento in dataPartido.eventos)
        {
            eventos.Add(new Evento(evento, _equipo));
        }
    }

    public Partido(SaveDataPartido dataPartido, SaveDataEstadisticas dataEstadisticas, Jugador _jugador, string _posicion)
    {
        nombre = dataPartido.GetNombre();
        estadisticas = new Estadisticas(dataEstadisticas);
        fecha = dataPartido.GetFecha();
        posicion = _posicion;
        isPartido = dataPartido.isPartido;

        eventos = new List<Evento>();
        foreach (var evento in dataPartido.eventos)
        {
            eventos.Add(new Evento(evento, _jugador));
        }
    }

    public string GetNombre()
    {
        return nombre;
    }
    public void SetPosicion(string _posicion)
    {
        posicion = _posicion;
    }
    public string GetPosicion()
    {
        return posicion;
    }
    public Estadisticas GetEstadisticas()
    {
        return estadisticas;
    }

    public DateTime GetFecha()
    {
        return fecha;
    }

    public bool IsPartido()
    {
        return isPartido;
    }

    public void BorrarEstadistica(Estadisticas estadisticas_)
    {
        estadisticas.BorrarEstadisticas(estadisticas_);
    }

    public void AgregarResultadoEntradaDatos(ResultadoEntradaDatos _resultadoPartido, TipoResultadoPartido _tipoResultado)
    {
        resultadoPartido = _resultadoPartido;
        tipoResultado = _tipoResultado;
    }

    public ResultadoEntradaDatos GetResultadoEntradaDato()
    {
        return resultadoPartido;
    }

    public TipoResultadoPartido GetTipoResultadoPartido()
    {
        return tipoResultado;
    }

    public List<Evento> GetEventos()
    {
        return eventos;
    }

    public void SetListaEventos(List<Evento> _eventos)
    {
        eventos = _eventos;
    }
}
