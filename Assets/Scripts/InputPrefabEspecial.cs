using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputPrefabEspecial : InputPrefab
{
    [SerializeField] private GameObject menuOpcionesEspecialesPrefab = null;
    [SerializeField] private TextLanguage textValor = null;
    
    private List<string> opcionesEspañol;
    private List<string> opcionesIngles;

    public void MostrarOpcionesEspeciales()
    {
        opcionesEspañol = new List<string>();
        opcionesIngles = new List<string>();

        switch (nombreCategoria.text)
        {
            case "SEXO": case "SEX":
                opcionesEspañol.Add("No especifica"); opcionesIngles.Add("Does not specify");
                opcionesEspañol.Add("Masculino"); opcionesIngles.Add("Male");
                opcionesEspañol.Add("Femenino"); opcionesIngles.Add("Female");
                break;

            case "FACTOR SANGUINEO": case "BLOOD FACTOR":
                opcionesEspañol.Add("No especifica"); opcionesIngles.Add("Does not specify");
                opcionesEspañol.Add("O-"); opcionesIngles.Add("O-");
                opcionesEspañol.Add("O+"); opcionesIngles.Add("O+");
                opcionesEspañol.Add("A-"); opcionesIngles.Add("A-");
                opcionesEspañol.Add("A+"); opcionesIngles.Add("A+");
                opcionesEspañol.Add("B-"); opcionesIngles.Add("B-");
                opcionesEspañol.Add("B+"); opcionesIngles.Add("B+");
                opcionesEspañol.Add("AB-"); opcionesIngles.Add("AB-");
                opcionesEspañol.Add("AB+"); opcionesIngles.Add("AB+");
                break;

            case "FICHA MEDICA": case "MEDICAL FORM":
                opcionesEspañol.Add("SI"); opcionesIngles.Add("YES");
                opcionesEspañol.Add("NO"); opcionesIngles.Add("NO");
                break;
        }

        GameObject parent = GameObject.Find("PanelNuevoJugador");
        if (parent == null) parent = GameObject.Find("PanelInfoJugador");
        GameObject go = Instantiate(menuOpcionesEspecialesPrefab, parent.transform, false);
        go.GetComponent<OpcionesEspeciales>().SetMenu(opcionesEspañol, opcionesIngles, nombreCategoria.text, this);
    }

    public void SetValor(string valor, AppController.Idiomas _idioma)
    {
        textValor.SetText(valor, _idioma);
    }

    public override void HabilitarInput(bool _aux)
    {
        lineaSeparadora.gameObject.SetActive(_aux);
        Button buttonGO = GetComponent<Button>();
        if(buttonGO!=null)
            buttonGO.enabled = _aux;
    }
}
