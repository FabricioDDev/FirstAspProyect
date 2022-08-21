using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DB;
using DomainModel;

namespace DataModel
{
    public class UserData
    {
        public UserData()
        {
            data = new DataAccess();
        }
        private DataAccess data;

        public List<User> Listing()
        {
            List<User> List = new List<User>();
            try
            {
                data.Query("select UserNumber, UserName, Mail, Pass, Active from user_table");
                data.Read();
                while (data.readerProp.Read())
                {
                    User aux = new User();
                    aux.UserNumber = (int)data.readerProp["UserNumber"];
                    aux.UserName = (string)data.readerProp["UserName"];
                    aux.Password = (string)data.readerProp["Pass"];
                    aux.Mail = (string)data.readerProp["Mail"];
                    aux.Active = (bool)data.readerProp["Active"];
                    List.Add(aux);
                }
                return List;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { data.Close(); }
        }
    
        public void AddUser(User NewUser)
        {
            try
            {
                data.Query("insert into user_table (UserName, Mail, Pass, Active) values (@User, @Mail, @Pass, @Active)");
                data.Parameter("@User", NewUser.UserName);
                data.Parameter("@Mail", NewUser.Mail);
                data.Parameter("@Pass", NewUser.Password);
                data.Parameter("@Active", 1);
                data.Execute();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally { data.Close(); }
        }
    }
}
