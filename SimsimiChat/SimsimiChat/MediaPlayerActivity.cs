using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Media;
using System;
using Android.Graphics;
using Android.Runtime;
using Android.Util;

namespace SimsimiChat
{
    [Activity(Label = "MediaPlayerActivity")]
    public class MediaPlayerActivity : AppCompatActivity, ISurfaceHolderCallback, MediaPlayer.IOnPreparedListener
    {
        private SurfaceView surfaceView;
        private ISurfaceHolder surfaceHolder;
        private MediaPlayer mediaPlayer;
        private const string VIDEO_PATH = "https://video-cutter-online.com/download/wiWZucxFxmTr2cWi.mp4";

        protected override void OnCreate(Bundle bundle)
        {

            base.OnCreate(bundle);
            SetContentView(Resource.Layout.MediaPlayer);
            surfaceView = FindViewById<SurfaceView>(Resource.Id.surfaceView);
            surfaceHolder = surfaceView.Holder;
            surfaceHolder.AddCallback(this);

        }

        public void SurfaceChanged(ISurfaceHolder holder, [GeneratedEnum] Format format, int width, int height)
        {

        }

        public void SurfaceCreated(ISurfaceHolder holder)
        {
            mediaPlayer = new MediaPlayer();
            mediaPlayer.SetDisplay(surfaceHolder);
            try
            {
                mediaPlayer.SetDataSource(VIDEO_PATH);
                mediaPlayer.Prepare();
                mediaPlayer.SetOnPreparedListener(this);
                mediaPlayer.SetAudioStreamType(Stream.Music);
            }
            catch (Exception ex)
            {
                Log.Error("ERROR", ex.Message);
            }

        }

        public void SurfaceDestroyed(ISurfaceHolder holder)
        {

        }
        public void OnPrepared(MediaPlayer mp)
        {
            mediaPlayer.Start();
        }

        protected override void OnPause()
        {
            base.OnPause();
            ReleaseMediaPlayer();
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
            ReleaseMediaPlayer();

        }

        private void ReleaseMediaPlayer()
        {

            if (mediaPlayer != null)
            {
                mediaPlayer.Release(); mediaPlayer = null;
            }

        }


    }
}