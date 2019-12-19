using UnityEngine;
using UnityEngine.UI;

public class InfoPrefab : MonoBehaviour
{
    [SerializeField] private Text nombreCategoria = null;
    [SerializeField] private Text valorCategoria = null;

    public void SetNombreCategoria(string nombre)
    {
        nombreCategoria.text = nombre;
    }

    public void SetValorCategoria(string valor)
    {
        valorCategoria.text = valor;
    }
}
