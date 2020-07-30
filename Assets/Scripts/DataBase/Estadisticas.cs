using System;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 
/// Clase base para el manejo de estadísticas, tanto de equipos como de jugadores
/// 
/// </summary>

public class Estadisticas {

    //Parametros a llevar por las estadísticas
    protected Dictionary<string, int> listaEstadisticas;
    protected DateTime fecha;
    protected Deportes.DeporteEnum deporte;

    public Estadisticas(Deportes.DeporteEnum _deporte)                                                       
    {
        listaEstadisticas = new Dictionary<string, int>();
        deporte = _deporte;
    }

    public Estadisticas(SaveDataEstadisticas saveData)
    {
        //A partir de saveData, extraer los dos arrays publicos
        //y crear el diccionario (listaEstadisticas) con ellos
        deporte = saveData.deporte;
        listaEstadisticas = new Dictionary<string, int>();

        List<string> keys = saveData.nombresCategorias;
        List<int> values = saveData.valoresCategorias;

        for (int i = 0; i < keys.Count; i++)
        {
            listaEstadisticas[keys[i]] = values[i];
        }
    }

    public void SetFecha(DateTime _fecha)
    {
        fecha = _fecha;
    }

    public DateTime GetFecha()
    {
        return fecha;
    }

    public void AgregarEstadisticas(string categoria, int cantidad)                  //Suma, a los campos actuales, los campos de otra estadística
    {
        if (!listaEstadisticas.ContainsKey(categoria)) listaEstadisticas[categoria] = cantidad;      //si el campo nunca fue seteado, inicializarlo
        else                                           listaEstadisticas[categoria] += cantidad;     //si ya existe el campo, se le agregan "cantidad"
    }

    public void AgregarEstadisticas(Estadisticas estadisticas_)
    {
        foreach (var item in estadisticas_.GetDictionary())
        {
            if (listaEstadisticas.ContainsKey(item.Key))
            {
                listaEstadisticas[item.Key] += item.Value;
            }
            else
            {
                listaEstadisticas[item.Key] = item.Value;
            }
        }
    }

    public void BorrarEstadisticas(Estadisticas estadisticas_)
    {
        foreach (var item in estadisticas_.GetDictionary())
        {
            if (listaEstadisticas.ContainsKey(item.Key))
            {
                listaEstadisticas[item.Key] -= item.Value;
            }
            else
            {
                listaEstadisticas[item.Key] = 0;
            }
        }
    }

    public Dictionary<string, int> GetDictionary()
    {
        return listaEstadisticas;
    }

    public int GetCantidadCategorias()
    {
        return listaEstadisticas.Count;
    }

    public string GetKeyAtIndex(int index)
    {
        int i = 0;
        foreach (var item in listaEstadisticas.Keys)
        {
            if(i == index)
            {
                return item;
            }
            i++;
        }

        return null;
    }

    public int GetValueAtIndex(int index)
    {
        int i = 0;
        int value = 0;
        foreach (var item in listaEstadisticas.Values)
        {
            if (i == index)
            {
                value = item;
                break;
            }
            i++;
        }

        return value;
    }

    public int[] Find(string _key) //Busca si la estadistica "_key" esta en el diccionario. Devuelve en [0] si la encontró (1) o no (0), y en [1] el valor de esa estadistica
    {
        int[] result = new int[2];
        bool aux = false;

        foreach (var est in listaEstadisticas)
        {
            if (est.Key == _key)
            {
                result[0] = 1;
                result[1] = est.Value;
                aux = true;
                break;
            }
        }
        if (!aux)
        {
            result[0] = 0;
            result[1] = -1;
        }

        return result;
    }

    public bool isEmpty()
    {
        return listaEstadisticas == null || listaEstadisticas.Keys.Count == 0;
    }

    public EstadisticaDeporte GetEstadisticaDeporte()
    {
        return Deportes.instance.GetEstadisticaDeporte(deporte);
    }

    public Deportes.DeporteEnum GetDeporte()
    {
        return deporte;
    }
}
