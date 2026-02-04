using System;
using System.Text.RegularExpressions;
using UnityEngine;

namespace _Project.Code.Infrastructure.Common.LogService
{
    public class LogService : ILogService
    {
        public void Log(string message, bool bold = false) => 
            Debug.Log(bold ? $"<b>{message}</b>" : message);

        public void Log(string message, Color color, bool bold = false)
        {
            string hexColor = ColorUtility.ToHtmlStringRGB(color);
            Log($"<color=#{hexColor}>{message}</color>", bold);
        }
        
        public void LogWithBoldWords(string message)
        {
            string pattern = @"<([^>]+)>";
    
            string result = Regex.Replace(message, pattern, 
                match => $"<b>{match.Groups[1].Value}</b>");

            Debug.Log(result);
        }

        public void LogWithHighlight(string message, Color color)
        {
            string hexColor = ColorUtility.ToHtmlStringRGB(color);
    
            string pattern = @"\[(!?)([^\]]+)\]";
    
            string result = Regex.Replace(message, pattern, 
                match =>
                {
                    string word = match.Groups[2].Value;
                    bool isBold = string.IsNullOrEmpty(match.Groups[1].Value);

                    return isBold ? $"<color=#{hexColor}><b>{word}</b></color>" : $"<color=#{hexColor}>{word}</color>";
                });
    
            Debug.Log(result);
        }

        public void LogInfo(string message) =>
            Log(message, Color.white);

        public void LogWarning(string message) =>
            Log(message, Color.yellow);

        public void LogError(string message) =>
            Log(message, Color.red);

        public void LogException(Exception exception) => 
            Debug.LogException(exception);
    }
}