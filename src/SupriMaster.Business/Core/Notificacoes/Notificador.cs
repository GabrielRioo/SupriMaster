using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupriMaster.Business.Core.Notificacoes
{
	public class Notificador : INotificador
	{
		private List<Notificacao> _notificacoes;

		public Notificador()
		{
			_notificacoes = new List<Notificacao>();
		}

		public void Handle(Notificacao notificacao)
		{
			throw new NotImplementedException();
		}

		public List<Notificacao> ObterNotificacoes()
		{
			throw new NotImplementedException();
		}

		public bool TemNotificacao()
		{
			throw new NotImplementedException();
		}
	}
}
