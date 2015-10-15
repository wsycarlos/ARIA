/*
 * Advanced C# messenger by Ilya Suzdalnitski. V1.0
 * 
 * Based on Rod Hyde's "CSharpMessenger" and Magnus Wolffelt's "CSharpMessenger Extended".
 * 
 * Features:
    * Prevents a MissingReferenceException because of a reference to a destroyed message handler.
    * Option to log all messages
    * Extensive error detection, preventing silent bugs
 * 
 * Usage examples:
    1. Messenger.AddListener<GameObject>("prop collected", PropCollected);
       Messenger.Broadcast<GameObject>("prop collected", prop);
    2. Messenger.AddListener<float>("speed changed", SpeedChanged);
       Messenger.Broadcast<float>("speed changed", 0.5f);
 * 
 * Messenger cleans up its evenTable automatically upon loading of a new level.
 * 
 * Don't forget that the messages that should survive the cleanup, should be marked with Messenger.MarkAsPermanent(string)
 * 
 */

//#define LOG_ALL_MESSAGES
//#define LOG_ADD_LISTENER
//#define LOG_BROADCAST_MESSAGE
//#define REQUIRE_LISTENER
using System;
using System.Collections.Generic;
using UnityEngine;

public static class Messenger
{
    #region Internal variables

    //Disable the unused variable warning
#pragma warning disable 0414
    //Ensures that the MessengerHelper will be created automatically upon start of the game.
    private static MessengerHelper messengerHelper = (new GameObject("MessengerHelper")).AddComponent<MessengerHelper>();
#pragma warning restore 0414

    public static Dictionary<string, Delegate> eventTable = new Dictionary<string, Delegate>();

    //Message handlers that should never be removed, regardless of calling Cleanup
    public static List<string> permanentMessages = new List<string>();
    #endregion
    #region Helper methods
    //Marks a certain message as permanent.
    public static void MarkAsPermanent(string eventType)
    {
#if LOG_ALL_MESSAGES
        D.log("Messenger MarkAsPermanent \t\"" + eventType + "\"");
#endif

        permanentMessages.Add(eventType);
    }

    public static void Cleanup()
    {
#if LOG_ALL_MESSAGES
        D.log("MESSENGER Cleanup. Make sure that none of necessary listeners are removed.");
#endif

        List<string> messagesToRemove = new List<string>();

        foreach (KeyValuePair<string, Delegate> pair in eventTable)
        {
            bool wasFound = false;

            foreach (string message in permanentMessages)
            {
                if (pair.Key == message)
                {
                    wasFound = true;
                    break;
                }
            }

            if (!wasFound)
                messagesToRemove.Add(pair.Key);
        }

        foreach (string message in messagesToRemove)
        {
            eventTable.Remove(message);
        }
    }

    public static void PrintEventTable()
    {
        D.log("\t\t\t=== MESSENGER PrintEventTable ===");

        foreach (KeyValuePair<string, Delegate> pair in eventTable)
        {
            D.log("\t\t\t" + pair.Key + "\t\t" + pair.Value);
        }

        D.log("\n");
    }
    #endregion

    #region Message logging and exception throwing
    public static void OnListenerAdding(string eventType, Delegate listenerBeingAdded)
    {
#if LOG_ALL_MESSAGES || LOG_ADD_LISTENER
        D.log("MESSENGER OnListenerAdding \t\"" + eventType + "\"\t{" + listenerBeingAdded.Target + " -> " + listenerBeingAdded.Method + "}");
#endif

        if (!eventTable.ContainsKey(eventType))
        {
            eventTable.Add(eventType, null);
        }

        Delegate d = eventTable[eventType];
        if (d != null && d.GetType() != listenerBeingAdded.GetType())
        {
            throw new ListenerException(string.Format("Attempting to add listener with inconsistent signature for event type {0}. Current listeners have type {1} and listener being added has type {2}", eventType, d.GetType().Name, listenerBeingAdded.GetType().Name));
        }
    }

    public static void OnListenerRemoving(string eventType, Delegate listenerBeingRemoved)
    {
#if LOG_ALL_MESSAGES
        D.log("MESSENGER OnListenerRemoving \t\"" + eventType + "\"\t{" + listenerBeingRemoved.Target + " -> " + listenerBeingRemoved.Method + "}");
#endif

        if (eventTable.ContainsKey(eventType))
        {
            Delegate d = eventTable[eventType];

            if (d == null)
            {
                throw new ListenerException(string.Format("Attempting to remove listener with for event type \"{0}\" but current listener is null.", eventType));
            }
            else if (d.GetType() != listenerBeingRemoved.GetType())
            {
                throw new ListenerException(string.Format("Attempting to remove listener with inconsistent signature for event type {0}. Current listeners have type {1} and listener being removed has type {2}", eventType, d.GetType().Name, listenerBeingRemoved.GetType().Name));
            }
        }
        else
        {
#if DEBUG
            throw new ListenerException(string.Format("Attempting to remove listener for type \"{0}\" but Messenger doesn't know about this event type.", eventType));
#endif
        }
    }

    public static void OnListenerRemoved(string eventType)
    {
        if (eventTable[eventType] == null)
        {
            eventTable.Remove(eventType);
        }
    }

    public static void OnBroadcasting(string eventType)
    {
#if REQUIRE_LISTENER
        if (!eventTable.ContainsKey (eventType)) {
            throw new BroadcastException (string.Format ("Broadcasting message \"{0}\" but no listener found. Try marking the message with Messenger.MarkAsPermanent.", eventType));
        }
#endif
    }

    public static BroadcastException CreateBroadcastSignatureException(string eventType)
    {
        return new BroadcastException(string.Format("Broadcasting message \"{0}\" but listeners have a different signature than the broadcaster.", eventType));
    }

