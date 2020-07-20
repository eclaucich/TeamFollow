﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PanelNuevoJugador : Panel
{
    //[SerializeField] private Text nombreJugadorText = null;
    //[SerializeField] private InputField inputPeso = null;
    //[SerializeField] private InputField inputAltura = null;
    [SerializeField] private Transform parentTransform = null;
    [SerializeField] private GameObject prefabInputInfo = null;
    [SerializeField] private GameObject prefabInputInfoEspecial = null;
    [SerializeField] private GameObject prefabInputFecha = null;

    [SerializeField] private MensajeError mensajeError = null;

    [SerializeField] private ScrollRect scrollRect = null;
    [SerializeField] private FlechasScroll flechasScroll = null;

    //private InfoJugador infoJugador;

    //private List<GameObject> listaPrefabs;
    private List<InputPrefab> inputsString;
    private List<InputPrefab> inputsInt;
    private List<InputPrefab> inputsEspecial;
    private List<InputPrefab> inputsObligatorios;
    private InputPrefabFecha inputFecha;
    private InfoJugador infoJugador;

    private int cantMinima;
    private float prefabHeight;

    private void Start()
    {
        infoJugador = new InfoJugador();

        //listaPrefabs = new List<GameObject>();
        inputsString = new List<InputPrefab>();
        inputsInt = new List<InputPrefab>();
        inputsEspecial = new List<InputPrefab>();
        inputsObligatorios = new List<InputPrefab>();
        //inputAltura.keyboardType = TouchScreenKeyboardType.NumberPad;
        //inputPeso.keyboardType = TouchScreenKeyboardType.NumberPad;

        mensajeError.Desactivar();

        //GO.transform.GetChild(0).GetComponent<Text>().text = "Fecha Nacimiento";
        //listaPrefabs.Add(GO);

        InfoJugador infoJugadorAux = new InfoJugador();

        /*inputsString = new List<InputPrefab>();
        inputsInt = new List<InputPrefab>();
        inputsEspecial = new List<InputPrefab>();*/

        prefabHeight = prefabInputInfo.GetComponent<RectTransform>().rect.height;

        foreach (var info in infoJugador.GetInfoObligatoria())
        {
            GameObject go = Instantiate(prefabInputInfo, parentTransform);
            go.gameObject.SetActive(true);
            InputPrefab IPgo = go.GetComponent<InputPrefab>();
            IPgo.SetNombreCategoria(info.Key.ToString());
            IPgo.SetCampoObligatorio(true);
            IPgo.SetKeyboardType(TouchScreenKeyboardType.Default);
            inputsObligatorios.Add(IPgo);
        }

        GameObject GO = Instantiate(prefabInputFecha, parentTransform);
        GO.SetActive(true);
        inputFecha = GO.GetComponent<InputPrefabFecha>();
        inputFecha.SetCampoObligatorio(true);
        inputFecha.SetNombreCategoria("Fecha Nacimiento");
        inputFecha.ResetValor();

        foreach (var info in infoJugadorAux.GetInfoString())
        {
            GameObject go = Instantiate(prefabInputInfo, parentTransform);
            go.SetActive(true);
            InputPrefab IPgo = go.GetComponent<InputPrefab>();
            IPgo.SetNombreCategoria(info.Key.ToString());
            IPgo.SetKeyboardType(TouchScreenKeyboardType.Default);
            inputsString.Add(IPgo);

            //go.transform.GetChild(0).GetComponent<Text>().text = info.Key.ToString();
            //listaPrefabs.Add(go);
        }

        foreach (var info in infoJugadorAux.GetInfoInt())
        {
            GameObject go = Instantiate(prefabInputInfo, parentTransform);
            go.SetActive(true);
            InputPrefab IPgo = go.GetComponent<InputPrefab>();
            IPgo.SetNombreCategoria(info.Key.ToString());
            IPgo.SetKeyboardType(TouchScreenKeyboardType.NumberPad);
            inputsInt.Add(IPgo);

            //go.transform.GetChild(0).GetComponent<Text>().text = info.Key.ToString();
            //listaPrefabs.Add(go);
        }

        foreach (var info in infoJugadorAux.GetInfoEspecial())
        {
            GameObject go = Instantiate(prefabInputInfoEspecial, parentTransform);
            go.SetActive(true);
            InputPrefabEspecial IPgo = go.GetComponent<InputPrefabEspecial>();
            IPgo.SetNombreCategoria(info.Key.ToString());
            inputsEspecial.Add(IPgo);
        }

    }

    public void SetPanel()
    {
        if(inputsObligatorios != null)
            foreach (var input in inputsObligatorios)
                input.ResetValor();
        if (inputsString != null)
            foreach (var input in inputsString)
                input.ResetValor();
        if (inputsInt != null)
            foreach (var input in inputsInt)
                input.ResetValor();
        if (inputsEspecial != null)
            foreach (var input in inputsEspecial)
                input.ResetValor();
        if (inputFecha != null)
            inputFecha.ResetValor();

        if(prefabHeight == 0) prefabHeight = prefabInputInfo.GetComponent<RectTransform>().rect.height;
        cantMinima = (int)(scrollRect.GetComponent<RectTransform>().rect.height / (prefabHeight + parentTransform.GetComponent<VerticalLayoutGroup>().spacing));
    }

    private void FixedUpdate()
    {
        if (prefabHeight == 0) prefabHeight = prefabInputInfo.GetComponent<RectTransform>().rect.height;
        
        flechasScroll.Actualizar(scrollRect, cantMinima, inputsString.Count + inputsEspecial.Count + inputsObligatorios.Count + 1);
    }

    public void GuardarNuevoJugador()
    {
        Equipo equipoActual = AppController.instance.equipoActual;
        
        InfoJugador ij = new InfoJugador();
        
        if (!inputFecha.IsDateValid())
        {
            mensajeError.SetText("Completar campos obligatorios (*)");
            mensajeError.Activar();
            return;
        }
        foreach (var input in inputsObligatorios)
        {
            if (input.GetValorCategoria() == "")
            {
                mensajeError.SetText("Completar campos obligatorios (*)");
                mensajeError.Activar();
                return;
            }
            ij.SetInfoObligatoria(input);
        }

        //Reviasr si existe el nombre (hacer una función de comporbación de nombres general en appcontroller
        if (equipoActual.BuscarPorNombre(ij.GetNombre()) != null || ij.GetNombre() == "" || ij.GetNombre() == " ")
        {
            mensajeError.SetText("Nombre inválido/existente");
            mensajeError.Activar();
            return;
        }

        foreach (var input in inputsString)
            ij.SetInfoString(input);

        foreach (var input in inputsEspecial)
            ij.SetInfoEspecial(input);

        ij.SetFechaNac(inputFecha.GetFecha());

        equipoActual.NuevoJugador(ij);

        CanvasController.instance.MostrarPanelAnterior();
    }
}
