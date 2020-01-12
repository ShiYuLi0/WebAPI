using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShiYuDB.Models;

namespace WebShiYuDB.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "世昱呀~";
            return View();
        }
        //获取用户ID、密码、用户名、以及头像信息
        public ActionResult GetData()
        {
            List<User> users = new List<User>();
            string sqlselect = " SELECT [ID],[UserPassword],[UserName],[picture],[RegisterTime] ,[SpecialMark] FROM [ShiYu].[dbo].[TBUser]";
            DataTable dataTable = SQLHelper.GetDataTable(sqlselect);
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {  
                User s = new User();
                s.ID = (int)dataTable.Rows[i]["ID"];
                s.UserPassword = dataTable.Rows[i]["UserPassword"].ToString();
                s.UserName = dataTable.Rows[i]["UserName"].ToString();
                s.picture = dataTable.Rows[i]["picture"].ToString();
                s.time = dataTable.Rows[i]["RegisterTime"].ToString();
                s.SpecialMark = dataTable.Rows[i]["SpecialMark"].ToString();
                users.Add(s);
            }   
            return Json(users,JsonRequestBehavior.AllowGet);
        }
        //新用户注册
        public string Intsert(string username, string password1,string password2,int picture)
        {
            string result = "HHHHHHHHHHHHHHHHHHHHHHHHH注册失败~";
            if (password1 != password2)
            {
                result="密码不一致，请重新输入";
            }
            else
            {
                //判断用户名是否存在
                string sqlcountusername = "SELECT count(UserName) FROM [ShiYu].[dbo].[TBUser] where UserName='" + username + "'";
                DataTable dtusercount = SQLHelper.GetDataTable(sqlcountusername);
                if (dtusercount.Rows[0][0].Equals(0))
                {
                    string sqlnewuser = "USE [ShiYu] INSERT INTO [dbo].[TBUser] ([UserPassword],[UserName],[picture]) VALUES('" + password1 + "','" + username + "','"+picture+"') ";
                    if (username == "" || password1 == "")
                    {
                        result="用户名或密码不能为空";
                    }
                    else
                    {
                        int i = SQLHelper.CommandSQL(sqlnewuser);
                        if (i == 0)
                        {
                           result="注册失败，服务器错误";
                        }
                        else
                        {
                            result = "注册成功,当前头像为" + picture;
                        }
                    }
                }
                else
                {
                    result="用户名已存在";
                }
            }
            return result;
        }
        //登录
        public bool Login(string username,string password)
        {
            bool ss;
            string sqlfindusercount = "SELECT count([ID])FROM [ShiYu].[dbo].[TBUser] where UserName='" + username + "'" + " AND UserPassword='" + password + "';";
            DataTable dtusercount = SQLHelper.GetDataTable(sqlfindusercount);
            if (dtusercount.Rows[0][0].Equals(1))
            {
                ss = true;
            }
            else
            {
                ss = false;
            }
            return ss;
        }
        //查询当前用户的信息
        public ActionResult userinfo(string username)
        {
            string sqluserinfo = "USE [ShiYu] SELECT [ID] ,[UserPassword] ,[UserName] ,[picture] ,[RegisterTime] ,[SpecialMark]  FROM [dbo].[TBUser]where UserName='"+username+"'";
            DataTable dataTable = SQLHelper.GetDataTable(sqluserinfo);
            User userinfo = new User();
            userinfo.ID = (int)dataTable.Rows[0]["ID"];
            userinfo.UserPassword = dataTable.Rows[0]["UserPassword"].ToString();
            userinfo.UserName = dataTable.Rows[0]["UserName"].ToString();
            userinfo.picture = dataTable.Rows[0]["picture"].ToString();
            userinfo.time = dataTable.Rows[0]["RegisterTime"].ToString();
            userinfo.SpecialMark = dataTable.Rows[0]["SpecialMark"].ToString();
            return Json(userinfo,JsonRequestBehavior.AllowGet);
        }
    }
}
