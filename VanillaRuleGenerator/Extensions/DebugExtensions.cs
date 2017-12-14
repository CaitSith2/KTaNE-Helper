using System;

namespace VanillaRuleGenerator.Extensions
{
    //Faking the Debug calls to make direct copy/paste code much easier.
    public static class Debug
    {
        //public static string log = string.Empty;

        public static void Log(string message)
        {
            //log += message + Environment.NewLine;
        }

        public static void Log(string message, Object context)
        {
            //log += message + Environment.NewLine;
        }

        public static void LogError(object message){}
        public static void LogError(object message, Object context){}

        public static void LogErrorFormat(string format, params object[] args){}
        public static void LogErrorFormat(Object context, string format, params object[] args){}

        public static void LogException(Exception exception){}
        public static void LogException(Exception exception, Object context){}

        public static void LogFormat(string format, params object[] args)
        {
            //log += string.Format(format, args) + Environment.NewLine;
        }
        public static void LogFormat(Object context, string format, params object[] args){}

        public static void LogWarning(object message){}
        public static void LogWarning(object message, Object context){}

        public static void LogWarningFormat(string format, params object[] args){}
        public static void LogWarningFormat(Object context, string format, params object[] args){}

        public static void LogAssertionFormat(string format, params object[] args){}
        public static void LogAssertionFormat(Object context, string format, params object[] args){}

        public static void LogAssertion(object message){}
        public static void LogAssertion(object message, Object context){}
    }
}