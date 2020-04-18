﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Management;
using System.Runtime.InteropServices;

namespace zabbixscr
{
    /*
     * Класс описывает вспомогательные функции
     */
    class Auxiliary
    {
        public static string UserDir(string UserDir, ref string ExUserDir)
        {
            DirectoryInfo Dir = new DirectoryInfo(UserDir + ".1ESKA");
            if (Dir.Exists)
            {
                if (Dir.LastWriteTime.Year == DateTime.Now.Year)
                {
                    ExUserDir = Dir.FullName;
                }
                else
                {
                    ExUserDir = UserDir;
                }
            }
            else
            {
                ExUserDir = UserDir;
            }
            return ExUserDir;
        }
        public static string[] SplittoMass(string Source, ref string[] Output)
        {
            Output = Source.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            return Output;
        }
        //Список папок пользователей с последней записью в этом году
        public static string GetUserDirList(ref string UserDirList)
        {
            DirectoryInfo Dir = new DirectoryInfo(@"C:\Users");
            foreach (var item in Dir.GetDirectories())
            {
                if (item.LastWriteTime.Year == DateTime.Now.Year)
                {
                    UserDirList += item.Name + " ";
                }
            }
            return UserDirList;
        }
        public void SMART()
        {
            try
            {           
                var dicDrives = new Dictionary<int, HDD>();
                var wdSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive");
                int iDriveIndex = 0;
                foreach (ManagementObject drive in wdSearcher.Get())
                {
                    var hdd = new HDD();
                    hdd.Model = drive["Model"].ToString().Trim();
                    hdd.Type = drive["InterfaceType"].ToString().Trim();
                    dicDrives.Add(iDriveIndex, hdd);
                    iDriveIndex++;
                }
                var pmsearcher = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMedia");
                iDriveIndex = 0;
                foreach (ManagementObject drive in pmsearcher.Get())
                {
                    if (iDriveIndex >= dicDrives.Count)
                        break;
                    dicDrives[iDriveIndex].Serial = drive["SerialNumber"] == null ? "None" : drive["SerialNumber"].ToString().Trim();
                    iDriveIndex++;
                }
                var searcher = new ManagementObjectSearcher("Select * from Win32_DiskDrive");
                searcher.Scope = new ManagementScope(@"\root\wmi");
                searcher.Query = new ObjectQuery("Select * from MSStorageDriver_FailurePredictStatus");
                iDriveIndex = 0;
                foreach (ManagementObject drive in searcher.Get())
                {
                    dicDrives[iDriveIndex].IsOK = (bool)drive.Properties["PredictFailure"].Value == false;
                    iDriveIndex++;
                }
                searcher.Query = new ObjectQuery("Select * from MSStorageDriver_FailurePredictData");
                iDriveIndex = 0;
                foreach (ManagementObject data in searcher.Get())
                {
                    Byte[] bytes = (Byte[])data.Properties["VendorSpecific"].Value;
                    for (int i = 0; i < 30; ++i)
                    {
                        try
                        {
                            int id = bytes[i * 12 + 2];
                            int flags = bytes[i * 12 + 4];
                            bool failureImminent = (flags & 0x1) == 0x1;
                            int value = bytes[i * 12 + 5];
                            int worst = bytes[i * 12 + 6];
                            int vendordata = BitConverter.ToInt32(bytes, i * 12 + 7);
                            if (id == 0) continue;

                            var attr = dicDrives[iDriveIndex].Attributes[id];
                            attr.Current = value;
                            attr.Worst = worst;
                            attr.Data = vendordata;
                            attr.IsOK = failureImminent == false;
                        }
                        catch
                        {
                            
                        }
                    }
                    iDriveIndex++;
                }
                searcher.Query = new ObjectQuery("Select * from MSStorageDriver_FailurePredictThresholds");
                iDriveIndex = 0;
                foreach (ManagementObject data in searcher.Get())
                {
                    Byte[] bytes = (Byte[])data.Properties["VendorSpecific"].Value;
                    for (int i = 0; i < 30; ++i)
                    {
                        try
                        {

                            int id = bytes[i * 12 + 2];
                            int thresh = bytes[i * 12 + 3];
                            if (id == 0) continue;

                            var attr = dicDrives[iDriveIndex].Attributes[id];
                            attr.Threshold = thresh;
                        }
                        catch
                        {
                            
                        }
                    }

                    iDriveIndex++;
                }
                foreach (var drive in dicDrives)
                {
                    Console.WriteLine(" DRIVE ({0}): " + drive.Value.Serial + " - " + drive.Value.Model + " - " + drive.Value.Type, ((drive.Value.IsOK) ? "OK" : "BAD"));
                }
            }
            catch (ManagementException e)
            {
                Console.WriteLine("An error occurred while querying for WMI data: " + e.Message);
            }
        }
    }
}
