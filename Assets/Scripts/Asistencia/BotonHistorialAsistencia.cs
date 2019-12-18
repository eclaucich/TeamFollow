using UnityEngine;
using UnityEngine.UI;

public class BotonHistorialAsistencia : MonoBehaviour {

    [SerializeField] private Text nombrePlanillaText = null;

    public void SetBotonHistorialAsistencia(string nombrePlanilla)
    {
        nombrePlanillaText.text = nombrePlanilla;
    }

    public void MostrarPlanilla()
    {
        GameObject.Find("PanelPlanillaAsistencia").GetComponent<PanelPlanillaAsistencia>().MostrarPanelPlanilla(nombrePlanillaText.text);
    }

    public void BorrarAsistencia()
    {
        GameObject.Find("PanelHistorialPlanillas").GetComponent<PanelHistorialPlanillas>().BorrarAsistencia(gameObject, nombrePlanillaText.text);
    }
}
