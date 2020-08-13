using UnityEngine;
using UnityEngine.UI;

public class BackgroundUI : UIComponent
{
    override public void Start()
    {
        colorUI = AppController.instance.colorTheme.detalle2;

        base.Start();
    }

}
