using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private SliderJoint2D _slider;
    private bool _isForward = true;
    private float _speed;
    private Vector3 _point1;
    private Vector3 _point2;

    private void Awake()
    {
        _slider = GetComponent<SliderJoint2D>();
        _speed = _slider.motor.motorSpeed;
        _point1 = _slider.transform.position; _point1.x += _slider.limits.max;
        _point2 = _slider.transform.position; _point2.x += _slider.limits.min;
    }

    private void Update()
    {
        if ((_isForward) ? (transform.position.x >= _point1.x) : (transform.position.x <= _point2.x))
        {
            _slider.motor = new JointMotor2D()
            {
                motorSpeed = (_isForward ? -_speed : _speed),
                maxMotorTorque = _slider.motor.maxMotorTorque
            };
            _isForward = !_isForward;
        } 
    }
}
