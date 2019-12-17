using UnityEngine;
using UnityEngine.UI;

public class TextScript : MonoBehaviour {

    public void SetName(string name_)
    {
        GetComponentInChildren<Text>().text = name_;
        gameObject.name = name_;
    }
}
