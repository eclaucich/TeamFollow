using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AppController : MonoBehaviour {

    public ColorTheme colorTheme;
    [SerializeField] public List<ColorTheme> colorThemes = null;
    [SerializeField] public Font textFont = null;
    [SerializeField] public Font numberFont = null;
    [SerializeField] public PantallaCarga pantallaCarga = null;

    [SerializeField] private Text debugText = null;

    public static AppController instance = null;
    public List<Equipo> equipos;
    public Equipo equipoActual;
    public Jugador jugadorActual;

    public List<CarpetaJugada> carpetasJugadas;

    [HideInInspector] public Deportes.DeporteEnum deporteFavorito = Deportes.DeporteEnum.Ninguno;
    [HideInInspector] public string equipoFavorito = null;

    [HideInInspector] public int resWidth;
    [HideInInspector] public int resHeight;

    public enum Idiomas
    {
        Español = 0,
        Ingles = 1
    };
    public Idiomas idioma;
    public enum Temas
    {
        Oscuro,
        Claro
    };
    public Temas tema;

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(instance);

        equipos = new List<Equipo>();
        equipoActual = null;
        jugadorActual = null;

        resWidth = Screen.width;
        resHeight = Screen.height;

        carpetasJugadas = new List<CarpetaJugada>();

        debugText.text = Application.persistentDataPath;

        pantallaCarga.gameObject.SetActive(true);

        LoadSystem.LoadData();

        DontDestroyOnLoad(this);
    }

    #region Control de equipos
    public void AgregarEquipo(Equipo equipo)
    {
        equipos.Add(equipo);
        SaveSystem.GuardarEquipo(equipo);
    }
    public void BorrarEquipo(Equipo _equipo)
    {
        if (equipos.Contains(_equipo))
        {
            SaveSystem.BorrarEquipo(_equipo);

            equipos.Remove(_equipo);
        }
    }
    public Equipo BuscarEquipoPorNombre(string nombreEquipo)
    {
        foreach (var equipo in equipos)
        {
            if (equipo.GetNombre().ToUpper() == nombreEquipo.ToUpper())
                return equipo;
        }
        return null;
    }
    #endregion

    #region Control de carpetas de biblioteca
    public void AgregarCarpetaJugada(CarpetaJugada _carpeta)
    {
        carpetasJugadas.Add(_carpeta);
    }
    public bool VerificarNombreCarpeta(string _nombreCarpeta)
    {
        foreach (var carpeta in carpetasJugadas)
        {
            if (carpeta.GetNombre() == _nombreCarpeta || _nombreCarpeta == "SIN CARPETA" || _nombreCarpeta == " ")
                return false;
        }
        return VerificarNombre(_nombreCarpeta);
    }
    public void BorrarCarpeta(CarpetaJugada _carpeta)
    {
        if (!carpetasJugadas.Contains(_carpeta))
            return;

        carpetasJugadas.Remove(_carpeta);
        SaveSystem.BorrarCarpeta(_carpeta);
    }
    public CarpetaJugada BuscarCarpetaPorNombre(string _nombre)
    {
        foreach (var carpeta in carpetasJugadas)
        {
            if (carpeta.GetNombre().ToUpper() == _nombre.ToUpper())
                return carpeta;
        }

        return null;
    }
    #endregion

    #region Auxiliares
    public bool VerificarNombre(string _nombre)
    {
        if( _nombre.Contains("?") ||
            _nombre.Contains("!") ||
            _nombre.Contains("°") ||
            _nombre.Contains("\"") ||
            _nombre.Contains("#") ||
            _nombre.Contains("$") ||
            _nombre.Contains("%") ||
            _nombre.Contains("/") ||
            _nombre.Contains("(") ||
            _nombre.Contains(")") ||
            _nombre.Contains("=") ||
            _nombre.Contains("'") ||
            _nombre.Contains("¿") ||
            _nombre.Contains("¡") ||
            _nombre.Contains("´") ||
            _nombre.Contains("¨") ||
            _nombre.Contains("{") ||
            _nombre.Contains("[") ||
            _nombre.Contains("^") ||
            _nombre.Contains("}") ||
            _nombre.Contains("]") ||
            _nombre.Contains("`") ||
            _nombre.Contains("+") ||
            _nombre.Contains("*") ||
            _nombre.Contains("~") ||
            _nombre.Contains(".") ||
            _nombre.Contains(":") ||
            _nombre.Contains(",") ||
            _nombre.Contains(";") ||
            _nombre.Contains("¬") ||
            _nombre.Contains("\\") ||
            _nombre.Contains("<") ||
            _nombre.Contains(">"))
        {
            return false;
        }

        if (_nombre.StartsWith(" "))
            return false;

        return true;
    }

    public void SetSettings(SaveDataSettings _settings)
    {
        idioma = _settings.idioma;
        tema = _settings.tema;
        deporteFavorito = _settings.deporteFavorito;

        SetTemaActual(tema);
        Debug.Log("IDIOMA: " + idioma);
        Debug.Log("TEMA: " + tema);
        Debug.Log("DEP FAV: " + deporteFavorito);
        Debug.Log("EQU FAV: " + equipoFavorito);
    }

    public void SetTemaActual(Temas _tema)
    {
        switch (_tema)
        {
            case Temas.Claro:
                colorTheme = colorThemes[0];
                break;
            case Temas.Oscuro:
                colorTheme = colorThemes[1];
                break;
        }
    }
    #endregion

    public void SetTeamAsFavourite(string _equipo)
    {
        if (_equipo == equipoFavorito)
            return;

        if (_equipo == "")
            equipoFavorito = null;

        equipoFavorito = _equipo;
    }


    #region Displays
    public string GetDisplayNameIdioma(Idiomas _idiomaIn, Idiomas _idiomaOut)
    {
        switch (_idiomaIn)
        {
            case Idiomas.Español:
                if (_idiomaOut == Idiomas.Español)
                    return "Español";
                else
                    return "Spanish";
            case Idiomas.Ingles:
                if (_idiomaOut == Idiomas.Español)
                    return "Inglés";
                else
                    return "English";
            default:
                return "ERROR";
        }
    }
    public string GetDisplayNameTema(Temas _tema, Idiomas _idioma)
    {
        switch (_tema)
        {
            case Temas.Claro:
                if (_idioma == Idiomas.Español)
                    return "Claro";
                else
                    return "Light";
            case Temas.Oscuro:
                if (_idioma == Idiomas.Español)
                    return "Oscuro";
                else
                    return "Dark";
            default:
                return "ERROR";
        }
    }
    #endregion
}