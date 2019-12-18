using System.Collections.Generic;

[System.Serializable]
public class SaveDataJugador {

    public int fechaNacYear;
    public int fechaNacMonth;
    public int fechaNacDay;

    public List<string> catString;
    public List<string> valString;

    public List<string> catInt;
    public List<int> valInt;

    public SaveDataJugador(InfoJugador infoJugador_)
    {
        catString = ListaKeysString(infoJugador_.GetInfoString().Keys);
        valString = ListaValuesString(infoJugador_.GetInfoString().Values);

        catInt = ListaKeysInt(infoJugador_.GetInfoInt().Keys);
        valInt = ListaValuesInt(infoJugador_.GetInfoInt().Values);

        fechaNacYear = infoJugador_.GetFechaNac().Year;
        fechaNacMonth = infoJugador_.GetFechaNac().Month;
        fechaNacDay = infoJugador_.GetFechaNac().Day;
    }

    public Dictionary<string, string> GetInfoString()
    {
        Dictionary<string, string> dict = new Dictionary<string, string>();

        for (int i = 0; i < catString.Count; i++)
        {
            dict[catString[i]] = valString[i];
        }

        return dict;
    }

    public Dictionary<string, int> GetInfoInt()
    {
        Dictionary<string, int> dict = new Dictionary<string, int>();

        for (int i = 0; i < catInt.Count; i++)
        {
            dict[catInt[i]] = valInt[i];
        }

        return dict;
    }

    public System.DateTime GetFechaNacimiento()
    {
        return new System.DateTime(fechaNacYear, fechaNacMonth, fechaNacDay);
    }

    private List<string> ListaKeysString(Dictionary<string, string>.KeyCollection keys)
    {
        List<string> lista = new List<string>();
        foreach (var item in keys)
        {
            lista.Add(item);
        }

        return lista;
    }

    private List<string> ListaValuesString(Dictionary<string, string>.ValueCollection values)
    {
        List<string> lista = new List<string>();
        foreach (var item in values)
        {
            lista.Add(item);
        }

        return lista;
    }

    private List<string> ListaKeysInt(Dictionary<string, int>.KeyCollection keys)
    {
        List<string> lista = new List<string>();
        foreach (var item in keys)
        {
            lista.Add(item);
        }

        return lista;
    }

    private List<int> ListaValuesInt(Dictionary<string, int>.ValueCollection values)
    {
        List<int> lista = new List<int>();
        foreach (var item in values)
        {
            lista.Add(item);
        }

        return lista;
    }
}
