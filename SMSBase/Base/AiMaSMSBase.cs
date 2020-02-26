using SMSBase.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HttpNetHelper;
using System.Drawing;

namespace SMSBase.Base
{
    /// <summary>
    /// 爱码验证码平台
    /// </summary>
    public class AiMaSMSBase : ISMSInterface
    {
        private readonly HttpItem mHttpItem;
        private readonly HttpHelper Http;
        private readonly string ApiHost = "http://115.231.8.180:8000";
        private readonly string ApiHostPro = "http://115.231.8.180:";
        public AiMaSMSBase()
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
        public string Developer { get; set; }

        /// <summary>
        /// 加黑手机号码
        /// </summary>
        /// <param name="id">项目Id</param>
        /// <param name="phone">手机号码</param>
        /// <returns>是否成功</returns>
        public bool AddBlackPhone(string id, string phone)
        {
            if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(phone) )
            {
                ErrMsg = "项目ID不能为空或接收手机号为空";

                return false;
            }

            mHttpItem.URL = ApiHost + $"/api/yh_lh/id={id}&phone={phone}&token={Token}";

            List<string> ResultHtml = Http.GetHtml(mHttpItem).Html.Split('|').ToList();
            try
            {

                if (ResultHtml[0].Trim().Equals("0"))
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

            mHttpItem.URL = ApiHost + $"/api/yh_sf/id={id}&phone={phone}&token={Token}";

            List<string> ResultHtml = Http.GetHtml(mHttpItem).Html.Split('|').ToList();
            try
            {

                if (ResultHtml[0].Trim().Equals("0"))
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
        /// 获取支付宝充值二维码
        /// </summary>
        /// <param name="PayCount">充值金额</param>
        /// <param name="PayImg">返回支付宝二维码</param>
        /// <returns>是否获取到验证码</returns>
        public bool GetPayImg( int PayCount, out Image PayImg)
        { 
            if (PayCount < 10)
            {
                ErrMsg = "最少充值金额为10元";
                PayImg = null;
                return false;
            }
            mHttpItem.URL = ApiHostPro + $"16899/api/sc/token={Token}&j={PayCount}";
            Image AlipayImg = Http.GetImage(mHttpItem);
            if (AlipayImg.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Bmp)){
                PayImg = AlipayImg;
                return true;
            }
            else
            {
                ErrMsg = "获取二维码错误";
                PayImg = null;
                return false;
            }

        }

        /// <summary>
        /// 获取手机号码
        /// </summary>
        /// <param name="id">项目ID</param>
        /// <param name="ISP">运营商</param>
        /// <param name="area">地区</param>
        /// <param name="card">虚拟：1或实卡：2</param>
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

            mHttpItem.URL = ApiHost + $"/api/yh_qh/id={id}&operator={ISP}&Region={area}&card={card}&phone={phone}&loop={loop}&token={Token}";

            List<string> ResultHtml = Http.GetHtml(mHttpItem).Html.Split('|').ToList();
          
            try
            {

                if (ResultHtml[0].Trim().Equals("0"))
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
        /// <param name="author">作者ID</param>
        /// <returns>短信内容</returns>
        public bool GetPhoneMsg(string id, string phone, out string Result)
        {
            if (string.IsNullOrWhiteSpace(id)||string.IsNullOrWhiteSpace(phone))
            {
                ErrMsg = "项目ID不能为空或接收手机号为空";
                Result = string.Empty;
                return false;
            }

            mHttpItem.URL = ApiHost + $"/api/yh_qm/id={id}&phone={phone}&t={Developer}&token={Token}";

            List<string> ResultHtml = Http.GetHtml(mHttpItem).Html.Split('|').ToList();
            try
            {

                if (ResultHtml[0].Trim().Equals("0"))
                {
                    ErrMsg = ResultHtml[1].Trim();
                    Result = string.Empty;
                    return false;
                }
                else
                {
                    Result = ResultHtml[1].Trim();
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
            mHttpItem.URL = ApiHost + $"/api/yh_gx/token={Token}";
            List<string> ResultHtml = Http.GetHtml(mHttpItem).Html.Split('|').ToList();
            try
            {
                if (!ResultHtml[0].Trim().Equals("0"))
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

            mHttpItem.URL = ApiHost + $"/api/sign/username={username}&password={password}";

            List<string> ResultHtml = Http.GetHtml(mHttpItem).Html.Split('|').ToList();

            try
            {
                if (ResultHtml[0].Trim().Equals("0"))
                {
                    ErrMsg = ResultHtml[1].Trim();
                    return false;
                }
                else
                {
                    Token = ResultHtml[1].Trim();
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
        /// 发送短信
        /// </summary>
        /// <param name="id">项目Id</param>
        /// <param name="phone">手机号码</param>
        /// <param name="sendPhone">发送号码</param>
        /// <param name="content">发送内容</param>
        /// <returns>是否成功</returns>
        public bool SendMsg(string id, string phone, string sendPhone, string content)
        {
            if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(phone) || string.IsNullOrWhiteSpace(sendPhone) || string.IsNullOrWhiteSpace(content))
            {
                ErrMsg = "项目ID不能为空或接收手机号为空或发送对方为空号码及内容";
               
                return false;
            }

            mHttpItem.URL = ApiHost + $"/api/fdx_tj/id={id}&phone={phone}&send={sendPhone}&content={content}&token={Token}";

            List<string> ResultHtml = Http.GetHtml(mHttpItem).Html.Split('|').ToList();
            try
            {

                if (ResultHtml[0].Trim().Equals("0"))
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

                ErrMsg = "函数SendMsg出错";
                
                return false;
            }
        }
    }
}
