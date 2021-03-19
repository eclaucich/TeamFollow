using UnityEngine;
using UnityEngine.UI;

public class BotonPartido : MonoBehaviour
{
    [SerializeField] private PanelPartidosEquipo panelPartidosEquipo = null;
    [SerializeField] private PanelJugadores panelJugadores = null;
    [SerializeField] private Text nombrePartidoText = null;
    [SerializeField] private Image imagenFondo = null;

    [Space]
    [Header("Seleccion Multiple")]
    [SerializeField] private Toggle toggleSeleccionMultiple = null;
    private bool seleccionMultipleActivada;

    private Partido partidoFocus;

    private void Start()
    {
        toggleSeleccionMultiple.isOn = false;
        SetSeleccionMultiple(false);
    }

    public void SetPartidoFocus(Partido partido_)
    {
        partidoFocus = partido_;
        nombrePartidoText.text = partido_.GetNombre();
    }

    private void Update()
    {
        if(seleccionMultipleActivada)
        {
            if(toggleSeleccionMultiple.isOn)
                imagenFondo.color = AppController.instance.colorTheme.botonSeleccionado;
            else
                imagenFondo.color = AppController.instance.colorTheme.detalle4;
        }
    }

    public void VerDetallePartido()
    {
        if(!seleccionMultipleActivada)
            panelPartidosEquipo.MostrarPanelDetallePartido(partidoFocus);
        else
            toggleSeleccionMultiple.isOn = !toggleSeleccionMultiple.isOn;
    }

    public void VerDetallePartidoJugador()
    {
        if(!seleccionMultipleActivada)
            panelJugadores.SetPartidoFocus(this);
        else
            toggleSeleccionMultiple.isOn = !toggleSeleccionMultiple.isOn;
    }

    public Partido GetPartido()
    {
        return partidoFocus;
    }

    #region Seleccion Multiple
    public void SetSeleccionMultiple(bool aux)
    {
        seleccionMultipleActivada = aux;
        toggleSeleccionMultiple.gameObject.SetActive(aux);
        if(aux==false)
            imagenFondo.color = AppController.instance.colorTheme.detalle4;
    }

    public bool IsSelected()
    {
        return toggleSeleccionMultiple.isOn;
    }

    #endregion
}
