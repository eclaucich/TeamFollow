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
        closeZone.SetActive(false);
    }

    virtual public void Cerrar()
    {
        if (animator == null) return;
        desplegado = false;

        animator.SetBool("open", false);
        closeZone.SetActive(false);
    }

    virtual public void ToggleDesplegar()
    {        
        bool isOpen = animator.GetBool("open");

        desplegado = !isOpen;

        animator.SetBool("open", !isOpen);

        closeZone.SetActive(!isOpen);
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
