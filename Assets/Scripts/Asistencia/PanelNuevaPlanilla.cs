using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelNuevaPlanilla : PanelAsistencia {

    //[SerializeField] private GameObject detalleAsistenciaPrefab = null;
    [SerializeField] private Text aliasPlanilla = null;
    [SerializeField] private Button botonGuardar = null;
    [SerializeField] private GameObject seccionError = null;
    [SerializeField] private GameObject seccionAdvice = null;

    private List<GameObject> listaPrefabsDetalles;
    //private List<DetalleAsistencia> listaDetallesAsistencias;

    private Equipo equipo;
    private List<Jugador> jugadores;

    //private Transform parentTransform;

    private void Awake()
    {
        jugadores = new List<Jugador>();
        listaPrefabsDetalles = new List<GameObject>();
        //listaDetallesAsistencias = new List<DetalleAsistencia>();
        //parentTransform = GameObject.Find("SeccionAsistencias").transform;        
    }

    public void SetPanelNuevaPlanilla()
    {
        if (AppController.instance.GetEquipoActual() != equipo)
            equipo = AppController.instance.equipoActual;

        jugadores = equipo.GetJugadores();

        cantidadHojas = Mathf.CeilToInt(jugadores.Count / 13f);

        base.SetPanelPlanilla();

        seccionError.SetActive(false);
        if (AppController.instance.equipoActual.GetJugadores().Count == 0)
        {
            seccionAdvice.SetActive(true);
            botonGuardar.interactable = false;
        }
        else
        {
            seccionAdvice.SetActive(false);
            botonGuardar.interactable = true;
        }

        CrearPrefabsHoja(jugadores);

        /*BorrarPrefabs();
        CrearPrefabs();   */
    }

    /*public void CrearPrefabs()
    {
        for (int i = 0; i < jugadores.Count; i++)
        {
            GameObject detalleAsistenciaGO = Instantiate(detalleAsistenciaPrefab, parentTransform, false);
            detalleAsistenciaGO.SetActive(true);
            listaPrefabsDetalles.Add(detalleAsistenciaGO);

            listaDetallesAsistencias.Add(listaPrefabsDetalles[i].GetComponent<DetalleAsistencia>());
            listaDetallesAsistencias[i].SetNombreJugador(jugadores[i].GetNombre());
        }
    }*/

    /*public void BorrarPrefabs()
    {
        if (listaPrefabsDetalles == null) return;

        for (int i = 0; i < listaPrefabsDetalles.Count; i++)
        {
            Destroy(listaPrefabsDetalles[i]);
        }
        listaPrefabsDetalles.Clear();
        listaDetallesAsistencias.Clear();
    }*/

    public void GuardarPlanilla()
    {
        System.DateTime timeNow = System.DateTime.Now;

        //string nombrePlanilla = dayInput.text + "-" + monthInput.text + "-" + yearInput.text + " - " + hourInput.text + "-" + minuteInput.text;
        string nombrePlanilla = timeNow.ToString("yyyy-MM-dd-HH-mm-ss");

        if (equipo.ExistePlanilla(nombrePlanilla, aliasPlanilla.text))
        {
            seccionError.SetActive(true);
            seccionError.GetComponentInChildren<Text>().text = "Planilla Existente!";
            return;
        }

        equipo.NuevaPlanilla(nombrePlanilla, aliasPlanilla.text, detalles);

        GetComponentInParent<PanelPlanillaAsistencia>().MostrarPanelHistorialPlanillas();
    }
}
