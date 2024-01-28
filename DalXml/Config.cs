namespace Dal;

internal static class Config
{
    readonly static string s_data_config_xml = "data-config";
    internal static int NextDependenceId { get => XMLTools.GetAndIncreaseNextId(s_data_config_xml, "NextDependenceId"); }
    internal static int NextTaskId { get => XMLTools.GetAndIncreaseNextId(s_data_config_xml, "NextTaskId"); }
}


