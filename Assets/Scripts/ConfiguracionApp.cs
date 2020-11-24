using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfiguracionApp : MonoBehaviour
{
    public static ConfiguracionApp instance = null;

    public AppController.Idiomas idioma;
    public ColorTheme tema;
    public Deportes.DeporteEnum deporteFavorito;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this); 
    }

    public void SaveSettings()
    {
        
    }

}
