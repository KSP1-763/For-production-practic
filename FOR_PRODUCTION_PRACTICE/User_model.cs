using System;

public class Class1
{
	public Class1()
	{
		public int ID {  get; set; }
		public string Loging {  get; set; }
		public string Password { get; set; }
		public string LastName { get; set; }
		public string FirstName { get; set; }
		public string MiddleName { get; set; }
		public string position { get; set; }
		public string PhoneNumber { get; set; }
		public string Email { get; set; }
		public string Fullname => $"{LasttName} {FirstName} {MiddleName}".Trim();
	public string Shortname => $"{LastName} {FirstName[0]} {MiddleName[0]}".Trim(' ','.');
	}
}
