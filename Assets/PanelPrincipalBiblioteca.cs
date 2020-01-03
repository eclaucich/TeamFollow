using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelPrincipalBiblioteca : Panel
{
    [SerializeField] private GameObject botonImagenPrefab = null;
    [SerializeField] private Transform parentTransform = null;

    private List<GameObject> listaPrefabs = null;

    override public void Start()
    {
        base.Start();
        listaPrefabs = new List<GameObject>();

        SetPanePrincipal();
    }

    public void SetPanePrincipal()
    {
        BorrarPrefabs();
        CrearPrefabs();
    }

    private void BorrarPrefabs()
    {
        foreach (var prefab in listaPrefabs)
        {
            Destroy(prefab);
        }
        listaPrefabs.Clear();
    }

    private void CrearPrefabs()
    {
        //Por cada imagen en el sistema
        GameObject botonImagenGO = Instantiate(botonImagenPrefab.gameObject, parentTransform, false);

        botonImagenGO.GetComponentInChildren<Button>().GetComponentInChildren<Text>().text = "NOMBRE IMAGEN";

        listaPrefabs.Add(botonImagenGO);
    }
}
