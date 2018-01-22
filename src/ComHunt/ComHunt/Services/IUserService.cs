using System;
namespace ComHunt.Services
{
    public interface IUserService
    {
        string getName();
        void setName(string name);
        string getPassword();
        void setPassword(string pw);
        bool IsLogged();
        void Connect(string name);
        void LogOut();
    }
}
