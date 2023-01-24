using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC2
{
    internal class Setting
    {
        public void Set()
        {
            SetAllCivil();
        }

        public void SetAllCivil()
        {
            String allCivil = IniFile.GetValue(Form1.iniPath, "설정", "AllCivil", null);
            MessageBox.Show(allCivil);
        }
    }
}
