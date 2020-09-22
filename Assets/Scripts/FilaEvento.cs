using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FilaEvento : MonoBehaviour
{
    [SerializeField] private GameObject eventoPrefab = null;
    [SerializeField] private Transform parentTransform = null;
    [SerializeField] private RectTransform rectTransform = null;

    private List<Evento> eventos;
    private List<GameObject> listaPrefabs;
    private EstadisticaDeporte.Estadisticas tipoEstadistica;

    private void Start()
    {
        eventos = new List<Evento>();
        listaPrefabs = new List<GameObject>();
    }

    public void SetTipoEvento(EstadisticaDeporte.Estadisticas _tipoEstadistica)
    {
        tipoEstadistica = _tipoEstadistica;
    }

    public void AgregarPrefabEvento(Evento _evento)
    {
        if (eventos == null)
            eventos = new List<Evento>();
        eventos.Add(_evento);
    }

    public void ResetPrefabs()
    {
        BorrarPrefabs();
        CrearPrefabs();
    }

    public void BorrarPrefabs()
    {
        if (listaPrefabs == null)
            return;
        foreach (var _eventoGo in listaPrefabs)
        {
            Destroy(_eventoGo);
        }
        listaPrefabs.Clear();
    }

    public void CrearPrefabs()
    {
        if (eventos == null)
            return;
        if (listaPrefabs == null)
            listaPrefabs = new List<GameObject>();

        int offset = 0;
        float deltaX = 30;

        Evento lastEvento = null;

        for (int i = 0; i < eventos.Count; i++)
        {
            GameObject eventoGO = Instantiate(eventoPrefab, parentTransform, false);
            eventoGO.SetActive(true);

            BotonEvento _botonEvento = eventoGO.GetComponent<BotonEvento>();
            _botonEvento.SetEventoFocus(eventos[i]);

            if (lastEvento!=null)
            {
                if (Mathf.Floor(eventos[i].GetTiempo()) == Mathf.Floor(lastEvento.GetTiempo()))
                    offset++;
            }

            Vector3 localPos = eventoGO.transform.localPosition;
            eventoGO.transform.localPosition = new Vector3((Mathf.Floor(eventos[i].GetTiempo())+offset)*deltaX, localPos.y, localPos.z);

            lastEvento = eventos[i];
            listaPrefabs.Add(eventoGO);
        }

        rectTransform.sizeDelta = new Vector2(listaPrefabs[listaPrefabs.Count - 1].transform.localPosition.x + 8f, rectTransform.sizeDelta.y);
    }

    public EstadisticaDeporte.Estadisticas GetTipoEstadistica()
    {
        return tipoEstadistica;
    }
}
