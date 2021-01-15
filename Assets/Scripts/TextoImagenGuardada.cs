using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextoImagenGuardada : MonoBehaviour
{
    private Animator animator;

    private float currentTime = 0f;
    private float closeTime = 1f;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Mostrar()
    {
        animator.Play("Mostrar");
    }

    private void Cerrar()
    {
        if (currentTime < closeTime)
            currentTime += Time.deltaTime;
        else
        {
            animator.SetBool("open", false);
            currentTime = 0f;
        }
    }
}
