using UnityEngine;
using UnityEngine.UI;

public class UIComponent : MonoBehaviour
{
    protected Image image;
    protected RawImage rawImage;
    protected Color colorUI;

    virtual public void Start()
    {
        image = GetComponent<Image>();
        rawImage = GetComponent<RawImage>();

        if (image)
            image.color = colorUI;
        else
            rawImage.color = colorUI;
    }

    public Color GetUIColor()
    {
        return colorUI;
    }
}
