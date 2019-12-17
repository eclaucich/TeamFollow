using UnityEngine;

public class ConfirmacionBorradoPartido : ConfirmacionBorradoJugador
{
    BotonPartido botonFocus;

    public void ActivarPanel(BotonPartido botonPartido)
    {
        textoConfirmacion.text = "Borrar partido? \n Se verán afectadas las estadísticas de los jugadores y/o del equipo";
        ToggleDesplegar();
        botonFocus = botonPartido;
    }

    ///El booleano es para verificar si se esta borrando un partido de un jugador especifico (true)
    ///O si se esta borrando todo un partido de un equipo (false)
    public void ConfirmarBorrar(bool partidoJugador)
    {
        if (partidoJugador) GameObject.Find("PanelPartidosJugador").GetComponent<PanelPartidos>().BorrarPartido(botonFocus);
        else                GameObject.Find("PanelEstadisticasGlobalesEquipo").GetComponent<PanelPartidosEquipo>().BorrarPartido(botonFocus);
        ToggleDesplegar();
    }

}
