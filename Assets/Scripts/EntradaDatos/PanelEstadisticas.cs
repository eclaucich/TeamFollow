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

    [SerializeField] private ScrollRect scrollRect = null;

    private float prefabHeight;

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

        prefabHeight = togglePrefab.GetComponent<RectTransform>().rect.height;
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

    public int GetCantMInima()
    {
        if (scrollRect == null) Debug.Log("SCROLL RECT NULL");
        if(prefabHeight == 0) prefabHeight = togglePrefab.GetComponent<RectTransform>().rect.height;
        return (int)(scrollRect.GetComponent<RectTransform>().rect.height / (prefabHeight + 20)); //20 es el spacing
    }

    public int GetChildCount()
    {
        return listaEstadisticas.Count;
    }
}
