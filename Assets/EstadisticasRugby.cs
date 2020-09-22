using System;
public class EstadisticasRugby : EstadisticaDeporte
{
    public new enum Estadisticas
    {
        Takle = 3,
        KnockOn = 104,
        Drop = 105,
        PassForward = 310,
        Try = 412,
        Conversion = 413,
        Falta=201,
        Lateral=304,
        Offside=200,
        Penal=402,
        Amarilla=400,
        Roja=401,
        Intercepcion=202,
        MeterJugador = -1,
        SacarJugador = -2
    };

    public override string[] GetStatisticsName(int i, AppController.Idiomas idioma)
    {
        string[] res = new string[2]; //res[0]=nombre; res[1]=inicial
        switch (i)
        {
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
            case (int)Estadisticas.Lateral:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Lateral";
                    res[1] = "LAT";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "---";
                    res[1] = "---";
                }
                break;
            case (int)Estadisticas.KnockOn:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Knock On";
                    res[1] = "KON";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Knock On";
                    res[1] = "KON";
                }
                break;
            case (int)Estadisticas.Drop:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Drop";
                    res[1] = "DRP";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Drop";
                    res[1] = "DRP";
                }
                break;
            case (int)Estadisticas.Takle:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Takle";
                    res[1] = "TKL";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Tackle";
                    res[1] = "TKL";
                }
                break;
            case (int)Estadisticas.Offside:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Offside";
                    res[1] = "OFS";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Offside";
                    res[1] = "OFS";
                }
                break;
            case (int)Estadisticas.PassForward:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Pase forward";
                    res[1] = "PSF";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Pass forward";
                    res[1] = "PSF";
                }
                break;
            case (int)Estadisticas.Try:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Try";
                    res[1] = "TRY";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Try";
                    res[1] = "TRY";
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
            case (int)Estadisticas.Conversion:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Conversion";
                    res[1] = "CON";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Convertion";
                    res[1] = "CON";
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
            case (int)Estadisticas.Amarilla:
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
