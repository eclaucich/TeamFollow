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
        SetHerramientaActual();

        Deportes.DeporteEnum _deporteActual = Deportes.DeporteEnum.Ninguno;

        switch (indexDeporte_)
        {
            case 0: _deporteActual = Deportes.DeporteEnum.Basket; break;
            case 1: _deporteActual = Deportes.DeporteEnum.Futbol; break;
            case 2: _deporteActual = Deportes.DeporteEnum.Handball; break;
            case 3: _deporteActual = Deportes.DeporteEnum.HockeyCesped; break;
            case 4: _deporteActual = Deportes.DeporteEnum.HockeyPatines; break;
            case 5: _deporteActual = Deportes.DeporteEnum.Padel; break;
            case 6: _deporteActual = Deportes.DeporteEnum.Rugby; break;
            case 7: _deporteActual = Deportes.DeporteEnum.Softball; break;
            case 8: _deporteActual = Deportes.DeporteEnum.Tenis; break;
            case 9: _deporteActual = Deportes.DeporteEnum.Voley; break;
        }

        panelEdicion.ChangeSport(_deporteActual);
    }
}
