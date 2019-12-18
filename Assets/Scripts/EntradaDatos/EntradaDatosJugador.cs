using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

/// <summary>
/// 
/// Prefab que se crea por cada jugador en el equipo
/// Se crea cuando se estan ingresando nuevos datos
/// Tiene un texto por cada campo de las estadisticas que se pueden ingresar
/// 
/// </summary>

public class EntradaDatosJugador : MonoBehaviour {

    //Campos que se muestran en por cada jugador
    [SerializeField] private Text nombreJugadorText = null;
    /* [SerializeField] private Text cantidadGolesText;
     [SerializeField] private Text cantidadTirosText;
     [SerializeField] private Text cantidadPasesText;
     [SerializeField] private Text cantidadRecuperacionesText;
     [SerializeField] private Text cantidadPerdidaMarcaText;
     [SerializeField] private Text cantidadPenalesText;
     [SerializeField] private Text cantidadLibresText;
     [SerializeField] private Text cantidadFaltasText;
     [SerializeField] private Text cantidadTarjetasText;
     [SerializeField] private Text cantidadLateralesText;
     [SerializeField] private Text cantidadCornersText;
     [SerializeField] private Text cantidadOutsideText;*/

    [SerializeField] private GameObject botonPrefab = null;

    private Jugador jugador;                                                    //Jugador relacionado con el prefab

    //Campos que se pueden ingresar
    private int goles;
    private int tiros;
    private int pases;
    private int recuperaciones;
    private int perdidaMarca;
    private int penales;
    private int libres;
    private int faltas;
    private int tarjetas;
    private int laterales;
    private int corners;
    private int outside;

    private List<string> listaEstadisticas;

    public void SetEntradaDatosJugador(Jugador jugador_, List<string> listaEstadisticas_)                        //Se setea el prefab
    {
        jugador = jugador_;

        listaEstadisticas = listaEstadisticas_;

        nombreJugadorText.text = jugador.GetNombre();
        SetDetalles();
    }

    public void SetDetalles()                                                   //Se cambian los TEXT de los campos
    {
        /*cantidadGolesText.text = goles.ToString();
        cantidadTirosText.text = tiros.ToString();
        cantidadPasesText.text = pases.ToString();
        cantidadCornersText.text = corners.ToString();
        cantidadFaltasText.text = faltas.ToString();
        cantidadLateralesText.text = laterales.ToString();
        cantidadLibresText.text = libres.ToString();
        cantidadOutsideText.text = outside.ToString();
        cantidadPenalesText.text = penales.ToString();
        cantidadPerdidaMarcaText.text = perdidaMarca.ToString();
        cantidadRecuperacionesText.text = recuperaciones.ToString();
        cantidadTarjetasText.text = tarjetas.ToString();*/

        Transform parent = gameObject.transform;

        for (int i = 0; i < listaEstadisticas.Count; i++)
        {
            GameObject botonGO = Instantiate(botonPrefab);
            botonGO.transform.SetParent(parent);
        }
    }

    /*public void GuardarEntradaDatos(Equipo equipo, string tipo)                              //Al apretar el botón de guardar, se actualizan las estadísticas del jugador, y del equipo
    {
        Estadisticas estadisticas = new Estadisticas();
        if(tipo == "Partido")
        {
            jugador.SetEstadisticasPartido(estadisticas);
            equipo.SetEstadisticasPartido(estadisticas);
        }
        else if(tipo == "Practica")
        {
            jugador.SetEstadisticasPractica(estadisticas);
            equipo.SetEstadisticasPractica(estadisticas);
        }
    }*/

    //Funciones que agregan valores a los campos estadísticos, y actualizan los Text
    public void AgregarGoles()
    {
        goles++;
        SetDetalles();
    }

    public void AgregarTiros()
    {
        tiros++;
        SetDetalles();
    }

    public void AGregarPases()
    {
        pases++;
        SetDetalles();
    }

    public void AgregarRecuperaciones()
    {
        recuperaciones++;
        SetDetalles();
    }

    public void AgregarPerdidaMarca()
    {
        perdidaMarca++;
        SetDetalles();
    }

    public void AgregarPenales()
    {
        penales++;
        SetDetalles();
    }

    public void AgregarLibres()
    {
        libres++;
        SetDetalles();
    }

    public void AgregarFaltas()
    {
        faltas++;
        SetDetalles();
    }

    public void AgregarTarjetas()
    {
        tarjetas++;
        SetDetalles();
    }

    public void AgregarLaterales()
    {
        laterales++;
        SetDetalles();
    }

    public void AgregarCorners()
    {
        corners++;
        SetDetalles();
    }

    public void AgregarOutside()
    {
        outside++;
        SetDetalles();
    }
}
