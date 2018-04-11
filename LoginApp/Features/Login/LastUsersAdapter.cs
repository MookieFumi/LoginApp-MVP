using System.Collections.Generic;
using Android.Support.V7.Widget;
using Android.Support.V7.App;
using Android.Views;
using System;
using System.Linq;

namespace LoginApp.Features.Login
{
    public class LastUsersAdapter : RecyclerView.Adapter
    {
        private readonly AppCompatActivity _context;
        private Dictionary<string, string> _users;
        public event EventHandler<string> ItemClick;

        public LastUsersAdapter(AppCompatActivity context, Dictionary<string, string> users)
        {
            _context = context;
            _users = users;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var itemView = LayoutInflater.From(_context).Inflate(Resource.Layout.LastUserView, parent, false);
            return new LastUserViewHolder(itemView, OnItemClick);
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var viewHolder = holder as LastUserViewHolder;

            var relocationItem = _users.ElementAt(position);
            viewHolder.Image.SetImageResource(Resource.Drawable.ic_profile);
        }

        public override int ItemCount => _users.Count;

        public void OnItemClick(int position)
        {
            var item = _users.ElementAt(position);
            ItemClick?.Invoke(this, item.Key);
        }
    }
}

