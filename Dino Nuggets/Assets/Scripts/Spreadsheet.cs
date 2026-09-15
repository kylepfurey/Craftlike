#nullable enable
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace DN
{
    [CreateAssetMenu(fileName = "Spreadsheet", menuName = "Scriptable Objects/Spreadsheet")]
    public class Spreadsheet : ScriptableObject
    {
        [Serializable]
        public struct Row
        {
            [SerializeField] Dictionary<string, string> columns;

            public Row(Dictionary<string, string> columns) => this.columns = columns;

            delegate bool Parser<T>(string s, out T result);
            T? Get<T>(string column, Parser<T> parser)
            {
                if (!parser(columns[column], out T result))
                    return default(T);
                return result;
            }

            public bool? Bool(string column) => Get<bool>(column, bool.TryParse);
            public int? Int(string column) => Get<int>(column, int.TryParse);
            public float? Float(string column) => Get<float>(column, float.TryParse);
            public string String(string column)
            {
                if (!columns.TryGetValue(column, out string result))
                    return string.Empty;
                result = result.Trim();
                return result;
            }
            public string[] Array(string column)
            {
                if (!columns.TryGetValue(column, out string str))
                    return new string[0];
                string[] result = str.Split(',');
                for (int i = 0; i < result.Length; ++i)
                    result[i] = result[i].Trim();
                return result;
            }
        }

        [SerializeField] Dictionary<string, int> idToRow;
        [SerializeField] Row[] rows;

        public Row this[int row] { get => rows[row]; }
        public Row this[string id] { get => rows[idToRow[id.Trim().ToLower()]]; }

#if UNITY_EDITOR
        public bool Deserialize(string json)
        {
            var rows = JsonConvert.DeserializeObject<Dictionary<string, string>[]>(json);
            if (rows == null)
                return false;
            this.rows = new Row[rows.Length];
            for (int i = 0; i < rows.Length; ++i)
            {
                idToRow[rows[i]["id"].Trim().ToLower()] = i;
                this.rows[i] = new Row(rows[i]);
            }
            EditorUtility.SetDirty(this);
            return true;
        }
#endif
    }
}
