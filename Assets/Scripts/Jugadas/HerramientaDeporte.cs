using UnityEngine;
using UnityEngine.UI;

public class HerramientaDeporte : Herramienta
{
    [SerializeField] private PanelEdicion panelEdicion = null;
    [SerializeField] private Text nombreHerramienta = null;

    private void Start()
    {
        nombre = "Deporte";
    }

    public override void Usar()
    {
        throw new System.NotImplementedException();
    }

    public void SeleccionarDeporte(Text nombreDeporte_)
    {
        nombreHerramienta.text = nombreDeporte_.text;
        SetHerramientaActual();

        int index = 0;
        switch(nombreDeporte_.text)
        {
            case "Basket": index = 0; break;
            case "Futbol": index = 1; break;
            case "Handball": index = 2; break;
            case "HockeyCesped": index = 3; break;
            case "HockeyPatines": index = 4; break;
            case "Padel": index = 5; break;
            case "Rugby": index = 6; break;
            case "Softball": index = 7; break;
            case "Tenis": index = 8; break;
            case "BaskVoleyet": index = 9; break;
        }

        panelEdicion.ChangeSport(index);
    }
}
