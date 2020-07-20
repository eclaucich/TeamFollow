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

    private GameObject botonDeporteActual = null;
    private Color notSelectedColor;
    private Color selectedColor;

    private PanelMisEquipos panelMisEquipos;                                                //Componente padre para poder acceder a las funciones

    private void Start()
    {
        panelMisEquipos = GetComponentInParent<PanelMisEquipos>();
        notSelectedColor = AppController.instance.colorTheme.botonActivado;
        selectedColor = new Color(notSelectedColor.r, notSelectedColor.g, notSelectedColor.b, 160f / 255f);
    }

    public void SetPanel()
    {
        mensajeError.Desactivar();
    }


    public void GuardarNuevoEquipo()                                                        //Función llamada al apretar el botón GUARDAR. Se agrega un EQUIPO a la lista de EQUIPOS. Se vuelve a la sección principal. Se crea el botón en la sección de equipos.
    {
        if(AppController.instance.BuscarPorNombre(inputNombreNuevoEquipo.text) != -1)
        {
            mensajeError.SetText("Equipo Existente!");
            mensajeError.Activar();
            return;
        }
        if (inputNombreNuevoEquipo.text == "")
        { 
            mensajeError.SetText("Nombre Necesario!");
            mensajeError.Activar();
            return;
        }
        if(inputNombreNuevoEquipo.text == " " || inputNombreNuevoEquipo.text == "  " || inputNombreNuevoEquipo.text == "   ")
        {
            mensajeError.SetText("Nombre Inválido!");
            mensajeError.Activar();
            return;
        }

        AppController.instance.AgregarEquipo(new Equipo(inputNombreNuevoEquipo.text, nombreDeporteElegido.text));

        inputNombreEquipo.text = "";

        panelMisEquipos.MostrarPanelPrincipal();
    }

    public void CambiarDeporteElegido(GameObject botonDeporte)
    {
        nombreDeporteElegido.text = botonDeporte.name;
        if(botonDeporteActual!=null) botonDeporteActual.GetComponent<Image>().color = notSelectedColor;
        botonDeporteActual = botonDeporte;
        botonDeporteActual.GetComponent<Image>().color = selectedColor;
    }


}
