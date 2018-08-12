
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

using Android.Util;
using System.Net;



using Android.Provider;
using Android.Content.PM;
using Android.Graphics;
using Java.IO;

namespace SimsimiChat
{
    [Activity(Label = "HomePageActivity", MainLauncher = true, Theme = "@style/Theme.AppCompat.Light.NoActionBar" )]
    public class HomePageActivity : Activity
    {
        int count = 1;
        protected override void OnCreate(Bundle savedInstanceState)
        {

            Log.Info("SimsimiChat", "OnCreate");
            base.OnCreate(savedInstanceState);

            if (savedInstanceState != null)
            {
                count = savedInstanceState.GetInt("clicks");
            }

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

            button3.Click += delegate {
                var na = new Intent(this, typeof(MediaPlayerActivity));
                StartActivity(na);
            };
           
            Button counterBtn = FindViewById<Button>(Resource.Id.counterBtn);
            counterBtn.Click += new EventHandler(this.Button1Clicked);


            if (savedInstanceState != null)
            {
                count = savedInstanceState.GetInt("counter");
                counterBtn.Text = count.ToString();
            }



        }
        protected override void OnSaveInstanceState(Bundle outState)
        {
            outState.PutInt("counter", count);
            base.OnSaveInstanceState(outState);
        }
        protected void openPracs()
        {


        }
        public void Button1Clicked(object sender, EventArgs e)
        {
            Log.Info("TesterApp", "myButton - Clicked");
            ((Button)sender).Text = string.Format("{0} clicks!", count++);
        }

    }
}
