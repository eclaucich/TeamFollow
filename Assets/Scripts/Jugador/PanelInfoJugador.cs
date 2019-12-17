using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PanelInfoJugador : Panel
{
    /*[SerializeField] private Text nombreText = null;
    [SerializeField] private Text pesoText = null;
    [SerializeField] private Text alturaText = null;*/

    [SerializeField] private GameObject prefabInfo = null;
    [SerializeField] private Transform transformInfo = null;
    //[SerializeField] private List<string> listaInfo;

    private List<GameObject> categorias = null;
  

    public void SetearPanelInfoJugador(Jugador jugador)
    {
        /*nombreText.text = jugador.GetNombre();
        pesoText.text = jugador.GetPeso().ToString();
        alturaText.text = jugador.GetAltura().ToString();*/
        categorias = new List<GameObject>();

        InfoJugador infoJugador = jugador.GetInfoJugador();

        foreach (var info in infoJugador.GetInfoString())
        {
            GameObject GO = Instantiate(prefabInfo, transformInfo);
            GO.transform.GetChild(0).GetComponent<Text>().text = info.Key.ToString();
            GO.transform.GetChild(1).GetComponent<Text>().text = info.Value.ToString();
            categorias.Add(GO);
        }

        foreach (var info in infoJugador.GetInfoInt())
        {
            GameObject GO = Instantiate(prefabInfo, transformInfo);
            GO.transform.GetChild(0).GetComponent<Text>().text = info.Key.ToString();
            GO.transform.GetChild(1).GetComponent<Text>().text = info.Value.ToString();
            categorias.Add(GO);
        }

        GameObject go = Instantiate(prefabInfo, transformInfo);
        go.transform.GetChild(0).GetComponent<Text>().text = "Fecha Nacimiento";
        go.transform.GetChild(1).GetComponent<Text>().text = infoJugador.GetFechaNac().ToString();
        categorias.Add(go);
    }
}
