﻿using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System;

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

    public static void EditarJugador(Jugador jugador, Equipo equipo)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string pathJugador = pathEquipos + equipo.GetNombre() + "/jugadores" + "/" + jugador.GetNombre();

        Directory.Delete(pathJugador, true);
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
        else pathPartido += "/practicas";

        DateTime datePartido = partido.GetFecha();
        string stringTime = GetDateString(datePartido);

        pathPartido += "/" + stringTime + "-" + partido.GetNombre();
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
        else pathPartido += "/practicas";

        DateTime datePartido = partido.GetFecha();
        string stringTime = GetDateString(datePartido);

        pathPartido += "/" + stringTime + "-" + partido.GetNombre();
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

    public static void GuardarJugadaImagen(byte[] bytes, string nombreJugada, string categoria)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        // string imagenPath = Application.persistentDataPath + "/SaveData/ImagenesJugadas";
        string imagenPath = pathImagenJugadas + nombreJugada + "/";

        if (!Directory.Exists(imagenPath))
        {
            Directory.CreateDirectory(imagenPath);
        }

        //string nombreImagen = System.DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss");

        FileStream streamCategoria = new FileStream(imagenPath + "categoria.txt", FileMode.Create);
        formatter.Serialize(streamCategoria, categoria);
        streamCategoria.Close();

        File.WriteAllBytes(imagenPath + nombreJugada + ".png", bytes);
        AppController.instance.AgregarImagen(new ImagenBiblioteca(bytes, nombreJugada, categoria));
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
        else path += "/practicas/" + partido.GetNombre();

        if (Directory.Exists(path))
        {
            Directory.Delete(path, true);
        }
    }

    public static void BorrarPartido(bool isPartido, Partido partido, Equipo equipo)
    {
        string path = pathEquipos + equipo.GetNombre();

        if (isPartido) path += "/partidos/" + partido.GetNombre();
        else path += "/practicas/" + partido.GetNombre();

        if (Directory.Exists(path))
        {
            Directory.Delete(path, true);
        }
    }

    public static void BorrarJugada(string nombreJugada)
    {
        string path = pathImagenJugadas + nombreJugada;

        if (Directory.Exists(path))
            Directory.Delete(path, true);

    }

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
}
