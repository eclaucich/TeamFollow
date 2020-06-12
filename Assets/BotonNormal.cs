using UnityEngine;
using UnityEngine.UI;

public class BotonNormal : MonoBehaviour
{
    private Button button;

    private void Awake()
    {
        Activar();
    }

    /*
    private void Update()
    {
        button.image.color = AppController.instance.colorBotonNormal;
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
        button.image.color = AppController.instance.colorBotonNormal;
    }

    public void SetColorDesactivado()
    {
        if (button == null)
            button = GetComponent<Button>();
        button.image.color = new Color(AppController.instance.colorBotonNormal.r, AppController.instance.colorBotonNormal.g, AppController.instance.colorBotonNormal.b, 0.55f);
    }
}
