using UnityEngine;
using UnityEngine.UI;

public class TextScript : MonoBehaviour {

    [SerializeField] private Text inicialText = null;

    public void SetName(string name_, string inicial_)
    {
        GetComponentInChildren<Text>().text = name_;
        gameObject.name = name_;
        inicialText.text = inicial_;
    }
}
