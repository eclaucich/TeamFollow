using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotonUI : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Image>().color = AppController.instance.colorTheme.botonActivado;
    }
}
