using System;
using UnityEngine;

namespace Explorer._Scripts.Explorer.Systems.CombatSystem
{
    [Serializable]
    public struct GameplayTag : IEquatable<GameplayTag>
    {
        [SerializeField] private string path; // e.g., "Status.Burning"
        public string Path => path ?? string.Empty;
        public GameplayTag(string p) { path = p?.Trim() ?? string.Empty; }
        public bool Equals(GameplayTag other) => string.Equals(Path, other.Path, StringComparison.OrdinalIgnoreCase);
        public override bool Equals(object obj) => obj is GameplayTag gt && Equals(gt);
        public override int GetHashCode() => StringComparer.OrdinalIgnoreCase.GetHashCode(Path);
        public override string ToString() => Path;
        public bool Matches(GameplayTag other)
        {
            if (string.IsNullOrEmpty(other.Path) || string.IsNullOrEmpty(Path)) return false;
// Parent/child matching by dot‑segments
            var mine = Path.Split('.');
            var theirs = other.Path.Split('.');
            if (theirs.Length > mine.Length) return false;
            for (int i = 0; i < theirs.Length; i++)
                if (!string.Equals(mine[i], theirs[i], StringComparison.OrdinalIgnoreCase))
                    return false;
            return true;
        }
    }
}