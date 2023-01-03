using SIGED_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;


namespace WinAuthentication.Authentication.Windows
{
    public class WindowsAuthentication
    {
        #region DllImportLogOnUser
        [DllImport("advapi32.dll")]
        public static extern bool LogonUser(string username, string domain, string password, int logType, int logpv, ref IntPtr intPtr);
        #endregion

        public WindowsAuthentication(UserRequest userRequest)
        {
            UserRequest = userRequest;
        }

        public UserRequest UserRequest { get; set; }

        public bool Authenticate()
        {
            try
            {
                bool isAuthenticated = false;


                IntPtr ip = IntPtr.Zero;

                isAuthenticated = LogonUser(UserRequest.UserName, UserRequest.Domain, UserRequest.Password, 2, 0, ref ip);

                return isAuthenticated;
            }
            catch
            {
                throw;
            }
        }

    }
}
