using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// Clase base de Equipos
/// Tienen un nombre (único), una lista de jugadores,
///     estadísticas de partido y de práctica
/// 
/// </summary>

public class Equipo {

    private string nombre;                                                  //Nombre del Equipo
    private string deporteNombre;
    private Deportes.Deporte deporte;

    private List<Jugador> jugadores;                                        //Lista de jugadores

    //public Dictionary<string, List<DetalleAsistencia>> planillasAsistencia;

    private List<PlanillaAsistencia> planillasAsistencia;

    private Estadisticas estadisticasGlobalesPartido;                               //Estadísticas correspondientes a Partidos
    private Estadisticas estadisticasGlobalesPractica;                              //Estadísticas correspondientes a Prácticas

    private List<Partido> partidos;
    private List<Partido> practicas;

    public Equipo(string nombre_, string deporte_)                                           //Constructor por nombre
    {
        nombre = nombre_;
        deporteNombre = deporte_;
        deporte = GetDeporte(deporte_);

        jugadores = new List<Jugador>();
        estadisticasGlobalesPartido = new Estadisticas();
        estadisticasGlobalesPractica = new Estadisticas();
        //planillasAsistencia = new Dictionary<string, List<DetalleAsistencia>>();
        planillasAsistencia = new List<PlanillaAsistencia>();

        partidos = new List<Partido>();
        practicas = new List<Partido>();
    }

