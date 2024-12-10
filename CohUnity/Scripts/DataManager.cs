using UnityEngine;

namespace MyGameNamespace
{
    public class DataManager : MonoBehaviour
    {
        private static string userId = null;
        private static int currentQaust = 1; 
        private static int currentLevel = 0; 
        private static bool Checkpoint = true; 
        private static bool statusRe = true; 
        private static int Box = 2; 
        private static int indexBox = 0;
        private static float positionX = -6.19f;
        private static float positionY = -0.37f;

        public static string UserID
        {
            get { return userId; }
            set { userId = value; }
        }

        public static int Qaust
        {
            get { return currentQaust; }
            set { currentQaust = value; }
        }

        public static int Level
        {
            get { return currentLevel; }
            set { currentLevel = value; }
        }
        public static bool checkpoint
        {
            get { return Checkpoint; }
            set { Checkpoint = value; }
        }

        public static bool StatusReset
        {
            get { return statusRe; }
            set { statusRe = value; }
        }

        public static int BoxNum
        {
            get { return Box; }
            set { Box = value; }
        }

        public static int IndexBox
        {
            get { return indexBox; }
            set { indexBox = value; }
        }

        public static float X
        {
            get { return positionX; }
            set { positionX = value; }
        }

        public static float Y
        {
            get { return positionY; }
            set { positionY = value; }
        }

        public static void ResetPosition(){
            positionX = -6.19f;
            positionY = -0.37f;
        }
    }
}
