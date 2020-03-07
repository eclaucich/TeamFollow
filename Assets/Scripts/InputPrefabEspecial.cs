using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        GameObject go = Instantiate(menuOpcionesEspecialesPrefab, GameObject.Find("PanelNuevoJugador").transform, false);
        go.GetComponent<OpcionesEspeciales>().SetMenu(opciones, nombreCategoria.text, this);
    }

    public void SetValor(string valor)
    {
        valorCategoria.text = valor;
    }

}
