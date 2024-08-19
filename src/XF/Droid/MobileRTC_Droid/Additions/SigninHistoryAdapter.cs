using Android.Views;
using AndroidX.RecyclerView.Widget;

namespace Com.Zipow.Videobox.Fragment.Adapter
{
    public partial class SigninHistoryAdapter 
    {
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            OnBindViewHolder((Annotate.AnnoMultiPage.AnnoMultiPageAdapter.WhiteboardViewHolder)holder, position);
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            return OnCreateViewHolder(parent, viewType);
        }
    }
}