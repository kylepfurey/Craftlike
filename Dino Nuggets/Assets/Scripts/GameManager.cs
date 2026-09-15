using System.Collections.Generic;
using UnityEngine;

namespace DN
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] Dictionary<string, Spreadsheet> spreadsheets;

        public static GameManager Instance { get; private set; }
        public IReadOnlyDictionary<string, Spreadsheet> Spreadsheets => spreadsheets;

        void Awake() => Instance = this;
        void OnDestroy() { if (Instance == this) Instance = null; }
    }
}
