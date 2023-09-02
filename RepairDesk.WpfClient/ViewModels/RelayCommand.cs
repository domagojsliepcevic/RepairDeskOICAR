﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RepairDesk.WpfClient.ViewModels
{
	public class RelayCommand : ICommand
	{
		private readonly Action<object> execute;

		public RelayCommand(Action<object> execute)
		{
			this.execute = execute;
		}

		public event EventHandler CanExecuteChanged
		{
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}

		public bool CanExecute(object parameter)
		{
			return true;
		}

		public void Execute(object parameter)
		{
			execute(parameter);
		}
	}
}
