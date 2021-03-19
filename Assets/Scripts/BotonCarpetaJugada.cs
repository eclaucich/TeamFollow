using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotonCarpetaJugada : MonoBehaviour
{
    [SerializeField] private Text nombreCarpetaText = null;
    [SerializeField] private GameObject botonJugadaPrefab = null;
    [SerializeField] private InputField inputfield = null;
    [SerializeField] private GameObject botonBorrar = null;
    [SerializeField] private PanelPrincipalBiblioteca panelPrincipalBiblioteca = null;
    [SerializeField] private GameObject seccionJugadasPrefab = null;
    [SerializeField] private Image imagen = null;

    private CarpetaJugada carpeta;
    private GameObject seccionJugadas;
    private Animator animator;

    private List<BotonImagen> listaBotonImagen;

    [Space]
    [Header("Seleccion Multiple")]
    [SerializeField] private Toggle toggleSeleccionMultiple = null;
    private bool seleccionMultipleJugadasActivado;
    private bool seleccionMultipleActivada;

    private void Start()
    {
        inputfield.onEndEdit.AddListener(VerificarEdicionNombreCarpeta);
        animator = GetComponent<Animator>();

        toggleSeleccionMultiple.isOn = false;
        toggleSeleccionMultiple.gameObject.SetActive(false);
        seleccionMultipleActivada = false;
        seleccionMultipleJugadasActivado = false;
    }

    private void Update() 
    {
        if(seleccionMultipleActivada)   
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                toggleSeleccionMultiple.isOn = false;
                SetSeleccionMultiple(false);
            }

            if (toggleSeleccionMultiple.isOn)
                imagen.color = AppController.instance.colorTheme.botonSeleccionado;
            else
                imagen.color = AppController.instance.colorTheme.detalle4;
        } 
    }

    public void CrearPrefabs(CarpetaJugada _carpeta, Transform _goJugadas)
    {
        carpeta = _carpeta;
        if (_carpeta.GetNombre() == "-")
            nombreCarpetaText.text = "SIN CARPETA";
        else 
            nombreCarpetaText.text = _carpeta.GetNombre();

        seccionJugadas = Instantiate(seccionJugadasPrefab, _goJugadas, false);
        seccionJugadas.SetActive(false);
        
        listaBotonImagen = new List<BotonImagen>();

        foreach (var jugada in _carpeta.GetListaJugadas())
        {
            Debug.Log("NOMBRE: " + jugada.GetNombre());
            Debug.Log("CATEGORIA: " + jugada.GetCategoria());

            GameObject go = Instantiate(botonJugadaPrefab, seccionJugadas.transform, false);
            go.SetActive(true);
            BotonImagen botonGO = go.GetComponent<BotonImagen>();
            botonGO.SetJugadaFocus(jugada);
            listaBotonImagen.Add(botonGO);
        }
    }

    private void VerificarEdicionNombreCarpeta(string _nuevoNombre)
    {
        if (_nuevoNombre != nombreCarpetaText.text)
        {
            if (!AppController.instance.VerificarNombreCarpeta(_nuevoNombre.ToUpper()))
            {
                Debug.Log("NOMBRE EXISTENTE: " + _nuevoNombre);
                panelPrincipalBiblioteca.ActivarMensajeError();
                return;
            }
            else
            {
                Debug.Log("NOMBRE CAMBIADO");
                panelPrincipalBiblioteca.ActivarMensajeExitoso();
                carpeta.SetNombre(_nuevoNombre);
                SaveSystem.EditarCarpeta(nombreCarpetaText.text, _nuevoNombre.ToUpper());
                nombreCarpetaText.text = _nuevoNombre.ToUpper();
            }
        }
    }

    public void ToggleSeccionJugadas()
    {
        if(!seleccionMultipleActivada)
        {
            if (seccionJugadas != null)
            {
                bool active = !seccionJugadas.activeSelf;
                seccionJugadas.SetActive(active);
                if (animator) animator.SetBool("open", active);
            }
            Canvas.ForceUpdateCanvases();
        }
        else
        {
            toggleSeleccionMultiple.isOn = !toggleSeleccionMultiple.isOn;
        }
    }

    public CarpetaJugada GetCarpeta()
    {
        return carpeta;
    }

    public void SetCarpetaEspecial()
    {
        inputfield.gameObject.SetActive(false);
        botonBorrar.SetActive(false);
        TextLanguage _txtLanguage = nombreCarpetaText.gameObject.AddComponent<TextLanguage>();
        _txtLanguage.SetText("SIN CARPETA", AppController.Idiomas.Español);
        _txtLanguage.SetText("WITHOUT FOLDER", AppController.Idiomas.Ingles);
    }

    public List<BotonImagen> GetJugadas()
    {
        return listaBotonImagen;
    }


    #region Seleccion Multiple
    public void SetSeleccionMultipleJugadas(bool active)
    {
        seleccionMultipleJugadasActivado = active;

        foreach (var jugada in listaBotonImagen)
        {
            jugada.SetSeleccionMultiple(active);
        }
    }

    public void SetSeleccionMultiple(bool active)
    {
        if(carpeta.GetNombre() == "SIN CARPETA" || carpeta.GetNombre() == "WITHOUT FOLDER")
            return;
        seleccionMultipleActivada = active;

        toggleSeleccionMultiple.gameObject.SetActive(active);
        botonBorrar.SetActive(!active);
        inputfield.gameObject.SetActive(!active);

        if(!active)
            imagen.color = AppController.instance.colorTheme.detalle4;
    }

    public bool IsSelected()
    {
        return toggleSeleccionMultiple.isOn;
    }

    #endregion


    #region Buscador
    public void SetActiveFolders(bool active)
    {
        foreach (var jugada in listaBotonImagen)
        {
            jugada.gameObject.SetActive(active);
        }

        //Abrir y cerrar la carpeta (o al reves) arregla bugs graficos
        ToggleSeccionJugadas();
        ToggleSeccionJugadas();
    }

    public int SetActiveFolders(string filter)
    {
        int cantResultados = 0;

        foreach (var jugada in listaBotonImagen)
        {
            if (!jugada.GetNombre().Contains(filter.ToUpper()))
                jugada.gameObject.SetActive(false);
            else
            {
                jugada.gameObject.SetActive(true);
                cantResultados++;
            }   
        }

        //Abrir y cerrar la carpeta (o al reves) arregla bugs graficos
        ToggleSeccionJugadas();
        ToggleSeccionJugadas();

        return cantResultados;
    }

    #endregion
}
