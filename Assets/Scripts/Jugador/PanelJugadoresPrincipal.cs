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

    private void Awake()
    {
        jugadores = new List<Jugador>();
        listaBotonJugador = new List<GameObject>();
        parentTransform = GameObject.Find("ListaJugadores").transform;

        ActivarYDesactivarAdviceText();
    }

    private void FixedUpdate()
    {
        flechasScroll.Actualizar(scrollRect, 8, parentTransform.childCount);
        /*if (parentTransform.childCount < 8)
        {
            scrollRect.enabled = false;
            flechaAbajo.SetActive(false);
            flechaArriba.SetActive(false);
        }
        else
        {
            scrollRect.enabled = true;

            if (scrollRect.verticalNormalizedPosition > .95f) flechaArriba.SetActive(false); else flechaArriba.SetActive(true);
            if (scrollRect.verticalNormalizedPosition < 0.05f) flechaAbajo.SetActive(false); else flechaAbajo.SetActive(true);
        }*/
    }

    public void SetPanelJugadores(Equipo equipo_)
    {
        base.Start();

        AppController.instance.overlayPanel.SetNombrePanel("JUGADORES");

        equipo = equipo_;

        jugadores =equipo.GetJugadores();
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
        //botonJugadorGO.GetComponentInChildren<Text>().text = nuevoJugador.GetNombre();  ESTO ESTA MAL HACERLO ACA, LA LINEA DE ABAJO ES MAS CORRECTA
        botonJugadorGO.GetComponent<BotonJugador>().SetNombreJugador(nuevoJugador.GetNombre());

        listaBotonJugador.Add(botonJugadorGO);
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

    public void AbrirPanelConfirmacionBorrado()
    {
        panelConfirmacionBorrado.Activar(AppController.instance.jugadorActual.GetNombre(), botonJugadorFocus);
    }
}
