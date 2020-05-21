/*
	This C# source code was automatically generated using:

		SQL Server Centric .NET Code Generator
			v 1.0.1697.21165

			Generation Date: 28/12/2004 11:53:15
			Generator name: MAINSERVER\Administrator
			Template last update: 27/12/2004 16:39:29
			Template revision: 324

			SQL Server version: 08.00.0760
			Server: MAINSERVER\MAINSERVER
			Database: [OlymarsDemo]

	WARNING: This source is provided "AS IS" without warranty of any kind.
	The author disclaims all warranties, either express or implied, including
	the warranties of merchantability and fitness for a particular purpose.
	In no event shall the author or its suppliers be liable for any damages
	whatsoever including direct, indirect, incidental, consequential, loss of
	business profits or special damages, even if the author or its suppliers
	have been advised of the possibility of such damages.

	    More information: http://www.microsoft.com/france/msdn/olymars
	Latest interim build: http://www.olymars.net/latest.zip
	       Author's blog: http://blogs.msdn.com/olymars
*/

using System;

namespace OlymarsDemo.WebServices.WSTypes {

	/// <summary>
	/// [To be supplied.]
	/// </summary>
	public interface IWSType {

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		bool UseDefault { get; set; }
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		bool UseNull { get; set; }
	}

	/// <summary>
	/// [To be supplied.]
	/// </summary>
	public class WSInt16 : IWSType {

		private bool useDefault = false;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public bool UseDefault {
		
			get {
		
				return(useDefault);
			}
			set  {
		
				useDefault = value;
			}
		}

		private bool useNull = true;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public bool UseNull {
		
			get {
		
				return(useNull);
			}
			set  {
		
				useNull = value;
			}
		}
		
		private System.Int16 internalValue;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public System.Int16 Value {

			get {

				return(internalValue);
			}
			set {

				internalValue = value;
			}
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public static WSInt16 Null {

			get {

				WSInt16 typedValue = new WSInt16();
				typedValue.UseNull = true;
				return(typedValue);
			}
		}
	}

	/// <summary>
	/// [To be supplied.]
	/// </summary>
	public class WSInt32 : IWSType {

		private bool useDefault = false;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public bool UseDefault {
		
			get {
		
				return(useDefault);
			}
			set  {
		
				useDefault = value;
			}
		}

		private bool useNull = true;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public bool UseNull {
		
			get {
		
				return(useNull);
			}
			set  {
		
				useNull = value;
			}
		}
		
		private System.Int32 internalValue;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public System.Int32 Value {

			get {

				return(internalValue);
			}
			set {

				internalValue = value;
			}
		}
	}

	/// <summary>
	/// [To be supplied.]
	/// </summary>
	public class WSBinary : IWSType {

		private bool useDefault = false;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public bool UseDefault {
		
			get {
		
				return(useDefault);
			}
			set  {
		
				useDefault = value;
			}
		}

		private bool useNull = true;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public bool UseNull {
		
			get {
		
				return(useNull);
			}
			set  {
		
				useNull = value;
			}
		}
		
		private System.Byte[] internalValue;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public System.Byte[] Value {

			get {

				return(internalValue);
			}
			set {

				internalValue = value;
			}
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public static WSBinary Null {

			get {

				WSBinary typedValue = new WSBinary();
				typedValue.UseNull = true;
				return(typedValue);
			}
		}
	}

	/// <summary>
	/// [To be supplied.]
	/// </summary>
	public class WSString : IWSType {

		private bool useDefault = false;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public bool UseDefault {
		
			get {
		
				return(this.useDefault);
			}
			set  {
		
				this.useDefault = value;
			}
		}

		private bool useNull = true;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public bool UseNull {
		
			get {
		
				return(this.useNull);
			}
			set  {
		
				this.useNull = value;
			}
		}
		
		private System.String internalValue;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public System.String Value {

			get {

				return(this.internalValue);
			}
			set {

				this.internalValue = value;
			}
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public static WSString Null {

			get {

				WSString typedValue = new WSString();
				typedValue.UseNull = true;
				return(typedValue);
			}
		}
	}

