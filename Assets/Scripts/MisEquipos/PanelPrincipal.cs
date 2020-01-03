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

    [SerializeField] private GameObject prefabBotonEquipo = null;
    [SerializeField] private Text adviceText = null;

    private List<GameObject> listaPrefabsBoton;

    private Transform parentTransform;

    private void Awake()
    {
        panelMisEquipos = GetComponentInParent<PanelMisEquipos>();
        listaPrefabsBoton = new List<GameObject>();

        parentTransform = GameObject.Find("SeccionEquipos").transform;

        ActivarYDesactivarAdviceText();
    }
    
    public void SetearPanelPrincipal()
    {
        //CanvasController.instance.botonDespliegueMenu.SetActive(true);

        if (gameObject.activeSelf)
        {
            BorrarPrefabs();
            CrearPrefabs();
        }
     
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
            GameObject botonEquipoGO = Instantiate(prefabBotonEquipo.gameObject, parentTransform, false);

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

        Debug.Log("BORRADO");
    }
}
