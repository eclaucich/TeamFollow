
public class ErrorUI : UIComponent
{
    public override void Start()
    {
        colorUI = AppController.instance.colorTheme.error;

        base.Start();
    }
}