    public class BroadcastException : Exception
    {
        public BroadcastException(string msg)
            : base(msg)
        {
        }
    }

    public class ListenerException : Exception
    {
        public ListenerException(string msg)
            : base(msg)
        {
        }
    }
    #endregion

    #region AddListener
    //No parameters
    public static void AddListener(string eventType, Action handler)
    {
        OnListenerAdding(eventType, handler);
        eventTable[eventType] = (Action)eventTable[eventType] + handler;
    }

    //Single parameter
    public static void AddListener<T>(string eventType, Action<T> handler)
    {
        OnListenerAdding(eventType, handler);
        eventTable[eventType] = (Action<T>)eventTable[eventType] + handler;
    }

    //Two parameters
    public static void AddListener<T, U>(string eventType, Action<T, U> handler)
    {
        OnListenerAdding(eventType, handler);
        eventTable[eventType] = (Action<T, U>)eventTable[eventType] + handler;
    }

    //Three parameters
    public static void AddListener<T, U, V>(string eventType, Action<T, U, V> handler)
    {
        OnListenerAdding(eventType, handler);
        eventTable[eventType] = (Action<T, U, V>)eventTable[eventType] + handler;
    }
    #endregion

    #region RemoveListener
    //No parameters
    public static void RemoveListener(string eventType, Action handler)
    {
        if (eventTable.ContainsKey(eventType))
        {
            OnListenerRemoving(eventType, handler);
            eventTable[eventType] = (Action)eventTable[eventType] - handler;
            OnListenerRemoved(eventType);
        }
    }

    //Single parameter
    public static void RemoveListener<T>(string eventType, Action<T> handler)
    {
        if (eventTable.ContainsKey(eventType))
        {
            OnListenerRemoving(eventType, handler);
            eventTable[eventType] = (Action<T>)eventTable[eventType] - handler;
            OnListenerRemoved(eventType);
        }
    }

    //Two parameters
    public static void RemoveListener<T, U>(string eventType, Action<T, U> handler)
    {
        if (eventTable.ContainsKey(eventType))
        {
            OnListenerRemoving(eventType, handler);
            eventTable[eventType] = (Action<T, U>)eventTable[eventType] - handler;
            OnListenerRemoved(eventType);
        }
    }

    //Three parameters
    public static void RemoveListener<T, U, V>(string eventType, Action<T, U, V> handler)
    {
        if (eventTable.ContainsKey(eventType))
        {
            OnListenerRemoving(eventType, handler);
            eventTable[eventType] = (Action<T, U, V>)eventTable[eventType] - handler;
            OnListenerRemoved(eventType);
        }
    }

    #endregion

    #region Broadcast
    //No parameters
    public static void Broadcast(string eventType)
    {
#if LOG_ALL_MESSAGES || LOG_BROADCAST_MESSAGE
        D.log("MESSENGER\t" + System.DateTime.Now.ToString("hh:mm:ss.fff") + "\t\t\tInvoking \t\"" + eventType + "\"");
#endif
        OnBroadcasting(eventType);

        Delegate d;
        if (eventTable.TryGetValue(eventType, out d))
        {
            Action Action = d as Action;

            if (Action != null)
            {
                Action();
            }
            else
            {
                throw CreateBroadcastSignatureException(eventType);
            }
        }
    }

    //Single parameter
    public static void Broadcast<T>(string eventType, T arg1)
    {
#if LOG_ALL_MESSAGES || LOG_BROADCAST_MESSAGE
        D.log("MESSENGER\t" + System.DateTime.Now.ToString("hh:mm:ss.fff") + "\t\t\tInvoking \t\"" + eventType + "\"");
#endif
        OnBroadcasting(eventType);

        Delegate d;
        if (eventTable.TryGetValue(eventType, out d))
        {
            Action<T> Action = d as Action<T>;

            if (Action != null)
            {
                Action(arg1);
            }
            else
            {
                throw CreateBroadcastSignatureException(eventType);
            }
        }
    }

    //Two parameters
    public static void Broadcast<T, U>(string eventType, T arg1, U arg2)
    {
#if LOG_ALL_MESSAGES || LOG_BROADCAST_MESSAGE
        D.log("MESSENGER\t" + System.DateTime.Now.ToString("hh:mm:ss.fff") + "\t\t\tInvoking \t\"" + eventType + "\"");
#endif
        OnBroadcasting(eventType);

        Delegate d;
        if (eventTable.TryGetValue(eventType, out d))
        {
            Action<T, U> Action = d as Action<T, U>;

            if (Action != null)
            {
                Action(arg1, arg2);
            }
            else
            {
                throw CreateBroadcastSignatureException(eventType);
            }
        }
    }

    //Three parameters
    public static void Broadcast<T, U, V>(string eventType, T arg1, U arg2, V arg3)
    {
#if LOG_ALL_MESSAGES || LOG_BROADCAST_MESSAGE
        D.log("MESSENGER\t" + System.DateTime.Now.ToString("hh:mm:ss.fff") + "\t\t\tInvoking \t\"" + eventType + "\"");
#endif
        OnBroadcasting(eventType);

        Delegate d;
        if (eventTable.TryGetValue(eventType, out d))
        {
            Action<T, U, V> Action = d as Action<T, U, V>;

            if (Action != null)
            {
                Action(arg1, arg2, arg3);
            }
            else
            {
                throw CreateBroadcastSignatureException(eventType);
            }
        }
    }
    #endregion
}

//This manager will ensure that the messenger's eventTable will be cleaned up upon loading of a new level.
public sealed class MessengerHelper : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    //Clean up eventTable every time a new level loads.
    public void OnDisable()
    {
        Messenger.Cleanup();
    }

    void OnLevelWasLoaded(int level)
    {
        Messenger.Cleanup();
    }
}