using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpcionesFecha : MonoBehaviour
{
    [SerializeField] private Dropdown dropdownDay = null;
    [SerializeField] private Dropdown dropdownMonth = null;
    [SerializeField] private Dropdown dropdownYear = null;

    private InputPrefabFecha inputFecha;

    private int minYear = 1920;
    private int maxYear = 2020;

    public void SetInputFecha(InputPrefabFecha _input, int day, int month, int year)
    {
        inputFecha = _input;

        List<string> optionsMonth = new List<string>();
        for (int i = 1; i <= 12; i++)
            optionsMonth.Add(i.ToString());
        dropdownMonth.AddOptions(optionsMonth);
        dropdownMonth.value = 0;

        List<string> optionsYear = new List<string>();
        for (int i = minYear; i <= maxYear; i++)
            optionsYear.Add(i.ToString());
        dropdownYear.AddOptions(optionsYear);

        int maxDays = (dropdownMonth.value == 3 || dropdownMonth.value == 5 || dropdownMonth.value == 8 || dropdownMonth.value == 10) ? 30 : 31;
        if (dropdownMonth.value == 1) maxDays = 28;

        List<string> optionsDay = new List<string>();
        for (int i = 1; i <= maxDays; i++)
            optionsDay.Add(i.ToString());
        dropdownDay.AddOptions(optionsDay);

        //CONTROLAR AÑOS BISIESTOS

        if (day > 0 && month > 0 && year >= minYear)
        {
            dropdownDay.value = day - 1;
            dropdownMonth.value = month - 1;
            dropdownYear.value = year - minYear;
        }
    }


    public int GetDay()
    {
        return ((dropdownDay.value) + 1);
    }

    public int GetMonth()
    {
        return ((dropdownMonth.value) + 1);
    }

    public int GetYear()
    {
        return ((dropdownYear.value) + minYear);
    }

    public string GetDateString()
    {
        return ((dropdownDay.value)+1).ToString() + "/" + ((dropdownMonth.value)+1).ToString() + "/" + ((dropdownYear.value)+minYear).ToString();
    }

    public void AceptarFecha()
    {
        inputFecha.AceptarInputFecha((dropdownYear.value) + minYear, (dropdownMonth.value) + 1, (dropdownDay.value) + 1);
        Destroy(this.gameObject);
    }

    public void CancelarFecha()
    {
        Destroy(this.gameObject);
    }

}
