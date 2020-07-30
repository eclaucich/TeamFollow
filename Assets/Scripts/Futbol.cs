
public class Futbol : Deporte
{

    private void Start()
    {
        deporte = Deportes.DeporteEnum.Futbol;
        estadisticas = new EstadisticasFutbol();
    }

    public override string GetName(AppController.Idiomas _idioma)
    {
        if (_idioma == AppController.Idiomas.Español)
            return "Futbol";
        else if (_idioma == AppController.Idiomas.Ingles)
            return "Football";
        else
            return "ERROR FUTBOL";
    }

    public override string[] GetStatisticsName(int i, AppController.Idiomas _idioma)
    {
        return estadisticas.GetStatisticsName(i, _idioma);
    }
}
