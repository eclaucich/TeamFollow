using UnityEngine;

public class ConfirmacionBorradoPartido : ConfirmacionBorradoJugador
{
    Partido partidoFocus;

    [SerializeField] private PanelPartidos panelPartidos = null;
    [SerializeField] private PanelPartidosEquipo panelPartidosEquipo = null;

    public void ActivarPanel(Partido _partido)
    {
        Debug.Log("Idioma: "+ AppController.instance.idioma);
        //textoConfirmacion.text = $"Borrar partido {_partido.GetNombre()} ? \n Se verán afectadas las estadísticas de los jugadores y/o del equipo";
        text.SetText($"Borrar partido {_partido.GetNombre()} ? \n Se verán afectadas las estadísticas de los jugadores y/o del equipo".ToUpper(), AppController.Idiomas.Español);
        text.SetText($"Delete match {_partido.GetNombre()} ? \n All team and player statistics will be affected".ToUpper(), AppController.Idiomas.Ingles);
        ToggleDesplegar();
        partidoFocus = _partido;
    }

    ///El booleano es para verificar si se esta borrando un partido de un jugador especifico (true)
    ///O si se esta borrando todo un partido de un equipo (false)
    public void ConfirmarBorrar(bool partidoJugador)
    {
        ToggleDesplegar();
        if (partidoJugador) panelPartidos.BorrarPartido(partidoFocus);
        else                panelPartidosEquipo.BorrarPartido(partidoFocus);
        CanvasController.instance.MostrarPanelAnterior();
    }

}
