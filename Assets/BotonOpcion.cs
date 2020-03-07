using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotonOpcion : MonoBehaviour
{
    [SerializeField] private Text categoria = null;
    [SerializeField] private Text valor = null;

    public string GetCategoria()
    {
        return categoria.text;
    }

    public string GetValor()
    {
        return valor.text;
    }
}
