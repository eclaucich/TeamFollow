using UnityEngine;

public class PanelOpcionesHerramienta : MensajeDesplegable
{

    [SerializeField] private PanelEdicion panelEdicion = null;

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

        panelEdicion.SetPanelOpcionesActual(null);
    }

    public override void ToggleDesplegar()
    {
        gameObject.SetActive(!gameObject.activeSelf);
        if (gameObject.activeSelf)
        {
            panelEdicion.CerrarPanelOpcionesActual();
            panelEdicion.SetPanelOpcionesActual(this);
        }
        base.ToggleDesplegar();
    }
}
