using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuntaFlecha : MonoBehaviour
{
    public void SetMaterialColor(Color _color)
    {
        GetComponent<Image>().color = _color;
    }

}
