#define DEBUG_LEVEL_LOG
#define DEBUG_LEVEL_WARN
#define DEBUG_LEVEL_ERROR


using UnityEngine;


// setting the conditional to the platform of choice will only compile the method for that platform
// alternatively, use the #defines at the top of this file
public static class D
{
    //    static D()
    //    {
    //        Application.RegisterLogCallback(logCallback);
    //    }
    //
    //
    //    public static void logCallback(string log, string stackTrace, LogType type)
    //    {
    //        // error gets a stack trace
    //        if (type == LogType.Error)
    //        {
    //            Console.WriteLine(log);
    //            Console.WriteLine(stackTrace);
    //        }
    //        else
    //        {
    //            Console.WriteLine(log);
    //        }
    //    }

    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    [System.Diagnostics.Conditional("DEBUG_LEVEL_LOG")]
    [System.Diagnostics.Conditional("DEBUG_LEVEL_WARN")]
    [System.Diagnostics.Conditional("DEBUG_LEVEL_ERROR")]
    public static void log(object format, params object[] paramList)
    {
        if (format is string && paramList != null && paramList.Length > 0)
            Debug.Log(string.Format(format as string, paramList));
        else
            Debug.Log(format);
    }

    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    [System.Diagnostics.Conditional("DEBUG_LEVEL_WARN")]
    [System.Diagnostics.Conditional("DEBUG_LEVEL_ERROR")]
    public static void warn(object format, params object[] paramList)
    {
        if (format is string && paramList != null && paramList.Length > 0)
            Debug.LogWarning(string.Format(format as string, paramList));
        else
            Debug.LogWarning(format);
    }

    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    [System.Diagnostics.Conditional("DEBUG_LEVEL_ERROR")]
    public static void error(object format, params object[] paramList)
    {
        if (format is string && paramList != null && paramList.Length > 0)
            Debug.LogError(string.Format(format as string, paramList));
        else
            Debug.LogError(format);
    }


    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    [System.Diagnostics.Conditional("DEBUG_LEVEL_LOG")]
    public static void assert(bool condition)
    {
        assert(condition, string.Empty, true);
    }


    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    [System.Diagnostics.Conditional("DEBUG_LEVEL_LOG")]
    public static void assert(bool condition, string assertString)
    {
        assert(condition, assertString, false);
    }


    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    [System.Diagnostics.Conditional("DEBUG_LEVEL_LOG")]
    public static void assert(bool condition, string assertString, bool pauseOnFail)
    {
        if (!condition)
        {
            Debug.LogError("assert failed! " + assertString);

            if (pauseOnFail)
                Debug.Break();
        }
    }

}
