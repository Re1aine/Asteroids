using System;
using UnityEngine;

namespace Code.Infrastructure.Common.LogService
{
    public interface ILogService
    {
        void Log(string message, bool bold = false);
        void Log(string message, Color color, bool bold = false);
        void LogWithBoldWords(string message);
        void LogWithHighlight(string message, Color color);
        void LogInfo(string message);
        void LogWarning(string message);
        void LogError(string message);
        void LogException(Exception exception);
    }
}