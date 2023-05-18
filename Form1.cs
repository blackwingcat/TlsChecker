using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.NetworkInformation;
using Microsoft.VisualBasic;
//using Microsoft.VisualBasic.CompilerServices;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        [Flags]
        public enum SecurityProtocolType
        {
            // Token: 0x0400113A RID: 4410
            SystemDefault = 0,
            /// <summary>SSL (Secure Socket Layer) 3.0 セキュリティ プロトコルを示します。</summary>
            // Token: 0x0400113B RID: 4411
            Pct = 3,
            Ssl2 = 12,
            Ssl3 = 48,
            /// <summary>TLS (Transport Layer Security) 1.0 セキュリティ プロトコルを示します。</summary>
            // Token: 0x0400113C RID: 4412
            Tls = 192,
            /// <summary>TLS (Transport Layer Security) 1.1 セキュリティ プロトコルを示します。</summary>
            // Token: 0x0400113D RID: 4413
            Tls11 = 768,
            /// <summary>TLS (Transport Layer Security) 1.2 セキュリティ プロトコルを示します。</summary>
            // Token: 0x0400113E RID: 4414
            Tls12 = 3072,
            // Token: 0x0400113F RID: 4415
            Tls13 = 12288
        }
        // Token: 0x0200001A RID: 26
        private enum TLSVersion
        {
            // Token: 0x0400025D RID: 605
            TLS = 192,
            // Token: 0x0400025E RID: 606
            TLS11 = 768,
            // Token: 0x0400025F RID: 607
            TLS12 = 3072,
            // Token: 0x04000260 RID: 608
            TLS13 = 12288
        }
        /*
          private void StartupConnectionInternal()
              {
                  if (this._initial)
                  {
                      if (this.InvokeRequired && !this.IsDisposed)
                      {
                          this.Invoke(new Action(this.StartupConnectionInternal));
                          return;
                      }
                      this.tw.VerifyCredentials();
                      try
                      {
                          foreach (UserAccount userAccount in this._cfgCommon.UserAccounts)
                          {
                              if (Operators.CompareString(userAccount.Username.ToLower(), this.tw.Username.ToLower(), false) == 0)
                              {
                                  userAccount.UserId = this.tw.UserId;
                                  userAccount.UserIconUrl = this.tw.UserIconUrl;
                                  userAccount.UserIcon = new HttpVarious().GetImage(this.tw.UserIconUrl);
                                  break;
                              }
                          }
                      }
                      finally
                      {
                          List<UserAccount>.Enumerator enumerator;
                          ((IDisposable)enumerator).Dispose();
                      }
                      this.SetCurrentUserInfo();
                      try
                      {
                          foreach (UserAccount userAccount2 in this.SettingDialog.UserAccounts)
                          {
                              if (userAccount2.UserId == 0L && Operators.CompareString(userAccount2.Username.ToLower(), this.tw.Username.ToLower(), false) == 0)
                              {
                                  userAccount2.UserId = this.tw.UserId;
                                  break;
                              }
                          }
                      }
                      finally
                      {
                          List<UserAccount>.Enumerator enumerator2;
                          ((IDisposable)enumerator2).Dispose();
                      }
                      this.GetTimeline(MyCommon.WORKERTYPE.BlockIds, 0, 0, "");
                      Thread.Sleep(100);
                      if (this.SettingDialog.StartupFollowers)
                      {
                          this.GetTimeline(MyCommon.WORKERTYPE.Follower, 0, 0, "");
                          Thread.Sleep(100);
                      }
                      this.GetTimeline(MyCommon.WORKERTYPE.Configuration, 0, 0, "");
                      Thread.Sleep(100);
                      Thread.Sleep(100);
                      this._waitTimeline = true;
                      this.GetTimeline(MyCommon.WORKERTYPE.Timeline, 1, 1, "");
                      Thread.Sleep(100);
                      this._waitReply = true;
                      this.GetTimeline(MyCommon.WORKERTYPE.Reply, 1, 1, "");
                      Thread.Sleep(100);
                      this._waitDm = true;
                      this.GetTimeline(MyCommon.WORKERTYPE.DirectMessegeRcv, 1, 1, "");
                      Thread.Sleep(100);
                      if (this.SettingDialog.GetFav)
                      {
                          this._waitFav = true;
                          this.GetTimeline(MyCommon.WORKERTYPE.Favorites, 1, 1, "");
                          Thread.Sleep(100);
                      }
                      this._waitPubSearch = true;
                      this.GetTimeline(MyCommon.WORKERTYPE.PublicSearch, 1, 0, "");
                      Thread.Sleep(100);
                      this._waitUserTimeline = true;
                      this.GetTimeline(MyCommon.WORKERTYPE.UserTimeline, 1, 0, "");
                      Thread.Sleep(100);
                      this._waitLists = true;
                      this.GetTimeline(MyCommon.WORKERTYPE.List, 1, 0, "");
                      int num = 0;
                      int num2 = 0;
                      while (this.IsInitialRead() && !MyCommon._endingFlag)
                      {
                          Thread.Sleep(100);
                          MyProject.Application.DoEvents();
                          num++;
                          num2++;
                          if (num2 > 1200)
                          {
                              break;
                          }
                          if (num > 50)
                          {
                              if (MyCommon._endingFlag)
                              {
                                  return;
                              }
                              num = 0;
                          }
                      }
                      if (MyCommon._endingFlag)
                      {
                          return;
                      }
                      if (!this.tw.GetFollowersSuccess && this.SettingDialog.StartupFollowers)
                      {
                          this.GetTimeline(MyCommon.WORKERTYPE.Follower, 0, 0, "");
                      }
                      if (this.SettingDialog.TwitterConfiguration.PhotoSizeLimit == 0)
                      {
                          this.GetTimeline(MyCommon.WORKERTYPE.Configuration, 0, 0, "");
                      }
                      if (this.tw.ApiInformation.AccessLevel == ApiAccessLevel.ReadWrite)
                      {
                          MessageBox.Show(Resources.ReAuthorizeText);
                          this.SettingStripMenuItem_Click(null, null);
                      }
                      this.GetTimeline(MyCommon.WORKERTYPE.FriendshipsIncoming, 0, 0, "");
                      if (this.SettingDialog.StartupVersion)
                      {
                          this.CheckNewVersion(true);
                      }
                      this._initial = false;
                  }
              }
      */

        /*
    private Uri CreateTwitterUri(string path)
    {
        return new Uri(string.Format("{0}{1}{2}", "https://", "api.twitter.com", path));
    }

    // Token: 0x060004F4 RID: 1268 RVA: 0x0001BED8 File Offset: 0x0001A0D8
    public static string UrlEncode(string stringToEncode)
    {
        StringBuilder stringBuilder = new StringBuilder();
        byte[] bytes = Encoding.UTF8.GetBytes(stringToEncode);
        foreach (byte b in bytes)
        {
            if ("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.~".IndexOf(Strings.Chr((int)b)) != -1)
            {
                stringBuilder.Append(Strings.Chr((int)b));
            }
            else
            {
                stringBuilder.AppendFormat("%{0:X2}", b);
            }
        }
        return stringBuilder.ToString();
    }


    // Token: 0x060004F0 RID: 1264 RVA: 0x0001BC94 File Offset: 0x00019E94
    public static string CreateQueryString(IDictionary<string, string> param)
    {
        if (param == null || param.Count == 0)
        {
            return string.Empty;
        }
        StringBuilder stringBuilder = new StringBuilder();
        try
        {
            foreach (string text in param.Keys)
            {
                stringBuilder.AppendFormat("{0}={1}&", UrlEncode(text), UrlEncode(param[text]));
            }
        }
        finally
        {
//                IEnumerator<string> enumerator;
//                if (enumerator != null)
//                {
//                    enumerator.Dispose();
//                }
        }
        return stringBuilder.ToString(0, stringBuilder.Length - 1);
    }

    protected void CreateRequest(string method, Uri requestUri, Dictionary<string, string> param, byte[] bytes, bool withCookie)
    {
        UriBuilder uriBuilder = new UriBuilder(requestUri.AbsoluteUri);
        if (param != null)
        {
            uriBuilder.Query = CreateQueryString(param);
        }


        HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(uriBuilder.Uri);
        httpWebRequest.ReadWriteTimeout = 90000;
        httpWebRequest.Proxy = HttpWebRequest.DefaultWebProxy;
        if (requestUri.Host.Contains("twitter.com"))
        {
            httpWebRequest.KeepAlive = false;
        }
        if (withCookie)
        {
            httpWebRequest.CookieContainer = new CookieContainer();
        }
        httpWebRequest.Timeout = 6000;
        httpWebRequest.KeepAlive = false;
        httpWebRequest.Method = "GET";
        httpWebRequest.ContentType = "text/html";


        System.Text.Encoding enc = System.Text.Encoding.GetEncoding("utf-8");
        httpWebRequest.Credentials = CredentialCache.DefaultCredentials;



        WebResponse wrep = httpWebRequest.GetResponse();
        Stream s = wrep.GetResponseStream();
        //            Stream requestStream = httpWebRequest.GetRequestStream();
        //           requestStream.Write(bytes, 0, bytes.Length);
        //            return httpWebRequest;
        //読み込むバッファを用意
        Byte[] buff = new Byte[1024];
        int iread = 0;
        StringBuilder sb = new StringBuilder();

        //ストリームから読み込む...
        while ((iread = s.Read(buff, 0, buff.Length)) > 0)
        {
            sb.Append(Encoding.Default.GetString(buff, 0, iread));
            Console.Write(".");
        }
        Console.Write("\n");

        //クリーンアップ
        s.Close();
        wrep.Close();
    }

    public static bool IsNetworkAvailable()
    {
        bool result;
        try
        {
            result = NetworkInterface.GetIsNetworkAvailable();
        }
        catch (Exception ex)
        {
            result = false;
        }
        return result;
    }
    private void SetTempProxy()
    {
        HttpConnection.ProxyType proxyType;
        proxyType = HttpConnection.ProxyType.None;
        //proxyType = HttpConnection.ProxyType.IE;
        string proxyAddress = "";
        int proxyPort = 0;// int.Parse(this.TextProxyPort.Text.Trim());
        string proxyUser = ""; // this.TextProxyUser.Text.Trim();
        string proxyPassword = "";// this.TextProxyPassword.Text.Trim();
        HttpConnection.InitializeConnection(20, proxyType, proxyAddress, proxyPort, proxyUser, proxyPassword);
    }
    */
        private void Form1_Load(object sender, EventArgs e)
        {
            //            ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType)(SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls);
            //            CreateRequest("GET", this.CreateTwitterUri("/oauth/authorize?oauth_token=h2CuFAAAAAAACIKFAAABhuTqaF8"),null, null,false);
            /*            this.SetTempProxy();

                        if (!IsNetworkAvailable())
                        {
                            MessageBox.Show("Can't connect to Twitter.");
                            return;
                        }
            */
            /*
//            this.PINText.Text = "";
//            this.PINGroup.Enabled = false;
            this.SetTempProxy();
 //           HttpTwitter.TwitterUrl = this.TwitterAPIText.Text.Trim();
 //           HttpTwitter.TwitterSearchUrl = this.TwitterSearchAPIText.Text.Trim();
            if (!HttpConnection.IsInternetAvailable)
            {
                MessageBox.Show("Can't connect to Twitter.");
                return;
            }
            HttpConnection.IsSuspend = false;
 //           this._tw.Initialize("", "", "", 0L);
 //           if (this.StartAuth())
 //           {
 //               this.PINGroup.Enabled = true;
 //               this.PINText.Focus();
 //           }
            HttpConnection.IsSuspend = true;
*/

            int tlsflag = 0;
            try
            {
                ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType)(SecurityProtocolType.Tls);
                tlsflag |= 1;
                checkBox1.Checked = true;
            }
            catch
            {
            }
            try
            {
                ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType)(SecurityProtocolType.Tls11);
                tlsflag |= 2;
                checkBox2.Checked = true;
            }
            catch
            {
            }
            try
            {
                ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType)(SecurityProtocolType.Tls12);
                tlsflag |= 4;
                checkBox3.Checked = true;
            }
            catch
            {
            }
            try
            {
                ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType)(SecurityProtocolType.Tls13);
                tlsflag |= 8;
                checkBox4.Checked = true;
            }
            catch
            {
            }
            try
            {
                ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType)(SecurityProtocolType.Ssl3);
                tlsflag |= 16;
                checkBox6.Checked = true;
            }
            catch
            {
            }
            try
            {
                ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType)(SecurityProtocolType.Ssl2);
                tlsflag |= 32;
                checkBox5.Checked = true;
            }
            catch
            {
            }
            try
            {
                ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType)(SecurityProtocolType.Pct);
                tlsflag |= 64;
                checkBox7.Checked = true;
            }
            catch
            {
            }

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
