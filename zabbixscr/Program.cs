using System;
using System.Runtime.InteropServices;

namespace zabbixscr
{
    class Program
    {
        static void Main(string[] args)
        {
            //try
            //{
                //вывод инфо о системе в xml
                if (args.Length == 1 && args[0] == "SystemInfo")
                {
                    SystemInfo.GetSystemInfo();
                }
                //
                if (args.Length == 1 && args[0] == "UserPersonalFolder")
                {
                    Catalogs catalogs = new Catalogs();
                    catalogs.GetUserFolber();
                    string u = String.Join(",", catalogs.BigUser);
                    Console.WriteLine($"{{\"data\":[{u}]}}");

                }
                if (args[0] == "UserPersonalFolberSize" && args[1] != null)
                {
                    Catalogs catalogs = new Catalogs();
                    catalogs.GetSizeUserFolber(args[1], ref Data.Temp.Out);
                    Console.WriteLine(Data.Temp.Out.ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US")));
                }
                /*
                 * 
                 * Инвенторизация
                 * 
                 */
                if (args.Length == 1 && args[0] == "Mac")
                {
                    Console.WriteLine(WMI.Mac(ref Data.Temp.Mac));
                }
                if (args.Length == 1 && args[0] == "BoardMaker")
                {
                    Console.WriteLine(WMI.BoardMaker(ref Data.Temp.BoardMaker));
                }
                if (args.Length == 1 && args[0] == "SerialNumber")
                {
                    Console.WriteLine(WMI.SerialNumber(ref Data.Temp.SerialNumber));
                }
                if (args.Length == 1 && args[0] == "PCName")
                {
                    Console.WriteLine(Environment.MachineName);
                }
                if (args.Length == 1 && args[0] == "OSVersion")
                {
                    Console.WriteLine(Environment.OSVersion);
                }
                if (args.Length == 1 && args[0] == "MemoryType")
                {
                    SMBIOSdata smbios = new SMBIOSdata();
                    smbios.GetRawData();
                    smbios.GetTables();
                    foreach (SMBIOStable table in smbios.p_oSMBIOStables)
                    {
                        smbios.ParseTable(table);
                    }
                }
                if (args.Length == 1 && args[0] == "MemoryInfo")
                {
                    SMBIOSdata smbios = new SMBIOSdata();
                    smbios.GetRawData();
                    smbios.GetTables();
                    foreach (SMBIOStable table in smbios.p_oSMBIOStables)
                    {
                        SMBIOSdata.ParseTable1(table);
                    }
                }
                if (args.Length == 1 && args[0] == "PhysicalDisk")
                {
                    Console.WriteLine($"{{\"data\":[{WMI.PhysicalDisk(ref Data.Temp.PhysicalDisk)}]}}");
                }
                /*
                 * 
                 * Инфа о hdd
                 * 
                 */
                if (args.Length == 1 && args[0] == "SMART")
                {
                    Auxiliary SMART = new Auxiliary();
                    SMART.SMART();
                }
                if (args[0] == "PhysicalDiskSize" && args[1] != null)
                {
                    Console.WriteLine(WMI.PhysicalDiskSize(args[1], ref Data.Temp.PhysicalDiskSize));
                }
                if (args[0] == "SerialNumberD" && args[1] != null)
                {
                    Console.WriteLine(WMI.SerialNumber(args[1], ref Data.Temp.SerialNumberD));
                }
                if (args.Length == 1 && args[0] == "LogicalDisk")
                {
                    Console.WriteLine($"{{\"data\":[{WMI.LogicalDisk(ref Data.Temp.LogicalDisk)}]}}");
                }
                if (args[0] == "SectionNameD" && args[1] != null)
                {
                    Console.WriteLine(WMI.SectionNameD(args[1], ref Data.Temp.SectionNameD).ToString(System.Globalization.CultureInfo.GetCultureInfo("ru-RU")));
                }
                if (args[0] == "SerialNumberLD" && args[1] != null)
                {
                    Console.WriteLine(WMI.SerialNumberLD(args[1], ref Data.Temp.SerialNumberLD));
                }
                if (args[0] == "CompressedLD" && args[1] != null)
                {
                    Console.WriteLine(WMI.CompressedLD(args[1], ref Data.Temp.CompressedLD));
                }
                if (args[0] == "FileSystemLD" && args[1] != null)
                {
                    Console.WriteLine(WMI.FileSystemLD(args[1], ref Data.Temp.FileSystemLD));
                }
                if (args[0] == "SizeLD" && args[1] != null)
                {
                    Console.WriteLine(WMI.SizeLD(args[1], ref Data.Temp.SizeLD));
                }
                if (args[0] == "FreeSpaceLD" && args[1] != null)
                {
                    Console.WriteLine(WMI.FreeSpaceLD(args[1], ref Data.Temp.FreeSpaceLD));
                }
                if (args[0] == "PercentageFreeSpace" && args[1] != null)
                {
                    Console.WriteLine(WMI.PercentageFreeSpace(args[1], ref Data.Temp.PercentageFreeSpace));
                }
                if (args[0] == "ChekDirtyTom" && args[1] != null)
                {
                    Console.WriteLine(WMI.ChekDirtyTom(args[1], ref Data.Temp.ChekDirtyTom));
                }
                if (args[0] == "AvgDiskReadQueueLength" && args[1] != null)
                {
                    Console.WriteLine(WMI.AvgDiskReadQueueLength(args[1], ref Data.Temp.AvgDiskReadQueueLength).ToString());
                }
                if (args[0] == "AvgDiskWriteQueueLength" && args[1] != null)
                {
                    Console.WriteLine(WMI.AvgDiskWriteQueueLength(args[1], ref Data.Temp.AvgDiskWriteQueueLength).ToString());
                }
                if (args[0] == "SplitIOPerSec" && args[1] != null)
                {
                    Console.WriteLine(WMI.SplitIOPerSec(args[1], ref Data.Temp.SplitIOPerSec).ToString());
                }
                if (args.Length == 1 && args[0] == "AvailableMBytesM")
                {
                    Console.WriteLine(WMI.AvailableMBytesM(ref Data.Temp.AvailableMBytesM).ToString());
                }
                if (args.Length == 1 && args[0] == "CacheBytesM")
                {
                    Console.WriteLine(WMI.CacheBytesM(ref Data.Temp.CacheBytesM).ToString());
                }
                if (args.Length == 1 && args[0] == "TotalPhysicalMemory")
                {
                    Console.WriteLine(WMI.TotalPhysicalMemory(ref Data.Temp.TotalPhysicalMemory).ToString());
                }
                if (args.Length == 1 && args[0] == "PageSecInDSwap")
                {
                    Console.WriteLine(WMI.PageSecInDSwap(ref Data.Temp.PageSecInDSwap).ToString());
                }
                if (args.Length == 1 && args[0] == "SerialNumberMemory")
                {
                    Console.WriteLine(WMI.SerialNumberMemory(ref Data.Temp.SerialNumberMemory).ToString());
                }
                if (args.Length == 1 && args[0] == "SpeedM")
                {
                    Console.WriteLine(WMI.SpeedM(ref Data.Temp.SpeedM).ToString());
                }
                if (args.Length == 1 && args[0] == "ProcessorDetected")
                {
                    Console.WriteLine($"{{\"data\":[{WMI.ProcessorDetected(ref Data.Temp.ProcessorDetected)}]}}");
                }
                if (args[0] == "LoadPercentageP" && args[1] != null)
                {
                    Console.WriteLine(WMI.LoadPercentageP(args[1], ref Data.Temp.LoadPercentageP).ToString());
                }
                if (args[0] == "ModelP" && args[1] != null)
                {
                    Console.WriteLine(WMI.ModelP(args[1], ref Data.Temp.ModelP).ToString());
                }
                if (args[0] == "CurrentClockSpeedP" && args[1] != null)
                {
                    Console.WriteLine(WMI.CurrentClockSpeedP(args[1], ref Data.Temp.CurrentClockSpeedP).ToString());
                }
                if (args[0] == "MaxClockSpeeP" && args[1] != null)
                {
                    Console.WriteLine(WMI.MaxClockSpeeP(args[1], ref Data.Temp.MaxClockSpeeP).ToString());
                }
                if (args[0] == "NumberOfCoresP" && args[1] != null)
                {
                    Console.WriteLine(WMI.NumberOfCoresP(args[1], ref Data.Temp.NumberOfCoresP).ToString());
                }
                if (args[0] == "NumberOfLogicalProcessors" && args[1] != null)
                {
                    Console.WriteLine(WMI.NumberOfLogicalProcessors(args[1], ref Data.Temp.NumberOfLogicalProcessors).ToString());
                }
                if (args[0] == "VirtualizationFirmwareEnabled" && args[1] != null)
                {
                    Console.WriteLine(WMI.VirtualizationFirmwareEnabled(args[1], ref Data.Temp.VirtualizationFirmwareEnabled).ToString());
                }
                if (args[0] == "VMMonitorModeExtensions" && args[1] != null)
                {
                    Console.WriteLine(WMI.VMMonitorModeExtensions(args[1], ref Data.Temp.VMMonitorModeExtensions).ToString());
                }
                if (args[0] == "SocketDesignation" && args[1] != null)
                {
                    Console.WriteLine(WMI.SocketDesignation(args[1], ref Data.Temp.SocketDesignation).ToString());
                }
                if (args[0] == "SerialNumberP" && args[1] != null)
                {
                    Console.WriteLine(WMI.SerialNumberP(args[1], ref Data.Temp.SerialNumberP).ToString());
                }
                if (args.Length == 1 && args[0] == "TimeZone")
                {
                    Console.WriteLine(WMI.TimeZone(ref Data.Temp.TimeZone).ToString());
                }
                if (args.Length == 1 && args[0] == "AutomaticManagedPagefile")
                {
                    Console.WriteLine(WMI.AutomaticManagedPagefile(ref Data.Temp.AutomaticManagedPagefile).ToString());
                }
                if (args.Length == 1 && args[0] == "DNSHostName")
                {
                    Console.WriteLine(WMI.DNSHostName(ref Data.Temp.DNSHostName).ToString());
                }
                if (args.Length == 1 && args[0] == "Domain")
                {
                    Console.WriteLine(WMI.Domain(ref Data.Temp.Domain).ToString());
                }
                if (args.Length == 1 && args[0] == "DomainRole")
                {
                    Console.WriteLine(WMI.DomainRole(ref Data.Temp.DomainRole).ToString());
                }
            //}
            //catch
            //{
                Console.WriteLine("101");
            //}
        }
    }
}
