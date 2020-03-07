using UnityEngine;
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

    [SerializeField] private Text mensajeError = null;

    //private InfoJugador infoJugador;

    //private List<GameObject> listaPrefabs;
    private List<InputPrefab> inputsString;
    private List<InputPrefab> inputsInt;
    private List<InputPrefab> inputsEspecial;

    private InfoJugador infoJugador;

    public override void Start()
    {
        base.Start();

        infoJugador = new InfoJugador();

        //listaPrefabs = new List<GameObject>();
        inputsString = new List<InputPrefab>();
        inputsInt = new List<InputPrefab>();
        inputsEspecial = new List<InputPrefab>();
        //inputAltura.keyboardType = TouchScreenKeyboardType.NumberPad;
        //inputPeso.keyboardType = TouchScreenKeyboardType.NumberPad;

        mensajeError.gameObject.SetActive(false);

        //GO.transform.GetChild(0).GetComponent<Text>().text = "Fecha Nacimiento";
        //listaPrefabs.Add(GO);

        InfoJugador infoJugadorAux = new InfoJugador();

        inputsString = new List<InputPrefab>();
        inputsInt = new List<InputPrefab>();
        inputsEspecial = new List<InputPrefab>();

        foreach (var info in infoJugadorAux.GetInfoString())
        {
            GameObject go = Instantiate(prefabInputInfo, parentTransform);
            InputPrefab IPgo = go.GetComponent<InputPrefab>();
            IPgo.SetNombreCategoria(info.Key.ToString());
            inputsString.Add(IPgo);

            //go.transform.GetChild(0).GetComponent<Text>().text = info.Key.ToString();
            //listaPrefabs.Add(go);
        }

        foreach (var info in infoJugadorAux.GetInfoInt())
        {
            GameObject go = Instantiate(prefabInputInfo, parentTransform);
            InputPrefab IPgo = go.GetComponent<InputPrefab>();
            IPgo.SetNombreCategoria(info.Key.ToString());
            inputsInt.Add(IPgo);

            //go.transform.GetChild(0).GetComponent<Text>().text = info.Key.ToString();
            //listaPrefabs.Add(go);
        }

        foreach (var info in infoJugadorAux.GetInfoEspecial())
        {
            GameObject go = Instantiate(prefabInputInfoEspecial, parentTransform);
            InputPrefabEspecial IPgo = go.GetComponent<InputPrefabEspecial>();
            IPgo.SetNombreCategoria(info.Key.ToString());
            inputsEspecial.Add(IPgo);
        }

        GameObject GO = Instantiate(prefabInputInfo, parentTransform);
        InputPrefab IPGO = GO.GetComponent<InputPrefab>();
        IPGO.SetNombreCategoria("Fecha Nacimiento");
    }

    public void SetPanel()
    {
        if(inputsString != null)
            foreach (var input in inputsString)
                input.ResetValor();
        if (inputsInt != null)
            foreach (var input in inputsInt)
                input.ResetValor();
        if (inputsEspecial != null)
            foreach (var input in inputsEspecial)
                input.ResetValor();
    }

    public void GuardarNuevoJugador()
    {
        Equipo equipoActual = AppController.instance.equipoActual;

        /*if (equipoActual.BuscarPorNombre(nombreJugadorText.text) != null || nombreJugadorText.text == "" || nombreJugadorText.text == " ")
        {
            mensajeError.gameObject.SetActive(true);
            mensajeError.text = "Nombre Inválido/Existente";
            return;
        }*/
  
        mensajeError.gameObject.SetActive(false);

        ///**
        /// SETEAR EL INFOJUGADOR CON EL VALOR DE CADA PREFAB INPUT
        /// 

        foreach (var input in inputsString)
            infoJugador.SetInfoString(input);

        foreach (var input in inputsEspecial)
            infoJugador.SetInfoEspecial(input);

        //infoJugador.SetNombre(listaPrefabs[0].GetValorCategoria());
        //Debug.Log("Nombre Jugador: " + listaPrefabs[0].GetValorCategoria());

        equipoActual.NuevoJugador(new Jugador(infoJugador));



        CanvasController.instance.MostrarPanelAnterior();
    }
}
