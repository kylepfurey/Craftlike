using UnityEngine;

namespace DN
{
    public class Entity : MonoBehaviour
    {
        [SerializeField] string id;

        public virtual string Type => "Entities";
        public string Id => id;

        protected virtual void Awake() => LoadProperties(GameManager.Instance.Spreadsheets[Type][Id]);
        protected virtual void LoadProperties(Spreadsheet.Row properties) { }
    }
}
