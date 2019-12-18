using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Clase que permite controlar un panel que muestra una lista de botones con los nombres de los jugadores enfocados,
/// se pueden eleir tantos jugadores como el panel lo permita mediante "maxJugadoresSeleccion"
/// </summary>

public class SeleccionListaJugadores : MonoBehaviour
{
    private List<Jugador> listaJugadores;
    private List<Jugador> jugadoresSeleccionados;

    private int actualJugadoresSeleccionados = 0;
    [SerializeField] private int maxJugadoresSeleccionados = 0;    //Si no hay límite setearlo en -1

    [SerializeField] private GameObject botonJugador = null;
    [SerializeField] private Transform transformParent = null;

    /// 
    /// Crea botones con los nombres de los jugadores
    /// 
    public void SetearListaJugadores()
    {
        listaJugadores = AppController.instance.GetEquipoActual().GetJugadores();
        jugadoresSeleccionados = new List<Jugador>();

        foreach (var jugador in listaJugadores)
        {
            GameObject go = Instantiate(botonJugador, transformParent, false);
            go.GetComponentInChildren<Text>().text = jugador.GetNombre();
            go.SetActive(true);
        }
    }

    /// 
    /// Controla qué sucede cuando se presiona un botón con el nombre de un jugador:
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
            
        }
        else  //si no estaba seleccionado, hay que ver si se puede seleccionar
        {
            if (actualJugadoresSeleccionados < maxJugadoresSeleccionados && maxJugadoresSeleccionados > 0)
            {
                actualJugadoresSeleccionados++;
                _boton.ToggleSeleccionado();
                jugadoresSeleccionados.Add(BuscarJugador(listaJugadores,nombreJugador));
            } 
        }
    }

    /// 
    /// Se llama desde el boton "continuar" en el panel de seleccion de jugadores
    /// 
    public void TerminarSeleccion()
    {
        if (actualJugadoresSeleccionados > 0)
        {
            GetComponentInParent<EntradaDatos>().TerminarSeleccionJugadores(jugadoresSeleccionados);
        }
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
