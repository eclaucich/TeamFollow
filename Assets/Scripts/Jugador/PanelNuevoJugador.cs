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

    [SerializeField] private Text mensajeError = null;

    //private InfoJugador infoJugador;

    //private List<GameObject> listaPrefabs;
    private List<InputPrefab> listaPrefabs;

    public override void Start()
    {
        base.Start();

        //listaPrefabs = new List<GameObject>();
        listaPrefabs = new List<InputPrefab>();
        //inputAltura.keyboardType = TouchScreenKeyboardType.NumberPad;
        //inputPeso.keyboardType = TouchScreenKeyboardType.NumberPad;

        mensajeError.gameObject.SetActive(false);

        InfoJugador infoJugador = new InfoJugador();

        foreach (var info in infoJugador.GetInfoString())
        {
            GameObject go = Instantiate(prefabInputInfo, parentTransform);
            InputPrefab IPgo = go.GetComponent<InputPrefab>();
            IPgo.SetNombreCategoria(info.Key.ToString());
            listaPrefabs.Add(IPgo);

            //go.transform.GetChild(0).GetComponent<Text>().text = info.Key.ToString();
            //listaPrefabs.Add(go);
        }

        foreach (var info in infoJugador.GetInfoInt())
        {
            GameObject go = Instantiate(prefabInputInfo, parentTransform);
            InputPrefab IPgo = go.GetComponent<InputPrefab>();
            IPgo.SetNombreCategoria(info.Key.ToString());
            listaPrefabs.Add(IPgo);

            //go.transform.GetChild(0).GetComponent<Text>().text = info.Key.ToString();
            //listaPrefabs.Add(go);
        }

        GameObject GO = Instantiate(prefabInputInfo, parentTransform);
        InputPrefab IPGO = GO.GetComponent<InputPrefab>();
        IPGO.SetNombreCategoria("Fecha Nacimiento");
        listaPrefabs.Add(IPGO);
        
        //GO.transform.GetChild(0).GetComponent<Text>().text = "Fecha Nacimiento";
        //listaPrefabs.Add(GO);
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
        InfoJugador infoJugador = new InfoJugador();

        mensajeError.gameObject.SetActive(false);

        ///**
        /// SETEAR EL INFOJUGADOR CON EL VALOR DE CADA PREFAB INPUT
        /// 
        infoJugador.SetNombre(listaPrefabs[0].GetValorCategoria());
        Debug.Log("Nombre Jugador: " + listaPrefabs[0].GetValorCategoria());

        equipoActual.NuevoJugador(new Jugador(infoJugador));

        CanvasController.instance.MostrarPanelAnterior();
    }
}
