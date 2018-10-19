using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.IO;
using Square.Picasso;

namespace Photo247
{
    public class CustomListAdapter : BaseAdapter<Tree>
    {
        List<Tree> trees;

        public CustomListAdapter(List<Tree> trees)
        {
            this.trees = trees;
        }

        public void add(Tree tree)
        {
            trees.Add(tree);
            NotifyDataSetChanged();
        }

        public override Tree this[int position]
        {
            get
            {
                return trees[position];
            }
        }

        public override int Count
        {
            get
            {
                return trees.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView;

            if (view == null)
            {
                view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.lvtemplate, parent, false);

                var photo = view.FindViewById<ImageView>(Resource.Id.photo);
                var likes = view.FindViewById<TextView>(Resource.Id.likecount);

                view.Tag = new ViewHolder() { Photo = photo, Likes = likes };
            }

            var holder = (ViewHolder)view.Tag;

            Picasso.With(parent.Context).Load(trees[position].imgURL).Into(holder.Photo);

            //holder.Photo.SetImageBitmap(ImageManager.GetImageBitmapFromUrl(trees[position].imgURL));
            holder.Likes.Text = trees[position].Likes.ToString();
               

            return view;

        }
    }
}