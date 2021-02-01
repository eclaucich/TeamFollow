using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PanelNuevoEquipov2 : Panel
{
    [SerializeField] private Text inputNombreNuevoEquipo = null;
    [SerializeField] private MensajeError mensajeError = null;
    [SerializeField] private InputField inputNombreEquipo = null;

    [SerializeField] private Dropdown dropdownDeportes = null;
    [SerializeField] private Image imagenDeporteActual = null;

    private Deportes.DeporteEnum deporteActual;
    private PanelMisEquipos panelMisEquipos;

    private void Start()
    {
        panelMisEquipos = GetComponentInParent<PanelMisEquipos>();
        deporteActual = Deportes.DeporteEnum.Basket;
        imagenDeporteActual.sprite = Deportes.instance.GetIconoDeporte(deporteActual);
    }

    public void SetPanel()
    {
        CanvasController.instance.overlayPanel.SetNombrePanel("NUEVO EQUIPO", AppController.Idiomas.Español);
        CanvasController.instance.overlayPanel.SetNombrePanel("NEW TEAM", AppController.Idiomas.Ingles);
        mensajeError.Desactivar();

        SetDropdownDeporteOptions();
    }


    private void SetDropdownDeporteOptions()
    {
        dropdownDeportes.ClearOptions();
        List<Dropdown.OptionData> _options = new List<Dropdown.OptionData>();

        for (int i = 0; i < Deportes.instance.GetCantDeportes()-1; i++)
        {
             Dropdown.OptionData _od = new Dropdown.OptionData(Deportes.instance.GetDisplayName((Deportes.DeporteEnum)i, AppController.instance.idioma));

            _options.Add(_od);
        }

        dropdownDeportes.AddOptions(_options);

        dropdownDeportes.value = (int)deporteActual;
    }

    public void GuardarNuevoEquipo()
    {
        string nombreNuevoEquipo = inputNombreNuevoEquipo.text.ToUpper();

        if(AppController.instance.BuscarEquipoPorNombre(nombreNuevoEquipo) != null)
        {
            mensajeError.SetText("EQUIPO EXISTENTE", AppController.Idiomas.Español);
            mensajeError.SetText("EXISTING TEAM", AppController.Idiomas.Ingles);
            mensajeError.Activar();
            return;
        }
        if (nombreNuevoEquipo == "")
        { 
            mensajeError.SetText("TEAM'S NAME NEEDED", AppController.Idiomas.Ingles);
            mensajeError.SetText("NOMBRE DE EQUIPO NECESARIO", AppController.Idiomas.Español);
            mensajeError.Activar();
            return;
        }
        if(!AppController.instance.VerificarNombre(nombreNuevoEquipo))
        {
            mensajeError.SetText("NOMBRE INVALIDO", AppController.Idiomas.Español);
            mensajeError.SetText("INVALID NAME", AppController.Idiomas.Español);
            mensajeError.Activar();
            return;
        }

        AppController.instance.AgregarEquipo(new Equipo(nombreNuevoEquipo, deporteActual));

        inputNombreEquipo.text = string.Empty;

        panelMisEquipos.MostrarPanelPrincipal();
    }

    public void CambiarDeporteElegido()
    {
        deporteActual = (Deportes.DeporteEnum)dropdownDeportes.value;
        imagenDeporteActual.sprite = Deportes.instance.GetIconoDeporte(deporteActual);
    }
}
