using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotonCarpetaJugada : MonoBehaviour
{
    [SerializeField] private Text nombreCarpetaText = null;
    [SerializeField] private GameObject botonJugadaPrefab = null;
    [SerializeField] private InputField inputfield = null;
    [SerializeField] private PanelPrincipalBiblioteca panelPrincipalBiblioteca = null;
    [SerializeField] private GameObject seccionJugadasPrefab = null;

    private CarpetaJugada carpeta;
    private GameObject seccionJugadas;
    private Animator animator;

    private void Start()
    {
        inputfield.onEndEdit.AddListener(VerificarEdicionNombreCarpeta);
        animator = GetComponent<Animator>();
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
        
        foreach (var jugada in _carpeta.GetListaJugadas())
        {
            Debug.Log("NOMBRE: " + jugada.GetNombre());
            Debug.Log("CATEGORIA: " + jugada.GetCategoria());

            GameObject go = Instantiate(botonJugadaPrefab, seccionJugadas.transform, false);
            go.SetActive(true);
            BotonImagen botonGO = go.GetComponent<BotonImagen>();
            botonGO.SetJugadaFocus(jugada);
        }
    }

    private void VerificarEdicionNombreCarpeta(string _nuevoNombre)
    {
        if (_nuevoNombre != nombreCarpetaText.text)
        {
            if (AppController.instance.VerificarNombreCarpeta(_nuevoNombre.ToUpper()))
            {
                Debug.Log("NOMBRE EXISTENTE: " + _nuevoNombre);
                panelPrincipalBiblioteca.ActivarMensajeError();
                return;
            }
            else
            {
                Debug.Log("NOMBRE CAMBIADO");
                panelPrincipalBiblioteca.ActivarMensajeCambioNombreExitoso();
                carpeta.SetNombre(_nuevoNombre);
                SaveSystem.EditarCarpeta(nombreCarpetaText.text, _nuevoNombre.ToUpper());
                nombreCarpetaText.text = _nuevoNombre.ToUpper();
            }
        }
    }

    public void ToggleSeccionJugadas()
    {
        if (seccionJugadas != null)
        {
            bool active = !seccionJugadas.activeSelf;
            seccionJugadas.SetActive(active);
            if(animator) animator.SetBool("open", active);
        }
        Canvas.ForceUpdateCanvases();
    }

    public CarpetaJugada GetCarpeta()
    {
        return carpeta;
    }
}
