using UnityEngine;

public class PanelPrincipalDetalleEquipo : Panel
{
    [SerializeField] private GameObject botonVerEstadisticasGlobalesEquipo = null;

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
}
