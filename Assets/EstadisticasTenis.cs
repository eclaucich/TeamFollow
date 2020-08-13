using System;
public class EstadisticasTenis : EstadisticaDeporte
{
    public enum Estadisticas
    {
        WinnerDrive,
        WinnerReves,
        ErrorNoForzadoDrive,
        ErrorNoForzadoReves,
        Ace,
        DobleFalta,
        WinnerDrop,
        ErrorNoForzadoDrop
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
            case (int)Estadisticas.ErrorNoForzadoDrop:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Error no forzado drop";
                    res[1] = "EDP";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Drop unforced error";
                    res[1] = "DPE";
                }
                break;
            case (int)Estadisticas.ErrorNoForzadoReves:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Error no forzado reves";
                    res[1] = "ERV";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Backhand unforced error";
                    res[1] = "EBK";
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
            case (int)Estadisticas.WinnerDrop:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Winner drop";
                    res[1] = "WDP";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Drop winner";
                    res[1] = "DPW";
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
}
