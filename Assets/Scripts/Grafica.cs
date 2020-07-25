using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Grafica : MonoBehaviour
{
    [SerializeField] protected RectTransform rectGraficoEstadistica = null;
    [SerializeField] protected RectTransform rectOpciones = null;
    [SerializeField] protected RectTransform rectEjeX = null;
    [SerializeField] protected RectTransform rectEjeY = null;

    protected int xi, xf, yi, yf;
    protected int h, w;

    protected struct infoDatos
    {
        public int min, minIndex, max, maxIndex;
        public float median;
    }

    protected infoDatos info;
    protected int[] valores;

    protected bool debug = true;
    protected int cantidadDatos = 10;

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

    }
    public abstract void ResetGraph();
    protected void SetSizeVariables()
    {
        xi = 20; //xf = (int)GetComponent<RectTransform>().rect.height;
        yi = 20; //yf = (int)GetComponent<RectTransform>().rect.width-220;

        w = (int)rectGraficoEstadistica.rect.width;//Screen.currentResolution.width;
        h = (int)rectGraficoEstadistica.rect.height;// Screen.currentResolution.height;

        if (h > w)
        {
            int aux = h; h = w; w = aux;
        }

        xf = (int)(w - rectEjeY.rect.width);
        yf = (int)(h - rectOpciones.rect.height - (1.3 * rectEjeX.rect.height));
    }
    protected float MapYValue(float value)
    {
        float prct = (value - info.min) / (info.max - info.min);
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
