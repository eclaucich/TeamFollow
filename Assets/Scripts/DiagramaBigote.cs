using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiagramaBigote : MonoBehaviour
{

    [SerializeField] private RectTransform caja = null;
    [SerializeField] private GameObject lineaMediana = null;
    [SerializeField] private RectTransform bigoteSuperior = null;
    [SerializeField] private RectTransform bigoteInferior = null;

    private float Q1;
    private float Q2;
    private float Q3;
    private float bigoteSup;
    private float bigoteInf;

    public void SetDiagrama(float _Q1, float _Q2, float _Q3, float _bigoteSup, float _bigoteInf)
    {
        Q1 = _Q1; Q2 = _Q2; Q3 = _Q3; bigoteSup = _bigoteSup; bigoteInf = _bigoteInf;

        Debug.Log("Q1: " + Q1);
        Debug.Log("Q2: " + Q2);
        Debug.Log("Q3: " + Q3);
        Debug.Log("BSUP: " + bigoteSup);
        Debug.Log("BINF: " + bigoteInf);

        SetTamañoCaja();
        SetLineaMediana();
        SetBigotes();
    }

    private void SetTamañoCaja()
    {
        caja.sizeDelta = new Vector2(Q3-Q1, caja.sizeDelta.y);
    }

    private void SetLineaMediana()
    {
        lineaMediana.transform.localPosition = new Vector2(Q2, lineaMediana.transform.localPosition.y);
    }

    private void SetBigotes()
    {
        bigoteInferior.sizeDelta = new Vector2(bigoteInf - Q1, bigoteInferior.sizeDelta.y);
        bigoteSuperior.sizeDelta = new Vector2(bigoteSup - Q3, bigoteSuperior.sizeDelta.y);
    }
}
