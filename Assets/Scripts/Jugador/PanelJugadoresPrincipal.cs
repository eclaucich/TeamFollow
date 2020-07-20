using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelJugadoresPrincipal : Panel {

    [SerializeField] private GameObject botonJugadorPrefab = null;
    private List<Jugador> jugadores;
    private Equipo equipo;
    private List<GameObject> listaBotonJugador;
    
    [SerializeField] private GameObject botonNuevoJugador = null;
    [SerializeField] private Text adviceText = null;

    [SerializeField] private ScrollRect scrollRect = null;
    [SerializeField] private FlechasScroll flechasScroll = null;

    private Transform parentTransform;

    [SerializeField] private ConfirmacionBorradoJugador panelConfirmacionBorrado = null;
    private GameObject botonJugadorFocus;

    private float prefabHeight;
    private int cantMinima;

    private void Awake()
    {
        jugadores = new List<Jugador>();
        listaBotonJugador = new List<GameObject>();
        parentTransform = GameObject.Find("ListaJugadores").transform;

        ActivarYDesactivarAdviceText();

        prefabHeight = botonJugadorPrefab.GetComponent<RectTransform>().rect.height;
    }

    private void FixedUpdate()
    {
        flechasScroll.Actualizar(scrollRect, cantMinima, listaBotonJugador.Count);
    }

    public void SetPanelJugadores(Equipo equipo_)
    {
        equipo = equipo_;

        AppController.instance.overlayPanel.SetNombrePanel(equipo.GetNombre());

        jugadores = equipo.GetJugadores();
        DetallarJugadores();

        botonNuevoJugador.SetActive(true);
        ActivarYDesactivarAdviceText();
    }


    private void DetallarJugadores()
    {
        BorrarDetalles();
        foreach (var jugador in jugadores)
            AgregarNuevoDetalle(jugador);
    }


    private void AgregarNuevoDetalle(Jugador nuevoJugador)
    {
        GameObject botonJugadorGO = Instantiate(botonJugadorPrefab, parentTransform, false);
        botonJugadorGO.SetActive(true);
        //botonJugadorGO.GetComponentInChildren<Text>().text = nuevoJugador.GetNombre();  ESTO ESTA MAL HACERLO ACA, LA LINEA DE ABAJO ES MAS CORRECTA
        botonJugadorGO.GetComponent<BotonJugador>().SetNombreJugador(nuevoJugador.GetNombre());

        listaBotonJugador.Add(botonJugadorGO);

        cantMinima = (int)(scrollRect.GetComponent<RectTransform>().rect.height / (prefabHeight + parentTransform.GetComponent<VerticalLayoutGroup>().spacing));
    }


    private void BorrarDetalles()
    {
        for (int i = 0; i < listaBotonJugador.Count; i++)
            Destroy(listaBotonJugador[i]);

        listaBotonJugador.Clear();
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

    public void SetBotonJugadorFocus(GameObject botonJugador_)
    {
        botonJugadorFocus = botonJugador_;
    }

    public BotonJugador GetBotonJugadorFocus()
    {
        return botonJugadorFocus.GetComponent<BotonJugador>();
    }


    public void AbrirPanelConfirmacionBorrado()
    {
        panelConfirmacionBorrado.Activar(AppController.instance.jugadorActual.GetNombre(), botonJugadorFocus);
    }

    public ConfirmacionBorradoJugador GetPanelConfirmacionBorrado()
    {
        return panelConfirmacionBorrado;
    }
}
