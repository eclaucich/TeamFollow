using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelPrincipalBiblioteca : Panel
{
    [SerializeField] private GameObject botonImagenPrefab = null;
    [SerializeField] private Transform parentTransform = null;
    [SerializeField] private FlechasScroll flechasScroll = null;
    [SerializeField] private ScrollRect scrollRect = null;
    [SerializeField] private GameObject adviceText = null;

    //private List<GameObject> listaPrefabs = null; //BotonBiblioteca


    public void FixedUpdate()
    {
        flechasScroll.Actualizar(scrollRect, 2, parentTransform.childCount);
    }

    public void SetPanePrincipal()
    {
        /*if (listaPrefabs == null)
        {
            listaPrefabs = new List<GameObject>();
        }
        else
            Debug.Log("CANTIDAD: " + listaPrefabs.Count);*/

        Screen.orientation = ScreenOrientation.Portrait;

        CanvasController.instance.botonDespliegueMenu.SetActive(true);

        BorrarPrefabs();
        CrearPrefabs();

        if (parentTransform.childCount <= 1)
            adviceText.SetActive(true);
        else
            adviceText.SetActive(false);
    }

    private void BorrarPrefabs()
    {
        //Debug.Log("DESTROY " + listaPrefabs.Count);
        for (int i = 1; i < parentTransform.childCount; i++)
        {
            Destroy(parentTransform.GetChild(i).gameObject);
        }
        //listaPrefabs.Clear();
    }

    private void CrearPrefabs()
    {
        //Por cada imagen en el sistema

        Debug.Log("Prefabs...");
        foreach (var imagen in AppController.instance.imagenesGuardadas)
        {
            GameObject botonImagenGO = Instantiate(botonImagenPrefab.gameObject, parentTransform, false);
            botonImagenGO.gameObject.SetActive(true);
            //listaPrefabs.Add(botonImagenGO);
            BotonImagen IGO = botonImagenGO.GetComponent<BotonImagen>();
            IGO.SetNombreBoton(imagen.GetNombre());
            IGO.SetImagenPreview(imagen.GetTexture());
            Debug.Log("IMAGEN: " + imagen.GetNombre());
        }
        
        /*GameObject botonImagenGO = Instantiate(botonImagenPrefab.gameObject, parentTransform, false);

        botonImagenGO.GetComponentInChildren<Button>().GetComponentInChildren<Text>().text = "NOMBRE IMAGEN";

        listaPrefabs.Add(botonImagenGO);*/
    }
}
