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
    [SerializeField] private Text mensajeError = null;
    [SerializeField] private InputField inputNombreEquipo = null;
    
    private PanelMisEquipos panelMisEquipos;                                                //Componente padre para poder acceder a las funciones

    public override void Start()
    {
        base.Start();
        panelMisEquipos = GetComponentInParent<PanelMisEquipos>();
        mensajeError.gameObject.SetActive(false);
    }



    public void SetPanel()
    {
        mensajeError.gameObject.SetActive(false);
    }



    public void GuardarNuevoEquipo()                                                        //Función llamada al apretar el botón GUARDAR. Se agrega un EQUIPO a la lista de EQUIPOS. Se vuelve a la sección principal. Se crea el botón en la sección de equipos.
    {
        if(AppController.instance.BuscarPorNombre(inputNombreNuevoEquipo.text) != -1)
        {
            mensajeError.gameObject.SetActive(true);
            mensajeError.text = "Equipo Existente!";
            return;
        }
        if (inputNombreNuevoEquipo.text == "")
        {
            mensajeError.gameObject.SetActive(true);
            mensajeError.text = "Nombre Necesario!";
            return;
        }
        if(inputNombreNuevoEquipo.text == " " || inputNombreNuevoEquipo.text == "  " || inputNombreNuevoEquipo.text == "   ")
        {
            mensajeError.gameObject.SetActive(true);
            mensajeError.text = "Nombre Inválido!";
            return;
        }

        AppController.instance.AgregarEquipo(new Equipo(inputNombreNuevoEquipo.text, inputNombreDeporte.text));

        inputNombreEquipo.text = "";

        panelMisEquipos.MostrarPanelPrincipal();
    }
}
