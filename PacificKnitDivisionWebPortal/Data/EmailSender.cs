﻿using Microsoft.AspNetCore.Identity.UI.Services;

namespace OnlineBookOrderManagementSystem.Data
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            //logic to send email
            return Task.CompletedTask;
        }
    }
}
