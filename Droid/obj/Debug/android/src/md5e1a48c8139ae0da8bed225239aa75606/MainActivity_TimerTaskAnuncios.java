package md5e1a48c8139ae0da8bed225239aa75606;


public class MainActivity_TimerTaskAnuncios
	extends java.util.TimerTask
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_run:()V:GetRunHandler\n" +
			"";
		mono.android.Runtime.register ("aparcame.Droid.MainActivity+TimerTaskAnuncios, aparcame.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", MainActivity_TimerTaskAnuncios.class, __md_methods);
	}


	public MainActivity_TimerTaskAnuncios ()
	{
		super ();
		if (getClass () == MainActivity_TimerTaskAnuncios.class)
			mono.android.TypeManager.Activate ("aparcame.Droid.MainActivity+TimerTaskAnuncios, aparcame.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void run ()
	{
		n_run ();
	}

	private native void n_run ();

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
