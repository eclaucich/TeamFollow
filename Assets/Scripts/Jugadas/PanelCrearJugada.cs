using UnityEngine;

public class PanelCrearJugada : PanelCrearJugadas {

    public static PanelCrearJugada instance = null;

    private void Awake()
    {
        if (instance == null)                                                                //Control del singleton
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }

        colorActual = Color.black;
    }


    public void SetPanelOpcionesActual(PanelOpcionesHerramienta _panelOpciones)
    {
        panelEdicion.SetPanelOpcionesActual(_panelOpciones);
    }

    public void CerrarPanelOpcionesActual()
    {
        panelEdicion.CerrarPanelOpcionesActual();
    }
}
