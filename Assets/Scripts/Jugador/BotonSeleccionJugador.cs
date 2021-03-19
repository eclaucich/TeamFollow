using UnityEngine;
using UnityEngine.UI;

public class BotonSeleccionJugador : MonoBehaviour
{
    private bool seleccionado = false;

    private Image imagen;

    private void Start()
    {
        imagen = GetComponent<Image>();
        imagen.color = new Color(1f, 1f, 1f, 0f); //transparente
    }

    public void ToggleSeleccionado()
    {
        seleccionado = !seleccionado;

        SetColor();
    }

    public bool isSeleccionado()
    {
        return seleccionado;
    }

    private void SetColor()
    {
        if (seleccionado)
            imagen.color = AppController.instance.colorTheme.botonSeleccionado;
        else
            imagen.color = new Color(1f, 1f, 1f, 0f);
    }

    public void Seleccionar(bool active)
    {
        seleccionado = active;
        SetColor();
    }
}
