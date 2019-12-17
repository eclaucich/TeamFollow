using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelEntradaDatosPrincipal : MonoBehaviour
{
    private void Awake()
    {
        gameObject.GetComponent<RawImage>().texture = AppController.instance.GetComponent<Test>().myGradient.GetTexture(1280);
    }
}
