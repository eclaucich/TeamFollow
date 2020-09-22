using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.UI;

public class GraficaEstadistica : Grafica
{
    //Opciones
    [SerializeField] private GameObject grid = null;
    [SerializeField] private GameObject pointGraphPrefab = null;
    [SerializeField] private Transform pointsTransform = null;

    [SerializeField] private LineRenderer bestLine = null;
    [SerializeField] private GameObject bestPoint = null;
    [SerializeField] private Text bestText = null;

    [SerializeField] private LineRenderer worstLine = null;
    [SerializeField] private GameObject worstPoint = null;
    [SerializeField] private Text worstText = null;

    [SerializeField] private LineRenderer medianLine = null;
    [SerializeField] private Text medianText = null;

    private LineRenderer lr;

    private List<GameObject> graphPoints;

    override protected void Awake()
    {
        base.Awake();
        lr = GetComponent<LineRenderer>();
        graphPoints = new List<GameObject>();
    }

    public override void Graficar<T>(Dictionary<T, int> datos, bool setSize=true)
    {
        base.Graficar(datos);

        if (debug)
        {
            lr.positionCount = 0;
            int deltaT = w / (cantidadDatos);
            int coordX = xi;

            //SetLineRenderer(bestLine, maxDato[0], minDato[0], maxDato[0]);
            SetGraphPoint(bestPoint, deltaT, info.max, info.maxIndex);
            bestText.text = "Mejor:\n" + info.max.ToString();
            //SetLineRenderer(worstLine, minDato[0], minDato[0], maxDato[0]);
            SetGraphPoint(worstPoint, deltaT, info.min, info.minIndex);
            worstText.text = "Peor:\n" + info.min.ToString();
            SetLineRenderer(medianLine, info.median);
            medianText.text = "Media:\n" + info.median.ToString();

            for (int i = 0; i < cantidadDatos; i++)
            {
                lr.positionCount++;
                float mapValue = MapYValue(valores[i]);

                lr.SetPosition(i, new Vector3(coordX, mapValue, -10));

                GameObject pointGO = Instantiate(pointGraphPrefab, pointsTransform, false);
                pointGO.GetComponent<PointGraph>().SetPoint(coordX-xi-4f, mapValue-yi, valores[i]);

                graphPoints.Add(pointGO);
                coordX += deltaT;
            }

            lr.material.color = new Color(255f, 255f, 255f, 255f);
        }
        else
        {
            lr.positionCount = 0;

            //Cantidad de puntos
            int cantidadDatos = datos.Count;

            //Seperacion eje x, y coordenada x del punto actual
            int deltaT = 0, coordX = 0;

            if (cantidadDatos == 1)
            {
                deltaT = 4;
                coordX = xi;
            }
            else
            {
                deltaT = (xf-(3*xi)) / (cantidadDatos-1);
                coordX = xi;
            }

            info = SetInfoStruct(datos);

            //SetLineRenderer(bestLine, maxDato[0], minDato[0], maxDato[0]);
            SetGraphPoint(bestPoint, deltaT, info.max, info.maxIndex);
            bestText.text = "Mejor:\n" + info.max.ToString();

            //SetLineRenderer(worstLine, minDato[0], minDato[0], maxDato[0]);
            SetGraphPoint(worstPoint, deltaT, info.min, info.minIndex);
            worstText.text = "Peor:\n" + info.min.ToString();

            SetLineRenderer(medianLine, info.median);
            medianText.text = "Media:\n" + info.median.ToString();

            int index = 0;
            foreach (var dato in datos)
            {
                lr.positionCount++;

                float mapValue = MapYValue(dato.Value);
                if (cantidadDatos == 1) mapValue = yi;
 
                lr.SetPosition(index, new Vector3(coordX, mapValue, -10));

                GameObject pointGO = Instantiate(pointGraphPrefab, pointsTransform, false);

                pointGO.GetComponent<PointGraph>().SetPoint(coordX - xi - 4f, mapValue - yi, dato.Value);
                pointGO.GetComponent<PointGraph>().SetPartidoPadre((Partido)Convert.ChangeType(dato.Key, typeof(T)));

                graphPoints.Add(pointGO);
                coordX += deltaT;
                index++;
            }
        }
    }

    
    #region Funciones para setear estructura con mínimo, máximo, media y de mapeo
    private infoDatos SetInfoStruct<T>(Dictionary<T, int> datos)
    {
        int maxV=-1, maxI=-1, minV=-1, minI=-1;
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

    #region Funciones para setear Puntos, lineas y prefabs
    public override void CrearPrefabs<T>(Dictionary<T, int> datos, bool isDatoJugador)
    {
        foreach (var dato in datos)
        {
            GameObject go = Instantiate(botonDatoGraficaPrefab, datosTransform, false);
            go.SetActive(true);
            go.GetComponent<BotonDatoGrafica>().SetDato(dato.Key, dato.Value, isDatoJugador);
        }

        if (vertical)
        {
            cantMinima = (int)(scrollRect.GetComponent<RectTransform>().rect.height / (prefabHeight + datosTransform.GetComponent<VerticalLayoutGroup>().spacing));
        }
    }
    private void SetLineRenderer(LineRenderer lr, float value)
    {
        for (int i = 0; i < lr.positionCount; i++)
            lr.SetPosition(i, new Vector3(lr.GetPosition(i).x, MapYValue(value), -10));
    }

    private void SetGraphPoint(GameObject point, int deltaT, int value, int index)
    {
        float coordX = (float)(deltaT * index) - 4f; //el 4 es un offset para que esté bien centrado. Probar con distintos valores y resoluciones
        float coordY = MapYValue(value) - yi;
        point.transform.localPosition = new Vector3(coordX, coordY, -30f);
    }
    #endregion

    #region Funciones de Toggle de las opciones, y de reseteo de la grafica

    public override void ResetGraph()
    {
        foreach (var point in graphPoints)
            Destroy(point);
        graphPoints.Clear();

        grid.SetActive(false);
        bestPoint.SetActive(false);
        bestText.gameObject.SetActive(false);
        worstPoint.SetActive(false);
        worstText.gameObject.SetActive(false);
        medianLine.gameObject.SetActive(false);
        medianText.gameObject.SetActive(false);
    }

    public void ToggleGrid()
    {
        grid.SetActive(!grid.activeSelf);
    }

    public void ToggleBest()
    {
        bestPoint.gameObject.SetActive(!bestPoint.gameObject.activeSelf);
        bestText.gameObject.SetActive(!bestText.gameObject.activeSelf);
    }

    public void ToggleWorst()
    {
        worstPoint.gameObject.SetActive(!worstPoint.gameObject.activeSelf);
        worstText.gameObject.SetActive(!worstText.gameObject.activeSelf);
    }

    public void ToggleMedian()
    {
        medianLine.gameObject.SetActive(!medianLine.gameObject.activeSelf);
        medianText.gameObject.SetActive(!medianText.gameObject.activeSelf);
    }

    public void TogglePoints()
    {
        for (int i = 0; i < graphPoints.Count; i++)
            graphPoints[i].SetActive(!graphPoints[i].activeSelf);
    }
    #endregion
}
