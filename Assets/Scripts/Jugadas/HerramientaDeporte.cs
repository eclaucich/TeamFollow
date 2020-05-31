using UnityEngine;
using UnityEngine.UI;

public class HerramientaDeporte : Herramienta
{
    [SerializeField] private PanelEdicion panelEdicion = null;
    //[SerializeField] private Text nombreHerramienta = null;

    [SerializeField] private GameObject imagenBoton = null;

    /*[SerializeField] private Sprite basket = null;
    [SerializeField] private Sprite futbol = null;
    [SerializeField] private Sprite handball = null;
    [SerializeField] private Sprite hockeyCesped = null;
    [SerializeField] private Sprite hockeyPatines = null;
    [SerializeField] private Sprite padel = null;
    [SerializeField] private Sprite rugby = null;
    [SerializeField] private Sprite softball = null;
    [SerializeField] private Sprite tenis = null;
    [SerializeField] private Sprite voley = null;*/

    private void Start()
    {
        nombre = "Deporte";
        //SeleccionarDeporte(1);
    }

    public override void Usar()
    {
        throw new System.NotImplementedException();
    }

    public void CambiarImagenSeleccionada(Image imagenDeporte_)
    {
        imagenBoton.GetComponent<Image>().sprite = imagenDeporte_.sprite;
    }

    public void SeleccionarDeporte(int indexDeporte_)
    {
        //nombreHerramienta.text = nombreDeporte_.text;
        SetHerramientaActual();

        /*switch(indexDeporte_)
        {
            case 0: imagenBoton.GetComponent<Image>().sprite = basket; break;
            case 1: imagenBoton.GetComponent<Image>().sprite = futbol; break;
            case 2: imagenBoton.GetComponent<Image>().sprite = handball; break;
            case 3: imagenBoton.GetComponent<Image>().sprite = hockeyCesped; break;
            case 4: imagenBoton.GetComponent<Image>().sprite = hockeyPatines; break;
            case 5: imagenBoton.GetComponent<Image>().sprite = padel; break;
            case 6: imagenBoton.GetComponent<Image>().sprite = rugby; break;
            case 7: imagenBoton.GetComponent<Image>().sprite = softball; break;
            case 8: imagenBoton.GetComponent<Image>().sprite = tenis; break;
            case 9: imagenBoton.GetComponent<Image>().sprite = voley; break;
        }*/

        panelEdicion.ChangeSport(indexDeporte_);
    }
}
