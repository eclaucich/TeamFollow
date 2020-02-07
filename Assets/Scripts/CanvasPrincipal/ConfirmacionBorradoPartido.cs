using UnityEngine;

public class ConfirmacionBorradoPartido : ConfirmacionBorradoJugador
{
    BotonPartido botonFocus;

    [SerializeField] private PanelPartidos panelPartidos = null;
    [SerializeField] private PanelPartidosEquipo panelPartidosEquipo = null;

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
        ToggleDesplegar();
        if (partidoJugador) panelPartidos.BorrarPartido(botonFocus);
        else                panelPartidosEquipo.BorrarPartido(botonFocus);
        CanvasController.instance.MostrarPanelAnterior();
    }

}
