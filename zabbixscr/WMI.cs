using System;
using System.Management;
using System.Net.NetworkInformation;

namespace zabbixscr
{
    /*
     * Класс описывает функции WMI
     */
    class WMI
    {
        //MAC адрес компьютера
        public static string Mac(ref string Mac)
        {
            foreach (NetworkInterface i in NetworkInterface.GetAllNetworkInterfaces())
            {
                try
                {
                    if (i.OperationalStatus == OperationalStatus.Up)
                    {
                        Mac = i.GetPhysicalAddress().ToString();
                        break;
                    }
                }
                catch
                {
                    Mac = "Не определено";
                    break;
                }
            }
            if (String.IsNullOrEmpty(Mac))
            {
                Mac = "Не определено";
            }
            return Mac;
        }
        //Производитель материнской платы
        public static string BoardMaker(ref string BoardMaker)
        {
            ManagementObjectSearcher i = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_BaseBoard");
            foreach (ManagementObject y in i.Get())
            {
                try
                {
                    BoardMaker = y.GetPropertyValue("Manufacturer").ToString();
                    break;
                }
                catch
                {
                    BoardMaker = "Не определено";
                    break;
                }
            }
            i.Dispose();
            if (String.IsNullOrEmpty(BoardMaker))
            {
                BoardMaker = "Не определено";
            }
            return BoardMaker;
        }
        //Серийный номер мат.платы
        public static string SerialNumber(ref string SerialNumber)
        {
            ManagementObjectSearcher i = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_BaseBoard");
            foreach (ManagementObject y in i.Get())
            {
                try
                {
                    SerialNumber = y.GetPropertyValue("SerialNumber").ToString();
                    break;
                }
                catch
                {
                    SerialNumber = "Не определено";
                    break;
                }
            }
            i.Dispose();
            if (String.IsNullOrEmpty(SerialNumber))
            {
                SerialNumber = "Не определено";
            }
            return SerialNumber;    
        }
        //Физ.диски
        public static string PhysicalDisk(ref string PhysicalDisk)
        {
            ManagementObjectSearcher i = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_DiskDrive");
            foreach (ManagementObject y in i.Get())
            { 
                try
                {
                    Data.LisrDat.DiskModel.Add($"{{\"{{#PHYSUCALDISKMODEL}}\":\"{Convert.ToString(y["Model"])}\"}}");
                }
                catch
                {
                    PhysicalDisk = "Не определено";
                    break;
                }
            }
            i.Dispose();
            PhysicalDisk = String.Join(",", Data.LisrDat.DiskModel);
            return PhysicalDisk;
        }
        //Размер физ.диска
        public static double PhysicalDiskSize(string arg, ref double PhysicalDiskSize)
        {
            ManagementObjectSearcher i = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_DiskDrive");
            foreach (ManagementObject y in i.Get())
            {
                try
                {
                    if (Convert.ToString(y["Model"]) == arg)
                    {
                        PhysicalDiskSize = Math.Round((double)Convert.ToInt64(y["Size"]) / 1024 / 1024 / 1024, 0);
                    }
                }
                catch
                {
                    PhysicalDiskSize = 0;
                    break;
                }
            }
            i.Dispose();
            return PhysicalDiskSize;
        }
        //Серийный номер
        public static string SerialNumber(string arg, ref string SerialNumber)
        {
            ManagementObjectSearcher i = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_DiskDrive");
            foreach (ManagementObject y in i.Get())
            {
                try
                {
                    if (Convert.ToString(y["Model"]) == arg)
                    {
                        SerialNumber = Convert.ToString(y["SerialNumber"]);
                    }
                }
                catch
                {
                    SerialNumber = "Не определено";
                    break;
                }
            }
            i.Dispose();
            if (String.IsNullOrEmpty(SerialNumber))
            {
                SerialNumber = "Не определено";
            }
            return SerialNumber;
        }
        //Логические диски
        public static string LogicalDisk(ref string LogicalDisk)
        {
            ManagementObjectSearcher i = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_LogicalDisk");
            foreach (ManagementObject y in i.Get())
            {
                try
                {
                    Data.LisrDat.LogicalDiskName.Add($"{{\"{{#LOGICALDISK}}\":\"{Convert.ToString(y["Name"])}\"}}");
                }
                catch
                {
                    LogicalDisk = "Не определено";
                    break;
                }
            }
            i.Dispose();
            LogicalDisk = String.Join(",", Data.LisrDat.LogicalDiskName);
            return LogicalDisk;
        }
        //Название раздела
        public static string SectionNameD(string arg, ref string SectionNameD)
        {
            ManagementObjectSearcher i = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_LogicalDisk");
            foreach (ManagementObject y in i.Get())
            {
                try
                {
                    if (Convert.ToString(y["Name"]) == arg)
                    {
                        SectionNameD = Convert.ToString(y["VolumeName"]);
                    }
                }
                catch
                {
                    SectionNameD = "Не определено";
                    break;
                }
            }
            i.Dispose();
            if (String.IsNullOrEmpty(SectionNameD))
            {
                SectionNameD = $"Локальный диск ({arg})";
            }
            return SectionNameD;
        }
        //Серийный номер раздела
        public static string SerialNumberLD(string arg, ref string SerialNumberLD)
        {
            ManagementObjectSearcher i = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_LogicalDisk");
            foreach (ManagementObject y in i.Get())
            {
                try
                {
                    if (Convert.ToString(y["Name"]) == arg)
                    {
                        SerialNumberLD = Convert.ToString(y["VolumeSerialNumber"]);
                    }
                }
                catch
                {
                    SerialNumberLD = "Не определено";
                    break;
                }
            }
            i.Dispose();
            if (String.IsNullOrEmpty(SerialNumberLD))
            {
                SerialNumberLD = "Не определено";
            }
            return SerialNumberLD;
        }
        //Сжатие
        public static string CompressedLD(string arg, ref string CompressedLD)
        {
            ManagementObjectSearcher i = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_LogicalDisk");
            foreach (ManagementObject y in i.Get())
            {
                try
                {
                    if (Convert.ToString(y["Name"]) == arg)
                    {
                        CompressedLD = Convert.ToString(y["Compressed"]);
                    }
                }
                catch
                {
                    CompressedLD = "Не определено";
                    break;
                }
            }
            i.Dispose();
            if (String.IsNullOrEmpty(CompressedLD))
            {
                CompressedLD = "Не определено";
            }
            return CompressedLD;
        }
        //файловая система
        public static string FileSystemLD(string arg, ref string FileSystemLD)
        {
            ManagementObjectSearcher i = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_LogicalDisk");
            foreach (ManagementObject y in i.Get())
            {
                try
                {
                    if (Convert.ToString(y["Name"]) == arg)
                    {
                        FileSystemLD = Convert.ToString(y["FileSystem"]);
                    }
                }
                catch
                {
                    FileSystemLD = "Не определено";
                    break;
                }
            }
            i.Dispose();
            if (String.IsNullOrEmpty(FileSystemLD))
            {
                FileSystemLD = "Не определено";
            }
            return FileSystemLD;
        }
        //размер раздела
        public static double SizeLD(string arg, ref double SizeLD)
        {
            ManagementObjectSearcher i = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_LogicalDisk");
            foreach (ManagementObject y in i.Get())
            {
                try
                {
                    if (Convert.ToString(y["Name"]) == arg)
                    {
                        SizeLD = Math.Round((double)Convert.ToInt64(y["Size"]) / 1024 / 1024 / 1024 , 0);
                    }
                }
                catch
                {
                    SizeLD = 101;
                    break;
                }
            }
            i.Dispose();
            return SizeLD;
        }
        // свободно на диске
        public static double FreeSpaceLD(string arg, ref double FreeSpaceLD)
        {
            ManagementObjectSearcher i = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_LogicalDisk");
            foreach (ManagementObject y in i.Get())
            {
                try
                {
                    if (Convert.ToString(y["Name"]) == arg)
                    {
                        FreeSpaceLD = Math.Round((double)Convert.ToInt64(y["FreeSpace"]) / 1024 / 1024 / 1024, 0);
                    }
                }
                catch
                {
                    FreeSpaceLD = 101;
                    break;
                }
            }
            i.Dispose();
            return FreeSpaceLD;
        }
        //% свободного места на диске
        public static double PercentageFreeSpace(string arg, ref double PercentageFreeSpace)
        {
            double Size = 0;
            double Free = 0;
            PercentageFreeSpace = Math.Round((double)(FreeSpaceLD(arg, ref Free)) / (SizeLD(arg, ref Size)) * 100);
            return PercentageFreeSpace;
        }
        //"грязный том"
        public static string ChekDirtyTom(string arg, ref string ChekDirtyTom)
        {
            ManagementObjectSearcher i = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_LogicalDisk");
            foreach (ManagementObject y in i.Get())
            {
                try
                {
                    if (Convert.ToString(y["Name"]) == arg)
                    {
                        ChekDirtyTom = Convert.ToString(y["VolumeDirty"]);
                    }
                }
                catch
                {
                    ChekDirtyTom = "Не определено";
                    break;
                }
            }
            i.Dispose();
            if (String.IsNullOrEmpty(ChekDirtyTom))
            {
                ChekDirtyTom = "Не определено";
            }
            return ChekDirtyTom;
        }
        //очередь к диску чтение 
        public static int AvgDiskReadQueueLength(string arg, ref int AvgDiskReadQueueLength)
        {
            ManagementObjectSearcher i = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PerfFormattedData_PerfDisk_PhysicalDisk");
            foreach (ManagementObject y in i.Get())
            {
                try
                {
                    if (Convert.ToString(y["Name"]).Contains(arg))
                    {
                        AvgDiskReadQueueLength = Convert.ToInt32(y["AvgDiskReadQueueLength"]);
                        break;
                    }
                }
                catch
                {
                    AvgDiskReadQueueLength = 101;
                    break;
                }
            }
            i.Dispose();
            return AvgDiskReadQueueLength;
        }
        //очередь к диску запись
        public static int AvgDiskWriteQueueLength(string arg, ref int AvgDiskWriteQueueLength)
        {
            ManagementObjectSearcher i = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PerfFormattedData_PerfDisk_PhysicalDisk");
            foreach (ManagementObject y in i.Get())
            {
                try
                {
                    if (Convert.ToString(y["Name"]).Contains(arg))
                    {
                        AvgDiskWriteQueueLength = Convert.ToInt32(y["AvgDiskReadQueueLength"]);
                        break;
                    }
                }
                catch
                {
                    AvgDiskWriteQueueLength = 101;
                    break;
                }
            }
            i.Dispose();
            return AvgDiskWriteQueueLength;
        }
        //I/O
        public static int SplitIOPerSec(string arg, ref int SplitIOPerSec)
        {
            ManagementObjectSearcher i = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PerfFormattedData_PerfDisk_PhysicalDisk");
            foreach (ManagementObject y in i.Get())
            {
                try
                {
                    if (Convert.ToString(y["Name"]).Contains(arg))
                    {
                        SplitIOPerSec = Convert.ToInt32(y["SplitIOPerSec"]);
                        break;
                    }
                }
                catch
                {
                    SplitIOPerSec = 101;
                    break;
                }
            }
            i.Dispose();
            return SplitIOPerSec;
        }
        //доступно озу
        public static double AvailableMBytesM(ref double AvailableMBytesM)
        {
            ManagementObjectSearcher i = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PerfFormattedData_PerfOS_Memory");
            foreach (ManagementObject y in i.Get())
            {
                try
                {
                    AvailableMBytesM = Math.Round((double)Convert.ToInt64(y["AvailableMBytes"]), 0);
                }
                catch
                {
                    AvailableMBytesM = 101;
                    break;
                }
            }
            i.Dispose();
            return AvailableMBytesM;
        }
        //кэшировано озу
        public static double CacheBytesM(ref double CacheBytesM)
        {
            ManagementObjectSearcher i = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PerfFormattedData_PerfOS_Memory");
            foreach (ManagementObject y in i.Get())
            {
                try
                {
                    CacheBytesM = Math.Round((double)Convert.ToInt64(y["CacheBytes"]) / 1e+9, 0);
                }
                catch
                {
                    CacheBytesM = 101;
                    break;
                }
            }
            i.Dispose();
            return CacheBytesM;
        }
        //всего озу
        public static double TotalPhysicalMemory(ref double TotalPhysicalMemory)
        {
            ManagementObjectSearcher i = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PhysicalMemory");
            foreach (ManagementObject y in i.Get())
            {   
                try
                {
                    TotalPhysicalMemory += Convert.ToInt64(y["Capacity"]);
                }
                catch
                {
                    TotalPhysicalMemory = 101;
                    break;
                }
            }
            i.Dispose();
            TotalPhysicalMemory = Math.Round((double)TotalPhysicalMemory / 1e+9, 0);
            return TotalPhysicalMemory;
        }
        //использование диска файлом подкачки
        public static double PageSecInDSwap(ref double PageSecInDSwap)
        {
            Int32 q = 0;
            Int32 w = 0;
            ManagementObjectSearcher i = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PerfFormattedData_PerfOS_Memory");
            foreach (ManagementObject y in i.Get())
            {
                try
                {
                    q = Convert.ToInt32(y["PagesPersec"]);
                }
                catch
                {
                    q = 101;
                    break;
                }
            }
            i.Dispose();
            ManagementObjectSearcher u = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PerfFormattedData_PerfDisk_PhysicalDisk");
            foreach (ManagementObject y in u.Get())
            {
                try
                {
                    w = Convert.ToInt32(y["AvgDisksecPerTransfer"]);
                }
                catch
                {
                    w = 101;
                    break;
                }
            }
            u.Dispose();
            PageSecInDSwap = q * w;
            return PageSecInDSwap;
        }
        //серийный номер планки озу
        public static string SerialNumberMemory(ref string SerialNumberMemory)
        {
            ManagementObjectSearcher u = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PhysicalMemory");
            foreach (ManagementObject y in u.Get())
            {
                try
                {
                    Data.LisrDat.SerialNumberMemory.Add($"{Convert.ToString(y["Tag"])}  {Convert.ToString(y["SerialNumber"])}\n");
                }
                catch
                {
                    Data.LisrDat.SerialNumberMemory.Add("101");
                }
            }
            u.Dispose();
            SerialNumberMemory = String.Join("", Data.LisrDat.SerialNumberMemory);
            return SerialNumberMemory;
        }
        //частота озу
        public static string SpeedM(ref string SpeedM)
        {
            ManagementObjectSearcher i = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PhysicalMemory");
            foreach (ManagementObject y in i.Get())
            {
                try
                {
                    Data.LisrDat.SpeedM.Add($"{Convert.ToString(y["Tag"])}  {Convert.ToString(y["Speed"])}\n");
                }
                catch
                {
                    Data.LisrDat.SpeedM.Add("101");
                }
            }
            i.Dispose();
            SpeedM = String.Join("", Data.LisrDat.SpeedM);
            return SpeedM;
        }
        //обнаруджение процессора
        public static string ProcessorDetected(ref string ProcessorDetected)
        { 
            ManagementObjectSearcher i = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_Processor");
            foreach (ManagementObject y in i.Get())
            {
                try
                {
                    Data.LisrDat.ProcessorDetected.Add($"{{\"{{#PROCESSORKMODEL}}\":\"{Convert.ToString(y["DeviceID"])}\"}}");
                }
                catch
                {
                    ProcessorDetected = "101";
                    break;
                }
            }
            i.Dispose();
            ProcessorDetected = String.Join(",", Data.LisrDat.ProcessorDetected);
            return ProcessorDetected;
        }
        //загруженность процессора %
        public static int LoadPercentageP(string arg, ref int LoadPercentageP)
        {
            ManagementObjectSearcher i = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_Processor");
            foreach (ManagementObject y in i.Get())
            {
                try
                {
                    if (Convert.ToString(y["DeviceID"]).Contains(arg))
                    {
                        LoadPercentageP = Convert.ToInt32(y["LoadPercentage"]);
                    }
                }
                catch
                {
                    LoadPercentageP = 101;
                    break;
                }
            }
            i.Dispose();
            return LoadPercentageP;
        }
        //модель процессора
        public static string ModelP(string arg, ref string ModelP)
        {
            ManagementObjectSearcher i = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_Processor");
            foreach (ManagementObject y in i.Get())
            {
                try
                {
                    if (Convert.ToString(y["DeviceID"]).Contains(arg))
                    {
                        ModelP = y["Name"].ToString();
                    }
                }
                catch
                {
                    ModelP = "101";
                    break;
                }
            }
            i.Dispose();
            return ModelP;
        }
        //текущая частота процессора
        public static Int32 CurrentClockSpeedP(string arg, ref Int32 CurrentClockSpeedP)
        {
            ManagementObjectSearcher i = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_Processor");
            foreach (ManagementObject y in i.Get())
            {
                try
                {
                    if (Convert.ToString(y["DeviceID"]).Contains(arg))
                    {
                        CurrentClockSpeedP = Convert.ToInt32(y["CurrentClockSpeed"]);
                    }
                }
                catch
                {
                    CurrentClockSpeedP = 101;
                    break;
                }
            }
            i.Dispose();
            return CurrentClockSpeedP;
        }
        //максимальная частота процессора
        public static Int32 MaxClockSpeeP(string arg, ref Int32 MaxClockSpeeP)
        {
            ManagementObjectSearcher i = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_Processor");
            foreach (ManagementObject y in i.Get())
            {
                try
                {
                    if (Convert.ToString(y["DeviceID"]).Contains(arg))
                    {
                        MaxClockSpeeP = Convert.ToInt32(y["MaxClockSpeed"]);
                    }
                }
                catch
                {
                    MaxClockSpeeP = 101;
                    break;
                }
            }
            i.Dispose();
            return MaxClockSpeeP;
        }
        //кол-во физ.ядер
        public static Int32 NumberOfCoresP(string arg, ref Int32 NumberOfCoresP)
        {
            ManagementObjectSearcher i = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_Processor");
            foreach (ManagementObject y in i.Get())
            {
                try
                {
                    if (Convert.ToString(y["DeviceID"]).Contains(arg))
                    {
                        NumberOfCoresP = Convert.ToInt32(y["NumberOfCores"]);
                    }
                }
                catch
                {
                    NumberOfCoresP = 101;
                    break;
                }
            }
            i.Dispose();
            return NumberOfCoresP;
        }
        //кол-во логических ядер
        public static Int32 NumberOfLogicalProcessors(string arg, ref Int32 NumberOfLogicalProcessors)
        {
            ManagementObjectSearcher i = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_Processor");
            foreach (ManagementObject y in i.Get())
            {
                try
                {
                    if (Convert.ToString(y["DeviceID"]).Contains(arg))
                    {
                        NumberOfLogicalProcessors = Convert.ToInt32(y["NumberOfLogicalProcessors"]);
                    }
                }
                catch
                {
                    NumberOfLogicalProcessors = 101;
                    break;
                }
            }
            i.Dispose();
            return NumberOfLogicalProcessors;
        }
        //виртуализация
        public static string VirtualizationFirmwareEnabled(string arg, ref string VirtualizationFirmwareEnabled)
        {
            ManagementObjectSearcher i = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_Processor");
            foreach (ManagementObject y in i.Get())
            {
                try
                {
                    if (Convert.ToString(y["DeviceID"]).Contains(arg))
                    {
                        VirtualizationFirmwareEnabled = Convert.ToString(y["VirtualizationFirmwareEnabled"]);
                    }
                }
                catch
                {
                    VirtualizationFirmwareEnabled = "101";
                    break;
                }
            }
            i.Dispose();
            return VirtualizationFirmwareEnabled;
        }
        //поддержка виртуализации
        public static string VMMonitorModeExtensions(string arg, ref string VMMonitorModeExtensions)
        {
            ManagementObjectSearcher i = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_Processor");
            foreach (ManagementObject y in i.Get())
            {
                try
                {
                    if (Convert.ToString(y["DeviceID"]).Contains(arg))
                    {
                        VMMonitorModeExtensions = Convert.ToString(y["VMMonitorModeExtensions"]);
                    }
                }
                catch
                {
                    VMMonitorModeExtensions = "101";
                    break;
                }
            }
            i.Dispose();
            return VMMonitorModeExtensions;
        }
        //сокет
        public static string SocketDesignation(string arg, ref string SocketDesignation)
        {
            ManagementObjectSearcher i = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_Processor");
            foreach (ManagementObject y in i.Get())
            {
                try
                {
                    if (Convert.ToString(y["DeviceID"]).Contains(arg))
                    {
                        SocketDesignation = Convert.ToString(y["SocketDesignation"]);
                    }
                }
                catch
                {
                    SocketDesignation = "101";
                    break;
                }
            }
            i.Dispose();
            return SocketDesignation;
        }
        //серийный номер процессора 
        public static string SerialNumberP(string arg, ref string SerialNumberP)
        {
            ManagementObjectSearcher i = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_Processor");
            foreach (ManagementObject y in i.Get())
            {
                try
                {
                    if (Convert.ToString(y["DeviceID"]).Contains(arg))
                    {
                        SerialNumberP = Convert.ToString(y["SerialNumberP"]);
                    }
                }
                catch
                {
                    SerialNumberP = "101";
                    break;
                }
            }
            i.Dispose();
            return SerialNumberP;
        }
        //Часовой пояс
        public static string TimeZone(ref string TimeZone)
        {
            ManagementObjectSearcher i = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_TimeZone");
            foreach (ManagementObject y in i.Get())
            {
                try
                {
                    TimeZone = Convert.ToString(y["Description"]);
                }
                catch
                {
                    TimeZone = "101";
                    break;
                }
            }
            i.Dispose();
            return TimeZone;
        }
        //Автоматический файл подкачки
        public static string AutomaticManagedPagefile(ref string AutomaticManagedPagefile)
        {
            ManagementObjectSearcher i = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_ComputerSystem");
            foreach (ManagementObject y in i.Get())
            {
                try
                {
                    AutomaticManagedPagefile = Convert.ToString(y["AutomaticManagedPagefile"]);
                }
                catch
                {
                    AutomaticManagedPagefile = "101";
                    break;
                }
            }
            i.Dispose();
            return AutomaticManagedPagefile;
        }
        //днс имя
        public static string DNSHostName(ref string DNSHostName)
        {
            ManagementObjectSearcher i = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_ComputerSystem");
            foreach (ManagementObject y in i.Get())
            {
                try
                {
                    DNSHostName = Convert.ToString(y["DNSHostName"]);
                }
                catch
                {
                    DNSHostName = "101";
                    break;
                }
            }
            i.Dispose();
            return DNSHostName;
        }
        //домен
        public static string Domain(ref string Domain)
        {
            ManagementObjectSearcher i = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_ComputerSystem");
            foreach (ManagementObject y in i.Get())
            {
                try
                {
                    Domain = Convert.ToString(y["Domain"]);
                }
                catch
                {
                    Domain = "101";
                    break;
                }
            }
            i.Dispose();
            return Domain;
        }
        //роль в домене
        public static string DomainRole(ref string DomainRole)
        {
            ManagementObjectSearcher i = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_ComputerSystem");
            foreach (ManagementObject y in i.Get())
            {
                try
                {
                    DomainRole = Convert.ToString(y["DomainRole"]);
                }
                catch
                {
                    DomainRole = "101";
                    break;
                }
            }
            i.Dispose();
            if (DomainRole == "0")
            {
                DomainRole = "Автономная рабочая станция";
            }
            else if (DomainRole == "1")
            {
                DomainRole = "Рабочая станция участника";
            }
            else if (DomainRole == "2")
            {
                DomainRole = "Автономный сервер";
            }
            else if (DomainRole == "3")
            {
                DomainRole = "Рядовой сервер";
            }
            else if (DomainRole == "4")
            {
                DomainRole = "Резервный контроллер домена";
            }
            else if (DomainRole == "5")
            {
                DomainRole = "Основной контроллер домена";
            }
            return DomainRole;
        }
    }
}