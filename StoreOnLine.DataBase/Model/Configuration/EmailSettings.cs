using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreOnLine.DataBase.Entities;

namespace StoreOnLine.DataBase.Model.Configuration
{
    public class EmailSettings:DataBaseId
    {
        public string MailToAddress = "curichpedro@gmail.com";
        public string MailFromAddress = "StoreOnLine@CurichStore.com";
        public bool UseSsl = true;
        public string Username = "curichpedro";
        public string Password = "RADIKAL123";
        public string ServerName = "smtp.gmail.com";
        public int ServerPort = 587;
        public bool WriteAsFile = false;
        public string FileLocation = @"c:\Store_OnLine_Emails";
    }
}
