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

        PanelCrearJugada.instance.SetPanelOpcionesActual(null);
    }

    public override void ToggleDesplegar()
    {
        gameObject.SetActive(!gameObject.activeSelf);
        if (gameObject.activeSelf)
        {
            PanelCrearJugada.instance.CerrarPanelOpcionesActual();
            PanelCrearJugada.instance.SetPanelOpcionesActual(this);
        }
        base.ToggleDesplegar();
    }
}
