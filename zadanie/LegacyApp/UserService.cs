﻿using System;

namespace LegacyApp
{
    public class UserService
    {
        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            if (!CheckIfNamesCorrect(firstName, lastName)) return false;
            
            if (!CheckIsMailCorrect(email)) return false;

            if (!CheckIsAgeOver21(dateOfBirth)) return false;

            var client = ClientCreate(firstName, lastName, email, dateOfBirth, clientId, out var user);

            CheckClientImportanceAndSetupCreditsLimits(client, user);

            if (!ChceckCreditLimit(user)) return false;

            UserDataAccess.AddUser(user);
            return true;
        }

        private Client ClientCreate(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId,
            out User user)
        {
            var clientRepository = new ClientRepository();
            var client = clientRepository.GetById(clientId);

            user = new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firstName,
                LastName = lastName
            };
            return client;
        }

        private bool ChceckCreditLimit(User user)
        {
            if (user.HasCreditLimit && user.CreditLimit < 500)
            {
                return false;
            }

            return true;
        }

        private void CheckClientImportanceAndSetupCreditsLimits(Client client, User user)
        {
            if (client.Type == "VeryImportantClient")
            {
                user.HasCreditLimit = false;
            }
            else if (client.Type == "ImportantClient")
            {
                using (var userCreditService = new UserCreditService())
                {
                    int creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                    creditLimit = creditLimit * 2;
                    user.CreditLimit = creditLimit;
                }
            }
            else
            {
                user.HasCreditLimit = true;
                using (var userCreditService = new UserCreditService())
                {
                    int creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                    user.CreditLimit = creditLimit;
                }
            }
        }

        private bool CheckIsAgeOver21(DateTime dateOfBirth)
        {
            var now = DateTime.Now;
            int age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) 
                age--;

            if (age < 21)
            {
                return false;
            }

            return true;
        }

        private bool CheckIsMailCorrect(string email)
        {
            if (!email.Contains("@") && !email.Contains("."))
            {
                return false;
            }

            return true;
        }

        private bool CheckIfNamesCorrect(string firstName, string lastName)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
            {
                return false;
            }

            return true;
        }
    }
}
