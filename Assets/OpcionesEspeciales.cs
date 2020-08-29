using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpcionesEspeciales : MonoBehaviour
{
    [SerializeField] private GameObject opcionPrefab = null;
    [SerializeField] private Text nombreCategoria = null;
    [SerializeField] private Transform parentTransformOpciones = null;

    private Animator animator;
    private InputPrefabEspecial inputEspecial;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Cerrar();
    }
    public void SetMenu(List<string> opcionesEspañol, List<string> opcionesIngles, string nombreCategoria_, InputPrefabEspecial input)
    {
        Debug.Log("OPCIONES: " + opcionesEspañol.Count);
        CanvasController.instance.retrocesoPausado = true;

        for (int i = 0; i < opcionesEspañol.Count; i++)
        {
            GameObject go = Instantiate(opcionPrefab, parentTransformOpciones, false);
            go.SetActive(true);
            TextLanguage textLanguage = go.GetComponentInChildren<TextLanguage>();
            textLanguage.SetText(opcionesEspañol[i], AppController.Idiomas.Español);
            textLanguage.SetText(opcionesIngles[i], AppController.Idiomas.Ingles);
        }

        nombreCategoria.text = nombreCategoria_;

        inputEspecial = input;
    }

    public void SeleccionarOpcion(BotonOpcion opcion_)
    {
        inputEspecial.SetValor(opcion_.GetValor(AppController.Idiomas.Español), AppController.Idiomas.Español);
        inputEspecial.SetValor(opcion_.GetValor(AppController.Idiomas.Ingles), AppController.Idiomas.Ingles);
        Cerrar();
    }

    public void Cerrar()
    {
        CanvasController.instance.retrocesoPausado = false;
        Destroy(gameObject);
    }
}
