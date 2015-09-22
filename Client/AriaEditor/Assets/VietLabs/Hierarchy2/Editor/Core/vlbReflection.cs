using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace vietlabs
{
    public static class vlbReflection {
        private const BindingFlags AllFlags =   
                BindingFlags.Default
            //| BindingFlags.ExactBinding
              | BindingFlags.FlattenHierarchy
            //| BindingFlags.DeclaredOnly
            //| BindingFlags.CreateInstance
            //| BindingFlags.GetField
            //| BindingFlags.GetProperty
            //| BindingFlags.IgnoreCase
            //| BindingFlags.IgnoreReturn
            //| BindingFlags.SuppressChangeType
            //| BindingFlags.InvokeMethod
              | BindingFlags.NonPublic
              | BindingFlags.Public
            //| BindingFlags.OptionalParamBinding
            //| BindingFlags.PutDispProperty
            //| BindingFlags.PutRefDispProperty
            //| BindingFlags.SetField
            //| BindingFlags.SetProperty
              | BindingFlags.Instance
              | BindingFlags.Static;

        public static bool HasMethod(this object obj, string methodName, Type type = null, BindingFlags? flags = null) {
            if (obj == null || string.IsNullOrEmpty(methodName)) return false;
            if (type == null) type = obj is Type ? (Type)obj : obj.GetType();
            return type.GetMethod(methodName, flags ?? AllFlags) != null;
        }
        public static object Invoke(this object obj, string methodName, Type type = null, BindingFlags? flags = null, params object[] parameters) {
            if (obj == null || string.IsNullOrEmpty(methodName)) return null;

            if (type == null) type = (obj is Type) ? (Type)obj : obj.GetType();
            var f       = type.GetMethod(methodName, flags ?? AllFlags);
            if (f != null) return f.Invoke(obj, parameters);
            Debug.LogWarning(string.Format("Invoke Error : <{0}> is not a method of type <{1}>", methodName, type));
            return null;
        }

        public static T ChangeType<T>(this object p_value) {
            return (T)Convert.ChangeType(p_value, typeof(T));
        }

        public static bool HasField(this object obj, string name, Type type = null, BindingFlags? flags = null)
	    {
		    if (obj == null || string.IsNullOrEmpty(name)) return false;
            if (type == null) type = (obj is Type) ? (Type)obj : obj.GetType();
		    return type.GetField(name, flags ?? AllFlags) != null;
	    }
        public static object GetField(this object obj, string name, Type type = null, BindingFlags? flags = null)
	    {
		    if (obj == null || string.IsNullOrEmpty(name)) return false;

            if (type == null) type = (obj is Type) ? (Type)obj : obj.GetType();
		    var field = type.GetField(name, flags ?? AllFlags);
		    if (field == null) {
			    Debug.LogWarning(string.Format(
				    "GetField Error : <{0}> does not contains a field with name <{1}>",
				    type, name
			    ));
			    return null;
		    }

		    return field.GetValue(obj);
	    }
        public static void SetField(this object obj, string name, object value, Type type = null, BindingFlags? flags = null)
	    {
		    if (obj == null || string.IsNullOrEmpty(name)) return;

            if (type == null) type = (obj is Type) ? (Type)obj : obj.GetType();
		    var field = type.GetField(name, flags ?? AllFlags);

		    if (field == null) {
			    Debug.LogWarning(string.Format(
				    "SetField Error : <{0}> does not contains a field with name <{1}>",
				    type, name
			    ));
			    return;
		    }

		    field.SetValue(obj, value);
	    }
        
        public static bool HasProperty(this object obj, string name, Type type = null, BindingFlags? flags = null)
	    {
		    if (obj == null || string.IsNullOrEmpty(name)) return false;

            if (type == null) type = (obj is Type) ? (Type)obj : obj.GetType();
		    return type.GetProperty(name, flags ?? AllFlags) != null;
	    }
        public static void SetProperty(this object obj, string name, object value, Type type = null, BindingFlags? flags = null)
	    {
		    if (obj == null || string.IsNullOrEmpty(name)) return;

            if (type == null) type = (obj is Type) ? (Type)obj : obj.GetType();
		    var property = type.GetProperty(name, flags ?? AllFlags);

		    if (property == null) {
			    Debug.LogWarning(string.Format(
				    "SetProperty Error : <{0}> does not contains a property with name <{1}>",
				    obj, name
			    ));
			    return;
		    }

		    property.SetValue(obj, value, null);
	    }
        public static object GetProperty(this object obj, string name, Type type = null, BindingFlags? flags = null)
	    {
		    if (obj == null || string.IsNullOrEmpty(name)) return null;

            if (type == null) type = (obj is Type) ? (Type)obj : obj.GetType();
		    var property = type.GetProperty(name, flags ?? AllFlags);
		    if (property != null) return property.GetValue(obj, null);

		    Debug.LogWarning(string.Format(
			    "GetProperty Error : <{0}> does not contains a property with name <{1}>",
			    type, name
		    ));
		    return null;
	    }
	    
        private static Dictionary<string, Type> TypeDict;
        public static Type GetTypeByName(this string className, string classPackage) {
            if (TypeDict == null) TypeDict = new Dictionary<string, Type>();
            var hasCache = TypeDict.ContainsKey(className);
            var def = hasCache ? TypeDict[className] : null;

            if (hasCache) {
                if (def != null) return def;
                TypeDict.Remove(className);
            }

            def = Types.GetType(className, classPackage);
            if (def != null) {
                TypeDict.Add(className, def);
            } else {
                Debug.LogWarning(string.Format("Type <{0}> not found in package <{1}>", className, classPackage));
            }

            return def;
        }
    }
}
