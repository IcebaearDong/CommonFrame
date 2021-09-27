using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UISysyem;
using UnityEngine;

namespace Common
{
    public class LoginData
    {
        public byte result;
        public string account;
        public string pw;
        public LoginData(string account, string pw, byte result)
        {
            this.account = account;
            this.pw = pw;
            this.result = result;
        }
    }

    public class RegisterData
    {
        public string account;
        public string pw;
        public int result;
        public RegisterData(string account, string pw, int result)
        {
            this.account = account;
            this.pw = pw;
            this.result = result;
        }
    }
}
