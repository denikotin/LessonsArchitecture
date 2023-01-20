using System;

namespace Assets.Scripts.Data
{
    [Serializable]
    public class PositionOnLevel
    {
        public string Level;
        public NewVector3 Position;

        public PositionOnLevel(string level, NewVector3 position)
        {
            Level = level;
            Position = position;
        }
        
        public PositionOnLevel(string level)
        {
            Level = level;
        }
    }
}