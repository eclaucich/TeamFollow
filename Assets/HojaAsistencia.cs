using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HojaAsistencia : MonoBehaviour
{
    [SerializeField] private GameObject detalleAsistenciaPrefab = null;
    [SerializeField] private Transform parentTransform = null;

    private List<GameObject> listaPrefabs;

    private Animator animator;

    private int maxPorHoja = 13;

    public void SetHojaAsistencia(List<DetalleAsistencia> detalles, int numeroHoja)
    {
        listaPrefabs = new List<GameObject>();
        animator = GetComponent<Animator>();

        for (int i = numeroHoja*maxPorHoja, j=0; i < detalles.Count && j < maxPorHoja; i++, j++)
        {
            GameObject detalleGO = Instantiate(detalleAsistenciaPrefab, parentTransform, false);
            detalleGO.SetActive(true);

            detalleGO.GetComponent<DetalleAsistencia>().SetDetalle(detalles[i]);

            listaPrefabs.Add(detalleGO);
        }
    }

    public List<DetalleAsistencia> SetHojaAsistencia(List<Jugador> jugadores, int numeroHoja)
    {
        listaPrefabs = new List<GameObject>();
        animator = GetComponent<Animator>();

        List<DetalleAsistencia> detalles = new List<DetalleAsistencia>();

        for (int i = numeroHoja * maxPorHoja, j = 0; i < jugadores.Count && j < maxPorHoja; i++, j++)
        {
            GameObject detalleGO = Instantiate(detalleAsistenciaPrefab, parentTransform, false);
            detalleGO.SetActive(true);

            detalleGO.GetComponent<DetalleAsistencia>().SetNombreJugador(jugadores[i].GetNombre());

            detalles.Add(detalleGO.GetComponent<DetalleAsistencia>());

            listaPrefabs.Add(detalleGO);
        }

        return detalles;
    }

    public void AnimacionSiguiente(bool paginaActual)
    {
        if (paginaActual)
        {
            animator.Play("HojaAsistenciaSiguiente_1");
            //gameObject.SetActive(false);
        }
        else
        {
            //gameObject.SetActive(true);
            animator.Play("HojaAsistenciaSiguiente_2");
        }
    }

    public void AnimacionAnterior(bool paginaActual)
    {
        if (paginaActual)
        {
            animator.Play("HojaAsistenciaAnterior_1");
            //gameObject.SetActive(false);
        }
        else
        {
            //gameObject.SetActive(true);
            animator.Play("HojaAsistenciaAnterior_2");
        }
    }
}
