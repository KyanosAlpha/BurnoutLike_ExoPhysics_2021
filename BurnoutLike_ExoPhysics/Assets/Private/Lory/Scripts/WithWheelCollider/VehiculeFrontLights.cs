using System.Collections.Generic;
using UnityEngine;

public class VehiculeFrontLights : MonoBehaviour
{
    #region properties
    [SerializeField]
    private GameObject[] _lights;
    private bool _isLightOn;
    #endregion properties



    #region fields

    #endregion fields



    #region public methods

    #endregion public methods



    #region unity messages
    private void Awake()
    {
        var lightComponents = GetComponentsInChildren<Light>();
        var lightGameObjets = new List<GameObject>();

        for (int i = 0; i < lightComponents.Length; i++)
        {
            lightGameObjets.Add(lightComponents[i].gameObject);
            lightGameObjets[i].SetActive(false);
        }

        _lights = lightGameObjets.ToArray();
    }

    private void Start()
    {

    }

    private void Update()
    {
        SetActiveLight();
    }
    #endregion unity messages



    #region private methods
    private void SetActiveLight()
    {
        if (Input.GetKeyDown(KeyCode.L) && !_isLightOn)
        {
            for (int i = 0; i < _lights.Length; i++)
            {
                _lights[i].SetActive(true);
            }
            _isLightOn = true;
        }

        else if (Input.GetKeyDown(KeyCode.L) && _isLightOn)
        {
            for (int i = 0; i < _lights.Length; i++)
            {
                _lights[i].SetActive(false);
            }
            _isLightOn = false;
        }
    }
    #endregion private methods
}