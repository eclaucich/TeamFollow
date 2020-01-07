using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deportes : MonoBehaviour
{
    public static Deportes instance = null;

    public enum Deporte
    {
        Basket,
        Futbol,
        Handball,
        HockeyCesped,
        HockeyPatines,
        Padel,
        Rugby,    
        Softball,
        Tenis,
        Voley,

        NULL,
    }


    private void Awake()
    {
        if (instance == null)                                                                //Control del singleton
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }

        DontDestroyOnLoad(this);
    }
}
