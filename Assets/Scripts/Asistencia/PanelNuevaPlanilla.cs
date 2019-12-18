﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelNuevaPlanilla : Panel {

    [SerializeField] private GameObject detalleAsistenciaPrefab = null;

    [SerializeField] private Text dayInput = null;
    [SerializeField] private Text monthInput = null;
    [SerializeField] private Text yearInput = null;
    [SerializeField] private Text hourInput = null;
    [SerializeField] private Text minuteInput = null;

    [SerializeField] private Button botonGuardar = null;

    [SerializeField] private GameObject seccionError = null;
    [SerializeField] private GameObject seccionAdvice = null;

    private List<GameObject> asistencias;
    private List<DetalleAsistencia> listaDetallesAsistencias;

    private Equipo equipo;
    private List<Jugador> jugadores;

    private Transform parentTransform;

    private void Awake()
    {
        jugadores = new List<Jugador>();
        asistencias = new List<GameObject>();
        listaDetallesAsistencias = new List<DetalleAsistencia>();
        parentTransform = GameObject.Find("SeccionAsistencias").transform;        
    }

    public void SetPanelNuevaPlanilla()
    {
        if (AppController.instance.GetEquipoActual() != equipo)
            equipo = AppController.instance.equipoActual;

        jugadores = equipo.GetJugadores();

        seccionError.SetActive(false);
        if (AppController.instance.equipoActual.GetJugadores().Count == 0)
        {
            seccionAdvice.SetActive(true);
            botonGuardar.interactable = false;
        }
        else
        {
            seccionAdvice.SetActive(false);
            botonGuardar.interactable = true;
        }

        BorrarPrefabs();
        CrearPrefabs();   
    }

    public void CrearPrefabs()
    {
        for (int i = 0; i < jugadores.Count; i++)
        {
            GameObject detalleAsistenciaGO = Instantiate(detalleAsistenciaPrefab, parentTransform, false);
            asistencias.Add(detalleAsistenciaGO);

            listaDetallesAsistencias.Add(asistencias[i].GetComponent<DetalleAsistencia>());
            listaDetallesAsistencias[i].SetNombreJugador(jugadores[i].GetNombre());
        }
    }

    public void BorrarPrefabs()
    {
        if (asistencias == null) return;

        for (int i = 0; i < asistencias.Count; i++)
        {
            Destroy(asistencias[i]);
        }
        asistencias.Clear();
        listaDetallesAsistencias.Clear();
    }

    public void GuardarPlanilla()
    {
        string nombrePlanilla = dayInput.text + "-" + monthInput.text + "-" + yearInput.text + " - " + hourInput.text + "-" + minuteInput.text;

        if(equipo.ExistePlanilla(nombrePlanilla))
        {
            seccionError.SetActive(true);
            seccionError.GetComponentInChildren<Text>().text = "Planilla Existente!";
            return;
        }

        equipo.NuevaPlanilla(nombrePlanilla, listaDetallesAsistencias);

        GetComponentInParent<PanelPlanillaAsistencia>().MostrarPanelPrincipal();
    }
}
