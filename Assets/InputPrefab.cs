using UnityEngine;
using UnityEngine.UI;

public class InputPrefab : MonoBehaviour
{
    [SerializeField] private Text nombreCategoria = null;
    [SerializeField] private Text valorCategoria = null;

    private string nombre;

    public void SetNombreCategoria(string _nombre)
    {
        nombre = _nombre;
        nombreCategoria.text = _nombre;
        valorCategoria.text = " - ";
    }

    public string GetValorCategoria()
    {
        return valorCategoria.text;
    }
}
