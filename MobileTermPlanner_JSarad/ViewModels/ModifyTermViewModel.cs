﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MobileTermPlanner_JSarad.Models;
using Xamarin.Forms;
using MobileTermPlanner_JSarad.Services;

namespace MobileTermPlanner_JSarad.ViewModels
{
    public class ModifyTermViewModel : BaseViewModel
    {
        //properties

        public string AddEdit { get; set; }
        public List<Term> TermList { get; set; }
        
        private Term _term;
        public Term Term
        {
            get
            {
                return _term;
            }
            set
            {
                _term = value;
                OnPropertyChanged();
            }
        }
        
        public string TermName
        {
            get
            {
                return _term.Name;
            }
            set
            {
                _term.Name = value;
                OnPropertyChanged();
                ValidString(TermName);
                EmptyErrorMessageOne = ValidationMessage;
            }
        }

        public DateTime StartDate
        {
            get
            {
                return _term.StartDate;
            }
            set
            {
                _term.StartDate = value;
                OnPropertyChanged();
                ValidDates(StartDate, EndDate);
                DatesErrorMessageOne = ValidationMessage;
            }
        }

        public DateTime EndDate
        {
            get
            {
                return _term.EndDate;
            }
            set
            {
                _term.EndDate = value;
                OnPropertyChanged();
                ValidDates(StartDate, EndDate);
                DatesErrorMessageOne = ValidationMessage;
            }
        }

        //public bool NotifyStart
        //{
        //    get
        //    {
        //        return _term.NotifyStartDate;
        //    }
        //    set
        //    {
        //        _term.NotifyStartDate = value;
        //        OnPropertyChanged();
        //        UpdateNotifyLabel(NotifyStart, "Start");
        //        StartDateLabel = ValidationMessage;

        //    }
        //}

        //public bool NotifyEnd
        //{
        //    get
        //    {
        //        return _term.NotifyEndDate;
        //    }
        //    set
        //    {
        //        _term.NotifyEndDate = value;
        //        OnPropertyChanged();
        //        UpdateNotifyLabel(NotifyEnd, "End");
        //        EndDateLabel = ValidationMessage;
        //    }
        //}


        private string _overlapMesssage;
        public string OverlapMessage
        {
            get
            {
                return _overlapMesssage;
            }
            set
            {
                _overlapMesssage = value;
                OnPropertyChanged();
            }
        }

        public bool IsValidInput;
        
        //commands
        public ICommand SaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        
        public ModifyTermViewModel()
        {
            if (DatabaseService.IsAdd)
            {
                AddEdit = "Add Term";
                Term = new Term();
                StartDate = DateTime.Now;
                EndDate = StartDate.AddDays(180);
                //NotifyStart = false;
                //NotifyEnd = false;
            }
            else
            {
                AddEdit = "Edit Term";
                Term = DatabaseService.CurrentTerm;
            }
          
            SaveCommand = new Command(async () => await SaveTerm());
            CancelCommand = new Command(async () => await CancelTerm());
        }

        //methods
        private async Task SaveTerm()
        {
            TermList = await DatabaseService.GetTerms();
            IsValidInput = true;

            //validates unchanged properties and displays any errors to user on save attempt
            ValidString(TermName);
            EmptyErrorMessageOne = ValidationMessage;

            //checks for overlapping Terms
            if (TermList.Count > 0)
            {
                for (int i = 0; i < TermList.Count; i++)
                {
                    if (((TermList[i].StartDate <= Term.EndDate && TermList[i].StartDate >= Term.StartDate) ||
                        (TermList[i].EndDate <= Term.EndDate && TermList[i].EndDate >= Term.StartDate)) && (TermList[i].Id != Term.Id))
                    {
                        IsValidInput = false;
                        await Application.Current.MainPage.DisplayAlert("Overlapping Course", $" * There is an overlapping term for these " +
                            $"dates for Term { TermList[i].Name} from { TermList[i].StartDate.Date} to {TermList[i].EndDate.Date}", "Ok");
                    }
                }
            }

            //saves term if all validations return true
            if (IsValidInput && ValidString(TermName) && ValidDates(StartDate, EndDate))
            {
                if (DatabaseService.IsAdd)
                {
                    MessagingCenter.Send(this, "AddTerm", Term);
                    await Application.Current.MainPage.Navigation.PopAsync();
                }
                else
                {
                    MessagingCenter.Send(this, "UpdateTerm", Term);
                    await Application.Current.MainPage.Navigation.PopAsync();
                }
            }
        }

        private async Task CancelTerm()
        {
            //MessagingCenter.Send(this, "CancelChanges");
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
