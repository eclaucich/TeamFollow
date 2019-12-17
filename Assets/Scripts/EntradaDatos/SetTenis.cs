using UnityEngine;
using UnityEngine.UI;

public class SetTenis : MonoBehaviour
{
    private bool finalizado = false;

    private bool ventaja;

    [SerializeField] private Text gamesJ1Text = null;
    [SerializeField] private Text gamesJ2Text = null;

    [SerializeField] private Text tiebreakJ1Text = null;
    [SerializeField] private Text tiebreakJ2Text = null;

    ///Puntos del game
    private int puntosJ1 = 0;
    private int puntosJ2 = 0;

    ///Cantidad de games
    private int gamesJ1 = 0;
    private int gamesJ2 = 0;

    ///Puntos del Tiebreak
    private bool tiebreak = false;
    private int puntosTBJ1 = 0;
    private int puntosTBJ2 = 0;

    private EntradaDatosTenis entradaDatosTenis;

    private void Start()
    {
        entradaDatosTenis = GameObject.Find("EntradaDatosTenisPartido").GetComponent<EntradaDatosTenis>();

        gamesJ1Text.text = "0";
        gamesJ2Text.text = "0";

        tiebreakJ1Text.gameObject.SetActive(false);
        tiebreakJ2Text.gameObject.SetActive(false);
        tiebreakJ1Text.text = "0";
        tiebreakJ2Text.text = "0";
    }

    public void AgregarPuntoJ1()
    {
        puntosJ1++;

        if (isGameFinalizado(puntosJ1, puntosJ2))
        {
            AgregarGameJ1();
            ResetPuntosGame();
        }

        if (puntosJ1 < 4) entradaDatosTenis.SetPuntosGameJ1(puntosJ1);
        else              entradaDatosTenis.SetPuntosGameVentaja(puntosJ1, puntosJ2);
    }

    public void AgregarPuntoJ2()
    {
        puntosJ2++;
        
        if (isGameFinalizado(puntosJ2, puntosJ1))
        {
            AgregarGameJ2();
            ResetPuntosGame();
        }
    
        if (puntosJ2 < 4) entradaDatosTenis.SetPuntosGameJ2(puntosJ2);
        else              entradaDatosTenis.SetPuntosGameVentaja(puntosJ1, puntosJ2);
    }

    public void AgregarGameJ1()
    {
        gamesJ1++;
        gamesJ1Text.text = gamesJ1.ToString();
        RevisarSetFinalizado();
    }

    public void AgregarGameJ2()
    {
        gamesJ2++;
        gamesJ2Text.text = gamesJ2.ToString();
        RevisarSetFinalizado();
    }

    public void AgregarPuntoTBJ1()
    {
        puntosTBJ1++;
        entradaDatosTenis.SetPuntosTBJ1(puntosTBJ1);
        RevisarSetFinalizado();
    }

    public void AgregarPuntoTBJ2()
    {
        puntosTBJ2++;
        entradaDatosTenis.SetPuntosTBJ2(puntosTBJ2);
        RevisarSetFinalizado();
    }



    private bool isGameFinalizado(int _puntosA, int _puntosB)
    {
        if (ventaja) return (_puntosA >= 4 && (Mathf.Abs(_puntosA - _puntosB) >= 2));
        else         return _puntosA >= 4;
    }

    private bool isSetFinalizado()
    {
        if (gamesJ1 == 6 && gamesJ2 == 6)
        {
            tiebreak = true;
            entradaDatosTenis.ActivarSeccionTiebreak();
            return false;
        }
        return ((gamesJ1 == 6 && gamesJ2 < 5) || (gamesJ1 < 5 && gamesJ2 == 6) || (gamesJ1 == 7 && gamesJ2 == 5) || (gamesJ2 == 7 && gamesJ1 == 5));
    }

    private bool isTiebreakFinalizado()
    {
        return ((puntosTBJ1 >= 7 || puntosTBJ2 >= 7) && Mathf.Abs(puntosTBJ1 - puntosTBJ2) >= 2);
    } 

    private void RevisarSetFinalizado()
    {
        if (tiebreak) finalizado = isTiebreakFinalizado();
        else          finalizado = isSetFinalizado();

        if (finalizado)
        {
            if (tiebreak)
            {
                tiebreakJ1Text.gameObject.SetActive(true);
                tiebreakJ2Text.gameObject.SetActive(true);

                tiebreakJ1Text.text = puntosTBJ1.ToString();
                tiebreakJ2Text.text = puntosTBJ2.ToString();

                if (puntosTBJ1 > puntosTBJ2) { gamesJ1++; gamesJ1Text.text = gamesJ1.ToString(); }
                else                         { gamesJ2++; gamesJ2Text.text = gamesJ2.ToString(); }
            }

            entradaDatosTenis.CrearNuevoSet();
        }
    }

    private void ResetPuntosGame()
    {
        puntosJ1 = 0;
        puntosJ2 = 0;
        entradaDatosTenis.SetPuntosGameJ1(puntosJ1);
        entradaDatosTenis.SetPuntosGameJ2(puntosJ2);
    }

    public bool isFinalizado()
    {
        return finalizado;
    }

    public bool isTiebreak()
    {
        return tiebreak;
    }

    public void SetVentaja(bool _ventaja)
    {
        ventaja = _ventaja;
    }
}
