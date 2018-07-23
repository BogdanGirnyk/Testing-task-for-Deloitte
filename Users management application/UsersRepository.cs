using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web;
using Users_management_application.Models;

namespace Users_management_application
{
    public class UsersRepository
    {
        private static List<User> _users { get; set; }

        static UsersRepository()
        {
            _users = ReadUsersFromJson();
        }

        private static List<User> ReadUsersFromJson()
        {
            string locationOfAccountsFile = ConfigurationManager.AppSettings["PathToUsersDataFile"];
            string filePath = System.Web.Hosting.HostingEnvironment.MapPath(locationOfAccountsFile);

            string accountsJSON = System.IO.File.ReadAllText(filePath);

            dynamic accountsArray = JsonConvert.DeserializeObject(accountsJSON);

            var userList = new List<User>();

            foreach (dynamic arrayEl in accountsArray)
            {
                List<string> Tags = new List<string>();
                foreach (dynamic tagEl in arrayEl["tags"])
                {
                    Tags.Add(tagEl.ToString());
                }

                userList.Add(new User()
                {
                    Id = new Guid(arrayEl["guid"].ToString()),
                    PicturePath = arrayEl["picture"],
                    Name = arrayEl["name"],
                    Gender = arrayEl["gender"],
                    Company = arrayEl["company"],
                    Email = arrayEl["email"],
                    Phone = arrayEl["phone"],
                    Address = arrayEl["address"],
                    About = arrayEl["about"],
                    Registered = DateTime.Parse(arrayEl["registered"].ToString()),
                    Tags = Tags
                });
            }

            return userList;
        }
        public static DateTime ParseDate(string DateString)
        {
            return DateTime.ParseExact(DateString, "yyyy-MM-ddTHH:mm:", CultureInfo.InvariantCulture);
        }

        public List<User> GetAllUsers()
        {
            return _users;
        }
        public User GetUserById(Guid Id)
        {
            return _users.Find(c => c.Id == Id);
        }
    }
}