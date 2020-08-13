using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetPrefab : MonoBehaviour
{
    [SerializeField] private InputField inputPropio = null;
    [SerializeField] private InputField inputContrario = null;

    [SerializeField] private InputField inputTBPropio = null;
    [SerializeField] private InputField inputTBContrario = null;

    private int valorPropio;
    private int valorContrario;

    private int valorTBPropio;
    private int valorTBContrario;

    private bool isTieBreak;
    private bool setGanado;

    private void Start()
    {
        inputPropio.keyboardType = TouchScreenKeyboardType.NumberPad;
        inputContrario.keyboardType = TouchScreenKeyboardType.NumberPad;

        isTieBreak = false;
    }

    public SetPrefab(SaveDataSet _set)
    {
        valorPropio = _set.valorPropio;
        valorContrario = _set.valorContrario;

        isTieBreak = _set.isTieBreak;
        valorTBPropio = _set.valorTBPropio;
        valorTBContrario = _set.valorTBContrario;
    }

    public void SetResultado(bool fromInputs=true)
    {
        if (fromInputs)
        {
            if (!int.TryParse(inputPropio.text, out valorPropio))
                valorPropio = 0;
            if (!int.TryParse(inputContrario.text, out valorContrario))
                valorContrario = 0;
        }

        setGanado = valorPropio > valorContrario;

        if (fromInputs) isTieBreak = (inputTBPropio.text != "0" || inputTBContrario.text != "0");
        else isTieBreak = (valorTBPropio != 0 || valorTBContrario != 0);

        if (isTieBreak)
        {
            if (fromInputs)
            {
                if (!int.TryParse(inputTBPropio.text, out valorTBPropio))
                    valorTBPropio = 0;
                if (!int.TryParse(inputTBContrario.text, out valorTBContrario))
                    valorTBContrario = 0;
            }

            setGanado = valorTBPropio > valorTBContrario;
        }
        else
            setGanado = valorPropio > valorContrario;
    }

    public bool VerificarInputs()
    {
        if (inputPropio.text == "" || inputContrario.text == "")
            return false;
        if (isTieBreak)
        {
            if (inputTBPropio.text == "" || inputTBContrario.text == "")
                return false;
        }

        return true;
    }

    public void CopyDataFrom(SetPrefab _set)
    {
        valorPropio = _set.GetResultadoPropio();
        valorContrario = _set.GetResultadoContrario();

        valorTBPropio = _set.GetResultaoTBPropio();
        valorTBContrario = _set.GetResultadoTBContrario();

        Debug.Log("PROP: " + valorPropio);
        Debug.Log("CONT: " + valorContrario);
        Debug.Log("TB PROP: " + valorTBPropio);
        Debug.Log("TB CONT: " + valorTBContrario);

        SetInputsText();
    }

    public void DisableEdition()
    {
        inputPropio.readOnly = true;
        inputContrario.readOnly = true;

        inputTBPropio.readOnly = true;
        inputTBContrario.readOnly = true;
    }

    public void ActivateEdition()
    {
        inputPropio.readOnly = false;
        inputContrario.readOnly = false;

        inputTBPropio.readOnly = false;
        inputTBContrario.readOnly = false;
    }

    public void SetInputsText()
    {
        inputPropio.text = valorPropio.ToString();
        inputContrario.text = valorContrario.ToString();

        inputTBPropio.text = valorTBPropio.ToString();
        inputTBContrario.text = valorTBContrario.ToString();
    }

    #region Getters
    public bool IsGanado()
    {
        return setGanado;
    }

    public int GetResultadoPropio()
    {
        return valorPropio;
    }

    public int GetResultadoContrario()
    {
        return valorContrario;
    }

    public bool IsTieBreak()
    {
        return isTieBreak;
    }

    public int GetResultaoTBPropio()
    {
        return valorTBPropio;
    }

    public int GetResultadoTBContrario()
    {
        return valorTBContrario;
    }
    #endregion
}
