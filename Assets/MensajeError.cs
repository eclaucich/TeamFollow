using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MensajeError : MonoBehaviour
{
    [SerializeField] private Text mensajeText = null;

    [SerializeField] private float aliveTime = 2f;
    private float currentTime = 0f;

    private void Update()
    {
        if (gameObject.activeSelf)
        {
            currentTime += Time.deltaTime;
            if (currentTime > aliveTime)
                Desactivar();
        }
    }

    public void Activar()
    {
        gameObject.SetActive(true);
    }

    public void Desactivar()
    {
        gameObject.SetActive(false);
        currentTime = 0f;
    }

    public void SetText(string text_)
    {
        mensajeText.text = text_;
    }
}
