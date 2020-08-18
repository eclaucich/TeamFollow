using System;
public class EstadisticasPadel : EstadisticaDeporte
{
    public new enum Estadisticas
    {
        WinnerDrive = 25,
        WinnerReves = 26,
        ErrorNoForzadoDrive = 27,
        ErrorNoForzadoReves = 28,
        Ace = 29,
        DobleFalta = 30,
        WinnerDrop = 31,
        ErrorNoForzadoDrop = 32,
        Sacada = 44
    };

    public override string[] GetStatisticsName(int i, AppController.Idiomas idioma)
    {
        string[] res = new string[2]; //res[0]=nombre; res[1]=inicial
        switch (i)
        {
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
            case (int)Estadisticas.DobleFalta:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Doble falta";
                    res[1] = "DBF";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Double foul";
                    res[1] = "DBF";
                }
                break;
            case (int)Estadisticas.ErrorNoForzadoDrive:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Error no forzado Drive";
                    res[1] = "EDR";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Drive nforced error";
                    res[1] = "DVE";
                }
                break;
            case (int)Estadisticas.Sacada:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Sacada de cancha";
                    res[1] = "SDC";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "---";
                    res[1] = "---";
                }
                break;
            case (int)Estadisticas.WinnerDrive:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Winner drive";
                    res[1] = "WDR";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Drive winner";
                    res[1] = "DRW";
                }
                break;
            case (int)Estadisticas.WinnerReves:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Winner reves";
                    res[1] = "WRV";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Backhand winner";
                    res[1] = "BKW";
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
