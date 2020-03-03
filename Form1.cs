using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LockHelper;
using SMSBase.Base;
namespace SMSHelp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {                       
            var clsaima = new KaoLaMaBase();

            Console.WriteLine(clsaima.Login("",""));
            Console.WriteLine(clsaima.Token);
            Console.WriteLine(clsaima.GetPhoneMsg("21472","13556489648",out string aa));
            Console.WriteLine(clsaima.GetPayImg(10, out Image twm));
            pictureBox1.Image = twm;
            Console.WriteLine(clsaima.ErrMsg);
            using (UsingLock.UsLock().Read())
            {

            }
        }
    }
}
