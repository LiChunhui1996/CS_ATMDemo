//开发人员：李纯辉   2016118162
//开发时间：2018年11月10日
//此模块功能介绍：ATM机界面和操作的主要界面
//申明：此文段里面的服务器信息需要被保密，以免造成损失，谢谢配合。

using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Practice_2_ATM
{
    public partial class Form1 : Form
    {
        //定义的全局变量
        Int64 bankid;
        Int64 bankid_2;
        Account account1;
        Account account2;
        int inputcount = 0;
        int getmoney = 0;
        int recharger = 0;

        public Form1()
        {
            InitializeComponent();
            //进入程序是需要将要现实的界面显示出来
            panel1.Show();
            panel2.Hide();
            panel3.Hide();
            panel4.Hide();
            panel5.Hide();
            panel6.Hide();
            panel9.Hide();
        }

//Panel1控件      空闲界面 
        private void button6_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                try
                {
                    bankid = Convert.ToInt64(textBox1.Text);
                    label1.Text = "";
                    textBox1.Text = "";
                }
                catch
                {
                    textBox1.BackColor = Color.Red;
                    label1.Text = "您只能输入数字！";
                }

                Console.WriteLine(bankid);
                if (Account.HasAccount(bankid.ToString()))
                {
                    account1 = new Account(bankid.ToString());
                    Console.WriteLine("余额：" + account1.balance.ToString());
                    Console.WriteLine("密码：" + account1.password.ToString());
                    panel7.Visible = false;
                    panel8.Visible = true;
                }
                else
                {
                    Console.WriteLine("您输入的账户不存在！");
                    label13.Text = "您输入的账户不存在！";
                }
            }
            else
            {
                Console.WriteLine("请输入账户账号！");
            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            inputcount++;
            label2.Text = "";
            if (inputcount >= 3)
            {
                label2.Text = "已吞卡，请联系本人携带有效身份证件来领取！";
                label2.Text = "";
                InitPanel1(sender, e);
            }
            else
            {
                label2.Text = "第" + (inputcount) + "次输入密码！";
                int password = -1;
                if (textBox2.Text != "")
                {
                    try
                    {
                        password = Convert.ToInt32(textBox2.Text);
                        label1.Text = "";
                    }
                    catch
                    {
                        textBox2.BackColor = Color.Red;
                        label1.Text = "您只能输入数字！";
                    }

                    if (password == Convert.ToInt32(account1.password))
                    {
                        Console.WriteLine("密码正确！");
                        ShowPanel(sender, e, 2);
                    }
                    else
                    {
                        label1.Text = "您输入的密码错误！";
                        Console.WriteLine("密码错误！");
                    }
                }
                else
                {
                    Console.WriteLine("请输入密码！");
                }
            }

        }

//Pannel2的控件        功能选择界面
        //查询余额
        private void button1_Click(object sender, EventArgs e)
        {
            ShowPanel(sender, e, 3);
            label3.Text = "账户余额：" + account1.balance + " 元";
        }

        //转账
        private void button2_Click(object sender, EventArgs e)
        {
            ShowPanel(sender, e, 6);
        }

        //充值
        private void button3_Click(object sender, EventArgs e)
        {
            ShowPanel(sender, e, 5);
        }

        //取款
        private void button4_Click(object sender, EventArgs e)
        {
            ShowPanel(sender, e, 4);
        }

        //退出
        private void button5_Click(object sender, EventArgs e) {
            BackCard(sender, e);
        }

 

//Panel3控件     显示余额界面
        private void button8_Click(object sender, EventArgs e)
        {
            //ShowPanel(sender, e, 2);
            BackCard(sender,e);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            ShowPanel(sender, e, 2);
        }

 //Panel4控件        取款界面
        //100元
        private void button10_Click(object sender, EventArgs e)
        {
            getmoney = 100;
            textBox3.Text = getmoney.ToString();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            getmoney = 200;
            textBox3.Text = getmoney.ToString();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            getmoney = 500;
            textBox3.Text = getmoney.ToString();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            getmoney = 1000;
            textBox3.Text = getmoney.ToString();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            getmoney = 2000;
            textBox3.Text = getmoney.ToString();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            ShowPanel(sender, e, 2);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            if (textBox3.Text != "")
            {
                getmoney = Convert.ToInt32(textBox3.Text);
                int balance = Convert.ToInt32(account1.balance);
                balance = balance - getmoney;
                if (balance > 0)
                {
                    Account.GetMoney(balance.ToString(), bankid.ToString());
                    account1 = new Account(bankid.ToString());
                    Console.WriteLine("balance is " + account1.balance);
                    ShowPanel(sender, e, 9);
                }
                else
                {
                    textBox3.Text = "金额大于余额！";
                }
            }
            else
            {

            }
        }

        //Panel5控件      缴费界面
        private void button17_Click(object sender, EventArgs e)
        {
            recharger = 20; ShowRechargeInfo(sender,e,20);
        }

        private void button18_Click(object sender, EventArgs e)
        {
            recharger = 50; ShowRechargeInfo(sender, e, 50);
        }

        private void button19_Click(object sender, EventArgs e)
        {
            recharger = 100; ShowRechargeInfo(sender, e, 100);
        }

        private void button20_Click(object sender, EventArgs e)
        {
            recharger = 200; ShowRechargeInfo(sender, e, 200);
        }

        private void button21_Click(object sender, EventArgs e)
        {
            recharger = 500; ShowRechargeInfo(sender, e, 500);
        }

        private void ShowRechargeInfo(Object sender,EventArgs e,int amount)
        {
            if (textBox4.Text == "")
            {
                label7.Text = "为" + account1.telphone + " 充值" + recharger + "元 ?";
            }
            else
            {
                long telphone=0;
                try
                {
                    telphone = Convert.ToInt64(textBox4.Text);
                    label7.Text = "为" + telphone + " 充值" + recharger + "元 ?";
                }
                catch
                {
                    textBox4.BackColor = Color.Red;
                    label7.Text = "请检查手机号码！";
                }

            }
        }

        private void button22_Click(object sender, EventArgs e)
        {
            if (textBox4.Text == "")
            {
                int balance = Convert.ToInt32(account1.balance);
                balance = balance - recharger;
                if (balance > 0)
                {
                    Account.GetMoney(balance.ToString(), bankid.ToString());
                    account1 = new Account(bankid.ToString());
                    Console.WriteLine("balance is " + account1.balance);
                    ShowPanel(sender, e, 9);
                }
                else
                {
                    label7.Text = "金额大于余额！";
                }
            }
            else
            {

            }
        }

        private void button23_Click(object sender, EventArgs e)
        {
            ShowPanel(sender, e, 2);
        }

 //Panel6控件         转账界面
        private void button24_Click(object sender, EventArgs e)
        {
            ShowPanel(sender, e, 2);
        }

        private void button25_Click(object sender, EventArgs e)
        {
            if (textBox5.Text != "")
            {
                try
                {
                    bankid_2 = Convert.ToInt64(textBox5.Text);
                    account2 = new Account(bankid_2.ToString());
                    if (!Account.HasAccount(bankid_2.ToString()))
                    {
                        label8.Text = "您输入的卡号不是本行账号！";
                    }
                    else
                    {
                        try
                        {
                            int amount = Convert.ToInt32(textBox6.Text);
                            int balance_1 = Convert.ToInt32(account1.balance);
                            int balance_2 = Convert.ToInt32(account2.balance);

                            balance_1 = balance_1 - amount;
                            balance_2 = balance_2 + amount;
                            Account.GetMoney(balance_1.ToString(), bankid.ToString());
                            Account.GetMoney(balance_2.ToString(), bankid_2.ToString());
                            account1 = new Account(bankid.ToString());
                            account2 = new Account(bankid_2.ToString());
                            Console.WriteLine(account1.balance + "        " + account2.balance);
                            ShowPanel(sender, e, 9);
                        }
                        catch
                        {
                            textBox6.BackColor = Color.Red;
                            label12.Text = "请检查所输入的金额！";
                        }
                    }
                }
                catch
                {
                    textBox5.BackColor = Color.Red;
                    label8.Text = "请检查您输入的卡号！";
                }
            }
            else
            {
                textBox5.Text = "请输入对方卡号！";
                textBox5.BackColor = Color.Red;
                Console.WriteLine("请输入对方卡号！");
            }
        }



//被引用函数部分
        private void BackCard(object sender, EventArgs e)
        {
            InitPanel1(sender, e);
            //ShowPanel(sender, e, 1);
        }
        private void InitPanel1(object sender,EventArgs e)
        {
            inputcount = 0;
            ShowPanel(sender, e, 1);
            panel7.Visible = true;
            panel8.Visible = false;
            textBox1.Text = "";
            textBox2.Text = "";

        }

        //以下是界面切换的控制函数
        public void ShowPanel(object sender,EventArgs e,int i)
        {
            
            switch (i)
            {                
             case 1:
                    textBox1.BackColor = Color.White;
                    panel1.Show(); panel2.Hide();panel3.Hide();panel4.Hide();panel5.Hide();panel6.Hide();panel9.Hide();
                    break;
                case 2:
                    panel2.Show(); panel1.Hide(); panel3.Hide(); panel4.Hide(); panel5.Hide(); panel6.Hide(); panel9.Hide();
                    label9.Text = "欢迎您" + account1.name + "，请选择服务项目：";
                    break;
                case 3:
                    panel3.Show(); panel2.Hide(); panel1.Hide(); panel4.Hide(); panel5.Hide(); panel6.Hide(); panel9.Hide();
                    break;
                case 4:
                    panel4.Show(); panel2.Hide(); panel3.Hide(); panel1.Hide(); panel5.Hide(); panel6.Hide(); panel9.Hide();
                    break;
                case 5:
                    panel5.Show(); panel2.Hide(); panel3.Hide(); panel4.Hide(); panel1.Hide(); panel6.Hide(); panel9.Hide();
                    break;
                case 6:
                    panel6.Show(); panel2.Hide(); panel3.Hide(); panel4.Hide(); panel5.Hide(); panel1.Hide(); panel9.Hide();
                    break;
                case 9:
                    panel9.Show(); panel2.Hide(); panel3.Hide(); panel4.Hide(); panel5.Hide(); panel6.Hide(); panel1.Hide();
                    Panel9Show(sender, e);
                    break;
            }
        }

        private void Panel9Show(object sender, EventArgs e)
        {
            //交易已经完成，正在跳转！
            Thread.Sleep(3000);
            ShowPanel(sender, e, 2);
        }

    }
}
