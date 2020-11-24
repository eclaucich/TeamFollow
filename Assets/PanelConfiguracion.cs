using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelConfiguracion : MonoBehaviour
{
    [SerializeField] private Dropdown dropdownIdiomas = null;

    [SerializeField] private Dropdown dropdownTemas = null;
    [SerializeField] private MensajeDesplegable confirmacionReinicio = null;

    [SerializeField] private Dropdown dropdownDeportes = null;

    [SerializeField] private FlechasScroll flechasScroll = null;

    private AppController.Idiomas _idioma;
    private Deportes.DeporteEnum _deporteFavorito;
    private AppController.Temas _tema;

    #region Inicializacion
    private void Start()
    {
        confirmacionReinicio.Cerrar();
    }

    public void SetPanelConfiguracion()
    {
        CanvasController.instance.overlayPanel.SetNombrePanel("CONFIGURACION", AppController.Idiomas.Español);
        CanvasController.instance.overlayPanel.SetNombrePanel("SETTINGS", AppController.Idiomas.Ingles);

        CanvasController.instance.AgregarPanelAnterior(CanvasController.Paneles.MisEquipos);

        _idioma = AppController.instance.idioma;
        _deporteFavorito = AppController.instance.deporteFavorito;
        _tema = AppController.instance.tema;

        SetDropdownIdiomasOptions();
        SetDropdownTemasOptions();
        SetSeccionDeporteFavorito();
    }
    #endregion

    #region Seccion Deporte Favorito
    private void SetSeccionDeporteFavorito()
    {
        SetDropdownDeporteOptions();
        SetTextDeporteActual();
    }
    private void SetDropdownDeporteOptions()
    {
        dropdownDeportes.ClearOptions();
        List<Dropdown.OptionData> _options = new List<Dropdown.OptionData>();

        foreach (var _deporte in Enum.GetValues(typeof(Deportes.DeporteEnum)))
        {
            Dropdown.OptionData _od = new Dropdown.OptionData(Deportes.instance.GetDisplayName((Deportes.DeporteEnum)_deporte, AppController.instance.idioma));

            _options.Add(_od);
        }

        dropdownDeportes.AddOptions(_options);

        dropdownDeportes.value = (int)_deporteFavorito;
    }
    private void SetTextDeporteActual()
    {
        //deporteActualText.SetText(Deportes.instance.GetDisplayName(_deporteFavorito, AppController.Idiomas.Español), AppController.Idiomas.Español);
        //deporteActualText.SetText(Deportes.instance.GetDisplayName(_deporteFavorito, AppController.Idiomas.Ingles), AppController.Idiomas.Ingles);
    }

    public void CambiarDeporteFavorito()
    {
        _deporteFavorito = (Deportes.DeporteEnum)dropdownDeportes.value;
        SetTextDeporteActual();
        SaveSettings();
    }

    public void RefreshDropdownDeportes()
    {
        if (dropdownDeportes.options.Count == 0)
            return;
        int i = 0;
        foreach (var _deporte in Enum.GetValues(typeof(Deportes.DeporteEnum)))
        {
            dropdownDeportes.options[i].text = Deportes.instance.GetDisplayName((Deportes.DeporteEnum)_deporte, AppController.instance.idioma);
            i++;
        }
        dropdownDeportes.RefreshShownValue();
    }
    #endregion

    #region Seccion Temas
    private void SetDropdownTemasOptions()
    {
        dropdownTemas.ClearOptions();
        List<Dropdown.OptionData> _options = new List<Dropdown.OptionData>();

        foreach (var _t in Enum.GetValues(typeof(AppController.Temas)))
        {
            Dropdown.OptionData _od = new Dropdown.OptionData(AppController.instance.GetDisplayNameTema((AppController.Temas)_t, _idioma));
            _options.Add(_od);
        }

        dropdownTemas.AddOptions(_options);

        dropdownTemas.value = (int)_tema;
    }
    public void CambiarTema()
    {
        confirmacionReinicio.ToggleDesplegar();
        //AppController.instance.tema = _tema;
    }

    public void AceptarCambioTema()
    {
        _tema = (AppController.Temas)dropdownTemas.value;
        SaveSettings();
        Application.Quit();
    }
    public void CancelarCambioTema()
    {
        dropdownTemas.value = (int)_tema;
        confirmacionReinicio.Cerrar();
    }

    public void RefreshDropdownTemas()
    {
        if (dropdownTemas.options.Count == 0)
            return;

        int i = 0;
        foreach (var _tema in Enum.GetValues(typeof(AppController.Temas)))
        {
            dropdownTemas.options[i].text = AppController.instance.GetDisplayNameTema((AppController.Temas)_tema, AppController.instance.idioma);
            i++;
        }
        dropdownTemas.RefreshShownValue();
    }

    #endregion

    #region Seccion Idiomas
    private void SetDropdownIdiomasOptions()
    {
        dropdownIdiomas.ClearOptions();
        List<Dropdown.OptionData> _options = new List<Dropdown.OptionData>();

        foreach (var _i in Enum.GetValues(typeof(AppController.Idiomas)))
        {
            Dropdown.OptionData _od = new Dropdown.OptionData(AppController.instance.GetDisplayNameIdioma((AppController.Idiomas)_i, _idioma));
            _options.Add(_od);
        }

        dropdownIdiomas.AddOptions(_options);

        dropdownIdiomas.value = (int)_idioma;
    }
    public void CambiarIdioma()
    {
        _idioma = (AppController.Idiomas)dropdownIdiomas.value;
        AppController.instance.idioma = _idioma;

        RefreshDropdownDeportes();
        RefreshDropdownTemas();
        RefreshDropdownIdiomas();

        SaveSettings();
    }

    public void RefreshDropdownIdiomas()
    {
        if (dropdownIdiomas.options.Count == 0)
            return;

        int i = 0;
        foreach (var _idioma in Enum.GetValues(typeof(AppController.Idiomas)))
        {
            dropdownIdiomas.options[i].text = AppController.instance.GetDisplayNameIdioma((AppController.Idiomas)_idioma, AppController.instance.idioma);
            i++;
        }
        dropdownIdiomas.RefreshShownValue();
    }
    #endregion

    #region Seccion Configuracion Deportes

    private void SetDropdownsConfiguracionDeportes()
    {

        /*
            Por cada deporte hacer un dropdown que tenga como opciones las estadisticas de ese deporte

        */
        foreach (var _deporte in Enum.GetValues(typeof(Deportes.DeporteEnum)))
        {
            //SetDropdownDeporte((Deportes.DeporteEnum)_deporte);
        }
    }

    #endregion

    public void SaveSettings()
    {
        SaveSystem.SaveSettings(_idioma, _tema, _deporteFavorito);

        SaveDataSettings _settings = new SaveDataSettings(_idioma, _tema, _deporteFavorito);
        AppController.instance.SetSettings(_settings);
    }
}
