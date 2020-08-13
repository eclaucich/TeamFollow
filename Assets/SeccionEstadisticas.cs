using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeccionEstadisticas : MonoBehaviour
{
    [SerializeField] private Text nombreJugadorText = null;
    [SerializeField] private GameObject prefabEstadistica = null;
    [SerializeField] private Transform parentPrefabs = null;

    private JugadorEntradaDato jugadorEntradaDatoFocus;

    private List<EstadisticaEntradaDato> listaEstadisticaEntradaDatos;

    private void Start()
    {
        listaEstadisticaEntradaDatos = new List<EstadisticaEntradaDato>();
    }

    public void SetSeccionEstadisticas(List<string> _nombres, List<string> _iniciales)
    {
        nombreJugadorText.text = "";

        for (int i=0; i<_nombres.Count; i++)
        {
            GameObject go = Instantiate(prefabEstadistica, parentPrefabs, false);
            go.SetActive(true);
            EstadisticaEntradaDato eed = go.GetComponent<EstadisticaEntradaDato>();
            eed.Initiate(_nombres[i], _iniciales[i]);
            listaEstadisticaEntradaDatos.Add(eed);
        }
    }

    public void SetJugadorEntradaDatoFocus(JugadorEntradaDato _jugadorEntradaDato)
    {
        jugadorEntradaDatoFocus = _jugadorEntradaDato;
        nombreJugadorText.text = _jugadorEntradaDato.GetNombreJugador();
        SetPrefabsEstadisticaEntradaDato();
    }

    private void SetPrefabsEstadisticaEntradaDato()
    {
        foreach (var eed in listaEstadisticaEntradaDatos)
        {
            eed.SetValor(jugadorEntradaDatoFocus);
        }
    }

    public void AgregarEstadisticasJugadorFocus(string _categoria, int cant)
    {
        jugadorEntradaDatoFocus.AgregarEstadistica(_categoria, cant);
    }
}
