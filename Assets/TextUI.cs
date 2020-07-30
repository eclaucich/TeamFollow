using UnityEngine.UI;

public class TextUI : UIComponent
{
    protected Text _text;

    override public void Start()
    {
        _text = GetComponent<Text>();
        colorUI = AppController.instance.colorTheme.texto;

        _text.color = colorUI;
        _text.font = AppController.instance.textFont;
    }
}
