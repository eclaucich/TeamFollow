using UnityEngine;
using UnityEngine.UI;

public class Panel : MonoBehaviour
{
    protected string nombrePanel = "";

    public virtual void Start()
    {
        UpdateTexture();
    }

    public void UpdateTexture()
    {
        GetComponent<RawImage>().texture = AppController.instance.GetTextureActual();
    }
}
