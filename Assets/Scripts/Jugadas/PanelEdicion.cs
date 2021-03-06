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
    [SerializeField] private Text resolucionText = null;

    private int deporteIndex = 0;

    [SerializeField] private GameObject seccionGuardarJugada = null;
    [SerializeField] private Text nombreJugadaText = null;

    [SerializeField] private MensajeError textoJugadaGuardada = null;
    [SerializeField] private MensajeError mensajeTutorial = null;
    [SerializeField] private MensajeError mensajeErrorGuardar = null;

    [SerializeField] private GameObject botonSeleccionarCarpetaPrefab = null;
    [SerializeField] private Transform parentSeleccionCarpeta = null;

    private List<Texture> currentTextures;
    private int currentTextureIndex = 0;

    private PanelOpcionesHerramienta panelOpcionesActual;

    int width;// = 1280;
    int width2;// = 1210;
    int height;// = 720;

    private float swipeMax;

    private Vector2 vectInitialPos;
    private Vector2 vectFinalPos;
    private Vector2 vectSwipe;
    private float swipeDiff;
    private bool swipeEnabled = false;

    private string nombreJugada = string.Empty;
    private string categoriaActual = string.Empty;
    private BotonSeleccionarCarpeta carpetaSeleccionada = null;

    private void Awake()
    {
        panelCrearJugada = GetComponentInParent<PanelCrearJugadas>();
        snapshotCamera.gameObject.SetActive(false);

        currentTextures = new List<Texture>();
        currentTextures.Add(Deportes.instance.GetImagenCancha(Deportes.DeporteEnum.Futbol, Deportes.TipoCanchasEnum.CanchaEntera).texture);

        //panelHerramientas.gameObject.SetActive(false);
    }

    private void Start()
    {
        width = AppController.instance.resWidth;
        height = AppController.instance.resHeight;

        if(height > width)
        {
            int aux = width;
            width = height;
            height = aux;
        }
        width2 = width - 70;

        swipeMax = 0.2f * height;
        Debug.Log("SWIPE: " + swipeMax);

        //Debug.Log("WE: " + width + " HE: " + height);
        resolucionText.text = "WE: " + width + ", HE: " + height;
        //panelHerramientas.ToogleActive();

        mensajeTutorial.SetText("Deslizar hacia arriba/abajo para abrir/cerrar las herramientas".ToUpper(), AppController.Idiomas.Español);
        mensajeTutorial.SetText("Slide up/down to toggle open the tools window".ToUpper(), AppController.Idiomas.Ingles);
        mensajeTutorial.Activar();

        seccionGuardarJugada.SetActive(false);
        CambiarCategoriaJugada("null");
    }

    private void Update()
    {
        CanvasController.instance.botonDespliegueMenu.SetActive(false);
        
        //Mido el swipe solo si esta habilitado
        if (swipeEnabled)
        {
            //Mientras mantenga apretado
            if (Input.GetMouseButtonDown(0))
            {
                vectInitialPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                vectFinalPos = Camera.main.ScreenToWorldPoint(vectFinalPos);
            }
            //Cuando suelta, medir la distancia del swipe
            if (Input.GetMouseButtonUp(0))
            {
                vectFinalPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                vectFinalPos = Camera.main.ScreenToWorldPoint(vectFinalPos);
                vectSwipe = vectFinalPos - vectInitialPos;

                swipeDiff = vectFinalPos.y - vectInitialPos.y;
            }
        }

        //Si esta abierto el cartel para guardar la jugada no tener en cuenta el swipe
        if (seccionGuardarJugada.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                CerrarSeccionGuardarJugada();
        }
        /*else
        {
            //Si el panel de herramientas esta abierto
            if (panelHerramientas.isActive())
            {
                //Me fijo si el swipe fue hacia abajo, y lo cierro
                if (swipeEnabled && swipeDiff < -swipeMax)
                    if (panelCrearJugada.GetHerramientaActual() == null || panelCrearJugada.GetHerramientaActual().GetNombre() != "Flecha")
                        panelHerramientas.ToogleActive();
            }
            else //Si el panel de herramientas esta cerrado
            {
                //MNe fijo si el swipe fue hacia arriba, para abrirlo
                if (swipeEnabled && swipeDiff > swipeMax)
                {
                    if (panelCrearJugada.GetHerramientaActual() == null || panelCrearJugada.GetHerramientaActual().GetNombre() != "Flecha")
                        panelHerramientas.ToogleActive();
                    CerrarPanelOpcionesActual();
                }
            }
        }*/
    }

    private void LateUpdate()
    { 
        if (snapshotCamera.gameObject.activeInHierarchy)
        { 
            panelHerramientas.gameObject.SetActive(false);

            CanvasController.instance.GetComponent<Canvas>().worldCamera = snapshotCamera;
            Texture2D snapshot = new Texture2D(width-0, height, TextureFormat.RGB24, false);
            snapshotCamera.Render();
            RenderTexture.active = snapshotCamera.targetTexture;
            snapshot.ReadPixels(new Rect(0, 0, width, height), 0, 0);
            byte[] bytes = snapshot.EncodeToPNG();

            CarpetaJugada _carpeta;
            _carpeta = carpetaSeleccionada.GetCarpeta();
            SaveSystem.GuardarJugadaImagen(bytes, nombreJugada, categoriaActual, _carpeta);

            snapshotCamera.gameObject.SetActive(false);
            CanvasController.instance.GetComponent<Canvas>().worldCamera = Camera.main;

            textoJugadaGuardada.SetText("Jugada guardada existosamente".ToUpper(), AppController.Idiomas.Español);
            textoJugadaGuardada.SetText("Strategy successfully saved".ToUpper(), AppController.Idiomas.Ingles);
            textoJugadaGuardada.Activar();
            swipeEnabled = true;
            panelHerramientas.gameObject.SetActive(true);
        }
    }

