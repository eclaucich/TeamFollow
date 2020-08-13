using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelPrincipalBiblioteca : Panel
{
    [SerializeField] private GameObject botonCarpetaPrefab = null;
    [SerializeField] private Transform parentTransform = null;
    [SerializeField] private FlechasScroll flechasScroll = null;
    [SerializeField] private ScrollRect scrollRect = null;
    [SerializeField] private GameObject adviceText = null;

    [SerializeField] private MensajeError mensajeErrorNuevoNombre = null;
    [SerializeField] private MensajeError mensajeCambioNombreExitoso = null;

    [SerializeField] private InputField inputNombreCarpeta = null;
    [SerializeField] private MensajeError mensajeErrorNuevaCarpeta = null;
    [SerializeField] private GameObject jugadasPrefab = null;

    private float prefabHeight;
    private int cantMinima;

    private void Start()
    {
        prefabHeight = botonCarpetaPrefab.GetComponent<RectTransform>().rect.height;

        mensajeErrorNuevoNombre.SetText("NOMBRE EXISTENTE", AppController.Idiomas.Español);
        mensajeErrorNuevoNombre.SetText("EXISTING NAME", AppController.Idiomas.Ingles);

        mensajeCambioNombreExitoso.SetText("NOMBRE CAMBIADO EXITOSAMENTE", AppController.Idiomas.Español);
        mensajeCambioNombreExitoso.SetText("NAME SUCCESSFULLY CHANGED", AppController.Idiomas.Ingles);

        mensajeErrorNuevaCarpeta.SetText("CARPETA EXISTENTE", AppController.Idiomas.Español);
        mensajeErrorNuevaCarpeta.SetText("EXISTING FOLDER", AppController.Idiomas.Ingles);
    }

    public void FixedUpdate()
    {
        flechasScroll.Actualizar(scrollRect, cantMinima, ChildsActive());
        if (parentTransform.childCount <= 2)
            adviceText.SetActive(true);
        else
            adviceText.SetActive(false);
    }

    private int ChildsActive()
    {
        int cantActivos = 0;
        for (int i = 0; i < parentTransform.childCount; i++)
        {
            if (parentTransform.GetChild(i).gameObject.activeSelf)
                cantActivos++;
        }
        return cantActivos;
    }

    public void SetPanePrincipal(bool reset = true)
    {
        /*if (listaPrefabs == null)
        {
            listaPrefabs = new List<GameObject>();
        }
        else
            Debug.Log("CANTIDAD: " + listaPrefabs.Count);*/

        Screen.orientation = ScreenOrientation.Portrait;

        CanvasController.instance.botonDespliegueMenu.SetActive(true);

        if (reset)
        {
            ResetPrefabs();

            if (parentTransform.childCount <= 1)
                adviceText.SetActive(true);
            else
                adviceText.SetActive(false);
        }
    }

    public void ResetPrefabs()
    {
        BorrarPrefabs();
        CrearPrefabs();
    }
    private void BorrarPrefabs()
    {
        //Debug.Log("DESTROY " + listaPrefabs.Count);
        for (int i = 2; i < parentTransform.childCount; i++)
        {
            Destroy(parentTransform.GetChild(i).gameObject);
        }
        //listaPrefabs.Clear();
    }

    private void CrearPrefabs()
    {
        //Por cada imagen en el sistema

        foreach (var carpeta in AppController.instance.carpetasJugadas)
        {
            NuevaCarpeta(carpeta);
        }


        cantMinima = (int)(scrollRect.GetComponent<RectTransform>().rect.height / (prefabHeight + parentTransform.GetComponent<VerticalLayoutGroup>().spacing));
        /*foreach (var imagen in AppController.instance.imagenesGuardadas)
        {
            GameObject botonImagenGO = Instantiate(botonImagenPrefab.gameObject, parentTransform, false);
            botonImagenGO.gameObject.SetActive(true);
            //listaPrefabs.Add(botonImagenGO);
            BotonImagen IGO = botonImagenGO.GetComponent<BotonImagen>();
            IGO.SetNombreBoton(imagen.GetNombre());
            IGO.SetImagenPreview(imagen.GetTexture());
            IGO.SetCategoria(imagen.GetCategoria());
            Debug.Log("IMAGEN: " + imagen.GetNombre());
        }

        cantMinima = (int)(scrollRect.GetComponent<RectTransform>().rect.height / (prefabHeight + parentTransform.GetComponent<VerticalLayoutGroup>().spacing));
        */
    }

    public void CrearNuevaCarpeta()
    {
        string _nombreCarpeta = inputNombreCarpeta.text.ToUpper();

        if (!AppController.instance.VerificarNombreCarpeta(_nombreCarpeta))
        {
            mensajeErrorNuevaCarpeta.Activar();
            return;
        }

        CarpetaJugada _nuevaCarpeta = new CarpetaJugada(_nombreCarpeta);

        AppController.instance.AgregarCarpetaJugada(_nuevaCarpeta);

        NuevaCarpeta(_nuevaCarpeta);
    }

    public void NuevaCarpeta(CarpetaJugada _carpeta)
    {
        GameObject goCarpeta = Instantiate(botonCarpetaPrefab, parentTransform, false);
        goCarpeta.SetActive(true);
        //GameObject goJugadas = Instantiate(jugadasPrefab, parentTransform, false);
        //goJugadas.SetActive(true);
        goCarpeta.GetComponent<BotonCarpetaJugada>().CrearPrefabs(_carpeta, parentTransform);

        //esto arregla el bug al abrir las carpetas la primera vez
        goCarpeta.GetComponent<BotonCarpetaJugada>().ToggleSeccionJugadas();
        goCarpeta.GetComponent<BotonCarpetaJugada>().ToggleSeccionJugadas();

        SaveSystem.GuardarCarpetaBiblioteca(_carpeta);
    }


    public void ActivarMensajeError()
    {
        mensajeErrorNuevoNombre.Activar();
    }

    public void ActivarMensajeCambioNombreExitoso()
    {
        mensajeCambioNombreExitoso.Activar();
    }
}
