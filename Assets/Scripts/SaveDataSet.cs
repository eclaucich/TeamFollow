[System.Serializable]
public class SaveDataSet
{
    public int valorPropio;
    public int valorContrario;

    public bool isTieBreak;
    public int valorTBPropio;
    public int valorTBContrario;

    public SaveDataSet(SetPrefab _set)
    {
        valorPropio = _set.GetResultadoPropio();
        valorContrario = _set.GetResultadoContrario();

        isTieBreak = _set.IsTieBreak();

        valorTBPropio = _set.GetResultaoTBPropio();
        valorTBContrario = _set.GetResultadoTBContrario();
    }
}
