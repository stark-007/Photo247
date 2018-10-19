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
using MongoDB.Bson;

namespace Photo247
{
    public class Tree
    {
        public ObjectId _id { get; set; }
        public string imgURL { get; set; }
        public string Name { get; set; }
        public int Likes { get; set; }
    }
}