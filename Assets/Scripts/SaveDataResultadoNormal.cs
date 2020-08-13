[System.Serializable]
public class SaveDataResultadoNormal
{
    public int valorPropio;
    public int valorContrario;

    public bool isPenales;
    public int valorPenalesPropio;
    public int valorPenalesContrario;

    public SaveDataResultadoNormal(ResultadoNormal _res)
    {
        valorPropio = _res.GetResultadoPropio();
        valorContrario = _res.GetResultadoContrario();

        isPenales = _res.IsPenales();

        valorPenalesPropio = _res.GetResultadoPenalesPropio();
        valorPenalesContrario = _res.GetResultadoPenalesContrario();
    }

    #region Getters
    public int GetResultadoPropio()
    {
        return valorPropio;
    }
    public int GetResultadoContrario()
    {
        return valorContrario;
    }
    public int GetResultadoPenalesPropio()
    {
        return valorPenalesPropio;
    }
    public int GetResultadoPenalesContrario()
    {
        return valorPenalesContrario;
    }
    public bool IsPenales()
    {
        return isPenales;
    }
    #endregion
}
