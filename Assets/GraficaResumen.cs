using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraficaResumen : MonoBehaviour
{
    [SerializeField] private GameObject eventoPrefab = null;
    [SerializeField] private Transform parentTransformEventos = null;

    [SerializeField] private GameObject adviceText = null;

    [SerializeField] private FlechasScroll flechasScroll = null;
    [SerializeField] private ScrollRect scrollRectVertical = null;
    [SerializeField] private ScrollRect scrollRectHorizontal = null;

    [SerializeField] private GameObject inicialPrefab = null;
    [SerializeField] private Transform parentIniciales = null;

    [SerializeField] private GameObject filaPrefab = null;
    [SerializeField] private Transform parentTransformFilas= null;

    private Jugador jugadorFocus;
    private Partido partidoFocus;
    private List<Evento> eventos;

    private List<GameObject> listaPrefabs;

    private int cantMinima;
    private float prefabHeight;

    private void Awake()
    {
        listaPrefabs = new List<GameObject>();
    }

    private void FixedUpdate()
    {
        flechasScroll.Actualizar(scrollRectVertical, cantMinima, listaPrefabs.Count/2);
    }

    public void SetGraficaResumen(Partido _partido, Jugador _jugador=null)
    {
        gameObject.SetActive(true);

        Screen.orientation = ScreenOrientation.Landscape;

        scrollRectHorizontal.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.height - parentIniciales.GetComponent<RectTransform>().rect.width - 50f, scrollRectHorizontal.GetComponent<RectTransform>().sizeDelta.y);
        prefabHeight = filaPrefab.GetComponent<RectTransform>().rect.height;

        if (_partido.IsPartido())
        {
            CanvasController.instance.overlayPanel.SetNombrePanel("Resumen del partido: ".ToUpper() + _partido.GetNombre().ToUpper(), AppController.Idiomas.Español);
            CanvasController.instance.overlayPanel.SetNombrePanel("Summary of match: ".ToUpper() + _partido.GetNombre().ToUpper(), AppController.Idiomas.Ingles);
        }
        else
        {
            CanvasController.instance.overlayPanel.SetNombrePanel("Resumen de la práctica: ".ToUpper() + _partido.GetNombre().ToUpper(), AppController.Idiomas.Español);
            CanvasController.instance.overlayPanel.SetNombrePanel("Summary of practice: ".ToUpper() + _partido.GetNombre().ToUpper(), AppController.Idiomas.Ingles);
        }

        partidoFocus = _partido;
        jugadorFocus = _jugador;

        eventos = partidoFocus.GetEventos();

        if (eventos.Count == 0)
            adviceText.SetActive(true);
        else
            adviceText.SetActive(false);

        ResetPrefabs();
    }

    private void ResetPrefabs()
    {
        BorrarPrefabs();
        CrearPrefabs2();
    }

    private void BorrarPrefabs()
    {
        if (listaPrefabs == null) return;

        foreach (var go in listaPrefabs)
            Destroy(go);

        listaPrefabs.Clear();
    }

    private void CrearPrefabs()
    {
        foreach (var evento in eventos)
        {
            if (jugadorFocus != null)
            {
                if (evento.GetAutor() == jugadorFocus)
                    NuevoEventoPrefab(evento);
            }
            else
            {
                NuevoEventoPrefab(evento);
            }
        }

        //cantMinima = (int)(scrollRectEventos.GetComponent<RectTransform>().rect.height / (prefabHeight + parentTransformEventos.GetComponent<VerticalLayoutGroup>().spacing));
    }


    private void CrearPrefabs2()
    {
        //Crear un dictionary<tipoEvento, list<Evento>>
        List<EstadisticaDeporte.Estadisticas> _tiposEventos = new List<EstadisticaDeporte.Estadisticas>();

        List<FilaEvento> _listaFilas = new List<FilaEvento>();

        foreach (var _evento in eventos)
        {
            if (!_tiposEventos.Contains(_evento.GetTipoEstadistica()))
            {
                _tiposEventos.Add(_evento.GetTipoEstadistica());

                GameObject inicialGO = Instantiate(inicialPrefab, parentIniciales, false);
                inicialGO.SetActive(true);
                inicialGO.GetComponent<Text>().text = _evento.GetInicialTipo();

                GameObject filaGO = Instantiate(filaPrefab, parentTransformFilas, false);
                filaGO.SetActive(true);

                listaPrefabs.Add(inicialGO);
                listaPrefabs.Add(filaGO);

                FilaEvento _fila = filaGO.GetComponent<FilaEvento>();
                _fila.SetTipoEvento(_evento.GetTipoEstadistica());
                _fila.AgregarPrefabEvento(_evento);
                _listaFilas.Add(filaGO.GetComponent<FilaEvento>());
            }
            else
            {
                foreach (var _fila in _listaFilas)
                {
                    if (_fila.GetTipoEstadistica() == _evento.GetTipoEstadistica())
                    {
                        _fila.AgregarPrefabEvento(_evento);
                        break;
                    }
                }
            }
        }

        foreach (var _fila in _listaFilas)
        {
            _fila.ResetPrefabs();
        }

        cantMinima = (int)(scrollRectVertical.GetComponent<RectTransform>().rect.width / (prefabHeight + parentTransformEventos.GetComponent<VerticalLayoutGroup>().spacing))-2;
    }


    private void NuevoEventoPrefab(Evento _evento)
    {
        GameObject go = Instantiate(eventoPrefab, parentTransformEventos, false);
        go.SetActive(true);
        BotonEvento goEvento = go.GetComponent<BotonEvento>();
        goEvento.SetEventoFocus(_evento);
        listaPrefabs.Add(go);
    }
}
