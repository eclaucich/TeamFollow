using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deportes : MonoBehaviour
{
    public static Deportes instance = null;

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

        NULL,
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
            /*case DeporteEnum.Handball:
               
            case DeporteEnum.HockeyCesped:
                
            case DeporteEnum.HockeyPatines:
                
            case DeporteEnum.Padel:
                
            case DeporteEnum.Rugby:
                
            case DeporteEnum.Softball:
                
            case DeporteEnum.Tenis:

            case DeporteEnum.Voley:*/

            default:
                return null;

        }
    }
}
