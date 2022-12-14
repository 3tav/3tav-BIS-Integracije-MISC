using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Perun3WsLib;

namespace Komunikator3TavLib.OdcitkiPlin
{
    public class OdcitkiPlinService
    {
        private string _connString;        
        public OdcitkiPlinService()
        {

        }

        public void Init()
        {
            _connString = cSettings.Get_3Tav_ConnString();        
        }

        public sFunctionResult VnosOdcitka(OdcitekPlin o)
        {            
            sFunctionResult fr = cFunctionResult.Init();            
            try
            {               
                fr = cFunctionResult.Set(true, (Int32)efrErrorCodes.OK, "", "", "");
                using (SqlConnection conn = new SqlConnection(cSettings.Get_3Tav_ConnString()))
                { 
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "bis_PlinVnosOdcitka";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@odjemno_mesto", o.MerilnoMesto);                        
                        cmd.Parameters.AddWithValue("@datum_odcitka", o.DatumOdcitka);
                        cmd.Parameters.AddWithValue("@odcitek_ET", o.Stanje);
                        cmd.Parameters.AddWithValue("@uporabnik", o.Uporabnik);
                        cmd.Parameters.AddWithValue("@tip_odcitka", o.TipOdcitka);
                        cmd.Parameters.AddWithValue("@opomba", o.Opomba);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {                
                fr = cFunctionResult.Set(false, (Int32)efrErrorCodes.SQLError , "Napaka: vnos stanja plina", ex.Message.ToString(), "");
            }
            
            return fr;
        }

        public sFunctionResult VnosOdcitka(OdcitekPlin o, string vir)
        {
            sFunctionResult fr = cFunctionResult.Init();
            try
            {
                fr = cFunctionResult.Set(true, (Int32)efrErrorCodes.OK, "", "", "");
                using (SqlConnection conn = new SqlConnection(cSettings.Get_3Tav_ConnString()))
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "bis_PlinVnosOdcitka";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@odjemno_mesto", o.MerilnoMesto);
                        cmd.Parameters.AddWithValue("@datum_odcitka", o.DatumOdcitka);
                        cmd.Parameters.AddWithValue("@odcitek_ET", o.Stanje);
                        cmd.Parameters.AddWithValue("@uporabnik", o.Uporabnik);
                        cmd.Parameters.AddWithValue("@tip_odcitka", o.TipOdcitka);
                        cmd.Parameters.AddWithValue("@opomba", (o.Opomba == null ? string.Empty : o.Opomba));
                        cmd.Parameters.AddWithValue("@vir", (vir == null ? string.Empty : vir));
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                fr = cFunctionResult.Set(false, (Int32)efrErrorCodes.SQLError, "Napaka: vnos stanja plina", ex.Message.ToString(), "");
            }

            return fr;
        }

        public sFunctionResult GetZadnjiOdcitek(int merilnoMesto, out dcPerunOdcitek odcitek)
        {            
            sFunctionResult fr = cFunctionResult.Init();
            odcitek = new dcPerunOdcitek();

            try
            {
                fr = cFunctionResult.Set(true, (Int32)efrErrorCodes.OK, "", "", "");
                using (SqlConnection conn = new SqlConnection(cSettings.Get_3Tav_ConnString()))
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "bis_PlinZadnjiOdcitek";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@odjemno_mesto", merilnoMesto);
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            if (rdr.Read())
                            {
                                odcitek = new dcPerunOdcitek();
                                odcitek.SMM = rdr.GetInt32(0);
                                odcitek.DatumStanja = rdr.GetDateTime(2);
                                odcitek.StanjeET = (rdr.IsDBNull(3) ? "" : Convert.ToString(rdr.GetValue(3)));
                                odcitek.VrstaOdcitka = rdr.GetInt32(4);
                            }
                            else
                            {
                                fr = cFunctionResult.Set(false, (Int32)efrErrorCodes.NoDataFound, "Napaka: branje zadnjega odčitka", "Zadnjega stanja ni mogoče pridobiti!", "");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                fr = cFunctionResult.Set(false, (Int32)efrErrorCodes.SQLError, "Napaka: branje zadnjega odčitka", ex.Message.ToString(), "");
            }

            return fr;
        }
    }
}