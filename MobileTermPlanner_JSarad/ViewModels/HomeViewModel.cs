﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MobileTermPlanner_JSarad.Services;
using MobileTermPlanner_JSarad.Models;
using MobileTermPlanner_JSarad.Views;
using Plugin.LocalNotifications;
using Xamarin.Forms;

namespace MobileTermPlanner_JSarad.ViewModels
{
    public class HomeViewModel
    {
        
        public ICommand NavToTermsCommand { get; set; }
        //public ICommand DisplayNotificationsCommand { get; set; }

        public HomeViewModel()
        {
            //DatabaseService.IsBusy = true;
            //Task.Run(async() => await DatabaseService.Init());

            //DisplayNotifications();
            NavToTermsCommand = new Command(async () => await NavToTerms());
            //Task.Run(async () => { await DisplayNotifications(); });
            //DisplayNotificationsCommand = new Command(async () => await DisplayNotifications());

        }

        private async Task NavToTerms()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new TermViewPage());
        }

        //private async Task DisplayNotifications()
        //{
        //    await DatabaseService.Init();
        //    var notifications = await DatabaseService.db.Table<Notifications>().ToListAsync();
        //    if (notifications.Count > 0)
        //    {
        //        try
        //        {
        //            foreach (Notifications notification in notifications)
        //            {
        //                CrossLocalNotifications.Current.Show("Reminder", $"{notification.Type}: {notification.TypeName} is {notification.Occurrence} Today" +
        //                    $"on {notification.NotifyDate}");
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine($"{ex}");
        //        }
        //    }
        //}

        //private async void DisplayNotifications()
        //{
        //    NotificationsService notify = new NotificationsService();
        //    await notify.CheckNotifications();
        //}

        //private async Task CheckNotifications()
        //{
        //    //if (DatabaseService.IsBusy)
        //    //{
        //        await DatabaseService.Init();
        //        //await DatabaseService.FillSampleData();

        //        //List<Term> termList = await DatabaseService.GetTerms();
        //        //List<Course> courseList = await DatabaseService.GetCourses();
        //        //List<Assessment> assessmentList = await DatabaseService.GetAssessments();
        //        var termList = await DatabaseService.db.Table<Term>().ToListAsync();
        //        var courseList = await DatabaseService.db.Table<Course>().ToListAsync();
        //        var assessmentList = await DatabaseService.db.Table<Assessment>().ToListAsync();


        //        if (termList.Count > 0)
        //        {
        //            foreach (Term term in termList)
        //            {
        //                if (term.NotifyStartDate && (term.StartDate.Date == DateTime.Today))
        //                {
        //                    Notifications notification = new Notifications
        //                    {
        //                        Type = "Term",
        //                        TypeName = $"{term.Name}",
        //                        NotifyDate = term.StartDate,
        //                        Occurrence = "Upcoming"
        //                    };
        //                    await DatabaseService.AddNotification(notification);
        //                    //CrossLocalNotifications.Current.Show("Reminder", $"Upcoming Term {term.Name} starts today on {term.StartDate} ");
        //                }
        //                else if (term.NotifyEndDate && (term.EndDate.Date == DateTime.Today))
        //                {
        //                    CrossLocalNotifications.Current.Show("Reminder", $"Term {term.Name} ends today on {term.EndDate} ");
        //                }
        //            }
        //        }
        //        if (courseList.Count > 0)
        //        {
        //            foreach (Course course in courseList)
        //            {
        //                if (course.NotifyStartDate && (course.StartDate.Date == DateTime.Today))
        //                {
        //                    CrossLocalNotifications.Current.Show("Reminder", $"Upcoming Course {course.Name} starts today on {course.StartDate} ");
        //                }
        //                else if (course.NotifyEndDate && (course.EndDate.Date == DateTime.Today))
        //                {
        //                    CrossLocalNotifications.Current.Show("Reminder", $"Course {course.Name} ends today on {course.EndDate} ");
        //                }
        //            }
        //        }

        //        if (assessmentList.Count > 0)
        //        {
        //            foreach (Assessment assessment in assessmentList)
        //            {
        //                if (assessment.NotifyStartDate && (assessment.StartDate.Date == DateTime.Today))
        //                {
        //                    CrossLocalNotifications.Current.Show("Reminder", $"Upcoming Assessment {assessment.Name} that starts today on {assessment.StartDate} ");
        //                }
        //                else if (assessment.NotifyEndDate && (assessment.EndDate.Date == DateTime.Today))
        //                {
        //                    CrossLocalNotifications.Current.Show("Reminder", $"Assessment {assessment.Name} ends today on {assessment.EndDate} ");
        //                }
        //            }
        //        }
        //        //DatabaseService.IsBusy = false;
        //    //}
        //}
    }
}
