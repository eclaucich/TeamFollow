using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelHerramientas : MonoBehaviour
{
    [SerializeField] private PanelCrearJugadas panelCrearJugadas = null;
    [SerializeField] private Sprite spriteLockOpen = null;
    [SerializeField] private Sprite spriteLockClose = null;
    [SerializeField] private Image lockButtonImage = null;
    private bool blocked = false;

    private void Start()
    {
        blocked = false;
        gameObject.SetActive(false);
        lockButtonImage.sprite = spriteLockOpen;
    }

    public PanelCrearJugadas GetPanelCrearJugadas()
    {
        return panelCrearJugadas;
    }

    public void ToogleActive()
    {
        if(!blocked)
            gameObject.SetActive(!gameObject.activeSelf);
    }

    public void ToggleBlock()
    {
        blocked = !blocked;
        if (blocked)
            lockButtonImage.sprite = spriteLockClose;
        else
            lockButtonImage.sprite = spriteLockOpen;
    }

    public bool isActive()
    {
        return gameObject.activeSelf;
    }
}
