﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
/// <summary>
/// 
/// Clase base de un jugador
/// Cada jugador tiene un nombre (único), 
///     y estadisticas de partido y de práctica
///     
/// </summary>

public class Jugador {

    private string nombre;                                                          //Nombre de jugador (único)
    private InfoJugador infoJugador;
    private Deportes.DeporteEnum deporte;

    private Estadisticas estadisticasGlobalesPartido;                                       //Estadísticas correspondientes a Partidos
    private Estadisticas estadisticasGlobalesPractica;                                      //Estadísticas correspondientes a Practicas

    private List<Partido> partidos;
    private List<Partido> practicas;

    public Jugador(/*string nombre_, int peso_, int altura_, */InfoJugador infoJugador_, Deportes.DeporteEnum deporte_)                                                  //Constructor por nombre
    {
        /*nombre = nombre_;
        peso = peso_;
        altura = altura_;*/
        infoJugador = infoJugador_;
        deporte = deporte_;

        estadisticasGlobalesPartido = new Estadisticas(deporte_);
        estadisticasGlobalesPractica = new Estadisticas(deporte_);

        partidos = new List<Partido>();
        practicas = new List<Partido>();
    }

    public Jugador(SaveDataJugador dataJugador)//, SaveDataEstadisticas dataPartido, SaveDataEstadisticas dataPractica)
    {
        /*nombre = dataJugador.GetNombre();
        peso = dataJugador.GetPeso();
        altura = dataJugador.GetAltura();*/
        /*estadisticasGlobalesPartido = new Estadisticas(dataPartido);
        estadisticasGlobalesPractica = new Estadisticas(dataPractica);*/

        infoJugador = new InfoJugador(dataJugador); //CAMBIAR ESTO, EL SAVE DATA JUGADOR TIENE QUE GUARDAR UN INFOJUGADOR

        estadisticasGlobalesPartido = new Estadisticas(dataJugador.deporte);
        estadisticasGlobalesPractica = new Estadisticas(dataJugador.deporte);

        partidos = new List<Partido>();
        practicas = new List<Partido>();
    }

    public void Editar(InfoJugador infoJugador_)
    {
        SaveSystem.EditarJugador(this, AppController.instance.equipoActual, infoJugador_.GetNombre());
        infoJugador.NuevaInfo(infoJugador_);
        SaveSystem.EditarInfoJugador(this, AppController.instance.equipoActual);
    }

    public InfoJugador GetInfoJugador()
    {
        return infoJugador;
    }

    public void CargarEstadisticasGlobalesPartido(Estadisticas estadisticas)    //SOLO LLAMADA DESDE EL LOAD SYSTEM PARA CARGAR LOS DATOS
    {
        estadisticasGlobalesPartido = estadisticas;
    }

    public void CargarEstadisticasGlobalesPractica(Estadisticas estadisticas)  //SOLO LLAMADA DESDE EL LOAD SYSTEM PARA CARGAR LOS DATOS
    {
        estadisticasGlobalesPractica = estadisticas;
    }

    public void CargarPartido(Partido partido)                                //SOLO LLAMADA DESDE EL LOAD SYSTEM PARA CARGAR LOS DATOS     
    {
        partidos.Add(partido);
    }

    public void CargarPractica(Partido partido)                                //SOLO LLAMADA DESDE EL LOAD SYSTEM PARA CARGAR LOS DATOS     
    {
        practicas.Add(partido);
    }

    public string GetNombre()                                                       //Devuelve el nombre de jugador
    {
        return infoJugador.GetNombre();
    }
    public string GetNumeroCamiseta()
    {
        return infoJugador.GetNumeroCamiseta();
    }
    public Estadisticas GetEstadisticasPartido()                                    //Devuelve las estadísticas correspondietes a Partidos
    {
        return estadisticasGlobalesPartido;
    }

    public void GuardarEntradaDato(string tipoEntradaDato, Estadisticas estadisticas, string _nombrePartido, DateTime _fecha, ResultadoEntradaDatos _res, List<Evento> _eventos, Partido.TipoResultadoPartido _tipoResultado, int _cantPeriodos)
    {
        bool _isPartido = tipoEntradaDato == "Partido";

        Partido _partido = new Partido(_nombrePartido, estadisticas, _fecha, _isPartido, _cantPeriodos);
        _partido.AgregarResultadoEntradaDatos(_res, _tipoResultado);
        _partido.SetListaEventos(_eventos);
        _partido.SetPosicion(GetPosicionActual());

        if(_isPartido)
        {
            estadisticasGlobalesPartido.AgregarEstadisticas(estadisticas);
            partidos.Add(_partido);
            SaveSystem.GuardarEntradaDato(tipoEntradaDato, estadisticasGlobalesPartido, _partido, this, AppController.instance.equipoActual);
        }
        else
        {
            estadisticasGlobalesPractica.AgregarEstadisticas(estadisticas);
            practicas.Add(_partido);
            SaveSystem.GuardarEntradaDato(tipoEntradaDato, estadisticasGlobalesPractica, _partido, this, AppController.instance.equipoActual);
        }
    }

