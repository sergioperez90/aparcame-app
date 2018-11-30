package md57574ea7997e8a871fc54d9e08899937e;


public class BaseActivity
	extends android.support.v7.app.AppCompatActivity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onOptionsItemSelected:(Landroid/view/MenuItem;)Z:GetOnOptionsItemSelected_Landroid_view_MenuItem_Handler\n" +
			"";
		mono.android.Runtime.register ("aparcame.Droid.Activities.BaseActivity, aparcame.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", BaseActivity.class, __md_methods);
	}


	public BaseActivity ()
	{
		super ();
		if (getClass () == BaseActivity.class)
			mono.android.TypeManager.Activate ("aparcame.Droid.Activities.BaseActivity, aparcame.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public boolean onOptionsItemSelected (android.view.MenuItem p0)
	{
		return n_onOptionsItemSelected (p0);
	}

	private native boolean n_onOptionsItemSelected (android.view.MenuItem p0);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
