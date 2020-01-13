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

        panelesEstadisticas.Add(panelBasket);
        panelesEstadisticas.Add(panelFutbol);
        panelesEstadisticas.Add(panelHandball);
        panelesEstadisticas.Add(panelHockeyCesped);
        panelesEstadisticas.Add(panelHockeyPatines);
        panelesEstadisticas.Add(panelPadel);
        panelesEstadisticas.Add(panelRugby);
        panelesEstadisticas.Add(panelSoftball);
        panelesEstadisticas.Add(panelTenis);
        panelesEstadisticas.Add(panelVoley);
    }

    public Transform Set()
    {
        SetListaPaneles();

        foreach (var panel in panelesEstadisticas)
        {
            panel.SetActive(false);
        }

        int index = (int)AppController.instance.equipoActual.GetDeporte();
        panelesEstadisticas[index].SetActive(true);

        GetComponent<ScrollRect>().content = panelesEstadisticas[index].GetComponent<RectTransform>();

        return panelesEstadisticas[index].transform;
    }
}
