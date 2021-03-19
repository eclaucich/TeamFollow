using UnityEngine;

public class MensajeDesplegable : MonoBehaviour
{
    protected Animator animator;
    protected bool desplegado = false;
  
    [Space]
    [Header("Mensaje Desplegable")]
    [SerializeField] protected GameObject closeZone = null;
    [SerializeField] protected TextLanguage text = null;
    [SerializeField] private bool controlarBotonMenu = true;


    virtual public void Start()
    {
        animator = GetComponent<Animator>();
        if(closeZone!=null)
            closeZone.SetActive(false);
    }

    virtual public void Cerrar()
    {
        if (animator == null) return;
        desplegado = false;

        animator.SetBool("open", false);
        if(closeZone!=null)
            closeZone.SetActive(false);
        
        if(controlarBotonMenu)
            CanvasController.instance.botonDespliegueMenu.SetActive(true);
    }

    virtual public void ToggleDesplegar()
    {        
        bool isOpen = animator.GetBool("open");

        desplegado = !isOpen;

        animator.SetBool("open", !isOpen);

        if(closeZone!=null)
            closeZone.SetActive(!isOpen);

        if(controlarBotonMenu)
        {
            if(desplegado)
                CanvasController.instance.botonDespliegueMenu.SetActive(false);
            else
                CanvasController.instance.botonDespliegueMenu.SetActive(true);
        }
    }

    public bool isDesplegado()
    {
        return desplegado;
    }

    public void DeshabilitarCloseZone()
    {
        closeZone.GetComponent<CloseMenuZone>().DeshabilitarCloseZone();
    }
}
