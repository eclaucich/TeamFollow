using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraficaResumen : MonoBehaviour
{
    [SerializeField] private GameObject eventoPrefab = null;
    [SerializeField] private Transform parentTransform = null;

    private Partido partidoFocus;
    private List<Evento> eventos;

    private List<GameObject> listaPrefabs;

    private void Awake()
    {
        listaPrefabs = new List<GameObject>();
    }

    public void SetGraficaResumen(Partido _partido)
    {
        Screen.orientation = ScreenOrientation.Landscape;

        gameObject.SetActive(true);

        partidoFocus = _partido;
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
            GameObject go = Instantiate(eventoPrefab, parentTransform, false);
            go.SetActive(true);
            BotonEvento goEvento = go.GetComponent<BotonEvento>();
            goEvento.SetEventoFocus(evento);
            listaPrefabs.Add(go);
        }
    }
}
