using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelPlanilla : Panel {

    [SerializeField] private Text nombrePlanillaText = null;
    [SerializeField] private GameObject detalleAsistenciaPrefab = null;

    private List<GameObject> listaPrefabs;

    private Transform parentTransform;

    private void Awake()
    {
        listaPrefabs = new List<GameObject>();

        parentTransform = GameObject.Find("DetallesPlanilla").transform;
    }

    public void SetPanelPlanilla(string nombrePlanilla)
    {
        BorrarPrefabs();
        nombrePlanillaText.text = nombrePlanilla;

        List<DetalleAsistencia> detalles = AppController.instance.GetEquipoActual().planillasAsistencia[nombrePlanilla];

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
}
