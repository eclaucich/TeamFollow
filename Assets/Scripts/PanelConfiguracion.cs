using System;
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

    [Space]
    [Header("NUEVO")]
    [SerializeField] private GameObject panelOpcionesPrefab = null;
    [SerializeField] private Text idiomaActualText = null;
    [SerializeField] private Text temaActualText = null;
    [SerializeField] private Text deporteFavoritoActualText = null;


    private AppController.Idiomas _idioma;
    private Deportes.DeporteEnum _deporteFavorito;
    private AppController.Temas _tema;

    private enum OpcionActual
    {
        ninguna = -1,
        idioma = 0,
        tema = 1,
        deporte = 2
    }
    private OpcionActual opcionEditandoActualmente;

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

        opcionEditandoActualmente = OpcionActual.ninguna;
        idiomaActualText.text = AppController.instance.GetDisplayNameIdioma(_idioma, _idioma);
        temaActualText.text = AppController.instance.GetDisplayNameTema(_tema, _idioma);
        deporteFavoritoActualText.text = Deportes.instance.GetDisplayName(_deporteFavorito, _idioma);

        // !!!!! DESCOMENTAR ESTO SI SE VUELVE A VERSION 1 !!!!!
        /*SetDropdownIdiomasOptions();
        SetDropdownTemasOptions();
        SetSeccionDeporteFavorito();*/
    }
    #endregion
/*
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
*/
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
        //_tema = (AppController.Temas)dropdownTemas.value;   !!!!!! DESCOMENTAR ESTO SI VUELVE A LA VERSION 1 !!!!!!!
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
/*

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
    */

    public void SaveSettings()
    {
        SaveSystem.SaveSettings(_idioma, _tema, _deporteFavorito);

        SaveDataSettings _settings = new SaveDataSettings(_idioma, _tema, _deporteFavorito);
        AppController.instance.SetSettings(_settings);
    }

    #region FUNCIONES NUEVAS
    public void ShowLanguageOptions()
    {
        opcionEditandoActualmente = OpcionActual.idioma;

        GameObject panelOpcionesGO = Instantiate(panelOpcionesPrefab, transform, false);
        OpcionesEspeciales panelOpciones = panelOpcionesGO.GetComponent<OpcionesEspeciales>();

        string nombreCategoria = AppController.instance.idioma == AppController.Idiomas.Español ? "IDIOMA" : "LANGUAGE";

        List<string> opciones = new List<string>();
        foreach (var i in Enum.GetValues(typeof(AppController.Idiomas)))
        {
            string op = AppController.instance.GetDisplayNameIdioma((AppController.Idiomas)i, AppController.instance.idioma);
            opciones.Add(op);
        }

        panelOpciones.SetMenu(opciones, nombreCategoria, AppController.instance.idioma);
    }

    public void ShowThemesOptions()
    {
        opcionEditandoActualmente = OpcionActual.tema;

        GameObject panelOpcionesGO = Instantiate(panelOpcionesPrefab, transform, false);
        OpcionesEspeciales panelOpciones = panelOpcionesGO.GetComponent<OpcionesEspeciales>();

        string nombreCategoria = AppController.instance.idioma == AppController.Idiomas.Español ? "TEMA" : "THEME";

        List<string> opciones = new List<string>();
        foreach (var i in Enum.GetValues(typeof(AppController.Temas)))
        {
            string op = AppController.instance.GetDisplayNameTema((AppController.Temas)i, AppController.instance.idioma);
            opciones.Add(op);
        }

        panelOpciones.SetMenu(opciones, nombreCategoria, AppController.instance.idioma);
    }

    public void ShowSportsOptions()
    {
        opcionEditandoActualmente = OpcionActual.deporte;

        GameObject panelOpcionesGO = Instantiate(panelOpcionesPrefab, transform, false);
        OpcionesEspeciales panelOpciones = panelOpcionesGO.GetComponent<OpcionesEspeciales>();

        string nombreCategoria = AppController.instance.idioma == AppController.Idiomas.Español ? "DEPORTE FAVORITO" : "FAVORITE SPORT";

        List<string> opciones = new List<string>();
        foreach (var i in Enum.GetValues(typeof(Deportes.DeporteEnum)))
        {
            string op = Deportes.instance.GetDisplayName((Deportes.DeporteEnum)i, AppController.instance.idioma);
            opciones.Add(op);
        }

        panelOpciones.SetMenu(opciones, nombreCategoria, AppController.instance.idioma);
    }

    public void SelectOption(string opcionString)
    {
        switch(opcionEditandoActualmente)
        {
            case OpcionActual.idioma:
                _idioma = AppController.instance.GetLanguageEnumFromString(opcionString);
                AppController.instance.idioma = _idioma;
                idiomaActualText.text = opcionString;

                idiomaActualText.text = AppController.instance.GetDisplayNameIdioma(_idioma, _idioma).ToUpper();
                temaActualText.text = AppController.instance.GetDisplayNameTema(_tema, _idioma).ToUpper();
                deporteFavoritoActualText.text = Deportes.instance.GetDisplayName(_deporteFavorito, _idioma).ToUpper();
                SaveSettings();
                break;

            case OpcionActual.tema:
                _tema = AppController.instance.GetThemeEnumFromString(opcionString);
                temaActualText.text = opcionString;
                break;

            case OpcionActual.deporte:
                Debug.Log("DEPORTE CAMBIADO: " + opcionString);
                _deporteFavorito = Deportes.instance.GetSportEnumFromString(opcionString);
                deporteFavoritoActualText.text = opcionString;
                SaveSettings();
                break;

            default:
                Debug.LogError("ESTO NO DEBERIA HABER PASADO");
                break;
        }

        opcionEditandoActualmente = OpcionActual.ninguna;
    }

    #endregion
}
