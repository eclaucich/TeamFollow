using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PanelInfoJugador : Panel
{
    [SerializeField] private Transform parentTransform = null;
    [SerializeField] private GameObject prefabInputInfo = null;
    [SerializeField] private GameObject prefabInputInfoEspecial = null;
    [SerializeField] private GameObject prefabInputFecha = null;

    [SerializeField] private ScrollRect scrollRect = null;
    [SerializeField] private FlechasScroll flechasScroll = null;

    [SerializeField] private GameObject botonGuardarCambios = null;
    [SerializeField] private GameObject textEditando = null;

    [SerializeField] private MensajeError mensajeError = null;

    private InfoJugador infoJugador;
    private Jugador jugadorFocus;

    //private List<InputPrefab> listaPrefabs = null;
    private List<InputPrefab> inputsString;
    private List<InputPrefab> inputsInt;
    private List<InputPrefab> inputsEspecial;
    private List<InputPrefab> inputsObligatorios;
    private InputPrefabFecha inputFecha;

    void Awake()
    {
        //listaPrefabs = new List<InputPrefab>();
        inputsString = new List<InputPrefab>();
        inputsInt = new List<InputPrefab>();
        inputsEspecial = new List<InputPrefab>();
        inputsObligatorios = new List<InputPrefab>();
    }

    private void FixedUpdate()
    {
        flechasScroll.Actualizar(scrollRect, 6, parentTransform.childCount);
        /*if (parentTransform.childCount < 6)
        {
            scrollRect.enabled = false;
            flechasScroll.Abajo(false);
            flechasScroll.Arriba(false);
        }
        else
        {
            scrollRect.enabled = true;

            if (scrollRect.verticalNormalizedPosition > .95f) flechasScroll.Arriba(false); else flechasScroll.Arriba(true);
            if (scrollRect.verticalNormalizedPosition < 0.05f) flechasScroll.Abajo(false); else flechasScroll.Abajo(true);
        }*/
    }


    public void SetearPanelInfoJugador(Jugador jugador)
    {
        AppController.instance.overlayPanel.SetNombrePanel(jugador.GetNombre());

        textEditando.SetActive(false);
        botonGuardarCambios.SetActive(false);

        infoJugador = jugador.GetInfoJugador();
        jugadorFocus = jugador;

        mensajeError.Desactivar();

        BorrarPrefabs();
        CrearPrefabs();
    }

    private void CrearPrefabs()
    {
        //if(listaPrefabs == null) return;

        foreach (var info in infoJugador.GetInfoObligatoria())
        {  
            InputPrefab IPgo = Instantiate(prefabInputInfo, parentTransform).GetComponent<InputPrefab>();
            IPgo.SetNombreCategoria(info.Key.ToString());
            IPgo.SetPlaceholder(info.Value.ToString());
            IPgo.HabilitarInput(false);
            inputsObligatorios.Add(IPgo);
        }

        InputPrefabFecha IPGO = Instantiate(prefabInputFecha, parentTransform).GetComponent<InputPrefabFecha>();
        IPGO.SetNombreCategoria("Fecha Nacimiento");
        IPGO.SetValorCategoria(infoJugador.GetFechaNac().ToShortDateString());
        IPGO.HabilitarInput(false);
        //listaPrefabs.Add(IPGO);
        inputFecha = IPGO;
        //GO.transform.GetChild(0).GetComponent<Text>().text = "Fecha Nacimiento";
        //listaPrefabs.Add(GO);

        foreach (var info in infoJugador.GetInfoString())
        {
            InputPrefab IPgo = Instantiate(prefabInputInfo, parentTransform).GetComponent<InputPrefab>();
            IPgo.SetNombreCategoria(info.Key.ToString());
            IPgo.SetPlaceholder(info.Value.ToString());
            IPgo.HabilitarInput(false);
            inputsString.Add(IPgo);

            //go.transform.GetChild(0).GetComponent<Text>().text = info.Key.ToString();
            //listaPrefabs.Add(go);
        }

        foreach (var info in infoJugador.GetInfoInt())
        {
            InputPrefab IPgo = Instantiate(prefabInputInfo, parentTransform).GetComponent<InputPrefab>();
            IPgo.SetNombreCategoria(info.Key.ToString());
            IPgo.SetPlaceholder(info.Value.ToString());
            IPgo.HabilitarInput(false);
            inputsInt.Add(IPgo);

            //go.transform.GetChild(0).GetComponent<Text>().text = info.Key.ToString();
            //listaPrefabs.Add(go);
        }

        foreach (var info in infoJugador.GetInfoEspecial())
        {
            InputPrefabEspecial IPgo = Instantiate(prefabInputInfoEspecial, parentTransform).GetComponent<InputPrefabEspecial>();
            IPgo.SetNombreCategoria(info.Key.ToString());
            IPgo.SetValor(info.Value.ToString());
            IPgo.HabilitarInput(false);
            inputsEspecial.Add(IPgo);
        }

    }

    private void BorrarPrefabs()
    {
        //if(listaPrefabs == null) return;

        /*for(int i=0; i<listaPrefabs.Count; i++)
        {
            Destroy(listaPrefabs[i].gameObject);
        }*/

        foreach (var input in inputsObligatorios)
            Destroy(input.gameObject);
        foreach (var input in inputsString)
            Destroy(input.gameObject);
        foreach (var input in inputsInt)
            Destroy(input.gameObject);
        foreach (var input in inputsEspecial)
            Destroy(input.gameObject);
        if(inputFecha != null) Destroy(inputFecha.gameObject);

        inputsObligatorios.Clear();
        inputsString.Clear();
        inputsInt.Clear();
        inputsEspecial.Clear();
    }

    public void HabilitarEdicion(bool _aux)
    {
        foreach (var input in inputsObligatorios)
            input.HabilitarInput(_aux);
        foreach (var input in inputsString)
            input.HabilitarInput(_aux);
        foreach (var input in inputsInt)
            input.HabilitarInput(_aux);
        foreach (var input in inputsEspecial)
            input.HabilitarInput(_aux);
        inputFecha.HabilitarInput(_aux);

        textEditando.SetActive(_aux);
        botonGuardarCambios.SetActive(_aux);
    }

    public void ConfirmarEdicion()
    {
        InfoJugador ij = new InfoJugador();

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
        if (AppController.instance.equipoActual.BuscarPorNombre(ij.GetNombre()) != null || ij.GetNombre() == "" || ij.GetNombre() == " ")
        {
            mensajeError.SetText("Nombre inválido/existente");
            mensajeError.Activar();
            return;
        }

        /*foreach (var input in inputsObligatorios)
        {
            if (input.GetPlaceholder() == "")
            {
                //mensajeCampoObligatorio.gameObject.SetActive(true);
                return;
            }
            ij.SetInfoObligatoriaPlaceholder(input);
        }*/


        foreach (var input in inputsString)
            ij.SetInfoStringPlaceholder(input);

        foreach (var input in inputsEspecial)
            ij.SetInfoEspecial(input);

        ij.SetFechaNac(inputFecha.GetFechaPlaceholder());

        jugadorFocus.Editar(ij);

        HabilitarEdicion(false);
        AppController.instance.overlayPanel.SetNombrePanel(jugadorFocus.GetNombre());
    }
}
