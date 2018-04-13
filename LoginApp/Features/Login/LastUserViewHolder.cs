using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Refractored.Controls;
using System;

namespace LoginApp.Features.Login
{
    public class LastUserViewHolder : RecyclerView.ViewHolder
    {
        public CircleImageView Image { get; }
        public TextView Name { get; }

        public LastUserViewHolder(View itemView, Action<int> onItemClick)
            : base(itemView)
        {
            Image = itemView.FindViewById<CircleImageView>(Resource.Id.lastUserImage);
            Name = itemView.FindViewById<TextView>(Resource.Id.name);

            itemView.Click += (sender, e) => onItemClick(base.LayoutPosition);
        }
    }
}

