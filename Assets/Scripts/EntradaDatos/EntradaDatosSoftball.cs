using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntradaDatosSoftball : EntradaDatos
{
    ///ESTADOS
    enum Estados
    {
        EleccionAtaqueDefensa,
        Ataque,
        EleccionBateador,
        EventoBateador,
        Strike,
        OutAtaque,
        Golpe,
        RoboBase,
        EventoBase,
        EleccionNuevaBase,
        Carrera,
        TerminarAtaque,
        Defensa,
        EventoLanzador,
        EventoCatcher,
        EventoJugador,
        OutDefensa,
        TerminarDefensa
    }

    /// JUGADORES EN LA ENTRADA DE DATOS
    private List<Jugador> jugadores;

    ///ELEMENTOS DE LA ENTRADA DE DATOS
    private List<GameObject> listaGameObjects;
    private List<BotonSoftball> listaBotones;
    [SerializeField] private GameObject eleccionAtaqueDefensa = null; //TE DEJA ELEGIR SI EMPEZAR EN ATAQUE O DEFENSA
    [SerializeField] private GameObject eleccionBateador = null;    // TE DEJA ELEGIR EL BATEADOR
    [SerializeField] private GameObject eleccionCambioJugador = null;  // TE DEJA CAMBIAR UN JUGADOR POR OTRO
    [SerializeField] private GameObject eleccionCambiarJugadores = null;  // TE DEJA ELEGIR SI SE MANTIENEN LOS JUGADORES O HAY CAMBIOS
    [SerializeField] private GameObject mensajeElegirNuevaBase = null; 
    [SerializeField] private GameObject botonSiguienteBateador = null; 
    [SerializeField] private GameObject botonSiguienteLanzamiento = null;

    [SerializeField] private MarcadorSoftball marcador = null;

    [SerializeField] private BotonSoftball primera = null;
    [SerializeField] private BotonSoftball segunda = null;
    [SerializeField] private BotonSoftball tercera = null;
    [SerializeField] private BotonSoftball cuarta = null;

    [SerializeField] private BotonSoftball pitcher = null;
    [SerializeField] private BotonSoftball catcher = null;
    [SerializeField] private BotonSoftball bateador = null;
    [SerializeField] private BotonSoftball jardinero1 = null;


    ///VARIABLES AUXILIARES
    private int strikes = 0;
    private int outs = 0;



    private Estados estadoActual;

    private void Start()
    {
        jugadores = new List<Jugador>();

        listaGameObjects = new List<GameObject>();
        listaGameObjects.Add(eleccionAtaqueDefensa);
        listaGameObjects.Add(eleccionBateador);
        listaGameObjects.Add(eleccionCambioJugador);
        listaGameObjects.Add(botonSiguienteBateador);
        listaGameObjects.Add(botonSiguienteLanzamiento);
        listaGameObjects.Add(eleccionCambiarJugadores);
        listaGameObjects.Add(mensajeElegirNuevaBase);

        listaBotones = new List<BotonSoftball>();
        listaBotones.Add(primera);
        listaBotones.Add(segunda);
        listaBotones.Add(tercera);
        listaBotones.Add(cuarta);
        listaBotones.Add(pitcher);
        listaBotones.Add(catcher);
        listaBotones.Add(bateador);
        listaBotones.Add(jardinero1);

        NuevoEstado(Estados.EleccionAtaqueDefensa);
    }


    private void NuevoEstado(Estados _nuevoEstado)
    {
        ///SEGUN EL ESTADO ACTUAL SETEAR EL PANEL COMO SEA DEBIDO
        estadoActual = _nuevoEstado;

        switch (estadoActual)
        {
            case Estados.EleccionAtaqueDefensa:
                ///MOSTRAR DOS BOTONES ATAQUE Y DEFENSA
                ///SEGUN EL QUE SE ELIJA SE LLAMA A UNA FUNCION QUE SETEA EL ESTADO CORRESPONDIENTE
                DesactivarGameObjects();
                eleccionAtaqueDefensa.SetActive(true);
                DesactivarBotones();

                break;
            case Estados.EleccionBateador:
                ///MOSTRAR LA LISTA DE JUGADORES, EL QUE SE PRESIONA ES EL BATEADOR
                ///APRENTANDO EL JUGADOR SE SETEA EL SIGUIENTE ESTADO
                DesactivarGameObjects();
                eleccionBateador.SetActive(true);
                DesactivarBotones();

                strikes = 0;

                break;
            case Estados.EventoBateador:
                ///AL APRETAR LA PRIMERA BASE SE MUESTRAN LAS OPCIONES DEL BATEADOR
                DesactivarGameObjects();
                ActivarBotones();
                bateador.MostrarOpciones();

                break;
            case Estados.Strike:
                ///AUMENTAR EL CONTADOR DE STRIKES
                ///CONTROLAR EL SIGUIENTE ESTADO: 
                ///EVENTO BATEADOR, OUT
                break;
            case Estados.OutAtaque:
                ///CONTROLA EL SIGUIENTE ESTADO
                ///ELECCION BATEADOR, TERMINAR ATAQUE
                break;
            case Estados.TerminarAtaque:
                ///MOSTRAR QUE SE TERMINO EL ATAQUE Y QUE COMIENZA LA DEFENSA
                ///SIGUIENTE ESTADO -> Defensa

                DesactivarGameObjects();
                eleccionCambiarJugadores.SetActive(true);

                break;
            case Estados.EventoBase:
                ///AL APRETAR CUALQUIER BASE OCUPADA LO QUE DA DOS OPCIONES
                ///OUT O CORRER
                ///SI ES CORRER SE PASA AL ESTADO nuevaBase
                ///MOSTRAR UN BOTON QUE SEA SIGUIENTE BATEADOR
                ///AL APRETARLO SE VA AL ESTADO elegirBateador
                break;
            case Estados.RoboBase:
                ///LUEGO DE CIERTO ESTADO DEL BATEADOR SE PERMITE APRETAR ALGUNA BASE OCUPADA
                ///SE MUESTARN DOS OPCIONES
                ///OUT o ROBADA
                ///SI ES ROBADA TE DEJA PRESIONAR EL ESTADO ES nuevaBase
                break;
            case Estados.EleccionNuevaBase:
                ///AL PRESIONAR OTRA BASE EL JUGADOR SE MUEVE A ESTA
                ///HACER LO NECESARIO SI LA BASE YA ESTABA OCUPADA
                ///AUTOMATICAMENTE MOSTRAR LAS DOS OPCIONES PARA ESE JUGAODR QUE YA ESTABA EN LA BASE
                ///PASAR AL ESTADO EventoBase

                mensajeElegirNuevaBase.SetActive(true);

                break;
            case Estados.Carrera:
                ///SI LA NUEVA BASE ES LA PRIMERA, AUMENTAR EL CONTADOR DE CARRERAS
                ///VOLVER AL ESTADO DE EVENTO BASE
                break;
            case Estados.Defensa:
                ///SETEAR TODO PARA LA DEFENSA

                break;
            case Estados.EventoLanzador:
                ///AL APRETAR AL LANZADOR SE DAN LAS OPCIONES
                ///CONTROLA SIGUIENTE ESTADO SEGUN LA OPCION ELEGIDA
                break;
            case Estados.EventoCatcher:
                ///DESPUES DEL EVENTO LANZADOR SE PUEDE APRETAR AL PITCHER MOSTANDO LAS OPCIONES DE ESTE
                ///CONTROLAR SIGUIENTE ESTADO
                break;
            case Estados.EventoJugador:
                ///AL APRETAR CUALQUIER OtRO JUGADOR SE MUESTRAN LAS OPCIONES DE ESTE
                ///DAR LA OPCION DE TERMINAR Y VOLVER AL EVENTO LANZADOR
                break;
            case Estados.OutDefensa:
                ///AUMENTAR EL CONTADOR DE OUTS
                ///CONTROLAR EL SIGUIENTE ESTADO 
                break;
            case Estados.TerminarDefensa:
                ///INDICAR CUANTAS CARRERARA HIZO EL CONTRARIO
                break;
        }
    }

    public void ComenzarAtaque()
    {
        NuevoEstado(Estados.EleccionBateador);
    }

    public void ComenzarDefensa()
    {
        NuevoEstado(Estados.Defensa);
    }

    public void FinalizarEleccionBateador(string nombreJugador)
    {
        Jugador jugador = AppController.instance.equipoActual.BuscarPorNombre(nombreJugador);
        bateador.SetJugador(jugador);
        NuevoEstado(Estados.EventoBateador);
    }

    public void Strike()
    {
        strikes++;
        if (strikes == 3)
            Out();
    }

    public void Out()
    {
        outs++;
        if (outs == 3)
            NuevoEstado(Estados.TerminarAtaque);
        else
            NuevoEstado(Estados.EventoBase);


    }

    public void Batear()
    {
        bateador.MostrarOpciones();
    }

    public void Correr()
    {
        NuevoEstado(Estados.EleccionNuevaBase);
    }

    private void DesactivarGameObjects()
    {
        foreach (var GO in listaGameObjects)
            GO.SetActive(false);
    }

    private void DesactivarBotones()
    {
        foreach (var boton in listaBotones)
            boton.DesactivarBoton();
    }

    private void ActivarBotones()
    {
        foreach (var boton in listaBotones)
            boton.ActivarBoton();
    }
}
