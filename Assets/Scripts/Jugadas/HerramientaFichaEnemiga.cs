using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HerramientaFichaEnemiga : Herramienta {

    [SerializeField] private GameObject fichaEnemigaPrefab = null;

    private void Start()
    {
        nombre = "Ficha Enemiga";
    }

    public override void Usar()
    {
        Transform parent = GameObject.Find("PanelEdicion").transform;
        GameObject go = Instantiate(fichaEnemigaPrefab, parent, false);
        go.transform.SetPositionAndRotation(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10), Quaternion.identity);

        go.transform.GetChild(0).GetComponent<MeshRenderer>().material = ElegirMaterial(PanelCrearJugada.instance.GetColorActual());
        go.transform.GetChild(1).GetComponent<MeshRenderer>().material = ElegirMaterial(PanelCrearJugada.instance.GetColorActual());
    }
}
