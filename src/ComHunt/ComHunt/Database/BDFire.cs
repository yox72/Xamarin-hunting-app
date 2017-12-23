using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Xamarin.Database;
using System.Linq;
using Xamarin.Forms;
using ComHunt.Models;

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
                        .Child("Vue")
                        .OnceAsync<Vue>())
                        .Select(item =>
                                new Vue
                                {
                                    Sanglier = item.Object.Sanglier,
                                    Chevreuil = item.Object.Chevreuil,
                                    Renard = item.Object.Renard
                                }
                       
                       ).ToList();

            return list; 
        }
    }
}

