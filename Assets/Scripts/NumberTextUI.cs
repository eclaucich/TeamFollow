
public class NumberTextUI : TextUI
{
    override public void Start()
    {
        base.Start();
        _text.font = AppController.instance.numberFont;
    }
}
