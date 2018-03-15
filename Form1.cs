using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 打地鼠
{
    public partial class Form1 : Form
    {
        Image ii  = 打地鼠.Properties.Resources.face;
        Image ii2 = 打地鼠.Properties.Resources.boom;
        Label[] lb = new Label[9];

        int score;
        int time;
        bool _GameStar = false;

        public Form1()
        {
            InitializeComponent();




            for (int i = 0; i < lb.Length; i++)
            {
                lb[i] = new Label();
                lb[i].Text = "";
                lb[i].Name = "M" + i;
                lb[i].Tag = 0;
                switch (i)
                {
                    case 0:
                    case 1:
                    case 2:
                    lb[i].Location = new Point(50 + 150 * i, 50);
                        break;
                    case 3:
                    case 4:
                    case 5:
                        lb[i].Location = new Point(50 + 150 * (i % 3), 200);
                        break;
                    case 6:
                    case 7:
                    case 8:
                        lb[i].Location = new Point(50 + 150 * (i % 3), 350);
                        break;
                }
                lb[i].ImageAlign = ContentAlignment.MiddleCenter;

                lb[i].Click += Lb_Click;
                lb[i].Image = ii;
                lb[i].Size = ii.Size;
                
            }

            foreach (var x in lb)
            {
                this.Controls.Add(x);
            }
           
        }

        private void Lb_Click(object sender, EventArgs e)
        {
            
            Label lb = sender as Label;
            if (lb.Tag.ToString() == "0" && _GameStar == true)
            {
                //音效
                SoundPlayer sp = new SoundPlayer(打地鼠.Properties.Resources.Bee);
                sp.Play();

                //加分
                score++;
                label1.Text = "分數 : " + score;
                //圖案改變
                lb.Image = ii2;

                lb.Tag = 1;
            }

        }
        
        private void GameStar(object sender, EventArgs e)
        {
            time = 30;
            score = 0;
            label1.Text = "分數: "+0;

            _GameStar = true;
            if (_GameStar)
            {
                timer1.Start();
                timer2.Start();
            }
          
            
        }

        
        private void timer1_Tick(object sender, EventArgs e)
        {
            foreach (var x in lb)
            {

                //把地鼠隱藏
                x.Visible = false;

            }
            Random random = new Random();

            //多隻 => 重複出現
            for (int k = 1; k <= 4; k++)
            {
                //隨機選取一隻出現
                
                int n = random.Next(8)+1;
                lb[n].Visible = true;

                //恢復原狀(圖片與點擊狀態)
                lb[n].Image = ii;
                lb[n].Tag = 0;
            }
            timer1.Interval = 500 + new Random().Next(1000);
            
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            time--;
            progressBar1.Value = time;

            label2.Text = time.ToString();

            if (time == 0)
            {
                timer1.Stop();
                timer2.Stop();
                MessageBox.Show("遊戲結束!");
            }
        }
    }
}