#region Funciones del mouse
    public void OnPointerClick(PointerEventData eventData)
    {
        Herramienta herramientaActual = panelCrearJugada.GetHerramientaActual();
        if (herramientaActual == null) return;

        if (herramientaActual.GetNombre() != "Flecha")
        {
            panelCrearJugada.UsarHerramientaActual();
        }

        Debug.Log("CLICK");
    }

    public void OnDrag(PointerEventData eventData)
    {
        Herramienta herramientaActual = panelCrearJugada.GetHerramientaActual();
        if (herramientaActual == null) return;

        if (herramientaActual.GetNombre() == "Flecha" && Input.GetMouseButton(0))
        {
            panelCrearJugada.UsarHerramientaActual();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Herramienta herramientaActual = panelCrearJugada.GetHerramientaActual();
        if (herramientaActual == null) return;

        if (herramientaActual.GetNombre() == "Flecha")
        {
            panelCrearJugada.GetHerramientaActual().DejarDeUsar();
        }
    }
#endregion

    #region Seccion Guardar Jugada
    public void AbrirSeccionGuardarJugada()
    {
        AndroidManager.HapticFeedback();

        CanvasController.instance.retrocesoPausado = true;
        seccionGuardarJugada.SetActive(true);
        bool aux = false; //poner en false para que la primera carpeta este seleccionada al empezar
        foreach (var carpeta in AppController.instance.carpetasJugadas)
        {
            Debug.Log("CARP: " + carpeta.GetNombre());
            GameObject go = Instantiate(botonSeleccionarCarpetaPrefab, parentSeleccionCarpeta, false);
            go.SetActive(true);
            BotonSeleccionarCarpeta botonGO = go.GetComponent<BotonSeleccionarCarpeta>();
            botonGO.SetCarpeta(carpeta);
            if (!aux)
            {
                SetCarpetaSeleccionada(botonGO);
                aux = true;
            }
            else
                botonGO.Deseleccionar();
            if (carpeta.GetNombre() == "-")
            {
                go.transform.SetAsFirstSibling();
                SetCarpetaSeleccionada(botonGO);
                parentSeleccionCarpeta.GetChild(1).SetAsFirstSibling();
            }
        }
    }

    public void SetCarpetaSeleccionada(BotonSeleccionarCarpeta _botonCarpeta)
    {
        if (carpetaSeleccionada != null)
        {
            carpetaSeleccionada.Deseleccionar();
        }
        carpetaSeleccionada = _botonCarpeta;
        carpetaSeleccionada.Seleccionar();
    }

    public void CerrarSeccionGuardarJugada()
    {
        for (int i = 1; i < parentSeleccionCarpeta.childCount; i++)
        {
            Destroy(parentSeleccionCarpeta.GetChild(i).gameObject);
        }
        seccionGuardarJugada.SetActive(false);
        CanvasController.instance.retrocesoPausado = false;
    }

    public void CambiarCategoriaJugada(string categoria_)
    {
        categoriaActual = categoria_;
    }

    public void GuardarJugadaImagen()
    {
        nombreJugada = nombreJugadaText.text.ToUpper();

        if (carpetaSeleccionada!=null && carpetaSeleccionada.GetCarpeta().ExistsJugada(nombreJugada))
        {
            mensajeErrorGuardar.SetText("Nombre existente!".ToUpper(), AppController.Idiomas.Español);
            mensajeErrorGuardar.SetText("Existing name!".ToUpper(), AppController.Idiomas.Ingles);
            mensajeErrorGuardar.Activar();
            return;
        }
        else if(!AppController.instance.VerificarNombre(nombreJugada))
        {
            mensajeErrorGuardar.SetText("Nombre invalido!".ToUpper(), AppController.Idiomas.Español);
            mensajeErrorGuardar.SetText("Invalid name!".ToUpper(), AppController.Idiomas.Ingles);
            mensajeErrorGuardar.Activar();
            return;
        }

        CerrarSeccionGuardarJugada();
        swipeEnabled = false;
        //panelHerramientas.gameObject.SetActive(false);
        CerrarPanelOpcionesActual();

        snapshotCamera.gameObject.SetActive(true);
        snapshotCamera.targetTexture = new RenderTexture(width, height, 24);
    }
    #endregion

    #region Control de la imagen de fondo
    public void SetPanelEdicion()
    {
        CerrarPanelHerramientas();
        
        if(AppController.instance.deporteFavorito != Deportes.DeporteEnum.Ninguno)
        {
            ChangeSport(AppController.instance.deporteFavorito);
        }
        else
        {
            ChangeSport(Deportes.DeporteEnum.Futbol);
        }
    }

    public void LimpiarPanel()
    {
        AndroidManager.HapticFeedback();

        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void NextBackgroundImage()
    {
        AndroidManager.HapticFeedback();
        
        currentTextureIndex = currentTextureIndex == currentTextures.Count - 1 ? 0 : currentTextureIndex + 1;

        GetComponent<RawImage>().texture = currentTextures[currentTextureIndex];
    }

    public void ChangeSport(Deportes.DeporteEnum _nuevoDeporte)
    {
        currentTextures.Clear();
        currentTextures.Add(Deportes.instance.GetImagenCancha(_nuevoDeporte, Deportes.TipoCanchasEnum.CanchaEntera).texture);

        currentTextureIndex = 0;
        NextBackgroundImage();
    }
    #endregion

    #region Control de paneles de herramientas y opciones
    public void CerrarPanelOpcionesActual()
    {
        if (panelOpcionesActual != null)
            panelOpcionesActual.Cerrar();
    }

    public void SetPanelOpcionesActual(PanelOpcionesHerramienta panel_)
    {
        panelOpcionesActual = panel_;
    }

    public void CerrarPanelHerramientas()
    {
        if (panelHerramientas.isActive())
        {
            panelHerramientas.ToogleActive();
            Debug.Log("CERRAR PANEL");
        }
    }

    public void AbrirPanelHerramientas()
    {
        if (!panelHerramientas.isActive())
        {
            panelHerramientas.ToogleActive();
            Debug.Log("ABRIR PANEL");
        }
    }

    public void TogglePanelHerramientas()
    {
        panelHerramientas.ToogleActive();
    }

    public void SetSwipe(bool aux)
    {
        swipeEnabled = aux;
    }

    public PanelHerramientas GetPanelHerramientas()
    {
        return panelHerramientas;
    }
    #endregion
}
