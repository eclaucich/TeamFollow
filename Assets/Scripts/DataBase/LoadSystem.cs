using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using JetBrains.Annotations;

public static class LoadSystem
{
    private static string pathEquipos = SaveSystem.pathEquipos;
    private static string pathImagenJugadas = SaveSystem.pathImagenJugadas;

    public static void LoadData()
    {
        BinaryFormatter formatter = new BinaryFormatter();

        if (Directory.Exists(pathEquipos))
        {
            #region Equipo
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

                #region Jugadores
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

                        #region Estadisticas globales jugadores
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
                        #endregion

                        #region Partidos jugadores
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

                                FileStream streamPosicion = new FileStream(partidoDir + "/posicion.txt", FileMode.Open);
                                string dataPosicion = (string)formatter.Deserialize(streamPosicion);

                                FileStream streamEstadisticas = new FileStream(partidoDir + "/estadisticas.txt", FileMode.Open);
                                SaveDataEstadisticas dataEstadisticas = (SaveDataEstadisticas)formatter.Deserialize(streamEstadisticas);

                                FileStream streamResultado = new FileStream(partidoDir + "/resultado.txt", FileMode.Open);

                                Partido _partido = new Partido(dataPartido, dataEstadisticas, jugador, dataPosicion);

                                if (equipo.GetDeporte() == Deportes.DeporteEnum.Tenis || equipo.GetDeporte() == Deportes.DeporteEnum.Padel || equipo.GetDeporte() == Deportes.DeporteEnum.Voley)
                                {
                                    SaveDataResultadoSets resPartidoSets = (SaveDataResultadoSets)formatter.Deserialize(streamResultado);
                                    ResultadoSets _res = new ResultadoSets(resPartidoSets);
                                    _partido.AgregarResultadoEntradaDatos(_res, Partido.TipoResultadoPartido.Sets);
                                }
                                else
                                {
                                    SaveDataResultadoNormal resPartidoNormal = (SaveDataResultadoNormal)formatter.Deserialize(streamResultado);
                                    ResultadoNormal _res = new ResultadoNormal(resPartidoNormal);
                                    _partido.AgregarResultadoEntradaDatos(_res, Partido.TipoResultadoPartido.Normal);
                                }

                                jugador.CargarPartido(_partido);

                                streamPartido.Close();
                                streamEstadisticas.Close();
                            }
                        }
                        #endregion

                        #region Practicas jugadores
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

                                FileStream streamPosicion = new FileStream(practicaDir + "/posicion.txt", FileMode.Open);
                                string dataPosicion = (string)formatter.Deserialize(streamPosicion);
                                Debug.Log("POS LOAD: " + jugador.GetPosicionActual());

                                FileStream streamEstadisticas = new FileStream(practicaDir + "/estadisticas.txt", FileMode.Open);
                                SaveDataEstadisticas dataEstadisticas = (SaveDataEstadisticas)formatter.Deserialize(streamEstadisticas);

                                FileStream streamResultado = new FileStream(practicaDir + "/resultado.txt", FileMode.Open);

                                Partido _partido = new Partido(dataPractica, dataEstadisticas, jugador, dataPosicion);

                                if (equipo.GetDeporte() == Deportes.DeporteEnum.Tenis || equipo.GetDeporte() == Deportes.DeporteEnum.Padel || equipo.GetDeporte() == Deportes.DeporteEnum.Voley)
                                {
                                    SaveDataResultadoSets resPartidoSets = (SaveDataResultadoSets)formatter.Deserialize(streamResultado);
                                    ResultadoSets _res = new ResultadoSets(resPartidoSets);
                                    _partido.AgregarResultadoEntradaDatos(_res, Partido.TipoResultadoPartido.Sets);
                                }
                                else
                                {
                                    SaveDataResultadoNormal resPartidoNormal = (SaveDataResultadoNormal)formatter.Deserialize(streamResultado);
                                    ResultadoNormal _res = new ResultadoNormal(resPartidoNormal);
                                    _partido.AgregarResultadoEntradaDatos(_res, Partido.TipoResultadoPartido.Normal);
                                }

