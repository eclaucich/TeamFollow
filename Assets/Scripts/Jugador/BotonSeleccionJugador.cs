using UnityEngine;
using UnityEngine.UI;

public class BotonSeleccionJugador : MonoBehaviour
{
    [SerializeField] private Color colorSeleccionado = new Color();
    [SerializeField] private Color colorNoSeleccionado = new Color();

    private bool seleccionado = false;

    private void Start()
    {
        GetComponent<Image>().color = colorNoSeleccionado;
    }

    public void ToggleSeleccionado()
    {
        seleccionado = !seleccionado;

        if (seleccionado) GetComponent<Image>().color = colorSeleccionado;
        else GetComponent<Image>().color = colorNoSeleccionado;
    }

    public bool isSeleccionado()
    {
        return seleccionado;
    }
}
