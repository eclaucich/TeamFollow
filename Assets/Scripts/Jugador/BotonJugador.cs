using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// Prefab que se crea por cada jugador existente en un equipo.
/// Se crea en el PANEL JUGADORES
/// El prefab tiene dos botones:
///     Uno para mostrar los detalles de ese jugador
///     Y otro para borrar el jugador junto con el prefab
/// 
/// </summary>

public class BotonJugador : MonoBehaviour {

    [SerializeField] private Text nombreJugadorText = null;                                                //Texto que muestra el nombre del jugador
    private string nombreJugador;

    private PanelJugadores panelJugadores;                                                          //Panel principal PANEL JUGADORES

    private GameObject panelConfirmacionBorrado;
    private PanelJugadoresPrincipal panelJugadoresPrincipal;

    public void Start()
    {
        panelJugadores = GameObject.Find("PanelJugadores").GetComponent<PanelJugadores>();          //Se busca el panel por nombre, solo puede haber uno
        panelConfirmacionBorrado = AppController.instance.panelConfirmacionBorradoJugador;
        panelJugadoresPrincipal = GameObject.Find("PanelJugadoresPrincipal").GetComponent<PanelJugadoresPrincipal>();
    }

    public void SetNombreJugador(string nombre)
    {
        nombreJugador = nombre;
        nombreJugadorText.text = nombreJugador;
    }

    public void MostrarDetallesJugador()                                                            //Se muestran todos los detalles del jugador relacionado con el prefab
    {
        AppController.instance.jugadorActual = AppController.instance.equipoActual.BuscarPorNombre(nombreJugador);
        panelJugadoresPrincipal.SetBotonJugadorFocus(gameObject);
        panelJugadores.MostrarPanelInfoJugador(/*nombreJugador*/);
    }

    public void MostrarPartidosJugador()
    {
        panelJugadoresPrincipal.SetBotonJugadorFocus(gameObject);
        AppController.instance.jugadorActual = AppController.instance.equipoActual.BuscarPorNombre(nombreJugador);
        CanvasController.instance.AgregarPanelAnterior(CanvasController.Paneles.DetalleEquipoPrincipal);
        panelJugadores.MostrarPanelPartidos();
    }

    public void AbrirPanelConfirmacionBorrado()
    {
        panelConfirmacionBorrado.GetComponent<ConfirmacionBorradoJugador>().Activar(nombreJugador, gameObject);
    }

    public string GetNombre()
    {
        return nombreJugador;
    }
}
