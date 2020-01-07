using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EstadisticasGlobalesEquipo : MonoBehaviour
{
    [SerializeField] private GameObject panelFutbol = null;
    [SerializeField] private GameObject panelTenis = null;
    [SerializeField] private GameObject panelHockeyCesped = null;
    [SerializeField] private GameObject panelRugby = null;
    [SerializeField] private GameObject panelPadel = null;
    [SerializeField] private GameObject panelHockeyPatines = null;
    [SerializeField] private GameObject panelVoley = null;
    [SerializeField] private GameObject panelSoftball = null;
    [SerializeField] private GameObject panelHandball = null;
    [SerializeField] private GameObject panelBasket = null;

    private List<GameObject> panelesEstadisticas;

    private void SetListaPaneles()
    {
        panelesEstadisticas = new List<GameObject>();

        panelesEstadisticas.Add(panelFutbol);
        panelesEstadisticas.Add(panelTenis);
        panelesEstadisticas.Add(panelHockeyCesped);
        panelesEstadisticas.Add(panelRugby);
        panelesEstadisticas.Add(panelPadel);
        panelesEstadisticas.Add(panelHockeyPatines);
        panelesEstadisticas.Add(panelVoley);
        panelesEstadisticas.Add(panelSoftball);
        panelesEstadisticas.Add(panelHandball);
        panelesEstadisticas.Add(panelBasket);
    }

    public Transform SetPanelEstadisticas()
    {
        SetListaPaneles();

        int index = 0;

        foreach (var panel in panelesEstadisticas)
        {
            panel.SetActive(false);
        }

        switch (AppController.instance.equipoActual.GetDeporte())
        {
            case Deportes.Deporte.Futbol:
                panelesEstadisticas[0].SetActive(true);
                index = 0;
                break;
            case Deportes.Deporte.Tenis:
                panelesEstadisticas[1].SetActive(true);
                index = 1;
                break;
            case Deportes.Deporte.HockeyCesped:
                panelesEstadisticas[2].SetActive(true);
                index = 2;
                break;
            case Deportes.Deporte.Rugby:
                panelesEstadisticas[3].SetActive(true);
                index = 3;
                break;
            case Deportes.Deporte.Padel:
                panelesEstadisticas[4].SetActive(true);
                index = 4;
                break;
            case Deportes.Deporte.HockeyPatines:
                panelesEstadisticas[5].SetActive(true);
                index = 5;
                break;
            case Deportes.Deporte.Voley:
                panelesEstadisticas[6].SetActive(true);
                index = 6;
                break;
            case Deportes.Deporte.Softball:
                panelesEstadisticas[7].SetActive(true);
                index = 7;
                break;
            case Deportes.Deporte.Handball:
                panelesEstadisticas[8].SetActive(true);
                index = 8;
                break;
            case Deportes.Deporte.Basket:
                panelesEstadisticas[9].SetActive(true);
                index = 9;
                break;
        }

        GetComponent<ScrollRect>().content = panelesEstadisticas[index].GetComponent<RectTransform>();

        return panelesEstadisticas[index].transform;
    }
}
