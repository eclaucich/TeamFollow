using UnityEngine;
using UnityEngine.UI;

public class BotonNormal : MonoBehaviour
{
    private Button button;

    /*private void Start()
    {
        Activar();
    }*/

    public void Activar()
    {
        if (button == null)
            button = GetComponent<Button>();
        button.enabled = true;
        SetColorActivado();
    }

    public void Desactivar()
    {
        if (button == null)
            button = GetComponent<Button>();
        button.enabled = false;
        SetColorDesactivado();
    }

    public void SetColorActivado()
    {
        if (button == null)
            button = GetComponent<Button>();
        if (AppController.instance == null) Debug.LogError("APPCONTROLER NULL");
        if (AppController.instance.colorTheme == null) Debug.LogError("THEME NULL");
        button.image.color = AppController.instance.colorTheme.botonActivado;
    }

    public void SetColorDesactivado()
    {
        if (button == null)
            button = GetComponent<Button>();
        button.image.color = AppController.instance.colorTheme.botonDesactivado;
    }

    public void SetColorSeleccionado()
    {
        if (button == null)
            button = GetComponent<Button>();
        button.image.color = AppController.instance.colorTheme.botonSeleccionado;
    }
}
