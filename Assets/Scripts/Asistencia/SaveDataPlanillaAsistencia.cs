[System.Serializable]
public class SaveDataPlanillaAsistencia
{
    public string nombre;
    public string alias;
    
    public SaveDataPlanillaAsistencia(string nombre_, string alias_)
    {
        nombre = nombre_;
        alias = alias_;
    }

    public SaveDataPlanillaAsistencia(PlanillaAsistencia planilla)
    {
        nombre = planilla.GetNombre();
        alias = planilla.GetAlias();
    }

    public string GetNombre()
    {
        return nombre;
    }

    public string GetAlias()
    {
        return alias;
    }
}
