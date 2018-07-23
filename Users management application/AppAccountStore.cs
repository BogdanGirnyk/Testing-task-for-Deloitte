using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Users_management_application.Models;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Configuration;
using Newtonsoft.Json;

namespace Users_management_application
{

    /// <summary>
    /// Custom UserStore using account data from JSON file
    /// </summary>
    public class AppAccountsStore : IUserPasswordStore<AppAccount>,IUserStore<AppAccount>, IUserLockoutStore<AppAccount,string>, IUserTwoFactorStore<AppAccount, string>
    {
        private static List<AppAccount> _accounts { get; set; }

        static AppAccountsStore()
        {
            _accounts = ReadAccountsFromJson();
        }

        private static List<AppAccount> ReadAccountsFromJson()
        {
            string locationOfAccountsFile = ConfigurationManager.AppSettings["PathToAccountsFile"];
            string filePath = System.Web.Hosting.HostingEnvironment.MapPath(locationOfAccountsFile);

            string accountsJSON = System.IO.File.ReadAllText(filePath);

            dynamic accountsArray = JsonConvert.DeserializeObject(accountsJSON);

            var accList = new List<AppAccount>();

            foreach (dynamic arrayEl in accountsArray)
            {
                accList.Add(new AppAccount() {
                    Id = Guid.NewGuid().ToString(),
                    UserName = arrayEl["name"],
                    Login = arrayEl["login"],
                    Password = arrayEl["password"],
                });
            }

            return accList;
        }

        public AppAccountsStore()
        {

        }

        public Task<AppAccount> FindByIdAsync(string userId)
        {
            return Task.FromResult(_accounts.Find(c => c.Id == userId));
        }

        public Task<AppAccount> FindByNameAsync(string userName)
        {
            return Task.FromResult(_accounts.Find(c => c.Login == userName));
        }

        public Task<string> GetPasswordHashAsync(AppAccount user)
        {
            return Task.FromResult(Crypto.HashPassword(user.Password));
        }

        public Task<bool> HasPasswordAsync(AppAccount user)
        {
            return Task.FromResult(true);
        }

        public Task<bool> GetLockoutEnabledAsync(AppAccount user)
        {
            return Task.FromResult(false);
        }

        public Task<bool> GetTwoFactorEnabledAsync(AppAccount user)
        {
            return Task.FromResult(false);
        }


        public Task<int> GetAccessFailedCountAsync(AppAccount user)
        {
            return Task.FromResult(0);
        }

        public void Dispose()
        {
            
        }


        #region Unimplemented methods  
        public Task CreateAsync(AppAccount user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(AppAccount user)
        {
            throw new NotImplementedException();
        }


        public Task UpdateAsync(AppAccount user)
        {
            throw new NotImplementedException();
        }

        public Task SetPasswordHashAsync(AppAccount user, string passwordHash)
        {
            throw new NotImplementedException();
        }

        public Task<DateTimeOffset> GetLockoutEndDateAsync(AppAccount user)
        {
            throw new NotImplementedException();
        }

        public Task SetLockoutEndDateAsync(AppAccount user, DateTimeOffset lockoutEnd)
        {
            throw new NotImplementedException();
        }

        public Task<int> IncrementAccessFailedCountAsync(AppAccount user)
        {
            throw new NotImplementedException();
        }

        public Task ResetAccessFailedCountAsync(AppAccount user)
        {
            throw new NotImplementedException();
        }


        public Task SetLockoutEnabledAsync(AppAccount user, bool enabled)
        {
            throw new NotImplementedException();
        }

        public Task SetTwoFactorEnabledAsync(AppAccount user, bool enabled)
        {
            throw new NotImplementedException();
        }


        #endregion  

    }
}