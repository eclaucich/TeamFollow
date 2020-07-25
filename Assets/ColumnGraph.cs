using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColumnGraph : MonoBehaviour
{
    [SerializeField] private Text valueText = null;

    public void SetValue(float value)
    {
        valueText.text = value.ToString();
    }

    public void SetHeight(float _height)
    {
        GetComponent<RectTransform>().sizeDelta = new Vector2(GetComponent<RectTransform>().sizeDelta.x, _height); 
    }
}
