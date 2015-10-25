using System;
using com.robobat.Model;
using System.Xml;
using System.IO;

namespace com.robobat.XMLParser
{
	public class XMLParser
	{
		public TVShow myShow{ get; set; }

		public XMLParser ()
		{
			myShow = new TVShow ();

		}

		public void populateShowInitialDetails (string data)
		{
			if (data != null) {
				myShow = new TVShow ();
				myShow.fullXML = data;
				//String URLString = url;
				//String myXML = "";
				String currentElement = "";
				using (XmlReader reader = XmlReader.Create (new StringReader (data))) {
					Boolean isDone = false;

					while (reader.Read () && !isDone) {
						switch (reader.NodeType) {
						case XmlNodeType.Element:
							// The node is an element.
							//Console.Write ("<" + reader.Name);
							currentElement = reader.Name;
							while (reader.MoveToNextAttribute ()) {
								// Read the attributes.
								//Console.Write (" " + reader.Name + "='" + reader.Value + "'");
							}
							//Console.Write (">");
							//Console.WriteLine (">");
							break;
						case XmlNodeType.Text:
							//Display the text in each element.
							//Console.WriteLine (reader.Value);
							//Console.Write (" " + reader.Name + "='" + reader.Value + "'");
							switch (currentElement) {
							case "id":
								myShow.TVDBID = reader.Value;
								break;
							case "SeriesName":
								myShow.Name = reader.Value;
								break;
							case "Network":
								myShow.Network = reader.Value;
								break;
							case "poster":
								myShow.posterLink = reader.Value;
								//Console.WriteLine ("My poster link is " + myShow.posterLink);
								break;
							case "banner":
								myShow.bannerLink = reader.Value;
								break;
							case "IMDB_ID":
								myShow.IMDBID = reader.Value;
								break;
							case "Overview":
								myShow.Overview = reader.Value;
								break;
							default:
								break;
							}
							break;
						case XmlNodeType.EndElement:
							//Display the end of the element.
							if (string.Equals (reader.Name, "Series")) {
								isDone = true;
								Console.WriteLine ("I reached here");
							}
							//Console.Write ("</" + reader.Name);
							//Console.WriteLine (">");
							break;
						}
					}
				}

			}



		}
	}
}

