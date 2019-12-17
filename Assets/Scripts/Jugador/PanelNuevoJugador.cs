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

    private InfoJugador infoJugador;

    private List<GameObject> listaPrefabs;

    public override void Start()
    {
        base.Start();

        listaPrefabs = new List<GameObject>();
        //inputAltura.keyboardType = TouchScreenKeyboardType.NumberPad;
        //inputPeso.keyboardType = TouchScreenKeyboardType.NumberPad;

        mensajeError.gameObject.SetActive(false);

        infoJugador = new InfoJugador();

        foreach (var info in infoJugador.GetInfoString())
        {
            GameObject go = Instantiate(prefabInputInfo, parentTransform);
            go.transform.GetChild(0).GetComponent<Text>().text = info.Key.ToString();
            listaPrefabs.Add(go);
        }

        foreach (var info in infoJugador.GetInfoInt())
        {
            GameObject go = Instantiate(prefabInputInfo, parentTransform);
            go.transform.GetChild(0).GetComponent<Text>().text = info.Key.ToString();
            listaPrefabs.Add(go);
        }

        GameObject GO = Instantiate(prefabInputInfo, parentTransform);
        GO.transform.GetChild(0).GetComponent<Text>().text = "Fecha Nacimiento";
        listaPrefabs.Add(GO);
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



        equipoActual.NuevoJugador(new Jugador(infoJugador));

        CanvasController.instance.MostrarPanelAnterior();
    }
}
