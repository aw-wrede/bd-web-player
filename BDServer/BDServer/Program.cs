using System.Diagnostics;
using System.Runtime.InteropServices;
using Commons.Logging;
using LibblurayConnector;

// Check for bd arg
if (args.Length < 1 || string.IsNullOrEmpty(args[0]))
{
    Logger.Error("Missing required argument bd path!");
    return 2;
}

var discPath = args[0];

if (!Directory.Exists(discPath))
{
    Logger.Error($"Directory {discPath} not found!");
    return 1;
}

Logger.Info($"Using disc path {discPath}");

var libraryPath = "C:\\libbluray\\bin\\libbluray-4.dll";

try
{
    NativeMethods.Configure(libraryPath);
    
    using var disc = BluRayDisc.Open(args[0]);
    var info = disc.Info;
    
    var name = info.DiscName ?? info.VolumeId ?? "(ohne Namen)";
    Logger.Info($"Disc: {name}");
    Logger.Info($"First Play unterstützt: {info.FirstPlaySupported}");
    Logger.Info($"Top-Menü unterstützt: {info.TopMenuSupported}");
    Logger.Info($"Titel im Disc-Index: {info.TitleCount}");
    Logger.Info(
        $"HDMV: {info.HdmvTitleCount}, BD-J: {info.BdjTitleCount}, " +
        $"nicht unterstützt: {info.UnsupportedTitleCount}");
    Logger.Info(
        $"BD-J erkannt: {info.BdjDetected}, JVM erkannt: {info.JvmDetected}, " +
        $"BD-J verfügbar: {info.BdjAvailable}");

    return 0;
}
catch (Exception e)
{
    Logger.Exception(e);
    return 1;
}
