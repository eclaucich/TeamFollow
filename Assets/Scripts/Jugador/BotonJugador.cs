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

    [SerializeField] private PanelJugadoresPrincipal panelJugadoresPrincipal = null;
    [SerializeField] private Text nombreJugadorText = null;                                                
    private string nombreJugador;

    private PanelJugadores panelJugadores;                                                          

    private ConfirmacionBorradoJugador panelConfirmacionBorrado;

    [SerializeField] private Image imagenFavorito = null;
    [SerializeField] private Sprite estrellaVacia = null;
    [SerializeField] private Sprite estrellaLlena = null;

    private Jugador jugadorFocus;

    public void Start()
    {
        panelJugadores = GameObject.Find("PanelJugadores").GetComponent<PanelJugadores>();          
        panelConfirmacionBorrado = panelJugadoresPrincipal.GetPanelConfirmacionBorrado();
    }

    public void SetJugadorFocus(Jugador _jugador)
    {
        jugadorFocus = _jugador;
        nombreJugador = _jugador.GetNombre();
        nombreJugadorText.text = nombreJugador;
    }

    public void MostrarDetallesJugador()                                                            
    {
        AppController.instance.jugadorActual = AppController.instance.equipoActual.BuscarPorNombre(nombreJugador);
        panelJugadoresPrincipal.SetBotonJugadorFocus(gameObject);
        panelJugadores.MostrarPanelInfoJugador();
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
        AndroidManager.HapticFeedback();
        panelConfirmacionBorrado.Activar(nombreJugador, gameObject);
    }

    public string GetNombre()
    {
        return nombreJugador;
    }

    public void SetComoFavorito()
    {
        AppController.instance.equipoActual.SetJugadorFavorito(jugadorFocus.GetNombre());
        panelJugadoresPrincipal.ResetFavouritePlayers();
        imagenFavorito.sprite = estrellaLlena;
        transform.SetAsFirstSibling();
    }

    private void SetComoFavoritoFeedBack(){
        SetComoFavorito();
        panelJugadoresPrincipal.MensajeFavorito();
    }

    public void RemoverFavorito()
    {
        AppController.instance.equipoActual.SetJugadorFavorito(null);
        imagenFavorito.sprite = estrellaVacia;
    }

    public void GuardarComoFavorito()
    {
        Jugador _jugadorFavoritoActual= AppController.instance.equipoActual.GetJugadorFavorito();
        if (_jugadorFavoritoActual != null && _jugadorFavoritoActual == jugadorFocus)
        {
            RemoverFavorito();
            SaveSystem.SaveFavouritePlayer(AppController.instance.equipoActual, null);
        }
        else
        {
            SetComoFavoritoFeedBack();
            SaveSystem.SaveFavouritePlayer(AppController.instance.equipoActual, jugadorFocus);
        }
    }

    public void DesactivarFavorito()
    {
        imagenFavorito.sprite = estrellaVacia;
    }

    public Jugador GetJugadorFocus()
    {
        return jugadorFocus;
    }
}
