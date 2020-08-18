using System;
public class EstadisticasVoley : EstadisticaDeporte
{
    public new enum Estadisticas
    {
        Recepcion = 64,
        Bloqueada = 65,
        MalaRecepcion = 66,
        ToqueDeRed = 67,
        ToqueDeBarilla = 68,
        Llevada = 69,
        Invasion = 70,
        DobleGolpe = 71,
        Saque = 72,
        ErrorDeSaque = 73,
        Pase =1,
        Remate=51,
        Ace=29,
        Amarilla=2,
        Roja=3,
    };

    public override string[] GetStatisticsName(int i, AppController.Idiomas idioma)
    {
        string[] res = new string[2]; //res[0]=nombre; res[1]=inicial
        switch (i)
        {
            case (int)Estadisticas.MalaRecepcion:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Mala recepcion";
                    res[1] = "MRC";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Bad reception";
                    res[1] = "BRC";
                }
                break;
            case (int)Estadisticas.ToqueDeRed:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Toque de red";
                    res[1] = "TDR";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Net touch";
                    res[1] = "NTC";
                }
                break;
            case (int)Estadisticas.Bloqueada:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Bloqueada";
                    res[1] = "BLO";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Block";
                    res[1] = "BLK";
                }
                break;
            case (int)Estadisticas.Llevada:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Llevada";
                    res[1] = "LLV";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "---";
                    res[1] = "---";
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
                    res[0] = "Smash";
                    res[1] = "SMA";
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
            case (int)Estadisticas.Recepcion:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Recepcion";
                    res[1] = "REC";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Reception";
                    res[1] = "REC";
                }
                break;
            case (int)Estadisticas.ToqueDeBarilla:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Toque de barilla";
                    res[1] = "TDB";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "---";
                    res[1] = "---";
                }
                break;
            case (int)Estadisticas.Ace:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Ace";
                    res[1] = "ACE";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Ace";
                    res[1] = "ACE";
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
            case (int)Estadisticas.DobleGolpe:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Doble golpe";
                    res[1] = "DBG";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Double hit";
                    res[1] = "DBH";
                }
                break;
            case (int)Estadisticas.ErrorDeSaque:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Error de saque";
                    res[1] = "ESQ";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Serve error";
                    res[1] = "---";
                }
                break;
            case (int)Estadisticas.Invasion:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Invasion";
                    res[1] = "INV";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Invasion";
                    res[1] = "INV";
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
            case (int)Estadisticas.Saque:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Saque";
                    res[1] = "SAQ";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Serve";
                    res[1] = "SRV";
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
