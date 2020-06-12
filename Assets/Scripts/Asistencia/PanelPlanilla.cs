using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelPlanilla : PanelAsistencia {

    [SerializeField] private Text nombrePlanillaText = null;
    [SerializeField] private PanelHistorialPlanillas panelHistorialPlanillas = null;
    [SerializeField] private ConfirmacionBorradoAsistencia confirmacionBorradoAsistencia = null;

    [SerializeField] private GameObject botonBorrar = null;
    [SerializeField] private GameObject botonEditar = null;
    [SerializeField] private GameObject botonGuardar = null;

    [SerializeField] private InputField nuevoNombreInput = null;
    [SerializeField] private MensajeError mensajeError = null;

    private BotonHistorialAsistencia botonFocus;
    private string nombrePlanilla;

    private List<DetalleAsistencia> newDetalles;

    public void SetPanelPlanilla(BotonHistorialAsistencia botonFocus_)
    {
        newDetalles = new List<DetalleAsistencia>();

        botonFocus = botonFocus_;

        detalles = AppController.instance.GetEquipoActual().GetPlanillaWithName(botonFocus.GetNombre()).GetDetalles();
        cantidadHojas = Mathf.CeilToInt(detalles.Count / 13f);

        base.SetPanelPlanilla();

        AppController.instance.overlayPanel.SetNombrePanel(botonFocus_.GetDisplayNombre());

        CrearPrefabsHoja(false);

        if (botonFocus_.GetAlias() != "") nombrePlanillaText.text = botonFocus_.GetFecha();
        else nombrePlanillaText.text = "";

        nombrePlanilla = botonFocus.GetNombre();
        nombrePlanillaText.gameObject.SetActive(false);

        mensajeError.Desactivar();
        botonBorrar.SetActive(true);
        botonEditar.SetActive(true);
        botonGuardar.SetActive(false);
        nuevoNombreInput.gameObject.SetActive(false);
    }

    public void BorrarAsistencia()
    {
        confirmacionBorradoAsistencia.Activar(botonFocus);
    }

    public void EditarPlanilla()
    {
        botonEditar.SetActive(false);
        botonBorrar.SetActive(false);
        botonGuardar.SetActive(true);
        nuevoNombreInput.gameObject.SetActive(true);

        base.BorrarPrefabs();
        newDetalles.Clear();
        newDetalles = CrearPrefabsHoja(AppController.instance.equipoActual.GetJugadores(), detalles);
    }

    public void GuardarPlanillaEditada()
    {
        /*
         * si no se editó el nombre, se deja el mismo que ya tenía
         * si se editó, antes de guardar hay que revisar si ese nombre no está en uso
         * si está en uso -> error, sino -> guardar
         * sea el caso, cuadno se guarda, se borra la existente y se crea una nueva.
         * 
         */
        Equipo equipoActual = AppController.instance.equipoActual;

        string nuevoNombre = nuevoNombreInput.text;
        Debug.Log("nobmre actual: " + nombrePlanilla);
        Debug.Log("nuevo nombre: " + nuevoNombre);

        /*if (nuevoNombre == "")
            nuevoNombre = nombrePlanilla;*/
        /*if(nombrePlanilla != nuevoNombre)
        {
            if (equipoActual.ExistePlanilla(nombrePlanilla, ""))
            {
                mensajeError.SetText("Planilla Existente!");
                mensajeError.Activar();
                return;
            }
        }*/

        botonFocus.SetBotonHistorialAsistencia(nombrePlanilla, nuevoNombre);
        CanvasController.instance.MostrarPanelAnterior();
        CanvasController.instance.MostrarPanelAnterior();
        equipoActual.BorrarAsistencia(nombrePlanilla);
        equipoActual.NuevaPlanilla(nombrePlanilla, nuevoNombre, newDetalles);

    }
}
