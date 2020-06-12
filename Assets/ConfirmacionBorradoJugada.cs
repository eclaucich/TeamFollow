using UnityEngine;
using UnityEngine.UI;

public class ConfirmacionBorradoJugada : MensajeDesplegable
{
    [SerializeField] private PanelPrincipalBiblioteca panelPrincipalBiblioteca = null;
    [SerializeField] private Text textoConfirmacion = null;
    private BotonImagen botonFocus;
    
    public void Activar(BotonImagen botonImagen)
    {
        botonFocus = botonImagen;
        textoConfirmacion.text = "Borrar jugada \"" + botonFocus.GetNombre() + "\"?";
        ToggleDesplegar();
    }

    public void BorrarJugada()
    {
        ToggleDesplegar();
        AppController.instance.BorrarJugada(botonFocus.GetNombre());
        panelPrincipalBiblioteca.SetPanePrincipal();
    }
}
