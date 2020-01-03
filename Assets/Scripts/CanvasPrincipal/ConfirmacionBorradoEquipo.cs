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
        Debug.Log("ACTIVADO: " + botonEquipoFocus.name);
    }

    public void BorrarEquipo()
    {
        /*CanvasController.instance.AbrirMisEquipos();
        panelPrincipal.BorrarBotonEquipo(botonEquipoFocus);
        AppController.instance.BorrarEquipo(equipoFocus.GetNombre());
        Cerrar();
        botonEquipoFocus = null;
        */
        ToggleDesplegar();
        panelPrincipal.BorrarBotonEquipo(botonEquipoFocus);
        AppController.instance.BorrarEquipo(equipoFocus.GetNombre()); 
        CanvasController.instance.MostrarPanelAnterior(); 
    }
}
