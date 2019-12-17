using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelOpcionesHerramienta : MensajeDesplegable
{

    public override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        Cerrar();
    }

    public override void Cerrar()
    {
        if (animator == null) return;
        desplegado = false;

        animator.SetBool("open", false);
        closeZone.SetActive(false);
        gameObject.SetActive(false);
    }

    public override void ToggleDesplegar()
    {
        gameObject.SetActive(!gameObject.activeSelf);
        base.ToggleDesplegar();
    }
}
