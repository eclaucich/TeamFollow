using UnityEngine;
using UnityEngine.UI;

public class OverlayPanel : MonoBehaviour
{
    [SerializeField] private TextLanguage nombrePanel = null;

    public void SetNombrePanel(string _nombre, AppController.Idiomas _idioma)
    {
        nombrePanel.SetText(_nombre, _idioma);
    }
}
