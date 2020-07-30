using UnityEngine;

public abstract class EstadisticaDeporte : MonoBehaviour
{
    public abstract string[] GetStatisticsName(int i, AppController.Idiomas idioma);
    public abstract int GetSize();
    public abstract string GetValueAtIndex(int i);
}
