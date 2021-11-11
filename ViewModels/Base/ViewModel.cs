using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PoloniexBot.ViewModels.Base
{
	internal abstract class ViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged([CallerMemberName] string ProperyName = null)
		{
			PropertyChanged!.Invoke(this, new PropertyChangedEventArgs(ProperyName));
		}

		protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string ProperyName = null)
		{
			if(Equals(field, value))
			{
				return false;
			}
			field = value;
			this.OnPropertyChanged(ProperyName);
			return true;
		}
	}
}
