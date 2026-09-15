using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DN
{
    public static class Extensions
    {
        public static void ForEach(this IEnumerable collection, Action<object> action) { foreach (var value in collection) action(value); }
        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action) { foreach (var value in collection) action(value); }

        public static bool IsNearby(this Vector2 vector, Vector2 other, float maxDistance = 0.1f) => (other - vector).sqrMagnitude < maxDistance * maxDistance;
        public static bool IsNearby(this Vector3 vector, Vector3 other, float maxDistance = 0.1f) => (other - vector).sqrMagnitude < maxDistance * maxDistance;
    }
}
