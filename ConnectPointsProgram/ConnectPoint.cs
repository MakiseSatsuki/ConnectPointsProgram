using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConnectPointsProgram
{
    public partial class ConnectPoint : Form
    {
        public ConnectPoint()
        {
            InitializeComponent();
        }

        [System.Runtime.InteropServices.DllImport("user32")]
        private static extern int mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        const int MOUSEEVENTF_LEFTDOWN = 0x0002; //模拟鼠标左键按下

        const int MOUSEEVENTF_LEFTUP = 0x0004; //模拟鼠标左键抬起

        int StopPoints = 0;

        private void ReadyForEndPoints(object sender, EventArgs e)
        {
            StopPoints = 1;

        }

        int num;//点击次数
        int stime;//间隔时间
        private void ReadyForStartPoints(object sender, EventArgs e)
        {
            //获取点击次数
            if (HitCountTxt.Text == null || HitCountTxt.Text == "")
            {
                num = 100;
            }
            else
                num = int.Parse(HitCountTxt.Text);

            //获取点击时间间隔
            if (HitGapTxt.Text == null || HitGapTxt.Text == "")
            {
                stime = 3000;
            }
            else
                stime = int.Parse(HitGapTxt.Text);
            ThreadStart childref = new ThreadStart(TrueStartPoints);
            Thread childThread = new Thread(childref);
            MessageBox.Show("5秒后开始进行 \n " +
                "间隔" + stime / 1000 + "\n" +
                " 共 " + num + "次的连击"
                );
            childThread.Start();
        }

        private void TrueStartPoints()
        {
            StopPoints = 0;
            Thread.Sleep(5000);
            //Thread.Sleep(2000);

            for (int i = 0; i < num; i++)
            {
                mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                Thread.Sleep(stime);

                if (StopPoints == 1)
                {
                    StopPoints = 0;
                    MessageBox.Show("已暂停");
                    return;
                }
            }
        }



    }
}
