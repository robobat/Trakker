using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace com.robobat.Model
{
	public class MainPageViewModel
	{
		public ObservableCollection<Contact> Contacts { get; set; }

		public MainPageViewModel (IDataService dataService)
		{
			Contacts = new ObservableCollection<Contact> ();
			IEnumerable<Contact> contactsList = dataService.GetContacts ();
			foreach (Contact contact in contactsList) {
				Contacts.Add (contact);
			}
		}
	}
}

