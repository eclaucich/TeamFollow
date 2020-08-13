using System.Collections.Generic;

[System.Serializable]
public class SaveDataJugador {

    public Deportes.DeporteEnum deporte;

    public int fechaNacYear;
    public int fechaNacMonth;
    public int fechaNacDay;

    public List<string> catObligatoria;
    public List<string> valObligatoria;

    public List<string> catString;
    public List<string> valString;

    public List<string> catInt;
    public List<string> valInt;

    public List<string> catEspecial;
    public List<string> valEspecial;

    public SaveDataJugador(InfoJugador infoJugador_, Deportes.DeporteEnum deporte_)
    {
        deporte = deporte_;

        catObligatoria = ListaKeysString(infoJugador_.GetInfoObligatoria().Keys);
        valObligatoria = ListaValuesString(infoJugador_.GetInfoObligatoria().Values);

        catString = ListaKeysString(infoJugador_.GetInfoString().Keys);
        valString = ListaValuesString(infoJugador_.GetInfoString().Values);

        catInt = ListaKeysInt(infoJugador_.GetInfoInt().Keys);
        valInt = ListaValuesInt(infoJugador_.GetInfoInt().Values);

        catEspecial = ListaKeysString(infoJugador_.GetInfoEspecial().Keys);
        valEspecial = ListaValuesString(infoJugador_.GetInfoEspecial().Values);

        fechaNacYear = infoJugador_.GetFechaNac().Year;
        fechaNacMonth = infoJugador_.GetFechaNac().Month;
        fechaNacDay = infoJugador_.GetFechaNac().Day;
    }

    public Dictionary<string, string> GetInfoObligatoria()
    {
        Dictionary<string, string> dict = new Dictionary<string, string>();

        for (int i = 0; i < catObligatoria.Count; i++)
        {
            dict[catObligatoria[i]] = valObligatoria[i];
        }

        return dict;
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

    public Dictionary<string, string> GetInfoInt()
    {
        Dictionary<string, string> dict = new Dictionary<string, string>();

        for (int i = 0; i < catInt.Count; i++)
        {
            dict[catInt[i]] = valInt[i];
        }

        return dict;
    }

    public Dictionary<string, string> GetInfoEspecial()
    {
        Dictionary<string, string> dict = new Dictionary<string, string>();

        for (int i = 0; i < catEspecial.Count; i++)
        {
            dict[catEspecial[i]] = valEspecial[i];
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

    private List<string> ListaKeysInt(Dictionary<string, string>.KeyCollection keys)
    {
        List<string> lista = new List<string>();
        foreach (var item in keys)
        {
            lista.Add(item);
        }

        return lista;
    }

    private List<string> ListaValuesInt(Dictionary<string, string>.ValueCollection values)
    {
        List<string> lista = new List<string>();
        foreach (var item in values)
        {
            lista.Add(item);
        }

        return lista;
    }
}
