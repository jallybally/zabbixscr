using System;
using System.Management;
using System.IO;
using System.Xml.Linq;

namespace zabbixscr
{
    class SystemInfo
    {
        public static void GetSystemInfo()
        {
            DopWMI.ProcessorDetected();
            DopWMI.PhysicalDisk();
            CreatFile.Craetfile();
        }
    }
    class CreatFile
    {
        public static void Craetfile()
        {
            Data.TempSysInfoEn.FileInfoName = $"{Directory.GetCurrentDirectory()}\\SystemInfo.xml";
            if (!File.Exists( Data.TempSysInfoEn.FileInfoName))
            {
                XML.CreatXML();
            }
            else
            {
                File.Delete(Data.TempSysInfoEn.FileInfoName);
                CreatFile.Craetfile();
            }
        }
    }
    class XML
    {
        public static void CreatXML()
        {
            XDocument xdoc = new XDocument();
            XElement SystemInfo = new XElement(Data.TempSysInfoEn.SystemInfo);
            //System
            XElement СomputerName = new XElement(Data.TempSysInfoEn.СomputerName, Environment.MachineName);
            SystemInfo.Add(СomputerName);
            XElement OSVersion = new XElement(Data.TempSysInfoEn.OSVersion, Environment.OSVersion);
            SystemInfo.Add(OSVersion);
            XElement TimeZone = new XElement(Data.TempSysInfoEn.TimeZone, WMI.TimeZone(ref Data.Temp.TimeZone));
            SystemInfo.Add(TimeZone);
            XElement AutomaticManagedPagefile = new XElement(Data.TempSysInfoEn.AutomaticManagedPagefile, WMI.AutomaticManagedPagefile(ref Data.Temp.AutomaticManagedPagefile));
            SystemInfo.Add(AutomaticManagedPagefile);
            XElement DNSHostName = new XElement(Data.TempSysInfoEn.DNSHostName, WMI.DNSHostName(ref Data.Temp.DNSHostName));
            SystemInfo.Add(DNSHostName);
            XElement Domain = new XElement(Data.TempSysInfoEn.Domain, WMI.Domain(ref Data.Temp.Domain));
            SystemInfo.Add(Domain);
            XElement DomainRole = new XElement(Data.TempSysInfoEn.DomainRole, WMI.DomainRole(ref Data.Temp.DomainRole));
            SystemInfo.Add(DomainRole);
            foreach (var i in Data.TempSysInfoEn.Processor)
            {
                XElement VirtualizationFirmwareEnabled = new XElement(Data.TempSysInfoEn.VirtualizationFirmwareEnabled, WMI.VirtualizationFirmwareEnabled(i, ref Data.Temp.VirtualizationFirmwareEnabled));
                SystemInfo.Add(VirtualizationFirmwareEnabled);
            }
            foreach (var i in Data.TempSysInfoEn.Processor)
            {
                XElement VMMonitorModeExtensions = new XElement(Data.TempSysInfoEn.VMMonitorModeExtensions, WMI.VMMonitorModeExtensions(i, ref Data.Temp.VMMonitorModeExtensions));
                SystemInfo.Add(VMMonitorModeExtensions);
            }
            //Disk
            XElement Disk = new XElement(Data.TempSysInfoEn.Disk);
            foreach (var i in Data.TempSysInfoEn.PhysicalDisk)
            {
                XElement PhysicalDiskSize = new XElement(Data.TempSysInfoEn.PhysicalDiskSize,$"{ i } : {WMI.PhysicalDiskSize(i, ref Data.Temp.PhysicalDiskSize)}");
                Disk.Add(PhysicalDiskSize);
            }
            foreach (var i in Data.TempSysInfoEn.PhysicalDisk)
            {
                XElement PhysicalDiskSize = new XElement(Data.TempSysInfoEn.SerialNumberD, $"{ i } : {WMI.SerialNumber(i, ref Data.Temp.SerialNumberD)}");
                Disk.Add(PhysicalDiskSize);
            }
            //Processor
            XElement Processor = new XElement(Data.TempSysInfoEn.ProcessorInfo);
            foreach (var i in Data.TempSysInfoEn.Processor)
            {
                XElement ProcessorModel = new XElement(Data.TempSysInfoEn.ProcessorModel, $"{ i } : {WMI.ModelP(i, ref Data.Temp.ModelP)}");
                Processor.Add(ProcessorModel);
            }
            foreach (var i in Data.TempSysInfoEn.Processor)
            {
                XElement MaxClockSpeeP = new XElement(Data.TempSysInfoEn.MaxClockSpeeP, $"{ i } : {WMI.MaxClockSpeeP(i, ref Data.Temp.MaxClockSpeeP)}");
                Processor.Add(MaxClockSpeeP);
            }
            foreach (var i in Data.TempSysInfoEn.Processor)
            {
                XElement NumberOfCoresP = new XElement(Data.TempSysInfoEn.NumberOfCoresP, $"{ i } : {WMI.NumberOfCoresP(i, ref Data.Temp.NumberOfCoresP)}");
                Processor.Add(NumberOfCoresP);
            }
            foreach (var i in Data.TempSysInfoEn.Processor)
            {
                XElement NumberOfLogicalProcessors = new XElement(Data.TempSysInfoEn.NumberOfLogicalProcessors, $"{ i } : {WMI.NumberOfLogicalProcessors(i, ref Data.Temp.NumberOfLogicalProcessors)}");
                Processor.Add(NumberOfLogicalProcessors);
            }
            foreach (var i in Data.TempSysInfoEn.Processor)
            {
                XElement SerialNumberP = new XElement(Data.TempSysInfoEn.SerialNumberP, $"{ i } : {WMI.SerialNumberP(i, ref Data.Temp.SerialNumberP)}");
                Processor.Add(SerialNumberP);
            }
            // создаем корневой элемент
            XElement Info = new XElement("Info");
            // добавляем в корневой элемент
            Info.Add(SystemInfo, Disk, Processor);
            // добавляем корневой элемент в документ
            xdoc.Add(Info);
            //сохраняем документ
            xdoc.Save(Data.TempSysInfoEn.FileInfoName);
        }
    }
    class DopWMI
    {
        //обнаруджение процессора
        public static void ProcessorDetected()
        {
            ManagementObjectSearcher i = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_Processor");
            foreach (ManagementObject y in i.Get())
            {
                try
                {
                    Data.TempSysInfoEn.Processor.Add(Convert.ToString(y["DeviceID"]));
                }
                catch
                {
                    Data.TempSysInfoEn.Processor.Add(Convert.ToString("101"));
                    break;
                }
            }
            i.Dispose();
        }
        //Физ.диски
        public static void PhysicalDisk()
        {
            ManagementObjectSearcher i = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_DiskDrive");
            foreach (ManagementObject y in i.Get())
            {
                try
                {
                    Data.TempSysInfoEn.PhysicalDisk.Add(Convert.ToString(y["Model"]).Trim('"'));
                }
                catch
                {
                    Data.TempSysInfoEn.PhysicalDisk.Add("Не определено");
                    break;
                }
            }
            i.Dispose();
        }
    }
}