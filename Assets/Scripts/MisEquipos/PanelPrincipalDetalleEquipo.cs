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
        Equipo equipoActual = AppController.instance.equipoActual;

        AppController.instance.overlayPanel.SetNombrePanel("EQUIPO: " + equipoActual.GetNombre(), AppController.Idiomas.Español);
        AppController.instance.overlayPanel.SetNombrePanel("TEAM: " + equipoActual.GetNombre(), AppController.Idiomas.Ingles);

        if (AppController.instance.equipoActual.GetDeporte() == Deportes.DeporteEnum.Tenis)
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
