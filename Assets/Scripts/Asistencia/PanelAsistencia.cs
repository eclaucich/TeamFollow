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

    [SerializeField] protected GameObject flechaSiguiente = null;
    [SerializeField] protected GameObject flechaAnterior = null;

    protected List<GameObject> listaPrefabsHojas;
    protected List<DetalleAsistencia> detalles;


    virtual public void SetPanelPlanilla()
    {
        BorrarPrefabs();

        hojaActual = 1;

        /*numeroHojaText.text = hojaActual + "/" + cantidadHojas;

        flechaAnterior.SetActive(false);
        if (cantidadHojas == 1)
            flechaSiguiente.SetActive(false);
        else
            flechaSiguiente.SetActive(true);*/

        flechaAnterior.SetActive(false);
        flechaSiguiente.SetActive(false);
        numeroHojaText.gameObject.SetActive(false);
    }

    public void CrearPrefabsHoja(bool activarBoton)
    {
        listaPrefabsHojas = new List<GameObject>();
        for (int i = 0; i < cantidadHojas; i++)
        {
            GameObject hojaAsistenciaGO = Instantiate(hojaAsistenciaPrefab, parentTransformHojas, false);
            hojaAsistenciaGO.SetActive(true);
            hojaAsistenciaGO.transform.SetAsFirstSibling();

            hojaAsistenciaGO.GetComponent<HojaAsistencia>().SetHojaAsistencia(detalles, i, activarBoton);

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

    public List<DetalleAsistencia> CrearPrefabsHoja(List<Jugador> jugadores, List<DetalleAsistencia> detalles)
    {
        listaPrefabsHojas = new List<GameObject>();
        List<DetalleAsistencia> newDetalles = new List<DetalleAsistencia>();

        /*foreach (var det in detalles)
        {
            newDetalles.Add(new DetalleAsistencia(det));
        }*/

        for (int i = 0; i < cantidadHojas; i++)
        {
            GameObject hojaAsistenciaGO = Instantiate(hojaAsistenciaPrefab, parentTransformHojas, false);
            hojaAsistenciaGO.SetActive(true);
            hojaAsistenciaGO.transform.SetAsFirstSibling();

            newDetalles.AddRange(hojaAsistenciaGO.GetComponent<HojaAsistencia>().SetHojaAsistenciaAux(detalles, i, true));

            listaPrefabsHojas.Add(hojaAsistenciaGO);
        }

        return newDetalles;
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
        /*if (hojaActual+1 >= cantidadHojas)
        {
            hojaActual = cantidadHojas;
            flechaSiguiente.SetActive(false);
            return;
        }*/

        listaPrefabsHojas[hojaActual - 1].GetComponent<HojaAsistencia>().AnimacionSiguiente(true);
        hojaActual++;
        listaPrefabsHojas[hojaActual - 1].GetComponent<HojaAsistencia>().AnimacionSiguiente(false);

        numeroHojaText.text = hojaActual + "/" + cantidadHojas;

        flechaAnterior.SetActive(true);
        if (hojaActual >= cantidadHojas)
            flechaSiguiente.SetActive(false);
        else
            flechaSiguiente.SetActive(true);
    }

    public void HojaAnterior()
    {
        /*if (hojaActual <= 1)
        {
            flechaAnterior.SetActive(false);
            hojaActual = 1;
            return;
        }*/

        listaPrefabsHojas[hojaActual - 1].GetComponent<HojaAsistencia>().AnimacionAnterior(true);
        hojaActual--;
        listaPrefabsHojas[hojaActual - 1].GetComponent<HojaAsistencia>().AnimacionAnterior(false);

        numeroHojaText.text = hojaActual + "/" + cantidadHojas;

        flechaSiguiente.SetActive(true);
        if (hojaActual <= 1)
            flechaAnterior.SetActive(false);
        else
            flechaAnterior.SetActive(true);
    }
}
