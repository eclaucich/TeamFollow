using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppController : MonoBehaviour {

    [SerializeField] public ColorTheme colorTheme = null;
    [SerializeField] public Font textFont = null;
    [SerializeField] public Font numberFont = null;

    [SerializeField] private Text resolucionText = null;

    public static AppController instance = null;
    public List<Equipo> equipos;
    public Equipo equipoActual;
    public Jugador jugadorActual;

    public List<CarpetaJugada> carpetasJugadas;

    public int resWidth;
    public int resHeight;

    public enum Idiomas
    {
        Español = 0,
        Ingles = 1
    };
    public Idiomas idioma;
    
    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(instance);

        equipos = new List<Equipo>();
        equipoActual = null;
        jugadorActual = null;

        carpetasJugadas = new List<CarpetaJugada>();

        resWidth = Screen.currentResolution.width;
        resHeight = Screen.currentResolution.height;
        resolucionText.text = "W:" + resWidth + ", H:" + resHeight;

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
        return true;
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
    //SOLO DE DEBUG!!
    public void ToogleLanguage()
    {
        if (idioma == Idiomas.Español)
            idioma = Idiomas.Ingles;
        else
            idioma = Idiomas.Español;
    }
    #endregion
}
