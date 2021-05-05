using System.Collections.Generic;
using UnityEngine;

public class VehiculeNitro : MonoBehaviour
{
    #region properties
    #endregion properties



    #region fields
    [SerializeField]
    private TrailRenderer[] _nitroGraphics;
    #endregion fields



    #region public methods
    #endregion public methods



    #region unity messages
    private void Awake()
    {
        var trailComponents = GetComponentsInChildren<TrailRenderer>();
        var trailGameObjets = new List<GameObject>();

        for (int i = 0; i < trailComponents.Length; i++)
        {
            trailGameObjets.Add(trailComponents[i].gameObject);
            trailGameObjets[i].SetActive(false);
        }

        //_nitroGraphics = trailGameObjets.ToArray();
    }

    private void Start()
    {

    }

    private void Update()
    {

    }
    #endregion unity messages



    #region private methods
    #endregion private methods
}