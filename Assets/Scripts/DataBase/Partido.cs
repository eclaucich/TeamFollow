public class Partido
{
    private string nombre;
    private Estadisticas estadisticas;

    public Partido(string _nombre, Estadisticas _estadisticas)
    {
        nombre = _nombre;
        estadisticas = _estadisticas;
    }

    public Partido(SaveDataPartido dataPartido, SaveDataEstadisticas dataEstadisticas)
    {
        nombre = dataPartido.GetNombre();
        estadisticas = new Estadisticas(dataEstadisticas);
    }

    public string GetNombre()
    {
        return nombre;
    }

    public Estadisticas GetEstadisticas()
    {
        return estadisticas;
    }

    public void BorrarEstadistica(Estadisticas estadisticas_)
    {
        estadisticas.BorrarEstadisticas(estadisticas_);
    }
}
