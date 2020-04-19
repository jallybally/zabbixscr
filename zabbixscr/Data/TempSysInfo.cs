using System.Collections.Generic;
namespace zabbixscr.Data
{
    struct TempSysInfoEn
    {
        public static string FileInfoName = System.String.Empty;
        //
        public static string SystemInfo = "SystemInfo";
        public static string Disk = "DiskInfo";
        public static string ProcessorInfo = "ProcessorInfo";
        public static string СomputerName = "СomputerName";
        public static string OSVersion = "OSVersion";
        public static string TimeZone = "TimeZone";
        public static string AutomaticManagedPagefile = "AutomaticManagedPagefile";
        public static string DNSHostName = "DNSHostName";
        public static string Domain = "Domain";
        public static string DomainRole = "DomainRole";
        public static string VirtualizationFirmwareEnabled = "VirtualizationFirmwareEnabled";
        public static string VMMonitorModeExtensions = "VMMonitorModeExtensions";
        public static string PhysicalDiskSize = "PhysicalDiskSize";
        public static string SerialNumberD = "SerialNumberD";
        public static string ProcessorModel = "ProcessorModel";
        public static string MaxClockSpeeP = "MaxClockSpeeP";
        public static string NumberOfCoresP = "NumberOfCoresP";
        public static string NumberOfLogicalProcessors = "NumberOfLogicalProcessors";
        public static string SerialNumberP = "SerialNumberP";
        //
        public static List<string> Processor = new List<string>();
        public static List<string> PhysicalDisk = new List<string>();
    }
}
