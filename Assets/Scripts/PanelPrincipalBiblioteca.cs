using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelPrincipalBiblioteca : Panel
{
    [SerializeField] private PanelBiblioteca panelBiblioteca = null;

    [Space]
    [Header("Seccion carpetas/jugadas")]
    [SerializeField] private GameObject botonCarpetaPrefab = null;
    [SerializeField] private Transform parentTransform = null;
    [SerializeField] private FlechasScroll flechasScroll = null;
    [SerializeField] private ScrollRect scrollRect = null;
    [SerializeField] private GameObject adviceText = null;

    [Space]
    [Header("Mensajes Error/Exito")]
    [SerializeField] private MensajeError mensajeError = null;
    [SerializeField] private MensajeError mensajeExitoso = null;

    [Space]
    [Header("Seccion elegir carpeta")]
    [SerializeField] private GameObject carpetaPrefab = null;
    [SerializeField] private Transform transformCarpetas = null;

    [Space]
    [Header("Seccion nueva carpeta")]
    [SerializeField] private MensajeDesplegable seccionSeleccionarNuevaCarpeta = null;
    [SerializeField] private MensajeDesplegable seccionCrearNuevaCarpeta = null;

    [Space]
    [Header("Seleccion Multiple")]
    [SerializeField] private ConfirmacionBorradoSeleccionMultiple confirmacionBorradoSeleccionMultiple = null;
    [SerializeField] private GameObject botonBorrarSeleccionMultiple = null;
    [SerializeField] private GameObject botonNuevaCarpeta = null;
    private bool seleccionMultipleJugadasActivado = false;
    private bool seleccionMultipleCarpetasActivado = false;

    [Space]
    [Header("Buscador")]
    [SerializeField] private Buscador buscador = null;

    [Space]
    [Header("Otros")]
    [SerializeField] private InputField inputNombreCarpeta = null;
    [SerializeField] private InputField inputNuevoNombre = null;
    [SerializeField] private GameObject botones = null;
    [SerializeField] private ConfirmacionBorradoJugada confirmacionBorradoJugada = null;
    

    private List<BotonCarpetaJugada> listaBotonCarpeta;

    private float prefabHeight;
    private int cantMinima;

    private BotonImagen _botonImagenFocus;

    private void Start()
    {
        prefabHeight = botonCarpetaPrefab.GetComponent<RectTransform>().rect.height;

        mensajeExitoso.SetText("NOMBRE CAMBIADO EXITOSAMENTE", AppController.Idiomas.Español);
        mensajeExitoso.SetText("NAME SUCCESSFULLY CHANGED", AppController.Idiomas.Ingles);

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
                mensajeError.SetText("NOMBRE EXISTENTE", AppController.Idiomas.Español);
                mensajeError.SetText("EXISTING NAME", AppController.Idiomas.Ingles);
                mensajeError.Activar();
                return;
            }
            else
            {
                Debug.Log("NOMBRE CAMBIADO");
                mensajeExitoso.Activar();
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
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(seccionSeleccionarNuevaCarpeta.isDesplegado())
                CerrarSeleccionNuevaCarpeta();
            else if(seccionCrearNuevaCarpeta.isDesplegado())
                CerrarSeccionCrearNuevaCarpeta();
            
            if(seleccionMultipleCarpetasActivado)
                SetSeleccionMultipleCarpetas(false);
            if(seleccionMultipleJugadasActivado)
                SetSeleccionMultipleJugadas(false);
        }

        //no sería lo mas ótimo ésto
        if(seleccionMultipleCarpetasActivado || seleccionMultipleJugadasActivado)
            CanvasController.instance.retrocesoPausado = true;
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
        Screen.orientation = ScreenOrientation.Portrait;

        SetPublicity();

        buscador.SetBuscador(false);

        CanvasController.instance.retrocesoPausado = false;
        seleccionMultipleJugadasActivado = false;
        seleccionMultipleCarpetasActivado = false;
        botonBorrarSeleccionMultiple.SetActive(false);

        CanvasController.instance.botonDespliegueMenu.SetActive(true);

        if (reset)
        {
            listaBotonCarpeta = new List<BotonCarpetaJugada>();
            listaBotonCarpeta.Clear();

            BorrarPrefabsCarpetas();
            CrearPrefabsCarpetas();
            ResetPrefabs();

            if (parentTransform.childCount <= 1)
                adviceText.SetActive(true);
            else
                adviceText.SetActive(false);

            _botonImagenFocus = null;
            botones.SetActive(false);
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
            mensajeError.SetText("CARPETA EXISTENTE", AppController.Idiomas.Español);
            mensajeError.SetText("EXISTING FOLDER", AppController.Idiomas.Ingles);
            mensajeError.Activar();
            return;
        }

        if(_nombreCarpeta == SaveSystem.carpetaEspecialEspañol || _nombreCarpeta == SaveSystem.carpetaEspecialIngles)
        {
            mensajeError.SetText("NOMBRE RESERVADO", AppController.Idiomas.Español);
            mensajeError.SetText("RESERVED NAME", AppController.Idiomas.Ingles);
            mensajeError.Activar();
            return;
        }

        CarpetaJugada _nuevaCarpeta = new CarpetaJugada(_nombreCarpeta);

        AppController.instance.AgregarCarpetaJugada(_nuevaCarpeta);

        NuevaCarpeta(_nuevaCarpeta, true);

        seccionCrearNuevaCarpeta.ToggleDesplegar();
    }

    public void NuevaCarpeta(CarpetaJugada _carpeta, bool saveNew = false)
    {
        GameObject goCarpeta = Instantiate(botonCarpetaPrefab, parentTransform, false);
        goCarpeta.SetActive(true);
        BotonCarpetaJugada _botonCarpeta = goCarpeta.GetComponent<BotonCarpetaJugada>();

        _botonCarpeta.CrearPrefabs(_carpeta, parentTransform);

        listaBotonCarpeta.Add(_botonCarpeta);

        //esto arregla el bug al abrir las carpetas la primera vez
        _botonCarpeta.ToggleSeccionJugadas();
        _botonCarpeta.ToggleSeccionJugadas();

        if (_carpeta.GetNombre() == SaveSystem.carpetaEspecialEspañol)
            _botonCarpeta.SetCarpetaEspecial();

        if (saveNew) 
            SaveSystem.GuardarCarpetaBiblioteca(_carpeta); 

        //BorrarPrefabsCarpetas();
        //CrearPrefabsCarpetas();
    }

    public void SetBotonImagenFocus(BotonImagen _botonFocus)
    {
        if(!seleccionMultipleJugadasActivado && !seleccionMultipleCarpetasActivado)
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
        else if(seleccionMultipleJugadasActivado)
        {
            _botonFocus.ToggleSelected();
        }
    }

    public void BorrarImagenFocus()
    {
        AndroidManager.HapticFeedback();
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

    #region Mensajes Error/Exitoso
    public void ActivarMensajeError()
    {
        mensajeError.Activar();
    }

    public void ActivarMensajeExitoso()
    {
        mensajeExitoso.Activar();
    }
    #endregion

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
            mensajeError.SetText("YA EXISTE UNA JUGADA CON ESTE NOMBRE", AppController.Idiomas.Español);
            mensajeError.SetText("THERE'S ALREADY A STRATEGY WITH THIS NAME", AppController.Idiomas.Ingles);
            mensajeError.Activar();
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
            if (_nombreCarpeta == SaveSystem.carpetaEspecialEspañol && AppController.instance.idioma == AppController.Idiomas.Ingles)
                _nombreCarpeta = SaveSystem.carpetaEspecialIngles;
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
    

    #region Seleccion Multiple
    public void SetSeleccionMultipleJugadas(bool active)
    {
        if(seleccionMultipleCarpetasActivado)
            return;
        if(active && seleccionMultipleJugadasActivado)
            return;
        if(!active && !seleccionMultipleJugadasActivado)
            return;

        seleccionMultipleJugadasActivado = active;

        CanvasController.instance.retrocesoPausado = active;
        botonBorrarSeleccionMultiple.SetActive(active);
        botonNuevaCarpeta.SetActive(!active);

        foreach (var carpeta in listaBotonCarpeta)
        {
            carpeta.SetSeleccionMultipleJugadas(active);
        }
    }

    public void SetSeleccionMultipleCarpetas(bool active)
    {
        if(seleccionMultipleJugadasActivado)
            return;
        if(active && seleccionMultipleCarpetasActivado)
            return;
        if(!active && !seleccionMultipleCarpetasActivado)
            return;

        seleccionMultipleCarpetasActivado = active;

        CanvasController.instance.retrocesoPausado = active;
        botonBorrarSeleccionMultiple.SetActive(active);
        botonNuevaCarpeta.SetActive(!active);

        foreach (var carpeta in listaBotonCarpeta)
        {
            if(carpeta.GetCarpeta().GetNombre() == SaveSystem.carpetaEspecialEspañol || carpeta.GetCarpeta().GetNombre() == SaveSystem.carpetaEspecialIngles)
                continue;
            carpeta.SetSeleccionMultiple(active);
        }
    }

    public void ActivarBorradoSeleccionMultiple()
    {
        if(seleccionMultipleJugadasActivado)
        {
            List<BotonImagen> botonesImagen = new List<BotonImagen>();

            foreach (var carpeta in listaBotonCarpeta)
            {
                foreach (var jugada in carpeta.GetJugadas())
                {
                    if (jugada.IsSelected())
                        botonesImagen.Add(jugada);
                }
            }

            confirmacionBorradoSeleccionMultiple.Activar(botonesImagen);
        }
        else if(seleccionMultipleCarpetasActivado)
        {
            List<BotonCarpetaJugada> botonesCarpeta = new List<BotonCarpetaJugada>();

            foreach (var carpeta in listaBotonCarpeta)
            {
                if(carpeta.IsSelected())
                    botonesCarpeta.Add(carpeta);
            }

            confirmacionBorradoSeleccionMultiple.Activar(botonesCarpeta);
        }
    }

    #endregion

    #region Buscador

    public void ActualizarBusqueda(Text filterText)
    {
        string filter = filterText.text;

        int cantResultados = 0;

        foreach (var boton in listaBotonCarpeta)
        {
            cantResultados += boton.SetActiveFolders(filter.ToUpper());
        }

        buscador.SetCantidadResultados(cantResultados);
    }

    public void CerrarFiltrado()
    {
        foreach (var boton in listaBotonCarpeta)
        {
            boton.SetActiveFolders(true);
        }
    }

    #endregion
}
