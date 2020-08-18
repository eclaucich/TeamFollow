[System.Serializable]

public class SaveDataEvento
{
    public EstadisticaDeporte.Estadisticas tipoEstadistica;
    public string nombreAutor;
    public int period;
    public float tiempo;

    public SaveDataEvento(Evento _evento)
    {
        tipoEstadistica = _evento.GetTipoEstadistica();
        nombreAutor = _evento.GetAutor().GetNombre();
        period = _evento.GetPeriod();
        tiempo = _evento.GetTiempo();
    }
}
