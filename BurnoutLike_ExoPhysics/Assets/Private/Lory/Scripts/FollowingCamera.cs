using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
    #region properties
    #endregion properties



    #region fields
    [SerializeField]
    private GameObject _vehiculeView;
    private Vector3 _velocity;
    [SerializeField]
    private Transform[] _cameraLocation;
    [SerializeField, Range (0, 20)]
    private float _smoothTime = 5;
    private int _locationIndicator = 3;
    
    #endregion fields



    #region public methods
    #endregion public methods



    #region unity messages
    private void Awake()
    {
        _cameraLocation = _vehiculeView.GetComponentsInChildren<Transform>();
        _velocity = Vector3.zero;
    }

    private void Start()
    {

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            _locationIndicator = (_locationIndicator == 2) ? 3 : 2;
        }
    }

    private void LateUpdate()
    {
        CameraBehaviour();
    }
    #endregion unity messages



    #region private methods
    private void CameraBehaviour()
    {
        if(_locationIndicator == 2)
        {
            transform.position = _cameraLocation[2].transform.position;
        }

        var newPosition = Vector3.SmoothDamp(transform.position, _cameraLocation[_locationIndicator].transform.position, ref _velocity, _smoothTime * Time.deltaTime);
        transform.position = newPosition;
        transform.LookAt(_cameraLocation[1].transform);
    }
    #endregion private methods
}