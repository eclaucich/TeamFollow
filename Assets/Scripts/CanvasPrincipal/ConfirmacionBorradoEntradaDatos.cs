using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmacionBorradoEntradaDatos : MonoBehaviour
{
    [SerializeField] private EntradaDatos panelEntradaDatos = null;

    public void Desplegar()
    {
        gameObject.SetActive(true);
    }

    public void AceptarGuardado()
    {
        //gameObject.SetActive(false);
        panelEntradaDatos.GuardarEntradaDatos();
    }

    public void CancelarGuardado()
    {
        panelEntradaDatos.CancelarGuardado();
        //CanvasController.instance.escenas.Add(6);
    }

    public void DescartarGuardado()
    {
        gameObject.SetActive(false);
        panelEntradaDatos.DescartarDatos();
    }
}
