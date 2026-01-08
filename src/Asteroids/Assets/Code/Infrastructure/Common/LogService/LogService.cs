using System.Text.RegularExpressions;
using UnityEngine;

namespace Code.Infrastructure.Common.LogService
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

        public void LogWithBoldWords(string message, params string[] boldWords)
        {
            string result = message;

            foreach (string word in boldWords)
                result = result.Replace(word, $"<b>{word}</b>");

            Debug.Log(result);
        }

        public void LogWithBracketsHighlight(string message, Color color)
        {
            string hexColor = ColorUtility.ToHtmlStringRGB(color);

            string pattern = @"\[([^\]]+)\]";

            string result = Regex.Replace(message, pattern,
                match => $"<color=#{hexColor}><b>{match.Groups[1].Value}</b></color>");

            Debug.Log(result);
        }

        public void LogInfo(string message) =>
            Log(message, Color.white);

        public void LogWarning(string message) =>
            Log(message, Color.yellow);

        public void LogError(string message) =>
            Log(message, Color.red);

        public void LogSuccess(string message) =>
            Log(message, Color.green);
    }
}