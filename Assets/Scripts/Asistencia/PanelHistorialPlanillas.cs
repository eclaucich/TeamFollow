using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelHistorialPlanillas : Panel {

    [SerializeField] private ConfirmacionBorradoAsistencia confirmacionBorradoAsistencia = null;

    [SerializeField] private GameObject botonHistorialPrefab = null;
    private List<GameObject> listaBotonHistorial;

    [SerializeField] private GameObject seccionAdvice = null;

    [SerializeField] private GameObject mensajeError = null;

    [SerializeField] private ScrollRect scrollRect = null;
    [SerializeField] private GameObject flechaArriba = null;
    [SerializeField] private GameObject flechaAbajo = null;

    private Equipo equipo;

    private Transform parentTransform;

    public void Awake()
    {
        listaBotonHistorial = new List<GameObject>();
        parentTransform = GameObject.Find("SeccionHistorialAsistencias").transform;
    }

    private void FixedUpdate()
    {
        if (parentTransform.childCount < 8)
        {
            scrollRect.enabled = false;
            flechaAbajo.SetActive(false);
            flechaArriba.SetActive(false);
        }
        else
        {
            scrollRect.enabled = true;

            if (scrollRect.verticalNormalizedPosition > .95f) flechaArriba.SetActive(false); else flechaArriba.SetActive(true);
            if (scrollRect.verticalNormalizedPosition < 0.05f) flechaAbajo.SetActive(false); else flechaAbajo.SetActive(true);
        }
    }

    public void SetPanelHistorialPlanillas()
    {
        mensajeError.SetActive(false);

        AppController.instance.overlayPanel.SetNombrePanel("ASISTENCIAS");

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
        /*for (int i = 0; i < equipo.planillasAsistencia.Count; i++)
        {
            GameObject botonGO = Instantiate(botonHistorialPrefab, parentTransform, false);
            botonGO.SetActive(true);

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
        }*/

        for(int i = 0; i < equipo.GetPlanillasAsistencia().Count; i++)
        {
            GameObject botonGO = Instantiate(botonHistorialPrefab, parentTransform, false);
            botonGO.SetActive(true);

            PlanillaAsistencia planilla = equipo.GetPlanillaAtIndex(i);
            string nombrePlanilla = planilla.GetNombre();
            string aliasPlanilla = planilla.GetAlias();
            botonGO.GetComponent<BotonHistorialAsistencia>().SetBotonHistorialAsistencia(nombrePlanilla, aliasPlanilla);

            listaBotonHistorial.Add(botonGO);
        }
    }

    public void NuevaPlanilla()
    {
        if(AppController.instance.equipoActual.GetJugadores().Count == 0)
        {
            mensajeError.SetActive(true);
            return;
        }

        GetComponentInParent<PanelPlanillaAsistencia>().MostrarPanelNuevaPlanilla();
    }

    public void BorrarPlanilla(BotonHistorialAsistencia botonFocus)
    {
        Destroy(botonFocus.gameObject);
        listaBotonHistorial.Remove(botonFocus.gameObject);
        ActivarYDesactivarAdviceText();
    }

    private void ActivarYDesactivarAdviceText()
    {
        if (listaBotonHistorial.Count == 0) seccionAdvice.SetActive(true);
        else                                seccionAdvice.SetActive(false);
    }
}
