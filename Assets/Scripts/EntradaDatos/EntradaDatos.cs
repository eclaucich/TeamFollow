using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Clase genérica de las entrada de datos,
/// cada deporte tendrá una entrada de datos que herede de ésta
/// </summary>

public class EntradaDatos : Panel
{    
    virtual public void GuardarEntradaDatos()
    {

    }

    virtual public void CancelarGuardado()
    {

    }

    virtual public void DescartarDatos()
    {
        CanvasController.instance.escenas.Add(1);
        CanvasController.instance.retrocesoPausado = false;
        CanvasController.instance.MostrarPanelAnterior();
    }

    virtual public void Display(bool isPartido)
    {

    }

    virtual public void TerminarSeleccionJugadores(List<Jugador> listaJugadores, int cantSeleccionados)
    {

    }
}
