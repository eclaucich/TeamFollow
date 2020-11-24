[System.Serializable]
public class SaveDataSettings
{
    public AppController.Idiomas idioma;
    public AppController.Temas tema;
    public Deportes.DeporteEnum deporteFavorito;

    public SaveDataSettings(AppController.Idiomas _idioma, AppController.Temas _tema, Deportes.DeporteEnum _deporteFavorito)
    {
        idioma = _idioma;
        tema = _tema;
        deporteFavorito = _deporteFavorito;
    }
}
