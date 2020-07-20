using UnityEngine;
using UnityEngine.UI;

public class Detalle4UI : UIComponent
{
    public override void Start()
    {
        colorUI = AppController.instance.colorTheme.detalle4;
        base.Start();
    }
}
