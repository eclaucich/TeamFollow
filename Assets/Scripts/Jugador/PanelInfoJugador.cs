using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PanelInfoJugador : Panel
{
    [SerializeField] private InfoPrefab prefabInputInfo = null;
    [SerializeField] private Transform parentTransform = null;

    [SerializeField] private ScrollRect scrollRect = null;
    [SerializeField] private GameObject flechaArriba = null;
    [SerializeField] private GameObject flechaAbajo = null;

    private InfoJugador infoJugador;

    private List<InfoPrefab> listaPrefabs = null;
  
    void Awake()
    {
        listaPrefabs = new List<InfoPrefab>();
    }

    private void FixedUpdate()
    {
        if (parentTransform.childCount < 6)
        {
            scrollRect.enabled = false;
            flechaAbajo.SetActive(false);
            flechaArriba.SetActive(false);
        }
        else
        {
            scrollRect.enabled = true;

            if (scrollRect.verticalNormalizedPosition > .95f) flechaArriba.SetActive(false); else flechaArriba.SetActive(true);
            if (scrollRect.verticalNormalizedPosition < 0.05f) flechaAbajo.SetActive(false); else flechaAbajo.SetActive(true);
        }
    }


    public void SetearPanelInfoJugador(Jugador jugador)
    {
        AppController.instance.overlayPanel.SetNombrePanel(jugador.GetNombre());

        infoJugador = jugador.GetInfoJugador();

        BorrarPrefabs();
        CrearPrefabs();
    }

    private void CrearPrefabs()
    {
        if(listaPrefabs == null) return;

        foreach (var info in infoJugador.GetInfoObligatoria())
        {
            InfoPrefab IPgo = Instantiate(prefabInputInfo, parentTransform);
            IPgo.SetNombreCategoria(info.Key.ToString());
            IPgo.SetValorCategoria(info.Value.ToString());
            listaPrefabs.Add(IPgo);
        }

        foreach (var info in infoJugador.GetInfoString())
        {
            InfoPrefab IPgo = Instantiate(prefabInputInfo, parentTransform);
            IPgo.SetNombreCategoria(info.Key.ToString());
            IPgo.SetValorCategoria(info.Value.ToString());
            listaPrefabs.Add(IPgo);

            //go.transform.GetChild(0).GetComponent<Text>().text = info.Key.ToString();
            //listaPrefabs.Add(go);
        }

        foreach (var info in infoJugador.GetInfoInt())
        {
            InfoPrefab IPgo = Instantiate(prefabInputInfo, parentTransform);
            IPgo.SetNombreCategoria(info.Key.ToString());
            IPgo.SetValorCategoria(info.Value.ToString());
            listaPrefabs.Add(IPgo);

            //go.transform.GetChild(0).GetComponent<Text>().text = info.Key.ToString();
            //listaPrefabs.Add(go);
        }

        foreach (var info in infoJugador.GetInfoEspecial())
        {
            InfoPrefab IPgo = Instantiate(prefabInputInfo, parentTransform);
            IPgo.SetNombreCategoria(info.Key.ToString());
            IPgo.SetValorCategoria(info.Value.ToString());
            listaPrefabs.Add(IPgo);
        }

        InfoPrefab IPGO = Instantiate(prefabInputInfo, parentTransform);
        IPGO.SetNombreCategoria("Fecha Nacimiento");
        IPGO.SetValorCategoria(infoJugador.GetFechaNac().ToString());
        listaPrefabs.Add(IPGO);
        
        //GO.transform.GetChild(0).GetComponent<Text>().text = "Fecha Nacimiento";
        //listaPrefabs.Add(GO);
    }

    private void BorrarPrefabs()
    {
        if(listaPrefabs == null) return;

        for(int i=0; i<listaPrefabs.Count; i++)
        {
            Destroy(listaPrefabs[i].gameObject);
        }

        listaPrefabs.Clear();
    }
}
