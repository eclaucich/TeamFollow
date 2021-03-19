using UnityEngine;

public class Evento
{
    private EstadisticaDeporte.Estadisticas tipoEstadistica;
    private Jugador autor;
    private int period;
    private float tiempo;
    private Sprite sprite;

    public Evento(EstadisticaDeporte.Estadisticas _tipoEstadistica, Jugador _autor, int _period, float _tiempo)
    {
        tipoEstadistica = _tipoEstadistica;
        autor = _autor;
        period = _period;
        tiempo = _tiempo;
    }

    public Evento(SaveDataEvento _saveData, Equipo _equipo)
    {
        tipoEstadistica = _saveData.tipoEstadistica;
        autor = _equipo.BuscarPorNombre(_saveData.nombreAutor);
        tiempo = _saveData.tiempo;
        period = _saveData.period;
    }

    public Evento(SaveDataEvento _saveData, Jugador _jugador)
    {
        tipoEstadistica = _saveData.tipoEstadistica;
        autor = _jugador;
        tiempo = _saveData.tiempo;
        period = _saveData.period;
    }

    public EstadisticaDeporte.Estadisticas GetTipoEstadistica()
    {
        return tipoEstadistica;
    }
    public string GetNombreTipo()
    {
        Debug.Log(tipoEstadistica + ": " + EstadisticasDeporteDisplay.GetStatisticsName(tipoEstadistica, AppController.instance.idioma)[0].ToUpper());
        return EstadisticasDeporteDisplay.GetStatisticsName(tipoEstadistica, AppController.instance.idioma)[0].ToUpper();
    }
    public string GetInicialTipo()
    {
        return EstadisticasDeporteDisplay.GetStatisticsName(tipoEstadistica, AppController.instance.idioma)[1].ToUpper();
    }

    public Jugador GetAutor()
    {
        return autor;
    }
    public int GetPeriod()
    {
        return period;
    }
    public float GetTiempo()
    {
        return tiempo;
    }
}
