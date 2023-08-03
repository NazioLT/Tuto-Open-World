using UnityEngine;

[System.Serializable]
public class RangeAngleDetection
{
    public RangeAngleDetection(float range, float angle)
    {
        _range = range;
        _angle = angle;
    }

    [SerializeField] private float _range = 5f;
    [SerializeField] private float _angle = 90f;

    public bool IsAngleRangeDetected(Vector3 origin, Vector3 target, Vector3 originForward)
    {
        Vector3 delta = target - origin;
        float distance = delta.magnitude;

        if(distance > _range)
            return false;

        float targetAngle = Vector2.Angle(new Vector2(delta.x, delta.z), new Vector2(originForward.x, originForward.z));
        float trueAngle = _angle / 2f;

        bool isInVisionRange = targetAngle < trueAngle;

        return isInVisionRange;
    }
}