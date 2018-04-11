using Android.Support.V7.Widget;
using Android.Views;
using Refractored.Controls;
using System;

namespace LoginApp.Features.Login
{
    public class LastUserViewHolder : RecyclerView.ViewHolder
    {
        public CircleImageView Image { get; }

        public LastUserViewHolder(View itemView, Action<int> onItemClick)
            : base(itemView)
        {
            Image = itemView.FindViewById<CircleImageView>(Resource.Id.lastUserImage);

            itemView.Click += (sender, e) => onItemClick(base.LayoutPosition);
        }
    }
}

