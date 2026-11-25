namespace LibblurayConnector;

public sealed record BluRayDiscInfo(
    string? DiscName,
    string? VolumeId,
    bool FirstPlaySupported,
    bool TopMenuSupported,
    uint TitleCount,
    uint HdmvTitleCount,
    uint BdjTitleCount,
    uint UnsupportedTitleCount,
    bool BdjDetected,
    bool JvmDetected,
    bool BdjAvailable);