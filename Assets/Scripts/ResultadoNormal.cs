using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultadoNormal : ResultadoEntradaDatos
{
    [SerializeField] private InputField inputPropio = null;
    [SerializeField] private InputField inputContrario = null;

    [SerializeField] private InputField inputPenalesPropio = null;
    [SerializeField] private InputField inputPenalesContrario = null;

    [SerializeField] private GameObject toggleInputPenales = null;

    private int valorPropio;
    private int valorContrario;

    private int valorPenalesPropio;
    private int valorPenalesContrario;

    private bool isPenales;
    private bool isEditable;

    private void Start()
    {
        inputPropio.keyboardType = TouchScreenKeyboardType.NumberPad;
        inputContrario.keyboardType = TouchScreenKeyboardType.NumberPad;

        inputPenalesPropio.keyboardType = TouchScreenKeyboardType.NumberPad;
        inputPenalesContrario.keyboardType = TouchScreenKeyboardType.NumberPad;

        isPenales = false;
        isEditable = true;
    }

    public ResultadoNormal(SaveDataResultadoNormal _saveDataResultado)
    {
        valorPropio = _saveDataResultado.valorPropio;
        valorContrario = _saveDataResultado.valorContrario;

        isPenales = _saveDataResultado.isPenales;
        valorPenalesPropio = _saveDataResultado.valorPenalesPropio;
        valorPenalesContrario = _saveDataResultado.valorPenalesContrario;

        SetResultado(false);
    }

    public void ToggleInputPenales()
    {
        isPenales = !isPenales;
        inputPenalesPropio.gameObject.SetActive(isPenales);
        inputPenalesContrario.gameObject.SetActive(isPenales);
    }

    public override void SetResultado(bool fromInputs=true)
    {
        if(fromInputs) 
            GetValuesFromInputs();

        if (valorPropio > valorContrario)
            resultado = Resultado.Victoria;
        else if (valorPropio < valorContrario)
            resultado = Resultado.Derrota;
        else
        {
            if (isPenales)
            {
                if (valorPenalesPropio > valorPenalesContrario)
                    resultado = Resultado.Victoria;
                else if (valorPenalesPropio < valorPenalesContrario)
                    resultado = Resultado.Derrota;
            }
            else
            {
                resultado = Resultado.Empate;
            }
        }
    }

    public override bool VerificarInputs()
    {
        if (inputPropio.text == "" || inputContrario.text == "")
            return false;
        if (isPenales)
        {
            if (inputPenalesPropio.text == "" || inputPenalesContrario.text == "")
                return false;
        }

        return true;
    }

    public void CopyDataFrom(ResultadoNormal _res)
    {
        valorPropio = _res.GetResultadoPropio();
        valorContrario = _res.GetResultadoContrario();

        valorPenalesPropio = _res.GetResultadoPenalesPropio();
        valorPenalesContrario = _res.GetResultadoPenalesContrario();

        isPenales = _res.IsPenales();

        SetResultado(false);

        SetInputsText();
    }


    #region Activar y Desactivar edicion
    public void DisableEdition(bool hasPenales=true)
    {
        inputPropio.readOnly = true;
        inputContrario.readOnly = true;

        isPenales = hasPenales;

        if (isPenales)
        {
            inputPenalesPropio.gameObject.SetActive(true);
            inputPenalesContrario.gameObject.SetActive(true);
            inputPenalesPropio.readOnly = true;
            inputPenalesContrario.readOnly = true;
        }
        else
        {
            inputPenalesPropio.gameObject.SetActive(false);
            inputPenalesContrario.gameObject.SetActive(false);
        }
        toggleInputPenales.gameObject.SetActive(false);

        isEditable = false;
    }

    public void ActivateEdition(bool hasPenales=true)
    {
        inputPropio.readOnly = false;
        inputContrario.readOnly = false;

        isPenales = hasPenales;

        if (isPenales)
        {
            inputPenalesPropio.gameObject.SetActive(true);
            inputPenalesContrario.gameObject.SetActive(true);
            inputPenalesPropio.readOnly = false;
            inputPenalesContrario.readOnly = false; 
        }
        else
        {
            inputPenalesPropio.gameObject.SetActive(false);
            inputPenalesContrario.gameObject.SetActive(false);
        }
        toggleInputPenales.SetActive(hasPenales);

        isEditable = true;
    }
    #endregion

    private void GetValuesFromInputs()
    {
        if (!int.TryParse(inputPropio.text, out valorPropio))
            valorPropio = 0;
        if (!int.TryParse(inputContrario.text, out valorContrario))
            valorContrario = 0;

        if (!int.TryParse(inputPenalesPropio.text, out valorPenalesPropio))
            valorPenalesPropio = 0;
        if (!int.TryParse(inputPenalesContrario.text, out valorPenalesContrario))
            valorPenalesContrario = 0;
    }

    private void SetInputsText()
    {
        inputPropio.text = valorPropio.ToString();
        inputContrario.text = valorContrario.ToString();

        inputPenalesPropio.text = valorPenalesPropio.ToString();
        inputPenalesContrario.text = valorPenalesContrario.ToString();
    }

    #region Getters
    public int GetResultadoPropio()
    {
        return valorPropio;
    }

    public int GetResultadoContrario()
    {
        return valorContrario;
    }

    public bool IsPenales()
    {
        return isPenales;
    }

    public int GetResultadoPenalesPropio()
    {
        return valorPenalesPropio;
    }

    public int GetResultadoPenalesContrario()
    {
        return valorPenalesContrario;
    }
    #endregion
}
