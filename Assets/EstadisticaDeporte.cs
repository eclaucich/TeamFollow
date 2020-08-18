using System;
using UnityEngine;

public abstract class EstadisticaDeporte : MonoBehaviour
{
    public enum Estadisticas
    {
        //FUTBOL
        Gol=0,
        Pase=1,
        Amarilla=2,
        Roja=3,
        Recuperacion=4,
        PerdidaDeMarca=5,
        Offside=6,
        Falta=7,
        Intercepcion=8,
        PerdidaDeBalon=9,
        ProposicionDeJugada=10,
        Tiro=11,
        TiroLibre=12,
        Corner=13,
        Lateral=14,
        Penal=15,

        //BASKET
        Doble=16,
        Triple=17,
        Libre=18,
        Volcada=19,
        Pared=20,
        Bloqueo=21,
        DisputaGanada=22,
        DisputaPerdida=23,
        Tecnica=24,

        //TENIS
        WinnerDrive=25,
        WinnerReves=26,
        ErrorNoForzadoDrive=27,
        ErrorNoForzadoReves=28,
        Ace=29,
        DobleFalta=30,
        WinnerDrop=31,
        ErrorNoForzadoDrop=32,

        //RUGBY
        Takle=33,
        KnockOn=34,
        Drop=35,
        PassForward=36,
        Try=37,
        Conversion=38,
        
        //SOFTBALL
        Out=39,
        Mala=40,
        Buena=41,
        Carrera=42,
        HomeRun=43,

        //PADEL
        Sacada=44,

        //HOCKEY CESPED
        CornerCorto=45,
        CornerLargo=46,
        BochaPeligrosa=47,
        TarjetaVerde=48,
        TarjetaAmarilla=49,
        TarjetaRoja=50,

        //HOCKEY PATIN
        Remate=51,
        Mareada=52,
        FaltaPatin=53,
        Azul=54,
        RecuperacionDeBocha=55,
        Disputa=56,
        BochaFueraDeCancha=57,
        FaltaDePenal=58,
        FaltaDeLibre=59,
        Altura=60,
        CincoSegundos=61,

        //HANDBALL
        Camina=62,
        PerdidaDePosesion=63,

        //VOLEY
        Recepcion=64,
        Bloqueada=65,
        MalaRecepcion=66,
        ToqueDeRed=67,
        ToqueDeBarilla=68,
        Llevada=69,
        Invasion=70,
        DobleGolpe=71,
        Saque=72,
        ErrorDeSaque=73

    };

    public abstract Array GetEstadisticas();
    public abstract string[] GetStatisticsName(int i, AppController.Idiomas idioma);
    public abstract int GetSize();
    public abstract string GetValueAtIndex(int i);
}
