using System.Runtime.InteropServices;

namespace LibblurayConnector;

[StructLayout(LayoutKind.Sequential)]
public struct NativeDiscInfo
{
    public byte BluRayDetected;

    public IntPtr DiscName;
    public IntPtr UdfVolumeId;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20, ArraySubType = UnmanagedType.U1)]
    public byte[] DiscId;

    public byte NoMenuSupport;
    public byte FirstPlaySupported;
    public byte TopMenuSupported;

    public uint NumTitles;
    public IntPtr Titles;
    public IntPtr FirstPlay;
    public IntPtr TopMenu;

    public uint NumHdmvTitles;
    public uint NumBdjTitles;
    public uint NumUnsupportedTitles;

    public byte BdjDetected;
    public byte BdjSupported;
    public byte LibJvmDetected;
    public byte BdjHandled;
}