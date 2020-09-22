using UnityEngine;
using UnityEngine.UI;

public class ConfirmacionBorradoJugador : MensajeDesplegable
{
    protected Jugador jugadorFocus;
    protected GameObject botonJugadorFocus;
    [SerializeField] private ConfirmacionBorradoJugador eleccionBorradoEstadistica = null;


    virtual public void Activar(string nombreJugador, GameObject botonJugador)
    {
        jugadorFocus = AppController.instance.equipoActual.BuscarPorNombre(nombreJugador);
        //textoConfirmacion.text = "Borrar Jugador \"" + jugadorFocus.GetNombre() + "\"?";
        text.SetText("Borrar jugador \"" + jugadorFocus.GetNombre() + "\"?".ToUpper(), AppController.Idiomas.Español);
        text.SetText("Delete player \"" + jugadorFocus.GetNombre() + "\"?".ToUpper(), AppController.Idiomas.Ingles);
        botonJugadorFocus = botonJugador;
        ToggleDesplegar();
    }

    public void BorrarJugador()
    {
        eleccionBorradoEstadistica.Activar(jugadorFocus.GetNombre(), botonJugadorFocus);
        this.Cerrar();
    }
}
