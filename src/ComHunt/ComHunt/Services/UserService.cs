using ComHunt.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(UserService))]
namespace ComHunt.Services
{
    public class UserService : IUserService
    {
        static string default_name = "";
        static string default_password = "";

        string name { get; set; }
        string password { get; set; }
        bool isLogged { get; set; }
        int numberCurrentHunt { get; set; }
        int positionGPS { get; set; } //set peut être useless

        public UserService()
        {
            name = default_name;
            password = default_password;
            isLogged = false;
        }

        public string getName()
        {
            return name;
        }

        public string getPassword()
        {
            return password;
        }

        public void setName(string name)
        {
            this.name = name;
        }

        public void setPassword(string pw)
        {
            password = pw;
        }

        public bool IsLogged()
        {
            return isLogged;
        }

        public void Connect(string name){
            this.name = name;
            isLogged = true;
        }

        public void LogOut(){
            this.name = default_name;
            this.password = default_password;
            isLogged = false;
        }

        //Numero Current Chasse
        public int GetNumberCurrentHunt(){
            return numberCurrentHunt;
        }

        public void SetNumberCurrentHunt(int number)
        {
            numberCurrentHunt=number;
        }

        //Position GPS
        public int GetPositionGPS()
        {
            return positionGPS;
        }

        public void SetPositionGPS(int number) //peut être useless !!!
        {
            positionGPS = number;
        }
    }
}
