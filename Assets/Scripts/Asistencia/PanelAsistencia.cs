using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelAsistencia : Panel
{
    protected int hojaActual;
    protected int cantidadHojas;
    [SerializeField] private GameObject hojaAsistenciaPrefab = null;
    [SerializeField] private Transform parentTransformHojas = null;
    [SerializeField] private Text numeroHojaText = null;

    protected List<GameObject> listaPrefabsHojas;
    protected List<DetalleAsistencia> detalles;

    override public void Start()
    {
        base.Start();
    }

    virtual public void SetPanelPlanilla()
    {
        BorrarPrefabs();

        hojaActual = 1;

        numeroHojaText.text = hojaActual + "/" + cantidadHojas;
    }

    public void CrearPrefabsHoja()
    {
        listaPrefabsHojas = new List<GameObject>();
        for (int i = 0; i < cantidadHojas; i++)
        {
            GameObject hojaAsistenciaGO = Instantiate(hojaAsistenciaPrefab, parentTransformHojas, false);
            hojaAsistenciaGO.SetActive(true);
            hojaAsistenciaGO.transform.SetAsFirstSibling();

            hojaAsistenciaGO.GetComponent<HojaAsistencia>().SetHojaAsistencia(detalles, i);

            listaPrefabsHojas.Add(hojaAsistenciaGO);
        }
    }

    public void CrearPrefabsHoja(List<Jugador> jugadores)
    {
        listaPrefabsHojas = new List<GameObject>();
        detalles = new List<DetalleAsistencia>();

        for (int i = 0; i < cantidadHojas; i++)
        {
            GameObject hojaAsistenciaGO = Instantiate(hojaAsistenciaPrefab, parentTransformHojas, false);
            hojaAsistenciaGO.SetActive(true);
            hojaAsistenciaGO.transform.SetAsFirstSibling();

            detalles.AddRange(hojaAsistenciaGO.GetComponent<HojaAsistencia>().SetHojaAsistencia(jugadores, i));

            listaPrefabsHojas.Add(hojaAsistenciaGO);
        }
    }

    public void BorrarPrefabs()
    {
        if (listaPrefabsHojas == null) return;
        for (int i = 0; i < listaPrefabsHojas.Count; i++)
            Destroy(listaPrefabsHojas[i]);

        listaPrefabsHojas.Clear();
    }

    public void HojaSiguiente()
    {
        if (hojaActual + 1 > cantidadHojas)
        {
            hojaActual = cantidadHojas;
            return;
        }

        listaPrefabsHojas[hojaActual - 1].GetComponent<HojaAsistencia>().AnimacionSiguiente(true);
        hojaActual++;
        listaPrefabsHojas[hojaActual - 1].GetComponent<HojaAsistencia>().AnimacionSiguiente(false);

        numeroHojaText.text = hojaActual + "/" + cantidadHojas;
    }

    public void HojaAnterior()
    {
        if (hojaActual - 1 < 1)
        {
            hojaActual = 1;
            return;
        }

        listaPrefabsHojas[hojaActual - 1].GetComponent<HojaAsistencia>().AnimacionAnterior(true);
        hojaActual--;
        listaPrefabsHojas[hojaActual - 1].GetComponent<HojaAsistencia>().AnimacionAnterior(false);

        numeroHojaText.text = hojaActual + "/" + cantidadHojas;
    }
}
