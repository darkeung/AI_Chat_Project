
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
using Android.Media;
using Android.Support.V7.App;
using Dyanamitechetan.Vusikview;
using static Android.Views.View;
using Java.Lang;
using Java.Util.Concurrent;
using static Android.Media.MediaPlayer;
using Android.Content.Res;

namespace SimsimiChat
{
    [Activity(Label = "MusicPlayerActivity",  Theme = "@style/Theme.AppCompat.Light.NoActionBar", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class MusicPlayerActivity : AppCompatActivity, IOnClickListener, IOnTouchListener, IOnBufferingUpdateListener, IOnCompletionListener
    {
        public ImageButton btn_play_pause;
        public SeekBar seekBar;
        public TextView txt_timer;

        public VusikView musicView;

        public MediaPlayer mediaPlayer;
        public int mediaFileLength, realTime;
        public Handler handler = new Handler();



        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.MusicPlayer);

            musicView = FindViewById<VusikView>(Resource.Id.musicView);

            btn_play_pause = FindViewById<ImageButton>(Resource.Id.btn_play_pause);
            btn_play_pause.SetOnClickListener(this);

            seekBar = FindViewById<SeekBar>(Resource.Id.seekBar);
            seekBar.Max = 99;
            seekBar.SetOnTouchListener(this);

            txt_timer = FindViewById<TextView>(Resource.Id.txt_timer);
            mediaPlayer = new MediaPlayer();
            mediaPlayer.SetOnBufferingUpdateListener(this);
            mediaPlayer.SetOnCompletionListener(this);

        }

       

        public void OnClick(View v)
        {
            if (v.Id == Resource.Id.btn_play_pause)
            {
                new MP3Play(this).Execute("https://s0.vocaroo.com/media/download_temp/Vocaroo_s062ccnFxy4f.mp3");
                musicView.Start();
            }
        }

        public bool OnTouch(View v, MotionEvent e)
        {
            if(v.Id == Resource.Id.seekBar)
            {
                if(mediaPlayer.IsPlaying)
                {
                    SeekBar sb = (SeekBar)v;
                    int playPosition = (mediaFileLength / 100) * sb.Progress;
                    mediaPlayer.SeekTo(playPosition);
                }
            }
            return false;
        }

        public void OnBufferingUpdate(MediaPlayer mp, int percent)
        {
            seekBar.SecondaryProgress = percent;
        }

        public void OnCompletion(MediaPlayer mp)
        {
            btn_play_pause.SetImageResource(Resource.Drawable.ic_play);
            musicView.StopNotesFall();
        }
    }
        internal class MP3Play:AsyncTask<string,string,string>
        {
            private MusicPlayerActivity musicPlayerActivity;
            private ProgressDialog mDialog;

            public MP3Play(MusicPlayerActivity musicPlayerActivity)
            {
                this.musicPlayerActivity = musicPlayerActivity;
            }

            protected override void OnPreExecute()
            {
                base.OnPreExecute();
                mDialog = new ProgressDialog(musicPlayerActivity.BaseContext);
                mDialog.Window.SetType(WindowManagerTypes.SystemAlert);
                mDialog.SetMessage("Please wait...");
                mDialog.Show();
            }

            protected override string RunInBackground(params string[] @params)
            {
                try
                {
                    musicPlayerActivity.mediaPlayer.SetDataSource(@params[0]);
                    musicPlayerActivity.mediaPlayer.Prepare();
                }
                catch(Java.Lang.Exception ex)
                {
                    
                }
                return "";
            }

        protected override void OnPostExecute(string result)
        {
            musicPlayerActivity.mediaFileLength = musicPlayerActivity.realTime = musicPlayerActivity.mediaPlayer.Duration;
            if(!musicPlayerActivity.mediaPlayer.IsPlaying)
            {
                musicPlayerActivity.mediaPlayer.Start();
                musicPlayerActivity.btn_play_pause.SetImageResource(Resource.Drawable.ic_pause);
            }
            else
            {
                musicPlayerActivity.mediaPlayer.Pause();
                musicPlayerActivity.btn_play_pause.SetImageResource(Resource.Drawable.ic_play);
            }

            UpdateSeekBar();
            mDialog.Dismiss();
        }

        private void UpdateSeekBar()
        {
            musicPlayerActivity.seekBar.Progress = ((int)(((float)musicPlayerActivity.mediaPlayer.CurrentPosition / musicPlayerActivity.mediaFileLength) * 100));
            if(musicPlayerActivity.mediaPlayer.IsPlaying)
            {
                Runnable update = new Runnable(() =>
                {
                    UpdateSeekBar();
                    musicPlayerActivity.realTime -= 1000;
                    musicPlayerActivity.txt_timer.Text = $"{TimeUnit.Milliseconds.ToMinutes(musicPlayerActivity.realTime)}:{TimeUnit.Milliseconds.ToSeconds(musicPlayerActivity.realTime)-TimeUnit.Milliseconds.ToMinutes(musicPlayerActivity.realTime)}";
               
                });
                musicPlayerActivity.handler.PostDelayed(update, 1000);
            }
        }

    }
}
