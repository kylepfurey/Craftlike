using UnityEngine;

namespace DN
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        void Awake() => Instance = this;
        void OnDestroy() { if (Instance == this) Instance = null; }
    }
}
