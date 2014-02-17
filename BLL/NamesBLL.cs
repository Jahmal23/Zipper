
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Zipper.DAL;
using Zipper.Models;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using Microsoft.VisualBasic.FileIO;

namespace Zipper.BLL
{
    public static class NamesBLL
    {
        
        public static void ProcessNamesFile(HttpPostedFileBase f)
        {
            NameSource namesource = new NameSource();

            ParseCSV(f, namesource);
            KillAndFillNames(namesource);
          
        }


        public static NameSource ParseCSV(HttpPostedFileBase f, NameSource n)
        {
            using (var reader = new StreamReader(f.InputStream))
            using (var vbParser = new TextFieldParser(reader))
            {
                vbParser.TextFieldType = FieldType.Delimited;
                vbParser.SetDelimiters(",");

                bool first = true;
                while (!vbParser.EndOfData)
                {
                    try
                    {
                        string[] fields = vbParser.ReadFields();
                        if (first)
                        {
                            first = false;
                            continue;
                        }

                        n.Names.Add(new Name()
                        {
                            Value = fields[0],
                            Lang = Language.Portuguese,
                            Type = NameType.LastName
                        });

                    }
                    catch { }
                }
            }

            return n;
        }


        public static void KillAndFillNames(NameSource n)
        {
            if (n.Names.Count > 0)
            {
                var db = DbLayer.GetRemoteDatabase();

                var collection = db.GetCollection<Name>("searchNames");

                collection.RemoveAll();

                try
                {
                    collection.InsertBatch(n.Names);
                }
                catch {   }
            }
        }


        public static bool IsValidFile(HttpPostedFileBase f)
        {
            return (f != null && f.ContentLength > 0 && f.FileName.EndsWith(".csv"));
        }

        public static NameSource GetAllNames()
        {
            NameSource nameSource = new NameSource();

            var db = DbLayer.GetRemoteDatabase();

            var collection = db.GetCollection<Name>("searchNames");

            var allNames = collection.AsQueryable<Name>();

            nameSource.Names = allNames.ToList<Name>();


            // Name namea = new Name { Value = "Almeida", Lang = Language.Portuguese, Type = NameType.LastName };
            //Name namec = new Name { Value = "Antunes", Lang = Language.Portuguese, Type = NameType.LastName };
            //Name namee= new Name { Value = "Esteves", Lang = Language.Portuguese, Type = NameType.LastName };
            // Name nameh = new Name { Value = "Figueira", Lang = Language.Portuguese, Type = NameType.LastName };
            //Name namel = new Name { Value = "Martins", Lang = Language.Portuguese, Type = NameType.LastName };
            //Name namen = new Name { Value = "Meneses", Lang = Language.Portuguese, Type = NameType.LastName };
            //Name namet = new Name { Value = "Ribeiro", Lang = Language.Portuguese, Type = NameType.LastName };
            //Name nameu = new Name { Value = "Rodrigues", Lang = Language.Portuguese, Type = NameType.LastName };
            //Name namez = new Name { Value = "Tavares", Lang = Language.Portuguese, Type = NameType.LastName };
            // Name nameaa = new Name { Value = "Teixeira", Lang = Language.Portuguese, Type = NameType.LastName };


            //nameSource.Names = new List<Name> { 
            //    new Name { Value = "Andrade", Lang = Language.Portuguese, Type = NameType.LastName }, 
            //    new Name { Value = "Carvalho", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "Fernandes", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "Ferreira", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "Gonçalves", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "Lopes", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "Moreira", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "Nogueira", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "Oliveira", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "Pires", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "Pereira", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "dos Santos", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "Silva", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "Siqueira", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "Soares", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "da Silva", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "Brandão", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "Pinheiro", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "Machado", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "Leite", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "Gonsalves", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "Gomes", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "Domingues", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "Lima", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "Rodrigues", Lang = Language.Portuguese, Type = NameType.LastName }
            //};

            //nameSource.Names = new List<Name> { 
            //    new Name { Value = "Akwá", Lang = Language.Portuguese, Type = NameType.LastName }, 
            //    new Name { Value = "Bagina", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "Bonga", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "Chivukuvuku", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "CASANDI", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "CHACOCO", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "CAVITA", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "CHICA", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "Chara", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "Carago", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "Cabungula", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "Chakussanga", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "Diakité", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "Djalma", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "Elavoko", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "EPALANGA", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "ELAVOKO", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "EKUMBI", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "HOSSI", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "HANDANGA", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "Jamba", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "Jamuana", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "KASOMA", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "KAPIÑGALA", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "KASINDA", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "KAPUKA", Lang = Language.Portuguese, Type = NameType.LastName }, 
            //    new Name { Value = "KAVINOQUEKA", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "KALUNGA", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "KAHOSI", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "KATCHIKUKUVANDA", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "LUVINDA", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "LUSATI", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "Luana", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "Luena", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "Lueji", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "Lukamba", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "Lama", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "MOKO", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "MUHONGO", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "MULUNGO", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "MUNGA", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "Makanga", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "Mabina", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "Mantorras", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "Matondo", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "Mingas", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "Mbande", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "Macanga", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "Maliana", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "Muxima", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "NGEVE", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "Nbandi", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "NJAMBA", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "NGONGO", Lang = Language.Portuguese, Type = NameType.LastName }, 
            //    new Name { Value = "Nzingha", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "NAMBUNDI", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "NDAVOKA", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "NANGOMBE", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "NATULA", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "NATUMBU", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "NAVIMBI", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "NAVITA", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "Nzumba", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "NDALA", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "NDUMBU", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "NGONGA", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "NASSOMA", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "Neusa", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "Nayola", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "PAKISI", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "Pepetela", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "Panasco", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "Quipemba", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "SONEHÃ", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "SASONDE", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "SINJEKUMBI", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "SAMAHINA", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "SANDAMBONGO", Lang = Language.Portuguese, Type = NameType.LastName },
            //     new Name { Value = "SAPALO", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "SAVIMBI", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "Samakuva", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "TCHIKUKUMA", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "TCHILEPUE", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "TCHOPELONGA", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "TCHILOMBO", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "TCHIMUKU", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "TCHIPENDA", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "TCHIPILIKA", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "TCHITEKULO", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "TCHITULA", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "TCHITWE", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "TCHIVINDA", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "Tetebua", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "Vemba", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "VITI", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "VITULO", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "WELEMA", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "Weza", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "Welvicha", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "Webba", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "Wanga", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "Xavito", Lang = Language.Portuguese, Type = NameType.LastName }, 
            //    new Name { Value = "Yamba", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "Zinga ", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "Zuela", Lang = Language.Portuguese, Type = NameType.LastName },
            //    new Name { Value = "Zumba", Lang = Language.Portuguese, Type = NameType.LastName }
            // };

            return nameSource;
        }
    }
}