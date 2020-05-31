using UnityEngine;
using UnityEngine.UI;

public class BotonNormal : MonoBehaviour
{
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        //button.image = AppController.instance.imagenBotonNormal.texture.;
        Activar();
    }

    /*
    private void Update()
    {
        button.image.color = AppController.instance.colorBotonNormal;
    }*/

    public void Activar()
    { 
        button.enabled = true;
        SetColorActivado();
    }

    public void Desactivar()
    { 
        button.enabled = false;
        SetColorDesactivado();
    }

    public void SetColorActivado()
    {
        button.image.color = AppController.instance.colorBotonNormal;
    }

    public void SetColorDesactivado()
    {
        button.image.color = new Color(AppController.instance.colorBotonNormal.r, AppController.instance.colorBotonNormal.g, AppController.instance.colorBotonNormal.b, 0.55f);
    }
}
