using System.Collections.Generic;
using System;

public class InfoJugador 
{
    public Dictionary<string, string> infoObligatoria;
    public Dictionary<string, string> infoString;
    public Dictionary<string, int> infoInt;
    public Dictionary<string, string> infoEspecial;
    public DateTime fechaNac;

    public InfoJugador()
    {
        infoObligatoria = new Dictionary<string, string>();
        infoString = new Dictionary<string, string>();
        infoInt = new Dictionary<string, int>();
        infoEspecial = new Dictionary<string, string>();

        infoObligatoria["Nombre"] = "nombre";

        infoString["Mail"] = "";
        infoString["Alergias"] = "";
        infoString["Direccion"] = "";
        infoString["Ciudad"] = "";
        infoString["Provincia"] = "";
        infoString["Categoria"] = "";

        infoString["DNI"] = "0";
        infoString["Altura"] = "0";
        infoString["Numero Celular"] = "0";
        infoString["Numero Contacto Emergencia"] = "0";

        /*infoInt["DNI"] = 0;
        infoInt["Altura"] = 0;
        infoInt["Numero Celular"] = 0;
        infoInt["Numero Contacto Emergencia"] = 0;*/

        infoEspecial["Sexo"] = "";
        infoEspecial["Factor Sanguineo"] = "";
        infoEspecial["Ficha Medica"] = "";

        fechaNac = DateTime.MinValue;
    }

    public InfoJugador(SaveDataJugador dataJugador)
    {
        infoObligatoria = dataJugador.GetInfoObligatoria();
        infoString = dataJugador.GetInfoString();
        infoInt = dataJugador.GetInfoInt();
        infoEspecial = dataJugador.GetInfoEspecial();
        fechaNac = dataJugador.GetFechaNacimiento();
    }

    public void NuevaInfo(InfoJugador info_)
    {
        infoObligatoria = info_.infoObligatoria;
        infoString = info_.infoString;
        infoInt = info_.infoInt;
        infoEspecial = info_.infoEspecial;
        fechaNac = info_.fechaNac;
    }

    public Dictionary<string, string> GetInfoObligatoria()
    {
        return infoObligatoria;
    }

    public Dictionary<string, string> GetInfoString()
    {
        return infoString;
    }

    public Dictionary<string , int> GetInfoInt()
    {
        return infoInt;
    }

    public Dictionary<string, string> GetInfoEspecial()
    {
        return infoEspecial;
    }

    public DateTime GetFechaNac()
    {
        return fechaNac;
    }

    public string GetNombre()
    {
        return infoObligatoria["Nombre"];
    } 

    public void SetEspecial(string categoria, string valor)
    {
        infoEspecial[categoria] = valor;
    }

    public void SetInfoObligatoria(InputPrefab input)
    {
        infoObligatoria[input.GetNombreCategoria()] = input.GetValorCategoria();
    }

    public void SetInfoString(InputPrefab input)
    {
        infoString[input.GetNombreCategoria()] = input.GetValorCategoria();
    }

    public void SetInfoObligatoriaPlaceholder(InputPrefab input)
    {
        infoObligatoria[input.GetNombreCategoria()] = input.GetPlaceholder();
    }

    public void SetInfoStringPlaceholder(InputPrefab input)
    {
        infoString[input.GetNombreCategoria()] = input.GetPlaceholder();
    }

    public void SetInfoInt(InputPrefab input)
    {
        //infoInt[input.GetNombreCategoria()] = input.GetValorCategoria();
    }

    public void SetInfoEspecial(InputPrefab input)
    {
        infoEspecial[input.GetNombreCategoria()] = input.GetValorCategoria();
    }

    public void SetFechaNac(DateTime dt)
    {
        fechaNac = dt;
    }

    public void SetFechaNac(string fecha)
    {
        string s_day = ""; string s_month = ""; string s_year = "";
        int aux = 0;
        for (int i = 0; i < fecha.Length; i++)
        {
            if (aux == 0)
            {
                if (fecha[i] != '/')
                    s_day = s_day + fecha[i];
                else aux++;
            }
            else if (aux == 1)
            {
                if (fecha[i] != '/')
                    s_month = s_month + fecha[i];
                else aux++;
            }
            else
                s_year = s_year + fecha[i];
        }
        int day = int.Parse(s_day);
        int month = int.Parse(s_month);
        int year = int.Parse(s_year);

        fechaNac = new DateTime(year, month, day);
    }
}
