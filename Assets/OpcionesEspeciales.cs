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

    public void SetMenu(List<string> opciones, string nombreCategoria_, InputPrefabEspecial input)
    {
        for (int i = 0; i < opciones.Count; i++)
        {
            GameObject go = Instantiate(opcionPrefab, parentTransformOpciones, false);
            go.SetActive(true);
            go.GetComponentInChildren<Text>().text = opciones[i];
        }

        nombreCategoria.text = nombreCategoria_;

        inputEspecial = input;
    }

    public void SeleccionarOpcion(BotonOpcion opcion_)
    {
        inputEspecial.SetValor(opcion_.GetValor());
        Cerrar();
    }

    public void Cerrar()
    {
        Destroy(gameObject);
    }
}
