using System;
using System.Collections.Generic;

[System.Serializable]
public class SaveDataEstadisticas {

    private Dictionary<string, int> estadisticas;

    public List<string> nombresCategorias;
    public List<int> valoresCategorias;
    public DateTime fecha;

    public SaveDataEstadisticas(Estadisticas estadisticas_)
    {
        estadisticas = estadisticas_.GetDictionary();

        nombresCategorias = new List<string>();
        valoresCategorias = new List<int>();

        //Separar el diccionario en un array de strings (keys) y un array de int (values)
        List<string> keys = ListaKeys(estadisticas.Keys);
        List<int> values = ListaValues(estadisticas.Values);

        for (int i = 0; i < keys.Count; i++)
        {
            nombresCategorias.Add(keys[i]);
            valoresCategorias.Add(values[i]);
        }

        fecha = estadisticas_.GetFecha();
    }


    private List<string> ListaKeys(Dictionary<string, int>.KeyCollection keys)
    {
        List<string> lista = new List<string>();
        foreach (var item in keys)
        {
            lista.Add(item);
        }

        return lista;
    }

    private List<int> ListaValues(Dictionary<string, int>.ValueCollection values)
    {
        List<int> lista = new List<int>();
        foreach (var item in values)
        {
            lista.Add(item);
        }

        return lista;
    }

}
