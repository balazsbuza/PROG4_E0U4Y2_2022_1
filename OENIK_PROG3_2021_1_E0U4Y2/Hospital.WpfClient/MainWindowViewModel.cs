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
using Hospital.Logic;

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

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public ICollection<ClinicGender> ClinicGenders
        {
            get { return Rest.Get<ClinicGender>("/Stat/ClinicGender"); }
        }

        public IList<DoctorWorkaddress> DoctorWorkAddress
        {
            get { return Rest.Get<DoctorWorkaddress>("/Stat/DoctorWorkAddress"); }
        }

        public IList<PatientLastTreatment> PatientLastTreatment
        {
            get { return Rest.Get<PatientLastTreatment>("/Stat/PatientLastTreatment"); }
        }

        public IList<DoctorOfficeHours> DoctorOfficeHours
        {
            get { return Rest.Get<DoctorOfficeHours>("/Stat/DoctorOfficeHours"); }
        }

        public IList<PatientTreatmentLastYear> PatientTreatmentLastYear
        {
            get { return Rest.Get<PatientTreatmentLastYear>("/Stat/PatientTreatmentLastYear"); }
        }

        

        RestService Rest;

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
                        ClinicId = value.ClinicId,
                        Address = value.Address
                        //Officehours = value.Officehours,
                        //Phone = value.Phone,
                        //Company = value.Company
                    };
                    OnPropertyChanged();
                    OnPropertyChanged("PatientLastTreatment");
                    OnPropertyChanged("ClinicGenders");
                    OnPropertyChanged("DoctorWorkAddress");
                    OnPropertyChanged("DoctorOfficeHours");
                    OnPropertyChanged("PatientTreatmentLastYear");
                    (DeleteClinicCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }


        public ICommand CreateClinicCommand { get; set; }

        public ICommand DeleteClinicCommand { get; set; }

        public ICommand UpdateClinicCommand { get; set; }

        public RestCollection<Patient> Patients { get; set; }

        private Patient selectedPatient;

        public Patient SelectedPatient
        {
            get { return selectedPatient; }
            set
            {
                if (value != null)
                {
                    selectedPatient = new Patient()
                    {
                        Name = value.Name,
                        PatientId = value.PatientId,
                        Disease = value.Disease,
                        Gender = value.Gender,
                        Dateofbirth = value.Dateofbirth,
                        Nameofmother = value.Nameofmother,
                        DoctorId = value.DoctorId
                    };
                    OnPropertyChanged();
                    OnPropertyChanged("PatientLastTreatment");
                    OnPropertyChanged("ClinicGenders");
                    OnPropertyChanged("DoctorWorkAddress");
                    OnPropertyChanged("DoctorOfficeHours");
                    OnPropertyChanged("PatientTreatmentLastYear");
                    (CreateTreatmentCommand as RelayCommand).NotifyCanExecuteChanged();
                    (DeletePatientCommand as RelayCommand).NotifyCanExecuteChanged();
                }                
            }
        }


        public ICommand CreatePatientCommand { get; set; }

        public ICommand DeletePatientCommand { get; set; }

        public ICommand UpdatePatientCommand { get; set; }

        public RestCollection<Treatment> Treatments { get; set; }

        private Treatment selectedTreatment;

        public Treatment SelectedTreatment
        {
            get { return selectedTreatment; }
            set
            {
                if (value != null)
                {
                    selectedTreatment = new Treatment()
                    {
                        DoctorId = value.DoctorId,
                        PatientId = value.PatientId,
                        TreatmentId = value.TreatmentId,
                        Description = value.Description
                    };
                    OnPropertyChanged();
                    OnPropertyChanged("PatientLastTreatment");
                    OnPropertyChanged("ClinicGenders");
                    OnPropertyChanged("DoctorWorkAddress");
                    OnPropertyChanged("DoctorOfficeHours");
                    OnPropertyChanged("PatientTreatmentLastYear");
                    (DeleteTreatmentCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }


        public ICommand CreateTreatmentCommand { get; set; }

        public ICommand DeleteTreatmentCommand { get; set; }

        public ICommand UpdateTreatmentCommand { get; set; }

        public RestCollection<Doctor> Doctors { get; set; }

        private Doctor selectedDoctor;

        public Doctor SelectedDoctor
        {
            get { return selectedDoctor; }
            set
            {
                if (value != null)
                {
                    selectedDoctor = new Doctor()
                    {
                        Name = value.Name,
                        DoctorId = value.DoctorId,
                    };
                    OnPropertyChanged();
                    (CreatePatientCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }


        public MainWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                Rest = new RestService("http://localhost:43747/");

                Doctors = new RestCollection<Doctor>("http://localhost:43747/", "Doctor", "hub");

                Clinics = new RestCollection<Clinic>("http://localhost:43747/", "Clinic", "hub");

                Patients = new RestCollection<Patient>("http://localhost:43747/", "Patient", "hub");

                Treatments = new RestCollection<Treatment>("http://localhost:43747/", "Treatment", "hub");

                CreateClinicCommand = new RelayCommand(() =>
                {
                    Clinics.Add(new Clinic()
                    {
                        Name = SelectedClinic.Name,
                        Address = SelectedClinic.Address
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

                CreateTreatmentCommand = new RelayCommand(() =>
                {
                    Treatments.Add(new Treatment()
                    {
                        Description = SelectedTreatment.Description,
                        PatientId = SelectedPatient.PatientId,
                        DoctorId = SelectedPatient.DoctorId,
                        Treatmenttime = DateTime.Now
                    });
                },
                () => SelectedPatient != null && SelectedDoctor != null
                );

                UpdateTreatmentCommand = new RelayCommand(() =>
                {
                    try
                    {
                        Treatments.Update(SelectedTreatment);
                    }
                    catch (ArgumentException ex)
                    {
                        ErrorMessage = ex.Message;
                    }

                });

                DeleteTreatmentCommand = new RelayCommand(() =>
                {
                    Treatments.Delete(SelectedTreatment.TreatmentId);
                },
                () =>
                {
                    return SelectedTreatment != null;
                });
                SelectedTreatment = new Treatment();              

                CreatePatientCommand = new RelayCommand(() =>
                {
                    Patients.Add(new Patient()
                    {
                        DoctorId = SelectedDoctor.DoctorId,
                        Name = SelectedPatient.Name,
                        Disease = SelectedPatient.Disease,
                        Gender = "M",
                        Dateofbirth = SelectedPatient.Dateofbirth,
                        Nameofmother = SelectedPatient.Nameofmother
                    });
                },
                () => SelectedDoctor != null
                );

                UpdatePatientCommand = new RelayCommand(() =>
                {
                    try
                    {
                        Patients.Update(SelectedPatient);
                    }
                    catch (ArgumentException ex)
                    {
                        ErrorMessage = ex.Message;
                    }

                });

                DeletePatientCommand = new RelayCommand(() =>
                {
                    Patients.Delete(SelectedPatient.PatientId);
                    Treatments = new RestCollection<Treatment>("http://localhost:43747/", "Treatment", "hub");
                },
                () =>
                {
                    return SelectedPatient != null;
                });
                SelectedPatient = new Patient();
            }
            
        }
    }
}
