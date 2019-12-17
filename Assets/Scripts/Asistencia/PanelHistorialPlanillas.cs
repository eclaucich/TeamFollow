using System.Collections.Generic;
using UnityEngine;

public class PanelHistorialPlanillas : Panel {

    [SerializeField] private ConfirmacionBorradoAsistencia confirmacionBorradoAsistencia = null;

    [SerializeField] private GameObject botonHistorialPrefab = null;
    private List<GameObject> listaBotonHistorial;

    [SerializeField] private GameObject seccionAdvice = null;

    private Equipo equipo;

    private Transform parentTransform;


    public void Awake()
    {
        listaBotonHistorial = new List<GameObject>();
        parentTransform = GameObject.Find("SeccionHistorialAsistencias").transform;
    }

    public void SetPanelHistorialPlanillas()
    {
        equipo = AppController.instance.equipoActual;

        BorrarPrefabs();
        CrearPrefabs();

        ActivarYDesactivarAdviceText();
    }

    public void BorrarPrefabs()
    {
        for (int i = 0; i < listaBotonHistorial.Count; i++)
        {
            Destroy(listaBotonHistorial[i]);
        }
        listaBotonHistorial.Clear();
    }

    public void CrearPrefabs()
    {
        for (int i = 0; i < equipo.planillasAsistencia.Count; i++)
        {
            GameObject botonGO = Instantiate(botonHistorialPrefab, parentTransform, false);

            string nombrePlanilla = "";

            Dictionary<string, List<DetalleAsistencia>>.KeyCollection keys = equipo.planillasAsistencia.Keys;
            int aux = 0;
            foreach (string s in keys)
            {
                if(aux == i)
                {
                    nombrePlanilla = s;
                    break;
                }
                aux++;
            }

            botonGO.GetComponent<BotonHistorialAsistencia>().SetBotonHistorialAsistencia(nombrePlanilla);

            listaBotonHistorial.Add(botonGO);
        }
    }

    public void BorrarAsistencia(GameObject botonFocus, string nombreAsistencia)
    {
        confirmacionBorradoAsistencia.Activar(botonFocus, nombreAsistencia);
    }

    public void BorrarPlanilla(GameObject planillaFocus, string nombrePlanilla)
    {
        Destroy(planillaFocus);
        listaBotonHistorial.Remove(planillaFocus);
        AppController.instance.equipoActual.BorrarAsistencia(nombrePlanilla);
        ActivarYDesactivarAdviceText();
    }

    private void ActivarYDesactivarAdviceText()
    {
        if (listaBotonHistorial.Count == 0) seccionAdvice.SetActive(true);
        else                                seccionAdvice.SetActive(false);
    }
}
