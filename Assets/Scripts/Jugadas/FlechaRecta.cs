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

    [SerializeField] private Material materialFlechaNormal = null;
    [SerializeField] private Material materialFlechaPunteada = null;
    private float dist = 0f;
    private int positionIndex = 0;

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
        Vector3 mPos = Input.mousePosition;
        mPos.z = 85f;
        Vector3 goPos = Camera.main.ScreenToWorldPoint(mPos);

        if (mode == 0)   //Flecha normal
        {
            Debug.Log("FLECHA NORMAL");
            if (line == null)
            {
                line = GetComponent<LineRenderer>();
                line.material = materialFlechaNormal;
                line.positionCount = 1;
                line.startColor = Color.black;
                line.endColor = Color.black;
                line.SetPosition(0, goPos);
            }

            if (currentTime >= timeBtwPoints)
            {
                line.positionCount++;
                line.SetPosition(line.positionCount - 1, goPos);
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
                line.transform.localPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f);
            }

        }
        else if(mode == 2)  //Flecha punteada
        {
            if (line == null)
            {
                line = GetComponent<LineRenderer>();
                line.material = materialFlechaPunteada;
                line.positionCount = 1;
                line.SetPosition(0, goPos);
                dist = 0f;
                positionIndex = 0;
            }

            if (currentTime >= timeBtwPoints)
            {
                line.positionCount++;
                line.SetPosition(line.positionCount - 1, goPos);
                currentTime = 0f;
                positionIndex++;
            }

            currentTime += Time.deltaTime;
            
            if(line.positionCount>2)
                dist += Vector2.Distance(line.GetPosition(positionIndex-1), line.GetPosition(positionIndex))/100;
            if (dist < 1) dist = 1;
        }
        else
        {
            Debug.Log("ERROR EN EL MODO DE FLECHA");
        }

    }

    public void CrearPunta(int mode)
    {
        Vector3 mPos = Input.mousePosition;
        mPos.z = 2f;
        Vector3 goPos = Camera.main.ScreenToWorldPoint(mPos);

        if (line == null) return;
        
        if(mode == 1)
        {
            line.positionCount++;
            line.SetPosition(line.positionCount - 1, goPos);
        }
        else if(mode==2)
        { 
            line.material.mainTextureScale = new Vector2(dist/2f, 1f);
        }

        positionInitial = line.GetPosition(line.positionCount - 2);
        positionFinal = line.GetPosition(line.positionCount - 1);

        float op = positionFinal.y - positionInitial.y;
        float ady = positionFinal.x - positionInitial.x;
        float rad = Mathf.Atan2(op, ady);
        float grados = rad * 180f / Mathf.PI;

        //transform.SetPositionAndRotation(positionFinal, Quaternion.identity);
        //transform.Rotate(Vector3.forward, grados);
        
        line.material.color = PanelCrearJugada.instance.GetColorActual();

        GameObject puntaFlechaGO = Instantiate(puntaFlecha);

        Vector3 positionFlecha = new Vector3(positionFinal.x, positionFinal.y, 3f);

        //puntaFlechaGO.transform.SetPositionAndRotation(positionFinal, Quaternion.identity);
        puntaFlechaGO.transform.localPosition = new Vector3(positionFlecha.x, positionFlecha.y, 0f);
        puntaFlechaGO.transform.Rotate(Vector3.forward, grados+90f);
        puntaFlechaGO.transform.SetParent(this.transform);
        puntaFlechaGO.GetComponent<PuntaFlecha>().SetMaterialColor(PanelCrearJugada.instance.GetColorActual());
        // Image imagen = GetComponent<Image>();
        // imagen.enabled = true;
    }
}
