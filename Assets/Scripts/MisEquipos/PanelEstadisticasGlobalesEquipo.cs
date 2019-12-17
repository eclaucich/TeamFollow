using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PanelEstadisticasGlobalesEquipo : Panel {

    [SerializeField] private Text nombreEquipoText = null;
    [SerializeField] private EstadisticasGlobalesEquipo estadisticasGlobales = null;
    [SerializeField] private GameObject estadisticaPrefab = null;

    protected Dictionary<string, int> diccEstadisticas;

    private Estadisticas estadisticas;
    private Equipo equipo;

    //private bool tipoEstadisticasPartido = true;                                       //true="Partido", false="Practica"
 
    private Transform parentTransform;
    private List<GameObject> listaPrefabsTextos;


    public override void Start()
    {
        base.Start();
        diccEstadisticas = new Dictionary<string, int>();
    }


    public void SetPanelEstadisticasGlobalesEquipo(Estadisticas _estadisticas)
    {
        CanvasController.instance.AgregarPanelAnterior(CanvasController.Paneles.EstadisticasGlobalesEquipo);

        if (listaPrefabsTextos == null) listaPrefabsTextos = new List<GameObject>();

        equipo = AppController.instance.GetEquipoActual();
        nombreEquipoText.text = equipo.GetNombre();

        estadisticas = _estadisticas;

        diccEstadisticas = estadisticas.GetDictionary();

        parentTransform = estadisticasGlobales.Set();

        BorrarPrefabs();
        CrearPrefabs();
    }



    virtual public void CrearPrefabs()
    {
        for (int i = 0; i < estadisticas.GetCantidadCategorias(); i++)
        {
            //Debug.Log(estadisticas.GetKeyAtIndex(i) + "  " + estadisticas.GetValueAtIndex(i).ToString());
            GameObject estadisticaGO = Instantiate(estadisticaPrefab, parentTransform, false);
            Text[] textos = estadisticaGO.GetComponentsInChildren<Text>();
            textos[0].text = estadisticas.GetKeyAtIndex(i);
            textos[1].text = estadisticas.GetValueAtIndex(i).ToString();

            listaPrefabsTextos.Add(estadisticaGO);
        }
    }

    public void BorrarPrefabs()
    {
        for (int i = 0; i < listaPrefabsTextos.Count; i++)
        {
            Destroy(listaPrefabsTextos[i]);
        }
        listaPrefabsTextos.Clear();
    }
}
