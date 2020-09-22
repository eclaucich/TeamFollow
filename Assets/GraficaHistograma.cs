using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class GraficaHistograma : Grafica
{
    [SerializeField] private GameObject columnaPrefab = null;
    [SerializeField] private Transform columnsTransform = null;

    private List<GameObject> columnPrefabs;

    private bool maxOn = false;
    private bool minOn = false;


    protected override void Awake()
    {
        base.Awake();
        columnPrefabs = new List<GameObject>();
    }

    public override void Graficar<T>(Dictionary<T, int> datos, bool setSize=true)
    {
        base.Graficar(datos);

        if (debug)
        {
            for (int i = 0; i < valores.Length; i++)
            {
                GameObject cGO = Instantiate(columnaPrefab, columnsTransform, false);
                cGO.SetActive(true);

                ColumnGraph column = cGO.GetComponent<ColumnGraph>();
                column.SetHeight(MapYValue(valores[i]));
                column.SetValue(valores[i]);

                columnPrefabs.Add(cGO);
            }
        }
        else
        {
            info = SetInfoStruct(datos);

            foreach (var dato in datos)
            {
                GameObject cGO = Instantiate(columnaPrefab, columnsTransform, false);
                cGO.SetActive(true);

                ColumnGraph column = cGO.GetComponent<ColumnGraph>();

                column.SetHeight(MapYValue(dato.Value));
                column.SetValue(dato.Value);

                columnPrefabs.Add(cGO);
            }
        }

    }


    #region Setear el struct con la info dela grafica
    private infoDatos SetInfoStruct<T>(Dictionary<T, int> datos)
    {
        int maxV = -1, maxI = -1, minV = -1, minI = -1;
        int index = 0, acum = 0;
        foreach (var dato in datos)
        {
            if (maxV == -1)
            {
                maxV = dato.Value;
                maxI = index;
            }
            else
            {
                if (dato.Value > maxV)
                {
                    maxV = dato.Value;
                    maxI = index;
                }
            }
            if (minV == -1)
            {
                minV = dato.Value;
                minI = index;
            }
            else
            {
                if (dato.Value < minV)
                {
                    minV = dato.Value;
                    minI = index;
                }
            }
            acum += dato.Value;
            index++;
        }

        infoDatos info = new infoDatos
        {
            min = minV,
            minIndex = minI,
            max = maxV,
            maxIndex = maxI,
            median = (float)acum / datos.Count
        };

        return info;
    }
    #endregion

    #region Estado inicial de la grafica y creación de prefabs
    public override void CrearPrefabs<T>(Dictionary<T, int> datos, bool isDatoJugador)
    {
        foreach (var dato in datos)
        {
            GameObject go = Instantiate(botonDatoGraficaPrefab, datosTransform, false);
            go.SetActive(true);
            go.GetComponent<BotonDatoGrafica>().SetDato(dato.Key, dato.Value, isDatoJugador);
        }

        if (vertical)
            cantMinima = (int)(scrollRect.GetComponent<RectTransform>().rect.height / (prefabHeight + datosTransform.GetComponent<VerticalLayoutGroup>().spacing));
    }
    public override void ResetGraph()
    {
        foreach (var prefab in columnPrefabs)
        {
            Destroy(prefab);
        }
        columnPrefabs.Clear();
    }
    #endregion

    #region Funciones para activar/desactivar las opciones
    public void ToggleMaxValue()
    {
        maxOn = !maxOn;
        if(maxOn) columnPrefabs[info.maxIndex].GetComponent<ColumnGraph>().SetColorMax();
        else      columnPrefabs[info.maxIndex].GetComponent<ColumnGraph>().SetColorNormal();
    }
    public void ToggleMinValue()
    {
        minOn = !minOn;
        if (minOn) columnPrefabs[info.minIndex].GetComponent<ColumnGraph>().SetColorMin();
        else       columnPrefabs[info.minIndex].GetComponent<ColumnGraph>().SetColorNormal();
    }
    #endregion
}
