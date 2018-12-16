//开发人员：李纯辉   2016118162
//开发时间：2018年11月10日
//此模块功能介绍：主要用来接受和处理ATM和数据库之间的联系
//申明：此文段里面的服务器信息需要被保密，以免造成损失，谢谢配合。

using System;
using MySql.Data.MySqlClient;

namespace Practice_2_ATM
{
    class Account
    {
        //此处申明的变量和数据库的表对应。
        public string bankid;
        public string name;
        public string age;
        public string telphone;
        public string balance;
        public string password;
        //连接语句
        public static string connetString = "server=119.29.90.13;port=3333;user=root;password=123456; database=bank;";


        //用来创建一个账户对象
        public Account(string id)
        {

            MySqlConnection connnect = new MySqlConnection(connetString);
            try {
                connnect.Open();Console.WriteLine("已经建立连接");
                string sql = " select * from bank.account " + " where bankid= '" + id + "';";
                Console.WriteLine(sql);
                MySqlCommand cmd = new MySqlCommand(sql, connnect);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read()) {
                    bankid = reader.GetString("bankid").ToString();
                    name = reader.GetString("name").ToString();
                    age = reader.GetInt32("age").ToString();
                    telphone = reader.GetString("telphone").ToString();
                    balance = reader.GetString("balance").ToString();
                    password = reader.GetString("password").ToString();
                    Console.WriteLine(reader.GetString("name") + reader.GetString("age"));
                }
                connnect.Close();
            }catch (MySqlException ex) {Console.WriteLine(ex.Message); }
            finally{connnect.Close();}
        }

        //通过扫描数据库判断输入的ID是不是存在
        public static bool HasAccount(string id)
        {
            bool hasAcoount = false;
            MySqlConnection connnect = new MySqlConnection(connetString);
            try
            {
                connnect.Open();
                Console.WriteLine("已经建立连接,正在检查账户是否存在！");

                string sql = "select * from bank.account ";
                sql = sql + " where bankid= '" + id + "';";
                MySqlCommand cmd = new MySqlCommand(sql, connnect);
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    hasAcoount = true;
                    Console.WriteLine("账户存在！");
                }

                connnect.Close();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connnect.Close();
            }
            return hasAcoount;
        }

        //对数据库的数据（Balance）进行操作
        public static void GetMoney(string balance, string id)
        {
            MySqlConnection connnect = new MySqlConnection(connetString);
            try
            {
                connnect.Open();
                Console.WriteLine("已经建立连接");
                string sql = "UPDATE bank.account SET balance = '" + balance + "' WHERE bankid = '"+id+"';";
                Console.WriteLine(sql);
                MySqlCommand cmd = new MySqlCommand(sql, connnect);
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connnect.Close();
            }
        }
    }
}
