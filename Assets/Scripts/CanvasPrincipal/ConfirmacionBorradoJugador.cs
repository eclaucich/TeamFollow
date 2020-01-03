﻿using UnityEngine;
using UnityEngine.UI;

public class ConfirmacionBorradoJugador : MensajeDesplegable
{
    protected Jugador jugadorFocus;
    protected GameObject botonJugadorFocus;
    [SerializeField] protected Text textoConfirmacion = null;
    [SerializeField] private ConfirmacionBorradoJugador eleccionBorradoEstadistica = null;

    /*private PanelJugadoresPrincipal panelJugadoresPrincipal;

    void Start()
    {
        panelJugadoresPrincipal = GameObject.Find("PanelJugadoresPrincipal").GetComponent<PanelJugadoresPrincipal>();
    }*/

    virtual public void Activar(string nombreJugador, GameObject botonJugador)
    {
        jugadorFocus = AppController.instance.equipoActual.BuscarPorNombre(nombreJugador);
        textoConfirmacion.text = "Borrar Jugador \"" + jugadorFocus.GetNombre() + "\"?";
        botonJugadorFocus = botonJugador;
        ToggleDesplegar();
    }

    public void BorrarJugador()
    {
        eleccionBorradoEstadistica.Activar(jugadorFocus.GetNombre(), botonJugadorFocus);
        this.Cerrar();
        //panelJugadoresPrincipal.BorrarBotonJugador(botonJugadorFocus);
    }
}
