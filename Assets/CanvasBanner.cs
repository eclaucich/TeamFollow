using UnityEngine;

public class CanvasBanner : MonoBehaviour
{

    private void Start()
    {
        GetComponent<Canvas>().worldCamera = Camera.main;
    }

}
