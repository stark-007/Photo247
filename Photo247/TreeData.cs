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

namespace Photo247
{
    public static class TreeData
    {
        public static List<Tree> Trees { get; private set; }

        static TreeData()
        {
            var temp = new List<Tree>();

            AddUser(temp);
            //AddUser(temp);
            //AddUser(temp);

            Trees = temp.ToList();
        }

        static void AddUser(List<Tree> trees)
        {
            trees.Add(new Tree()
            {
                imgURL = "https://i0.wp.com/www.dzzyn.com/wp-content/uploads/2017/03/10-TREE-PNG-IMAGES-FOR-ARCHITECTURE-LANDSCAPE-INTERIOR-RENDERINGS-big-TREE.png?resize=640%2C429",
                Name = "Narra",
                Likes = 19
            });

            trees.Add(new Tree()
            {
                imgURL = "https://img00.deviantart.net/5e3a/i/2010/289/0/2/tree_34_png_by_gd08-d30unt3.png",
                Name = "Acacia",
                Likes = 200
            });
            trees.Add(new Tree()
            {
                imgURL = "https://i.pinimg.com/originals/70/0c/85/700c85c3cce506136645dac4019bf94c.png",
                Name = "Atis",
                Likes = 25
            });
            trees.Add(new Tree()
            {
                imgURL = "http://www.honnelles.be/evenements/photos/erable.png/@@images/image.png",
                Name = "Mangga",
                Likes = 500
            });
            trees.Add(new Tree()
            {
                imgURL = "https://i0.wp.com/www.dzzyn.com/wp-content/uploads/2017/03/10-TREE-PNG-IMAGES-FOR-ARCHITECTURE-LANDSCAPE-INTERIOR-RENDERINGS-big-TREE.png?resize=640%2C429",
                Name = "Narra",
                Likes = 19
            });

            trees.Add(new Tree()
            {
                imgURL = "https://img00.deviantart.net/5e3a/i/2010/289/0/2/tree_34_png_by_gd08-d30unt3.png",
                Name = "Acacia",
                Likes = 200
            });
            trees.Add(new Tree()
            {
                imgURL = "https://i.pinimg.com/originals/70/0c/85/700c85c3cce506136645dac4019bf94c.png",
                Name = "Atis",
                Likes = 25
            });
            trees.Add(new Tree()
            {
                imgURL = "http://www.honnelles.be/evenements/photos/erable.png/@@images/image.png",
                Name = "Mangga",
                Likes = 500
            });
        }
    }

}