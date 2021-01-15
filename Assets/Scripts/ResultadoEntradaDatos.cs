using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ResultadoEntradaDatos : MonoBehaviour
{
    public enum Resultado
    {
        Victoria,
        Derrota,
        Empate
    }

    protected Resultado resultado;

    public Resultado GetResultado()
    {
        return resultado;
    }
    public abstract void SetResultado(bool fromInputs=true);
    public abstract bool VerificarInputs();
}
