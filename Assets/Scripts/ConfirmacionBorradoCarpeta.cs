using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmacionBorradoCarpeta : MensajeDesplegable
{
    [SerializeField] private PanelPrincipalBiblioteca panelPrincipalBiblioteca = null;
    private CarpetaJugada _carpetaFocus;

    public void Activar(BotonCarpetaJugada _botonCarpeta)
    {
        AndroidManager.HapticFeedback();
        _carpetaFocus = _botonCarpeta.GetCarpeta();

        text.SetText("Borrar carpeta \"" + _carpetaFocus.GetNombre() + "\"? Todas las jugadas en esta carpeta serán eliminadas".ToUpper(), AppController.Idiomas.Español);
        text.SetText("Delete folder \"" + _carpetaFocus.GetNombre() + "\"? Every strategy in this folder will be deleted".ToUpper(), AppController.Idiomas.Ingles);
        ToggleDesplegar();
    }

    public void BorrarCarpeta()
    {
        _carpetaFocus.BorrarCarpeta();
        ToggleDesplegar();
        panelPrincipalBiblioteca.SetPanePrincipal();
    }
}
