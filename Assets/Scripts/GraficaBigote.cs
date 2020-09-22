using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraficaBigote : Grafica
{

    [SerializeField] private DiagramaBigote diagramaBigote = null;

    public override void Graficar<T>(Dictionary<T, int> datos, bool setSize=true)
    {
        debug = true;
        base.Graficar(datos,false);

        //Datos -> <Jugador, valor> (estadistica global del jugador en el equipo
        float Q2;
        if (debug)
        {
            List<int> sortedList = new List<int>();
            for (int i = 0; i < valores.Length; i++)
                sortedList.Add(valores[i]);
            sortedList.Sort();
            Q2 = sortedList[(int)Mathf.Floor(sortedList .Count / 2)];
        }
        else
        {
            List<int> _valores = new List<int>();
            foreach (var _dato in datos)
                _valores.Add(_dato.Value);
            _valores.Sort();

            Q2 = _valores[(int)Mathf.Floor(_valores.Count / 2)];
        }

        float Q1 = (Q2 - info.min) / 2;
        float Q3 = Q2 + (info.max - Q2) / 2;
        float bigoteSup = Q3 + 1.5f * (Q3 - Q1);
        float bigoteInf = Q3 + 1.5f * (Q3 - Q1);

        Debug.Log("max: " + info.max);
        Debug.Log("min: " + info.min);

        diagramaBigote.SetDiagrama(Q1, Q2, Q3, bigoteSup, bigoteInf);
    }

    public override void CrearPrefabs<T>(Dictionary<T, int> datos, bool isDatoPartido)
    {
        //Setear la lista de jugadores
        //Por cada jugador crear un boton con el nombre del jugador y su valor al lado
    }

    public override void ResetGraph()
    {
        //Borrar los prefabs de la lista de jugadores
    }
}
