using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlechaRecta : ObjetoEdicion
{
    private RectTransform rectTransform;

    private Vector3 positionInitial;
    private Vector3 positionFinal;

    List<LineRenderer> lines; //Vector de lineas para la flecha punteada
    private bool firstLine = false;

    LineRenderer line;

    [SerializeField] private float timeBtwPoints = 0.3f;
    private float currentTime = 0f;

    [SerializeField] private GameObject puntaFlecha = null;

    private void Start()
    {
        line = new LineRenderer();
        lines = new List<LineRenderer>();
    }

    /*public void SetInitialPosition(Vector3 _position)
    {
        positionInitial = _position;
        gameObject.GetComponent<Image>().enabled = false;
    }

    public void SetFinalPosition(Vector3 _position)
    {
        positionFinal = _position;

        float op = positionFinal.y - positionInitial.y;
        float ady = positionFinal.x - positionInitial.x;
        float rad = Mathf.Atan2(op, ady);
        float grados = rad * 180f / Mathf.PI;

        float hip = Mathf.Pow(Mathf.Pow(ady,2)+Mathf.Pow(op,2), 0.5f);

        transform.Rotate(Vector3.forward,grados);

        gameObject.GetComponent<Image>().enabled = true;

        LineRenderer line = GetComponent<LineRenderer>();
        line.positionCount = 2;
        line.SetPosition(0, positionInitial);
        line.SetPosition(1, positionFinal);
    }*/

    public void CreateLineRenderer(int mode)
    {
        if(mode == 0)   //Flecha normal
        {
            if (line == null)
            {
                line = GetComponent<LineRenderer>();
                line.positionCount = 1;
                line.startColor = Color.black;
                line.endColor = Color.black;
                line.SetPosition(0, Input.mousePosition);
            }

            if (currentTime >= timeBtwPoints)
            {
                line.positionCount++;
                line.SetPosition(line.positionCount - 1, Input.mousePosition);
                currentTime = 0f;
            }

            currentTime += Time.deltaTime;
        }
        else if(mode == 1)  //Flecha recta
        {
            if (line == null)
            {
                line = GetComponent<LineRenderer>();
                line.positionCount = 1;
                line.startColor = Color.black;
                line.endColor = Color.black;
                line.SetPosition(0, Input.mousePosition);
            }

        }
        else if(mode == 2)  //Flecha punteada
        {
            

        }
        else
        {
            Debug.Log("ERROR EN EL MODE DE FLECHA RECTA");
        }
        
    }

    public void CrearPunta(int mode)
    {
        if(line == null) return;
        
        if(mode == 1)
        {
            line.positionCount++;
            line.SetPosition(line.positionCount - 1, Input.mousePosition);
        }
        positionInitial = line.GetPosition(line.positionCount - 2);
        positionFinal = line.GetPosition(line.positionCount - 1);

        float op = positionFinal.y - positionInitial.y;
        float ady = positionFinal.x - positionInitial.x;
        float rad = Mathf.Atan2(op, ady);
        float grados = rad * 180f / Mathf.PI;

        transform.SetPositionAndRotation(positionFinal, Quaternion.identity);
        //transform.Rotate(Vector3.forward, grados);

        line.startColor = PanelCrearJugada.instance.GetColorActual();
        line.endColor = PanelCrearJugada.instance.GetColorActual();

        GameObject puntaFlechaGO = Instantiate(puntaFlecha);

        positionFinal = new Vector3(positionFinal.x, positionFinal.y, 3f);

        puntaFlechaGO.transform.SetPositionAndRotation(positionFinal, Quaternion.identity);
        puntaFlechaGO.transform.Rotate(Vector3.forward, grados);
        puntaFlechaGO.transform.SetParent(this.transform);
        puntaFlechaGO.GetComponent<PuntaFlecha>().SetMaterialColor(PanelCrearJugada.instance.GetColorActual());
        // Image imagen = GetComponent<Image>();
        // imagen.enabled = true;
    }
}
