using System.Runtime.InteropServices;

namespace LibblurayConnector;

[StructLayout(LayoutKind.Sequential)]
public struct BdEvent
{
    public uint Event;
    public uint Param;
}