using System;
using System.Net;
using Android.App;
using Android.Content;
//using Android.Graphics;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using static Android.Support.V7.Widget.RecyclerView;
using SQLite;
using System.IO;
using Android.Graphics;
using Android.Support.V4.Content;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Photo247
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true, ExcludeFromRecents = true)]
    public class MainActivity : AppCompatActivity
    {
        ListView treesListview;
        CustomListAdapter customAdapter;
        CloudStorageAccount storageAccount;
        CloudBlobClient blobClient;
        CloudBlobContainer container;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClick;


            treesListview = FindViewById<ListView>(Resource.Id.treesListview);

            //initialize blob service
            // Retrieve storage account from connection string.
            storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=247blobstorage;AccountKey=K3Nj4RCexzsJFUhnUCCervyT+7XeidUwM/odzHIME5BW5L6jdOG7UVNyBVWFm5bPrlJn+RTEae7a3Ub4f5bmcA==");

            // Create the blob client.
            blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            container = blobClient.GetContainerReference("247images");
        }

        protected async override void OnStart()
        {
            base.OnStart();
            customAdapter = new CustomListAdapter(await MongoService.GetAllItems());
            treesListview.Adapter = customAdapter;

        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }
            else if (id == Resource.Id.action_exit)
            {
                this.FinishAffinity();
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            //SaveToDB();

            var m_activity = new Intent(this, typeof(UploadDialogActivity));
            //(Bitmap)data.Extras.Get("data");
            m_activity.PutExtra("image", (Bitmap)data.Extras.Get("data"));
            this.StartActivity(m_activity);
        }

        void SaveToDB(Bitmap bitmap)
        {
            //save file
            var sdCardPath = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath;
            var fileName = "IMG" + DateTime.Now.ToString("yyyyMMddHHmmss");
            var filePath = System.IO.Path.Combine(sdCardPath, fileName + ".jpg");
            var stream = new FileStream(filePath, FileMode.Create);
            bitmap.Compress(Bitmap.CompressFormat.Jpeg, 100, stream);
            stream.Close();
            stream.Dispose();
            bitmap.Dispose();

            CloudBlockBlob blockBlob = container.GetBlockBlobReference(fileName);
            // Upload the file
            blockBlob.UploadFromFileAsync(filePath);

            var tree = new Tree()
            {
                imgURL = "https://247blobstorage.blob.core.windows.net/247images/" + fileName,
                Name = fileName,
                Likes = new Random().Next(0, 500)
            };

            //save to db
            //db.Insert(tree);
            MongoService.InsertItem(tree);
            customAdapter.add(tree);
        }


        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            //Intent intent = new Intent(Intent.ActionGetContent);
            ////Intent intent = new Intent(MediaStore.ActionImageCapture);
            //StartActivityForResult(intent, 0);

            var intent = new Intent(Intent.ActionPick, MediaStore.Images.Media.ExternalContentUri);
            intent.SetType("image/*");
            this.StartActivityForResult(Intent.CreateChooser(intent, "Select Picture"), 0);

        }
    }
}

