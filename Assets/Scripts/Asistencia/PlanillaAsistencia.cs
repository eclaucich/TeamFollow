using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanillaAsistencia
{
    private string nombre;
    private string alias;
    public List<DetalleAsistencia> detalles;

    public PlanillaAsistencia(SaveDataPlanillaAsistencia dataPlanillaAsistencia)
    {
        nombre = dataPlanillaAsistencia.GetNombre();
        alias = dataPlanillaAsistencia.GetAlias();
        detalles = new List<DetalleAsistencia>();
    }

    public PlanillaAsistencia(string nombre_, string alias_, List<DetalleAsistencia> detalles_)
    {
        nombre = nombre_;
        alias = alias_;
        detalles = detalles_;
    }

    public void AgregarDetalle(DetalleAsistencia detalle)
    {
        detalles.Add(detalle);
    }

    public DetalleAsistencia GetDetalleAtIndex(int index)
    {
        return detalles[index];
    }

    public string GetNombre()
    {
        return nombre;
    }

    public string GetAlias()
    {
        return alias;
    }

    public List<DetalleAsistencia> GetDetalles()
    {
        return detalles;
    }

    
}
