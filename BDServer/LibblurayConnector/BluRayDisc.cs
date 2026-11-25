using System;
using System.IO;
using System.Runtime.InteropServices;

namespace LibblurayConnector;

public class BluRayDisc : IDisposable
{
    private readonly DiscHandle _handle;
    
    public string DiscPath { get; }
    public BluRayDiscInfo Info { get; }
    
    private BluRayDisc(string discPath, DiscHandle handle, BluRayDiscInfo info)
    {
        DiscPath = discPath;
        _handle = handle;
        Info = info;
    }

    public void Dispose() => _handle.Dispose();
    
    public static BluRayDisc Open(string path)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(path);
        
        var fullPath = Path.GetFullPath(path);
        if (!Directory.Exists(fullPath))
        {
            throw new DirectoryNotFoundException($"Directory does not exist or is not accessible: {path}");
        }
        
        var handle = DiscHandle.Open(fullPath);
        try
        {
            var info = ReadInfo(handle.DangerousGetHandle(), fullPath);
            return new BluRayDisc(fullPath, handle, info);
        }
        catch
        {
            handle.Dispose();
            throw;
        }
    }


    private static BluRayDiscInfo ReadInfo(IntPtr handle, string path)
    {
        var pointer = NativeMethods.bd_get_disc_info(handle);
        if (pointer == IntPtr.Zero)
        {
            throw new IOException("libbluray could not get disc info.");
        }

        var info = Marshal.PtrToStructure<NativeDiscInfo>(pointer);
        if (info.BluRayDetected == 0)
        {
            throw new InvalidDataException($"No Blu-ray detected: {path}");
        }
        
        return new BluRayDiscInfo(
            DiscName: Marshal.PtrToStringUTF8(info.DiscName),
            VolumeId: Marshal.PtrToStringUTF8(info.UdfVolumeId),
            FirstPlaySupported: info.FirstPlaySupported != 0,
            TopMenuSupported: info.TopMenuSupported != 0,
            TitleCount: info.NumTitles,
            HdmvTitleCount: info.NumHdmvTitles,
            BdjTitleCount: info.NumBdjTitles,
            UnsupportedTitleCount: info.NumUnsupportedTitles,
            BdjDetected: info.BdjDetected != 0,
            JvmDetected: info.LibJvmDetected != 0,
            BdjAvailable: info.BdjHandled != 0
            );
    }
    
}