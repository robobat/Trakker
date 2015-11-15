using System;
using System.Collections.Generic;

namespace com.robobat.Model
{
	public class Contact
	{
		public string FullName { get; set; }
	}

	public interface IDataService
	{
		string Name{ get; set; }

		IEnumerable<Contact> GetContacts ();

		string returnDexter ();
	}

	public class DataService : IDataService
	{
		string name;

		public string Name {
			get {
				return name;	
			}
			set {
				name = value;
			}
		}

		public string returnDexter ()
		{

			return "Dexter";
		}

		public IEnumerable<Contact> GetContacts ()
		{

			return null;
		}

	}

	public interface IEventsTest
	{
		event EventHandler TestEvent;

		void ExecuteWithSideEffect ();
	}


}