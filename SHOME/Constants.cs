using System.Collections.Generic;
namespace SHOME
{
	public class Constants 
	{
		public static string Username = "jorge";
		public static string Password = "123";
	}
}

public class Home
{
	private List<Division> divisions = new List<Division>();

	public List<Division> MyList
	{
		get { return divisions; }
	}

	public void add(Division div)
	{
		divisions.Add(div);
	}


}

public class Division
{
	public string Name { get; set; }
	public string Type { get; set; }
	public Division(string name, string type)
	{
		Name = name;
		Type = type;
	}
	//Other properties, methods, events...
}

