using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelPlanilla : PanelAsistencia {

    [SerializeField] private PanelHistorialPlanillas panelHistorialPlanillas = null;
    [SerializeField] private ConfirmacionBorradoAsistencia confirmacionBorradoAsistencia = null;

    [SerializeField] private GameObject botonBorrar = null;
    [SerializeField] private GameObject botonEditar = null;
    [SerializeField] private GameObject botonGuardar = null;
    [SerializeField] private InputField inputNuevoAlias = null;

    [SerializeField] private TextLanguage fechaText = null;
    [SerializeField] private TextLanguage aliasText = null;

    [SerializeField] private MensajeError mensajeError = null;

    private BotonHistorialAsistencia botonFocus;
    private List<DetalleAsistencia> detalles;
    private List<DetalleAsistencia> newDetalles;

    public void SetPanelPlanilla(BotonHistorialAsistencia botonFocus_)
    {
        newDetalles = new List<DetalleAsistencia>();

        botonFocus = botonFocus_;

        detalles = AppController.instance.equipoActual.GetPlanillaWithName(botonFocus.GetNombre()).GetDetalles();

        CanvasController.instance.overlayPanel.SetNombrePanel("PLANILLA DE ASISTENCIA", AppController.Idiomas.Español);
        CanvasController.instance.overlayPanel.SetNombrePanel("ASSISTANCE FORM", AppController.Idiomas.Ingles);

        CrearPrefabs(detalles, false);

        //Si no tiene alias lo desactivo
        aliasText.gameObject.SetActive(true);
        if (botonFocus_.GetAlias() == "")
            aliasText.gameObject.SetActive(false);

        base.SetPanelAsistencia();

        fechaText.SetText("FECHA:\n" + botonFocus_.GetFecha(), AppController.Idiomas.Español);
        fechaText.SetText("DATE:\n" + botonFocus_.GetFecha(), AppController.Idiomas.Ingles);

        aliasText.SetText("NOMBRE:\n" + botonFocus_.GetAlias(), AppController.Idiomas.Español);
        aliasText.SetText("NAME:\n" + botonFocus_.GetAlias(), AppController.Idiomas.Ingles);

        inputNuevoAlias.text = "";

        //Estado incial de los botones
        botonBorrar.SetActive(true);
        botonEditar.SetActive(true);
        botonGuardar.SetActive(false);
        inputNuevoAlias.gameObject.SetActive(false);
    }

    public void BorrarAsistencia()
    {
        AndroidManager.HapticFeedback();
        confirmacionBorradoAsistencia.Activar(botonFocus);
    }

    public void EditarPlanilla()
    {
        botonEditar.SetActive(false);
        botonBorrar.SetActive(false);
        aliasText.gameObject.SetActive(false);
        botonGuardar.SetActive(true);
        inputNuevoAlias.gameObject.SetActive(true);
        inputNuevoAlias.text = botonFocus.GetAlias();

        newDetalles = CrearPrefabsDetalles(detalles, true);
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

        string nuevoAlias = inputNuevoAlias.text;
        string nombrePlanilla = botonFocus.GetNombre();
        string aliasPlanilla = botonFocus.GetAlias();

        Debug.Log("Nuevo alias: " + nuevoAlias);
        Debug.Log("Viejo alias: " + aliasPlanilla);

        if (nuevoAlias == "")
            nuevoAlias = aliasPlanilla;
        else if(nuevoAlias != aliasPlanilla)
        {
            if (equipoActual.ExistePlanillaConAlias(nuevoAlias))
            {
                Debug.Log("EXISTIA");
                mensajeError.SetText("Nombre existente", AppController.Idiomas.Español);
                mensajeError.SetText("Existing name", AppController.Idiomas.Ingles);
                mensajeError.Activar();
                return;
            }
            Debug.Log("NO EXISTIA");
        }

        equipoActual.BorrarAsistencia(nombrePlanilla);
        equipoActual.NuevaPlanilla(nombrePlanilla, nuevoAlias, newDetalles);
        botonFocus.SetBotonHistorialAsistencia(nombrePlanilla, nuevoAlias);
        CanvasController.instance.MostrarPanelAnterior();
    }
}
