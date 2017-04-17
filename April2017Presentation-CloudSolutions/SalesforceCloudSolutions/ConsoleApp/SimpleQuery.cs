using EnterpriseSoapApi;
using Salesforce.Common.Models;
using Salesforce.Force;
using SalesforceBusinessObjects;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class SimpleQuery
    {
        private static readonly string worlWideImportersConnectionString = ConfigurationManager.ConnectionStrings["WorlWideImporters"].ConnectionString;
        readonly ForceClient thornburgForceClient = ThornburgForceClient.GetForceClient().Result;

        public async System.Threading.Tasks.Task TestSimpleQuery()
        {
            await UpdateContactsFromSalesforce();
            await UpdateEventsFromSalesforce();
            await UpdateActivitiesFromSalesforce();
            await ProcessEtl();



            // retrieve all accounts
            //Console.WriteLine(value: "Get Contacts from SalesForce");

            //const string Query = "Select Id, LastName, FirstName FROM Contact";
            //var accts = new List<Contact>();
            //QueryResult<Contact> contacts = await thornburgForceClient.QueryAsync<Contact>(Query);
            //int totalSize = contacts.TotalSize;

            //Console.WriteLine("Queried " + totalSize + " records.");

            //Console.WriteLine();
            //Console.WriteLine();

            //using (SqlConnection sqlConnection = new SqlConnection(worlWideImportersConnectionString))
            //{
            //    sqlConnection.Open();

            //    using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
            //    {
            //        sqlCommand.CommandType = System.Data.CommandType.Text;


            //        Console.WriteLine(value: "Clear Contact records.");
            //        sqlCommand.CommandText = "DELETE FROM INTEGRATION.CONTACT;";
            //        sqlCommand.ExecuteNonQuery();

            //        Console.WriteLine(value: "Add Contact records.");
            //        foreach (var contact in contacts.Records)
            //        {
            //            sqlCommand.CommandText = string.Format(format: @"INSERT INTO INTEGRATION.CONTACT(Id, FirstName, LastName) VALUES('{0}', '{1}', '{2}')",
            //                                                            arg0: contact.Id, arg1: contact.FirstName, arg2: contact.LastName.Replace(oldValue: "'", newValue: "''"));

            //            try
            //            {
            //                sqlCommand.ExecuteNonQuery();
            //            }
            //            catch (Exception ex)
            //            {
            //                throw;
            //            }
            //        }
            //    }
            //}

        }

        private async System.Threading.Tasks.Task ProcessEtl()
        {
            Console.WriteLine(value: "Process Etl...");

            using (SqlConnection sqlConnection = new SqlConnection(worlWideImportersConnectionString))
            {
                sqlConnection.Open();

                using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.CommandText = "ProcessETL";
                    await sqlCommand.ExecuteNonQueryAsync();

                }

            }


            Console.WriteLine(value: "Successfully Processed Etl...");
        }

        private async System.Threading.Tasks.Task UpdateEventsFromSalesforce()
        {
            // retrieve all accounts
            Console.WriteLine(value: "Get Events from SalesForce");

            const string Query = "Select Id, Subject, ActivityDate, WHOID FROM Event";
            var accts = new List<SalesforceBusinessObjects.Task>();
            QueryResult<SalesforceBusinessObjects.Task> events = await thornburgForceClient.QueryAsync<SalesforceBusinessObjects.Task>(Query);
            int totalSize = events.TotalSize;

            Console.WriteLine("Queried " + totalSize + " records.");

            Console.WriteLine();
            Console.WriteLine();

            using (SqlConnection sqlConnection = new SqlConnection(worlWideImportersConnectionString))
            {
                sqlConnection.Open();

                using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;


                  

                    Console.WriteLine(value: "Add Event records.");


                    foreach (var eventItem in events.Records)
                    {
                        sqlCommand.CommandText = string.Format(format: @"INSERT INTO INTEGRATION.Event(Id, Subject, ActivityDate, WhoID) VALUES('{0}', '{1}', '{2}', '" + eventItem.WhoId +"')",
                                                                        arg0: eventItem.Id, arg1:  eventItem.Subject, arg2:  eventItem.ActivityDate);

                        try
                        {
                            sqlCommand.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            throw;
                        }
                    }

                }
            }
        }

        private async System.Threading.Tasks.Task UpdateActivitiesFromSalesforce()
        {
            // retrieve all accounts
            Console.WriteLine(value: "Get Tasks from SalesForce");

            const string Query = "Select Id, Subject, ActivityDate, WhoId FROM Task";
            var accts = new List<Event>();
            QueryResult<Event> events = await thornburgForceClient.QueryAsync<Event>(Query);
            int totalSize = events.TotalSize;

            Console.WriteLine("Queried " + totalSize + " records.");

            Console.WriteLine();
            Console.WriteLine();

            using (SqlConnection sqlConnection = new SqlConnection(worlWideImportersConnectionString))
            {
                sqlConnection.Open();

                using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;



                    Console.WriteLine(value: "Add Event records.");


                    foreach (var eventItem in events.Records)
                    {
                        sqlCommand.CommandText = string.Format(format: @"INSERT INTO INTEGRATION.Event(Id, Subject, ActivityDate, WhoID) VALUES('{0}', '{1}', '{2}',  '" + eventItem.WhoId + "')",
                                                                        arg0: eventItem.Id, arg1: eventItem.Subject, arg2: eventItem.ActivityDate);

                        try
                        {
                            sqlCommand.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            throw;
                        }
                    }

                }
            }
        }

        private async System.Threading.Tasks.Task UpdateContactsFromSalesforce()
        {
            // retrieve all accounts
            Console.WriteLine(value: "Get Contacts from SalesForce");

            const string Query = "Select Id, LastName, FirstName FROM Contact";
            var accts = new List<Contact>();
            QueryResult<Contact> contacts = await thornburgForceClient.QueryAsync<Contact>(Query);
            int totalSize = contacts.TotalSize;

            Console.WriteLine("Queried " + totalSize + " records.");

            Console.WriteLine();
            Console.WriteLine();

            using (SqlConnection sqlConnection = new SqlConnection(worlWideImportersConnectionString))
            {
                sqlConnection.Open();

                using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;

                    Console.WriteLine(value: "Add Contact records.");
                    foreach (var contact in contacts.Records)
                    {
                        sqlCommand.CommandText = string.Format(format: @"INSERT INTO INTEGRATION.CONTACT(Id, FirstName, LastName) VALUES('{0}', '{1}', '{2}')",
                                                                        arg0: contact.Id, arg1: contact.FirstName, arg2: contact.LastName.Replace(oldValue: "'", newValue: "''"));

                        try
                        {
                            sqlCommand.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            throw;
                        }
                    }
                }
            }

            //private void ClearTerritoryTable()
            //{
            //    string deleteSql = "delete from territories";

            //    using (SqlConnection sqlConnection = new SqlConnection(connectionString: salesforce3ConnectionString))
            //    {
            //        sqlConnection.Open();

            //        using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
            //        {
            //            sqlCommand.CommandText = deleteSql;

            //            try
            //            {
            //                sqlCommand.ExecuteNonQuery();
            //            }
            //            catch (Exception ex)
            //            {
            //                throw;
            //            }
            //        }
            //    }
            //}

            //private void AddTerritoryTable(Territory territory)
            //{
            //    string insertSql = string.Format(format: @"INSERT INTO[dbo].[Territories]
            //                   ([Id]
            //                   ,[IsDeleted]
            //                   ,[Name]
            //                   ,[City__c]
            //                   ,[Institutional_Regional_Associate__c]
            //                   ,[Institutional_Region__c]
            //                   ,[State__c]
            //                   ,[ZipCode__c]
            //                   ,[County__c]
            //                   ,[Assignment_per_Kevin_File__c])
            //        VALUES
            //        ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}')",
            //        args: new object[]
            //        {
            //            territory.Id,
            //            territory.IsDeleted,
            //            !String.IsNullOrEmpty(territory.Name) ? territory.Name.FormatForSql() : String.Empty,
            //            !String.IsNullOrEmpty(territory.City__c) ? territory.City__c.FormatForSql() : String.Empty,
            //            territory.Institutional_Regional_Associate__c,
            //            territory.Institutional_Region__c,
            //            territory.State__c,
            //            territory.ZipCode__c,
            //            territory.County__c,
            //            territory.Assignment_per_Kevin_File__c
            //        });




            //using (SqlConnection sqlConnection = new SqlConnection(connectionString: salesforce3ConnectionString))
            //{

            //    //var sqlBulkCopy = new SqlBulkCopy(
            //    //    sqlConnection,
            //    //    SqlBulkCopyOptions.TableLock |
            //    //    SqlBulkCopyOptions.FireTriggers |
            //    //    SqlBulkCopyOptions.UseInternalTransaction, 
            //    //    externalTransaction: null
            //    //);

            //    sqlConnection.Open();

            //    using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
            //    {
            //        sqlCommand.CommandText = insertSql;
            //        sqlCommand.CommandType = System.Data.CommandType.Text;

            //        try
            //        {
            //            sqlCommand.ExecuteNonQuery();
            //        }
            //        catch (Exception ex)
            //        {
            //            throw;
            //        }

            //    }
            //}


        }
    }
}