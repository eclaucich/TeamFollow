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

public class BotonJugador : MonoBehaviour
{

    [SerializeField] private PanelJugadoresPrincipal panelJugadoresPrincipal = null;
    [SerializeField] private Text nombreJugadorText = null;                                                
    private string nombreJugador;

    private PanelJugadores panelJugadores;                                                          

    private ConfirmacionBorradoJugador panelConfirmacionBorrado;

    [SerializeField] private Image imagenFavorito = null;
    [SerializeField] private Sprite estrellaVacia = null;
    [SerializeField] private Sprite estrellaLlena = null;

    [SerializeField] private Image imagenFondo = null;

    [Space]
    [Header("Seleccion Multiple")]
    [SerializeField] private Toggle toggleSeleccion = null;

    [Space]
    [Header("Botones")]
    [SerializeField] private GameObject botonBorrar = null;
    [SerializeField] private GameObject botonFavorito = null;
    [SerializeField] private GameObject botonEstadisticas = null;

    private Jugador jugadorFocus;

    private bool seleccionMultipleActivada = false;


    public void Start()
    {
        panelJugadores = GameObject.Find("PanelJugadores").GetComponent<PanelJugadores>();          
        panelConfirmacionBorrado = panelJugadoresPrincipal.GetPanelConfirmacionBorrado();
        toggleSeleccion.isOn = false;
        toggleSeleccion.gameObject.SetActive(false);
    }

    private void Update()
    {
        if(seleccionMultipleActivada)
        {
            if(toggleSeleccion.isOn)
                imagenFondo.color = AppController.instance.colorTheme.botonSeleccionado;
            else
                imagenFondo.color = AppController.instance.colorTheme.detalle4;
        }
    }

    public void SetJugadorFocus(Jugador _jugador)
    {
        jugadorFocus = _jugador;
        nombreJugador = _jugador.GetNombre();
        nombreJugadorText.text = nombreJugador;
    }

    public void MostrarDetallesJugador()                                                            
    {
        if(!seleccionMultipleActivada)
        {
            AppController.instance.jugadorActual = AppController.instance.equipoActual.BuscarPorNombre(nombreJugador);
            panelJugadoresPrincipal.SetBotonJugadorFocus(gameObject);
            panelJugadores.MostrarPanelInfoJugador();
        }
        else
        {
            toggleSeleccion.isOn = !toggleSeleccion.isOn;
        }
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

    #region Edicion Multiple
    public void SetSeleccionMultiple(bool aux)
    {
        seleccionMultipleActivada = aux;
        toggleSeleccion.gameObject.SetActive(aux);
        botonBorrar.SetActive(!aux);
        botonFavorito.SetActive(!aux);
        botonEstadisticas.SetActive(!aux);
        if(aux==false)
            imagenFondo.color = AppController.instance.colorTheme.detalle4;
    }

    public bool IsSelected()
    {
        return toggleSeleccion.isOn;
    }
    #endregion
}
