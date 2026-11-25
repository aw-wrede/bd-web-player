using System.Runtime.InteropServices;

namespace LibblurayConnector;

public sealed class DiscHandle : SafeHandle
{
    public override bool IsInvalid => handle == IntPtr.Zero;
    
    private DiscHandle() : base(IntPtr.Zero, ownsHandle: true) { }
    
    protected override bool ReleaseHandle()
    {
        NativeMethods.bd_close(handle);
        return true;
    }
    
    
    public static DiscHandle Open(string path)
    {
        var result = new DiscHandle();
        try
        {
            result.SetHandle(NativeMethods.bd_open(path, IntPtr.Zero));
            return result.IsInvalid ? throw new IOException($"libbluray could not open disc: {path}") : result;
        }
        catch
        {
            result.Dispose();
            throw;
        }
    }
    
    
    
}