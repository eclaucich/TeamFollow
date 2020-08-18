using System.Collections.Generic;
using System;

public class InfoJugador 
{
    public Dictionary<string, string> infoObligatoria;
    public Dictionary<string, string> infoString;
    public Dictionary<string, string> infoInt;
    public Dictionary<string, string> infoEspecial;
    public DateTime fechaNac;

    public InfoJugador()
    {
        infoObligatoria = new Dictionary<string, string>();
        infoString = new Dictionary<string, string>();
        infoInt = new Dictionary<string, string>();
        infoEspecial = new Dictionary<string, string>();


        //OBLIGATORIOS
        infoObligatoria["NOMBRE"] = "NOMBRE";
        fechaNac = DateTime.MinValue;

        //STRINGS
        infoString["MAIL"] = "";
        infoString["ALERGIAS"] = "";
        infoString["DIRECCION"] = "";
        infoString["CIUDAD"] = "";
        infoString["PROVINCIA"] = "";
        infoString["CATEGORIA"] = "";
        infoString["POSICION"] = "";

        /*infoString["DNI"] = "0";
        infoString["ALTURA"] = "0";
        infoString["PESO"] = "0";
        infoString["NUMERO CELULAR"] = "0";
        infoString["NUMERO CONTACTO EMERGENCIA"] = "0";*/

        //ENTEROS
        infoInt["NUMERO CAMISETA"] = "-1";
        infoInt["DNI"] = "0";
        infoInt["ALTURA"] = "0";
        infoInt["PESO"] = "0";
        infoInt["NUMERO CELULAR"] = "0";
        infoInt["NUMERO CONTACTO EMERGENCIA"] = "0";

        //ESPECIALES
        infoEspecial["SEXO"] = "";
        infoEspecial["FACTOR SANGUINEO"] = "";
        infoEspecial["FICHA MEDICA"] = "";
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

    #region Getters de la info de cada tipo
    public Dictionary<string, string> GetInfoObligatoria()
    {
        return infoObligatoria;
    }
    public Dictionary<string, string> GetInfoString()
    {
        return infoString;
    }
    public Dictionary<string , string> GetInfoInt()
    {
        return infoInt;
    }
    public Dictionary<string, string> GetInfoEspecial()
    {
        return infoEspecial;
    }
    #endregion

    #region Getters de categoría específica
    public DateTime GetFechaNac()
    {
        return fechaNac;
    }
    public string GetNombre()
    {
        return infoObligatoria["NOMBRE"];
    } 
    public string GetNumeroCamiseta()
    {
        return infoInt["NUMERO CAMISETA"];
    }
    public string GetPosicion()
    {
        return infoString["POSICION"];
    }
    #endregion

    #region Setters
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
        infoInt[input.GetNombreCategoria()] = input.GetValorCategoria();
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
    #endregion

    #region Getters en idioma
    public string GetKeyInLaguage(string _key, AppController.Idiomas idioma)
    {
        switch (_key)
        {
            case "NOMBRE":
                if (idioma == AppController.Idiomas.Ingles)
                    return "NAME";
                break;
            case "MAIL":
                if (idioma == AppController.Idiomas.Ingles)
                    return "MAIL";
                break;
            case "ALERGIAS":
                if (idioma == AppController.Idiomas.Ingles)
                    return "ALLERGIES";
                break;
            case "DIRECCION":
                if (idioma == AppController.Idiomas.Ingles)
                    return "ADDRESS";
                break;
            case "CIUDAD":
                if (idioma == AppController.Idiomas.Ingles)
                    return "CITY";
                break;
            case "PROVINCIA":
                if (idioma == AppController.Idiomas.Ingles)
                    return "STATE";
                break;
            case "CATEGORIA":
                if (idioma == AppController.Idiomas.Ingles)
                    return "CATEGORY";
                break;
            case "POSICION":
                if (idioma == AppController.Idiomas.Ingles)
                    return "POSITION";
                break;
            case "NUMERO CAMISETA":
                if (idioma == AppController.Idiomas.Ingles)
                    return "SHIRT NUMBER";
                break;
            case "DNI":
                if (idioma == AppController.Idiomas.Ingles)
                    return "ID NUMBER";
                break;
            case "ALTURA":
                if (idioma == AppController.Idiomas.Ingles)
                    return "HEIGHT";
                break;
            case "PESO":
                if (idioma == AppController.Idiomas.Ingles)
                    return "WEIGHT";
                break;
            case "NUMERO CELULAR":
                if (idioma == AppController.Idiomas.Ingles)
                    return "CELLPHONE";
                break;
            case "NUMERO CONTACTO EMERGENCIA":
                if (idioma == AppController.Idiomas.Ingles)
                    return "EMERGENCY CONTACT PHONE NUMBER";
                break;
            case "SEXO":
                if (idioma == AppController.Idiomas.Ingles)
                    return "SEX";
                break;
            case "FACTOR SANGUINEO":
                if (idioma == AppController.Idiomas.Ingles)
                    return "BLOOD FACTOR";
                break;
            case "FICHA MEDICA":
                if (idioma == AppController.Idiomas.Ingles)
                    return "MEDICAL FORM";
                break;
            default:
                return "ERROR";
        }
        return "ERROR";
    }

    public string GetSpecialValueInLanguage(string _value, AppController.Idiomas _idioma)
    {
        if(_value == "NO ESPECIFICA" || _value == "DOES NOT SPECIFY")
        {
            if (_idioma == AppController.Idiomas.Español)
                return "NO ESPECIFICA";
            else
                return "DOES NOT SPECIFY";
        }
        else if(_value == "MASCULINO" || _value == "MALE")
        {
            if (_idioma == AppController.Idiomas.Español)
                return "MASCULINO";
            else
                return "MALE";
        }
        else if (_value == "FEMENINO" || _value == "FEMALE")
        {
            if (_idioma == AppController.Idiomas.Español)
                return "FEMENINO";
            else
                return "FEMALE";
        }
        else if (_value == "FICHA MEDICA" || _value == "MEDICAL FORM")
        {
            if (_idioma == AppController.Idiomas.Español)
                return "FICHA MEDICA";
            else
                return "MEDICAL FORM";
        }
        else if (_value == "SI" || _value == "YES")
        {
            if (_idioma == AppController.Idiomas.Español)
                return "SI";
            else
                return "YES";
        }
        else
        {
            return _value;
        }
    }
    #endregion
}
