using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        private int[,] boardNum;
        private Label[,] boardLabel;
        private int score;
        private Label scoreLabel;
        private Boolean over;
        private Label overLabel;

        Random rand = new Random();
        int rx;
        int ry;

        private void CreateNewNum(int x, int y)
        {
            // create new 2
            boardNum[x, y] = 2;
            boardLabel[x, y].Text = boardNum[x, y].ToString();
        }

        public void ConnectComponents()
        {
            boardLabel = new Label[4, 4];

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    int index = 4 + i * 4 + j;
                    string name = "label" + index.ToString();

                    boardLabel[i, j] = this.Controls.Find(name, true).First() as Label;
                }
            }

            scoreLabel = new Label();
            overLabel = new Label();
            scoreLabel = this.Controls.Find("label3", true).First() as Label;
            overLabel = this.Controls.Find("label20", true).First() as Label;
        }

        public void InitGame()
        {
            boardNum = new int[4,4];

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    boardNum[i, j] = 0;
                    boardLabel[i, j].Text = "";
                }
            }
            
            rx = rand.Next() % 4;
            ry = rand.Next() % 4;
            CreateNewNum(rx, ry);
   
            score = 0;
            scoreLabel.Text = string.Format("{0}", score);
            over = false;
            overLabel.Visible = false;
        }


        public Form1()
        {
            InitializeComponent();
            ConnectComponents();

            InitGame();
        }

        private void SetBoardLabel()
        {
            for(int i=0;i<4;i++)
                for (int j = 0; j < 4; j++)
                {
                    if (boardNum[i, j] == 0) boardLabel[i, j].Text = "";
                    else boardLabel[i, j].Text = boardNum[i, j].ToString();
                }
            scoreLabel.Text = string.Format("{0}", score);
        }

        private void IsOver()
        {
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                {
                    if (boardNum[i, j] == 0)
                    {
                        over = false;
                        return;
                    }
                }

            over = true;
            overLabel.Visible = true;
        }

        // 조작
        private void UpClick(object sender, EventArgs e)
        {
            // 숫자들 전부위로
            for (int j = 0; j < 4; j++)
            {
                for (int k = 0; k < 3; k++)
                {
                    for (int i = 1; i < 4 - k; i++)
                    {
                        if (boardNum[i, j] == 0) continue;

                        // 합치기
                        if (boardNum[i, j] == boardNum[i - 1, j])
                        {
                            score += boardNum[i, j];
                            boardNum[i, j] = 0;
                            boardNum[i - 1, j] = boardNum[i - 1, j] * 2;
                        }
                        else if (boardNum[i - 1, j] == 0)
                        {
                            boardNum[i - 1, j] = boardNum[i, j];
                            boardNum[i, j] = 0;
                        }
                    }
                }
            }

            IsOver();
            if (over) return;

            while (true)
            {
                ry = rand.Next() % 4;
                if (boardNum[3, ry] == 0) break;
            }
            CreateNewNum(3, ry);

            SetBoardLabel();
        }

        private void DownClick(object sender, EventArgs e)
        {
            for (int j = 0; j < 4; j++)
            {
                for (int k = 0; k < 3; k++)
                {
                    for (int i = 2; i >= k; i--)
                    {
                        if (boardNum[i, j] == 0) continue;

                        // 합치기
                        if (boardNum[i, j] == boardNum[i + 1, j])
                        {
                            score += boardNum[i, j];
                            boardNum[i, j] = 0;
                            boardNum[i + 1, j] = boardNum[i + 1, j] * 2;
                        }
                        else if (boardNum[i+1, j] == 0)
                        {
                            boardNum[i + 1, j] = boardNum[i, j];
                            boardNum[i, j] = 0;
                        }
                    }
                }
            }

            IsOver();
            if (over) return;

            while (true)
            {
                ry = rand.Next() % 4;
                if (boardNum[0, ry] == 0) break;
            }
            CreateNewNum(0, ry);

            SetBoardLabel();
        }

        private void LeftClick(object sender, EventArgs e)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int k = 0; k < 3; k++)
                {
                    for (int j = 1; j < 4-k; j++)
                    {
                         if (boardNum[i, j] == 0) continue;

                        // 합치기
                        if (boardNum[i, j] == boardNum[i, j - 1])
                        {
                            score += boardNum[i, j];
                            boardNum[i, j] = 0;
                            boardNum[i, j - 1] = boardNum[i, j - 1] * 2;
                        }
                        else if (boardNum[i, j - 1] == 0)
                        {
                            boardNum[i, j - 1] = boardNum[i, j];
                            boardNum[i, j] = 0;
                        }
                    }
                }
            }

            IsOver();
            if (over) return;

            while (true)
            {
                rx = rand.Next() % 4;
                if (boardNum[rx, 3] == 0) break;
            }
            CreateNewNum(rx, 3);

            SetBoardLabel();
        }

        private void RightClick(object sender, EventArgs e)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int k = 0; k < 3; k++)
                {
                    for (int j = 2; j >= k; j--)
                    {
                        if (boardNum[i, j] == 0) continue;

                        // 합치기
                        if (boardNum[i, j] == boardNum[i, j + 1])
                        {
                            score += boardNum[i, j];
                            boardNum[i, j] = 0;
                            boardNum[i, j + 1] = boardNum[i, j + 1] * 2;
                        }
                        // 밀기
                        else if (boardNum[i, j + 1] == 0)
                        {
                            boardNum[i, j + 1] = boardNum[i, j];
                            boardNum[i, j] = 0;
                        }
                    }
                }
            }

            IsOver();
            if (over) return;

            while(true)
            {
                rx = rand.Next()%4;
                if (boardNum[rx, 0] == 0) break;
            }
            CreateNewNum(rx, 0);

            SetBoardLabel();
        }

        private void label20_Click(object sender, EventArgs e)
        {
            InitGame();
        }
    }
}
