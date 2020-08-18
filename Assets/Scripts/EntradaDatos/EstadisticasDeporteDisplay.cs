using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstadisticasDeporteDisplay : MonoBehaviour
{
    [SerializeField] public Sprite gol;
    [SerializeField] public Sprite pase;
    [SerializeField] public Sprite amarilla;
    [SerializeField] public Sprite roja;
    [SerializeField] public Sprite corner;
    [SerializeField] public Sprite falta;
    [SerializeField] public Sprite intercepcion;
    [SerializeField] public Sprite lateral;
    [SerializeField] public Sprite offside;
    [SerializeField] public Sprite perdidaDeMarca;
    [SerializeField] public Sprite perdidaDeBalon;
    [SerializeField] public Sprite penal;
    [SerializeField] public Sprite proposicionDeJugada;
    [SerializeField] public Sprite recuperacion;
    [SerializeField] public Sprite tiro;
    [SerializeField] public Sprite tiroLibre;

    public static string[] GetStatisticsName(EstadisticaDeporte.Estadisticas _tipoEstadistica, AppController.Idiomas idioma)
    {
        string[] res = new string[2]; //res[0]=nombre; res[1]=inicial
        switch (_tipoEstadistica)
        {
            case EstadisticaDeporte.Estadisticas.Gol:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Gol";
                    res[1] = "GOL";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Goal";
                    res[1] = "GOL";
                }
                break;
            case EstadisticaDeporte.Estadisticas.Pase:
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
            case EstadisticaDeporte.Estadisticas.Amarilla:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Amarilla";
                    res[1] = "AMA";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Yellow";
                    res[1] = "YEW";
                }
                break;
            case EstadisticaDeporte.Estadisticas.Roja:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Roja";
                    res[1] = "ROJ";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Red";
                    res[1] = "RED";
                }
                break;
            case EstadisticaDeporte.Estadisticas.Corner:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Corner";
                    res[1] = "COR";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Corner";
                    res[1] = "COR";
                }
                break;
            case EstadisticaDeporte.Estadisticas.Falta:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Falta";
                    res[1] = "FAL";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Foul";
                    res[1] = "FOU";
                }
                break;
            case EstadisticaDeporte.Estadisticas.Intercepcion:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Intercepcion";
                    res[1] = "INT";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Interception";
                    res[1] = "INT";
                }
                break;
            case EstadisticaDeporte.Estadisticas.Lateral:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Lateral";
                    res[1] = "LAT";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Throw-in";
                    res[1] = "TRI";
                }
                break;
            case EstadisticaDeporte.Estadisticas.Offside:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Offside";
                    res[1] = "OFF";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Offside";
                    res[1] = "OFF";
                }
                break;
            case EstadisticaDeporte.Estadisticas.Penal:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Penal";
                    res[1] = "PEN";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Penalty";
                    res[1] = "PEN";
                }
                break;
            case EstadisticaDeporte.Estadisticas.PerdidaDeBalon:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Perdida de balon";
                    res[1] = "PDB";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Lost ball";
                    res[1] = "LSB";
                }
                break;
            case EstadisticaDeporte.Estadisticas.PerdidaDeMarca:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Perdida de marca";
                    res[1] = "PDM";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "---";
                    res[1] = "---";
                }
                break;
            case EstadisticaDeporte.Estadisticas.ProposicionDeJugada:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Proposicion de jugada";
                    res[1] = "PDJ";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Play proposal";
                    res[1] = "PPR";
                }
                break;
            case EstadisticaDeporte.Estadisticas.Recuperacion:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Recuperacion";
                    res[1] = "REC";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Recovery";
                    res[1] = "REC";
                }
                break;
            case EstadisticaDeporte.Estadisticas.Tiro:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Tiro";
                    res[1] = "TIR";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Kick";
                    res[1] = "KCK";
                }
                break;
            case EstadisticaDeporte.Estadisticas.TiroLibre:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Tiro libre";
                    res[1] = "TLB";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Free Kick";
                    res[1] = "FKC";
                }
                break;
            case EstadisticaDeporte.Estadisticas.Doble:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Doble";
                    res[1] = "DOB";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Double";
                    res[1] = "DBL";
                }
                break;
            case EstadisticaDeporte.Estadisticas.Triple:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Triple";
                    res[1] = "TRP";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Triple";
                    res[1] = "TRP";
                }
                break;
            case EstadisticaDeporte.Estadisticas.Libre:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Libre";
                    res[1] = "LIB";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Free Throw";
                    res[1] = "FRE";
                }
                break;
            case EstadisticaDeporte.Estadisticas.Bloqueo:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Bloqueo";
                    res[1] = "BLO";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Block";
                    res[1] = "BLK";
                }
                break;
            case EstadisticaDeporte.Estadisticas.Volcada:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Volcada";
                    res[1] = "VOL";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Slam Dunk";
                    res[1] = "SDK";
                }
                break;
            case EstadisticaDeporte.Estadisticas.DisputaGanada:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Disputa ganada";
                    res[1] = "DGN";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Win Dispute";
                    res[1] = "WDP";
                }
                break;
            case EstadisticaDeporte.Estadisticas.DisputaPerdida:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Disputa perdida";
                    res[1] = "DPR";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Loss Dispute";
                    res[1] = "LDP";
                }
                break;
            case EstadisticaDeporte.Estadisticas.Pared:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Pared";
                    res[1] = "PAR";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Wall";
                    res[1] = "WLL";
                }
                break;
            case EstadisticaDeporte.Estadisticas.Tecnica:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Tecnica";
                    res[1] = "TEC";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Technical";
                    res[1] = "TEC";
                }
                break;
            case EstadisticaDeporte.Estadisticas.Ace:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Ace";
                    res[1] = "ACE";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Ace";
                    res[1] = "ACE";
                }
                break;
            case EstadisticaDeporte.Estadisticas.DobleFalta:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Doble falta";
                    res[1] = "DBF";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Double foul";
                    res[1] = "DBF";
                }
                break;
            case EstadisticaDeporte.Estadisticas.ErrorNoForzadoDrive:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Error no forzado Drive";
                    res[1] = "EDR";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Drive nforced error";
                    res[1] = "DVE";
                }
                break;
            case EstadisticaDeporte.Estadisticas.ErrorNoForzadoDrop:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Error no forzado drop";
                    res[1] = "EDP";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Drop unforced error";
                    res[1] = "DPE";
                }
                break;
            case EstadisticaDeporte.Estadisticas.WinnerDrive:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Winner drive";
                    res[1] = "WDR";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Drive winner";
                    res[1] = "DRW";
                }
                break;
            case EstadisticaDeporte.Estadisticas.WinnerReves:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Winner reves";
                    res[1] = "WRV";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Backhand winner";
                    res[1] = "BKW";
                }
                break;
            case EstadisticaDeporte.Estadisticas.WinnerDrop:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Winner drop";
                    res[1] = "WDP";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Drop winner";
                    res[1] = "DPW";
                }
                break;
            case EstadisticaDeporte.Estadisticas.KnockOn:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Knock On";
                    res[1] = "KON";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Knock On";
                    res[1] = "KON";
                }
                break;
            case EstadisticaDeporte.Estadisticas.Drop:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Drop";
                    res[1] = "DRP";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Drop";
                    res[1] = "DRP";
                }
                break;
            case EstadisticaDeporte.Estadisticas.Takle:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Takle";
                    res[1] = "TKL";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Tackle";
                    res[1] = "TKL";
                }
                break;
            case EstadisticaDeporte.Estadisticas.PassForward:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Pase forward";
                    res[1] = "PSF";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Pass forward";
                    res[1] = "PSF";
                }
                break;
            case EstadisticaDeporte.Estadisticas.Try:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Try";
                    res[1] = "TRY";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Try";
                    res[1] = "TRY";
                }
                break;
            case EstadisticaDeporte.Estadisticas.Conversion:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Conversion";
                    res[1] = "CON";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Convertion";
                    res[1] = "CON";
                }
                break;
            case EstadisticaDeporte.Estadisticas.HomeRun:
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
            case EstadisticaDeporte.Estadisticas.Buena:
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
            case EstadisticaDeporte.Estadisticas.Carrera:
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
            case EstadisticaDeporte.Estadisticas.Out:
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
            case EstadisticaDeporte.Estadisticas.Mala:
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
            case EstadisticaDeporte.Estadisticas.Sacada:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Sacada de cancha";
                    res[1] = "SDC";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "---";
                    res[1] = "---";
                }
                break;
            case EstadisticaDeporte.Estadisticas.BochaPeligrosa:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "BochaPeligrosa";
                    res[1] = "BPL";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Dangerous ball";
                    res[1] = "DBL";
                }
                break;
            case EstadisticaDeporte.Estadisticas.CornerCorto:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Corner corto";
                    res[1] = "CCT";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Short Corner";
                    res[1] = "SCR";
                }
                break;
            case EstadisticaDeporte.Estadisticas.CornerLargo:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Corner Largo";
                    res[1] = "CLG";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Long Corner";
                    res[1] = "LCR";
                }
                break;
            case EstadisticaDeporte.Estadisticas.TarjetaVerde:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Verde";
                    res[1] = "VER";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Green card";
                    res[1] = "GRE";
                }
                break;
            case EstadisticaDeporte.Estadisticas.Mareada:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Mareada";
                    res[1] = "MAR";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "---";
                    res[1] = "---";
                }
                break;
            case EstadisticaDeporte.Estadisticas.Remate:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Remate";
                    res[1] = "REM";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "---";
                    res[1] = "---";
                }
                break;
            case EstadisticaDeporte.Estadisticas.FaltaPatin:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Falta de patin";
                    res[1] = "FPT";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "---";
                    res[1] = "---";
                }
                break;
            case EstadisticaDeporte.Estadisticas.Altura:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Altura";
                    res[1] = "ALT";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "---";
                    res[1] = "---";
                }
                break;
            case EstadisticaDeporte.Estadisticas.Azul:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Azul";
                    res[1] = "AZL";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Blue card";
                    res[1] = "BLU";
                }
                break;
            case EstadisticaDeporte.Estadisticas.BochaFueraDeCancha:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Bocha fuera de cancha";
                    res[1] = "BFC";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Ball outside court";
                    res[1] = "BOC";
                }
                break;
            case EstadisticaDeporte.Estadisticas.CincoSegundos:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Cinco segundos";
                    res[1] = "5SG";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Five seconds";
                    res[1] = "5SC";
                }
                break;
            case EstadisticaDeporte.Estadisticas.Disputa:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Disputa";
                    res[1] = "DIS";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Dispute";
                    res[1] = "DIS";
                }
                break;
            case EstadisticaDeporte.Estadisticas.FaltaDeLibre:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Falta de libre";
                    res[1] = "FLB";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Free kick foul";
                    res[1] = "FKF";
                }
                break;
            case EstadisticaDeporte.Estadisticas.FaltaDePenal:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Falta de penal";
                    res[1] = "FDP";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Penalty foul";
                    res[1] = "PFL";
                }
                break;
            case EstadisticaDeporte.Estadisticas.RecuperacionDeBocha:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Recupertacion de bocha";
                    res[1] = "RDB";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Ball recovery";
                    res[1] = "BRV";
                }
                break;
            case EstadisticaDeporte.Estadisticas.Camina:
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
            case EstadisticaDeporte.Estadisticas.PerdidaDePosesion:
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
            case EstadisticaDeporte.Estadisticas.MalaRecepcion:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Mala recepcion";
                    res[1] = "MRC";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Bad reception";
                    res[1] = "BRC";
                }
                break;
            case EstadisticaDeporte.Estadisticas.ToqueDeRed:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Toque de red";
                    res[1] = "TDR";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Net touch";
                    res[1] = "NTC";
                }
                break;
            case EstadisticaDeporte.Estadisticas.Bloqueada:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Bloqueada";
                    res[1] = "BLO";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Block";
                    res[1] = "BLK";
                }
                break;
            case EstadisticaDeporte.Estadisticas.Llevada:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Llevada";
                    res[1] = "LLV";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "---";
                    res[1] = "---";
                }
                break;
            case EstadisticaDeporte.Estadisticas.Recepcion:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Recepcion";
                    res[1] = "REC";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Reception";
                    res[1] = "REC";
                }
                break;
            case EstadisticaDeporte.Estadisticas.ToqueDeBarilla:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Toque de barilla";
                    res[1] = "TDB";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "---";
                    res[1] = "---";
                }
                break;
            case EstadisticaDeporte.Estadisticas.DobleGolpe:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Doble golpe";
                    res[1] = "DBG";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Double hit";
                    res[1] = "DBH";
                }
                break;
            case EstadisticaDeporte.Estadisticas.ErrorDeSaque:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Error de saque";
                    res[1] = "ESQ";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Serve error";
                    res[1] = "---";
                }
                break;
            case EstadisticaDeporte.Estadisticas.Invasion:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Invasion";
                    res[1] = "INV";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Invasion";
                    res[1] = "INV";
                }
                break;
            case EstadisticaDeporte.Estadisticas.Saque:
                if (idioma == AppController.Idiomas.Español)
                {
                    res[0] = "Saque";
                    res[1] = "SAQ";
                }
                else if (idioma == AppController.Idiomas.Ingles)
                {
                    res[0] = "Serve";
                    res[1] = "SRV";
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

    public Sprite GetSprite(EstadisticaDeporte.Estadisticas _tipoEstadistica)
    {
        switch (_tipoEstadistica)
        {
            case EstadisticaDeporte.Estadisticas.Gol:
                return gol;
            case EstadisticaDeporte.Estadisticas.Pase:
                return pase;
            case EstadisticaDeporte.Estadisticas.Amarilla:
                return amarilla;
            case EstadisticaDeporte.Estadisticas.Roja:
                return roja;
            case EstadisticaDeporte.Estadisticas.Corner:
                return corner;
            case EstadisticaDeporte.Estadisticas.Falta:
                return falta;
            case EstadisticaDeporte.Estadisticas.Intercepcion:
                return intercepcion;
            case EstadisticaDeporte.Estadisticas.Lateral:
                return lateral;
            case EstadisticaDeporte.Estadisticas.Offside:
                return offside;
            case EstadisticaDeporte.Estadisticas.Penal:
                return penal;
            case EstadisticaDeporte.Estadisticas.PerdidaDeBalon:
                return perdidaDeBalon;
            case EstadisticaDeporte.Estadisticas.PerdidaDeMarca:
                return perdidaDeMarca;
            case EstadisticaDeporte.Estadisticas.ProposicionDeJugada:
                return proposicionDeJugada;
            case EstadisticaDeporte.Estadisticas.Recuperacion:
                return recuperacion;
            case EstadisticaDeporte.Estadisticas.Tiro:
                return tiro;
            case EstadisticaDeporte.Estadisticas.TiroLibre:
                return tiroLibre;
            default:
                return null;
        }
    }
}
