using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PanelNuevoEquipov3 : MensajeDesplegable
{
    [Space]
    [SerializeField] private Text nombreEquipoText = null;
    [SerializeField] private MensajeError mensajeError = null;
    [SerializeField] private InputField inputNombreEquipo = null;


    [Space]
    [SerializeField] private Text deporteActualText = null;
    [SerializeField] private Transform deportesParent = null;
    [SerializeField] private Button botonDeportePrefab = null;


    private Deportes.DeporteEnum deporteActual;
    private PanelMisEquipos panelMisEquipos;
    private List<Button> listaBotones;

    override public void Start() 
    {
        base.Start();
        panelMisEquipos = GetComponentInParent<PanelMisEquipos>();
        listaBotones = new List<Button>();
        SetBotonesDeportes();
        CambiarDeporteElegido(Deportes.DeporteEnum.Basket);
        ActivarBoton(listaBotones[0]);
    } 

    private void Update() 
    {
        if(desplegado)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                Cerrar();
            }
        }
    }

    private void SetBotonesDeportes()
    {
        for (int i = 0; i < Deportes.instance.GetCantDeportes()-1; i++)
        {
            Deportes.DeporteEnum dep = (Deportes.DeporteEnum)i;
            Button depGO = Instantiate(botonDeportePrefab, deportesParent, false);
            depGO.gameObject.SetActive(true);
            depGO.image.sprite = Deportes.instance.GetIconoDeporte((Deportes.DeporteEnum)dep);
            depGO.onClick.AddListener(delegate {CambiarDeporteElegido((Deportes.DeporteEnum)dep); });
            depGO.onClick.AddListener(delegate {ActivarBoton(depGO); });
            listaBotones.Add(depGO);
        }
    }

    public override void ToggleDesplegar()
    {
        base.ToggleDesplegar();
        if(desplegado)
        {
            CanvasController.instance.retrocesoPausado = true;
        }
        else
        {
            CanvasController.instance.retrocesoPausado = false;
        }
    }

    public override void Cerrar()
    {
        base.Cerrar();
        CanvasController.instance.retrocesoPausado = false;
    }

    public void GuardarNuevoEquipo()
    {
        string nombreNuevoEquipo = nombreEquipoText.text.ToUpper();

        if (AppController.instance.BuscarEquipoPorNombre(nombreNuevoEquipo) != null)
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
        if (!AppController.instance.VerificarNombre(nombreNuevoEquipo))
        {
            mensajeError.SetText("NOMBRE INVALIDO", AppController.Idiomas.Español);
            mensajeError.SetText("INVALID NAME", AppController.Idiomas.Español);
            mensajeError.Activar();
            return;
        }

        AppController.instance.AgregarEquipo(new Equipo(nombreNuevoEquipo, deporteActual));

        inputNombreEquipo.text = string.Empty;

        panelMisEquipos.MostrarPanelPrincipal();

        Cerrar();
    }

    public void CambiarDeporteElegido(Deportes.DeporteEnum _deporte)
    {
        Debug.Log("DEPORTE: " + _deporte);
        deporteActual = _deporte;
        deporteActualText.text = Deportes.instance.GetDisplayName(_deporte, AppController.instance.idioma).ToUpper();
    }

    public void ActivarBoton(Button button)
    {
        DeseleccionarBotones();
        button.image.color = AppController.instance.colorTheme.detalle1;
    }

    private void DeseleccionarBotones()
    {
        foreach (var bot in listaBotones)
        {
            bot.image.color = AppController.instance.colorTheme.icon;
        }
    }
}