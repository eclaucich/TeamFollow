
using UnityEngine;

public abstract class Deporte : MonoBehaviour
{
    protected Deportes.DeporteEnum deporte;
    protected EstadisticaDeporte estadisticas;
    public Deporte instance = null;

    private void Awake()
    {
        if (instance == null)                                                  
            instance = this;
        else
            Destroy(instance);

        DontDestroyOnLoad(this);
    }

    public abstract string GetName(AppController.Idiomas _idioma);
    public abstract string[] GetStatisticsName(int i, AppController.Idiomas _idioma);
}
