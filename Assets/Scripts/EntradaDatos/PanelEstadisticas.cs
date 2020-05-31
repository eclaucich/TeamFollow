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

    private ScrollRect scrollRect;
    private int maxShown = 13; // esto es para determinar cuándo aparecen las flechas del scroll (cambiar según la cantidad total de cosas en el scroll) para este caso entran 12 sinnecesidad de scroll, a los 13 se activa el scroll

    private void Start()
    {
        parentTransform = transform.GetChild(0);
        listaToggles = new List<Toggle>();
        
        for (int i = 0; i < listaEstadisticas.Count; i++)
        {
            GameObject toggleGO = Instantiate(togglePrefab, parentTransform, false);
            toggleGO.GetComponent<TextScript>().SetName(listaEstadisticas[i], listaIniciales[i]);
            listaToggles.Add(toggleGO.GetComponent<Toggle>());
        }

        scrollRect = GetComponent<ScrollRect>();
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

    public ScrollRect GetScrollRect()
    {
        return scrollRect;
    }

    public int GetMaxShown()
    {
        return maxShown;
    }

    public int GetChildCount()
    {
        return parentTransform.childCount;
    }
}
