using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Management;
using System.Security.Cryptography;

namespace Dog
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //2015_01_25_xuchenhui
        private List<string> _serialNumber = new List<string>();
        private void button1_Click(object sender, EventArgs e)
        {
            string fName;
            _serialNumber = GetDriveSerialNums();
            if (_serialNumber == null || _serialNumber.Count == 0)
            {
                MessageBox.Show("请插入看门狗!", "提示", MessageBoxButtons.OK);
                return;
            }
            OpenFileDialog OpenConfigFile = new OpenFileDialog();
            OpenConfigFile.Filter = "所有文件|*.*";
            if (OpenConfigFile.ShowDialog() == DialogResult.OK)
            {
                fName = OpenConfigFile.FileName;
                string fileName = fName.Substring(fName.Length - 14);
                if (fileName == "WatchDogConfig")
                {
                    
                    foreach (string serialNum in _serialNumber)
                    { 
                        string tempPassword = MD5(serialNum);
                        List<string> passwordList = new List<string>();
                        StreamReader sr = new StreamReader(fName, true);
                        string temp = sr.ReadLine();
                        while (temp != null)
                        {
                            passwordList.Add(temp);
                            temp = sr.ReadLine();
                        }
                        sr.Close();
                        if (passwordList.Contains(tempPassword))
                        {
                            MessageBox.Show("该加密狗已存在", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        else
                        {
                            StreamWriter sw = new StreamWriter(fName, true);
                            sw.WriteLine(tempPassword);
                            sw.Close();
                        }
                    }
                    if (MessageBox.Show("文件保存成功，是否关闭加密软件?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("请选择正确的配置文件", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        //获取设配序列号
        public static List<string> GetDriveSerialNums()
        {
            List<string> _serialNumber = new List<string>();
            string[] diskArray;
            string driveNumber;
            string driveLetter;
            
            ManagementObjectSearcher searcher1 = new ManagementObjectSearcher("SELECT * FROM Win32_LogicalDiskToPartition");
            foreach (ManagementObject dm in searcher1.Get())
            {
                diskArray = null;
                driveLetter = getValueInQuotes(dm["Dependent"].ToString());
                diskArray = getValueInQuotes(dm["Antecedent"].ToString()).Split(',');

                driveNumber = diskArray[0].Remove(0, 6).Trim();
                ManagementObjectSearcher disks = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive");
                foreach (ManagementObject disk in disks.Get())
                {
                    if (disk["Name"].ToString() == ("\\\\.\\PHYSICALDRIVE" + driveNumber) & disk["InterfaceType"].ToString() == "USB")
                        _serialNumber.Add(parseSerialFromDeviceID(disk["PNPDeviceID"].ToString()));
                }
            }
            return _serialNumber;
        }
        public static string getValueInQuotes(string inValue)
        {
            string parsedValue = "";
            int posFoundStart = 0;
            int posFoundEnd = 0;
            posFoundStart = inValue.IndexOf("\"");
            posFoundEnd = inValue.IndexOf("\"", posFoundStart + 1);
            parsedValue = inValue.Substring(posFoundStart + 1, (posFoundEnd - posFoundStart) - 1);
            return parsedValue;
        }
        //读取加密狗序列号
        public static string parseSerialFromDeviceID(string DeviceId)
        {
            string[] splitDeviceId = DeviceId.Split('\\');
            string[] serialArray;
            string serial;
            int arrayLen = splitDeviceId.Length - 1;

            serialArray = splitDeviceId[arrayLen].Split('&');
            serial = serialArray[0];
            return serial;
        }
        public static string MD5(string encryptString)
        {
            byte[] result = Encoding.Default.GetBytes(encryptString);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(result);
            string encryptResult = BitConverter.ToString(output).Replace("-", "");
            //return encryptString;
            return encryptResult;
        }

        private void New_Click(object sender, EventArgs e)
        {
            string fName;
            _serialNumber = GetDriveSerialNums();
            if (_serialNumber == null || _serialNumber.Count == 0)
            {
                MessageBox.Show("请插入看门狗!", "提示", MessageBoxButtons.OK);
                return;
            }
            if (tbremaind.Text == "")
            {
                MessageBox.Show("请输入系统码！", "提示", MessageBoxButtons.OK);
                return;
            }
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "所有文件|*.*";
            saveFile.Title = "新建加密文件";
            saveFile.FileName = "WatchDogConfig";
            if (saveFile.ShowDialog() == DialogResult.OK && saveFile.FileName.Substring(saveFile.FileName.Length - 14) == "WatchDogConfig")
            {
                fName = saveFile.FileName;
                FileStream fs = File.Create(fName);
                StreamWriter sw = new StreamWriter(fs, Encoding.Default);               
                foreach (string serialNum in _serialNumber)
                {
                    string total_serial = serialNum + tbremaind.Text;
                    string tempPassword = MD5(total_serial);
                    sw.WriteLine(tempPassword);
                }
                sw.Close();
                if (MessageBox.Show("文件新建成功，是否关闭加密软件?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    this.Close();
                }
            }
            else if (saveFile.FileName.Substring(saveFile.FileName.Length - 14) != "WatchDogConfig")
            {
                MessageBox.Show("请将新建文件命名为WatchDogConfig", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        //声明事件
        public delegate void CloseLoginForm(object sender, FormClosedEventArgs e);
        public event CloseLoginForm CloseLoginEvent;
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            CloseLoginEvent(sender, e);//触发事件
        }

    }
}
