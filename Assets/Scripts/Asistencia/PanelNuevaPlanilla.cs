using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelNuevaPlanilla : PanelAsistencia 
{

    [SerializeField] private Text aliasPlanilla = null;
    [SerializeField] private Button botonGuardar = null;
    [SerializeField] private MensajeError mensajeError = null;

    [SerializeField] private InputField inputAlias = null;

    private Equipo equipo;
    private List<DetalleAsistencia> listaDetalles;

    public void SetPanelNuevaPlanilla()
    {
        equipo = AppController.instance.equipoActual;

        listaDetalles = CrearPrefabsDetalles(equipo.GetJugadores());

        base.SetPanelAsistencia();

        inputAlias.text = "";
    }

    public void GuardarPlanilla()
    {
        if (listaDetalles.Count == 0)
            Debug.Log("NO DETALLES");

        System.DateTime timeNow = System.DateTime.Now;

        string nombrePlanilla = timeNow.ToString("yyyy-MM-dd-HH-mm-ss");
        string alias = aliasPlanilla.text.ToUpper();

        if (equipo.ExistePlanilla(nombrePlanilla, alias))
        {
            mensajeError.SetText(("Nombre Existente!").ToUpper(), AppController.Idiomas.Español);
            mensajeError.SetText(("Existing Name!").ToUpper(), AppController.Idiomas.Ingles);
            mensajeError.Activar();
            return;
        }

        if(alias!=string.Empty && !AppController.instance.VerificarNombre(alias))
        {
            mensajeError.SetText("NOMBRE INVÁLIDO!", AppController.Idiomas.Español);
            mensajeError.SetText("INVALID NAME!", AppController.Idiomas.Ingles);
            mensajeError.Activar();
            return;
        }

        equipo.NuevaPlanilla(nombrePlanilla, alias, listaDetalles);

        GetComponentInParent<PanelPlanillaAsistencia>().MostrarPanelHistorialPlanillas();
    }
}
