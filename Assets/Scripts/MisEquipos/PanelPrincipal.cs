using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// Contiene un botón para crear nuevos equipos,
/// una sección donde se muestran los equipos existentes
/// 
/// </summary>

public class PanelPrincipal : Panel {

    private PanelMisEquipos panelMisEquipos;                                                    //Componente padre para acceder a las funciones

    ///Va a haber un prefab del boton por cada deporte
    ///Y una lista que los contenga a todos y cuando se crea el boton se crea el correspondiente al deporte
    [SerializeField] private GameObject prefabBotonEquipo = null;
    [SerializeField] private Text adviceText = null;
    [SerializeField] private Transform seccionEquiposTransform = null;

    [SerializeField] private ScrollRect scrollRect = null;
    [SerializeField] private GameObject flechaArriba = null;
    [SerializeField] private GameObject flechaAbajo = null;

    private List<GameObject> listaPrefabsBoton;

    private void Awake()
    {
        nombrePanel = "MIS EQUIPOS";

        panelMisEquipos = GetComponentInParent<PanelMisEquipos>();
        listaPrefabsBoton = new List<GameObject>();

        ActivarYDesactivarAdviceText();
    }

    private void FixedUpdate()
    {
        if(seccionEquiposTransform.childCount < 6)
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

    public void SetearPanelPrincipal()
    {
        //CanvasController.instance.botonDespliegueMenu.SetActive(true);

        AppController.instance.overlayPanel.SetNombrePanel(nombrePanel);

        if (gameObject.activeSelf)
        {
            BorrarPrefabs();
            CrearPrefabs();
        }

        AppController.instance.SetEquipoActual(null);

        ActivarYDesactivarAdviceText();
    }

    public void BorrarPrefabs()
    {
        foreach (GameObject prefab in listaPrefabsBoton)
        {
            Destroy(prefab);
        }
        listaPrefabsBoton.Clear();
    }

    public void CrearPrefabs()                                                                //Instancia el prefab del botón
    {
        foreach (Equipo equipo in AppController.instance.equipos)
        {
            //Crear el prefab correspondiente al deporte
            //GameObject botonEquipoGO = Instantiate(listaPrefabsDeportes[(int)equipo.GetDeporte()].gameObject, parentTransform, false);

            GameObject botonEquipoGO = Instantiate(prefabBotonEquipo.gameObject, seccionEquiposTransform, false);

            botonEquipoGO.GetComponentInChildren<Button>().GetComponentInChildren<Text>().text = equipo.GetNombre();
            botonEquipoGO.GetComponent<BotonEquipo>().SetSpriteBotonEquipo(equipo);

            listaPrefabsBoton.Add(botonEquipoGO);
        } 
        
    }



    public void CrearNuevoEquipo()                                                             //Función que se llama al apretar el botón NUEVO EQUIPO. Se muestra el panel para crear un nuevo equipo.
    {
        panelMisEquipos.MostrarPanelNuevoEquipo();
    }



    public void ActivarYDesactivarAdviceText()
    {
        if (listaPrefabsBoton.Count == 0)
        {
            adviceText.gameObject.SetActive(true);
        }
        else
        {
            adviceText.gameObject.SetActive(false);
        }
    }

    public void BorrarBotonEquipo(GameObject botonEquipo)
    {
        Destroy(botonEquipo);
        listaPrefabsBoton.Remove(botonEquipo);

        //Debug.Log("BORRADO");
    }
}
