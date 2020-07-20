using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelMenuSeleccion : MensajeDesplegable
{
    [SerializeField] private GameObject botonDespliegueMenu = null;
    [SerializeField] private float swipeMaxTolerance = 0.2f;

    private float swipeMax;
    private Vector2 vectInitialPos;
    private Vector2 vectFinalPos;
    private Vector2 vectSwipe;
    private float swipeDiff;
    private float yActual;
    private bool swipeEnabled = true;

    override public void Start()
    {
        base.Start();
        swipeMax = AppController.instance.resWidth * swipeMaxTolerance;
    }

   /* private void Update()
    {
        swipeMax = AppController.instance.resWidth * swipeMaxTolerance;
        if (Input.GetMouseButtonDown(0))
        {
            vectInitialPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            vectFinalPos = Camera.main.ScreenToWorldPoint(vectFinalPos);
        }

        if (Input.GetMouseButtonUp(0))
        {
            vectFinalPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            vectFinalPos = Camera.main.ScreenToWorldPoint(vectFinalPos);
            vectSwipe = vectFinalPos - vectInitialPos;

            swipeDiff = vectFinalPos.x - vectInitialPos.x;

            if (isDesplegado())
            {
                if (swipeDiff < 0 && (swipeDiff * -1) > swipeMax)
                    ToggleDesplegar();
            }
            else
            {
                if (swipeDiff > 0 && swipeDiff > swipeMax)
                    ToggleDesplegar();
            }
        }
    }*/
}
