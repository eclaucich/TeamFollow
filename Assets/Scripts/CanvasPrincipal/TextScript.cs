using UnityEngine;
using UnityEngine.UI;

public class TextScript : MonoBehaviour {

    [SerializeField] private TextLanguage nombreEstadistica = null;
    [SerializeField] private TextLanguage inicialEstadistica = null;

    private Toggle tgl;

    private void Start()
    {
        tgl = GetComponent<Toggle>();
        tgl.isOn = false;
        ToggleColor();
    }

    public void SetName(string name_, string inicial_, AppController.Idiomas idioma = AppController.Idiomas.Español)
    {
        /*GetComponentInChildren<Text>().text = name_;
        gameObject.name = name_;
        inicialText.text = inicial_;*/

        nombreEstadistica.SetText(name_, idioma);
        inicialEstadistica.SetText(inicial_, idioma);
    }

    public void ToggleColor()
    {
        if (tgl.isOn)
            tgl.image.color = AppController.instance.colorTheme.botonSeleccionado;
        else
            tgl.image.color = AppController.instance.colorTheme.botonActivado;
    }

    public bool IsOn()
    {
        return tgl.isOn;
    }

    public string GetNombre()
    {
        return nombreEstadistica.GetTextInLanguage(AppController.Idiomas.Español);
    }

    public string GetInicial()
    {
        return inicialEstadistica.GetTextActual();
    }
}
