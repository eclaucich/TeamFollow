using UnityEngine;
using UnityEngine.UI;

public class BotonHistorialAsistencia : MonoBehaviour {

    [SerializeField] private PanelPlanillaAsistencia panelPlanillaAsistencia = null;
    [SerializeField] private Text nombrePlanillaText = null;
    private string nombrePlanilla;
    private string aliasPlanilla;

    public void SetBotonHistorialAsistencia(string nombrePlanilla_, string alias_)
    {
        nombrePlanilla = nombrePlanilla_;
        aliasPlanilla = alias_;

        nombrePlanillaText.text = GetDisplayNombre();
    }

    public void MostrarPlanilla()
    {
        panelPlanillaAsistencia.MostrarPanelPlanilla(this);
    }

    /*public void BorrarAsistencia()
    {
        GameObject.Find("PanelHistorialPlanillas").GetComponent<PanelHistorialPlanillas>().BorrarAsistencia(this, nombrePlanilla);
    }*/

    public string GetNombre()
    {
        return nombrePlanilla;
    }

    public string GetAlias()
    {
        return aliasPlanilla;
    }

    public string GetFecha()
    {
        string dia = nombrePlanilla.Substring(8, 2);
        string mes = nombrePlanilla.Substring(5, 2);
        string anio = nombrePlanilla.Substring(0, 4);
        string minuto = nombrePlanilla.Substring(14, 2);
        string hora = nombrePlanilla.Substring(11, 2);

        return dia + "/" + mes + "/" + anio + " " + hora + ":" + minuto;
    }

    public string GetDisplayNombre()
    {
        if(aliasPlanilla != "") return aliasPlanilla;
        
        return GetFecha();
    }
}
