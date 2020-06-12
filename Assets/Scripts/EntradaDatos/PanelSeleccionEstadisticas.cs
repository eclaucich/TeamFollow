using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Este es el panel principal de las entradas de datos, todos los deportes mostrarán este panel
/// Posee todos los prefabs de las estadísticas de los distintos deportes
/// </summary>

public class PanelSeleccionEstadisticas : Panel {

    public static PanelSeleccionEstadisticas instance = null;   //singleton

    [SerializeField] private MensajeError mensajeError = null;

    //Selección Partido-Práctica
    [SerializeField] private Button seleccionarPartido = null;
    [SerializeField] private Button seleccionarPractica = null;
    [SerializeField] private Color colorSeleccionado = new Color();
    [SerializeField] private Color colorNoSeleccionado = new Color();
    [SerializeField] private FlechasScroll flechasScroll = null;
    private bool isPartido = true;

    //Prefabs de estadísticas
    [SerializeField] private GameObject panelEstadisticasBasket = null;
    [SerializeField] private GameObject panelEstadisticasFutbol = null;
    [SerializeField] private GameObject panelEstadisticasHandball = null;
    [SerializeField] private GameObject panelEstadisticasHockeyCesped = null;
    [SerializeField] private GameObject panelEstadisticasHockeyPatines = null;
    [SerializeField] private GameObject panelEstadisticasPadel = null;
    [SerializeField] private GameObject panelEstadisticasRugby = null;
    [SerializeField] private GameObject panelEstadisticasSoftball = null;
    [SerializeField] private GameObject panelEstadisticasTenis = null;
    [SerializeField] private GameObject panelEstadisticasVoley = null;

    private PanelEstadisticas panelActual = null;

    //Lista para simplificar sintaxis
    private List<PanelEstadisticas> listaPaneles = null;

    int cantMinima;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }

        listaPaneles = new List<PanelEstadisticas>();

        listaPaneles.Add(panelEstadisticasBasket.GetComponent<PanelEstadisticas>());
        listaPaneles.Add(panelEstadisticasFutbol.GetComponent<PanelEstadisticas>());
        listaPaneles.Add(panelEstadisticasHandball.GetComponent<PanelEstadisticas>());
        listaPaneles.Add(panelEstadisticasHockeyCesped.GetComponent<PanelEstadisticas>());
        listaPaneles.Add(panelEstadisticasHockeyPatines.GetComponent<PanelEstadisticas>());
        listaPaneles.Add(panelEstadisticasPadel.GetComponent<PanelEstadisticas>());
        listaPaneles.Add(panelEstadisticasRugby.GetComponent<PanelEstadisticas>());
        listaPaneles.Add(panelEstadisticasSoftball.GetComponent<PanelEstadisticas>());
        listaPaneles.Add(panelEstadisticasTenis.GetComponent<PanelEstadisticas>());
        listaPaneles.Add(panelEstadisticasVoley.GetComponent<PanelEstadisticas>());


        seleccionarPartido.GetComponent<Image>().color = colorSeleccionado;
        seleccionarPractica.GetComponent<Image>().color = colorNoSeleccionado;
    }

    public override void Start()
    {
        base.Start();

        AppController.instance.overlayPanel.SetNombrePanel("ESTADISTICAS");
    }

    private void FixedUpdate()
    {
        if (panelActual != null && cantMinima>0)
        {
            flechasScroll.Actualizar(panelActual.GetScrollRect(), cantMinima, panelActual.GetChildCount());
        }
    }

    /// 
    /// Según el deporte enfocado se activa el panel correspondiente
    /// 
    public void SetearPanelEstadisticas()
    {
        for (int i = 0; i < listaPaneles.Count; i++)
        {
            listaPaneles[i].Desactivar();
        }

        panelActual = listaPaneles[(int)AppController.instance.equipoActual.GetDeporte()];
        panelActual.Activar();

        mensajeError.Desactivar();

        cantMinima = panelActual.GetCantMInima();
        Debug.Log("CANT MINIMA: " + cantMinima); 
    }

    /// 
    /// Se obtiene una lista con los nombres de las estadísticas del deporte adecuado
    /// 
    public List<string> GetListaEstadisticas()
    {
        for (int i = 0; i < listaPaneles.Count; i++)
        {
            if (listaPaneles[i].gameObject.activeSelf)
            {
                return listaPaneles[i].GetListaEstadisticasActivas();
            }
        }

        return null;
    }

    public List<string> GetListaInicialesEstadisticas()
    {
        for (int i = 0; i < listaPaneles.Count; i++)
        {
            if (listaPaneles[i].gameObject.activeSelf)
            {
                return listaPaneles[i].GetListaInicialesEstadisticasActivas();
            }
        }

        return null;
    }

    ///
    /// Se ejecuta desde los botones de Partido-Practica, permite cambiar la opción, cambiando cómo se muestran en pantalla
    ///
    public void CambiarPartidoPractica()
    {
        isPartido = !isPartido;
        if (isPartido)
        {
            seleccionarPartido.GetComponent<Image>().color = colorSeleccionado;
            seleccionarPractica.GetComponent<Image>().color = colorNoSeleccionado;
        }
        else
        {
            seleccionarPartido.GetComponent<Image>().color = colorNoSeleccionado;
            seleccionarPractica.GetComponent<Image>().color = colorSeleccionado;
        }
    }


    /// 
    /// Se ejecuta desde el botón presente en el panel de seleccion de estadisticas,
    /// Permite crear el prefab adecuado al deporte
    ///
    public void IniciarSeleccionJugadores()
    {
        int cantEstSeleccionadas = GetListaEstadisticas().Count;

        if (AppController.instance.equipoActual.GetJugadores().Count == 0)
            MostrarMensajeError("Este equipo no tiene jugadores");
        else if (cantEstSeleccionadas == 0)
            MostrarMensajeError("No hay estadísticas seleccionadas");
        else
            GetComponentInParent<PanelEntradaDatos>().MostrarPanelNuevaEntradaDatos(isPartido);
    }

    public void MostrarMensajeError(string error_)
    {
        mensajeError.SetText(error_);
        mensajeError.Activar();
    }
}
