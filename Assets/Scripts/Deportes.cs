using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deportes : MonoBehaviour
{
    public static Deportes instance = null;

    [SerializeField] private List<Sprite> iconosDeportes = null;
    [SerializeField] private List<Sprite> canchasBasket = null;
    [SerializeField] private List<Sprite> canchasFutbol = null;
    [SerializeField] private List<Sprite> canchasHandball = null;
    [SerializeField] private List<Sprite> canchasHockeyCesped = null;
    [SerializeField] private List<Sprite> canchasHockeyPatines = null;
    [SerializeField] private List<Sprite> canchasPadel = null;
    [SerializeField] private List<Sprite> canchasRugby = null;
    [SerializeField] private List<Sprite> canchasSoftball = null;
    [SerializeField] private List<Sprite> canchasTenis = null;
    [SerializeField] private List<Sprite> canchasVoley = null;

    public enum DeporteEnum
    {
        Basket,
        Futbol,
        Handball,
        HockeyCesped,
        HockeyPatines,
        Padel,
        Rugby,    
        Softball,
        Tenis,
        Voley,

        Ninguno,
    }

    public enum TipoCanchasEnum
    {
        CanchaEntera,
        MediaCancha
    }

    private void Awake()
    {
        if (instance == null)                                                                
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }

        DontDestroyOnLoad(this);
    }

    public int GetCantDeportes()
    {
        return Enum.GetNames(typeof(DeporteEnum)).Length;
    }

    public string GetDisplayName(DeporteEnum _deporte, AppController.Idiomas _idioma)
    {
        string nombreDeporte = string.Empty;
        switch (_deporte)
        {
            case DeporteEnum.Futbol:
                if (_idioma == AppController.Idiomas.Español)
                    nombreDeporte = "Futbol";
                else if (_idioma == AppController.Idiomas.Ingles)
                    nombreDeporte = "Football";
                break;
            case DeporteEnum.Basket:
                if (_idioma == AppController.Idiomas.Español)
                    nombreDeporte = "Basket";
                else if (_idioma == AppController.Idiomas.Ingles)
                    nombreDeporte = "Basket";
                break;
            case DeporteEnum.Handball:
                if (_idioma == AppController.Idiomas.Español)
                    nombreDeporte = "Handball";
                else if (_idioma == AppController.Idiomas.Ingles)
                    nombreDeporte = "Handball";
                break;
            case DeporteEnum.HockeyCesped:
                if (_idioma == AppController.Idiomas.Español)
                    nombreDeporte = "Hockey cesped";
                else if (_idioma == AppController.Idiomas.Ingles)
                    nombreDeporte = "Grass Hockey";
                break;
            case DeporteEnum.HockeyPatines:
                if (_idioma == AppController.Idiomas.Español)
                    nombreDeporte = "Hockey patines";
                else if (_idioma == AppController.Idiomas.Ingles)
                    nombreDeporte = "Roller Hockey";
                break;
            case DeporteEnum.Padel:
                if (_idioma == AppController.Idiomas.Español)
                    nombreDeporte = "Padel";
                else if (_idioma == AppController.Idiomas.Ingles)
                    nombreDeporte = "Paddle";
                break;
            case DeporteEnum.Rugby:
                if (_idioma == AppController.Idiomas.Español)
                    nombreDeporte = "Rugby";
                else if (_idioma == AppController.Idiomas.Ingles)
                    nombreDeporte = "Rugby";
                break;
            case DeporteEnum.Softball:
                if (_idioma == AppController.Idiomas.Español)
                    nombreDeporte = "Softball";
                else if (_idioma == AppController.Idiomas.Ingles)
                    nombreDeporte = "Softball";
                break;
            case DeporteEnum.Tenis:
                if (_idioma == AppController.Idiomas.Español)
                    nombreDeporte = "Tenis";
                else if (_idioma == AppController.Idiomas.Ingles)
                    nombreDeporte = "Tennis";
                break;
            case DeporteEnum.Voley:
                if (_idioma == AppController.Idiomas.Español)
                    nombreDeporte = "Voley";
                else if (_idioma == AppController.Idiomas.Ingles)
                    nombreDeporte = "Volley";
                break;
            case DeporteEnum.Ninguno:
                if (_idioma == AppController.Idiomas.Español)
                    nombreDeporte = "Ninguno";
                else if (_idioma == AppController.Idiomas.Ingles)
                    nombreDeporte = "None";
                break;

            default:
                nombreDeporte = "ERROR NOMBRE DEPORTE";
                break;
        }

        return nombreDeporte;
    }

    public EstadisticaDeporte GetEstadisticaDeporte(DeporteEnum deporte)
    {
        switch (deporte)
        {
            case DeporteEnum.Futbol:
                return new EstadisticasFutbol();
            case DeporteEnum.Basket:
                return new EstadisticasBasket();
            case DeporteEnum.Handball:
                return new EstadisticasHandball();
            case DeporteEnum.HockeyCesped:
                return new EstadisticasHockeyCesped();
            case DeporteEnum.HockeyPatines:
                return new EstadisticasHockeyPatines();
            case DeporteEnum.Padel:
                return new EstadisticasPadel();
            case DeporteEnum.Rugby:
                return new EstadisticasRugby();
            case DeporteEnum.Softball:
                return new EstadisticasSoftball();
            case DeporteEnum.Tenis:
                return new EstadisticasTenis();
            case DeporteEnum.Voley:
                return new EstadisticasVoley();
            default:
                return null;

        }
    }

    public Sprite GetImagenCancha(DeporteEnum _deporte, TipoCanchasEnum _tipoCancha)
    {
        switch (_deporte)
        {
            case DeporteEnum.Basket:
                if (_tipoCancha == TipoCanchasEnum.CanchaEntera)
                    return canchasBasket[0];
                else
                    return canchasBasket[1];

            case DeporteEnum.Futbol:
                if (_tipoCancha == TipoCanchasEnum.CanchaEntera)
                    return canchasFutbol[0];
                else
                    return canchasFutbol[1];

            case DeporteEnum.Handball:
                if (_tipoCancha == TipoCanchasEnum.CanchaEntera)
                    return canchasHandball[0];
                else
                    return canchasHandball[1];

            case DeporteEnum.HockeyCesped:
                if (_tipoCancha == TipoCanchasEnum.CanchaEntera)
                    return canchasHockeyCesped[0];
                else
                    return canchasHockeyCesped[1];

            case DeporteEnum.HockeyPatines:
                if (_tipoCancha == TipoCanchasEnum.CanchaEntera)
                    return canchasHockeyPatines[0];
                else
                    return canchasHockeyPatines[1];

            case DeporteEnum.Padel:
                if (_tipoCancha == TipoCanchasEnum.CanchaEntera)
                    return canchasPadel[0];
                else
                    return canchasPadel[1];

            case DeporteEnum.Rugby:
                if (_tipoCancha == TipoCanchasEnum.CanchaEntera)
                    return canchasRugby[0];
                else
                    return canchasRugby[1];

            case DeporteEnum.Softball:
                if (_tipoCancha == TipoCanchasEnum.CanchaEntera)
                    return canchasSoftball[0];
                else
                    return canchasSoftball[1];

            case DeporteEnum.Tenis:
                if (_tipoCancha == TipoCanchasEnum.CanchaEntera)
                    return canchasTenis[0];
                else
                    return canchasTenis[1];

            case DeporteEnum.Voley:
                if (_tipoCancha == TipoCanchasEnum.CanchaEntera)
                    return canchasVoley[0];
                else
                    return canchasVoley[1];

            default:
                return null;
        }
    }

    public Sprite GetIconoDeporte(DeporteEnum _deporte)
    {
        return iconosDeportes[(int)_deporte];
    }
}
