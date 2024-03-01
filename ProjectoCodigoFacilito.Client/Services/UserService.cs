﻿using ProjectoCodigoFacilito.Client.Models.UserModel;
using ProjectoCodigoFacilito.Client.Services.Interfaces;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;

namespace ProjectoCodigoFacilito.Client.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;
        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> CreateUser(CreateUserModel user) //Sign up
        {
            try
            {
                // Validar que las contraseñas coincidan
                if (user.Password != user.ConfirmPassword)
                {
                    return "Error: Passwords do not match";
                }

                byte[] bytesUserName = Encoding.UTF8.GetBytes(user.Name);
                byte[] bytesEmail = Encoding.UTF8.GetBytes(user.Email);
                byte[] bytesPassword = Encoding.UTF8.GetBytes(user.Password);
                user.Name = Convert.ToBase64String(bytesUserName);
                user.Email = Convert.ToBase64String(bytesEmail);
                user.Password = Convert.ToBase64String(bytesPassword);

                var response = await _httpClient.PostAsJsonAsync("api/User", user);
                response.EnsureSuccessStatusCode();

                if(!response.IsSuccessStatusCode)
                {
                    throw new ApplicationException(response.Content.ToString());
                }

                return "Ok";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        public async Task<string> SignInUser(SignInUserModel user)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/User/check", user);
                response.EnsureSuccessStatusCode();
                
                if(!response.IsSuccessStatusCode)
                {
                    throw new ApplicationException(response.Content.ToString());
                }
                
            
                return "Ok";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }

        }
    }
}