﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MensajeError : MonoBehaviour
{
    [SerializeField] private TextLanguage mensajeText = null;
    [SerializeField] private float aliveTime = 2f;

    public bool animate = true;

    private Animator animator;
    private float currentTime = 0f;
    private bool active = false;

    private void Start()
    {
        if (animate)
        {
            animator = GetComponent<Animator>();
        }
        else 
            Desactivar();
    }

    private void Update()
    {
        if (active)
        {
            currentTime += Time.deltaTime;
            if (currentTime > aliveTime)
                Desactivar();
        }
    }

    public void Activar()
    {
        active = true;
        if (animate)
        {
            animator.SetBool("open", active);
        }

        else gameObject.SetActive(true);
    }

    public void Desactivar()
    {
        currentTime = 0f;
        active = false;
        if (animate)
        {
            animator.SetBool("open", active);
        }
        else gameObject.SetActive(false);
    }

    public void SetText(string text_, AppController.Idiomas idioma)
    {
        mensajeText.SetText(text_, idioma);
    }
}
