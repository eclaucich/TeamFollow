using UnityEngine;

public class PanelPrincipalDetalleEquipo : Panel
{
    [SerializeField] private GameObject botonVerEstadisticasGlobalesEquipo = null;
    [SerializeField] private ConfirmacionBorradoEquipo panelConfirmacionBorrado = null;

    private GameObject botonEquipoFocus;

    public void SetBotonEquipoFocus(GameObject botonEquipo_)
    {
        botonEquipoFocus = botonEquipo_;
    }

    public void SetBotonesDisponibles()
    {
        AppController.instance.overlayPanel.SetNombrePanel(AppController.instance.equipoActual.GetNombre());

        if(AppController.instance.equipoActual.GetDeporte() == Deportes.Deporte.Tenis)
        {
            botonVerEstadisticasGlobalesEquipo.SetActive(false);
        }
        else
        {
            botonVerEstadisticasGlobalesEquipo.SetActive(true);
        }
    }

    public void AbrirPanelConfirmacionBorrado()
    {
        panelConfirmacionBorrado.Activar(AppController.instance.equipoActual.GetNombre(), botonEquipoFocus);
    }
}
