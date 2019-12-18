using UnityEngine;
using UnityEngine.UI;

public class ConfirmacionBorradoEquipo : MensajeDesplegable
{
    private Equipo equipoFocus;
    private GameObject botonEquipoFocus;
    [SerializeField] protected Text textoConfirmacion = null;
    private PanelPrincipal panelPrincipal;

    public override void Start()
    {
        base.Start();
        panelPrincipal = GameObject.FindGameObjectWithTag("PanelMisEquipos").GetComponent<PanelPrincipal>();
    }

    public void Activar(string nombreEquipo, GameObject botonEquipo)
    {
        equipoFocus = AppController.instance.equipos[AppController.instance.BuscarPorNombre(nombreEquipo)];
        botonEquipoFocus = botonEquipo;
        textoConfirmacion.text = "Borrar Equipo \"" + equipoFocus.GetNombre() + "\"?";
        ToggleDesplegar();
    }

    public void BorrarEquipo()
    {
        AppController.instance.BorrarEquipo(equipoFocus.GetNombre());  
        panelPrincipal.BorrarBotonEquipo(botonEquipoFocus);
        ToggleDesplegar();
    }
}
