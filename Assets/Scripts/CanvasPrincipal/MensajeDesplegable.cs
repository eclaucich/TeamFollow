using UnityEngine;

public class MensajeDesplegable : MonoBehaviour
{
    protected Animator animator;
    protected bool desplegado = false;
  
    [SerializeField] protected GameObject closeZone = null;
    [SerializeField] protected TextLanguage text = null;

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
        Debug.Log("CERRAR");
    }

    virtual public void ToggleDesplegar()
    {        
        bool isOpen = animator.GetBool("open");

        desplegado = !isOpen;

        animator.SetBool("open", !isOpen);

        if(closeZone!=null)
            closeZone.SetActive(!isOpen);

        Debug.Log("TOGGLE");
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
