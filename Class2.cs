using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using AnagraficaApp.Model;

namespace AnagraficaApp.ViewModel
{
    public class AnagraficaViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Persona> Persone { get; set; }
        private Persona _nuovaPersona;

        public Persona NuovaPersona
        {
            get { return _nuovaPersona; }
            set
            {
                _nuovaPersona = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CanAddPersona));
            }
        }

        public ICommand AggiungiPersonaCommand { get; set; }
        public bool CanAddPersona => !string.IsNullOrWhiteSpace(NuovaPersona?.Nome) &&
                                     !string.IsNullOrWhiteSpace(NuovaPersona?.Cognome);

        private int _nextId = 1;

        public AnagraficaViewModel()
        {
            Persone = new ObservableCollection<Persona>();
            NuovaPersona = new Persona(0, string.Empty, string.Empty);
            AggiungiPersonaCommand = new RelayCommand(AddPersona, () => CanAddPersona);
        }

        private void AddPersona()
        {
            NuovaPersona.ID = _nextId++;
            Persone.Add(new Persona(NuovaPersona.ID, NuovaPersona.Nome, NuovaPersona.Cognome)
            {
                DataNascita = NuovaPersona.DataNascita,
                Indirizzo = NuovaPersona.Indirizzo,
                Citta = NuovaPersona.Citta,
                CAP = NuovaPersona.CAP,
                Telefono = NuovaPersona.Telefono
            });
            Log($"Added new persona: {NuovaPersona.Nome} {NuovaPersona.Cognome}");
            NuovaPersona = new Persona(0, string.Empty, string.Empty);
        }

        private void Log(string message)
        {
            System.IO.File.AppendAllText("log.txt", message + "\n");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        public event EventHandler CanExecuteChanged;

        public RelayCommand(Action execute, Func<bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter) => _canExecute();
        public void Execute(object parameter) => _execute();
        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
