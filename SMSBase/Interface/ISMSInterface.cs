using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace SMSBase.Interface
{
    interface ISMSInterface
    {
        /// <summary>
        /// 登录Cookie
        /// </summary>
        string Token { get; set; }
        /// <summary>
        /// 错误消息
        /// </summary>
        string ErrMsg { get; set; }

        string Developer { get; set; }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        bool Login(string username, string password);
        /// <summary>
        /// 获取支付宝充值二维码
        /// </summary>
        /// <param name="PayCount">充值金额</param>
        /// <param name="PayImg">返回支付宝二维码</param>
        /// <returns>是否获取到验证码</returns>
        bool GetPayImg(int PayCount, out Image PayImg);
        /// <summary>
        /// 查询用户余额
        /// </summary>
        /// <returns></returns>
        bool GetUserBalance(out List<string> ResultInfo);

        /// <summary>
        /// 获取手机号码
        /// </summary>
        /// <param name="id">项目ID</param>
        /// <param name="Result">返回手机号列表</param>
        /// <param name="ISP">运营商</param>
        /// <param name="area">地区</param>
        /// <param name="card">虚拟：1或实卡：2</param>
        /// <param name="phone">指定手机号</param>
        /// <param name="loop">过滤已做过号码 过滤：1 不过滤：2</param>
        /// <returns>返回手机号码</returns>
        bool GetPhone(string id, out List<string> Result, string ISP= "0", string area="0", int card= 0, string phone = null, int loop = 1);

        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <param name="id">项目Id</param>
        /// <param name="phone">手机号码</param>      
        /// <returns>短信内容</returns>
        bool GetPhoneMsg(string id, string phone, out string Result);

        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="id">项目Id</param>
        /// <param name="phone">手机号码</param>
        /// <param name="sendPhone">发送号码</param>
        /// <param name="content">发送内容</param>
        /// <returns>是否成功</returns>
        bool SendMsg(string id, string phone, string sendPhone, string content);

        /// <summary>
        /// 加黑手机号码
        /// </summary>
        /// <param name="id">项目Id</param>
        /// <param name="phone">手机号码</param>
        /// <returns>是否成功</returns>
        bool AddBlackPhone(string id, string phone);

        /// <summary>
        /// 释放手机号
        /// </summary>
        /// <param name="id">项目Id</param>
        /// <param name="phone">手机号码</param>
        /// <returns>是否成功</returns>
        bool FreePhone(string id, string phone);
    }
}
