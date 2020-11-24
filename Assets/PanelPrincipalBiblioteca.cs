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
    [SerializeField] private MensajeError mensajeErrorCambioCarpeta = null;

    [SerializeField] private InputField inputNombreCarpeta = null;
    [SerializeField] private MensajeError mensajeErrorNuevaCarpeta = null;
    [SerializeField] private GameObject jugadasPrefab = null;

    [SerializeField] private PanelBiblioteca panelBiblioteca = null;

    [SerializeField] private ConfirmacionBorradoJugada confirmacionBorradoJugada = null;

    [SerializeField] private GameObject botones = null;
    [SerializeField] private InputField inputNuevoNombre = null;

    [SerializeField] private GameObject carpetaPrefab = null;
    [SerializeField] private Transform transformCarpetas = null;
    [SerializeField] private MensajeDesplegable seccionSeleccionarNuevaCarpeta = null;
    [SerializeField] private MensajeDesplegable seccionCrearNuevaCarpeta = null;

    private float prefabHeight;
    private int cantMinima;

    private BotonImagen _botonImagenFocus;

    private void Start()
    {
        prefabHeight = botonCarpetaPrefab.GetComponent<RectTransform>().rect.height;

        mensajeErrorNuevoNombre.SetText("NOMBRE EXISTENTE", AppController.Idiomas.Español);
        mensajeErrorNuevoNombre.SetText("EXISTING NAME", AppController.Idiomas.Ingles);

        mensajeCambioNombreExitoso.SetText("NOMBRE CAMBIADO EXITOSAMENTE", AppController.Idiomas.Español);
        mensajeCambioNombreExitoso.SetText("NAME SUCCESSFULLY CHANGED", AppController.Idiomas.Ingles);

        mensajeErrorNuevaCarpeta.SetText("CARPETA EXISTENTE", AppController.Idiomas.Español);
        mensajeErrorNuevaCarpeta.SetText("EXISTING FOLDER", AppController.Idiomas.Ingles);

        mensajeErrorCambioCarpeta.SetText("YA EXISTE UNA JUGADA CON ESTE NOMBRE", AppController.Idiomas.Español);
        mensajeErrorCambioCarpeta.SetText("THERE'S ALREADY A STRATEGY WITH THIS NAME", AppController.Idiomas.Ingles);

        inputNuevoNombre.onEndEdit.AddListener(VerificarEdicionNombreJugada);

        _botonImagenFocus = null;
        botones.SetActive(false);
    }

    private void VerificarEdicionNombreJugada(string _nuevoNombre)
    {
        if (!inputNuevoNombre.wasCanceled && _nuevoNombre != _botonImagenFocus.GetNombre())
        {
            CarpetaJugada _carpeta = _botonImagenFocus.GetCarpeta();

            if (_carpeta.ExistsJugada(_nuevoNombre.ToUpper()))
            {
                Debug.Log("NOMBRE EXISTENTE: " + _nuevoNombre);
                ActivarMensajeError();
                return;
            }
            else
            {
                Debug.Log("NOMBRE CAMBIADO");
                ActivarMensajeCambioNombreExitoso();
                SaveSystem.EditarJugada(_botonImagenFocus.GetNombre(), _nuevoNombre.ToUpper(), _botonImagenFocus.GetCarpeta());
                _botonImagenFocus.SetNewName(_nuevoNombre.ToUpper());
            }
        }
    }

    public void FixedUpdate()
    {
        flechasScroll.Actualizar(scrollRect, cantMinima, ChildsActive());
        if (parentTransform.childCount <= 2)
            adviceText.SetActive(true);
        else
            adviceText.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (seccionSeleccionarNuevaCarpeta.isDesplegado())
                CerrarSeleccionNuevaCarpeta();
            else if (seccionCrearNuevaCarpeta.isDesplegado())
                CerrarSeccionCrearNuevaCarpeta();
        }
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
            BorrarPrefabsCarpetas();
            CrearPrefabsCarpetas();
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


        cantMinima = (int)(scrollRect.GetComponent<RectTransform>().rect.height / (prefabHeight + parentTransform.GetComponent<VerticalLayoutGroup>().spacing + parentTransform.GetComponent<VerticalLayoutGroup>().padding.top));
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

        NuevaCarpeta(_nuevaCarpeta, true);
    }

    public void NuevaCarpeta(CarpetaJugada _carpeta, bool saveNew = false)
    {
        GameObject goCarpeta = Instantiate(botonCarpetaPrefab, parentTransform, false);
        goCarpeta.SetActive(true);
        BotonCarpetaJugada _botonCarpeta = goCarpeta.GetComponent<BotonCarpetaJugada>();
        //GameObject goJugadas = Instantiate(jugadasPrefab, parentTransform, false);
        //goJugadas.SetActive(true);
        _botonCarpeta.CrearPrefabs(_carpeta, parentTransform);

        //esto arregla el bug al abrir las carpetas la primera vez
        _botonCarpeta.ToggleSeccionJugadas();
        _botonCarpeta.ToggleSeccionJugadas();

        Debug.Log("CARP: " + _carpeta.GetNombre());
        if (_carpeta.GetNombre() == "SIN CARPETA")
            _botonCarpeta.SetCarpetaEspecial();

        if (saveNew) 
            SaveSystem.GuardarCarpetaBiblioteca(_carpeta); 

        BorrarPrefabsCarpetas();
        CrearPrefabsCarpetas();
    }

    public void SetBotonImagenFocus(BotonImagen _botonFocus)
    {
        if (_botonImagenFocus != null)
        {
            _botonImagenFocus.DesactivarSeleccion();
            if (_botonImagenFocus == _botonFocus)
            {
                _botonImagenFocus = null;
                botones.SetActive(false);
                return;
            }
        }

        _botonImagenFocus = _botonFocus;
        _botonImagenFocus.ActivarSeleccion();

        botones.SetActive(true);
    }

    public void BorrarImagenFocus()
    {
        confirmacionBorradoJugada.Activar(_botonImagenFocus);
    }

    public void VerImageFocus()
    {
        panelBiblioteca.MostrarPanelImagen(_botonImagenFocus);
    }

    public void AbrirSeccionCrearNuevaCarpeta()
    {
        seccionCrearNuevaCarpeta.ToggleDesplegar();
        CanvasController.instance.retrocesoPausado = true;
    }

    public void CerrarSeccionCrearNuevaCarpeta()
    {
        seccionCrearNuevaCarpeta.ToggleDesplegar();
        CanvasController.instance.retrocesoPausado = false;
    }

    public void MoverImageFocus()
    {
        seccionSeleccionarNuevaCarpeta.ToggleDesplegar();
        CanvasController.instance.retrocesoPausado = true;
    }

    public void CerrarSeleccionNuevaCarpeta()
    {
        seccionSeleccionarNuevaCarpeta.ToggleDesplegar();
        CanvasController.instance.retrocesoPausado = false;
    }

    public void SetImageNuevaCarpeta(Text _nombreCarpetaText)
    {
        CarpetaJugada _nuevaCarpeta = AppController.instance.BuscarCarpetaPorNombre(_nombreCarpetaText.text.ToUpper());
        if (_nuevaCarpeta == _botonImagenFocus.GetCarpeta())
        {
            seccionSeleccionarNuevaCarpeta.Cerrar();
            return;
        }
        if (!_botonImagenFocus.VerificarNombreJugadasCarpeta(_nuevaCarpeta))
        {
            Debug.Log("NOMBRE EXISTENTE");
            mensajeErrorCambioCarpeta.Activar();
            return;
        }
        _botonImagenFocus.SetCarpeta(_nuevaCarpeta);
        seccionSeleccionarNuevaCarpeta.Cerrar();
        CanvasController.instance.retrocesoPausado = false;
        _botonImagenFocus = null;
        botones.SetActive(false);
        ResetPrefabs();
    }

    private void CrearPrefabsCarpetas()
    {
        foreach (var carpeta in AppController.instance.carpetasJugadas)
        {
            GameObject go = Instantiate(carpetaPrefab, transformCarpetas, false);
            go.SetActive(true);
            string _nombreCarpeta = carpeta.GetNombre().ToUpper();
            if (_nombreCarpeta == "SIN CARPETA" && AppController.instance.idioma == AppController.Idiomas.Ingles)
                _nombreCarpeta = "WITHOUT FOLDER";
            go.GetComponentInChildren<Text>().text = _nombreCarpeta;
        }
    }

    private void BorrarPrefabsCarpetas()
    {
        for (int i = 1; i < transformCarpetas.childCount; i++)
        {
            Destroy(transformCarpetas.GetChild(i).gameObject);
        }
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
