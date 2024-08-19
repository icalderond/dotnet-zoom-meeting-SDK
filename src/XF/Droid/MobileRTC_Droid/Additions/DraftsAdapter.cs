using Android.Views;
using AndroidX.RecyclerView.Widget;

namespace US.Zoom.Zimmsg.Draft
{
    public partial class DraftsAdapter
    {
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            OnBindViewHolder(holder, position);
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            return OnCreateViewHolder(parent, viewType);
        }
    }
}