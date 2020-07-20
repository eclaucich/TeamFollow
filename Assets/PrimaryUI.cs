public class PrimaryUI : UIComponent
{
    public override void Start()
    {
        colorUI = AppController.instance.colorTheme.principal;

        base.Start();
    }
}
