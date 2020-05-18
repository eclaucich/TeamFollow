using UnityEngine;
using UnityEngine.UI;

public class TextScript : MonoBehaviour {

    [SerializeField] private Text inicialText = null;

    [SerializeField] private Color selectedColor;
    [SerializeField] private Color notSelectedColor;

    private Toggle tgl;

    private void Start()
    {
        tgl = GetComponent<Toggle>();
        tgl.image.color = notSelectedColor;
        tgl.isOn = false;
    }

    public void SetName(string name_, string inicial_)
    {
        GetComponentInChildren<Text>().text = name_;
        gameObject.name = name_;
        inicialText.text = inicial_;
    }

    public void ToggleColor()
    {
        Toggle tgl = GetComponent<Toggle>();
        if (tgl.isOn)
            tgl.image.color = selectedColor;
        else
            tgl.image.color = notSelectedColor;
    }
}
