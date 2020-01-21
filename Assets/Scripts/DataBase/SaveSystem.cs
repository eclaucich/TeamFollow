using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem {

    public static string pathEquipos = Application.persistentDataPath + "/SaveData/Equipos/";
    public static string pathImagenJugadas = Application.persistentDataPath + "/SaveData/ImagenJugadas/";

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

    public static void GuardarEntradaDato(string tipoEntradaDato, Estadisticas estadisticas_globales, Partido partido, Equipo equipo)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        //GLOBAL
        GuardarEstadisticasGlobales(tipoEntradaDato, estadisticas_globales, equipo);  

        //PARTIDO / PRACTICA
        //string pathPartido = Application.persistentDataPath + "/SaveData" + "/" + equipo.GetNombre();
        string pathPartido = pathEquipos + equipo.GetNombre();

        if (tipoEntradaDato == "Partido") pathPartido += "/partidos";
        else                              pathPartido += "/practicas";
 
        pathPartido += "/" + partido.GetNombre();
        if (!Directory.Exists(pathPartido)) Directory.CreateDirectory(pathPartido);

        SaveDataPartido dataPartido = new SaveDataPartido(partido);
        FileStream streamPartido = new FileStream(pathPartido + "/partido.txt", FileMode.Create);

        SaveDataEstadisticas dataEstPartido = new SaveDataEstadisticas(partido.GetEstadisticas());
        FileStream streamEstPartido = new FileStream(pathPartido + "/estadisticas.txt", FileMode.Create);

        //SERIALIZAR Y CERRAR                                              //Guarda los archivos
        formatter.Serialize(streamPartido, dataPartido);
        formatter.Serialize(streamEstPartido, dataEstPartido);

        streamPartido.Close();
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
        else                              pathPartido += "/practicas";

        pathPartido += "/" + partido.GetNombre();
        if (!Directory.Exists(pathPartido)) Directory.CreateDirectory(pathPartido);

        SaveDataPartido dataPartido = new SaveDataPartido(partido);
        FileStream streamPartido = new FileStream(pathPartido + "/partido.txt", FileMode.Create);

        SaveDataEstadisticas dataEstPartido = new SaveDataEstadisticas(partido.GetEstadisticas());
        FileStream streamEstPartido = new FileStream(pathPartido + "/estadisticas.txt", FileMode.Create);


        //SERIALIZAR Y CERRAR                                                //Guarda los archivos
        formatter.Serialize(streamPartido, dataPartido);
        formatter.Serialize(streamEstPartido, dataEstPartido);

        streamPartido.Close();
        streamEstPartido.Close();
    }

    public static void GuardarEstadisticasGlobales(string isPartido, Estadisticas estadisticas, Jugador jugador, Equipo equipo)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = pathEquipos + equipo.GetNombre() + "/jugadores" + "/" + jugador.GetNombre();

        string pathEstadisticasGlobales = path;
        if (isPartido == "Partido") pathEstadisticasGlobales += "/estGlobalPartido.txt";
        else                        pathEstadisticasGlobales += "/estGlobalPractica.txt";

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
        else           path += "/estGlobalPractica.txt";

        FileStream streamEstadisticasGlobales = new FileStream(path, FileMode.Create);      //Crea los archivos 

        formatter.Serialize(streamEstadisticasGlobales, dataEstadisticasGlobales);
        streamEstadisticasGlobales.Close();
    }
    


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

    public static void GuardarJugadaImagen(byte[] bytes)
    {
       // string imagenPath = Application.persistentDataPath + "/SaveData/ImagenesJugadas";
        string imagenPath = pathImagenJugadas;

        if (!Directory.Exists(imagenPath))
        {
            Directory.CreateDirectory(imagenPath);
        }

        File.WriteAllBytes(imagenPath + System.DateTime.Now.ToString("yyyy-mm-dd-hh-mm") + ".png", bytes);
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


    public static void BorrarEquipo(Equipo equipo)
    {
        //string path = Application.persistentDataPath + "/SaveData" + "/" + equipo.GetNombre();
        string path = pathEquipos + equipo.GetNombre();

        if (Directory.Exists(path))
        {
            Directory.Delete(path, true);
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

    public static void BorrarPartido(bool isPartido, Partido partido, Jugador jugador, Equipo equipo)
    {
        string path = pathEquipos + equipo.GetNombre() + "/jugadores/" + jugador.GetNombre();

        if (isPartido) path += "/partidos/" + partido.GetNombre();
        else           path += "/practicas/" + partido.GetNombre();

        if (Directory.Exists(path))
        {
            Directory.Delete(path, true);
        }
    }

    public static void BorrarPartido(bool isPartido, Partido partido, Equipo equipo)
    {
        string path = pathEquipos + equipo.GetNombre();

        if (isPartido) path += "/partidos/" + partido.GetNombre();
        else           path += "/practicas/" + partido.GetNombre();

        if (Directory.Exists(path))
        {
            Directory.Delete(path, true);
        }
    }

}
