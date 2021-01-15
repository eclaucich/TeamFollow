using UnityEngine;
using UnityEngine.UI;

public class ConfirmacionBorradoJugada : MensajeDesplegable
{
    [SerializeField] private PanelPrincipalBiblioteca panelPrincipalBiblioteca = null;
    private BotonImagen botonFocus;
    
    public void Activar(BotonImagen botonImagen)
    {
        botonFocus = botonImagen;
        //textoConfirmacion.text = "Borrar jugada \"" + botonFocus.GetNombre() + "\"?";
        text.SetText("Borrar jugada \"" + botonFocus.GetNombre() + "\"?".ToUpper(), AppController.Idiomas.Español);
        text.SetText("Borrar strategy \"" + botonFocus.GetNombre() + "\"?".ToUpper(), AppController.Idiomas.Ingles);
        ToggleDesplegar();
    }

    public void BorrarJugada()
    {
        ToggleDesplegar();
        botonFocus.BorrarJugadaFocus();
        panelPrincipalBiblioteca.SetPanePrincipal();
    }
}
