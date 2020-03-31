using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputPrefabEspecial : InputPrefab
{
    [SerializeField] private GameObject menuOpcionesEspecialesPrefab = null;

    private List<string> opciones;

    public void MostrarOpcionesEspeciales()
    {
        opciones = new List<string>();

        switch (nombreCategoria.text)
        {
            case "Sexo":
                opciones.Add("Masculino");
                opciones.Add("Femenino");
                break;

            case "Factor Sanguineo":
                opciones.Add("No especifica");
                opciones.Add("O-");
                opciones.Add("O+");
                opciones.Add("A-");
                opciones.Add("A+");
                opciones.Add("B-");
                opciones.Add("B+");
                opciones.Add("AB-");
                opciones.Add("AB+");
                break;

            case "Ficha Medica":
                opciones.Add("SI");
                opciones.Add("NO");
                break;
        }

        GameObject parent = GameObject.Find("PanelNuevoJugador");
        if (parent == null) parent = GameObject.Find("PanelInfoJugador");
        GameObject go = Instantiate(menuOpcionesEspecialesPrefab, parent.transform, false);
        go.GetComponent<OpcionesEspeciales>().SetMenu(opciones, nombreCategoria.text, this);
    }

    public void SetValor(string valor)
    {
        valorCategoria.text = valor;
    }

    public override void HabilitarInput(bool _aux)
    {
        GetComponent<Button>().enabled = _aux;
    }
}
