using UnityEngine;

namespace MyGameNamespace
{
    public class DataManager : MonoBehaviour
    {
        private static string userId = null;

        public static string UserID
        {
            get { return userId; }
            set { userId = value; }
        }
    }
}
