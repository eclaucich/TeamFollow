using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PanelEstadisticasGlobalesEquipo : Panel {

    [SerializeField] private Text nombreEquipoText = null;
    [SerializeField] private EstadisticasGlobalesEquipo estadisticasGlobales = null;
    [SerializeField] private GameObject estadisticaPrefab = null;
    [SerializeField] private ConfirmacionBorradoPartido confirmacionBorradoPartido = null;

    protected Dictionary<string, int> diccEstadisticas;

    private Estadisticas estadisticas;
    private Equipo equipo;

    //private bool tipoEstadisticasPartido = true;                                       //true="Partido", false="Practica"
 
    private Transform parentTransform;
    private List<GameObject> listaPrefabsTextos;

    private BotonPartido botonFocus;

    [SerializeField] private GameObject botonBorrar;

    public override void Start()
    {
        base.Start();
        diccEstadisticas = new Dictionary<string, int>();
    }


    public void SetPanelEstadisticasGlobalesEquipo(BotonPartido _botonPartido, Estadisticas _estadisticas)
    {
        if (_botonPartido == null) botonBorrar.SetActive(false); else botonBorrar.SetActive(true);
        botonFocus = _botonPartido;

        CanvasController.instance.AgregarPanelAnterior(CanvasController.Paneles.EstadisticasGlobalesEquipo);

        if (listaPrefabsTextos == null) listaPrefabsTextos = new List<GameObject>();

        equipo = AppController.instance.GetEquipoActual();
        nombreEquipoText.text = equipo.GetNombre();

        estadisticas = _estadisticas;
        
        Debug.Log("ESTADISTICAS: " + _estadisticas.GetDictionary() == null);

        /*diccEstadisticas = new Dictionary<string, int>();
        diccEstadisticas = estadisticas.GetDictionary();

        Debug.Log("DICC ESTADISTICAS: " + diccEstadisticas == null);
        */

        parentTransform = estadisticasGlobales.SetPanelEstadisticas();

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

    public void ActivarPanelConfirmacionBorrado()
    {
        confirmacionBorradoPartido.ActivarPanel(botonFocus);
    }
}
