using UnityEngine;
using UnityEngine.UI;

public class OverlayPanel : MonoBehaviour
{
    [SerializeField] private Text nombrePanel = null;

    public void SetNombrePanel(string nombrePanel_)
    {
        nombrePanel.text = nombrePanel_;
    }
}
