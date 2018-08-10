
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace SimsimiChat
{
    [Activity(Label = "HomePageActivity", MainLauncher = true, Theme = "@style/Theme.AppCompat.Light.NoActionBar" )]
    public class HomePageActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.HomePage);
            Button button1 = FindViewById<Button>(Resource.Id.button1);
            Button button2 = FindViewById<Button>(Resource.Id.button2);
            Button button3 = FindViewById<Button>(Resource.Id.button3);
           
            button1.Click += delegate {
                var name = new Intent(this, typeof(MainActivity));
                StartActivity(name);

            };
           



            button2.Click += delegate {
                var nam = new Intent(this, typeof(MusicPlayerActivity));
                StartActivity(nam);
            };
           
        }
    }
}
