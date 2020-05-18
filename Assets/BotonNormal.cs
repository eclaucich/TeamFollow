using UnityEngine;
using UnityEngine.UI;

public class BotonNormal : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Button>().image.color = AppController.instance.colorBotonNormal;
    }
}
