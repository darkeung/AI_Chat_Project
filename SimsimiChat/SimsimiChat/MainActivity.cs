using Android.App;  
using Android.Widget;  
using Android.OS;  
using Android.Support.V7.App;  
using Android.Support.Design.Widget;  
using SimsimiChat.Model;  
using System.Collections.Generic;  
using SimsimiChat.Helper;  
using Newtonsoft.Json;  
using SimsimiChat.Adapter;  




namespace SimsimiChat {  
    [Activity(Label = "SimsimiChat",  Theme = "@style/Theme.AppCompat.Light.NoActionBar",  ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]  
    public class MainActivity: AppCompatActivity {  
       
        public ListView list_of_message;  
        public EditText user_message;  
        FloatingActionButton btn_send;  
        public List < ChatModel > list_chat = new List < ChatModel > (); 

        protected override void OnCreate(Bundle savedInstanceState) {  
            base.OnCreate(savedInstanceState);  
            // Set our view from the "main" layout resource  
            SetContentView(Resource.Layout.Main);  
            list_of_message = FindViewById < ListView > (Resource.Id.list_of_message);  
            user_message = FindViewById < EditText > (Resource.Id.user_message);  
            btn_send = FindViewById < FloatingActionButton > (Resource.Id.fab);  
            btn_send.Click += delegate {  
                string text = user_message.Text;  
                ChatModel model = new ChatModel();  
                model.ChatMessage = text;  
                model.IsSend = true;  
                list_chat.Add(model);  
                new SimsimiAPI(this).Execute(user_message.Text);  
                user_message.Text = "";  
            };  

           
        }

    } 


    internal class SimsimiAPI: AsyncTask < string, string, string > {  
        private MainActivity mainActivity;  
        private  
        const string API_KEY = "abcce760-696f-4b1e-a026-1b830849c097";
        public SimsimiAPI(MainActivity mainActivity)
        {
            this.mainActivity = mainActivity;
        }
        protected override string RunInBackground(params string[] @params)
        {
            string url = $"http://sandbox.api.simsimi.com/request.p?key={API_KEY}&lc=en&ft=1.0&text={@params[0]}";
            HttpDataHandler dataHandler = new HttpDataHandler();
            return dataHandler.GetHTTPData(url);
        }
        protected override void OnPostExecute(string result)
        {
            SimsimiModel simsimiModel = JsonConvert.DeserializeObject<SimsimiModel>(result.ToString());
            ChatModel model = new ChatModel();
            model.ChatMessage = simsimiModel.response;
            model.IsSend = false;
            mainActivity.list_chat.Add(model);
            CustomAdapter adapter = new CustomAdapter(mainActivity.list_chat, mainActivity.BaseContext);
            mainActivity.list_of_message.Adapter = adapter;
        }
    }



}