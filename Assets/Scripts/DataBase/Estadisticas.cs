using System.Collections.Generic;
/// <summary>
/// 
/// Clase base para el manejo de estadísticas, tanto de equipos como de jugadores
/// 
/// </summary>

public class Estadisticas {

    //Parametros a llevar por las estadísticas
    protected Dictionary<string, int> listaEstadisticas; 

    public Estadisticas()                                                       //Constructor por defecto, todos los campos en 0
    {
        listaEstadisticas = new Dictionary<string, int>();
    }

    public Estadisticas(SaveDataEstadisticas saveData)
    {
        //A partir de saveData, extraer los dos arrays publicos
        //y crear el diccionario (listaEstadisticas) con ellos
        listaEstadisticas = new Dictionary<string, int>();

        List<string> keys = saveData.nombresCategorias;
        List<int> values = saveData.valoresCategorias;

        for (int i = 0; i < keys.Count; i++)
        {
            listaEstadisticas[keys[i]] = values[i];
        }
    }

    public void AgregarEstadisticas(string categoria, int cantidad)                  //Suma a los campos actuales, los campos de otra estadística
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

}
