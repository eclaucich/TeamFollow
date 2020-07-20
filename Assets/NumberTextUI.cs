
public class NumberTextUI : TextUI
{
    override public void Start()
    {
        base.Start();
        text.font = AppController.instance.numberFont;
    }
}
