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
        base.Start();

        if(AppController.instance.equipoActual.GetDeporte() == "Tenis")
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
