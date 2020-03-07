using System.Collections.Generic;
using System;

public class InfoJugador 
{
    public Dictionary<string, string> infoString;
    public Dictionary<string, int> infoInt;
    public Dictionary<string, string> infoEspecial;
    public DateTime fechaNac;

    public InfoJugador()
    {
        infoString = new Dictionary<string, string>();
        infoInt = new Dictionary<string, int>();
        infoEspecial = new Dictionary<string, string>();

        infoString["Nombre"] = "nombre";
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
        infoString = dataJugador.GetInfoString();
        infoInt = dataJugador.GetInfoInt();
        infoEspecial = dataJugador.GetInfoEspecial();
        fechaNac = dataJugador.GetFechaNacimiento();
    }

    public void NuevaInfo(InfoJugador info_)
    {
        infoString = info_.infoString;
        infoInt = info_.infoInt;
        infoEspecial = info_.infoEspecial;
        fechaNac = info_.fechaNac;
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

    public void SetNombre(string nombre)
    {
        infoString["Nombre"] = nombre;
    }

    public void SetEspecial(string categoria, string valor)
    {
        infoEspecial[categoria] = valor;
    }

    public string GetNombre()
    {
        return infoString["Nombre"];
    }

    public void SetInfoString(InputPrefab input)
    {
        infoString[input.GetNombreCategoria()] = input.GetValorCategoria();
    }

    public void SetInfoInt(InputPrefab input)
    {
        //infoInt[input.GetNombreCategoria()] = input.GetValorCategoria();
    }

    public void SetInfoEspecial(InputPrefab input)
    {
        infoEspecial[input.GetNombreCategoria()] = input.GetValorCategoria();
    }
}
