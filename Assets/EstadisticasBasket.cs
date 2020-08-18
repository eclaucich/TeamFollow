using System;

public class EstadisticasBasket : EstadisticaDeporte
{
    public new enum Estadisticas
    {
        Doble = 16,
        Triple = 17,
        Libre = 18,
        Volcada = 19,
        Pared = 20,
        Bloqueo = 21,
        DisputaGanada = 22,
        DisputaPerdida = 23,
        Tecnica = 24,
        Pase=1,
        Recuperacion=4,
        PerdidaDeMarca=5,
        Falta=7,
        Amarilla=2,
        Roja=3,
    };

    public override string[] GetStatisticsName(int i, AppController.Idiomas idioma)
    {
        string[] res = new string[2]; //res[0]=nombre; res[1]=inicial
        switch (i)
        {
            case (int)Estadisticas.Doble:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Doble";
                    res[1] = "DOB";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Double";
                    res[1] = "DBL";
                }
                break;
            case (int)Estadisticas.Triple:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Triple";
                    res[1] = "TRP";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Triple";
                    res[1] = "TRP";
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
            case (int)Estadisticas.Libre:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Libre";
                    res[1] = "LIB";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Free Throw";
                    res[1] = "FRE";
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
                    res[0] = "Yellow Card";
                    res[1] = "YEL";
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
                    res[0] = "Red Card";
                    res[1] = "RED";
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
            case (int)Estadisticas.Volcada:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Volcada";
                    res[1] = "VOL";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Slam Dunk";
                    res[1] = "SDK";
                }
                break;
            case (int)Estadisticas.DisputaGanada:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Disputa ganada";
                    res[1] = "DGN";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Win Dispute";
                    res[1] = "WDP";
                }
                break;
            case (int)Estadisticas.DisputaPerdida:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Disputa perdida";
                    res[1] = "DPR";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Loss Dispute";
                    res[1] = "LDP";
                }
                break;
            case (int)Estadisticas.Falta:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Falta";
                    res[1] = "FLT";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Foul";
                    res[1] = "FOL";
                }
                break;
            case (int)Estadisticas.Pared:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Pared";
                    res[1] = "PAR";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Wall";
                    res[1] = "WLL";
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
            case (int)Estadisticas.Tecnica:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Tecnica";
                    res[1] = "TEC";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Technical";
                    res[1] = "TEC";
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
