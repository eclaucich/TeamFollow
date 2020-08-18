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
    [SerializeField] private FlechasScroll flechasScroll = null;

    private List<GameObject> listaPrefabsBoton;

    private float prefabHeight;
    private int cantMinima;

    private void Awake()
    {
        panelMisEquipos = GetComponentInParent<PanelMisEquipos>();
        listaPrefabsBoton = new List<GameObject>();

        ActivarYDesactivarAdviceText();
        prefabHeight = prefabBotonEquipo.GetComponent<RectTransform>().rect.height;
    }

    private void FixedUpdate()
    {
        flechasScroll.Actualizar(scrollRect, cantMinima, listaPrefabsBoton.Count);
    }

    public void SetearPanelPrincipal()
    {
        //CanvasController.instance.botonDespliegueMenu.SetActive(true);
        cantMinima = (int)Mathf.Ceil(scrollRect.GetComponent<RectTransform>().rect.height / (prefabHeight + seccionEquiposTransform.GetComponent<VerticalLayoutGroup>().spacing));

        CanvasController.instance.overlayPanel.SetNombrePanel("MIS EQUIPOS", AppController.Idiomas.Español);
        CanvasController.instance.overlayPanel.SetNombrePanel("MY TEAMS", AppController.Idiomas.Ingles);

        if (gameObject.activeSelf)
        {
            BorrarPrefabs();
            CrearPrefabs();
        }

        AppController.instance.equipoActual = null;

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
            botonEquipoGO.SetActive(true);

            botonEquipoGO.GetComponentInChildren<Button>().GetComponentInChildren<Text>().text = equipo.GetNombre();
            botonEquipoGO.GetComponent<BotonEquipo>().SetSpriteBotonEquipo(equipo);

            listaPrefabsBoton.Add(botonEquipoGO);
        }

        cantMinima = (int)(scrollRect.GetComponent<RectTransform>().rect.height / (prefabHeight + seccionEquiposTransform.GetComponent<VerticalLayoutGroup>().spacing));
        flechasScroll.Actualizar(scrollRect, cantMinima, listaPrefabsBoton.Count);
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
