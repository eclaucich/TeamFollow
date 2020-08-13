using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeccionCancha : MonoBehaviour
{
    [SerializeField] private Image imagenCancha = null;
    [SerializeField] private Image contorno = null;

    private Deportes.DeporteEnum deporte;
    
    public void SetSeccionCancha()
    {
        contorno.gameObject.SetActive(false);

        deporte = AppController.instance.equipoActual.GetDeporte();

        imagenCancha.sprite =  Deportes.instance.GetImagenCancha(deporte, Deportes.TipoCanchasEnum.CanchaEntera);
    }

    public void SetActiveContorno(bool _aux)
    {
        contorno.gameObject.SetActive(_aux);
    }
}
