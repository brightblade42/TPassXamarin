using Android.Graphics;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Button), typeof(TPass.Droid.BorderButtonRenderer))]
namespace TPass.Droid
{

    public class BorderButtonRenderer : ButtonRenderer{

	protected override void OnDraw(Canvas canvas)
	{
	    base.OnDraw(canvas);
	}

	protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
	{
	    base.OnElementChanged(e);
	}
    }
}