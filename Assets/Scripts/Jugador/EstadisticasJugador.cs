using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EstadisticasJugador : MonoBehaviour
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

    public Transform Set()
    {
        SetListaPaneles();

        foreach (var panel in panelesEstadisticas)
        {
            panel.SetActive(false);
        }

        int index = 0;

        switch (AppController.instance.equipoActual.GetDeporte())
        {
            case "Fútbol":
                panelesEstadisticas[0].SetActive(true);
                index = 0;
                break;
            case "Tenis":
                panelesEstadisticas[1].SetActive(true);
                index = 1;
                break;
            case "Hockey Cesped":
                panelesEstadisticas[2].SetActive(true);
                index = 2;
                break;
            case "Rugby":
                panelesEstadisticas[3].SetActive(true);
                index = 3;
                break;
            case "Padel":
                panelesEstadisticas[4].SetActive(true);
                index = 4;
                break;
            case "Hockey Patines":
                panelesEstadisticas[5].SetActive(true);
                index = 5;
                break;
            case "Voley":
                panelesEstadisticas[6].SetActive(true);
                index = 6;
                break;
            case "Softball":
                panelesEstadisticas[7].SetActive(true);
                index = 7;
                break;
            case "Handball":
                panelesEstadisticas[8].SetActive(true);
                index = 8;
                break;
            case "Basket":
                panelesEstadisticas[9].SetActive(true);
                index = 9;
                break;
        }

        GetComponent<ScrollRect>().content = panelesEstadisticas[index].GetComponent<RectTransform>();

        return panelesEstadisticas[index].transform;
    }
}
