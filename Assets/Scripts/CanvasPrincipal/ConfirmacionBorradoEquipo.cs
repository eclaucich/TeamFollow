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

    ///Al apretar el "borrar" se guarda el equipo y el boton en cuestion
    public void Activar(string nombreEquipo, GameObject botonEquipo)
    {
        equipoFocus = AppController.instance.equipos[AppController.instance.BuscarPorNombre(nombreEquipo)];
        botonEquipoFocus = botonEquipo;
        textoConfirmacion.text = "Borrar Equipo \"" + equipoFocus.GetNombre() + "\"?";
        ToggleDesplegar();
    }

    public void BorrarEquipo()
    {
        ToggleDesplegar();
        panelPrincipal.BorrarBotonEquipo(botonEquipoFocus);
        AppController.instance.BorrarEquipo(equipoFocus.GetNombre()); 
        CanvasController.instance.MostrarPanelAnterior(); 
    }
}
