using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Xamarin.Database;
using System.Linq;
using Xamarin.Forms;
using ComHunt.Models;
using Firebase.Xamarin.Token;
using Firebase.Xamarin.Database.Query;

namespace ComHunt.Database
{
    public class BDFire // : ContentPage
    {
        FirebaseClient client;
        public BDFire()
        {
            client = new FirebaseClient("https://comhunt-5d0c1.firebaseio.com/"); 
        }

        public async Task<List<Vue>> getList(){
            var list = (await client
                        .Child("Chasse")
                        .Child("ChasseVue")
                        .OrderByKey()
                        //.LimitToFirst(2)
                        //.WithAuth("<Authentication Token>") // <-- Add Auth token if required. Auth instructions further down in readme.
                        .OnceAsync<Vue>())
                        .Select(item =>
                                new Vue
                                {
                                    Chevreuil = item.Object.Chevreuil,
                                    Renard = item.Object.Renard,
                                    Sanglier = item.Object.Sanglier
                                }
                       
                       ).ToList();

            return list;
        }
    }
}

