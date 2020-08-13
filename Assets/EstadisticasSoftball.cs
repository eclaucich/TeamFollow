using System;
public class EstadisticasSoftball : EstadisticaDeporte
{
    public enum Estadisticas
    {
        Out,
        Mala,
        Buena,
        Carrera,
        HomeRun
    };

    public override string[] GetStatisticsName(int i, AppController.Idiomas idioma)
    {
        string[] res = new string[2]; //res[0]=nombre; res[1]=inicial
        switch (i)
        {
            case (int)Estadisticas.HomeRun:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Home Run";
                    res[1] = "HRN";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Home Run";
                    res[1] = "HRN";
                }
                break;
            case (int)Estadisticas.Buena:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Buena bola";
                    res[1] = "BUE";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Good ball";
                    res[1] = "GOO";
                }
                break;
            case (int)Estadisticas.Carrera:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Carrera";
                    res[1] = "CAR";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Run";
                    res[1] = "RUN";
                }
                break;
            case (int)Estadisticas.Out:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Out";
                    res[1] = "OUT";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Out";
                    res[1] = "OUT";
                }
                break;
            case (int)Estadisticas.Mala:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Mala buena";
                    res[1] = "MAL";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Bad ball";
                    res[1] = "BAD";
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
