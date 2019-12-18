using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FichaEnemiga : ObjetoEdicion {

    private RectTransform rect;

    public void Start()
    {
        Physics.queriesHitTriggers = enabled;
        rect = GetComponent<RectTransform>();
        rect.localPosition = new Vector3(rect.localPosition.x, rect.localPosition.y, 0f);
    }

    private void Update()
    {
        rect.localPosition = new Vector3(rect.localPosition.x, rect.localPosition.y, 0f);
    }

}
