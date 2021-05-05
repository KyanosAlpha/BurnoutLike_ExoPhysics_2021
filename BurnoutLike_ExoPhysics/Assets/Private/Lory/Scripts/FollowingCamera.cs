using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
    #region properties
    #endregion properties



    #region fields
    [SerializeField]
    private GameObject _player;
    [SerializeField]
    private GameObject _cameraConstraint;
    [SerializeField]
    private Transform _transformCamera;
    [SerializeField]
    private float _speed;
    #endregion fields



    #region public methods
    #endregion public methods



    #region unity messages
    private void Awake()
    {
        _transformCamera = transform;
    }

    private void Start()
    {

    }

    private void Update()
    {
        Following();
    }

    private void LateUpdate()
    {
        
    }
    #endregion unity messages



    #region private methods
    private void Following()
    {
        _transformCamera.position = Vector3.Lerp(_transformCamera.position, _cameraConstraint.transform.position, Time.deltaTime * _speed);
        _transformCamera.LookAt(_player.transform.position);
    }
    #endregion private methods
}