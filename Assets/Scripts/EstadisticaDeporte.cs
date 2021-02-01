using System;
using UnityEngine;

public abstract class EstadisticaDeporte : MonoBehaviour
{
    public enum Estadisticas
    {
        //FUTBOL
        Gol=300,
        Pase=0,
        Amarilla=400,
        Roja=401,
        Recuperacion=100,
        PerdidaDeMarca=101,
        Offside=200,
        Falta=201,
        Intercepcion=202,
        PerdidaDeBalon=203,
        ProposicionDeJugada=204,
        Tiro=301,
        TiroLibre=303,
        Corner=302,
        Lateral=304,
        Penal=402,

        //BASKET
        Doble=106,
        Triple=214,
        Libre=414,
        Volcada=107,
        Pared=215,
        Bloqueo=216,
        DisputaGanada=415,
        DisputaPerdida=416,
        Tecnica=417,

        //TENIS
        WinnerDrive=25,
        WinnerReves=26,
        ErrorNoForzadoDrive=27,
        ErrorNoForzadoReves=28,
        Ace=29,
        DobleFalta=30,
        WinnerDrop=31,
        ErrorNoForzadoDrop=32,
        PrimerSaque=109,
        SegundoSaque=110,

        //RUGBY
        Takle=3,
        KnockOn=104,
        Drop=105,
        PassForward=310,
        Try=412,
        Conversion=413,
        
        //SOFTBALL
        Out=39,
        Mala=40,
        Buena=41,
        Carrera=42,
        HomeRun=43,

        //PADEL
        Sacada=418,

        //HOCKEY CESPED
        CornerCorto=205,
        CornerLargo=206,
        BochaPeligrosa=305,
        TarjetaVerde=403,
        TarjetaAmarilla=404,
        TarjetaRoja=405,

        //HOCKEY PATIN
        Remate=102,
        Mareada=207,
        FaltaPatin=306,
        Azul=406,
        RecuperacionDeBocha=103,
        Disputa=208,
        BochaFueraDeCancha=407,
        FaltaDePenal=408,
        FaltaDeLibre=409,
        Altura=307,
        CincoSegundos=308,

        //HANDBALL
        Camina=217,
        PerdidaDePosesion=218,

        //VOLEY
        Recepcion=1,
        Bloqueada=2,
        MalaRecepcion=209,
        ToqueDeRed=210,
        ToqueDeBarilla=211,
        Llevada=212,
        Invasion=213,
        DobleGolpe=309,
        Saque=410,
        ErrorDeSaque=411,

        MeterJugador=-1,
        SacarJugador=-2

    };

    public abstract Array GetEstadisticas();
    public abstract string[] GetStatisticsName(int i, AppController.Idiomas idioma);
    public abstract int GetSize();
    public abstract string GetValueAtIndex(int i);
}
