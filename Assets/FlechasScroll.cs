using UnityEngine;
using UnityEngine.UI;

public class FlechasScroll : MonoBehaviour
{
    [SerializeField] private GameObject flechaArriba = null;
    [SerializeField] private GameObject flechaAbajo = null;

    public void Actualizar(ScrollRect scrollRect, int top, int childCount)
    {
        if (childCount < top)
        {
            scrollRect.enabled = false;
            Abajo(false);
            Arriba(false);
        }
        else
        {
            scrollRect.enabled = true;

            if (scrollRect.verticalNormalizedPosition > .95f) Arriba(false); else Arriba(true);
            if (scrollRect.verticalNormalizedPosition < 0.05f) Abajo(false); else Abajo(true);
        }
    }

    private void Arriba(bool _aux)
    {
        flechaArriba.SetActive(_aux);
    }

    private void Abajo(bool _aux)
    {
        flechaAbajo.SetActive(_aux);
    }
}
