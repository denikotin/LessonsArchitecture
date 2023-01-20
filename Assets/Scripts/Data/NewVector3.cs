using System;

namespace Assets.Scripts.Data
{
    [Serializable]
    public class NewVector3
    {
        public float X;
        public float Y;
        public float Z;

        public NewVector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
}
