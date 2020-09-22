using UnityEngine;
using UnityEngine.UI;

public class PanelNuevoEquipo : Panel {

    [SerializeField] private Text inputNombreNuevoEquipo = null;

    [SerializeField] private MensajeError mensajeError = null;
    [SerializeField] private InputField inputNombreEquipo = null;
    [SerializeField] private Text nombreDeporteElegido = null;

    private Deportes.DeporteEnum deporteActual;

    private PanelMisEquipos panelMisEquipos;

    private void Start()
    {
        panelMisEquipos = GetComponentInParent<PanelMisEquipos>();
        deporteActual = Deportes.DeporteEnum.Basket;
        nombreDeporteElegido.text = Deportes.instance.GetDisplayName(deporteActual, AppController.instance.idioma);
    }

    public void SetPanel()
    {
        CanvasController.instance.overlayPanel.SetNombrePanel("NUEVO EQUIPO", AppController.Idiomas.Español);
        CanvasController.instance.overlayPanel.SetNombrePanel("NEW TEAM", AppController.Idiomas.Ingles);
        mensajeError.Desactivar();
    }


    public void GuardarNuevoEquipo()                                                        //Función llamada al apretar el botón GUARDAR. Se agrega un EQUIPO a la lista de EQUIPOS. Se vuelve a la sección principal. Se crea el botón en la sección de equipos.
    {
        string nombreNuevoEquipo = inputNombreNuevoEquipo.text.ToUpper();

        if(AppController.instance.BuscarEquipoPorNombre(nombreNuevoEquipo) != null)
        {
            mensajeError.SetText("EQUIPO EXISTENTE!", AppController.Idiomas.Español);
            mensajeError.SetText("EXISTING TEAM!", AppController.Idiomas.Ingles);
            mensajeError.Activar();
            return;
        }
        if (nombreNuevoEquipo == "")
        { 
            mensajeError.SetText("NAME NEEDED!", AppController.Idiomas.Ingles);
            mensajeError.SetText("NOMBRE NECESARIO!", AppController.Idiomas.Español);
            mensajeError.Activar();
            return;
        }
        if(!AppController.instance.VerificarNombre(nombreNuevoEquipo))
        {
            mensajeError.SetText("NOMBRE INVALIDO!", AppController.Idiomas.Español);
            mensajeError.SetText("INVALID NAME!", AppController.Idiomas.Español);
            mensajeError.Activar();
            return;
        }

        AppController.instance.AgregarEquipo(new Equipo(nombreNuevoEquipo, deporteActual));

        inputNombreEquipo.text = string.Empty;

        panelMisEquipos.MostrarPanelPrincipal();
    }

    public void CambiarDeporteElegido(Deportes.DeporteEnum _deporte)
    {
        deporteActual = _deporte;
        nombreDeporteElegido.text = Deportes.instance.GetDisplayName(_deporte, AppController.instance.idioma);
    }

    public Deportes.DeporteEnum GetDeporteActual()
    {
        return deporteActual;
    }
}
