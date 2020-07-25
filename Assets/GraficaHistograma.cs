using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml;
using UnityEngine;

public class GraficaHistograma : Grafica
{
    [SerializeField] private GameObject columnaPrefab = null;
    [SerializeField] private Transform columnsTransform = null;

    private List<GameObject> columnPrefabs;

    private void Awake()
    {
        columnPrefabs = new List<GameObject>();
    }

    public override void Graficar<T>(Dictionary<T, int> datos)
    {
        base.Graficar(datos);

        foreach (var dato in valores)
        {
            GameObject cGO = Instantiate(columnaPrefab, columnsTransform, false);
            cGO.SetActive(true);

            ColumnGraph column = cGO.GetComponent<ColumnGraph>();
            column.SetHeight(MapYValue(dato));
            column.SetValue(dato);

            columnPrefabs.Add(cGO);
        }

    }

    public override void ResetGraph()
    {
        foreach (var prefab in columnPrefabs)
        {
            Destroy(prefab);
        }
        columnPrefabs.Clear();
    }
}
