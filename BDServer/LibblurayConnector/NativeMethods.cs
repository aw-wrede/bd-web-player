using System.Runtime.InteropServices;

namespace LibblurayConnector;

public static class NativeMethods
{
    private const string Library = "libbluray";
    
    public static void Configure(string libraryPath)
    {
        NativeLibrary.SetDllImportResolver(typeof(NativeMethods).Assembly, 
            (name, _, _) => name == Library ? NativeLibrary.Load(libraryPath) : IntPtr.Zero);
    }
    
    
    [DllImport(Library, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr bd_open(
        [MarshalAs(UnmanagedType.LPUTF8Str)] string discPath,
        IntPtr keyFilePath);

    [DllImport(Library, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr bd_get_disc_info(IntPtr disc);

    [DllImport(Library, CallingConvention = CallingConvention.Cdecl)]
    public static extern void bd_close(IntPtr disc);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void ArgbOverlayCallback(IntPtr handle, IntPtr overlay);

    [DllImport(Library, CallingConvention = CallingConvention.Cdecl)]
    public static extern void bd_register_argb_overlay_proc(
        IntPtr disc, IntPtr handle, ArgbOverlayCallback callback, IntPtr buffer);

    [DllImport(Library, CallingConvention = CallingConvention.Cdecl)]
    public static extern int bd_play(IntPtr disc);

    [DllImport(Library, CallingConvention = CallingConvention.Cdecl)]
    public static extern int bd_read_ext(
        IntPtr disc, [Out] byte[] buffer, int length, out BdEvent ev);

    [DllImport(Library, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr bd_event_name(uint ev);
}