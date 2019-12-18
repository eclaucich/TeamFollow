[System.Serializable]
public class SaveDataPartido
{
    public string nombre;

    public SaveDataPartido(Partido partido)
    {
        nombre = partido.GetNombre();
    }

    public string GetNombre()
    {
        return nombre;
    }
}
