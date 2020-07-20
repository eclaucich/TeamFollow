using UnityEngine.UI;

public class TextUI : UIComponent
{
    protected Text text;

    override public void Start()
    {
        text = GetComponent<Text>();
        colorUI = AppController.instance.colorTheme.texto;

        text.color = colorUI;
        text.font = AppController.instance.textFont;
    }
}
