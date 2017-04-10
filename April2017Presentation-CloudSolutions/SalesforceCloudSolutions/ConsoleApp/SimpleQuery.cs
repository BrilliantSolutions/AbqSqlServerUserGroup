using EnterpriseSoapApi;
using Salesforce.Common.Models;
using Salesforce.Force;
using SalesforceBusinessObjects;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class SimpleQuery
    {
        //private static readonly string salesforce3ConnectionString = ConfigurationManager.ConnectionStrings["Salesforce3"].ConnectionString;

        public async Task TestSimpleQuery()
        {
            ForceClient thornburgForceClient = ThornburgForceClient.GetForceClient().Result;

            // retrieve all accounts
            Console.WriteLine(value: "Get Contacts from SalesForce");

            const string Query = "Select Id, IsDeleted, MasterRecordId, AccountId, LastName, FirstName FROM Contact";
            var accts = new List<Contact>();
            QueryResult<Contact> contacts = await thornburgForceClient.QueryAsync<Contact>(Query);
            int totalSize = contacts.TotalSize;

            Console.WriteLine("Queried " + totalSize + " records.");

            Console.WriteLine();
            Console.WriteLine();

            //Console.WriteLine(value: "Add records:");

            //if (territories.Records.Count > 0)
            //{
            //    string nextRecordsUrl = territories.NextRecordsUrl;
            //    ClearTerritoryTable();

            //    foreach (var territory in territories.Records)
            //    {
            //        AddTerritoryTable(territory);
            //    }

            //    while (!string.IsNullOrEmpty(nextRecordsUrl))
            //    {
            //        QueryResult<Territory> continuationResults = await thornburgForceClient.QueryContinuationAsync<Territory>(nextRecordsUrl);

            //        foreach (var territory in continuationResults.Records)
            //        {
            //            AddTerritoryTable(territory);
            //        }

            //        nextRecordsUrl = continuationResults.NextRecordsUrl;

            //    }

            //}
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
