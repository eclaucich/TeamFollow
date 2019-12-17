using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EleccionBorradoEstadisticas : ConfirmacionBorradoJugador
{
    private PanelJugadoresPrincipal panelJugadoresPrincipal;

    public override void Start()
    {
        base.Start();
        panelJugadoresPrincipal = GameObject.Find("PanelJugadoresPrincipal").GetComponent<PanelJugadoresPrincipal>();
    }

    public override void Activar(string nombreJugador, GameObject botonJugador)
    {
        base.Activar(nombreJugador, botonJugador);
        textoConfirmacion.text = "Se eliminará el jugador \"" + jugadorFocus.GetNombre() + "\"?" + "\n"+ "Desea eliminar las estadísticas relacionadas con éste?" + "\n" + "Se verán afectadas las estadísticas de equipo y de partidos donde se encuentre el jugador";
    }


    public void BorrarJugadorEstadisticas(bool borrarEstadisticas)
    {
        AppController.instance.equipoActual.BorrarJugador(jugadorFocus.GetNombre(), borrarEstadisticas);
        //Destroy(botonJugadorFocus);
        panelJugadoresPrincipal.BorrarBotonJugador(botonJugadorFocus);
        ToggleDesplegar();
    }
}
