using System;
using System.IO;
using System.Security.Cryptography;

namespace EnhancedSpectator.Logging;

/// <summary>
/// Logs local plugin binary diagnostics for multi-machine test verification.
/// </summary>
public static class PluginDiagnostics
{
    /// <summary>
    /// Logs the current plugin DLL path and SHA256 hash.
    /// </summary>
    public static void LogPluginBinaryHash(string? pluginPath)
    {
        if (string.IsNullOrWhiteSpace(pluginPath))
        {
            ModLog.Debug("Plugin DLL path unavailable; cannot compute SHA256.");
            return;
        }

        try
        {
            if (!File.Exists(pluginPath))
            {
                ModLog.Debug($"Plugin DLL path does not exist; cannot compute SHA256: {pluginPath}");
                return;
            }

            using FileStream stream = File.OpenRead(pluginPath);
            using SHA256 sha256 = SHA256.Create();
            byte[] hashBytes = sha256.ComputeHash(stream);
            ModLog.Debug($"Plugin binary SHA256: {ToHex(hashBytes)}; path={pluginPath}");
        }
        catch (Exception ex)
        {
            ModLog.Debug($"Plugin DLL SHA256 calculation failed: {ex.GetType().Name}.");
        }
    }

    private static string ToHex(byte[] bytes)
    {
        char[] chars = new char[bytes.Length * 2];
        for (int index = 0; index < bytes.Length; index++)
        {
            byte value = bytes[index];
            chars[index * 2] = GetHexValue(value >> 4);
            chars[(index * 2) + 1] = GetHexValue(value & 0x0F);
        }

        return new string(chars);
    }

    private static char GetHexValue(int value)
    {
        return (char)(value < 10 ? value + '0' : value - 10 + 'A');
    }
}
