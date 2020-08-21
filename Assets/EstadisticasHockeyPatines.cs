using System;
public class EstadisticasHockeyPatines : EstadisticaDeporte
{
    public new enum Estadisticas
    {
        Remate = 51,
        Mareada = 52,
        FaltaPatin = 53,
        Azul = 54,
        RecuperacionDeBocha = 55,
        Disputa = 56,
        BochaFueraDeCancha = 57,
        FaltaDePenal = 58,
        FaltaDeLibre = 59,
        Altura = 60,
        CincoSegundos = 61,
        Pase =1,
        Bloqueo=21,
        ProposicionDeJugada=10,
        Roja=3,
        PerdidaDeMarca=5,
        Tiro=11,
        Gol=0,
        MeterJugador = -1,
        SacarJugador = -2
    };

    public override string[] GetStatisticsName(int i, AppController.Idiomas idioma)
    {
        string[] res = new string[2]; //res[0]=nombre; res[1]=inicial
        switch (i)
        {
            case (int)Estadisticas.Mareada:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Mareada";
                    res[1] = "MAR";
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
                    res[0] = "---";
                    res[1] = "---";
                }
                break;
            case (int)Estadisticas.Bloqueo:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Bloqueo";
                    res[1] = "BLO";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Block";
                    res[1] = "BLK";
                }
                break;
            case (int)Estadisticas.Remate:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Remate";
                    res[1] = "REM";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "---";
                    res[1] = "---";
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
            case (int)Estadisticas.FaltaPatin:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Falta de patin";
                    res[1] = "FPT";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "---";
                    res[1] = "---";
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
                    res[0] = "Red card";
                    res[1] = "RED";
                }
                break;
            case (int)Estadisticas.Altura:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Altura";
                    res[1] = "ALT";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "---";
                    res[1] = "---";
                }
                break;
            case (int)Estadisticas.Azul:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Azul";
                    res[1] = "AZL";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Blue card";
                    res[1] = "BLU";
                }
                break;
            case (int)Estadisticas.BochaFueraDeCancha:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Bocha fuera de cancha";
                    res[1] = "BFC";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Ball outside court";
                    res[1] = "BOC";
                }
                break;
            case (int)Estadisticas.CincoSegundos:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Cinco segundos";
                    res[1] = "5SG";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Five seconds";
                    res[1] = "5SC";
                }
                break;
            case (int)Estadisticas.Disputa:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Disputa";
                    res[1] = "DIS";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Dispute";
                    res[1] = "DIS";
                }
                break;
            case (int)Estadisticas.FaltaDeLibre:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Falta de libre";
                    res[1] = "FLB";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Free kick foul";
                    res[1] = "FKF";
                }
                break;
            case (int)Estadisticas.FaltaDePenal:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Falta de penal";
                    res[1] = "FDP";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Penalty foul";
                    res[1] = "PFL";
                }
                break;
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
            case (int)Estadisticas.RecuperacionDeBocha:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Recupertacion de bocha";
                    res[1] = "RDB";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Ball recovery";
                    res[1] = "BRV";
                }
                break;
            case (int)Estadisticas.Tiro:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Tiro";
                    res[1] = "TRO";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Shoot";
                    res[1] = "SHT";
                }
                break;
            default:
                res[0] = "ERROR";
                res[1] = "ERROR";
                break;
        }

        res[0] = res[0].ToUpper();

        return res;
    }

    public override int GetSize()
    {
        return Enum.GetNames(typeof(Estadisticas)).Length;
    }

    public override string GetValueAtIndex(int i)
    {
        string[] nombresEnum = Enum.GetNames(typeof(Estadisticas));

        return nombresEnum[i].ToUpper();
    }

    public override Array GetEstadisticas()
    {
        return Enum.GetValues(typeof(Estadisticas));
    }
}