	/// <summary>
	/// [To be supplied.]
	/// </summary>
	public class WSDecimal : IWSType {

		private bool useDefault = false;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public bool UseDefault {
		
			get {
		
				return(this.useDefault);
			}
			set  {
		
				this.useDefault = value;
			}
		}

		private bool useNull = true;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public bool UseNull {
		
			get {
		
				return(this.useNull);
			}
			set  {
		
				this.useNull = value;
			}
		}
		
		private System.Decimal internalValue;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public System.Decimal Value {

			get {

				return(this.internalValue);
			}
			set {

				this.internalValue = value;
			}
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public static WSDecimal Null {

			get {

				WSDecimal typedValue = new WSDecimal();
				typedValue.UseNull = true;
				return(typedValue);
			}
		}
	}

	/// <summary>
	/// [To be supplied.]
	/// </summary>
	public class WSDouble : IWSType {

		private bool useDefault = false;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public bool UseDefault {
		
			get {
		
				return(this.useDefault);
			}
			set  {
		
				this.useDefault = value;
			}
		}

		private bool useNull = true;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public bool UseNull {
		
			get {
		
				return(this.useNull);
			}
			set  {
		
				this.useNull = value;
			}
		}
		
		private System.Double internalValue;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public System.Double Value {

			get {

				return(this.internalValue);
			}
			set {

				this.internalValue = value;
			}
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public static WSDouble Null {

			get {

				WSDouble typedValue = new WSDouble();
				typedValue.UseNull = true;
				return(typedValue);
			}
		}
	}

	/// <summary>
	/// [To be supplied.]
	/// </summary>
	public class WSDateTime : IWSType {

		private bool useDefault = false;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public bool UseDefault {
		
			get {
		
				return(this.useDefault);
			}
			set  {
		
				this.useDefault = value;
			}
		}

		private bool useNull = true;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public bool UseNull {
		
			get {
		
				return(this.useNull);
			}
			set  {
		
				this.useNull = value;
			}
		}
		
		private System.DateTime internalValue;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public System.DateTime Value {

			get {

				return(this.internalValue);
			}
			set {

				this.internalValue = value;
			}
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public static WSDateTime Null {

			get {

				WSDateTime typedValue = new WSDateTime();
				typedValue.UseNull = true;
				return(typedValue);
			}
		}
	}

	/// <summary>
	/// [To be supplied.]
	/// </summary>
	public class WSGuid : IWSType {

		private bool useDefault = false;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public bool UseDefault {
		
			get {
		
				return(this.useDefault);
			}
			set  {
		
				this.useDefault = value;
			}
		}

		private bool useNull = true;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public bool UseNull {
		
			get {
		
				return(this.useNull);
			}
			set  {
		
				this.useNull = value;
			}
		}
		
		private System.Guid internalValue;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public System.Guid Value {

			get {

				return(this.internalValue);
			}
			set {

				this.internalValue = value;
			}
		}
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public static WSGuid Null {

			get {

				WSGuid typedValue = new WSGuid();
				typedValue.UseNull = true;
				return(typedValue);
			}
		}
	}

	/// <summary>
	/// [To be supplied.]
	/// </summary>
	public class WSBoolean : IWSType {

		private bool useDefault = false;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public bool UseDefault {
		
			get {
		
				return(this.useDefault);
			}
			set  {
		
				this.useDefault = value;
			}
		}

		private bool useNull = true;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public bool UseNull {
		
			get {
		
				return(this.useNull);
			}
			set  {
		
				this.useNull = value;
			}
		}
		
		private System.Boolean internalValue;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public System.Boolean Value {

			get {

				return(this.internalValue);
			}
			set {

				this.internalValue = value;
			}
		}
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public static WSBoolean Null {

			get {

				WSBoolean typedValue = new WSBoolean();
				typedValue.UseNull = true;
				return(typedValue);
			}
		}
	}
}
