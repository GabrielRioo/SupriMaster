using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace SupriMaster.AppMvc.App_Start
{
	public class CultureConfig
	{
		public static void RegisterCulture()
		{
			var culture = new CultureInfo("pt-br");
			CultureInfo.DefaultThreadCurrentCulture = culture;
			CultureInfo.CurrentCulture = culture;
			CultureInfo.CurrentUICulture = culture;
		}
	}
}