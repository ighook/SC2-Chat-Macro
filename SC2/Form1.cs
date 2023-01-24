using System.Diagnostics;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

public enum WMessages : int
{
    WM_CHAR = 0x102     //char
}

namespace SC2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        [DllImport("user32.dll")]
        public static extern int PostMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

        [DllImport("user32.dll")]
        private static extern IntPtr FindWindow(string SClassName, string SWindowName);
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindowEx(IntPtr hWnd1, IntPtr hWnd2, string lpsz1, string lpsz2);
        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr findname);
        [DllImport("user32.dll")]
        private static extern bool ShowWindowAsync(IntPtr findname, int howShow);
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        public static String iniPath = Application.StartupPath + @"\config.ini";

        private const int showNORMAL = 1;
        private const int showMINIMIZED = 2;
        private const int showMAXIMIZED = 3;

        public IntPtr process;

        int civil1 = 0;
        int civil2 = 0;
        int civil3 = 0;
        bool sale1 = false;
        bool sale2 = false;
        bool sale3 = false;
        bool sale4 = false;
        bool sale5 = false;

        private void Form1_Load(object sender, EventArgs e)
        {
            process = FindWindow(null, "��Ÿũ����Ʈ II");
            //process = FindWindow(null, "MapleStory");
            if (process.Equals(IntPtr.Zero))
            {
                MessageBox.Show("��Ÿũ����Ʈ�� ã�� �� �����ϴ�.");
            }
            else
            {
                LoadSetting();
            }
        }

        public static IntPtr FindHandle(string window)
        {
            IntPtr hw1 = FindWindow(null, window);
            IntPtr hw2 = FindWindowEx(hw1, IntPtr.Zero, null, "");
            return hw2;
        }

        public void SendChar(char key)
        {
            PostMessage(process, (int)WMessages.WM_CHAR, key, 0);
        }

        
        public void SendCommand(String str)
        {
            PostMessage(process, 0x100, 0x0D, 0);
            foreach (char i in str)
            {
                SendChar(i);
            }
            PostMessage(process, 0x100, 0x0D, 0);
        }

        private void btnAllCivilLowUnit_Click(object sender, EventArgs e)
        {
            btnAllCivilLowUnit.BackColor = Color.FromArgb(183, 240, 177);
            btnAllCivilUnit.BackColor = Color.White;
            btnAllCivilMineral.BackColor = Color.White;
            btnAllCivilGas.BackColor = Color.White;
            
            civil1 = 1;
            IniFile.SetValue(iniPath, "����", "AllCivil", "LowUnit");
            SendCommand("@d");
        }

        private void btnAllCivilUnit_Click(object sender, EventArgs e)
        {
            btnAllCivilLowUnit.BackColor = Color.White;
            btnAllCivilUnit.BackColor = Color.FromArgb(183, 240, 177);
            btnAllCivilMineral.BackColor = Color.White;
            btnAllCivilGas.BackColor = Color.White;
            SendCommand("@u");
            IniFile.SetValue(iniPath, "����", "AllCivil", "Unit");
        }

        private void btnAllCivilMineral_Click(object sender, EventArgs e)
        {
            btnAllCivilLowUnit.BackColor = Color.White;
            btnAllCivilUnit.BackColor = Color.White;
            btnAllCivilMineral.BackColor = Color.FromArgb(183, 240, 177);
            btnAllCivilGas.BackColor = Color.White;
            SendCommand("@l");
            IniFile.SetValue(iniPath, "����", "AllCivil", "Mineral");
        }

        private void btnAllCivilGas_Click(object sender, EventArgs e)
        {
            btnAllCivilLowUnit.BackColor = Color.White;
            btnAllCivilUnit.BackColor = Color.White;
            btnAllCivilMineral.BackColor = Color.White;
            btnAllCivilGas.BackColor = Color.FromArgb(183, 240, 177);
            SendCommand("@r");
            IniFile.SetValue(iniPath, "����", "AllCivil", "GAS");
        }

        private void btnUnitCivilLowUnit_Click(object sender, EventArgs e)
        {
            btnUnitCivilLowUnit.BackColor = Color.FromArgb(183, 240, 177);
            btnUnitCivilCommon.BackColor = Color.White;
            SendCommand("@bu");
            IniFile.SetValue(iniPath, "����", "UnitCivil", "LowUnit");
        }

        private void btnUnitCivilCommon_Click(object sender, EventArgs e)
        {
            btnUnitCivilLowUnit.BackColor = Color.White;
            btnUnitCivilCommon.BackColor = Color.FromArgb(183, 240, 177);
            SendCommand("@bd");
            IniFile.SetValue(iniPath, "����", "UnitCivil", "Unit");
        }

        private void btnForceCivilLowUnit_Click(object sender, EventArgs e)
        {
            btnForceCivilLowUnit.BackColor = Color.FromArgb(183, 240, 177);
            btnForceCivilUnit.BackColor = Color.White;
            btnForceCivilMineral.BackColor = Color.White;
            btnForceCivilGas.BackColor = Color.White;
            SendCommand("@rd");
            IniFile.SetValue(iniPath, "����", "ForceCivil", "LowUnit");
        }

        private void btnForceCivilUnit_Click(object sender, EventArgs e)
        {
            btnForceCivilLowUnit.BackColor = Color.White;
            btnForceCivilUnit.BackColor = Color.FromArgb(183, 240, 177);
            btnForceCivilMineral.BackColor = Color.White;
            btnForceCivilGas.BackColor = Color.White;
            SendCommand("@ru");
            IniFile.SetValue(iniPath, "����", "ForceCivil", "Unit");
        }

        private void btnForceCivilMineral_Click(object sender, EventArgs e)
        {
            btnForceCivilLowUnit.BackColor = Color.White;
            btnForceCivilUnit.BackColor = Color.White;
            btnForceCivilMineral.BackColor = Color.FromArgb(183, 240, 177);
            btnForceCivilGas.BackColor = Color.White;
            SendCommand("@rl");
            IniFile.SetValue(iniPath, "����", "ForceCivil", "Mineral");
        }

        private void btnForceCivilGas_Click(object sender, EventArgs e)
        {
            btnForceCivilLowUnit.BackColor = Color.White;
            btnForceCivilUnit.BackColor = Color.White;
            btnForceCivilMineral.BackColor = Color.White;
            btnForceCivilGas.BackColor = Color.FromArgb(183, 240, 177);
            SendCommand("@rr");
            IniFile.SetValue(iniPath, "����", "ForceCivil", "Gas");
        }

        private void btnAutoSaleOn_Click(object sender, EventArgs e)
        {
            btnAutoSaleOn.BackColor = Color.FromArgb(183, 240, 177);
            btnAutoSaleOff.BackColor = Color.White;
            SendCommand("@as on");
            IniFile.SetValue(iniPath, "����", "AutoSale", "On");
        }

        private void btnAutoSaleOff_Click(object sender, EventArgs e)
        {
            btnAutoSaleOn.BackColor = Color.White;
            btnAutoSaleOff.BackColor = Color.FromArgb(183, 240, 177);
            SendCommand("@as off");
            IniFile.SetValue(iniPath, "����", "AutoSale", "Off");
        }

        private void btnUnitSaleOn_1_Click(object sender, EventArgs e)
        {
            btnUnitSaleOn_1.BackColor = Color.FromArgb(183, 240, 177);
            btnUnitSaleOff_1.BackColor = Color.White;
            SendCommand("@s1 on");
            IniFile.SetValue(iniPath, "����", "UnitSale1", "On");
        }

        private void btnUnitSaleOff_1_Click(object sender, EventArgs e)
        {
            btnUnitSaleOn_1.BackColor = Color.White;
            btnUnitSaleOff_1.BackColor = Color.FromArgb(183, 240, 177);
            SendCommand("@s1 off");
            IniFile.SetValue(iniPath, "����", "UnitSale1", "Off");
        }

        private void btnUnitSaleOn_2_Click(object sender, EventArgs e)
        {
            btnUnitSaleOn_2.BackColor = Color.FromArgb(183, 240, 177);
            btnUnitSaleOff_2.BackColor = Color.White;
            SendCommand("@s2 on");
            IniFile.SetValue(iniPath, "����", "UnitSale2", "On");
        }

        private void btnUnitSaleOff_2_Click(object sender, EventArgs e)
        {
            btnUnitSaleOn_2.BackColor = Color.White;
            btnUnitSaleOff_2.BackColor = Color.FromArgb(183, 240, 177);
            SendCommand("@s2 off");
            IniFile.SetValue(iniPath, "����", "UnitSale2", "Off");
        }

        private void btnUnitSaleOn_3_Click(object sender, EventArgs e)
        {
            btnUnitSaleOn_3.BackColor = Color.FromArgb(183, 240, 177);
            btnUnitSaleOff_3.BackColor = Color.White;
            SendCommand("@s3 on");
            IniFile.SetValue(iniPath, "����", "UnitSale3", "On");
        }

        private void btnUnitSaleOff_3_Click(object sender, EventArgs e)
        {
            btnUnitSaleOn_3.BackColor = Color.White;
            btnUnitSaleOff_3.BackColor = Color.FromArgb(183, 240, 177);
            SendCommand("@s3 off");
            IniFile.SetValue(iniPath, "����", "UnitSale3", "Off");
        }

        private void btnUnitSaleOn_4_Click(object sender, EventArgs e)
        {
            btnUnitSaleOn_4.BackColor = Color.FromArgb(183, 240, 177);
            btnUnitSaleOff_4.BackColor = Color.White;
            SendCommand("@s4 on");
            IniFile.SetValue(iniPath, "����", "UnitSale4", "On");
        }

        private void btnUnitSaleOff_4_Click(object sender, EventArgs e)
        {
            btnUnitSaleOn_4.BackColor = Color.White;
            btnUnitSaleOff_4.BackColor = Color.FromArgb(183, 240, 177);
            SendCommand("@s4 off");
            IniFile.SetValue(iniPath, "����", "UnitSale4", "Off");
        }

        private void btnLifeAlarmOn_Click(object sender, EventArgs e)
        {
            btnLifeAlarmOn.BackColor = Color.FromArgb(183, 240, 177);
            btnLifeAlarmOff.BackColor = Color.White;
            SendCommand("@pd on");
            IniFile.SetValue(iniPath, "����", "Alarm", "On");
        }

        private void btnLifeAlarmOff_Click(object sender, EventArgs e)
        {
            btnLifeAlarmOn.BackColor = Color.White;
            btnLifeAlarmOff.BackColor = Color.FromArgb(183, 240, 177);
            SendCommand("@pd off");
            IniFile.SetValue(iniPath, "����", "Alarm", "Off");
        }

        private void btnAutoSortOn_Click(object sender, EventArgs e)
        {
            btnAutoSortOn.BackColor = Color.FromArgb(183, 240, 177);
            btnAutoSortOff.BackColor = Color.White;
            SendCommand("@a on");
            IniFile.SetValue(iniPath, "����", "AutoSort", "On");
        }

        private void btnAutoSortOff_Click(object sender, EventArgs e)
        {
            btnAutoSortOn.BackColor = Color.White;
            btnAutoSortOff.BackColor = Color.FromArgb(183, 240, 177);
            SendCommand("@a off");
            IniFile.SetValue(iniPath, "����", "AutoSort", "Off");
        }

        private void btnUnitSaveOn_Click(object sender, EventArgs e)
        {
            btnUnitSaveOn.BackColor = Color.FromArgb(183, 240, 177);
            btnUnitSaveOff.BackColor = Color.White;
            SendCommand("@us on");
            IniFile.SetValue(iniPath, "����", "UnitSave", "On");
        }

        private void btnUnitSaveOff_Click(object sender, EventArgs e)
        {
            btnUnitSaveOn.BackColor = Color.White;
            btnUnitSaveOff.BackColor = Color.FromArgb(183, 240, 177);
            SendCommand("@us off");
            IniFile.SetValue(iniPath, "����", "UnitSave", "Off");
        }

        private void btnCivilSaveOn_Click(object sender, EventArgs e)
        {
            btnCivilSaveOn.BackColor = Color.FromArgb(183, 240, 177);
            btnCivilSaveOff.BackColor = Color.White;
            SendCommand("@cs on");
            IniFile.SetValue(iniPath, "����", "CivilSave", "On");
        }

        private void btnCivilSaveOff_Click(object sender, EventArgs e)
        {
            btnCivilSaveOn.BackColor = Color.White;
            btnCivilSaveOff.BackColor = Color.FromArgb(183, 240, 177);
            SendCommand("@cs off");
            IniFile.SetValue(iniPath, "����", "CivilSave", "Of");
        }

        

        private void LoadSetting()
        {
            String allCivil = IniFile.GetValue(Form1.iniPath, "����", "AllCivil", "");
            if(allCivil.Equals("LowUnit")) btnAllCivilLowUnit.BackColor = Color.FromArgb(183, 240, 177);
            if(allCivil.Equals("Unit")) btnAllCivilUnit.BackColor = Color.FromArgb(183, 240, 177);
            if(allCivil.Equals("Mineral")) btnAllCivilMineral.BackColor = Color.FromArgb(183, 240, 177);
            if(allCivil.Equals("Gas")) btnAllCivilGas.BackColor = Color.FromArgb(183, 240, 177);

            String unitCivil = IniFile.GetValue(Form1.iniPath, "����", "UnitCivil", "");
            if (unitCivil.Equals("LowUnit")) btnUnitCivilLowUnit.BackColor = Color.FromArgb(183, 240, 177);
            if (unitCivil.Equals("Unit")) btnUnitCivilLowUnit.BackColor = Color.FromArgb(183, 240, 177);

            String forceCivil = IniFile.GetValue(Form1.iniPath, "����", "ForceCivil", "");
            if (forceCivil.Equals("LowUnit")) btnForceCivilLowUnit.BackColor = Color.FromArgb(183, 240, 177);
            if (forceCivil.Equals("Unit")) btnForceCivilUnit.BackColor = Color.FromArgb(183, 240, 177);
            if (forceCivil.Equals("Mineral")) btnForceCivilMineral.BackColor = Color.FromArgb(183, 240, 177);
            if (forceCivil.Equals("Gas")) btnForceCivilGas.BackColor = Color.FromArgb(183, 240, 177);

            String AutoSale = IniFile.GetValue(Form1.iniPath, "����", "AutoSale", "");
            if (AutoSale.Equals("On")) btnAutoSaleOn.BackColor = Color.FromArgb(183, 240, 177);
            if (AutoSale.Equals("Off")) btnAutoSaleOff.BackColor = Color.FromArgb(183, 240, 177);

            String UnitSale1 = IniFile.GetValue(Form1.iniPath, "����", "UnitSale1", "");
            if (UnitSale1.Equals("On")) btnUnitSaleOn_1.BackColor = Color.FromArgb(183, 240, 177);
            if (UnitSale1.Equals("Off")) btnUnitSaleOff_1.BackColor = Color.FromArgb(183, 240, 177);

            String UnitSale2 = IniFile.GetValue(Form1.iniPath, "����", "UnitSale2", "");
            if (UnitSale2.Equals("On")) btnUnitSaleOn_2.BackColor = Color.FromArgb(183, 240, 177);
            if (UnitSale2.Equals("Off")) btnUnitSaleOff_2.BackColor = Color.FromArgb(183, 240, 177);

            String UnitSale3 = IniFile.GetValue(Form1.iniPath, "����", "UnitSale3", "");
            if (UnitSale3.Equals("On")) btnUnitSaleOn_3.BackColor = Color.FromArgb(183, 240, 177);
            if (UnitSale3.Equals("Off")) btnUnitSaleOff_3.BackColor = Color.FromArgb(183, 240, 177);

            String UnitSale4 = IniFile.GetValue(Form1.iniPath, "����", "UnitSale4", "");
            if (UnitSale4.Equals("On")) btnUnitSaleOn_4.BackColor = Color.FromArgb(183, 240, 177);
            if (UnitSale4.Equals("Off")) btnUnitSaleOff_4.BackColor = Color.FromArgb(183, 240, 177);

            String alarm = IniFile.GetValue(Form1.iniPath, "����", "Alarm", "");
            if (alarm.Equals("On")) btnLifeAlarmOn.BackColor = Color.FromArgb(183, 240, 177);
            if (alarm.Equals("Off")) btnLifeAlarmOff.BackColor = Color.FromArgb(183, 240, 177);

            String autoSort = IniFile.GetValue(Form1.iniPath, "����", "UnitSale4", "");
            if (autoSort.Equals("On")) btnAutoSortOn.BackColor = Color.FromArgb(183, 240, 177);
            if (autoSort.Equals("Off")) btnAutoSortOff.BackColor = Color.FromArgb(183, 240, 177);

            String unitSave = IniFile.GetValue(Form1.iniPath, "����", "UnitSale4", "");
            if (unitSave.Equals("On")) btnUnitSaveOn.BackColor = Color.FromArgb(183, 240, 177);
            if (unitSave.Equals("Off")) btnUnitSaveOff.BackColor = Color.FromArgb(183, 240, 177);

            String civilSave = IniFile.GetValue(Form1.iniPath, "����", "UnitSale4", "");
            if (civilSave.Equals("On")) btnCivilSaveOn.BackColor = Color.FromArgb(183, 240, 177);
            if (civilSave.Equals("Off")) btnCivilSaveOff.BackColor = Color.FromArgb(183, 240, 177);
        }
    }
}