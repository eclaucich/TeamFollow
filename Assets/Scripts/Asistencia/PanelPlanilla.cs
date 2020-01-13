using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelPlanilla : Panel {

    [SerializeField] private Text nombrePlanillaText = null;
    [SerializeField] private GameObject detalleAsistenciaPrefab = null;
    [SerializeField] private PanelHistorialPlanillas panelHistorialPlanillas = null;
    [SerializeField] private ConfirmacionBorradoAsistencia confirmacionBorradoAsistencia = null;

    private BotonHistorialAsistencia botonFocus;

    private List<GameObject> listaPrefabs;

    private Transform parentTransform;

    private void Awake()
    {
        listaPrefabs = new List<GameObject>();

        parentTransform = GameObject.Find("DetallesPlanilla").transform;
    }

    public void SetPanelPlanilla(BotonHistorialAsistencia botonFocus_)
    {
        BorrarPrefabs();

        if (botonFocus_ == null) Debug.Log("NULL");

        botonFocus = botonFocus_;

        nombrePlanillaText.text = botonFocus_.GetDisplayNombre();

        List<DetalleAsistencia> detalles = AppController.instance.GetEquipoActual().planillasAsistencia[botonFocus.GetNombre()];

        for (int i = 0; i < detalles.Count; i++)
        {
            GameObject detalleGO = Instantiate(detalleAsistenciaPrefab, parentTransform, false);

            detalleGO.GetComponent<DetalleAsistencia>().SetDetalle(detalles[i]);

            listaPrefabs.Add(detalleGO);
        }
    }

    public void BorrarPrefabs()
    {
        for (int i = 0; i < listaPrefabs.Count; i++)
        {
            Destroy(listaPrefabs[i]);
        }
        listaPrefabs.Clear();
    }

    public void BorrarAsistencia()
    {
        confirmacionBorradoAsistencia.Activar(botonFocus);
    }
}
