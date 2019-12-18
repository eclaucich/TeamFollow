using System;
using System.Collections.Generic;
/// <summary>
/// 
/// Clase base de un jugador
/// Cada jugador tiene un nombre (único), 
///     y estadisticas de partido y de práctica
///     
/// </summary>

public class Jugador {

    private string nombre;                                                          //Nombre de jugador (único)
    private int peso;
    private int altura;
    private InfoJugador infoJugador;

    private Estadisticas estadisticasGlobalesPartido;                                       //Estadísticas correspondientes a Partidos
    private Estadisticas estadisticasGlobalesPractica;                                      //Estadísticas correspondientes a Practicas

    private List<Partido> partidos;
    private List<Partido> practicas;

    public Jugador(/*string nombre_, int peso_, int altura_, */InfoJugador infoJugador_)                                                  //Constructor por nombre
    {
        /*nombre = nombre_;
        peso = peso_;
        altura = altura_;*/
        infoJugador = infoJugador_;

        estadisticasGlobalesPartido = new Estadisticas();
        estadisticasGlobalesPractica = new Estadisticas();

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

        estadisticasGlobalesPartido = new Estadisticas();
        estadisticasGlobalesPractica = new Estadisticas();

        partidos = new List<Partido>();
        practicas = new List<Partido>();
    }

    public void Editar(InfoJugador infoJugador_)
    {
        infoJugador.NuevaInfo(infoJugador_);
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

    public int GetPeso()
    {
        return peso;
    }

    public int GetAltura()
    {
        return altura;
    }

    public Estadisticas GetEstadisticasPartido()                                    //Devuelve las estadísticas correspondietes a Partidos
    {
        return estadisticasGlobalesPartido;
    }

    public void GuardarEntradaDato(string tipoEntradaDato, Estadisticas estadisticas, Partido partido)
    {
        if(tipoEntradaDato == "Partido")
        {
            estadisticasGlobalesPartido.AgregarEstadisticas(estadisticas);
            partidos.Add(partido);  
        }
        else
        {
            estadisticasGlobalesPractica.AgregarEstadisticas(estadisticas);
            practicas.Add(partido);
        }

        SaveSystem.GuardarEntradaDato(tipoEntradaDato, estadisticasGlobalesPartido, partido, this, AppController.instance.equipoActual);
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

    public SaveDataJugador CreateSaveData()
    {
        return new SaveDataJugador(infoJugador);
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
