using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PantallaCarga : MonoBehaviour
{

    private float time = 0f;
    private float maxTime = 3f;
    private bool isLoading = false;

    [SerializeField] private Animator animator;


    private void Start()
    {
        gameObject.SetActive(false);
        isLoading = false;
    }

    private void Update()
    {
        if (gameObject.activeSelf)
        {
            time += Time.deltaTime;
            if (time >= maxTime)
                Desactivar();
        }
    }

    public void Activar()
    {
        gameObject.SetActive(true);
        isLoading = true;
        CanvasController.instance.retrocesoPausado = true;
        animator.SetBool("open", true);
        Debug.Log("ACTIVADA PC");
    }

    public void Desactivar()
    {
        time = 0f;
        Screen.orientation = ScreenOrientation.Landscape;
        isLoading = false;
        animator.SetBool("open", false);
        CanvasController.instance.retrocesoPausado = false;
        gameObject.SetActive(false);
        Debug.Log("PANT CARGA DESAC");
    }

    public bool IsLoading()
    {
        return isLoading;
    }
}
