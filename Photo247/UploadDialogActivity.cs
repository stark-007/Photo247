using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage;
using Android.Graphics.Drawables;

namespace Photo247
{
    [Activity(Label = "UploadDialogActivity")]//[Activity(Theme = "@android:style/Theme.Dialog")]
    public class UploadDialogActivity : Activity
    {
        CloudStorageAccount storageAccount;
        CloudBlobClient blobClient;
        CloudBlobContainer container;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.UploadDialog);
            // Create your application here

            Bitmap name = (Bitmap)Intent.GetStringExtra("image");

            ImageView imageView1 = this.FindViewById<ImageView>(Resource.Id.imageView1);

            Button btn = FindViewById<Button>(Resource.Id.btnSubmit);
            btn.Click += BtnOnClick;

            //initialize blob service
            // Retrieve storage account from connection string.
            storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=247blobstorage;AccountKey=K3Nj4RCexzsJFUhnUCCervyT+7XeidUwM/odzHIME5BW5L6jdOG7UVNyBVWFm5bPrlJn+RTEae7a3Ub4f5bmcA==");

            // Create the blob client.
            blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            container = blobClient.GetContainerReference("247images");
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
        }

        private void BtnOnClick(object sender, EventArgs eventArgs)
        {
            //additional code to be added
            //ImageView imageView1 = this.FindViewById<ImageView>(Resource.Id.imageView1);
            ////SaveToDB(((BitmapDrawable)imageView1.get).getBitmap());

            //imageView1.BuildDrawingCache(true);
            //Bitmap bitmap = imageView1.GetDrawingCache(true);

            //BitmapDrawable drawable = (BitmapDrawable)imageView1.GetDrawable();
            //Bitmap bitmap = drawable.GetBitmap();
        }
    }
}