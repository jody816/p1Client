using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace p1Client.Class
{
    class MySQLManager
    {
        public void Initialize()
        {
            Debug.WriteLine("DataBase Initialize");

            string connectionPath = $"SERVER=;DATABASE=cptest;UID=plz;PASSWORD=1234";
            App.connection = new MySqlConnection(connectionPath);
        }

        // Create MySqlCommand
        public MySqlCommand CreateCommand(string query)
        {
            MySqlCommand command = new MySqlCommand(query, App.connection);
            return command;
        }

        // DataBase Connection
        public bool OpenMySqlConnection()
        {
            try
            {
                App.connection.Open();
                return true;
            }
            catch (MySqlException e)
            {
                switch (e.Number)
                {
                    case 0:
                        Debug.WriteLine("Unable to Connect to Server");
                        break;
                    case 1045:
                        Debug.WriteLine("Please check your ID or PassWord");
                        break;
                }
                return false;
            }
        }

        // DataBase Close
        public bool CloseMySqlConnection()
        {
            try
            {
                App.connection.Close();
                return true;
            }
            catch (MySqlException e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
        }

        // Queyr Executer(Insert, Delete, Update ...)
        public void MySqlQueryExecuter(string userQuery)
        {
            string query = userQuery;

            if (OpenMySqlConnection() == true)
            {
                MySqlCommand command = new MySqlCommand(query, App.connection);

                if (command.ExecuteNonQuery() == 1)
                {
                    Debug.WriteLine("값 저장 성공");
                    App.DataSaveResult = true;
                }
                else
                {
                    Debug.WriteLine("값 저장 실패");
                    App.DataSaveResult = false;
                }
                CloseMySqlConnection();
            }
        }

        public List<string>[] Select(string tableName, int columnCnt, string id, string pw)
        {
            string query = "SELECT * FROM" + " " + tableName + " " + "WHERE Id = '" + id + "' AND Password = '" + pw + "';";

            List<string>[] element = new List<string>[columnCnt];

            for (int index = 0; index < element.Length; index++)
            {
                element[index] = new List<string>();
            }

            if (this.OpenMySqlConnection() == true)
            {
                MySqlCommand command = CreateCommand(query);
                MySqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    element[0].Add(dataReader["Id"].ToString());
                    element[1].Add(dataReader["Password"].ToString());
                }

                // 추가된 코드
                if (element != null)
                {
                    for (int i = 0; i < element[0].Count; i++)
                    {
                        if (element[0][i].Contains(id))
                        {
                            for (int j = 0; j < element[1].Count; i++)
                            {
                                if (element[1][i].Contains(pw))
                                {
                                    App.DataSearchResult = true;
                                    break;
                                }
                            }
                        }
                        break;
                    }
                }

                dataReader.Close();
                this.CloseMySqlConnection();

                return element;
            }
            else
            {
                return null;
            }
        }
    }
}
