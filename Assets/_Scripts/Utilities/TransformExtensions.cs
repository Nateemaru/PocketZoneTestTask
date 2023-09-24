using UnityEngine;

namespace _Scripts.Utilities
{
    public static class TransformExtensions
    {
        public static void LookAtOnlyY(this Transform origin, Transform lookAtTarget, float offsetY = 0)
        {
            var p = lookAtTarget.position;
            p.y = origin.position.y;
            origin.LookAt(p);
            origin.eulerAngles += new Vector3(0, offsetY);
        }

        public static void LookAtOnlyY(this Transform origin, Vector3 lookAtTarget, float offsetY = 0)
        {
            var p = lookAtTarget;
            p.y = origin.position.y;
            origin.LookAt(p);
            origin.eulerAngles += new Vector3(0, offsetY);
        }

        public static void LookAtOnlyYSmooth(this Transform origin, Transform lookAtTarget, float smooth,
            float offsetY = 0)
        {
            var dir = lookAtTarget.position - origin.position;
            dir.y = offsetY;

            Quaternion rotation = Quaternion.LookRotation(dir, Vector3.up);
            origin.rotation = Quaternion.Lerp(origin.rotation, rotation, smooth * Time.deltaTime);
        }

        public static void LookAtOnlyYSmooth(this Transform origin, Vector3 lookAtTarget, float smooth,
            float offsetY = 0)
        {
            var dir = lookAtTarget - origin.position;
            dir.y = offsetY;

            Quaternion rotation = Quaternion.LookRotation(dir, Vector3.up);
            origin.rotation = Quaternion.Lerp(origin.rotation, rotation, smooth * Time.deltaTime);
        }

        public static bool IsTargetInSight(this Transform origin, Transform target, float fovAngle = 90)
        {
            var dir = target.position - origin.position;

            if (Vector2.Angle(origin.forward, dir) < fovAngle / 2)
                return true;

            return false;
        }

        public static bool IsTargetNearby(this Transform origin, Transform target, float distance)
        {
            if (Vector2.Distance(origin.position, target.position) <= distance)
                return true;

            return false;
        }
    }
}