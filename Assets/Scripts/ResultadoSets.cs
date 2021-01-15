using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultadoSets : ResultadoEntradaDatos
{
    [SerializeField] private GameObject setPrefab = null;
    [SerializeField] private Transform setParentTransform = null;

    [SerializeField] private GameObject botonAñadirSet = null;
    [SerializeField] private GameObject botonEliminarSet = null;

    private List<SetPrefab> listaSets;

    private void Awake()
    {
        listaSets = new List<SetPrefab>();

        //AgregarSet();
        SetBotonesAñadirEliminar();
    }

    public ResultadoSets(SaveDataResultadoSets _res)
    {
        List<SaveDataSet> _list = _res.listaSets;

        listaSets = new List<SetPrefab>();
        foreach (var set in _list)
        {
            listaSets.Add(new SetPrefab(set));
        }

        SetResultado(false);
    }

    public override void SetResultado(bool fromInputs=true)
    {
        int setsGanados = 0;
        foreach (var set in listaSets)
        {
            set.SetResultado(fromInputs);
            if (set.IsGanado())
                setsGanados++;
        }
        int setsPerdidos = listaSets.Count - setsGanados;

        if (setsGanados > setsPerdidos)
            resultado = Resultado.Victoria;
        else if (setsGanados < setsPerdidos)
            resultado = Resultado.Derrota;
        else
            resultado = Resultado.Empate;
    }

    public override bool VerificarInputs()
    {
        foreach (var set in listaSets)
        {
            if (!set.VerificarInputs())
                return false;
        }
        return true;
    }

    public void AgregarSet()
    {
        GameObject go = Instantiate(setPrefab, setParentTransform, false);
        go.SetActive(true);
        listaSets.Add(go.GetComponent<SetPrefab>());

        SetBotonesAñadirEliminar();
    }

    public void AgregarSet(SetPrefab _set)
    {
        GameObject go = Instantiate(setPrefab, setParentTransform, false);
        go.SetActive(true);
        
        SetPrefab setGO = go.GetComponent<SetPrefab>();
        setGO.CopyDataFrom(_set);
        listaSets.Add(setGO);
    }

    public void BorrarPrefabs()
    {
        foreach (var set in listaSets)
        {
            Destroy(set.gameObject);
        }
        listaSets.Clear();
    }

    public void EliminarSet()
    {
        Destroy(listaSets[listaSets.Count - 1].gameObject);
        listaSets.RemoveAt(listaSets.Count - 1);

        SetBotonesAñadirEliminar();
    }

    public void SetBotonesAñadirEliminar()
    {
        if (listaSets.Count == 5)
            botonAñadirSet.SetActive(false);
        else
            botonAñadirSet.SetActive(true);

        if (listaSets.Count == 1)
            botonEliminarSet.SetActive(false);
        else
            botonEliminarSet.SetActive(true);

        botonEliminarSet.transform.SetAsLastSibling();
        botonAñadirSet.transform.SetAsLastSibling();
    }

    public List<SetPrefab> GetListaSets()
    {
        return listaSets;
    }

    public void DisableEdition()
    {
        botonAñadirSet.SetActive(false);
        botonEliminarSet.SetActive(false);
        foreach (var set in listaSets)
        {
            set.DisableEdition();
        }
    }

    public void ActivateEdition()
    {
        botonAñadirSet.SetActive(true);
        botonEliminarSet.SetActive(true);
        foreach (var set in listaSets)
        {
            set.ActivateEdition();
        }
    }
}
