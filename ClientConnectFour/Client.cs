using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ClientConnectFour
{
    internal class Client
    {
        // HTTP
        static private HttpClient httpClient = new HttpClient();

        // User
        public int ID { get; set; }
        private string GameID;

        // HTTP PATH 
        private const string LOGIN_PATH = "login/";
        private const string REGISTER_PATH = "users/create";
        private const string START_GAME_PATH = "initgame/";
        private const string STEP_PATH = "step/";
        private const string END_GAME_PATH = "terminategame/";


        public Client()
        {
            // Define HTTP
            httpClient.BaseAddress = new Uri("https://localhost:7055/");
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public void RegisterForward()
        {
            System.Diagnostics.Process.Start(httpClient.BaseAddress + REGISTER_PATH);
        }

        public async Task<(bool, string)> LoginUserGET(string ID)
        {
            HttpResponseMessage response = await httpClient.GetAsync(LOGIN_PATH + ID);
            if (response.IsSuccessStatusCode)
            {
                this.ID = Int32.Parse(ID);
                return (true, "Login was successful\r\nHi " + await response.Content.ReadAsStringAsync());
            }
            return (false, "Login was unsuccessful");
        }

        public async Task<(bool, string)> StartGamePOST()
        {
            HttpResponseMessage response = await httpClient.PostAsJsonAsync(START_GAME_PATH, new { jID = this.ID });

            if (response.IsSuccessStatusCode)
            {
                this.GameID = await response.Content.ReadAsStringAsync();
                return (true, "Game was start\r\n");
            }
            return (false, "Game wasn't start");
        }

        public async Task<string> EndGamePOST(bool isWin)
        {
            var enddata = new { gameID = this.GameID, userWin = isWin };
            HttpResponseMessage response = await httpClient.PostAsJsonAsync(END_GAME_PATH, enddata);
            
            if (response.IsSuccessStatusCode)
            {
                return ("Game was end\r\n");
            }
            return ("Game wasn't end");
        }

        public async Task<int> StepGET()
        {
            HttpResponseMessage response = await httpClient.GetAsync(STEP_PATH);
            if (response.IsSuccessStatusCode)
            {
                var responseCol = await response.Content.ReadAsStringAsync();
                int randomCol = int.Parse(responseCol);
                return randomCol;
            }
            return -1;
        }
    }
}
