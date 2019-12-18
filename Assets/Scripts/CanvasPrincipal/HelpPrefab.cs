using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpPrefab : MonoBehaviour {

    [SerializeField] private GameObject helpText = null;

    private void Awake()
    {
        helpText.SetActive(false);
    }

    public void OnPress()
    {
        helpText.SetActive(true);
    }

    public void OnRelease()
    {
        helpText.SetActive(false);
    }
}
