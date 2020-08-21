using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelEstadisticas : MonoBehaviour {

    [SerializeField] protected List<string> listaEstadisticas;
    [SerializeField] protected List<string> listaIniciales;
    [SerializeField] private GameObject togglePrefab = null;
    protected EstadisticaDeporte nombreEstadisticas;

    private Transform parentTransform;

    private List<TextScript> listaToggles;

    [SerializeField] private ScrollRect scrollRect = null;

    private float prefabHeight;

    private void Start()
    {
        parentTransform = transform.GetChild(0);
        listaToggles = new List<TextScript>();
        nombreEstadisticas = GetComponent<EstadisticaDeporte>();
        Array listaTipoEstadisticas = nombreEstadisticas.GetEstadisticas();

        for (int i = 0; i < listaTipoEstadisticas.Length; i++)
        {
            if ((EstadisticaDeporte.Estadisticas)listaTipoEstadisticas.GetValue(i) > 0)
            {
                //SE DEBERÍA OBTENER DE nombreEstadisticas, EL TIPO DEL ENUM Y TRABAJR CON ESO EN VEZ DE CON EL NOMBRE
                GameObject toggleGO = Instantiate(togglePrefab, parentTransform, false);

                string[] nameEspañol = EstadisticasDeporteDisplay.GetStatisticsName((EstadisticaDeporte.Estadisticas)listaTipoEstadisticas.GetValue(i), AppController.Idiomas.Español);// nombreEstadisticas.GetStatisticsName(i, AppController.Idiomas.Español);
                string[] nameIngles = EstadisticasDeporteDisplay.GetStatisticsName((EstadisticaDeporte.Estadisticas)listaTipoEstadisticas.GetValue(i), AppController.Idiomas.Ingles); //nombreEstadisticas.GetStatisticsName(i, AppController.Idiomas.Ingles);

                TextScript txtScript = toggleGO.GetComponent<TextScript>();
                txtScript.SetTipoEstadistica((EstadisticaDeporte.Estadisticas)listaTipoEstadisticas.GetValue(i));
                txtScript.SetName(nameEspañol[0], nameEspañol[1], AppController.Idiomas.Español);
                toggleGO.GetComponent<TextScript>().SetName(nameIngles[0], nameIngles[1], AppController.Idiomas.Ingles);
                listaToggles.Add(toggleGO.GetComponent<TextScript>());
            }
        }
        /*
        for (int i = 0; i < listaEstadisticas.Count; i++)
        {
            GameObject toggleGO = Instantiate(togglePrefab, parentTransform, false);
            toggleGO.GetComponent<TextScript>().SetName(listaEstadisticas[i], listaIniciales[i]);
            listaToggles.Add(toggleGO.GetComponent<Toggle>());
        }*/

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

    public List<EstadisticaDeporte.Estadisticas> GetListaEstadisticasActivas()
    {
        List<EstadisticaDeporte.Estadisticas> lista = new List<EstadisticaDeporte.Estadisticas>();

        for (int i = 0; i < listaToggles.Count; i++)
        {
            if (listaToggles[i].IsOn())
            {
                lista.Add(listaToggles[i].GetTipoEstadisticaFocus());
            }
        }

        return lista;
    }

    public List<string> GetListaNombreEstadisticasActivas()
    {
        List<string> lista = new List<string>();

        for (int i = 0; i < listaToggles.Count; i++)
        {
            if (listaToggles[i].IsOn())
            {
                lista.Add(listaToggles[i].GetNombre());
            }
        }

        return lista;
    }

    public List<string> GetListaInicialesEstadisticasActivas()
    {
        List<string> lista = new List<string>();

        for (int i = 0; i < listaToggles.Count; i++)
        {
            if (listaToggles[i].IsOn())
            {
                lista.Add(listaToggles[i].GetInicial());
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
        return (int)(scrollRect.GetComponent<RectTransform>().rect.height / (prefabHeight + 50 +60)); //20 es el spacing
    }

    public int GetChildCount()
    {
        return listaEstadisticas.Count;
    }
}
