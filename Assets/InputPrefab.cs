using UnityEngine;
using UnityEngine.UI;

public class InputPrefab : MonoBehaviour
{
    [SerializeField] protected Text nombreCategoria = null;
    [SerializeField] protected Text valorCategoria = null;
    [SerializeField] protected InputField inputField = null;
    [SerializeField] private Text textCampoObligatorio = null;
    [SerializeField] private TextLanguage text = null;

    [SerializeField] private Image imageNombre = null;
    [SerializeField] private Image imageValor = null;

    private string nombre;

    public void SetKeyboardType(TouchScreenKeyboardType _keyboardType)
    {
        inputField.keyboardType = _keyboardType;
        Debug.Log("TIPO: " + _keyboardType.ToString());
    }

    public void SetNombreCategoria(string _nombre)
    {
        nombre = _nombre;
        //nombreCategoria.text = _nombre;
    }

    public void SetValorCategoria(string _valor)
    {
        valorCategoria.text = _valor;
    }

    public void SetPlaceholder(string _valor)
    {
        inputField.text = _valor;
    }

    public string GetPlaceholder()
    {
        return inputField.text;
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
        return nombre.ToUpper(); // return nombreCategoria.text;
    }

    public string GetValorCategoria()
    {
        return valorCategoria.text.ToUpper();
    }

    public virtual void HabilitarInput(bool _aux)
    {
        inputField.enabled = _aux;
    }

    public void SetText(string _text, AppController.Idiomas idioma)
    {
        text.SetText(_text, idioma);
    }

    public void SetColor(Color _color)
    {
        imageNombre.color = _color;
        imageValor.color = _color;
    }
}
