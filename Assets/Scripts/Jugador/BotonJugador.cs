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

    public void Start()
    {
        panelJugadores = GameObject.Find("PanelJugadores").GetComponent<PanelJugadores>();          //Se busca el panel por nombre, solo puede haber uno
        panelConfirmacionBorrado = AppController.instance.panelConfirmacionBorradoJugador;
    }

    public void SetNombreJugador(string nombre)
    {
        nombreJugador = nombre;
        nombreJugadorText.text = nombreJugador;
    }

    public void MostrarDetallesJugador()                                                            //Se muestran todos los detalles del jugador relacionado con el prefab
    {
        panelJugadores.MostrarPanelInfoJugador(nombreJugador);
        AppController.instance.jugadorActual = AppController.instance.equipoActual.BuscarPorNombre(nombreJugador);
    }

    public void AbrirPanelConfirmacionBorrado()
    {
        panelConfirmacionBorrado.GetComponent<ConfirmacionBorradoJugador>().Activar(nombreJugador, gameObject);
    }
}
