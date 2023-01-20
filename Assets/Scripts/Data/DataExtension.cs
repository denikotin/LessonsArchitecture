using UnityEngine;

namespace Assets.Scripts.Data
{
    public static class DataExtension
    {
        public static NewVector3 AsVectorData(this Vector3 vector) => new NewVector3(vector.x, vector.y, vector.z);

        public static Vector3 AsUnityVector(this NewVector3 vector) => new Vector3(vector.X, vector.Y, vector.Z);

        public static T ToDeserialized<T>(this string json) => JsonUtility.FromJson<T>(json);

        public static string ToJson(this object obj) => JsonUtility.ToJson(obj);

        public static Vector3 AddY(this Vector3 vector, float y)
        {
            vector.y += y;
            return vector;
        }
    }
}