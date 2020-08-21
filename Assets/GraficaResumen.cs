using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraficaResumen : MonoBehaviour
{
    [SerializeField] private GameObject eventoPrefab = null;
    [SerializeField] private Transform parentTransform = null;

    private Jugador jugadorFocus;
    private Partido partidoFocus;
    private List<Evento> eventos;

    private List<GameObject> listaPrefabs;

    private void Awake()
    {
        listaPrefabs = new List<GameObject>();
    }

    public void SetGraficaResumen(Partido _partido, Jugador _jugador=null)
    {
        Screen.orientation = ScreenOrientation.Landscape;

        gameObject.SetActive(true);

        partidoFocus = _partido;
        jugadorFocus = _jugador;
        eventos = partidoFocus.GetEventos();

        ResetPrefabs();
    }

    private void ResetPrefabs()
    {
        BorrarPrefabs();
        CrearPrefabs();
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
    }

    private void NuevoEventoPrefab(Evento _evento)
    {
        GameObject go = Instantiate(eventoPrefab, parentTransform, false);
        go.SetActive(true);
        BotonEvento goEvento = go.GetComponent<BotonEvento>();
        goEvento.SetEventoFocus(_evento);
        listaPrefabs.Add(go);
    }
}