    /*public void SetEstadisticas(Estadisticas estadisticas_, string tipoEntradaDatos_)
    {
        if (tipoEntradaDatos_ == "Partido" || tipoEntradaDatos_ == "partido")
        {
            estadisticasPartido.AgregarEstadisticas(estadisticas_);
            SaveSystem.GuardarEntradaDatoPartido(estadisticasPartido, this, AppController.instance.equipoActual);
        }
        else
        {
            estadisticasPractica.AgregarEstadisticas(estadisticas_);
            SaveSystem.GuardarEntradaDatoPractica(estadisticasPartido, this, AppController.instance.equipoActual);
        }
    }*/

    public void AgregarPartido(Partido _partido, string tipoEntradaDato)
    {
        if (tipoEntradaDato == "Partido") partidos.Add(_partido);
        else                              practicas.Add(_partido);
    }

    public Partido BuscarPartido(bool isPartido, string nombrePartido)
    {
        if (isPartido)
        {
            foreach (var partido in partidos)
            {
                if (partido.GetNombre() == nombrePartido) return partido;
            }
        }
        else
        {
            foreach (var practica in practicas)
            {
                if (practica.GetNombre() == nombrePartido) return practica;
            }
        }

        return null;
    }

    public void BorrarEstadisticaPartido(Partido partido)
    {
        estadisticasGlobalesPartido.BorrarEstadisticas(partido.GetEstadisticas());
        SaveSystem.GuardarEstadisticasGlobales("Partido", estadisticasGlobalesPartido, this, AppController.instance.equipoActual);
    }

    public void BorrarEstadisticaPractica(Partido practica)
    {
        estadisticasGlobalesPractica.BorrarEstadisticas(practica.GetEstadisticas());
        SaveSystem.GuardarEstadisticasGlobales("Practica", estadisticasGlobalesPractica, this, AppController.instance.equipoActual);
    }

    public void BorrarPartido(bool isPartido, string nombrePartido)
    {
        Partido partido = BuscarPartido(isPartido, nombrePartido);

        if (partido != null)
        {
            if (isPartido)
            {
                BorrarEstadisticaPartido(partido);
                partidos.Remove(partido);
            }
            else
            {
                BorrarEstadisticaPractica(partido);
                practicas.Remove(partido);
            }

            SaveSystem.BorrarPartido(isPartido, partido, this, AppController.instance.equipoActual);
        }
    }

    public bool ContienePartido(string tipoEntradaDato, string nombrePartido)
    {
        if (tipoEntradaDato == "Partido")
        {
            foreach (var partido in partidos)
            {
                if (partido.GetNombre() == nombrePartido) return true;
            }
            return false;
        }
        else
        {
            foreach (var practica in practicas)
            {
                if (practica.GetNombre() == nombrePartido) return true;
            }
            return false;
        }
    }

    public List<Partido> GetPartidos()
    {
        return partidos;
    }

    public List<Partido> GetPracticas()
    {
        return practicas;
    }


    /*public void SetEstadisticasPartido(Estadisticas estadisticas)                   //Agrega a las estadísticas de Partidos nuevas entradas
    {
        estadisticasPartido.AgregarEstadisticas(estadisticas);
        SaveSystem.GuardarEntradaDatoPartido(estadisticasPartido, this, AppController.instance.GetEquipoActual());
    }*/

    public Estadisticas GetEstadisticasPractica()                                    //Devuelve las estadísticas correspondietes a Practicas
    {
        return estadisticasGlobalesPractica;
    }

    /*public void SetEstadisticasPractica(Estadisticas estadisticas)                   //Agrega a las estadísticas de Practicas nuevas entradas
    {
        estadisticasPractica.AgregarEstadisticas(estadisticas);
        //SaveSystem.GuardarEntradaDatoPractica(estadisticasPractica.AgregarEstadisticas(estadisticas), this, AppController.instance.GetEquipoActual());
    }*/

    public string GetPosicionActual()
    {
        return infoJugador.GetPosicion();
    }

    public SaveDataJugador CreateSaveData()
    {
        return new SaveDataJugador(infoJugador, deporte);
    }

    public SaveDataEstadisticas CreateSaveDataEstadisticasPartido()
    {
        return new SaveDataEstadisticas(estadisticasGlobalesPartido);
    }

    public SaveDataEstadisticas CreateSaveDataEstadisticasPractica()
    {
        return new SaveDataEstadisticas(estadisticasGlobalesPractica);
    }
}
