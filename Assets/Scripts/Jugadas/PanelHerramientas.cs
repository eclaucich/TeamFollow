using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelHerramientas : MonoBehaviour
{
    [SerializeField] private PanelCrearJugadas panelCrearJugadas = null;
    [SerializeField] private GameObject botonDesplegar = null;
    [SerializeField] private Sprite spriteLockOpen = null;
    [SerializeField] private Sprite spriteLockClose = null;
    [SerializeField] private Image lockButtonImage = null;
    private bool blocked = false;

    private void Start()
    {
        blocked = false;
        //gameObject.SetActive(false);
        lockButtonImage.sprite = spriteLockOpen;
    }

    public PanelCrearJugadas GetPanelCrearJugadas()
    {
        return panelCrearJugadas;
    }

    public void ToogleActive()
    {
        if(!blocked)
        {
            GetComponent<MensajeDesplegable>().ToggleDesplegar();
        }
        Debug.Log("Togle Herramientas");
            //gameObject.SetActive(!gameObject.activeSelf);
    }

    public void ToggleBlock()
    {
        AndroidManager.HapticFeedback();
        
        blocked = !blocked;
        if (blocked)
        {
            lockButtonImage.sprite = spriteLockClose;
            botonDesplegar.SetActive(false);
        }
        else
        {
            lockButtonImage.sprite = spriteLockOpen;
            botonDesplegar.SetActive(true);
        }
    }

    public bool isActive()
    {
        return GetComponent<MensajeDesplegable>().isDesplegado();
    }
}
