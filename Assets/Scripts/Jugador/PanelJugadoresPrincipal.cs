using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelJugadoresPrincipal : Panel {

    [SerializeField] private GameObject botonJugadorPrefab = null;
    private List<Jugador> jugadores;
    private Equipo equipo;
    private List<GameObject> listaBotonJugador;

    [SerializeField] private GameObject nuevoJugadorEntryPrefab = null;
    [SerializeField] private Text inputNombreNuevoJugadorText = null;
    [SerializeField] private GameObject botonNuevoJugador = null;
    [SerializeField] private Text adviceText = null;
    [SerializeField] private Text mensajeError = null;

    private Transform parentTransform;

    private TouchScreenKeyboard keyboard;
    private string nombreNuevoJugador;
    private bool newPlayerButtonPressed = false;

    private void Awake()
    {
        jugadores = new List<Jugador>();
        listaBotonJugador = new List<GameObject>();
        parentTransform = GameObject.Find("ListaJugadores").transform;

        ActivarYDesactivarAdviceText();
    }

    private void Update()
    {
        if(keyboard != null && newPlayerButtonPressed)
        {
            if (keyboard.status == TouchScreenKeyboard.Status.Done)
            {
                nombreNuevoJugador = keyboard.text;
                newPlayerButtonPressed = false;
            }
        }
    }

    public void SetPanelJugadores(Equipo equipo_)
    {
        equipo = equipo_;

        jugadores = equipo.GetJugadores();
        DetallarJugadores();

        mensajeError.gameObject.SetActive(false);
        nuevoJugadorEntryPrefab.SetActive(false);
        botonNuevoJugador.SetActive(true);
        ActivarYDesactivarAdviceText();

        inputNombreNuevoJugadorText.text = "";
    }


    private void DetallarJugadores()
    {
        BorrarDetalles();
        for (int i = 0; i < jugadores.Count; i++)
        {
            AgregarNuevoDetalle(jugadores[i]);
        }
    }


    private void AgregarNuevoDetalle(Jugador nuevoJugador)
    {  
        GameObject botonJugadorGO = Instantiate(botonJugadorPrefab, parentTransform, false);
        //botonJugadorGO.GetComponentInChildren<Text>().text = nuevoJugador.GetNombre();  ESTO ESTA MAL HACERLO ACA, LA LINEA DE ABAJO ES MAS CORRECTA
        botonJugadorGO.GetComponent<BotonJugador>().SetNombreJugador(nuevoJugador.GetNombre());

        listaBotonJugador.Add(botonJugadorGO);
    }


    private void BorrarDetalles()
    {
        for (int i = 0; i < listaBotonJugador.Count; i++)
        {
            Destroy(listaBotonJugador[i]);
        }

        listaBotonJugador.Clear();
    }


    public void DesplegarNuevoJugadorEntry()
    {
        keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
        newPlayerButtonPressed = true;
    }


    public void GuardarNuevoJugador(string nombre, int peso, int altura)
    {
        Debug.Log("PRINCIPAL");
        Jugador nuevoJugador = new Jugador(new InfoJugador());
        equipo.NuevoJugador(nuevoJugador);

        AgregarNuevoDetalle(nuevoJugador);

        ActivarYDesactivarAdviceText();
    }

    public void BorrarBotonJugador(GameObject botonJugador)
    {
        Destroy(botonJugador);
        listaBotonJugador.Remove(botonJugador);
        ActivarYDesactivarAdviceText();
    }


    private void ActivarYDesactivarAdviceText()
    {
        if (listaBotonJugador.Count == 0)
        {
            adviceText.gameObject.SetActive(true);
        }
        else
        {
            adviceText.gameObject.SetActive(false);
        }
    }
}
