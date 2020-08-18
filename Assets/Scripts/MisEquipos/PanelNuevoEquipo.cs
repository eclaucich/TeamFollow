using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// Tiene un campo para poner el nombre de el nuevo equipo (por ahora solo eso es necesario)
/// Tiene un botón para guardar el equipo configurado.
///  
/// </summary>

public class PanelNuevoEquipo : Panel {

    [SerializeField] private Text inputNombreNuevoEquipo = null;                                   //Nombre del nuevo equipo ingresado por el usuario

    [SerializeField] private Text inputNombreDeporte = null;
    [SerializeField] private MensajeError mensajeError = null;
    [SerializeField] private InputField inputNombreEquipo = null;
    [SerializeField] private Text nombreDeporteElegido = null;

    [SerializeField] private GameObject botonElegirDeportePrefab = null;
    [SerializeField] private Transform botonesParentTransform = null;
    private List<GameObject> listaPrefabs = null;
    private Deportes.DeporteEnum deporteActual;

    private GameObject botonDeporteActual = null;
    private Color notSelectedColor;
    private Color selectedColor;

    private PanelMisEquipos panelMisEquipos;                                                //Componente padre para poder acceder a las funciones

    private void Start()
    {
        panelMisEquipos = GetComponentInParent<PanelMisEquipos>();
        notSelectedColor = AppController.instance.colorTheme.botonActivado;
        selectedColor = new Color(notSelectedColor.r, notSelectedColor.g, notSelectedColor.b, 160f / 255f);
        listaPrefabs = new List<GameObject>();
        deporteActual = Deportes.DeporteEnum.Basket;
        nombreDeporteElegido.text = Deportes.instance.GetDisplayName(deporteActual, AppController.instance.idioma);
    }
    public void SetPanel()
    {
        //mensajeError.Desactivar();
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
        if(nombreNuevoEquipo == " " || nombreNuevoEquipo == "  " || nombreNuevoEquipo == "   ")
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
        /*if(botonDeporteActual!=null) botonDeporteActual.GetComponent<Image>().color = notSelectedColor;
        botonDeporteActual = botonDeporte;
        botonDeporteActual.GetComponent<Image>().color = selectedColor;*/
    }

    public Deportes.DeporteEnum GetDeporteActual()
    {
        return deporteActual;
    }
}
