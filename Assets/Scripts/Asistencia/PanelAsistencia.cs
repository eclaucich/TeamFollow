using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelAsistencia : Panel
{
    [SerializeField] private GameObject detalleAsistenciaPrefab = null;
    [SerializeField] private Transform detallesParentTransform = null;
    [SerializeField] private FlechasScroll flechasScroll = null;

    [SerializeField] private TextLanguage nombreEquipoText = null;
    [SerializeField] private TextLanguage placeholderNuevoAlias = null;

    [SerializeField] private TextLanguage leyendaInicialPresente = null;
    [SerializeField] private TextLanguage leyendaPresente = null;

    [SerializeField] private TextLanguage leyendaInicialTarde = null;
    [SerializeField] private TextLanguage leyendaTarde = null;

    [SerializeField] private TextLanguage leyendaInicialAusente = null;
    [SerializeField] private TextLanguage leyendaAusente = null;

    [SerializeField] private ScrollRect scrollRectDetalles = null;
    private List<GameObject> listaPrefabs;

    private int cantMinima;
    private float prefabHeight;

    private void Awake()
    {
        //detalles = new List<DetalleAsistencia>();
        listaPrefabs = new List<GameObject>();
        prefabHeight = detalleAsistenciaPrefab.GetComponent<RectTransform>().rect.height;
    }

    public void SetPanelAsistencia()
    {
        //Setear la info de la asistencia
        nombreEquipoText.SetText("EQUIPO:\n" + AppController.instance.equipoActual.GetNombre(), AppController.Idiomas.Español);
        nombreEquipoText.SetText("TEAM:\n" + AppController.instance.equipoActual.GetNombre(), AppController.Idiomas.Ingles);

        placeholderNuevoAlias.SetText("NUEVO NOMBRE ...", AppController.Idiomas.Español);
        placeholderNuevoAlias.SetText("NEW NAME ...", AppController.Idiomas.Ingles);

        leyendaInicialPresente.SetText("P", AppController.Idiomas.Español);
        leyendaInicialPresente.SetText("P", AppController.Idiomas.Ingles);
        leyendaPresente.SetText("PRESENTE", AppController.Idiomas.Español);
        leyendaPresente.SetText("PRESENT", AppController.Idiomas.Ingles);

        leyendaInicialTarde.SetText("T", AppController.Idiomas.Español);
        leyendaInicialTarde.SetText("L", AppController.Idiomas.Ingles);
        leyendaTarde.SetText("TARDE", AppController.Idiomas.Español);
        leyendaTarde.SetText("LATE", AppController.Idiomas.Ingles);

        leyendaInicialAusente.SetText("A", AppController.Idiomas.Español);
        leyendaInicialAusente.SetText("A", AppController.Idiomas.Ingles);
        leyendaAusente.SetText("AUSENTE", AppController.Idiomas.Español);
        leyendaAusente.SetText("ABSCENT", AppController.Idiomas.Ingles);
    }

    private void FixedUpdate()
    {
        flechasScroll.Actualizar(scrollRectDetalles, cantMinima, listaPrefabs.Count);
    }

    public void CrearPrefabs(List<DetalleAsistencia> _detalles, bool activarBoton)
    {
        BorrarPrefabs();

        foreach (var _detalle in _detalles)
        {
            GameObject detalleGO = Instantiate(detalleAsistenciaPrefab, detallesParentTransform, false);
            detalleGO.SetActive(true);

            detalleGO.GetComponent<DetalleAsistencia>().SetDetalle(_detalle, activarBoton);

            listaPrefabs.Add(detalleGO);
        }

        cantMinima = (int)(scrollRectDetalles.GetComponent<RectTransform>().rect.height / (prefabHeight + detallesParentTransform.GetComponent<VerticalLayoutGroup>().spacing + detallesParentTransform.GetComponent<VerticalLayoutGroup>().padding.top));
    }

    public List<DetalleAsistencia> CrearPrefabsDetalles(List<Jugador> jugadores)
    {
        if (listaPrefabs == null)
            listaPrefabs = new List<GameObject>();

        BorrarPrefabs();

        List<DetalleAsistencia> _listaDetalles = new List<DetalleAsistencia>();

        foreach (var _jugador in jugadores)
        {
            GameObject detalleGO = Instantiate(detalleAsistenciaPrefab, detallesParentTransform, false);
            detalleGO.SetActive(true);

            DetalleAsistencia _detalle = detalleGO.GetComponent<DetalleAsistencia>();

            _detalle.SetNombreJugador(_jugador.GetNombre());
            _detalle.SetAsistenciaInicial(0);

            _listaDetalles.Add(_detalle);

            listaPrefabs.Add(detalleGO);
        }

        cantMinima = (int)(scrollRectDetalles.GetComponent<RectTransform>().rect.height / (prefabHeight + detallesParentTransform.GetComponent<VerticalLayoutGroup>().spacing + detallesParentTransform.GetComponent<VerticalLayoutGroup>().padding.top));

        return _listaDetalles;
    }

    public List<DetalleAsistencia> CrearPrefabsDetalles(List<DetalleAsistencia> _detalles, bool _activarBoton)
    {
        List<DetalleAsistencia> _listaDetalles = new List<DetalleAsistencia>();

        BorrarPrefabs();

        foreach (var _detalle in _detalles)
        {
            GameObject detalleGO = Instantiate(detalleAsistenciaPrefab, detallesParentTransform, false);
            detalleGO.SetActive(true);

            DetalleAsistencia _det = detalleGO.GetComponent<DetalleAsistencia>();

            _det.SetDetalle(_detalle, _activarBoton);
            _listaDetalles.Add(_det);

            listaPrefabs.Add(detalleGO);
        }

        Debug.Log("PREF: " + listaPrefabs.Count);

        cantMinima = (int)(scrollRectDetalles.GetComponent<RectTransform>().rect.height / (prefabHeight + detallesParentTransform.GetComponent<VerticalLayoutGroup>().spacing + detallesParentTransform.GetComponent<VerticalLayoutGroup>().padding.top));

        return _listaDetalles;
    }

    public void BorrarPrefabs()
    {
        if (listaPrefabs == null)
            return;

        foreach (var _go in listaPrefabs)
            Destroy(_go);
        listaPrefabs.Clear();
    }
}
