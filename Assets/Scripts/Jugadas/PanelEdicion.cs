﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PanelEdicion : MonoBehaviour, IPointerClickHandler, IDragHandler, IPointerUpHandler
{
    PanelCrearJugadas panelCrearJugada;

    [SerializeField] private Camera snapshotCamera = null;

    /*[SerializeField] private Texture fullField = null;
    [SerializeField] private Texture halfField = null;
    [SerializeField] private Texture areaField = null;*/

    [SerializeField] private PanelHerramientas panelHerramientas = null;
    [SerializeField] private GameObject botonDesplieguePanelHerramientas = null;

    private int deporteIndex = 0;

    [SerializeField] private List<Texture> texturesFutbol;
    [SerializeField] private List<Texture> texturesBasket;
    [SerializeField] private List<Texture> texturesHockeyCesped;
    [SerializeField] private List<Texture> texturesHockeyPatines;
    [SerializeField] private List<Texture> texturesHandball;
    [SerializeField] private List<Texture> texturesPadel;
    [SerializeField] private List<Texture> texturesSoftball;
    [SerializeField] private List<Texture> texturesVoley;
    [SerializeField] private List<Texture> texturesTenis;
    [SerializeField] private List<Texture> texturesRugby;

    [SerializeField] private TextoImagenGuardada textoJugadaGuardada = null;

    private List<Texture> currentTextures;
    private int currentTextureIndex = 0;

    private PanelOpcionesHerramienta panelOpcionesActual;

    int width = 1280;
    int height = 720;

    private Vector2 vectInitialPos;
    private Vector2 vectFinalPos;
    private Vector2 vectSwipe;

    private void Awake()
    {
        panelCrearJugada = GetComponentInParent<PanelCrearJugadas>();
        snapshotCamera.gameObject.SetActive(false);

       /* texturesFutbol = new List<Texture>();
        texturesFutbol.Add(fullField);
        texturesFutbol.Add(halfField);
        texturesFutbol.Add(areaField);
        GetComponent<RawImage>().texture = texturesFutbol[currentTextureIndex];*/
        currentTextures = texturesFutbol;
        GetComponent<RawImage>().texture = texturesFutbol[currentTextureIndex];
    }

    private void FixedUpdate()
    {
        if (!panelHerramientas.GetComponent<MensajeDesplegable>().isDesplegado())
        {
            if (panelCrearJugada.GetHerramientaActual() != null && 
                panelCrearJugada.GetHerramientaActual().GetNombre() != "Seleccionar" &&
                panelCrearJugada.GetHerramientaActual().GetNombre() != "Flecha")
            {
                if (Input.GetMouseButtonDown(0))
                {
                    vectInitialPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                }

                if (Input.GetMouseButtonUp(0))
                {
                    vectFinalPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                    vectSwipe = new Vector2(vectFinalPos.x - vectInitialPos.x, vectFinalPos.y - vectInitialPos.y);

                    if (vectFinalPos.x - vectInitialPos.x >= 0.3f)
                    {
                        panelHerramientas.GetComponent<MensajeDesplegable>().ToggleDesplegar();
                    }
                }
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(panelCrearJugada.GetHerramientaActual() != null && panelCrearJugada.GetHerramientaActual().GetNombre() != "Flecha")
        {
            panelCrearJugada.UsarHerramientaActual();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (panelCrearJugada.GetHerramientaActual().GetNombre() == "Flecha" && Input.GetMouseButton(0))
        {
            panelCrearJugada.UsarHerramientaActual();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (panelCrearJugada.GetHerramientaActual().GetNombre() == "Flecha")
        {
            panelCrearJugada.GetHerramientaActual().DejarDeUsar();
        }
    }

    public void LimpiarPanel()
    {
        List<GameObject> childs = new List<GameObject>();

        foreach (Transform child in transform)
        {
            childs.Add(child.gameObject);
        }

        foreach (var child in childs)
        {
            Destroy(child);
        }
    }

    private void LateUpdate()
    {
        if (snapshotCamera.gameObject.activeInHierarchy)
        { 
            CanvasController.instance.GetComponent<Canvas>().worldCamera = snapshotCamera;
            Texture2D snapshot = new Texture2D(width-0, height, TextureFormat.RGB24, false);
            snapshotCamera.Render();
            RenderTexture.active = snapshotCamera.targetTexture;
            snapshot.ReadPixels(new Rect(0, 0, width, height), 0, 0);
            byte[] bytes = snapshot.EncodeToPNG();
            SaveSystem.GuardarJugadaImagen(bytes);
            Debug.Log("Guardado");
            snapshotCamera.gameObject.SetActive(false);
            CanvasController.instance.GetComponent<Canvas>().worldCamera = Camera.main;

            panelHerramientas.gameObject.SetActive(true);
            botonDesplieguePanelHerramientas.SetActive(true);
        }
    }

    public void GuardarJugadaImagen()
    {
        if (panelHerramientas.GetComponent<MensajeDesplegable>().isDesplegado())
            panelHerramientas.GetComponent<MensajeDesplegable>().ToggleDesplegar();
        //panelHerramientas.gameObject.SetActive(false);
        botonDesplieguePanelHerramientas.SetActive(false);

        textoJugadaGuardada.Mostrar();

        snapshotCamera.gameObject.SetActive(true);
        snapshotCamera.targetTexture = new RenderTexture(width, height, 24);
    }


    public void NextBackgroundImage()
    {
        currentTextureIndex = currentTextureIndex == currentTextures.Count - 1 ? 0 : currentTextureIndex + 1;

        GetComponent<RawImage>().texture = currentTextures[currentTextureIndex];
    }

    public void ChangeSport(int i)
    {
        switch(i)
        {
            case 0: currentTextures = texturesBasket; break;
            case 1: currentTextures = texturesFutbol; break;
            case 2: currentTextures = texturesHandball; break;
            case 3: currentTextures = texturesHockeyCesped; break;
            case 4: currentTextures = texturesHockeyPatines; break;
            case 5: currentTextures = texturesPadel; break;
            case 6: currentTextures = texturesRugby; break;
            case 7: currentTextures = texturesSoftball; break;
            case 8: currentTextures = texturesTenis; break;
            case 9: currentTextures = texturesVoley; break;
        }
        currentTextureIndex = 0;
        NextBackgroundImage();
    }

    public void CerrarPanelOpcionesActual()
    {
        if (panelOpcionesActual != null)
            panelOpcionesActual.Cerrar();
    }

    public void SetPanelOpcionesActual(PanelOpcionesHerramienta panel_)
    {
        panelOpcionesActual = panel_;
    }

}
