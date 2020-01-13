using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class LoadSystem
{
    private static string pathEquipos = SaveSystem.pathEquipos;
    private static string pathImagenJugadas = SaveSystem.pathImagenJugadas;

    public static void LoadData()
    {
        BinaryFormatter formatter = new BinaryFormatter();

        //PATH PRINCIPAL
        //string path = Application.persistentDataPath + "/SaveData";

        if (!Directory.Exists(pathEquipos))
        {
            return;
        }

        //CARGAR EQUIPOS
        string[] equiposDirectories = Directory.GetDirectories(pathEquipos);

        for (int i = 0; i < equiposDirectories.Length; i++)
        {
            //PATH DE UN EQUIPO
            string pathEquipo = equiposDirectories[i];

            FileStream equipoStream = new FileStream(pathEquipo + "/equipo.txt", FileMode.Open);
            SaveDataEquipo dataEquipo = (SaveDataEquipo)formatter.Deserialize(equipoStream);

            //CREAR EL EQUIPO
            Equipo equipo = new Equipo(dataEquipo);

            equipoStream.Close();


            //CARGAR ESTADISTICAS GLOBALES
            if (File.Exists(pathEquipo + "/estGlobalPartido.txt"))
            {
                FileStream estPartidoStream = new FileStream(pathEquipo + "/estGlobalPartido.txt", FileMode.Open);
                SaveDataEstadisticas dataEstPartido = (SaveDataEstadisticas)formatter.Deserialize(estPartidoStream);
                equipo.CargarEstadisticasGlobalesPartido(new Estadisticas(dataEstPartido));
                estPartidoStream.Close();
            }
            if (File.Exists(pathEquipo + "/estGlobalPractica.txt"))
            {
                FileStream estPracticaStream = new FileStream(pathEquipo + "/estGlobalPractica.txt", FileMode.Open);
                SaveDataEstadisticas dataEstPractica = (SaveDataEstadisticas)formatter.Deserialize(estPracticaStream);
                equipo.CargarEstadisticasGlobalesPractica(new Estadisticas(dataEstPractica));
                estPracticaStream.Close();
            }


            //CARGAR PARTIDOS
            if(Directory.Exists(pathEquipo + "/partidos"))
            {
                string pathPartidos = pathEquipo + "/partidos";

                string[] partidosDirectories = Directory.GetDirectories(pathPartidos);

                foreach (var partidoDir in partidosDirectories)
                {
                    //string pathPartido = pathPartidos + partidoDir;

                    FileStream streamPartido = new FileStream(partidoDir + "/partido.txt", FileMode.Open);
                    SaveDataPartido dataPartido = (SaveDataPartido)formatter.Deserialize(streamPartido);

                    FileStream streamEstadisticas = new FileStream(partidoDir + "/estadisticas.txt", FileMode.Open);
                    SaveDataEstadisticas dataEstadisticas = (SaveDataEstadisticas)formatter.Deserialize(streamEstadisticas);

                    equipo.CargarPartido(new Partido(dataPartido, dataEstadisticas));

                    streamPartido.Close();
                    streamEstadisticas.Close();
                }
            }


            //CARGAR PRACTICAS
            if (Directory.Exists(pathEquipo + "/practicas"))
            {
                string pathPracticas = pathEquipo + "/practicas";

                string[] practicasDirectories = Directory.GetDirectories(pathPracticas);

                foreach (var practicaDir in practicasDirectories)
                {
                    //string pathPractica = pathPracticas + practicaDir;

                    FileStream streamPractica = new FileStream(practicaDir + "/partido.txt", FileMode.Open);
                    SaveDataPartido dataPractica = (SaveDataPartido)formatter.Deserialize(streamPractica);

                    FileStream streamEstadisticas = new FileStream(practicaDir + "/estadisticas.txt", FileMode.Open);
                    SaveDataEstadisticas dataEstadisticas = (SaveDataEstadisticas)formatter.Deserialize(streamEstadisticas);

                    equipo.CargarPractica(new Partido(dataPractica, dataEstadisticas));

                    streamPractica.Close();
                    streamEstadisticas.Close();
                }
            }


            //CARGAR JUGADORES
            string pathJugadores = pathEquipo + "/jugadores";

            if (Directory.Exists(pathJugadores))
            {
                string[] jugadoresDirectories = Directory.GetDirectories(pathJugadores);

                foreach (var jugadorDir in jugadoresDirectories)
                {
                    FileStream streamJugador = new FileStream(jugadorDir + "/jugador.txt", FileMode.Open);
                    SaveDataJugador dataJugador = (SaveDataJugador)formatter.Deserialize(streamJugador);

                    Jugador jugador = new Jugador(dataJugador);

                    streamJugador.Close();


                    //CARGAR ESTADISTICAS GLOBALES
                    if (File.Exists(jugadorDir + "/estGlobalPartido.txt"))
                    {
                        FileStream estPartidoStream = new FileStream(jugadorDir + "/estGlobalPartido.txt", FileMode.Open);
                        SaveDataEstadisticas dataEstPartido = (SaveDataEstadisticas)formatter.Deserialize(estPartidoStream);
                        jugador.CargarEstadisticasGlobalesPartido(new Estadisticas(dataEstPartido));
                        estPartidoStream.Close();
                    }
                    if (File.Exists(jugadorDir + "/estGlobalPractica.txt"))
                    {
                        FileStream estPracticaStream = new FileStream(jugadorDir + "/estGlobalPractica.txt", FileMode.Open);
                        SaveDataEstadisticas dataEstPractica = (SaveDataEstadisticas)formatter.Deserialize(estPracticaStream);
                        jugador.CargarEstadisticasGlobalesPractica(new Estadisticas(dataEstPractica));
                        estPracticaStream.Close();
                    }


                    //CARGAR PARTIDOS
                    if (Directory.Exists(jugadorDir + "/partidos"))
                    {
                        string pathPartidos = jugadorDir + "/partidos";

                        string[] partidosDirectories = Directory.GetDirectories(pathPartidos);

                        foreach (var partidoDir in partidosDirectories)
                        {
                            //string pathPartido = pathPartidos + partidoDir;

                            FileStream streamPartido = new FileStream(partidoDir + "/partido.txt", FileMode.Open);
                            SaveDataPartido dataPartido = (SaveDataPartido)formatter.Deserialize(streamPartido);

                            FileStream streamEstadisticas = new FileStream(partidoDir + "/estadisticas.txt", FileMode.Open);
                            SaveDataEstadisticas dataEstadisticas = (SaveDataEstadisticas)formatter.Deserialize(streamEstadisticas);

                            jugador.CargarPartido(new Partido(dataPartido, dataEstadisticas));

                            streamPartido.Close();
                            streamEstadisticas.Close();
                        }
                    }


                    //CARGAR PRACTICAS
                    if (Directory.Exists(jugadorDir + "/practicas"))
                    {
                        string pathPracticas = jugadorDir + "/practicas";

                        string[] practicasDirectories = Directory.GetDirectories(pathPracticas);

                        foreach (var practicaDir in practicasDirectories)
                        {
                            //string pathPractica = pathPracticas + practicaDir;

                            FileStream streamPractica = new FileStream(practicaDir + "/partido.txt", FileMode.Open);
                            SaveDataPartido dataPractica = (SaveDataPartido)formatter.Deserialize(streamPractica);

                            FileStream streamEstadisticas = new FileStream(practicaDir + "/estadisticas.txt", FileMode.Open);
                            SaveDataEstadisticas dataEstadisticas = (SaveDataEstadisticas)formatter.Deserialize(streamEstadisticas);

                            jugador.CargarPractica(new Partido(dataPractica, dataEstadisticas));

                            streamPractica.Close();
                            streamEstadisticas.Close();
                        }
                    }

                    equipo.CargarJugador(jugador);
                }
            }


            //CARGAR PLANILLAS
            string pathPlanillas = Application.persistentDataPath + "/SaveData" + "/Equipos/" + equipo.GetNombre() + "/planillas";

            if (Directory.Exists(pathPlanillas))
            {
                string[] planillasDirectories = Directory.GetDirectories(pathPlanillas);    //Vector de Carpetas de Planillas

                for (int j = 0; j < planillasDirectories.Length; j++)                       //Para cada carpeta de planillas
                {
                    string pathPlanilla = planillasDirectories[j];                          //Obtengo la dirección de la carpeta actual

                    string[] detallesFiles = Directory.GetFiles(pathPlanilla);              //Vector de todos los detalles en la carpeta actual

                    for (int k = 0; k < detallesFiles.Length; k++)                          //Para cada detalle
                    {
                        FileStream streamPlanilla = new FileStream(detallesFiles[k], FileMode.Open);

                        SaveDataPlanilla dataPlanilla = (SaveDataPlanilla)formatter.Deserialize(streamPlanilla);

                        //Agregar la Planilla al Equipo
                        equipo.AgregarDetalle(new DetalleAsistencia(dataPlanilla), dataPlanilla.GetNombrePlanilla());

                        streamPlanilla.Close();
                    }
                }
            }

            //Agregar Equipo a la lista de Equipos
            AppController.instance.AgregarEquipo(equipo);
        }
    }

}
