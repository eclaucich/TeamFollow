using UnityEngine;
using UnityEngine.UI;

public class InputPrefab : MonoBehaviour
{
    [SerializeField] protected Text nombreCategoria = null;
    [SerializeField] protected Text valorCategoria = null;
    [SerializeField] private InputField inputField = null;
    [SerializeField] private Text textCampoObligatorio = null;

    private string nombre;

    public void SetNombreCategoria(string _nombre)
    {
        nombre = _nombre;
        nombreCategoria.text = _nombre;
    }

    public void SetCampoObligatorio(bool aux_)
    {
        textCampoObligatorio.gameObject.SetActive(aux_);
    }

    public void ResetValor()
    {
        valorCategoria.text = "";
        if(inputField!=null) inputField.text = "";
    }

    public string GetNombreCategoria()
    {
        return nombreCategoria.text;
    }

    public string GetValorCategoria()
    {
        return valorCategoria.text;
    }
}
