using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PanelEdicion : MonoBehaviour, IPointerClickHandler, IDragHandler, IPointerUpHandler
{
    PanelCrearJugadas panelCrearJugada;

    [SerializeField] private Camera snapshotCamera = null;

    [SerializeField] private Texture fullField = null;
    [SerializeField] private Texture halfField = null;
    [SerializeField] private Texture areaField = null;

    private int deporteIndex = 0;

    private List<Texture> texturesFutbol;
    private List<Texture> texturesBasket;
    private List<Texture> texturesHockey;
    private List<Texture> texturesHandball;
    private List<Texture> texturesPadel;
    private List<Texture> texturesSoftball;

    private int currentTextureIndex = 0;

    int width = 1280;
    int height = 720;

    private void Awake()
    {
        panelCrearJugada = GetComponentInParent<PanelCrearJugadas>();
        snapshotCamera.gameObject.SetActive(false);

        texturesFutbol = new List<Texture>();
        texturesFutbol.Add(fullField);
        texturesFutbol.Add(halfField);
        texturesFutbol.Add(areaField);
        GetComponent<RawImage>().texture = texturesFutbol[currentTextureIndex];
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(panelCrearJugada.GetHerramientaActual() != null && panelCrearJugada.GetHerramientaActual().GetNombre() != "Flecha")
        {
            panelCrearJugada.UsarHerramientaActual();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (panelCrearJugada.GetHerramientaActual().GetNombre() == "Flecha" && Input.GetMouseButton(0))
        {
            panelCrearJugada.UsarHerramientaActual();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (panelCrearJugada.GetHerramientaActual().GetNombre() == "Flecha")
        {
            panelCrearJugada.GetHerramientaActual().DejarDeUsar();
        }
    }

    public void LimpiarPanel()
    {
        List<GameObject> childs = new List<GameObject>();

        foreach (Transform child in transform)
        {
            childs.Add(child.gameObject);
        }

        foreach (var child in childs)
        {
            Destroy(child);
        }
    }

    private void LateUpdate()
    {
        if (snapshotCamera.gameObject.activeInHierarchy)
        {
            CanvasController.instance.GetComponent<Canvas>().worldCamera = snapshotCamera;
            Texture2D snapshot = new Texture2D(width, height, TextureFormat.RGB24, false);
            snapshotCamera.Render();
            RenderTexture.active = snapshotCamera.targetTexture;
            snapshot.ReadPixels(new Rect(0, 0, width, height), 0, 0);
            byte[] bytes = snapshot.EncodeToPNG();
            SaveSystem.GuardarJugadaImagen(bytes);
            Debug.Log("Guardado");
            snapshotCamera.gameObject.SetActive(false);
            CanvasController.instance.GetComponent<Canvas>().worldCamera = Camera.main;
        }
    }

    public void GuardarJugadaImagen()
    {
        snapshotCamera.gameObject.SetActive(true);
        snapshotCamera.targetTexture = new RenderTexture(width, height, 24);
    }


    public void NextBackgroundImage()
    {
        currentTextureIndex = currentTextureIndex == texturesFutbol.Count - 1 ? 0 : currentTextureIndex + 1;

        switch (deporteIndex) ///CADA CASE HACE REFERENCIA A UN DEPORTE
        {
            case 0:
                GetComponent<RawImage>().texture = texturesFutbol[currentTextureIndex];
                break;
            case 1:
                GetComponent<RawImage>().texture = texturesFutbol[currentTextureIndex];
                break;
            case 2:
                GetComponent<RawImage>().texture = texturesFutbol[currentTextureIndex];
                break;
            case 3:
                GetComponent<RawImage>().texture = texturesFutbol[currentTextureIndex];
                break;
            case 4:
                GetComponent<RawImage>().texture = texturesFutbol[currentTextureIndex];
                break;
        }   
    }

    public void ChangeSport(int i)
    {
        deporteIndex = i;
        NextBackgroundImage();
    }
}
