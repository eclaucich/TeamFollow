using UnityEngine;
using UnityEngine.UI;

public class Buscador : MonoBehaviour
{
    [SerializeField] private GameObject botonCerrar = null;
    [SerializeField] private Text textInput = null;
    [SerializeField] private GameObject placeholder = null;
    [SerializeField] private TextLanguage cantidadResultadosText = null;

    [SerializeField] private Animator animator;

    private bool activado;

    void Start()
    {
        SetBuscador(false);
    }

    private void Update() 
    {
        if(activado)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                SetBuscador(false);
            }
        }
    }
    
    public void SetBuscador(bool active)
    {
        activado = active;
        CanvasController.instance.retrocesoPausado = active;
        animator.SetBool("open", active);
        if(!active)
            cantidadResultadosText.gameObject.SetActive(false);
    }

    public void SetCantidadResultados(int cantResultados)
    {
        cantidadResultadosText.gameObject.SetActive(true);
        cantidadResultadosText.SetText(cantResultados + " resultado/s encontrado/s".ToUpper(), AppController.Idiomas.Español);
        cantidadResultadosText.SetText(cantResultados + " result/s found".ToUpper(), AppController.Idiomas.Ingles);
    }
}
