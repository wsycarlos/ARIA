using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace vietlabs {

    public static class vlbArray {
        
        public static string Join(this IList list, string separator = ",", string prefix = null, string suffix = null) {
            var builder = new StringBuilder();
            var hasSep  = !string.IsNullOrEmpty(separator);

            if (prefix != null) builder.Append(prefix);
            for (var i = 0; i < list.Count; i++) {
                if (i > 0 && hasSep) builder.Append(separator);
                builder.Append(list[i]);
            }
            return builder.ToString();
        }
        public static T[] ToArray<T>(IList list) {
            var result = new T[list.Count];
            for (var i = 0; i < list.Count; i++) {
                result[i] = (T)list[i];
            }
            return result;
        }
        public static int IndexOf(this IList list, object item, int stIndex = 0) {
            for (var i = stIndex; i < list.Count; i++) {
                if (list[i].Equals(item)) return i;
            }
            return -1;
        }

    // Array Operations :: 
        public static T[] Add<T>(this T[] list, T item, bool checkUnique = false) {
            var tail = new[] { item };
            var result = checkUnique ? list.Union(tail) : list.Concat(tail);
            return result.ToArray();
        }
        public static T[] AddRange<T>(this T[] list, IList items, bool checkUnique = false) {
            var arr = new List<T>();
            if (items == null || items.Count == 0) return list;
            if (list != null && list.Length > 0) arr.AddRange(list);

            for (var i = 0; i < items.Count; i++) {
                var item = (T) items[i];
                if (checkUnique && arr.Contains(item)) continue;
                arr.Add(item);
            }

            return arr.ToArray<T>();
        }
        public static T[] RemoveAt<T>(this T[] source, int index) {
            if (index < 0 || index > source.Length - 1) return source;
            var dest = new T[source.Length - 1];
            Array.Copy(source, 0, dest, 0, index);
            Array.Copy(source, index + 1, dest, index, source.Length - index - 1);
            return dest;
        }
        public static T[] Remove<T>(this T[] source, T item) {
            if (!source.Contains(item)) return source;
            var list = source.ToList();
            list.Remove(item);
            return list.ToArray();
        }
        public static T[] Compact<T>(this T[] list) {
            return list.ToList().FindAll(item => !item.Equals(default(T))).ToArray();
        }
        public static T[] DuplicateToArray<T>(this T item, int nItems) {
            var result = new T[nItems];
            for (var i = 0; i < nItems; i++) {
                result[i] = item;
            }
            return result;
        }
    }    
}

