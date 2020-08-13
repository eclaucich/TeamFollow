using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;

public abstract class Grafica : MonoBehaviour
{
    [SerializeField] protected RectTransform rectGraficoEstadistica = null;
    [SerializeField] protected RectTransform rectOpciones = null;
    [SerializeField] protected RectTransform rectEjeX = null;
    [SerializeField] protected RectTransform rectEjeY = null;

    [SerializeField] protected GameObject opcionFuncionHistograma = null;

    [SerializeField] protected GameObject botonDatoGraficaPrefab = null;
    [SerializeField] protected Transform datosTransform = null;

    protected int cantMinima;
    protected float prefabHeight;
    [SerializeField] protected ScrollRect scrollRect = null;
    [SerializeField] protected FlechasScroll flechasScroll = null;


    protected int xi, xf, yi, yf;
    protected int h, w;

    public bool vertical = false;

    protected struct infoDatos
    {
        public int min, minIndex, max, maxIndex;
        public float median;
    }

    protected infoDatos info;
    protected int[] valores;

    protected bool debug = false;
    protected int cantidadDatos = 50;

    protected virtual void Awake()
    {
        if(vertical) prefabHeight = botonDatoGraficaPrefab.GetComponent<RectTransform>().rect.height;
    }

    protected void FixedUpdate()
    {
        if (vertical) flechasScroll.Actualizar(scrollRect, cantMinima, cantidadDatos);
    }

    public virtual void Graficar<T>(Dictionary<T, int> datos)
    {
        ResetGraph();
        SetSizeVariables();

        if (debug)
        {
            valores = new int[cantidadDatos];

            for (int i = 0; i < cantidadDatos; i++)
                valores[i] = Random.Range(10, 200);

            info = SetInfoStruct(valores);
        }
        else
        {
            cantidadDatos = datos.Count;
        }

    }

    public abstract void CrearPrefabs<T>(Dictionary<T, int> datos, bool isDatoPartido);
    public void BorrarPrefabs()
    {
        for (int i = 1; i < datosTransform.childCount; i++)
        {
            Destroy(datosTransform.GetChild(i).gameObject);
        }
    }
    public abstract void ResetGraph();
    protected void SetSizeVariables()
    {
        xi = 20; //xf = (int)GetComponent<RectTransform>().rect.height;
        yi = 20; //yf = (int)GetComponent<RectTransform>().rect.width-220;

        if (vertical)
        {
            w = (int)rectGraficoEstadistica.rect.width;//Screen.currentResolution.width;
            h = (int)rectGraficoEstadistica.rect.height;// Screen.currentResolution.height;

            xf = (int)(w - rectEjeY.rect.width);
            yf = (int)(h - rectOpciones.rect.height - (1.3 * rectEjeX.rect.height));
        }
        else
        {
            w = (int)rectGraficoEstadistica.rect.height;//Screen.currentResolution.width;
            h = (int)rectGraficoEstadistica.rect.width;// Screen.currentResolution.height;

            xf = (int)(w - rectEjeY.rect.width);
            yf = (int)(h - rectOpciones.rect.height - (1.3 * rectEjeX.rect.height));
        }

        if (h > w)
        {
            int aux = h; h = w; w = aux;
        }

        
    }
    protected float MapYValue(float value)
    {
        float prct=0f;
        if (info.max > 0 && info.max>info.min)
        {
            prct = (value - info.min) / (info.max - info.min);
        }
        return (prct * (yf - yi) + yi);
    }
    protected infoDatos SetInfoStruct(int[] datos)
    {
        int maxV = -1, maxI = -1, minV = -1, minI = -1;
        int index = 0, acum = 0;
        foreach (var dato in datos)
        {
            if (maxV == -1)
            {
                maxV = dato;
                maxI = index;
            }
            else
            {
                if (dato > maxV)
                {
                    maxV = dato;
                    maxI = index;
                }
            }
            if (minV == -1)
            {
                minV = dato;
                minI = index;
            }
            else
            {
                if (dato < minV)
                {
                    minV = dato;
                    minI = index;
                }
            }
            acum += dato;
            index++;
        }

        infoDatos info = new infoDatos
        {
            min = minV,
            minIndex = minI,
            max = maxV,
            maxIndex = maxI,
            median = (float)acum / datos.Length
        };

        return info;
    }
}
