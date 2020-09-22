
public class FondoOverlayUI : UIComponent
{
    public override void Start()
    {
        colorUI = AppController.instance.colorTheme.fondoOverlay;

        base.Start();
    }
}
