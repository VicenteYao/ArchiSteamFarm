using ASFService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASFHandler
{
	[Serializable]
	public class Handler : IASFLoginHandler {
		public string Invoke(string steamLogin, string requestType) {
			return "OK";
		}
	}

}
