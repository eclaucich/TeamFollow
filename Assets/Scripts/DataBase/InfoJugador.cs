using System.Collections.Generic;
using System;

public class InfoJugador 
{
    public Dictionary<string, string> infoString;
    public Dictionary<string, int> infoInt;
    public DateTime fechaNac;

    public InfoJugador()
    {
        infoString = new Dictionary<string, string>();
        infoInt = new Dictionary<string, int>();

        infoString["Nombre"] = "nombre";
        infoString["Mail"] = "";
        infoString["Sexo"] = "";
        infoString["Factor Sanguineo"] = "";
        infoString["Alergias"] = "";
        infoString["Direccion"] = "";
        infoString["Ciudad"] = "";
        infoString["Provincia"] = "";
        infoString["Categoria"] = "";

        infoInt["DNI"] = 0;
        infoInt["Altura"] = 0;
        infoInt["Ficha Medica"] = 0;
        infoInt["Numero Celular"] = 0;
        infoInt["Numero Contacto Emergencia"] = 0;

        fechaNac = DateTime.MinValue;
    }

    public InfoJugador(SaveDataJugador dataJugador)
    {
        infoString = dataJugador.GetInfoString();
        infoInt = dataJugador.GetInfoInt();
        fechaNac = dataJugador.GetFechaNacimiento();
    }

    public void NuevaInfo(InfoJugador info_)
    {
        infoString = info_.infoString;
        infoInt = info_.infoInt;
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

    public DateTime GetFechaNac()
    {
        return fechaNac;
    }

    public string GetNombre()
    {
        return infoString["Nombre"];
    }
}
