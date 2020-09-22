using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public static class SaveSystem {

    public static string pathEquipos = Application.persistentDataPath + "/TFSaveData/Equipos/";
    public static string pathImagenJugadas = Application.persistentDataPath + "/TFSaveData/ImagenJugadas/";

    #region Equipos
    public static void GuardarEquipo(Equipo equipo)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        //string pathPrincipal = Application.persistentDataPath + "/SaveData" + "/" + equipo.GetNombre();                        //Path de la carpeta con el nombre del Equipo
        string pathPrincipal = pathEquipos + equipo.GetNombre();

        Directory.CreateDirectory(pathPrincipal);                                                               //Crea la carpeta con el nombre del Equipo

        FileStream stream = new FileStream(pathPrincipal + "/equipo.txt", FileMode.Create);                      //Archivo con la info del Equipo a guardar

        SaveDataEquipo dataEquipo = new SaveDataEquipo(equipo);                                         //Clase con la información del equipo

        formatter.Serialize(stream, dataEquipo);                                                        //Guardar datos del Equipo
        stream.Close();
    }
    public static void EditarEquipo(Equipo equipo, string nuevoNombre)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string pathEquipo = pathEquipos + equipo.GetNombre();
        string nuevoPath = pathEquipos + nuevoNombre;
        Directory.Move(pathEquipo, nuevoPath);

        equipo.SetNombre(nuevoNombre);

        string filePath = nuevoPath + "/equipo.txt";
        File.Delete(filePath);

        FileStream stream = new FileStream(filePath, FileMode.Create);                      //Archivo con la info del Equipo a guardar

        SaveDataEquipo dataEquipo = new SaveDataEquipo(equipo);                                         //Clase con la información del equipo

        formatter.Serialize(stream, dataEquipo);                                                        //Guardar datos del Equipo
        stream.Close();
    }
    public static void BorrarEquipo(Equipo equipo)
    {
        //string path = Application.persistentDataPath + "/SaveData" + "/" + equipo.GetNombre();
        string path = pathEquipos + equipo.GetNombre();

        if (Directory.Exists(path))
        {
            Directory.Delete(path, true);
        }
    }
    #endregion

    #region Jugadores
    public static void GuardarJugador(Jugador jugador, Equipo equipo)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        //string pathJugador = Application.persistentDataPath + "/SaveData" + "/" + equipo.GetNombre() + "/jugadores" + "/" + jugador.GetNombre();                             //Path de la carpeta de un Jugador

        string pathJugador = pathEquipos + equipo.GetNombre() + "/jugadores" + "/" + jugador.GetNombre();

        Directory.CreateDirectory(pathJugador);                                                      //Crea la carptea con el nombre del Jugador

        FileStream streamJugador = new FileStream(pathJugador + "/jugador.txt", FileMode.Create);     //Archivo con la info del Jugador

        SaveDataJugador dataJugador = jugador.CreateSaveData();                                       //Clase con la info

        formatter.Serialize(streamJugador, dataJugador);                                              //Guardar archivo

        streamJugador.Close();
    }
    public static void EditarJugador(Jugador jugador, Equipo equipo, string nombreNuevo)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string pathJugadorViejo = pathEquipos + equipo.GetNombre() + "/jugadores" + "/" + jugador.GetNombre();
        string pathJugadorNuevo = pathEquipos + equipo.GetNombre() + "/jugadores/" + nombreNuevo;
        if(pathJugadorViejo != pathJugadorNuevo)
            Directory.Move(pathJugadorViejo, pathJugadorNuevo);
    }
    public static void EditarInfoJugador(Jugador _jugador, Equipo _equipo)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string pathJugador = pathEquipos + _equipo.GetNombre() + "/jugadores/" + _jugador.GetNombre();

        if (Directory.Exists(pathJugador + "jugador.txt"))
            Directory.Delete(pathJugador + "jugador.txt");

        FileStream streamJugador = new FileStream(pathJugador + "/jugador.txt", FileMode.Create);
        SaveDataJugador dataJugador = _jugador.CreateSaveData();

        formatter.Serialize(streamJugador, dataJugador);
        streamJugador.Close();
    }
    public static void BorrarJugador(string nombreJugador, Equipo equipo)
    {
        //string path = Application.persistentDataPath + "/SaveData" + "/" + equipo.GetNombre() + "/jugadores/" + nombreJugador;
        string path = pathEquipos + equipo.GetNombre() + "/jugadores/" + nombreJugador;

        if (Directory.Exists(path))
        {
            Directory.Delete(path, true);
        }
    }
    #endregion

    #region Entrada de datos
    public static void GuardarEntradaDato(string tipoEntradaDato, Estadisticas estadisticas_globales, Partido partido, Equipo equipo)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        //GLOBAL
        GuardarEstadisticasGlobales(tipoEntradaDato, estadisticas_globales, equipo);

        //PARTIDO / PRACTICA
        //string pathPartido = Application.persistentDataPath + "/SaveData" + "/" + equipo.GetNombre();
        string pathPartido = pathEquipos + equipo.GetNombre();

        if (tipoEntradaDato == "Partido") pathPartido += "/partidos";
        else pathPartido += "/practicas";

        DateTime datePartido = partido.GetFecha();
        string stringTime = GetDateString(datePartido);

        pathPartido += "/" + stringTime + "-" + partido.GetNombre();
        if (!Directory.Exists(pathPartido)) Directory.CreateDirectory(pathPartido);

        SaveDataPartido dataPartido = new SaveDataPartido(partido);
        FileStream streamPartido = new FileStream(pathPartido + "/partido.txt", FileMode.Create);


        Partido.TipoResultadoPartido tiporesultado = partido.GetTipoResultadoPartido();

        SaveDataResultadoNormal resPartidoNormal;
        SaveDataResultadoSets resPartidoSets;

        FileStream streamResultado = new FileStream(pathPartido + "/resultado.txt", FileMode.Create);

        if (tiporesultado == Partido.TipoResultadoPartido.Normal)
        {
            resPartidoNormal = new SaveDataResultadoNormal((ResultadoNormal)partido.GetResultadoEntradaDato());
            formatter.Serialize(streamResultado, resPartidoNormal);
        }
        else
        {
            resPartidoSets = new SaveDataResultadoSets((ResultadoSets)partido.GetResultadoEntradaDato());
            formatter.Serialize(streamResultado, resPartidoSets);
        }

        SaveDataEstadisticas dataEstPartido = new SaveDataEstadisticas(partido.GetEstadisticas());
        FileStream streamEstPartido = new FileStream(pathPartido + "/estadisticas.txt", FileMode.Create);

        //SERIALIZAR Y CERRAR                                              //Guarda los archivos
        formatter.Serialize(streamPartido, dataPartido);
        formatter.Serialize(streamEstPartido, dataEstPartido);

        streamPartido.Close();
        streamResultado.Close();
        streamEstPartido.Close();
    }
    public static void GuardarEntradaDato(string tipoEntradaDato, Estadisticas estadisticas_globales, Partido partido, Jugador jugador, Equipo equipo)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        //GLOBAL 
        GuardarEstadisticasGlobales(tipoEntradaDato, estadisticas_globales, jugador, equipo);

        //PARTIDO / PRACTICA
        //string pathPartido = Application.persistentDataPath + "/SaveData" + "/" + equipo.GetNombre() + "/jugadores" + "/" + jugador.GetNombre();
        string pathPartido = pathEquipos + equipo.GetNombre() + "/jugadores" + "/" + jugador.GetNombre();

        if (tipoEntradaDato == "Partido") pathPartido += "/partidos";
        else pathPartido += "/practicas";

        DateTime datePartido = partido.GetFecha();
        string stringTime = GetDateString(datePartido);

        pathPartido += "/" + stringTime + "-" + partido.GetNombre();
        if (!Directory.Exists(pathPartido)) Directory.CreateDirectory(pathPartido);

        SaveDataPartido dataPartido = new SaveDataPartido(partido);
        FileStream streamPartido = new FileStream(pathPartido + "/partido.txt", FileMode.Create);

        FileStream streamPosicion = new FileStream(pathPartido + "/posicion.txt", FileMode.Create);
        Debug.Log("POS SAVED: " + jugador.GetPosicionActual());

        Partido.TipoResultadoPartido tiporesultado = partido.GetTipoResultadoPartido();

        SaveDataResultadoNormal resPartidoNormal;
        SaveDataResultadoSets resPartidoSets;

        FileStream streamResultado = new FileStream(pathPartido + "/resultado.txt", FileMode.Create);

        if (tiporesultado == Partido.TipoResultadoPartido.Normal)
        {
            resPartidoNormal = new SaveDataResultadoNormal((ResultadoNormal)partido.GetResultadoEntradaDato());
            formatter.Serialize(streamResultado, resPartidoNormal);
        }
        else
        {
            resPartidoSets = new SaveDataResultadoSets((ResultadoSets)partido.GetResultadoEntradaDato());
            formatter.Serialize(streamResultado, resPartidoSets);
        }


        SaveDataEstadisticas dataEstPartido = new SaveDataEstadisticas(partido.GetEstadisticas());
        FileStream streamEstPartido = new FileStream(pathPartido + "/estadisticas.txt", FileMode.Create);

        //SERIALIZAR Y CERRAR                                                //Guarda los archivos
        formatter.Serialize(streamPartido, dataPartido);
        formatter.Serialize(streamEstPartido, dataEstPartido);
        formatter.Serialize(streamPosicion, (string)jugador.GetPosicionActual());

        streamPartido.Close();
        streamEstPartido.Close();
        streamPosicion.Close();
    }
    #endregion

    #region Carpetas biblioteca
    public static void GuardarCarpetaBiblioteca(CarpetaJugada _carpeta)
    {
        string path = pathImagenJugadas + _carpeta.GetNombre();
        Directory.CreateDirectory(path);
    }
    public static void EditarCarpeta(string nombreViejo, string nombreNuevo)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string pathViejo = pathImagenJugadas + nombreViejo;
        string pathNuevo = pathImagenJugadas + nombreNuevo;
        Directory.Move(pathViejo, pathNuevo);
    }
    public static void BorrarCarpeta(CarpetaJugada _carpeta)
    {
        string path = pathImagenJugadas + _carpeta.GetNombre();

        if (Directory.Exists(path))
            Directory.Delete(path, true);
    }
    #endregion

    #region Jugadas
    public static void GuardarJugadaImagen(byte[] bytes, string nombreJugada, string categoria, CarpetaJugada _carpeta)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string carpetaPath;
        if (_carpeta == null)
        {
            Debug.LogError("CARPETA NULL " + _carpeta == null);
            carpetaPath = pathImagenJugadas + "-" + "/";
        }
        else
            carpetaPath = pathImagenJugadas + _carpeta.GetNombre() + "/";

        string imagenPath = carpetaPath + nombreJugada + "/";

        if (!Directory.Exists(imagenPath))
        {
            Directory.CreateDirectory(imagenPath);
            if (_carpeta == null)
            {
                _carpeta = AppController.instance.BuscarCarpetaPorNombre("SIN CARPETA");
                if (_carpeta==null)
                {
                    _carpeta = new CarpetaJugada("SIN CARPETA");
                    AppController.instance.AgregarCarpetaJugada(_carpeta);
                }
            }
        }

        //string nombreImagen = System.DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss");

        FileStream streamCategoria = new FileStream(imagenPath + "categoria.txt", FileMode.Create);
        formatter.Serialize(streamCategoria, categoria);
        streamCategoria.Close();

        File.WriteAllBytes(imagenPath + nombreJugada + ".png", bytes);

        ImagenBiblioteca _imagenBiblioteca = new ImagenBiblioteca(bytes, nombreJugada, categoria, _carpeta);
        _carpeta.AgregarJugada(_imagenBiblioteca);
    }
    public static void EditarJugada(string nombreViejo, string nombreNuevo, CarpetaJugada _carpeta)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string pathViejo = pathImagenJugadas + _carpeta.GetNombre() + "/" + nombreViejo;
        string pathNuevo = pathImagenJugadas + _carpeta.GetNombre() + "/" + nombreNuevo;
        Directory.Move(pathViejo, pathNuevo);

        ImagenBiblioteca jugada = _carpeta.BuscarJugada(nombreViejo);
        if (jugada == null)
        {
            Debug.Log("JUGADA NULL");
            return;
        }

        jugada.SetNombre(nombreNuevo);

        string filePathViejo = pathNuevo + "/" + nombreViejo + ".png";
        string filePathNuevo = pathNuevo + "/" + nombreNuevo + ".png";
        File.Move(filePathViejo, filePathNuevo);
    }
    public static void EditarCarpetaJugada(ImagenBiblioteca _jugada, CarpetaJugada _carpetaVieja, CarpetaJugada _carpetaNueva)
    {
        if (_carpetaVieja == _carpetaNueva)
            return;

        string pathViejo = pathImagenJugadas + _carpetaVieja.GetNombre() + "/" + _jugada.GetNombre();
        string pathNuevo = pathImagenJugadas + _carpetaNueva.GetNombre() + "/" + _jugada.GetNombre();

        Directory.Move(pathViejo, pathNuevo);
    }
    public static void BorrarJugada(ImagenBiblioteca _jugada, CarpetaJugada _carpeta)
    {
        string path = pathImagenJugadas + _carpeta.GetNombre() + "/" + _jugada.GetNombre();

        if (Directory.Exists(path))
            Directory.Delete(path, true);
    }
    #endregion

    #region Estadisticas
    public static void GuardarEstadisticasGlobales(string isPartido, Estadisticas estadisticas, Jugador jugador, Equipo equipo)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = pathEquipos + equipo.GetNombre() + "/jugadores" + "/" + jugador.GetNombre();

        string pathEstadisticasGlobales = path;
        if (isPartido == "Partido") pathEstadisticasGlobales += "/estGlobalPartido.txt";
        else pathEstadisticasGlobales += "/estGlobalPractica.txt";

        SaveDataEstadisticas dataEstadisticasGlobales = new SaveDataEstadisticas(estadisticas);         //Clase con la info de estadisticas de Partido
        FileStream streamEstadisticasGlobales = new FileStream(pathEstadisticasGlobales, FileMode.Create);      //Crea los archivos

        formatter.Serialize(streamEstadisticasGlobales, dataEstadisticasGlobales);
        streamEstadisticasGlobales.Close();
    }
    public static void GuardarEstadisticasGlobales(string isPartido, Estadisticas estadisticas, Equipo equipo)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = pathEquipos + equipo.GetNombre();

        SaveDataEstadisticas dataEstadisticasGlobales = new SaveDataEstadisticas(estadisticas);         //Clase con la info de estadisticas de Partido

        if (isPartido == "Partido") path += "/estGlobalPartido.txt";
        else path += "/estGlobalPractica.txt";

        FileStream streamEstadisticasGlobales = new FileStream(path, FileMode.Create);      //Crea los archivos 

        formatter.Serialize(streamEstadisticasGlobales, dataEstadisticasGlobales);
        streamEstadisticasGlobales.Close();
    }
    #endregion

    #region Planillas
    public static void GuardarPlanilla(PlanillaAsistencia planilla, Equipo equipo)//string nombrePlanilla, Equipo equipo)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        //string path = Application.persistentDataPath + "/SaveData" + "/" + equipo.GetNombre() + "/planillas" + "/" + nombrePlanilla;
        string path = pathEquipos + equipo.GetNombre() + "/planillas" + "/" + planilla.GetNombre();

        Directory.CreateDirectory(path);

        FileStream streamPlanillaAsistencia = new FileStream(path + "/" + planilla.GetNombre() + ".txt", FileMode.Create);
        SaveDataPlanillaAsistencia dataPlanillaAsistencia = new SaveDataPlanillaAsistencia(planilla);
        formatter.Serialize(streamPlanillaAsistencia, dataPlanillaAsistencia);
        streamPlanillaAsistencia.Close();

        string pathDetalles = path + "/Detalles";
        Directory.CreateDirectory(pathDetalles);

        int indexStream = 0;
        foreach (DetalleAsistencia detalle in equipo.GetDetallesAsistencia(planilla))
        {
            FileStream streamPlanilla = new FileStream(pathDetalles + "/detalle" + indexStream.ToString() + ".txt", FileMode.Create);

            SaveDataPlanilla dataPlanilla = equipo.CreateSaveDataPlanilla(planilla, indexStream);

            formatter.Serialize(streamPlanilla, dataPlanilla);

            streamPlanilla.Close();

            indexStream++;
        }
    }
    public static void BorrarPlanilla(string nombrePlanilla, Equipo equipo)
    {
        //string path = Application.persistentDataPath + "/SaveData/" + equipo.GetNombre() + "/planillas/" + nombrePlanilla;
        string path = pathEquipos + equipo.GetNombre() + "/planillas/" + nombrePlanilla;

        if (Directory.Exists(path))
        {
            Directory.Delete(path, true);
        }
    }

    #endregion

    #region Partidos
    public static void BorrarPartido(bool isPartido, Partido partido, Jugador jugador, Equipo equipo)
    {
        string path = pathEquipos + equipo.GetNombre();

        if (jugador != null)
            path += "/jugadores/" + jugador.GetNombre();

        if (isPartido) path += "/partidos/";
        else path += "/practicas/";

        DateTime datePartido = partido.GetFecha();
        string stringTime = GetDateString(datePartido);

        path += stringTime + "-" + partido.GetNombre();

        if (Directory.Exists(path))
        {
            Directory.Delete(path, true);
        }
    }
    #endregion

    #region Auxiliares
    private static string GetDateString(DateTime datePartido)
    {
        string stringTime = datePartido.Year.ToString();

        if (datePartido.Month.ToString().Length == 1)
            stringTime += "0" + datePartido.Month.ToString();
        else
            stringTime += datePartido.Month.ToString();
        if (datePartido.Day.ToString().Length == 1)
            stringTime += "0" + datePartido.Day.ToString();
        else
            stringTime += datePartido.Day.ToString();
        if (datePartido.Hour.ToString().Length == 1)
            stringTime += "0" + datePartido.Hour.ToString();
        else
            stringTime += datePartido.Hour.ToString();
        if (datePartido.Minute.ToString().Length == 1)
            stringTime += "0" + datePartido.Minute.ToString();
        else
            stringTime += datePartido.Minute.ToString();
        if (datePartido.Second.ToString().Length == 1)
            stringTime += "0" + datePartido.Second.ToString();
        else
            stringTime += datePartido.Second.ToString();

        return stringTime;
    }
    #endregion
}