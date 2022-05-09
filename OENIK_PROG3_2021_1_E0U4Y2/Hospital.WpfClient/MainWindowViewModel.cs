using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Hospital.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Hospital.Data.Tables;

namespace Hospital.WpfClient
{
    public class MainWindowViewModel : ObservableRecipient
    {
        private string errorMessage;

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }


        public RestCollection<Clinic> Clinics { get; set; }

        private Clinic selectedClinic;

        public Clinic SelectedClinic
        {
            get { return selectedClinic; }
            set
            {
                if (value != null)
                {
                    selectedClinic = new Clinic()
                    {
                        Name = value.Name,
                        ClinicId = value.ClinicId
                    };
                    OnPropertyChanged();
                    (DeleteClinicCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }


        public ICommand CreateClinicCommand { get; set; }

        public ICommand DeleteClinicCommand { get; set; }

        public ICommand UpdateClinicCommand { get; set; }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }


        public MainWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                Clinics = new RestCollection<Clinic>("http://localhost:43747/", "Clinic", "hub");
                CreateClinicCommand = new RelayCommand(() =>
                {
                    Clinics.Add(new Clinic()
                    {
                        Name = SelectedClinic.Name
                    });
                });

                UpdateClinicCommand = new RelayCommand(() =>
                {
                    try
                    {
                        Clinics.Update(SelectedClinic);
                    }
                    catch (ArgumentException ex)
                    {
                        ErrorMessage = ex.Message;
                    }
                    
                });

                DeleteClinicCommand = new RelayCommand(() =>
                {
                    Clinics.Delete(SelectedClinic.ClinicId);
                },
                () =>
                {
                    return SelectedClinic != null;
                });
                SelectedClinic = new Clinic();
            }
            
        }
    }
}
