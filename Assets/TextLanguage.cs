using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextLanguage : MonoBehaviour
{
    private Text _text;
    public string textEspañol = string.Empty;
    public string textIngles = string.Empty;

    private AppController.Idiomas idiomaActual;

    private void Awake()
    {
        _text = GetComponent<Text>();
    }

    private void Start()
    {
        CambiarIdioma();
    }

    private void FixedUpdate()
    {
        if (idiomaActual != AppController.instance.idioma)// || (textEspañol==string.Empty || textIngles == string.Empty))
            CambiarIdioma();
    }

    public void CambiarIdioma()
    {
        idiomaActual = AppController.instance.idioma;
        if (idiomaActual == AppController.Idiomas.Español)
        {
            _text.text = textEspañol;
        }
        else if (idiomaActual == AppController.Idiomas.Ingles)
        {
            _text.text = textIngles;
        }
    }

    public void SetText(string text, AppController.Idiomas idioma)
    {
        if (idioma == AppController.Idiomas.Español)
        {
            textEspañol = text.ToUpper();
            //_text.text = textEspañol;
        }
        else if (idioma == AppController.Idiomas.Ingles)
        {
            textIngles = text.ToUpper();
            //_text.text = textIngles;
        }

        CambiarIdioma();
    }

    public string GetTextActual()
    {
        return _text.text;
    }

    public string GetTextInLanguage(AppController.Idiomas idioma)
    {
        string textInLanguage = string.Empty;

        if (idioma == AppController.Idiomas.Español)
            textInLanguage = textEspañol;
        else if (idioma == AppController.Idiomas.Ingles)
            textInLanguage = textIngles;

        return textInLanguage.ToUpper();
    }
}
