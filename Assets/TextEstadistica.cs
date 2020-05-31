using UnityEngine;
using UnityEngine.UI;

public class TextEstadistica : MonoBehaviour
{
    [SerializeField] private GameObject nombreCompletoText = null;

    private void Awake()
    {
        nombreCompletoText.SetActive(false);
    }

    public void OnPress()
    {
        nombreCompletoText.SetActive(true);
    }

    public void OnRelease()
    {
        nombreCompletoText.SetActive(false);
    }

    public void SetInicial(string _ini)
    {
        GetComponent<Text>().text = _ini;
    }

    public void SetNombreCompleto(string _nom)
    {
        nombreCompletoText.GetComponent<Text>().text = _nom;
    }
}
