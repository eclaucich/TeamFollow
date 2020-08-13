using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColumnGraph : MonoBehaviour
{
    [SerializeField] private Text valueText = null;
    [SerializeField] private Image image = null;

    public Color normalColor;
    public Color maxColor;
    public Color minColor;


    public void SetValue(float value)
    {
        valueText.text = value.ToString();
    }

    public void SetHeight(float _height)
    {
        GetComponent<RectTransform>().sizeDelta = new Vector2(GetComponent<RectTransform>().sizeDelta.x, _height); 
    }

    public void SetColorNormal()
    {
        image.color = normalColor;
    }

    public void SetColorMax()
    {
        image.color = maxColor;
    }

    public void SetColorMin()
    {
        image.color = minColor;
    }
}