    public Equipo(SaveDataEquipo saveData)//{, SaveDataEstadisticas saveDataEstPartido, SaveDataEstadisticas saveDataEstPractica)
    {
        nombre = saveData.GetNombre();
        deporteNombre = saveData.GetDeporte();
        deporte = GetDeporte(deporteNombre);

        /*estadisticasGlobalesPartido = new Estadisticas(saveDataEstPartido);
        estadisticasGlobalesPractica = new Estadisticas(saveDataEstPractica);*/

        estadisticasGlobalesPartido = new Estadisticas();
        estadisticasGlobalesPractica = new Estadisticas();

        jugadores = new List<Jugador>();
        //planillasAsistencia = new Dictionary<string, List<DetalleAsistencia>>();
        planillasAsistencia = new List<PlanillaAsistencia>();

        partidos = new List<Partido>();
        practicas = new List<Partido>();
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

    public void CargarPractica(Partido partido)                               //SOLO LLAMADA DESDE EL LOAD SYSTEM PARA CARGAR LOS DATOS     
    {
        practicas.Add(partido);
    }

    public void CargarJugador(Jugador jugador)                                //SOLO LLAMADA DESDE EL LOAD SYSTEM PARA CARGAR LOS DATOS
    {
        jugadores.Add(jugador);
    }

    public void NuevoJugador(InfoJugador info)                               //Agregar nuevo jugador a la lista de jugadores
    { 
        Jugador newJug = new Jugador(info);
        SaveSystem.GuardarJugador(newJug, this);
        
        jugadores.Add(newJug);
        foreach (var j in jugadores)
        {
            Debug.Log("DESPUES: " + j.GetNombre());
        }
    }

    public Jugador BuscarPorNombre(string nombre)                           //Devuelve un Jugador, buscado por nombre
    {
        for (int i = 0; i < jugadores.Count; i++)
        {
            if(jugadores[i].GetNombre() == nombre)
            {
                return jugadores[i];
            }
        }

        return null;
    }

    public void BorrarJugador(string nombre, bool borrarEstadisticas)                                //Eliminar un jugador de la lista
    {
        Jugador jugador = BuscarPorNombre(nombre);
        if (jugador != null)
        {
            if (borrarEstadisticas)
            {
                foreach (var partido in jugador.GetPartidos())
                {
                    BorrarEstadisticaPartido(partido);
                }
                foreach (var practica in jugador.GetPracticas())
                {
                    BorrarEstadisticaPractica(practica);
                }
            }
            SaveSystem.BorrarJugador(nombre, this);
            jugadores.Remove(jugador);
        }
    }

    public void BorrarPartido(bool isPartido, string nombrePartido)
    {
        Partido partido = BuscarPartido(isPartido, nombrePartido);

        if (partido != null)
        {
            if (isPartido)
            {
                estadisticasGlobalesPartido.BorrarEstadisticas(partido.GetEstadisticas());
                SaveSystem.GuardarEstadisticasGlobales("Partido", estadisticasGlobalesPartido, this);
                partidos.Remove(partido);
            }
            else
            {
                estadisticasGlobalesPractica.BorrarEstadisticas(partido.GetEstadisticas());
                SaveSystem.GuardarEstadisticasGlobales("Practica", estadisticasGlobalesPractica, this);
                practicas.Remove(partido);
            }

            SaveSystem.BorrarPartido(isPartido, partido, this);
        }
    }

    public void BorrarEstadisticaPartido(Partido partido)
    {
        int indexPartido = BuscarPartido(partido.GetNombre());
        if (indexPartido < 0) return;
        partidos[indexPartido].BorrarEstadistica(partido.GetEstadisticas());
        estadisticasGlobalesPartido.BorrarEstadisticas(partido.GetEstadisticas());
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

    public int BuscarPartido(string nombrePartido)
    {
        for (int i = 0; i < partidos.Count; i++)
        {
            if (partidos[i].GetNombre() == nombrePartido) return i;
        }

        return -1;
    }

    public int BuscarPractica(string nombrePractica)
    {
        for (int i = 0; i < practicas.Count; i++)
        {
            if (practicas[i].GetNombre() == nombrePractica) return i;
        }

        return -1;
    }

    public void BorrarEstadisticaPractica(Partido practica)
    {
        int indexPractica = BuscarPractica(practica.GetNombre());
        if (indexPractica < 0) return;
        practicas[indexPractica].BorrarEstadistica(practica.GetEstadisticas());
        estadisticasGlobalesPractica.BorrarEstadisticas(practica.GetEstadisticas());
    }

    public void AgregarPartido(Partido _partido, string tipoEntradaDato)
    {
        if (tipoEntradaDato == "Partido" || tipoEntradaDato == "partido") partidos.Add(_partido);
        else                                                              practicas.Add(_partido);
    }

    public bool ContienePartido(string tipoEntradaDato, string nombrePartido)
    {
        if(tipoEntradaDato == "Partido")
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

    public Estadisticas GetEstadisticasPartido()                            //Devuelve las estadísticas correspondiente a Partidos
    {
        return estadisticasGlobalesPartido;
    }

    public void GuardarEntradaDato(string tipoEntradaDato, Estadisticas estadisticas, Partido partido)
    {
        if (tipoEntradaDato == "Partido")
        {
            estadisticasGlobalesPartido.AgregarEstadisticas(estadisticas);
            partidos.Add(partido);
        }
        else
        {
            estadisticasGlobalesPractica.AgregarEstadisticas(estadisticas);
            practicas.Add(partido);     
        }

        SaveSystem.GuardarEntradaDato(tipoEntradaDato, estadisticas, partido, this);
    }

    /*public void SetEstadisticas(Estadisticas estadisticas_, string tipoEntradaDatos_)
    {
        if (tipoEntradaDatos_ == "Partido")
        {
            estadisticasPartido.AgregarEstadisticas(estadisticas_);
            SaveSystem.GuardarEntradaDatoPartido(estadisticasPartido, this);
        }
        else
        {
            estadisticasPractica.AgregarEstadisticas(estadisticas_);
            SaveSystem.GuardarEntradaDatoPractica(estadisticasPractica, this);
        }
    }*/

    /*public void SetEstadisticasPartido(Estadisticas estadisticas_)          //Suma a las estadísticas los campos de otra estadítica
    {
        estadisticasPartido = estadisticas_;
        SaveSystem.GuardarEntradaDatoPartido(estadisticasPartido.AgregarEstadisticas(estadisticas_), this);
    }*/

    public Estadisticas GetEstadisticasPractica()                            //Devuelve las estadísticas correspondiente a Practica
    {
        return estadisticasGlobalesPractica;
    }

    /*public void SetEstadisticasPractica(Estadisticas estadisticas_)          //Suma a las estadísticas los campos de otra estadítica
    {
        estadisticasPractica = estadisticas_;
        SaveSystem.GuardarEntradaDatoPractica(estadisticasPractica.AgregarEstadisticas(estadisticas_), this);
    }*/

    public string GetNombre()                                               //Devuelve el nombre del equipo
    {
        return nombre;
    }

    public string GetDeporteNombre()
    {
        return deporte.ToString();
    }

    public Deportes.Deporte GetDeporte()
    {
        return deporte;
    }

    public List<Jugador> GetJugadores()                                     //Devuelve la lista de jugadores
    {
        return jugadores;
    }

    public List<PlanillaAsistencia> GetPlanillasAsistencia()
    {
        return planillasAsistencia;
    }

    public PlanillaAsistencia GetPlanillaAtIndex(int index)
    {
        return planillasAsistencia[index];
    }

    public PlanillaAsistencia GetPlanillaWithName(string nombre)
    {
        foreach(var planilla in planillasAsistencia)
        {
            if(planilla.GetNombre() == nombre)
            {
                return planilla;
            }
        }

        Debug.Log("PLANILLA POR NOMBRE: " + nombre + " NO ENCONTRADA");
        return null;
    }

    public void AgregarPlanillaAsistencia(SaveDataPlanillaAsistencia dataPlanillaAsistencia)
    {
        planillasAsistencia.Add(new PlanillaAsistencia(dataPlanillaAsistencia));
    }

    public void AgregarDetalle(DetalleAsistencia detalle, string nombrePlanilla)
    {
        /*if (!planillasAsistencia.ContainsKey(nombrePlanilla))
        {
            planillasAsistencia[nombrePlanilla] = new List<DetalleAsistencia>();
        }

        planillasAsistencia[nombrePlanilla].Add(detalle);
        */
        //Debug.Log(planillasAsistencia.Count);
        foreach(var planilla in planillasAsistencia)
        {
            if(planilla.GetNombre() == nombrePlanilla)
            {
                planilla.AgregarDetalle(detalle);
                return;
            }
        }
        Debug.Log("Planilla no encontrada con nombre: " + nombrePlanilla);
    }


    public void NuevaPlanilla(string nombrePlanilla, string aliasPlanilla, List<DetalleAsistencia> detalles)
    {
        if (detalles.Count == 0) return;
        //List<DetalleAsistencia> detalle_ = detalles;
        //planillasAsistencia.Add(nombrePlanilla, detalle_);

        PlanillaAsistencia planilla = new PlanillaAsistencia(nombrePlanilla, aliasPlanilla, detalles);
        planillasAsistencia.Add(planilla);
        //SaveSystem.GuardarPlanilla(nombrePlanilla, this);
        SaveSystem.GuardarPlanilla(planilla, this);
    }

    public void BorrarAsistencia(string nombrePlanilla)
    {
        planillasAsistencia.Remove(GetPlanillaWithName(nombrePlanilla));
        SaveSystem.BorrarPlanilla(nombrePlanilla, this);
    }

    public bool ExistePlanilla(string nombrePlanilla, string aliasPlanilla)
    {
        //return planillasAsistencia.ContainsKey(nombrePlanilla);
        foreach(var planilla in planillasAsistencia)
        {  
            if (planilla.GetAlias() == "")
            {
                if (planilla.GetNombre() == nombrePlanilla)
                    return true;
            }
            else
            {
                if (planilla.GetAlias() == aliasPlanilla)
                    return true;
            }
        }

        return false;
    }

    public List<DetalleAsistencia> GetDetallesAsistencia(PlanillaAsistencia planilla)
    {
        //return planillasAsistencia[nombrePlanilla];
        return planilla.GetDetalles();
    }

    public SaveDataEstadisticas CreateSaveDataEstadisticasPartido()
    {
        return new SaveDataEstadisticas(estadisticasGlobalesPartido);
    }

    public SaveDataEstadisticas CreateSaveDataEstadisticasPractica()
    {
        return new SaveDataEstadisticas(estadisticasGlobalesPractica);
    }

    public SaveDataPlanilla CreateSaveDataPlanilla(PlanillaAsistencia planilla, int index)
    {
        DetalleAsistencia detalle = null;
        /*for (int i = 0; i < planillasAsistencia[nombrePlanilla].Count; i++)
        {
            if (i == index)
            {
                detalle = planillasAsistencia[nombrePlanilla][index];
                break;
            }
        }*/
        detalle = planilla.GetDetalleAtIndex(index);

        return new SaveDataPlanilla(detalle, planilla.GetNombre(), planilla.GetAlias());
    }


    public Deportes.Deporte GetDeporte(string deporte_)
    {
        switch (deporte_)
        {
            case "Fútbol": case "Futbol":
                return Deportes.Deporte.Futbol;
            case "Hockey Patines": case "HockeyPatines":
                return Deportes.Deporte.HockeyPatines;
            case "Hockey Cesped": case "HockeyCesped":
                return Deportes.Deporte.HockeyCesped;
            case "Voley":
                return Deportes.Deporte.Voley;
            case "Rugby":
                return Deportes.Deporte.Rugby;
            case "Handball":
                return Deportes.Deporte.Handball;
            case "Tenis":
                return Deportes.Deporte.Tenis;
            case "Padel":
                return Deportes.Deporte.Padel;
            case "Softball":
                return Deportes.Deporte.Softball;
            case "Basket":
                return Deportes.Deporte.Basket;
        }

        return Deportes.Deporte.NULL;
    }
}
