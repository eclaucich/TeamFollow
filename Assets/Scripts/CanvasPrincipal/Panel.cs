using UnityEngine;
using UnityEngine.UI;

public class Panel : MonoBehaviour
{
    public virtual void Start()
    {
        GetComponent<RawImage>().texture = AppController.instance.GetTextureActual();
    }

    public void UpdateTexture()
    {
        GetComponent<RawImage>().texture = AppController.instance.GetTextureActual();
    }
}
