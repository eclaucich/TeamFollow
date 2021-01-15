
public class SecundaryUI : UIComponent
{
    public override void Start()
    {
        colorUI = AppController.instance.colorTheme.secundario;

        base.Start(); 
    }
}
