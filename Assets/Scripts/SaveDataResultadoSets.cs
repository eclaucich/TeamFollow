using System.Collections.Generic;

[System.Serializable]

public class SaveDataResultadoSets
{
    public List<SaveDataSet> listaSets;

    public SaveDataResultadoSets(ResultadoSets _res)
    {
        List<SetPrefab> _list = _res.GetListaSets();
        listaSets = new List<SaveDataSet>();

        foreach (var set in _list)
        {
            listaSets.Add(new SaveDataSet(set));
        }
    }
}
