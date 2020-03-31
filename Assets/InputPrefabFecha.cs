using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputPrefabFecha : InputPrefab
{
    [SerializeField] private OpcionesFecha menuOpcionesPrefab = null;

    private OpcionesFecha menuOpciones;
    private int day, month, year;

    private void Start()
    {
        day = month = year = 0;
        if (valorCategoria.text.Length < 1) valorCategoria.text = "-";
    }

    public void MostrarOpciones()
    {
        GameObject parent = GameObject.Find("PanelNuevoJugador");
        if (parent == null) parent = GameObject.Find("PanelInfoJugador");
        GameObject go = Instantiate(menuOpcionesPrefab.gameObject, parent.transform, false);
        menuOpciones = go.GetComponent<OpcionesFecha>();
        menuOpciones.SetInputFecha(this, day, month, year);
    }

    public void AceptarInputFecha(int _year, int _month, int _day)
    {
        day = _day; month = _month; year = _year;
        valorCategoria.text = menuOpciones.GetDateString();
    }

    public DateTime GetFecha()
    {
        Debug.Log(day + " " + month + " " + year);
        return new DateTime(year, month, day); ;
    }

    public override void HabilitarInput(bool _aux)
    {
        GetComponent<Button>().enabled = _aux;
    }

    public string GetFechaPlaceholder()
    {
        return valorCategoria.text;
    }
}
