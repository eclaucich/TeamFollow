using UnityEngine;
using UnityEngine.UI;

public class HerramientaDeporte : Herramienta
{
    [SerializeField] private Text nombreHerramienta = null;

    private void Start()
    {
        nombre = "Deporte";
    }

    public override void Usar()
    {
        throw new System.NotImplementedException();
    }

    public void SeleccionarDeporte(Text _deporte)
    {
        nombreHerramienta.text = _deporte.text;
        SetHerramientaActual();
    }
}
