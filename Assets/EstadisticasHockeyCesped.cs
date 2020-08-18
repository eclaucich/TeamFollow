using System;
public class EstadisticasHockeyCesped : EstadisticaDeporte
{
    public new enum Estadisticas
    {
        CornerCorto = 45,
        CornerLargo = 46,
        BochaPeligrosa = 47,
        TarjetaVerde = 48,
        TarjetaAmarilla = 49,
        TarjetaRoja = 50,
        Pase =1,
        Recuperacion=4,
        PerdidaDeMarca=5,
        Intercepcion=8,
        Lateral=14,
        Gol=0,
        Penal=15
    }

    public override string[] GetStatisticsName(int i, AppController.Idiomas idioma)
    {
        string[] res = new string[2]; //res[0]=nombre; res[1]=inicial
        switch (i)
        {
            case (int)Estadisticas.BochaPeligrosa:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "BochaPeligrosa";
                    res[1] = "BPL";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Dangerous ball";
                    res[1] = "DBL";
                }
                break;
            case (int)Estadisticas.CornerCorto:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Corner corto";
                    res[1] = "CCT";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Short Corner";
                    res[1] = "SCR";
                }
                break;
            case (int)Estadisticas.CornerLargo:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Corner Largo";
                    res[1] = "CLG";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Long Corner";
                    res[1] = "LCR";
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
                    res[0] = "Side pass";
                    res[1] = "SPS";
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
            case (int)Estadisticas.TarjetaAmarilla:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Amarilla";
                    res[1] = "AMA";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Yellow card";
                    res[1] = "YEL";
                }
                break;
            case (int)Estadisticas.TarjetaRoja:
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
            case (int)Estadisticas.TarjetaVerde:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Verde";
                    res[1] = "VER";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Green card";
                    res[1] = "GRE";
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
