package com.aparcame.aparcame;


public class BackgroundService_TimerTaskToGetLocation
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
		mono.android.Runtime.register ("aparcame.Droid.Services.BackgroundService+TimerTaskToGetLocation, aparcame.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", BackgroundService_TimerTaskToGetLocation.class, __md_methods);
	}


	public BackgroundService_TimerTaskToGetLocation () throws java.lang.Throwable
	{
		super ();
		if (getClass () == BackgroundService_TimerTaskToGetLocation.class)
			mono.android.TypeManager.Activate ("aparcame.Droid.Services.BackgroundService+TimerTaskToGetLocation, aparcame.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
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
