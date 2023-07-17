using UnityEngine;

public static class Base64Convertor
{
    public static string ReturnInBase64(string value)
    {
        try
        {
            var clientBytes = System.Text.Encoding.UTF8.GetBytes(value);
            return System.Convert.ToBase64String(clientBytes);
        }
        catch
        {
            CustomDebug.LogFileWarning($"{value} don't convert to base64.");
            return "";
        }
    }

    public static string ReturnFromBase64(string value)
    {
        try
        {
            byte[] buffer = System.Convert.FromBase64String(value);
            return System.Text.Encoding.ASCII.GetString(buffer);
        }
        catch
        {
            CustomDebug.LogFileWarning($"{value} don't convert from base64.");
            return "";
        }
    }
}
