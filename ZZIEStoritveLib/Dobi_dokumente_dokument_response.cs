using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZZIEStoritveLib
{
    public class Dobi_dokumente_dokument_response
    {
        public int Id;
        public string Extern_id;
        public string Title;
        public string CreationTime;
        public string Creation_location;
        public string Filename;
        public string MineType;
        public string Organization;
        public string Insert_date;
        public string Classification_name;
        public string Account_name;
        public string Usr_id;
        public string Status;
        public string Usr_id_uporabnik;
        public int Size;
        public string Type;
        public int Acc_id;
        public string Ver_description;
        public int Doc_id;
        public string Data_reference;

        public Dobi_dokumente_dokument_response (int id,string extern_id,string title,string creationTime,
            string creation_location,string filename,string mineType,string organization,string insert_date,string classification_name,
            string account_name,string usr_id,string status,string usr_id_uporabnik,int size,string type,int acc_id,
            string ver_description,int doc_id,string data_reference)
        {
            Id = id;
            Extern_id = extern_id;
            Title = title;
            CreationTime = creationTime;
            Creation_location = creation_location;
            Filename = filename;
            MineType = mineType;
            Organization = organization;
            Insert_date = insert_date;
            Classification_name = classification_name;
            Account_name = account_name;
            Usr_id = usr_id;
            Status = status;
            Usr_id_uporabnik = usr_id_uporabnik;
            Size = size;
            Type = type;
            Acc_id = acc_id;
            Ver_description = ver_description;
            Doc_id = doc_id;
            Data_reference = data_reference;
        }




    }
}
