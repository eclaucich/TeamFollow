using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelEstadisticas : MonoBehaviour {

    [SerializeField] protected List<string> listaEstadisticas;
    [SerializeField] protected List<string> listaIniciales;
    [SerializeField] private GameObject togglePrefab = null;

    private Transform parentTransform;

    private List<Toggle> listaToggles;

    private void Start()
    {
        parentTransform = transform.GetChild(0);
        listaToggles = new List<Toggle>();
        
        for (int i = 0; i < listaEstadisticas.Count; i++)
        {
            GameObject toggleGO = Instantiate(togglePrefab, parentTransform, false);
            toggleGO.GetComponent<TextScript>().SetName(listaEstadisticas[i]);
            listaToggles.Add(toggleGO.GetComponent<Toggle>());
        }
    }

    public void Activar()
    {
        gameObject.SetActive(true);        
    }

    public void Desactivar()
    {
        gameObject.SetActive(false);
    }

    public List<string> GetListaEstadisticasActivas()
    {
        List<string> lista = new List<string>();

        for (int i = 0; i < listaToggles.Count; i++)
        {
            if (listaToggles[i].isOn)
            {
                lista.Add(listaToggles[i].name);
            }
        }

        return lista;
    }

    public List<string> GetListaInicialesEstadisticasActivas()
    {
        List<string> lista = new List<string>();

        for (int i = 0; i < listaToggles.Count; i++)
        {
            if (listaToggles[i].isOn)
            {
                lista.Add(listaIniciales[i]);
            }
        }

        return lista;
    }
}
