using UnityEngine;
using UnityEngine.UI;

public class BotonHistorialAsistencia : MonoBehaviour
{

    [SerializeField] private PanelPlanillaAsistencia panelPlanillaAsistencia = null;
    [SerializeField] private Text nombrePlanillaText = null;

    [SerializeField] private Image imagenFondo = null;

    [Space]
    [Header("Seleccion Multiple")]
    [SerializeField] private Toggle toggleSeleccionMultiple = null;

    private string nombrePlanilla;
    private string aliasPlanilla;

    private bool seleccionMultipleActivada;

    private void Start() 
    {
        seleccionMultipleActivada = false;
        toggleSeleccionMultiple.isOn = false;
        toggleSeleccionMultiple.gameObject.SetActive(false);    
    }

    private void Update()
    {
        if(seleccionMultipleActivada)
        {
            if(toggleSeleccionMultiple.isOn)
                imagenFondo.color = AppController.instance.colorTheme.botonSeleccionado;
            else
                imagenFondo.color = AppController.instance.colorTheme.botonActivado;
        }
    }

    public void SetBotonHistorialAsistencia(string nombrePlanilla_, string alias_)
    {
        nombrePlanilla = nombrePlanilla_;
        aliasPlanilla = alias_;

        nombrePlanillaText.text = GetDisplayNombre();
    }

    public void MostrarPlanilla()
    {
        if(!seleccionMultipleActivada)
            panelPlanillaAsistencia.MostrarPanelPlanilla(this);
        else
            toggleSeleccionMultiple.isOn = !toggleSeleccionMultiple.isOn;
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
        if(aliasPlanilla != "") 
            return aliasPlanilla + " - " + GetFecha();
        else
            return GetFecha();
    }

    #region Seleccion Multiple

    public void ToggleSeleccionMultiple(bool aux)
    {
        seleccionMultipleActivada = aux;
        toggleSeleccionMultiple.gameObject.SetActive(aux);
        if(aux==false)
            imagenFondo.color = AppController.instance.colorTheme.botonActivado;
    }

    public bool IsSelected()
    {
        return toggleSeleccionMultiple.isOn;
    }

    #endregion
}
