﻿using System;

public class EstadisticasFutbol : EstadisticaDeporte
{
    public enum Estadisticas
    {
        Gol,
        Pase,
        Amarilla,
        Roja,
        Recuperacion,
        PerdidaDeMarca,
        Offside,
        Falta,
        Intercepcion,
        PerdidaDeBalon,
        ProposicionDeJugada,
        Tiro,
        TiroLibre,
        Corner,
        Lateral,
        Penal
    };

    public override string[] GetStatisticsName(int i, AppController.Idiomas idioma)
    {
        string[] res = new string[2]; //res[0]=nombre; res[1]=inicial
        switch (i)
        {
            case (int)Estadisticas.Gol:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Gol";
                    res[1] = "GOL";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Goal";
                    res[1] = "GOL";
                }
                break;
            case (int)Estadisticas.Pase:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Pase";
                    res[1] = "PAS";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Pass";
                    res[1] = "PAS";
                }
                break;
            case (int)Estadisticas.Amarilla:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Amarilla";
                    res[1] = "AMA";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Yellow";
                    res[1] = "YEW";
                }
                break;
            case (int)Estadisticas.Roja:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Roja";
                    res[1] = "ROJ";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Red";
                    res[1] = "RED";
                }
                break;
            case (int)Estadisticas.Corner:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Corner";
                    res[1] = "COR";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Corner";
                    res[1] = "COR";
                }
                break;
            case (int)Estadisticas.Falta:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Falta";
                    res[1] = "FAL";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Foul";
                    res[1] = "FOU";
                }
                break;
            case (int)Estadisticas.Intercepcion:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Intercepcion";
                    res[1] = "INT";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Interception";
                    res[1] = "INT";
                }
                break;
            case (int)Estadisticas.Lateral:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Lateral";
                    res[1] = "LAT";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Throw-in";
                    res[1] = "TRI";
                }
                break;
            case (int)Estadisticas.Offside:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Offside";
                    res[1] = "OFF";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Offside";
                    res[1] = "OFF";
                }
                break;
            case (int)Estadisticas.Penal:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Penal";
                    res[1] = "PEN";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Penalty";
                    res[1] = "PEN";
                }
                break;
            case (int)Estadisticas.PerdidaDeBalon:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Perdida de balon";
                    res[1] = "PDB";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Lost ball";
                    res[1] = "LSB";
                }
                break;
            case (int)Estadisticas.PerdidaDeMarca:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Perdida de marca";
                    res[1] = "PDM";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "---";
                    res[1] = "---";
                }
                break;
            case (int)Estadisticas.ProposicionDeJugada:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Proposicion de jugada";
                    res[1] = "PDJ";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Play proposal";
                    res[1] = "PPR";
                }
                break;
            case (int)Estadisticas.Recuperacion:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Recuperacion";
                    res[1] = "REC";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Recovery";
                    res[1] = "REC";
                }
                break;
            case (int)Estadisticas.Tiro:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Tiro";
                    res[1] = "TIR";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Kick";
                    res[1] = "KCK";
                }
                break;
            case (int)Estadisticas.TiroLibre:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Tiro libre";
                    res[1] = "TLB";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Free Kick";
                    res[1] = "FKC";
                }
                break;
            default:
                res[0] = "ERROR";
                res[1] = "ERROR";
                break;
        }

        return res;
    }
    
    public override int GetSize()
    {
        return Enum.GetNames(typeof(Estadisticas)).Length;
    }

    public override string GetValueAtIndex(int i)
    {
        string[] nombresEnum = Enum.GetNames(typeof(Estadisticas));

        return nombresEnum[i];
    }
}
