using System.Collections.Generic;
using UnityEngine;

public class CarNitro : MonoBehaviour
{
    #region properties
    [SerializeField]
    private GameObject[] _nitros;
    #endregion properties



    #region fields

    #endregion fields



    #region public methods

    #endregion public methods



    #region unity messages
    private void Awake()
    {
        var nitroComponents = GetComponentsInChildren<TrailRenderer>();
        var nitroGameObjets = new List<GameObject>();

        for (int i = 0; i < nitroComponents.Length; i++)
        {
            nitroGameObjets.Add(nitroComponents[i].gameObject);
            nitroGameObjets[i].SetActive(false);
        }

        _nitros = nitroGameObjets.ToArray();
    }

    private void Start()
    {

    }

    private void Update()
    {
        SetActiveBoost();
    }
    #endregion unity messages



    #region private methods
    private void SetActiveBoost()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            for (int i = 0; i < _nitros.Length; i++)
            {
                _nitros[i].SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < _nitros.Length; i++)
            {
                _nitros[i].SetActive(false);
            }
        }
    }
    #endregion private methods
}