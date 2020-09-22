using System;
public class EstadisticasHandball : EstadisticaDeporte
{
    public new enum Estadisticas
    {
        Pase=0,
        Tiro=301,
        Camina=217,
        PerdidaDePosesion=218,
        Gol=300,
        MeterJugador = -1,
        SacarJugador = -2
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
                    res[1] = "GOl";
                }
                break;
            case (int)Estadisticas.Camina:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Camina";
                    res[1] = "CAM";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Walking";
                    res[1] = "WAL";
                }
                break;
            case (int)Estadisticas.PerdidaDePosesion:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Perdida de posesion";
                    res[1] = "PDP";
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
            case (int)Estadisticas.Tiro:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Tiro";
                    res[1] = "TRO";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Shot";
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
