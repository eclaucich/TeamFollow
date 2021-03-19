using UnityEngine;
using UnityEngine.UI;

public class PanelImagen : Panel
{
    [SerializeField] private Image imagen = null;

    public void SetPanel(BotonImagen botonFocus_)
    {
        Screen.orientation = ScreenOrientation.Landscape;
        //Screen.SetResolution(720, 1280, false);

        SetPublicity();

        CanvasController.instance.botonDespliegueMenu.SetActive(false);

        imagen.sprite = botonFocus_.GetSprite();
    }

}
