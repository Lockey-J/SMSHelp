using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSHelp
{
    class AiMaSMS : ISMSInterface
    {
        public string Token { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string ErrMsg { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool AddBlackPhone(string id, string phone)
        {
            throw new NotImplementedException();
        }

        public bool FreePhone(string id, string phone)
        {
            throw new NotImplementedException();
        }

        public string GetPhone(string id, string ISP, string area, int card, string phone = null, int loop = 1)
        {
            throw new NotImplementedException();
        }

        public string GetPhoneMsg(string id, string phone, string author)
        {
            throw new NotImplementedException();
        }

        public bool GetUserBalance(out List<string> ResultInfo)
        {
            throw new NotImplementedException();
        }

        public bool Login(string username, string password)
        {
            throw new NotImplementedException();
        }

        public bool SendMsg(string id, string phone, string sendPhone, string content)
        {
            throw new NotImplementedException();
        }
    }
}
