using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelHistorialPlanillas : Panel {

    [SerializeField] private ConfirmacionBorradoAsistencia confirmacionBorradoAsistencia = null;

    [SerializeField] private GameObject botonHistorialPrefab = null;
    private List<GameObject> listaBotonHistorial;

    [SerializeField] private GameObject seccionAdvice = null;

    [SerializeField] private MensajeError mensajeError = null;

    [SerializeField] private ScrollRect scrollRect = null;
    [SerializeField] private FlechasScroll flechasScroll = null;

    private Equipo equipo;

    private Transform parentTransform;

    private float prefabHeight;
    private int cantMinima;

    public void Awake()
    {
        listaBotonHistorial = new List<GameObject>();
        parentTransform = GameObject.Find("SeccionHistorialAsistencias").transform;
        prefabHeight = botonHistorialPrefab.GetComponent<RectTransform>().rect.height;
    }

    private void FixedUpdate()
    {
        flechasScroll.Actualizar(scrollRect, cantMinima, listaBotonHistorial.Count);
    }

    public void SetPanelHistorialPlanillas()
    {
        equipo = AppController.instance.equipoActual;

        AppController.instance.overlayPanel.SetNombrePanel(equipo.GetNombre() +  ": ASISTENCIAS", AppController.Idiomas.Español);
        AppController.instance.overlayPanel.SetNombrePanel(equipo.GetNombre() + ": ASSISTS", AppController.Idiomas.Ingles);

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

        cantMinima = (int)(scrollRect.GetComponent<RectTransform>().rect.height / (prefabHeight + parentTransform.GetComponent<VerticalLayoutGroup>().spacing));
    }

    public void NuevaPlanilla()
    {
        if(AppController.instance.equipoActual.GetJugadores().Count == 0)
        {
            mensajeError.SetText(("No hay jugadores en este equipo").ToUpper(), AppController.Idiomas.Español);
            mensajeError.SetText(("There are no players in this team").ToUpper(), AppController.Idiomas.Ingles);
            mensajeError.Activar();
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
