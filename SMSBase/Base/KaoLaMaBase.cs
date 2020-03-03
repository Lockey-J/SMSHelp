using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMSBase.Interface;
using HttpNetHelper;
using System.Drawing;

namespace SMSBase.Base
{
    public class KaoLaMaBase : ISMSInterface
    {
        private readonly HttpItem mHttpItem;
        private readonly HttpHelper Http;
        private readonly string ApiHost = "http://api.kaolama.com";
        public KaoLaMaBase()
        {
            mHttpItem = new HttpItem()
            {
                Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3",
                Method = "GET",
                ProxyIp = "ieproxy",
                Encoding = Encoding.UTF8,
                AutoRedirectCookie = true,
                Allowautoredirect = true
            };
            Http = new HttpHelper();
        }
        /// <summary>
        /// 登录Cookie
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 错误消息
        /// </summary>
        public string ErrMsg { get; set; }

        /// <summary>
        /// 开发者账号
        /// </summary>
        public string Developer { get; set; } = "";

        /// <summary>
        /// 加黑手机号码
        /// </summary>
        /// <param name="id">项目Id</param>
        /// <param name="phone">手机号码</param>
        /// <returns>是否成功</returns>
        public bool AddBlackPhone(string id, string phone)
        {
            if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(phone))
            {
                ErrMsg = "项目ID不能为空或接收手机号为空";

                return false;
            }

            mHttpItem.URL = ApiHost + $"/api/addBlacklist?sid={id}&phone={phone}&token={Token}";

            List<string> ResultHtml = Http.GetHtml(mHttpItem).Html.Split('|').ToList();
            try
            {

                if (ResultHtml[0].Trim().Equals("1"))
                {
                    ErrMsg = ResultHtml[1].Trim();

                    return false;
                }
                else
                {

                    return true;
                }
            }
            catch
            {

                ErrMsg = "函数AddBlackPhone出错";

                return false;
            }
        }

        /// <summary>
        /// 释放手机号
        /// </summary>
        /// <param name="id">项目Id</param>
        /// <param name="phone">手机号码</param>
        /// <returns>是否成功</returns>
        public bool FreePhone(string id, string phone)
        {
            if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(phone))
            {
                ErrMsg = "项目ID不能为空或接收手机号为空";

                return false;
            }

            mHttpItem.URL = ApiHost + $"/api/cancelRecv?sid={id}&phone={phone}&token={Token}";

            List<string> ResultHtml = Http.GetHtml(mHttpItem).Html.Split('|').ToList();
            try
            {

                if (ResultHtml[0].Trim().Equals("1"))
                {
                    ErrMsg = ResultHtml[1].Trim();

                    return false;
                }
                else
                {

                    return true;
                }
            }
            catch
            {

                ErrMsg = "函数AddBlackPhone出错";

                return false;
            }
        }

        public bool GetPayImg(int PayCount, out Image PayImg)
        {
            PayImg = null;
            return false;
        }

        /// <summary>
        /// 获取手机号码
        /// </summary>
        /// <param name="id">项目ID</param>
        /// <param name="ISP">运营商</param>
        /// <param name="area">地区</param>
        /// <param name="card">获取指定运营商选项，0:不限运营商，1:包含运营商，2:不含运营商(默认值0)</param>
        /// <param name="phone">指定手机号</param>
        /// <param name="loop">过滤已做过号码 过滤：1 不过滤：2</param>
        /// <returns>返回手机号码</returns>
        public bool GetPhone(string id, out List<string> Result, string ISP = "0", string area = "0", int card = 0, string phone = null, int loop = 1)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                ErrMsg = "项目ID不能为空";
                Result = null;
                return false;
            }
            
            mHttpItem.URL = ApiHost + $"/api/getPhone?sid={id}&operatorChoice={card}&operator={ISP}&areaChoice=1&areaType=1&areaKeyword={area}&token={Token}&developer={Developer}";

            List<string> ResultHtml = Http.GetHtml(mHttpItem).Html.Split('|').ToList();
            try
            {

                if (ResultHtml[0].Trim().Equals("1"))
                {
                    ErrMsg = ResultHtml[1].Trim();
                    Result = null;
                    return false;
                }
                else
                {
                    ResultHtml.Remove(ResultHtml[0]);
                    Result = ResultHtml;
                    return true;
                }
            }
            catch
            {

                ErrMsg = "函数GetPhone出错";
                Result = null;
                return false;
            }
        }
        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <param name="id">项目Id</param>
        /// <param name="phone">手机号码</param>       
        /// <returns>短信内容</returns>
        public bool GetPhoneMsg(string id, string phone, out string Result)
        {
            if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(phone))
            {
                ErrMsg = "项目ID不能为空或接收手机号为空";
                Result = string.Empty;
                return false;
            }

            mHttpItem.URL = ApiHost + $"/api/getMessage?sid={id}&phone={phone}&token={Token}";

            List<string> ResultHtml = Http.GetHtml(mHttpItem).Html.Split('|').ToList();
            try
            {

                if (ResultHtml[0].Trim().Equals("1"))
                {
                    ErrMsg = ResultHtml[1].Trim();
                    Result = string.Empty;
                    return false;
                }
                else
                {
                    Result = ResultHtml[2].Trim();
                    return true;
                }
            }
            catch
            {

                ErrMsg = "函数GetPhoneMsg出错";
                Result = string.Empty;
                return false;
            }
        }

        /// <summary>
        /// 查询账号余额
        /// </summary>
        /// <param name="ResultInfo">查询结果</param>
        /// <returns>查询是否成功</returns>
        public bool GetUserBalance(out List<string> ResultInfo)
        {
            if (string.IsNullOrWhiteSpace(Token))
            {
                ResultInfo = null;
                ErrMsg = "未登录账户";
                return false;
            }
            // string mUrl = string.Format(ApiHost + "/api/yh_gx/token={0}",Token);
            mHttpItem.URL = ApiHost + $"/api/getSummary?token={Token}";
            List<string> ResultHtml = Http.GetHtml(mHttpItem).Html.Split('|').ToList();
            try
            {
                if (!ResultHtml[0].Trim().Equals("1"))
                {
                    ResultHtml.Remove(ResultHtml[0]);
                    ResultInfo = ResultHtml;
                    return true;
                }
                else
                {
                    ResultInfo = null;
                    ErrMsg = ResultHtml[1].Trim();
                    return false;
                }
            }
            catch
            {
                ResultInfo = null;
                ErrMsg = "函数GetUserBalance出错";
                return false;
            }

        }
        /// <summary>
        /// 登录账户
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public bool Login(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                ErrMsg = "用户名或密码为空";
                return false;
            }

            mHttpItem.URL = ApiHost + $"/api/login?userName={username}&password={password}";

            List<string> ResultHtml = Http.GetHtml(mHttpItem).Html.Split(new char[]{'|'},StringSplitOptions.RemoveEmptyEntries).ToList();

            try
            {
                if (ResultHtml[0].Trim().Equals("1"))
                {
                    ErrMsg = ResultHtml[1].Trim();
                    return false;
                }
                else
                {
                    Token = ResultHtml[2].Trim();
                    return true;
                }
            }
            catch
            {
                ErrMsg = "函数Login出错";
                return false;
            }

        }
        /// <summary>
        /// 不支持
        /// </summary>
        /// <param name="id">项目Id</param>
        /// <param name="phone">手机号码</param>
        /// <param name="sendPhone">发送号码</param>
        /// <param name="content">发送内容</param>
        /// <returns>是否成功</returns>
        public bool SendMsg(string id, string phone, string sendPhone, string content)
        {
            return false;
        }
    }
}
