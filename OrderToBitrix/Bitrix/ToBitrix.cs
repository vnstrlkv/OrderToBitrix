using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;

namespace OrderToBitrix.Bitrix
{
    public class ToBitrix
    {

        private static void WriteLog(object obj)
        {
            string writePath = "logBitrix.txt";

            string text = obj.ToString();
            try
            {
                using (StreamWriter sw = new StreamWriter(writePath, true, System.Text.Encoding.Default))
                {
                    sw.WriteLine(text);
                }


                Console.WriteLine("Запись выполнена");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        //  'tasks.task.add', 
        // {fields:{TITLE:'task for test', RESPONSIBLE_ID:1}
        //  Dictionary<string,int> Staff = new Dictionary<string, int>();

        private static string GetStaffID(string Department)
        {
            Console.WriteLine(Department + " StafId");
            string id = null;
            switch (Department)
            {
                case "Мебель Sale":
                    {
                        id = "32";
                        Console.WriteLine(Department + " StafId");
                        break;
                    }
                case "Отдел Фасада":
                    {
                        id = "34";
                        Console.WriteLine(Department + " StafId");
                        break;
                    }
                case "Отдел Камня":
                    {
                        id = "34";
                        Console.WriteLine(Department + " StafId");
                        break;
                    }
                default:
                    break;
            }


            return id;
        }

        
        private static string GetGroupID(string Department)
        {
            Console.WriteLine(Department + " GroupID");
            string id = null;
            switch (Department)
            {
                case "Мебель Sale":
                    {
                        id = "20";
                        Console.WriteLine(Department + " GroupID");
                        break;
                    }
                case "Отдел Фасада":
                    {
                        id = "50";
                        Console.WriteLine(Department + " GroupID");
                        break;
                    }
                case "Отдел Камня":
                    {
                        id = "50";
                        Console.WriteLine(Department + " GroupID");
                        break;
                    }
                default:
                    break;
            }


            return id;
        }


        private static async Task<string> AddCheklistToBitrixTask(string ID, string NAME, string Person)
        {
           
                   
            INIManager BTX = new INIManager("c:\\config.ini");
            string BitrixName = BTX.GetPrivateString("BTX", "logPD");
            string BitrixPass = BTX.GetPrivateString("BTX", "pasPD");
            string task = "task.checklistitem.add";

            string url = "https://" + BitrixName + "/rest/32/" + BitrixPass + "/" + task;
            // Serialize our concrete class into a JSON String

            var stringPayload = await Task.Run(() => JsonConvert.SerializeObject(

                new
                {
                    TASKID=ID,
                    FIELDS = new[]
                    {                     
                        
                        new{    TITLE = NAME+" " +"@"+Person,
                        SORT_INDEX=1,
                            IS_COMPLETE = "N" },
                        new{    TITLE = NAME+"2323 " +"@"+Person,
                        SORT_INDEX=2,
                            IS_COMPLETE = "Y" },

                    }

                }

                ));
            var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {

                // Do the actual request and await the response
                var httpResponse = await httpClient.PostAsync(url, httpContent);
                // var httpResponse = await httpClient.GetAsync(url);
                // If the response contains content we want to read it!
                if (httpResponse.Content != null)
                {
                    var responseContent = await httpResponse.Content.ReadAsStringAsync();
                    WriteLog(responseContent);
                    Console.WriteLine(responseContent);
                    Console.WriteLine(DateTime.Now + " : " + task);
                    ID = responseContent;
                    // From here on you could deserialize the ResponseContent back again to a concrete C# type using Json.Net
                }

            }
            //  WriteLog(ID);
            ID = Regex.Match(Regex.Match(ID, @"""ID"":""\d+").Value, @"\d+").Value;
            //    WriteLog(ID);
            Console.WriteLine(DateTime.Now + " : " + ID);
            return ID;

        }

        private static async Task<List<object>> OrderToBitrixFormat(Order currentOrder)
        {
            string _TITLE, _DESCRIPTION, _DEADLINE,  _ALLOW_CHANGE_DEADLINE, _GROUP_ID, _RESPONSIBLE_ID, _CREATED_BY, _TASK_CONTROL;
            string stringPayload;
            if (true) //проверка на вторичный заказ
            {

                _TITLE = Regex.Match(currentOrder.Number, @"\d+").Value.ToString().TrimStart('0') + "_" + currentOrder.Comment + " " + currentOrder.Organization;
                if (currentOrder.SecondaryOrder != "")
                {
                    _TITLE = _TITLE + "_" + Regex.Match(currentOrder.SecondaryOrder, @"\d+").Value.ToString().TrimStart('0');
                }
                var IDtask = await GetIdTaskFromBitrix(_TITLE);
                
                {
                    
                    _DESCRIPTION = $"{currentOrder.TextTZ}\n\n";
                    _DEADLINE = currentOrder.DatePass;
                    _ALLOW_CHANGE_DEADLINE = "N";
                    _TASK_CONTROL = "Y";
                    _GROUP_ID = GetGroupID(currentOrder.Department);
                    _RESPONSIBLE_ID = GetStaffID(currentOrder.Department);
                    _CREATED_BY = await GetIdStaffFromBitrix(currentOrder.Manager);
                    if (IDtask == "")
                    {
                         stringPayload = await Task.Run(() => JsonConvert.SerializeObject(

               new
               {
                   fields = new
                   {
                       TITLE = _TITLE,
                       DESCRIPTION = _DESCRIPTION,
                       DEADLINE = _DEADLINE,
                       ALLOW_CHANGE_DEADLINE = _ALLOW_CHANGE_DEADLINE,
                       GROUP_ID = _GROUP_ID,
                       RESPONSIBLE_ID = _RESPONSIBLE_ID,
                       CREATED_BY = _CREATED_BY,
                       ACCOMPLICES= new[] {"576"},
                      AUDITORS = new[] {"26", "22", "108"}

                   }

               }


                       ));
                    }
                    else
                    {
                        stringPayload = await Task.Run(() => JsonConvert.SerializeObject(

             new
             {
                 taskId = IDtask,
                 fields = new
                 {
                     TITLE = _TITLE,
                     DESCRIPTION = _DESCRIPTION,
                     DEADLINE = _DEADLINE,
                     ALLOW_CHANGE_DEADLINE = _ALLOW_CHANGE_DEADLINE,
                     TASK_CONTROL = _TASK_CONTROL,
                      GROUP_ID = _GROUP_ID,
                      RESPONSIBLE_ID = _RESPONSIBLE_ID,
                      CREATED_BY = _CREATED_BY,
                  }

              }

               ));
                    }

                        return new List<object>() { stringPayload, IDtask }; 
                }
           
            }
            return null;

        }


        private static async Task<string> GetIdStaffFromBitrix(string Name)
        {
            string[] mystring = Name.Split(' ');
            string _NAME=mystring[1], _LAST_NAME= mystring[0];

            string ID=null;
            INIManager BTX = new INIManager("c:\\config.ini");
            string BitrixName = BTX.GetPrivateString("BTX", "logPD");
            string BitrixPass = BTX.GetPrivateString("BTX", "pasPD");
            string task = "user.search.json";
            
            string url = "https://" + BitrixName + "/rest/32/" + BitrixPass + "/"+task;
            // Serialize our concrete class into a JSON String

            var stringPayload = await Task.Run(() => JsonConvert.SerializeObject(

                new
                {
                    FILTER = new
                   {
                       NAME = _NAME,
                       LAST_NAME= _LAST_NAME,
                    }

                }
                
                ));
            var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {

                // Do the actual request and await the response
                  var httpResponse = await httpClient.PostAsync(url, httpContent);
               // var httpResponse = await httpClient.GetAsync(url);
                // If the response contains content we want to read it!
                if (httpResponse.Content != null)
                {
                    var responseContent = await httpResponse.Content.ReadAsStringAsync();
                    Console.WriteLine(DateTime.Now + " : " + task);
                    ID = responseContent;
                    // From here on you could deserialize the ResponseContent back again to a concrete C# type using Json.Net
                }

            }
        //  WriteLog(ID);
            ID=Regex.Match(Regex.Match(ID, @"""ID"":""\d+").Value, @"\d+").Value;
            //    WriteLog(ID);
            Console.WriteLine(DateTime.Now + " : " + ID);
            return ID;

        }



        private static async Task<string> GetIdTaskFromBitrix(string TaskName)
        {

            string _TITLE = TaskName;

            string ID = null;
            INIManager BTX = new INIManager("c:\\config.ini");
            string BitrixName = BTX.GetPrivateString("BTX", "logPD");
            string BitrixPass = BTX.GetPrivateString("BTX", "pasPD");
            string task = "tasks.task.list.json";

            string url = "https://" + BitrixName + "/rest/32/" + BitrixPass + "/" + task;
            // Serialize our concrete class into a JSON String

            var stringPayload = await Task.Run(() => JsonConvert.SerializeObject(

                new
                {
                    filter = new
                    {
                        TITLE = _TITLE,
                       
                    },
                    
                }

                ));
            var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {

                // Do the actual request and await the response
                var httpResponse = await httpClient.PostAsync(url, httpContent);
                // var httpResponse = await httpClient.GetAsync(url);
                // If the response contains content we want to read it!
                if (httpResponse.Content != null)
                {
                    var responseContent = await httpResponse.Content.ReadAsStringAsync();
                    Console.WriteLine(DateTime.Now + " : " + task);
                    ID = responseContent;
                    // From here on you could deserialize the ResponseContent back again to a concrete C# type using Json.Net
                }

            }
             // WriteLog(ID);
            ID = Regex.Match(Regex.Match(ID, @"""id"":""\d+").Value, @"\d+").Value;
            //   WriteLog(ID);
            Console.WriteLine(DateTime.Now + " : " + ID);
            return ID;

        }






        private static async Task<string> AddTaskToBitrix(Order currentOrder)
        {

            INIManager BTX = new INIManager("c:\\config.ini");
            string BitrixName = BTX.GetPrivateString("BTX", "logPD");
            string BitrixPass = BTX.GetPrivateString("BTX", "pasPD");
            string task = "tasks.task.add.json";
          
           
            // Serialize our concrete class into a JSON String

            List<object> stringPayload1 = await  OrderToBitrixFormat(currentOrder);
            if (stringPayload1 != null)
            {
                if(stringPayload1[1].ToString()!="")
                     task = "tasks.task.update.json";
                string url = "https://" + BitrixName + "/rest/32/" + BitrixPass + "/" + task;
                // Wrap our JSON inside a StringContent which then can be used by the HttpClient class
                var httpContent = new StringContent(stringPayload1[0].ToString(), Encoding.UTF8, "application/json");

                using (var httpClient = new HttpClient())
                {

                    // Do the actual request and await the response
                    var httpResponse = await httpClient.PostAsync(url, httpContent);

                    // If the response contains content we want to read it!
                    if (httpResponse.Content != null)
                    {
                        var responseContent = await httpResponse.Content.ReadAsStringAsync();
                        Console.WriteLine(DateTime.Now+" : "+task);
                        WriteLog(responseContent);
                        var ID = Regex.Match(Regex.Match(responseContent, @"""id"":""\d+").Value, @"\d+").Value;
                        return ID;
                    //   await AddCheklistToBitrixTask(ID, "ТЕСТ", "Иван Стрелков");
                        // From here on you could deserialize the ResponseContent back again to a concrete C# type using Json.Net
                    }

                }
            }
            else WriteLog(DateTime.Now + ": " + "Такая задача уже есть");
            return null;
        }
        private static async Task<string> AddPathTaskFromBitrix(string path, string _NAME)
        {

            ;

            string ID = null;
            INIManager BTX = new INIManager("c:\\config.ini");
            string BitrixName = BTX.GetPrivateString("BTX", "logPD");
            string BitrixPass = BTX.GetPrivateString("BTX", "pasPD");
            string task = "disk.folder.addsubfolder.json";

            string url = "https://" + BitrixName + "/rest/32/" + BitrixPass + "/" + task;
            // Serialize our concrete class into a JSON String

            var stringPayload = await Task.Run(() => JsonConvert.SerializeObject(

                new
                {
                    id=path,
                    data = new
                    {
                        NAME=_NAME,
                    }

                }

                ));
            var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {

                // Do the actual request and await the response
                var httpResponse = await httpClient.PostAsync(url, httpContent);
                // var httpResponse = await httpClient.GetAsync(url);
                // If the response contains content we want to read it!
                if (httpResponse.Content != null)
                {
                    var responseContent = await httpResponse.Content.ReadAsStringAsync();
                    Console.WriteLine(DateTime.Now + " : " + task);
                    Console.WriteLine(DateTime.Now + " : " + responseContent);
                    ID = responseContent.ToLower();
                    // From here on you could deserialize the ResponseContent back again to a concrete C# type using Json.Net
                }

            }
            // WriteLog(ID);

            ID = Regex.Match(Regex.Match(ID, "\"id\":\\d+").Value, @"\d+").Value;
            //   WriteLog(ID);
            Console.WriteLine(DateTime.Now + " : " + ID);
            return ID;

        }

        public static async void  Start(Order currentOrder)
        {       
                     
             string TaskID = await AddTaskToBitrix(currentOrder);
          //  string IDpath = null ;
            string _TITLE = Regex.Match(currentOrder.Number, @"\d+").Value + "_" + currentOrder.Comment + " " + currentOrder.Organization;
            if (currentOrder.SecondaryOrder != "")
            {
                _TITLE = _TITLE + "_" + Regex.Match(currentOrder.SecondaryOrder, @"\d+").Value;
            }
            /*
            if (TaskID != "" && IDpath == null)
            IDpath=await AddPathTaskFromBitrix("16778", _TITLE);
            if (IDpath!="")
            {
                AddPathTaskFromBitrix(IDpath, "Рабочая документация").Wait();
                AddPathTaskFromBitrix(IDpath, "Проектная документация").Wait();
                IDpath = null;
            }

            */

            Console.WriteLine(DateTime.Now + ": " + "Готово");
        }


    }
}
