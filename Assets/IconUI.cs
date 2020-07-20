
public class IconUI : UIComponent
{
    public override void Start()
    {
        colorUI = AppController.instance.colorTheme.icon;

        base.Start();
    }

}
