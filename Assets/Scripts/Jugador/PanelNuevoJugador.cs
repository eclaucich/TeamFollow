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
    [SerializeField] private GameObject prefabInputFecha = null;

    [SerializeField] private Text mensajeError = null;
    [SerializeField] private Text mensajeCampoObligatorio = null;

    [SerializeField] private ScrollRect scrollRect = null;
    [SerializeField] private GameObject flechaArriba = null;
    [SerializeField] private GameObject flechaAbajo = null;

    //private InfoJugador infoJugador;

    //private List<GameObject> listaPrefabs;
    private List<InputPrefab> inputsString;
    private List<InputPrefab> inputsInt;
    private List<InputPrefab> inputsEspecial;
    private List<InputPrefab> inputsObligatorios;
    private InputPrefabFecha inputFecha;
    private InfoJugador infoJugador;

    public override void Start()
    {
        base.Start();

        infoJugador = new InfoJugador();

        //listaPrefabs = new List<GameObject>();
        inputsString = new List<InputPrefab>();
        inputsInt = new List<InputPrefab>();
        inputsEspecial = new List<InputPrefab>();
        inputsObligatorios = new List<InputPrefab>();
        //inputAltura.keyboardType = TouchScreenKeyboardType.NumberPad;
        //inputPeso.keyboardType = TouchScreenKeyboardType.NumberPad;

        mensajeError.gameObject.SetActive(false);
        mensajeCampoObligatorio.gameObject.SetActive(false);

        //GO.transform.GetChild(0).GetComponent<Text>().text = "Fecha Nacimiento";
        //listaPrefabs.Add(GO);

        InfoJugador infoJugadorAux = new InfoJugador();

        /*inputsString = new List<InputPrefab>();
        inputsInt = new List<InputPrefab>();
        inputsEspecial = new List<InputPrefab>();*/

        foreach (var info in infoJugador.GetInfoObligatoria())
        {
            GameObject go = Instantiate(prefabInputInfo, parentTransform);
            InputPrefab IPgo = go.GetComponent<InputPrefab>();
            IPgo.SetNombreCategoria(info.Key.ToString());
            IPgo.SetCampoObligatorio(true);
            IPgo.SetKeyboardType(TouchScreenKeyboardType.Default);
            inputsObligatorios.Add(IPgo);
        }

        foreach (var info in infoJugadorAux.GetInfoString())
        {
            GameObject go = Instantiate(prefabInputInfo, parentTransform);
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
            InputPrefabEspecial IPgo = go.GetComponent<InputPrefabEspecial>();
            IPgo.SetNombreCategoria(info.Key.ToString());
            inputsEspecial.Add(IPgo);
        }

        GameObject GO = Instantiate(prefabInputFecha, parentTransform);
        inputFecha = GO.GetComponent<InputPrefabFecha>();
        inputFecha.SetNombreCategoria("Fecha Nacimiento");
        inputFecha.ResetValor();
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
    }

    private void FixedUpdate()
    {
        if (parentTransform.childCount < 6)
        {
            scrollRect.enabled = false;
            flechaAbajo.SetActive(false);
            flechaArriba.SetActive(false);
        }
        else
        {
            scrollRect.enabled = true;

            if (scrollRect.verticalNormalizedPosition > .95f) flechaArriba.SetActive(false); else flechaArriba.SetActive(true);
            if (scrollRect.verticalNormalizedPosition < 0.05f) flechaAbajo.SetActive(false); else flechaAbajo.SetActive(true);
        }
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
        

        InfoJugador ij = new InfoJugador();

        foreach (var input in inputsObligatorios)
        {
            if (input.GetValorCategoria() == "")
            {
                mensajeCampoObligatorio.gameObject.SetActive(true);
                return;
            }
            ij.SetInfoObligatoria(input);
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