                                jugador.CargarPractica(_partido);

                                streamPractica.Close();
                                streamEstadisticas.Close();
                            }
                        }
                        #endregion
                        equipo.CargarJugador(jugador);
                    }
                }
                #endregion

                #region Estadisticas globales equipo
                //CARGAR ESTADISTICAS GLOBALES
                if (File.Exists(pathEquipo + "/estGlobalPartido.txt"))
                {
                    FileStream estPartidoStream = new FileStream(pathEquipo + "/estGlobalPartido.txt", FileMode.Open);
                    SaveDataEstadisticas dataEstPartido = (SaveDataEstadisticas)formatter.Deserialize(estPartidoStream);
                    Estadisticas est = new Estadisticas(dataEstPartido);
                    equipo.CargarEstadisticasGlobalesPartido(est);
                    estPartidoStream.Close();
                }
                if (File.Exists(pathEquipo + "/estGlobalPractica.txt"))
                {
                    FileStream estPracticaStream = new FileStream(pathEquipo + "/estGlobalPractica.txt", FileMode.Open);
                    SaveDataEstadisticas dataEstPractica = (SaveDataEstadisticas)formatter.Deserialize(estPracticaStream);
                    equipo.CargarEstadisticasGlobalesPractica(new Estadisticas(dataEstPractica));
                    estPracticaStream.Close();
                }
                #endregion

                #region Partidos equipo
                //CARGAR PARTIDOS
                if (Directory.Exists(pathEquipo + "/partidos"))
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

                        FileStream streamResultado = new FileStream(partidoDir + "/resultado.txt", FileMode.Open);

                        Partido _partido = new Partido(dataPartido, dataEstadisticas, equipo);

                        if (equipo.GetDeporte() == Deportes.DeporteEnum.Tenis || equipo.GetDeporte() == Deportes.DeporteEnum.Padel || equipo.GetDeporte() == Deportes.DeporteEnum.Voley)
                        {
                            SaveDataResultadoSets resPartidoSets = (SaveDataResultadoSets)formatter.Deserialize(streamResultado);
                            ResultadoSets _res = new ResultadoSets(resPartidoSets);
                            _partido.AgregarResultadoEntradaDatos(_res, Partido.TipoResultadoPartido.Sets);
                        }
                        else
                        {
                            SaveDataResultadoNormal resPartidoNormal = (SaveDataResultadoNormal)formatter.Deserialize(streamResultado);
                            ResultadoNormal _res = new ResultadoNormal(resPartidoNormal);
                            _partido.AgregarResultadoEntradaDatos(_res, Partido.TipoResultadoPartido.Normal);
                        }


                        equipo.CargarPartido(_partido);

                        streamPartido.Close();
                        streamEstadisticas.Close();
                    }
                }
                #endregion

                #region Practica equipo
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

                        FileStream streamResultado = new FileStream(practicaDir + "/resultado.txt", FileMode.Open);

                        Partido _partido = new Partido(dataPractica, dataEstadisticas, equipo);

                        if (equipo.GetDeporte() == Deportes.DeporteEnum.Tenis || equipo.GetDeporte() == Deportes.DeporteEnum.Padel || equipo.GetDeporte() == Deportes.DeporteEnum.Voley)
                        {
                            SaveDataResultadoSets resPartidoSets = (SaveDataResultadoSets)formatter.Deserialize(streamResultado);
                            ResultadoSets _res = new ResultadoSets(resPartidoSets);
                            _partido.AgregarResultadoEntradaDatos(_res, Partido.TipoResultadoPartido.Sets);
                        }
                        else
                        {
                            SaveDataResultadoNormal resPartidoNormal = (SaveDataResultadoNormal)formatter.Deserialize(streamResultado);
                            ResultadoNormal _res = new ResultadoNormal(resPartidoNormal);
                            _partido.AgregarResultadoEntradaDatos(_res, Partido.TipoResultadoPartido.Normal);
                        }

                        equipo.CargarPractica(_partido);

                        streamPractica.Close();
                        streamEstadisticas.Close();
                    }
                }
                #endregion
                #endregion

                

                #region Planillas
                //CARGAR PLANILLAS
                string pathPlanillas = pathEquipos + equipo.GetNombre() + "/planillas";

                if (Directory.Exists(pathPlanillas))
                {
                    string[] planillasDirectories = Directory.GetDirectories(pathPlanillas);    //Vector de Carpetas de Planillas

                    for (int j = 0; j < planillasDirectories.Length; j++)                       //Para cada carpeta de planillas
                    {
                        string pathPlanillaAsistencia = Directory.GetFiles(planillasDirectories[j])[0];                          //Obtengo la dirección de la carpeta actual

                        FileStream streamPlanillaAsistencia = new FileStream(pathPlanillaAsistencia, FileMode.Open);
                        SaveDataPlanillaAsistencia dataPlanillaAsistencia = (SaveDataPlanillaAsistencia)formatter.Deserialize(streamPlanillaAsistencia);

                        equipo.AgregarPlanillaAsistencia(dataPlanillaAsistencia);

                        string[] detallesFiles = Directory.GetFiles(planillasDirectories[j] + "/Detalles");              //Vector de todos los detalles en la carpeta actual

                        for (int k = 0; k < detallesFiles.Length; k++)                          //Para cada detalle
                        {
                            FileStream streamDetalles = new FileStream(detallesFiles[k], FileMode.Open);

                            SaveDataPlanilla dataDetalle = (SaveDataPlanilla)formatter.Deserialize(streamDetalles);

                            //Agregar la Planilla al Equipo   
                            equipo.AgregarDetalle(new DetalleAsistencia(dataDetalle), dataDetalle.GetNombrePlanilla());

                            streamDetalles.Close();
                        }
                    }
                }
                #endregion

                //Agregar Equipo a la lista de Equipos
                AppController.instance.AgregarEquipo(equipo);
            }
        }

        #region Jugadas
        //Cargar imagenes
        if (Directory.Exists(pathImagenJugadas))
        {
            //Por cada imagen guardada creo un objeto imagen
            string[] pathsCarpeta = Directory.GetDirectories(pathImagenJugadas);

            Debug.Log("CANT: " + pathsCarpeta.Length);

            for (int i = 0; i < pathsCarpeta.Length; i++)
            {
                string _nombreCarpeta = Path.GetFileName(pathsCarpeta[i]);
                if (_nombreCarpeta == "-") 
                    _nombreCarpeta = "SIN CARPETA";

                CarpetaJugada _carpeta = new CarpetaJugada(_nombreCarpeta);

                string[] pathCarpetasJugadas = Directory.GetDirectories(pathsCarpeta[i]);

                for (int k = 0; k < pathCarpetasJugadas.Length; k++)
                {
                    string[] pathArchivos = Directory.GetFiles(pathCarpetasJugadas[k]);

                    byte[] bytes = null;
                    string nombre = string.Empty;
                    string categoria = string.Empty;

                    for (int j = 0; j < pathArchivos.Length; j++)
                    {
                        if (Path.GetExtension(pathArchivos[j]) == ".png")
                        {
                            nombre = Path.GetFileNameWithoutExtension(pathArchivos[j]);

                            bytes = File.ReadAllBytes(pathArchivos[j]);
                        }
                        else
                        {
                            FileStream streamCategoria = new FileStream(pathArchivos[j], FileMode.Open);
                            categoria = (string)formatter.Deserialize(streamCategoria);
                        }
                    }

                    ImagenBiblioteca _imagenBiblioteca = new ImagenBiblioteca(bytes, nombre, categoria, _carpeta);
                    //AppController.instance.AgregarImagen(_imagenBiblioteca);
                    _carpeta.AgregarJugada(_imagenBiblioteca);
                }
                AppController.instance.AgregarCarpetaJugada(_carpeta);
            }

            if(!Directory.Exists(pathImagenJugadas + "-"))
            {
                Directory.CreateDirectory(pathImagenJugadas + "-");
                AppController.instance.AgregarCarpetaJugada(new CarpetaJugada("SIN CARPETA"));
            }
        }
        #endregion
    }
}