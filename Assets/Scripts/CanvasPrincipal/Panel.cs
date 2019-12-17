using UnityEngine;
using UnityEngine.UI;

public class Panel : MonoBehaviour
{
    public virtual void Start()
    {
        //GetComponent<RawImage>().texture = AppController.instance.GetComponent<Test>().myGradient.GetTexture(1280);
        GetComponent<RawImage>().texture = AppController.instance.texturaPaneles;
    }
}
