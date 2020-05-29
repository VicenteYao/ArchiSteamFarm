using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASFService {
	public interface IASFLoginHandler {

		string Invoke(string steamLogin, string requestType);

	}
}
