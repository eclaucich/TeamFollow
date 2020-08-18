using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Clase que permite controlar un panel que muestra una lista de botones con los nombres de los jugadores enfocados,
/// se pueden eleir tantos jugadores como el panel lo permita mediante "maxJugadoresSeleccion"
/// </summary>

public class SeleccionListaJugadores : MonoBehaviour
{
    [SerializeField] private MensajeError mensajeErrorSeleccionJugadores = null;

    [SerializeField] private FlechasScroll flechasScroll = null;
    [SerializeField] private ScrollRect scrollRect = null;
    [SerializeField] private Transform transformListaJugadores = null;

    [SerializeField] private GameObject opcionesAdicionales = null;

    private List<Jugador> listaJugadores;
    private List<Jugador> jugadoresSeleccionados;

    private int actualJugadoresSeleccionados = 0;
    [SerializeField] private int maxJugadoresSeleccionados = 0;    //Si no hay límite setearlo en -1
    private BotonSeleccionJugador ultimoBotonSeleccionado;

    [SerializeField] private GameObject botonJugador = null;
    [SerializeField] private Transform transformParent = null;

    private int cantMinima;
    private float prefabHeight;

    private void Start()
    {
        prefabHeight = botonJugador.GetComponent<RectTransform>().rect.height;
        mensajeErrorSeleccionJugadores.SetText("Seleccionar al menos un jugador".ToUpper(), AppController.Idiomas.Español);
        mensajeErrorSeleccionJugadores.SetText("Select at least one player".ToUpper(), AppController.Idiomas.Ingles);
    }

    private void FixedUpdate()
    {
        if(listaJugadores != null) flechasScroll.Actualizar(scrollRect, cantMinima, listaJugadores.Count);
    }

    /// 
    /// Crea botones con los nombres de los jugadores
    /// 
    public void SetOpcionesAdicioanles()
    {
        if (actualJugadoresSeleccionados <= 0)
        {
            mensajeErrorSeleccionJugadores.Activar();
            return;
        }
        opcionesAdicionales.SetActive(true);
    }

    public void SetearListaJugadores()
    {
        gameObject.SetActive(true);

        CanvasController.instance.overlayPanel.SetNombrePanel("SELECCION JUGADORES", AppController.Idiomas.Español);
        CanvasController.instance.overlayPanel.SetNombrePanel("PLAYERS SELECTION", AppController.Idiomas.Ingles);

        listaJugadores = AppController.instance.equipoActual.GetJugadores();
        jugadoresSeleccionados = new List<Jugador>();

        foreach (var jugador in listaJugadores)
        {
            GameObject go = Instantiate(botonJugador, transformParent, false);
            go.GetComponentInChildren<Text>().text = jugador.GetNombre();
            go.SetActive(true);
            go.GetComponent<BotonNormal>().SetColorActivado();
        }

        if(prefabHeight == 0) prefabHeight = botonJugador.GetComponent<RectTransform>().rect.height;
        cantMinima = (int)(scrollRect.GetComponent<RectTransform>().rect.height / (prefabHeight + transformParent.GetComponent<VerticalLayoutGroup>().spacing));
    }

    /// 
    /// Controlar qué sucede cuando se presiona un botón con el nombre de un jugador:
    /// se le cambia el color al botón para indicar que fue seleccionado/deseleccionado,
    /// se agrega/elimina el jugador de la lista de los jugadores elegidos
    /// 
    public void SeleccionarJugador(BotonSeleccionJugador _boton)
    {
        string nombreJugador = _boton.GetComponentInChildren<Text>().text;

        if (_boton.isSeleccionado()) //si estaba seleccionado, lo deselecciono
        {
            jugadoresSeleccionados.Remove(BuscarJugador(jugadoresSeleccionados, nombreJugador));
            actualJugadoresSeleccionados--;
            _boton.ToggleSeleccionado();  
            ultimoBotonSeleccionado = null;
        }
        else  //si no estaba seleccionado, hay que ver si se puede seleccionar
        {
            if(actualJugadoresSeleccionados == 1 && maxJugadoresSeleccionados == 1)
            {   
                jugadoresSeleccionados.Remove(BuscarJugador(jugadoresSeleccionados, ultimoBotonSeleccionado.GetComponentInChildren<Text>().text));
                ultimoBotonSeleccionado.ToggleSeleccionado();

                _boton.ToggleSeleccionado();
                jugadoresSeleccionados.Add(BuscarJugador(listaJugadores, nombreJugador));

                ultimoBotonSeleccionado = _boton;
            }
            else if (maxJugadoresSeleccionados == -1 || (actualJugadoresSeleccionados < maxJugadoresSeleccionados && maxJugadoresSeleccionados > 0))
            {
                actualJugadoresSeleccionados++;
                _boton.ToggleSeleccionado();
                jugadoresSeleccionados.Add(BuscarJugador(listaJugadores,nombreJugador));
                ultimoBotonSeleccionado = _boton;
            } 
        }
    }

    /// 
    /// Se llama desde el boton "continuar" en el panel de seleccion de jugadores
    /// 
    public void TerminarSeleccion()
    {
        if (actualJugadoresSeleccionados <= 0)
        {
            mensajeErrorSeleccionJugadores.Activar();
            return;
        }
        GetComponentInParent<EntradaDatos>().TerminarSeleccionJugadores(jugadoresSeleccionados, actualJugadoresSeleccionados);
    }

    /// 
    /// Función auxliar para buscar en una lista, a un jugador por su nombre
    /// 
    private Jugador BuscarJugador(List<Jugador> _lista, string nombre)
    {
        foreach (var jugador in _lista)
        {
            if (jugador.GetNombre() == nombre)
                return jugador;
        }

        return null;
    }
}
