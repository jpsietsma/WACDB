using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using WAC_Extensions;


namespace ConsoleSandbox
{
    class Program
    {
       
        static void Main(string[] args)
        {
            //List<string> bmps = new List<string>
            //{
            //    "21aRepair",
            //    "04iMod2",
            //    "06aRR",
            //    "04d1RR",
            //    "5",
            //    "07",
            //    "8a",
            //    "09b",
            //    "04g3",
            //};
            //foreach (var item in bmps)
            //{
            //    bool result;

            //    //if (global::WACGlobal_Methods.BmpExistsForFarm(12, item))
            //    //    Console.WriteLine("Original bmp: {0} matched", item);
            //}
            //foreach (var str in bmps)
            //{
            //    var m = Regex.Match(str, @"^(\d{1,3}[a-z]?\d?)([a-zA-Z]*)(\d?)?$");
            //    if (m.Success)
            //    {
            //        Console.WriteLine("Original BMP: {0,10}  BMP: {1,15}  Qualifier: {2,5}  Version: {3,3}", m.Groups[0], m.Groups[1], m.Groups[2], m.Groups[3]);
            //    }
            //    else
            //        Console.WriteLine("no match: {0}", str);
            //}

            Bmps bmps = new Bmps();
            bmps.GetBmps();
            //using (DataClasses1DataContext wac = new DataClasses1DataContext())
            //{
            //    var a = wac.FarmNMCPAwardHistory(245, 2015);
            //    Console.ReadKey();  
            //}
            
            Console.ReadKey();   
        }
    }
    public class Bmps
    {
        public List<bmp_ag> GetBmps()
        {
            string qualifierVersion = string.Empty;
            byte qv;
            string bmp = string.Empty;
            string bmp_nbr = string.Empty;
            string qualifier = string.Empty;
            int counter = 0;
            using (DataClasses1DataContext wac = new DataClasses1DataContext())
            try
            {
                List<string> a = wac.list_BMPCodes.Select(s => s.pk_BMPCode_code).ToList();
                var b = wac.bmp_ags.Where(w => !w.bmp_nbr.StartsWith("000.") && !w.bmp_nbr.Contains("NMCP") && !w.bmp_nbr.Contains("PFM"))
                    .OrderBy(o => o.pk_bmp_ag)
                    //.Select(s => new
                    //{
                    //    pk_bmp_ag = s.pk_bmp_ag,
                    //    bmp_nbr = s.bmp_nbr,
                    //    Bmp = s.Bmp,
                    //    Qualifier = s.fk_BMPCode_code,
                    //    Version = s.QualifierVersion
                    //})
                    .ToList();
                if (b.Any())
                {

                    foreach (var item in b)
                    {
                        counter++;
                        bmp_nbr = item.bmp_nbr;
                        bmp = item.bmp_nbr.Trim();
                        if (!string.IsNullOrEmpty(item.bmp_nbr))
                        {
                            Dictionary<string,string> parts = BmpNumberDeconstruct(item.bmp_nbr, item.description, a);
                            if (parts != null && parts.TryGetValue("BMP", out bmp))
                            {
                                if (!parts.TryGetValue("QUALIFIER", out qualifier))
                                    qualifier = "UNC";
                                if (!parts.TryGetValue("VERSION", out qualifierVersion))
                                    qualifierVersion = "0";

                                item.Bmp = bmp.Trim();
                                item.fk_BMPCode_code = qualifier;
                                if (!Byte.TryParse(qualifierVersion, out qv))
                                    item.QualifierVersion = 0;
                                else
                                    item.QualifierVersion = qv;
                                Console.WriteLine("Original BMP: {0,10}  BMP: {1,15}  Qualifier: {2,5}  Version: {3,3}  Description: {4,24}", 
                                    item.bmp_nbr,item.Bmp, item.fk_BMPCode_code, item.QualifierVersion, item.description);
                            }
                        }
                        //Console.WriteLine("Count: {0}", counter);
                    }
                }
                
                wac.SubmitChanges();
            }
            catch(Exception ex) 
            {
                Console.WriteLine("Original BMP: {0,10}  {1}",bmp_nbr, ex.Message);
            }
            Console.WriteLine("Final count: {0}", counter);
            return null;
        }


        public Dictionary<string,string> BmpNumberDeconstruct(string bmpNumber, string bmpDescription, List<string> qualifiers)
        {
            Dictionary<string,string> parts = new Dictionary<string,string>();
            string descriptionUpper = bmpDescription.ToUpper();
            

            var m = Regex.Match(bmpNumber, @"^(\d{1,3}(?:\.\-_)?[a-zSXT]?[a-zSXT]?\d?)([a-zA-Z]*)(\d?)?$");
            if (m.Success)
            {
                parts.Add("BMP",m.Groups[1].Value);
                string qualifier = string.IsNullOrEmpty(m.Groups[2].Value) ? "UNC" : m.Groups[2].Value;
                // if qualifier is a valid qualifier we are done
                if (qualifiers.BinarySearch(qualifier) > 0)
                    parts.Add("QUALIFIER",qualifier);
                // if not we try some other ways to come up with a valid qualifier
                else if (qualifier.Contains("Rep"))
                    parts.Add("QUALIFIER","R");
                else if (qualifier.Contains("Flood"))
                    parts.Add("QUALIFIER","STR");
                else if (qualifier.Contains("Mod"))
                    parts.Add("QUALIFIER","M");
                else if (qualifier.Contains("WRE"))
                    parts.Add("QUALIFIER", "WRE");
                else if (qualifier.Contains("RERE"))
                    parts.Add("QUALIFIER", "RE");
                else if (qualifier.Contains("ER"))
                    parts.Add("QUALIFIER","ER");
                else if (qualifier.Contains("RR"))
                    parts.Add("QUALIFIER","RR");
                else if (qualifier.Contains("EX"))
                    parts.Add("QUALIFIER","EX");               
                else if (qualifier.Contains("RE"))
                    parts.Add("QUALIFIER", "RE");
                else if (qualifier.Contains("BYI"))
                    parts.Add("QUALIFIER","BYI");
                else parts.Add("QUALIFIER","UNC");
                if (!string.IsNullOrEmpty(bmpDescription))
                {
                    if (descriptionUpper.Contains("CREP") || descriptionUpper.Contains("CP-30"))
                        parts["QUALIFIER"] = "C";

                    else if (descriptionUpper.Contains("Re-en"))
                        parts["QUALIFIER"] = "RE";
                }
                parts.TryGetValue("QUALIFIER", out qualifier);

                byte version = 0;
                if (!string.IsNullOrEmpty(qualifier))
                {
                    if (!qualifier.In(new [] {"MTC","ENMC","UNC"}))
                    {
                        if (!Byte.TryParse(m.Groups[3].Value, out version))
                            version = 1;
                    }          
                }
                parts.Add("VERSION", version.ToString());
                return parts;
            }
            else if (bmpNumber.StartsWith("IRC"))
            {
                parts.Add("BMP", bmpNumber);
                parts.Add("QUALIFIER", "UNC");
                parts.Add("VERSION", "0");
                return parts;
            }
            return null;
        }

    }

}
