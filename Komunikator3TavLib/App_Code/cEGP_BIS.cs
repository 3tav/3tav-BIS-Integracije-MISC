﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Serialization;

namespace Komunikator3TavLib
{
    public enum eTarifaZaCenik
    {
        SeNeZaracunava = 0,
        ET = 1001,
        VT = 1002,
        MT = 1003
    }

    [DataContract]
    public class dcMerilnoMesto
    {
        [DataMember]
        public Int32? SMM { get; set; }
        [DataMember]
        public Int32? TipOdjemnegaMesta { get; set; }
        [DataMember]
        public Int32? Status { get; set; }
        [DataMember]
        public Int32? SODO { get; set; }
        [DataMember]
        public String SODONaziv { get; set; }
        [DataMember]
        public Int32? SODO_SMM { get; set; }
        [DataMember]
        public String Naziv { get; set; }
        [DataMember]
        public String Naslov { get; set; }
        [DataMember]
        public String Posta { get; set; }
        [DataMember]
        public String StevilkaStanovanja { get; set; }
        [DataMember]
        public Int32? NacinObracuna { get; set; }
        [DataMember]
        public String NacinObracunaNaziv { get; set; }
        [DataMember]
        public String ObracunSteviloTarif { get; set; }
        [DataMember]
        public String ObracunSteviloTarifNaziv { get; set; }
        [DataMember]
        public Int32? TarifnaSkupina { get; set; }
        [DataMember]
        public String TarifnaSkupinaNaziv { get; set; }
        [DataMember]
        public String OdjemnaSkupina { get; set; }
        [DataMember]
        public String OdjemnaSkupinaNaziv { get; set; }
        [DataMember]
        public String ObracunskeVarovalke { get; set; }
        [DataMember]
        public Boolean? DalinjskoOdcitavanje { get; set; }
        [DataMember]
        public Boolean? LocenRacun { get; set; }
        [DataMember]
        public Int32? StevilkaSoglasja { get; set; }
        [DataMember]
        public DateTime? DatumZadnjegaObracuna { get; set; }
        [DataMember]
        public DateTime? DatumZadnjePrijave { get; set; }
        [DataMember]
        public String NacinPlacila { get; set; }
        [DataMember]
        public Boolean? NeAkontacijski { get; set; }
    }

    [DataContract]
    public class dcNamescenaNaprava
    {
        [DataMember]
        public Int32? SMM { get; set; }
        [DataMember]
        public String VrstaNaprave { get; set; }
        [DataMember]
        public Int32? TipID { get; set; }
        [DataMember]
        public String TipNaziv { get; set; }
        [DataMember]
        public String TovSt { get; set; }
        [DataMember]
        public Int32? LetoIzdelave { get; set; }
        [DataMember]
        public Int32? LetoZiga { get; set; }
        [DataMember]
        public DateTime? DatumNamestitve { get; set; }
        [DataMember]
        public Int32? SteviloMestOdbirka { get; set; }
        [DataMember]
        public Int32? SteviloDecimalnihMest { get; set; }
        [DataMember]
        public String VnosnaPoljaStanj { get; set; }
    }

    [DataContract]
    public class dcNamescenaNapravaList
    {
        [DataMember]
        public int SteviloNaprav { get; set; }
        [DataMember]
        public List<dcNamescenaNaprava> Data { get; set; }
    }

    [DataContract]
    public class dcOdcitek
    {
        [DataMember]
        public Int32? SMM { get; set; }
        [DataMember]
        public DateTime? DatumStanja { get; set; }
        [DataMember]
        public Int32? VrstaOdcitka { get; set; }
        [DataMember]
        public Int32? NacinPridobitveOdcitka { get; set; }
        [DataMember]
        public Int32? TipID_D { get; set; }
        [DataMember]
        public String TovSt_D { get; set; }
        [DataMember]
        public Double? VrednostET { get; set; }
        [DataMember]
        public String SlikaET { get; set; }
        [DataMember]
        public Double? PorabaET { get; set; }
        [DataMember]
        public Double? PorabaNaDanET { get; set; }
        [DataMember]
        public Double? VrednostDVT { get; set; }
        [DataMember]
        public String SlikaDVT { get; set; }
        [DataMember]
        public Double? PorabaDVT { get; set; }
        [DataMember]
        public Double? PorabaNaDanDVT { get; set; }
        [DataMember]
        public Double? VrednostDMT { get; set; }
        [DataMember]
        public String SlikaDMT { get; set; }
        [DataMember]
        public Double? PorabaDMT { get; set; }
        [DataMember]
        public Double? PorabaNaDanDMT { get; set; }
        [DataMember]
        public Double? VrednostKT { get; set; }
        [DataMember]
        public String SlikaKT { get; set; }
        [DataMember]
        public Double? PorabaKT { get; set; }
        [DataMember]
        public Double? PorabaNaDanKT { get; set; }
        [DataMember]
        public Double? Moc { get; set; }
        [DataMember]
        public String SlikaMoc { get; set; }
        [DataMember]
        public Int32? ObracunID { get; set; }
        [DataMember]
        public Boolean OdcitekObracunan { get; set; }
    }

    [DataContract]
    public class dcOdcitekList
    {
        [DataMember]
        public int SteviloOdcitkov { get; set; }
        [DataMember]
        public List<dcOdcitek> Data { get; set; }
    }

    [DataContract]
    public class dcPovprecnaMesecnaPoraba
    {
        [DataMember]
        public Int32? SMM { get; set; }
        [DataMember]
        public Int32? PovprecjeET { get; set; }
        [DataMember]
        public Int32? PovprecjeDVT { get; set; }
        [DataMember]
        public Int32? PovprecjeDMT { get; set; }
    }

    [DataContract]
    public class dcPartnerInfo
    {
        [DataMember]
        public Int32? PartnerUID { get; set; }
        [DataMember]
        public String ZunanjiPartnerID { get; set; }
        [DataMember]
        public String Naziv { get; set; }
        [DataMember]
        public String Ulica { get; set; }
        [DataMember]
        public String Kraj { get; set; }
        [DataMember]
        public String HisnaStevilka { get; set; }
        [DataMember]
        public String HisnaStevilkaDodatek { get; set; }
        [DataMember]
        public String Stanovanje { get; set; }
        [DataMember]
        public String Naslov { get; set; }
        [DataMember]
        public String PostaStevilka { get; set; }
        [DataMember]
        public String PostaNaziv { get; set; }
        [DataMember]
        public String Posta { get; set; }
        [DataMember]
        public String DavcnaStevilka { get; set; }
    }

    [DataContract]
    public class dcKolicinePoObdobjuRealizacije
    {
        [DataMember]
        public Int32? SMM { get; set; }
        [DataMember]
        public String LetoMesec { get; set; }
        [DataMember]
        public Int32? ObdobjeRealizacije { get; set; }
        [DataMember]
        public Int32 Kolicina_Moc { get; set; }
        [DataMember]
        public Int32 Kolicina_DVT { get; set; }
        [DataMember]
        public Int32 Kolicina_DMT { get; set; }
        [DataMember]
        public Int32 Kolicina_KT { get; set; }
        [DataMember]
        public Int32 Kolicina_DET { get; set; }
        [DataMember]
        public Int32 Kolicina_Skupaj_D { get; set; }
    }

    [DataContract]
    public class dcKolicinePoObdobjuRealizacijeList
    {
        [DataMember]
        public int SteviloPostavk { get; set; }
        [DataMember]
        public List<dcKolicinePoObdobjuRealizacije> Data { get; set; }
    }

    [DataContract]
    public class dcSaldoKontnaKartica_Postavka
    {
        [DataMember]
        public Int32? MerilnoMesto { get; set; }
        [DataMember]
        public DateTime? DatumFakture { get; set; }
        [DataMember]
        public String StevilkaFakture { get; set; }
        [DataMember]
        public String Obdobje { get; set; }
        [DataMember]
        public String VrstaTemeljnice { get; set; }
        [DataMember]
        public String Konto { get; set; }
        [DataMember]
        public Double? Breme { get; set; }
        [DataMember]
        public Double? Dobro { get; set; }
        [DataMember]
        public Double? Saldo { get; set; }
        [DataMember]
        public DateTime? DatumZapadlosti { get; set; }
        [DataMember]
        public String OpisKnjizbe { get; set; }
    }

    [DataContract]
    public class dcSaldoKontnaKarticaList
    {
        [DataMember]
        public int SteviloPostavk { get; set; }
        [DataMember]
        public List<dcSaldoKontnaKartica_Postavka> Data { get; set; }
    }

    [DataContract]
    public class dcIzdanaFakturaGlava
    {
        [DataMember]
        public Int32? MerilnoMesto { get; set; }
        [DataMember]
        public Int32? StevilkaFakture { get; set; }
        [DataMember]
        public String OznakaFakture { get; set; }
        [DataMember]
        public String StevilkaSklica { get; set; }
        [DataMember]
        public DateTime? DatumFakture { get; set; }
        [DataMember]
        public DateTime? ValutaFakture { get; set; }
        [DataMember]
        public Int32? PlacnikID { get; set; }
        [DataMember]
        public Int32? NaslovnikID { get; set; }
        [DataMember]
        public String NamenPlacila { get; set; }
    }

    [DataContract]
    public class dcIzdanaFakturaGlavaList
    {
        [DataMember]
        public int SteviloDokumentov { get; set; }
        [DataMember]
        public List<dcIzdanaFakturaGlava> Data { get; set; }
    }

    public class dcPostavkaIzracuna
    {
        public Int32? PostavkaCenikID { get; set; }
        public String PostavkaNaziv { get; set; }
        public String PostavkaOznaka { get; set; }
        public Double? PovprecjeNaDan { get; set; }
        public Int32? SteviloDni { get; set; }
        public DateTime? ObdobjeOd { get; set; }
        public DateTime? ObdobjeDo { get; set; }
        public Double? Kolicina { get; set; }
        public Double? CenaNaEnoto { get; set; }
        public Double? StopnjaDDV { get; set; }
        public Double? ZnesekBrezDDV { get; set; }

        public dcPostavkaIzracuna()
        { }

        public dcPostavkaIzracuna(Int32? PostavkaCenikID, String PostavkaNaziv, Double? PovprecjeNaDan, Int32? SteviloDni, DateTime? ObdobjeOd, DateTime? ObdobjeDo, Double? Kolicina,Double? CenaNaEnoto, Double? StopnjaDDV, Double? ZnesekBrezDDV)
        { }

    }

    [DataContract]
    public class dcIzdanaFakturaPostavka
    {
        [DataMember]
        public Int32? SifraArtikla { get; set; }
        [DataMember]
        public Double? Kolicina { get; set; }
        [DataMember]
        public String EnotaMere { get; set; }
        [DataMember]
        public DateTime? DatumCenika { get; set; }
        [DataMember]
        public Double? CenaNaEnoto { get; set; }
        [DataMember]
        public Double? DavcnaStopnja { get; set; }
        [DataMember]
        public Double? ZnesekNeto { get; set; }
        [DataMember]
        public Double? ZnesekDavek { get; set; }
        [DataMember]
        public Double? ZnesekBruto { get; set; }
        [DataMember]
        public Double? ZnesekNetoPN { get; set; }
        [DataMember]
        public Double? ZnesekDavekPN { get; set; }
        [DataMember]
        public Double? ZnesekBrutoPN { get; set; }
        [DataMember]
        public String NazivArtikla { get; set; }
    }

    [DataContract]
    public class dcIzdanaFakturaPostavkaList
    {
        [DataMember]
        public int SteviloPostavk { get; set; }
        [DataMember]
        public List<dcIzdanaFakturaPostavka> Data { get; set; }
    }
    public class cEGP_BIS
    {

        private String SqlConnStr;

        public cEGP_BIS(String _SqlConnStr)
        {
            SqlConnStr = _SqlConnStr;
        }

        public sFunctionResult Get_MM(Int32 _MM_SMM, out dcMerilnoMesto _MM_Data)
        {
            sFunctionResult fr = cFunctionResult.Init();
            cSQL sql = new cSQL(SqlConnStr);
            SqlDataReader dbReader;
            _MM_Data = this.Init_dcMerilnoMesto();
            sql.Text = sql.Text + "select sOM.id_odjemnega_mesta SMM, so.SistemskiOperaterID DistributerID, so.Naziv DistributerNaziv, sOM.veza DistributerSMM, sOM.tip_odjemnega_mesta, status_odjemnega_mesta Status, sOM.naziv Naziv, sOM.datum_poracun_do DatumZadnjegaObracuna," + cCommon.CR();
            sql.Text = sql.Text + "       rtrim(coalesce(sOM.ulica, sOM.kraj)) + ' ' + IsNull(sOM.hisna_stevilka , '') + ' ' + IsNull(sOM.dodatek_hs,'') Naslov," + cCommon.CR();
            sql.Text = sql.Text + "       sOM.postna_stevilka + ' ' + sOM.kraj Posta, sOM.vrsta_porabe NacinObracuna, sOM.stevilka_stanovanja," + cCommon.CR();
            sql.Text = sql.Text + "       rtrim(ltrim(eOM.nacin_obracuna)) ObracunSteviloTarif, eOM.obracunska_varovalka ObracunskeVarovalke," + cCommon.CR();
            sql.Text = sql.Text + "       eOM.odjemna_skupina OdjemnaSkupina, os.opis OdjemnaSkupinaDesc, eOM.tarifna_skupina TarifnaSkupina, ts.Opis TarifnaSkupinaDesc, eOM.nacinpopisa NacinPopisa, eOM.SkupniRacun," + cCommon.CR();
            sql.Text = sql.Text + "       eOM.soglasje_ees StevilkaSoglasja, eOM.NeAkontacijski NeAkontacijski, eOM.Sirena Sirena," + cCommon.CR();
            sql.Text = sql.Text + "       PG.datum_veljavnosti_od PogodbaOd," + cCommon.CR();
            sql.Text = sql.Text + "       vp.NAZIV_PLACILA VrstaPlacila" + cCommon.CR();
            sql.Text = sql.Text + "from " + LocalTables.Get_3Tav_bis_odjemna_mesta() + " sOM inner join " + LocalTables.Get_3Tav_bie_odjemna_mesta() + " eOM on (sOM.id_odjemnega_mesta=eOM.id_odjemnega_mesta)" + cCommon.CR();
            sql.Text = sql.Text + "                                                          inner join " + LocalTables.Get_3Tav_Bie_SistemskiOperater() + " so on (sOM.sodo=so.poslovni_partner) " + cCommon.CR();
            sql.Text = sql.Text + "                                                          left join " + LocalTables.Get_3Tav_bis_pogodbe_gl() + " PG on (sOM.pogodba=PG.stevilka_pogodbe)" + cCommon.CR();
            sql.Text = sql.Text + "                                                          left join " + LocalTables.Get_3Tav_bie_odjemna_skupina() + " os on (eOM.odjemna_skupina=os.odjemna_skupina)" + cCommon.CR();
            sql.Text = sql.Text + "                                                          left join " + LocalTables.Get_3Tav_bie_omr_SifTarifneSkupine() + " ts on (eOM.tarifna_skupina=ts.TSK)" + cCommon.CR();
            sql.Text = sql.Text + "                                                          left join " + LocalTables.Get_3Tav_Vrste_Placil() + " vp on (PG.nacin_placila=vp.SIFRA_VRSTE_PLACILA)" + cCommon.CR();
            sql.Text = sql.Text + "where sOM.id_odjemnega_mesta = @id_odjemnega_mesta" + cCommon.CR();

            sql.AddSQLParameter("@id_odjemnega_mesta", SqlDbType.Int, ParameterDirection.Input, _MM_SMM);
            dbReader = sql.Query_Read("cEGP_BIS.Get", ref fr);
            if (!fr.resBool)
            {
                return fr;
            }
            try
            {
                if ((dbReader != null) && (dbReader.Read()))
                {
                    _MM_Data.SMM = sql.GetField_Int(ref dbReader, "SMM");
                    _MM_Data.TipOdjemnegaMesta = sql.GetField_Int(ref dbReader, "tip_odjemnega_mesta");
                    _MM_Data.Status = sql.GetField_Int(ref dbReader, "Status");
                    _MM_Data.SODO = sql.GetField_Int(ref dbReader, "DistributerID");
                    _MM_Data.SODONaziv = sql.GetField_String(ref dbReader, "DistributerNaziv").Trim();
                    if (cCommon.IsInteger(sql.GetField_String(ref dbReader, "DistributerSMM").Trim()))
                    {
                        _MM_Data.SODO_SMM = Convert.ToInt32(sql.GetField_String(ref dbReader, "DistributerSMM").Trim());
                    }
                    _MM_Data.Naziv = sql.GetField_String(ref dbReader, "Naziv").Trim();
                    _MM_Data.Naslov = sql.GetField_String(ref dbReader, "Naslov").Trim();
                    _MM_Data.Posta = sql.GetField_String(ref dbReader, "Posta").Trim();
                    _MM_Data.StevilkaStanovanja = sql.GetField_String(ref dbReader, "stevilka_stanovanja").Trim();
                    _MM_Data.NacinObracuna = sql.GetField_IntNull(ref dbReader, "NacinObracuna");
                    if (_MM_Data.NacinObracuna == 1)
                    {
                        _MM_Data.NacinObracunaNaziv = "LETNI OBRAČUN";
                    }
                    else
                    {
                        _MM_Data.NacinObracunaNaziv = "MESEČNI OBRAČUN";
                    }
                    _MM_Data.ObracunSteviloTarif = sql.GetField_String(ref dbReader, "ObracunSteviloTarif").ToString();
                    if (_MM_Data.ObracunSteviloTarif == "1")
                    {
                        _MM_Data.ObracunSteviloTarifNaziv = "ENOTARIFNI OBRAČUN";
                    }
                    else if (_MM_Data.ObracunSteviloTarif == "2")
                    {
                        _MM_Data.ObracunSteviloTarifNaziv = "DVOTARIFNI OBRAČUN";
                    }
                    else if (_MM_Data.ObracunSteviloTarif == "3")
                    {
                        _MM_Data.ObracunSteviloTarifNaziv = "TROTARIFNI OBRAČUN";
                    }
                    _MM_Data.ObracunskeVarovalke = sql.GetField_String(ref dbReader, "ObracunskeVarovalke").Trim();
                    _MM_Data.TarifnaSkupina = sql.GetField_IntNull(ref dbReader, "TarifnaSkupina");
                    _MM_Data.TarifnaSkupinaNaziv = sql.GetField_String(ref dbReader, "TarifnaSkupinaDesc").Trim();
                    _MM_Data.OdjemnaSkupina = sql.GetField_String(ref dbReader, "OdjemnaSkupina").Trim();
                    _MM_Data.OdjemnaSkupinaNaziv = sql.GetField_String(ref dbReader, "OdjemnaSkupinaDesc").Trim();
                    _MM_Data.DalinjskoOdcitavanje = (sql.GetField_IntNull(ref dbReader, "NacinPopisa") == 1);
                    _MM_Data.LocenRacun = (sql.GetField_IntNull(ref dbReader, "SkupniRacun") == 0);
                    if (cCommon.IsInteger(sql.GetField_String(ref dbReader, "StevilkaSoglasja")))
                    {
                        _MM_Data.StevilkaSoglasja = Convert.ToInt32(sql.GetField_String(ref dbReader, "StevilkaSoglasja"));
                    }
                    else
                    {
                        _MM_Data.StevilkaSoglasja = null;
                    }
                    _MM_Data.DatumZadnjePrijave = sql.GetField_DateTimeNull(ref dbReader, "PogodbaOd");
                    _MM_Data.DatumZadnjegaObracuna = sql.GetField_DateTimeNull(ref dbReader, "DatumZadnjegaObracuna");
                    if ((_MM_Data.LocenRacun == null) || ((Boolean)_MM_Data.LocenRacun == false))
                    {
                        _MM_Data.NacinPlacila = "Skupni račun";
                    }
                    else
                    {
                        _MM_Data.NacinPlacila = "Ločen račun - " + sql.GetField_String(ref dbReader, "VrstaPlacila");
                    }
                    _MM_Data.NeAkontacijski = (sql.GetField_IntNull(ref dbReader, "NeAkontacijski") == 0);
                }
                else
                {
                    return cFunctionResult.Set(true, (int)efrErrorCodes.NoDataFound, string.Format("Merilno mesto {0} ne obstaja!", _MM_SMM), "", sql.Text);
                }
            }
            catch (Exception ex)
            {
                return cFunctionResult.Set(true, (int)efrErrorCodes.SQLError, ex.Message, "", sql.Text);
            }
            finally
            {
                if (dbReader != null) { dbReader.Close(); }
                sql.Conn_Close();
            }
            return fr;
        }

        public DataTable GetInformativniIzracun(DateTime datum, int? obracun, DateTime? cenik, DateTime? paket, int? uporabnik, int idXmlPaket)
        {
            var dt = new DataTable();
            using (var conn = new SqlConnection(SqlConnStr))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {


                    cmd.CommandText = @"bis_obracun_storitev_informativni";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;

                    var obdobje = datum.Year * 100 + datum.Month; //long(left(as_datum,4)+mid(as_datum,6,2))

                    cmd.Parameters.Add(new SqlParameter("@datum", datum));
                    cmd.Parameters.Add(new SqlParameter("@obracun", obdobje));
                    cmd.Parameters.Add(new SqlParameter("@cenik", datum));
                    cmd.Parameters.Add(new SqlParameter("@paket", datum));
                    cmd.Parameters.Add(new SqlParameter("@uporabnik", -1));
                    cmd.Parameters.Add(new SqlParameter("@id_xmlpaket", idXmlPaket));

                    using (var da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }

                }
            }
            return dt;
        }

        public DataTable GetInformativniIzracunPaket(DateTime datum, int? obracun, DateTime? cenik, DateTime? paket, int? uporabnik, int idXmlPaket, int idAkcija)
        {
            var dt = new DataTable();
            using (var conn = new SqlConnection(SqlConnStr))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {


                    cmd.CommandText = @"bis_obracun_storitev_informativni_paket";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;

                    var obdobje = datum.Year * 100 + datum.Month; //long(left(as_datum,4)+mid(as_datum,6,2))

                    cmd.Parameters.Add(new SqlParameter("@datum", datum));
                    cmd.Parameters.Add(new SqlParameter("@obracun", obdobje));
                    cmd.Parameters.Add(new SqlParameter("@cenik", datum));
                    cmd.Parameters.Add(new SqlParameter("@paket", datum));
                    cmd.Parameters.Add(new SqlParameter("@uporabnik", -1));                    
                    cmd.Parameters.Add(new SqlParameter("@id_xmlpaket", idXmlPaket));
                    cmd.Parameters.Add(new SqlParameter("@akcija", idAkcija));

                    using (var da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }

                }
            }
            return dt;
        }

        public sFunctionResult GetMerilnoMestoId(string oznaka, out Int32 idMerilnegaMesta)
        {            
            sFunctionResult fr = cFunctionResult.Init();
            cSQL sql = new cSQL(SqlConnStr);
            SqlDataReader dbReader;

            idMerilnegaMesta = -1;
            sql.Text = @"select id_odjemnega_mesta from bis_odjemna_mesta where oznaka = @oznaka";
            sql.AddSQLParameter("@oznaka", SqlDbType.VarChar, ParameterDirection.Input, oznaka);
            dbReader = sql.Query_Read("cEGP_BIS.Get", ref fr);
            if (!fr.resBool)            
                return fr;

            try
            {
                if ((dbReader != null) && (dbReader.Read()))
                {
                    idMerilnegaMesta = sql.GetField_Int(ref dbReader, "id_odjemnega_mesta");
                }
                else
                {
                    return cFunctionResult.Set(true, (int)efrErrorCodes.NoDataFound, "Za oznako {0} ni najdenega merilnega mesta.", "", sql.Text);
                }
            }
            catch (Exception ex)
            {
                return cFunctionResult.Set(true, (int)efrErrorCodes.SQLError, ex.Message, "", sql.Text);
            }
            finally
            {
                if (dbReader != null) { dbReader.Close(); }
                sql.Conn_Close();
            }
            return fr;
        }

        public sFunctionResult GetZadnjiOdcitekCakalnica(string oznaka, out dcPerunOdcitek odcitek)
        {
            sFunctionResult fr = cFunctionResult.Init();
            odcitek = null;
            try
            {
                using (var conn = new SqlConnection(SqlConnStr))
                {
                    conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "p_perun3_job_oddaja_odcitkov_zadnji";
                        cmd.Parameters.AddWithValue("@oznaka", oznaka);
                        using (var rdr = cmd.ExecuteReader())
                        {
                            if (rdr.Read())
                            {
                                odcitek = new dcPerunOdcitek();
                                odcitek.DatumStanja = (rdr.IsDBNull(0) ? (DateTime?)null : rdr.GetDateTime(0));
                                odcitek.StanjeET = (rdr.IsDBNull(1) ? (string)null : rdr.GetString(1));
                                odcitek.StanjeVT = (rdr.IsDBNull(2) ? (string)null : rdr.GetString(2));
                                odcitek.StanjeMT = (rdr.IsDBNull(3) ? (string)null : rdr.GetString(3));
                                odcitek.Vir = (rdr.IsDBNull(4) ? (string)null : rdr.GetString(4));
                                odcitek.JobStatus = (rdr.IsDBNull(5) ? (Int32?)null : rdr.GetInt32(5));
                                odcitek.JobMessage = (rdr.IsDBNull(6) ? (string)null : rdr.GetString(6));
                                odcitek.VrstaOdcitka = (rdr.IsDBNull(7) ? (Int32?)null : rdr.GetInt32(7));
                            }
                        }
                    }            
                }
            }
            catch (Exception ex)
            {
                return cFunctionResult.Set(true, (int)efrErrorCodes.SQLError, ex.Message, ex.Message, string.Empty);
            }

            return cFunctionResult.Set(true, (int)efrErrorCodes.OK, string.Empty, string.Empty, string.Empty);
        }

        public sFunctionResult GetZadnjiOdcitekCache(string oznaka, out dcPerunOdcitek odcitek)
        {
            sFunctionResult fr = cFunctionResult.Init();
            odcitek = null;
            try
            {
                using (var conn = new SqlConnection(SqlConnStr))
                {
                    conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "p_perun3_zadnji_odcitek_cache";
                        cmd.Parameters.AddWithValue("@oznaka", oznaka);
                        using (var rdr = cmd.ExecuteReader())
                        {
                            if (rdr.Read())
                            {
                                odcitek = new dcPerunOdcitek();
                                odcitek.DatumStanja = (rdr.IsDBNull(0) ? (DateTime?)null : rdr.GetDateTime(0));
                                odcitek.StanjeET = (rdr.IsDBNull(1) ? (string)null : rdr.GetString(1));
                                odcitek.StanjeVT = (rdr.IsDBNull(1) ? (string)null : rdr.GetString(2));
                                odcitek.StanjeMT = (rdr.IsDBNull(1) ? (string)null : rdr.GetString(3));
                                odcitek.VrstaOdcitka = (int)VrsteOdcitka.PerunCache;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return cFunctionResult.Set(true, (int)efrErrorCodes.SQLError, ex.Message, ex.Message, string.Empty);
            }

            return cFunctionResult.Set(true, (int)efrErrorCodes.OK, string.Empty, string.Empty, string.Empty);
        }

        public sFunctionResult Get_MM_Plin(Int32 _MM_SMM, out dcMerilnoMesto _MM_Data)
        {
            sFunctionResult fr = cFunctionResult.Init();
            cSQL sql = new cSQL(SqlConnStr);
            SqlDataReader dbReader;
            _MM_Data = this.Init_dcMerilnoMesto();
            sql.Text = sql.Text + "select sOM.id_odjemnega_mesta SMM, isnull(so.SistemskiOperaterID, '') as DistributerID, isnull(so.Naziv, '') as DistributerNaziv, sOM.veza DistributerSMM, sOM.tip_odjemnega_mesta, status_odjemnega_mesta Status, sOM.naziv Naziv, sOM.datum_poracun_do DatumZadnjegaObracuna," + cCommon.CR();
            sql.Text = sql.Text + "       rtrim(coalesce(sOM.ulica, sOM.kraj)) + ' ' + IsNull(sOM.hisna_stevilka , '') + ' ' + IsNull(sOM.dodatek_hs,'') Naslov," + cCommon.CR();
            sql.Text = sql.Text + "       sOM.postna_stevilka + ' ' + sOM.kraj Posta, sOM.vrsta_porabe NacinObracuna, sOM.stevilka_stanovanja," + cCommon.CR();
            sql.Text = sql.Text + "       rtrim(ltrim(eOM.nacin_obracuna)) ObracunSteviloTarif, eOM.obracunska_varovalka ObracunskeVarovalke," + cCommon.CR();
            sql.Text = sql.Text + "       eOM.odjemna_skupina OdjemnaSkupina, os.opis OdjemnaSkupinaDesc, eOM.tarifna_skupina TarifnaSkupina, ts.Opis TarifnaSkupinaDesc, eOM.nacinpopisa NacinPopisa, eOM.SkupniRacun," + cCommon.CR();
            sql.Text = sql.Text + "       eOM.soglasje_ees StevilkaSoglasja, eOM.NeAkontacijski NeAkontacijski, eOM.Sirena Sirena," + cCommon.CR();
            sql.Text = sql.Text + "       PG.datum_veljavnosti_od PogodbaOd," + cCommon.CR();
            sql.Text = sql.Text + "       vp.NAZIV_PLACILA VrstaPlacila" + cCommon.CR();
            sql.Text = sql.Text + "from " + LocalTables.Get_3Tav_bis_odjemna_mesta() + " sOM left join " + LocalTables.Get_3Tav_bie_odjemna_mesta() + " eOM on (sOM.id_odjemnega_mesta=eOM.id_odjemnega_mesta)" + cCommon.CR();
            sql.Text = sql.Text + "                                                          left join " + LocalTables.Get_3Tav_Bie_SistemskiOperater() + " so on (sOM.sodo=so.SistemskiOperaterID) " + cCommon.CR();
            sql.Text = sql.Text + "                                                          left join " + LocalTables.Get_3Tav_bis_pogodbe_gl() + " PG on (sOM.pogodba=PG.stevilka_pogodbe)" + cCommon.CR();
            sql.Text = sql.Text + "                                                          left join " + LocalTables.Get_3Tav_bie_odjemna_skupina() + " os on (eOM.odjemna_skupina=os.odjemna_skupina)" + cCommon.CR();
            sql.Text = sql.Text + "                                                          left join " + LocalTables.Get_3Tav_bie_omr_SifTarifneSkupine() + " ts on (eOM.tarifna_skupina=ts.TSK)" + cCommon.CR();
            sql.Text = sql.Text + "                                                          left join " + LocalTables.Get_3Tav_Vrste_Placil() + " vp on (PG.nacin_placila=vp.SIFRA_VRSTE_PLACILA)" + cCommon.CR();
            sql.Text = sql.Text + "where sOM.id_odjemnega_mesta = @id_odjemnega_mesta" + cCommon.CR();

            sql.AddSQLParameter("@id_odjemnega_mesta", SqlDbType.Int, ParameterDirection.Input, _MM_SMM);
            dbReader = sql.Query_Read("cEGP_BIS.Get", ref fr);
            if (!fr.resBool)
            {
                return fr;
            }
            try
            {
                if ((dbReader != null) && (dbReader.Read()))
                {
                    _MM_Data.SMM = sql.GetField_Int(ref dbReader, "SMM");
                    _MM_Data.TipOdjemnegaMesta = sql.GetField_Int(ref dbReader, "tip_odjemnega_mesta");
                    _MM_Data.Status = sql.GetField_Int(ref dbReader, "Status");
                    _MM_Data.SODO = sql.GetField_Int(ref dbReader, "DistributerID");
                    _MM_Data.SODONaziv = sql.GetField_String(ref dbReader, "DistributerNaziv").Trim();
                    if (cCommon.IsInteger(sql.GetField_String(ref dbReader, "DistributerSMM").Trim()))
                    {
                        _MM_Data.SODO_SMM = Convert.ToInt32(sql.GetField_String(ref dbReader, "DistributerSMM").Trim());
                    }
                    _MM_Data.Naziv = sql.GetField_String(ref dbReader, "Naziv").Trim();
                    _MM_Data.Naslov = sql.GetField_String(ref dbReader, "Naslov").Trim();
                    _MM_Data.Posta = sql.GetField_String(ref dbReader, "Posta").Trim();
                    _MM_Data.StevilkaStanovanja = sql.GetField_String(ref dbReader, "stevilka_stanovanja").Trim();
                    _MM_Data.NacinObracuna = sql.GetField_IntNull(ref dbReader, "NacinObracuna");
                    if (_MM_Data.NacinObracuna == 1)
                    {
                        _MM_Data.NacinObracunaNaziv = "LETNI OBRAČUN";
                    }
                    else
                    {
                        _MM_Data.NacinObracunaNaziv = "MESEČNI OBRAČUN";
                    }
                    _MM_Data.ObracunSteviloTarif = sql.GetField_String(ref dbReader, "ObracunSteviloTarif").ToString();
                    if (_MM_Data.ObracunSteviloTarif == "1")
                    {
                        _MM_Data.ObracunSteviloTarifNaziv = "ENOTARIFNI OBRAČUN";
                    }
                    else if (_MM_Data.ObracunSteviloTarif == "2")
                    {
                        _MM_Data.ObracunSteviloTarifNaziv = "DVOTARIFNI OBRAČUN";
                    }
                    else if (_MM_Data.ObracunSteviloTarif == "3")
                    {
                        _MM_Data.ObracunSteviloTarifNaziv = "TROTARIFNI OBRAČUN";
                    }
                    _MM_Data.ObracunskeVarovalke = sql.GetField_String(ref dbReader, "ObracunskeVarovalke").Trim();
                    _MM_Data.TarifnaSkupina = sql.GetField_IntNull(ref dbReader, "TarifnaSkupina");
                    _MM_Data.TarifnaSkupinaNaziv = sql.GetField_String(ref dbReader, "TarifnaSkupinaDesc").Trim();
                    _MM_Data.OdjemnaSkupina = sql.GetField_String(ref dbReader, "OdjemnaSkupina").Trim();
                    _MM_Data.OdjemnaSkupinaNaziv = sql.GetField_String(ref dbReader, "OdjemnaSkupinaDesc").Trim();
                    _MM_Data.DalinjskoOdcitavanje = (sql.GetField_IntNull(ref dbReader, "NacinPopisa") == 1);
                    _MM_Data.LocenRacun = (sql.GetField_IntNull(ref dbReader, "SkupniRacun") == 0);
                    if (cCommon.IsInteger(sql.GetField_String(ref dbReader, "StevilkaSoglasja")))
                    {
                        _MM_Data.StevilkaSoglasja = Convert.ToInt32(sql.GetField_String(ref dbReader, "StevilkaSoglasja"));
                    }
                    else
                    {
                        _MM_Data.StevilkaSoglasja = null;
                    }
                    _MM_Data.DatumZadnjePrijave = sql.GetField_DateTimeNull(ref dbReader, "PogodbaOd");
                    _MM_Data.DatumZadnjegaObracuna = sql.GetField_DateTimeNull(ref dbReader, "DatumZadnjegaObracuna");
                    if ((_MM_Data.LocenRacun == null) || ((Boolean)_MM_Data.LocenRacun == false))
                    {
                        _MM_Data.NacinPlacila = "Skupni račun";
                    }
                    else
                    {
                        _MM_Data.NacinPlacila = "Ločen račun - " + sql.GetField_String(ref dbReader, "VrstaPlacila");
                    }
                    _MM_Data.NeAkontacijski = (sql.GetField_IntNull(ref dbReader, "NeAkontacijski") == 0);
                }
                else
                {
                    return cFunctionResult.Set(true, (int)efrErrorCodes.NoDataFound, string.Format("Ni podatkov za merilno mesto {0}!", _MM_SMM), "", sql.Text);
                }
            }
            finally
            {
                if (dbReader != null) { dbReader.Close(); }
                sql.Conn_Close();
            }
            return fr;
        }

        public sFunctionResult Get_EnergijskoMerilnoMestoIDZaOmrezninskoMerilnoMestoID(Int32 _SifraDistribucije, Int32 _SifraOmrezninskegaMerilnegaMesta, out Int32? _EnergijskoMM)
        {
            sFunctionResult fr = cFunctionResult.Init();
            cSQL sql = new cSQL(SqlConnStr);
            SqlDataReader dbReader;
            _EnergijskoMM = null;
            sql.Text = sql.Text + "select sOm.id_odjemnega_mesta" + cCommon.CR();
            sql.Text = sql.Text + "from bis_odjemna_mesta sOm inner join Bie_SistemskiOperater s on (sOm.SODO=s.poslovni_partner)" + cCommon.CR();
            sql.Text = sql.Text + "where sOm.veza = @id_odjemnega_mesta" + cCommon.CR();
            sql.Text = sql.Text + "  and sOm.status_odjemnega_mesta = 1" + cCommon.CR();
            sql.Text = sql.Text + "  and s.SistemskiOperaterID = @SistemskiOperaterID" + cCommon.CR();
            sql.AddSQLParameter("@id_odjemnega_mesta", SqlDbType.Int, ParameterDirection.Input, _SifraOmrezninskegaMerilnegaMesta);
            sql.AddSQLParameter("@SistemskiOperaterID", SqlDbType.Int, ParameterDirection.Input, _SifraDistribucije);
            dbReader = sql.Query_Read("cEGP_BIS.Get_EnergijskoMerilnoMestoIDZaOmrezninskoMerilnoMestoID", ref fr);
            if (!fr.resBool)
            {
                return fr;
            }
            try
            {
                if ((dbReader != null) && (dbReader.Read()))
                {
                    _EnergijskoMM = sql.GetField_Int(ref dbReader, "id_odjemnega_mesta");
                }
                else
                {
                    return cFunctionResult.Set(true, (int)efrErrorCodes.NoDataFound, "Za zahtevane omejitve ni rezultatov", "", sql.Text);
                }
            }
            finally
            {
                if (dbReader != null) { dbReader.Close(); }
                sql.Conn_Close();
            }
            return fr;

        }
        /// <summary>
        /// Dobimo podatke o nameščenih napravah
        /// </summary>
        /// <param name="_MM_SMM">Številka merilnega mesta</param>
        /// <param name="_VrstaNaprave">Vrsta naprave (opcijsko)</param>
        /// <param name="_NamesceneNaprave">out List<sNamescenaNaprava></param>
        /// <returns></returns>
        public sFunctionResult Get_MountedDevices(Int32 _MM_SMM, String _VrstaNaprave, out List<dcNamescenaNaprava> _NamesceneNaprave)
        {
            sFunctionResult fr = cFunctionResult.Init();
            cSQL sql = new cSQL(SqlConnStr);
            SqlDataReader dbReader;
            dcNamescenaNaprava NamescenaNaprava;

            _NamesceneNaprave = new List<dcNamescenaNaprava>(0);
            sql.Text = sql.Text + "select eOM.id_odjemnega_mesta SMM, VRSTA_MKN VrstaNaprave, smn.TipStevcaId TipID, TIP_OPIS TipNaziv, smn.SIFRA_MERILNE_NAPRAVE TovSt," + cCommon.CR();
            sql.Text = sql.Text + "       year(datum_izdelave) LetoIzdelave, year(datum_atesta) LetoZiga, ns.datumnamestitve DatumNamestitve," + cCommon.CR();
            sql.Text = sql.Text + "       SteviloVnosnihMest SteviloMestOdbirka, SteviloDecimalnihMest SteviloDecimalnihMest," + cCommon.CR();
            sql.Text = sql.Text + "       eOM.dobavitelj Dobavitelj, eOM.nacin_obracuna ObracunSteviloTarif, VRSTA_STEVILCNIKA, ST_FAZ" + cCommon.CR();
            sql.Text = sql.Text + "FROM " + LocalTables.Get_3Tav_bie_odjemna_mesta() + " eOM inner join " + LocalTables.Get_3Tav_bie_omnamescenistevci() + " ns on (eOM.id_odjemnega_mesta=ns.idodjemnegamesta)" + cCommon.CR();
            sql.Text = sql.Text + "                                                          inner join " + LocalTables.Get_3Tav_bis_merilne_naprave() + " smn on (ns.idstevca=smn.id_stevca)" + cCommon.CR();
            sql.Text = sql.Text + "                                                          inner join " + LocalTables.Get_3Tav_bie_merilne_naprave() + " emn on (smn.id_stevca=emn.MerilnaNaprava)" + cCommon.CR();
            sql.Text = sql.Text + "                                                          inner join " + LocalTables.Get_3Tav_bie_tipi_merilnih_naprav() + " on (TIP_SIF=smn.TipStevcaId)" + cCommon.CR();
            sql.Text = sql.Text + "where eOM.id_odjemnega_mesta = @id_odjemnega_mesta" + cCommon.CR();
            sql.Text = sql.Text + "  and ns.DatumOdstranitve is null" + cCommon.CR();
            if ((_VrstaNaprave != null) && (_VrstaNaprave != ""))
            {
                sql.Text = sql.Text + "  and VRSTA_MKN = @VRSTA_MKN" + cCommon.CR();
            }
            sql.Text = sql.Text + "order by VrstaNaprave, TipID" + cCommon.CR();

            sql.AddSQLParameter("@id_odjemnega_mesta", SqlDbType.Int, ParameterDirection.Input, _MM_SMM);
            if ((_VrstaNaprave != null) && (_VrstaNaprave != ""))
            {
                sql.AddSQLParameter("@VRSTA_MKN", SqlDbType.VarChar, ParameterDirection.Input, _VrstaNaprave);
            }

            dbReader = sql.Query_Read("cEGP_BIS.Get_MountedDevices", ref fr);
            if (!fr.resBool)
            {
                return fr;
            }
            try
            {
                if (dbReader != null)
                {
                    while (dbReader.Read())
                    {
                        NamescenaNaprava = Init_dcNamescenaNaprava();
                        NamescenaNaprava.SMM = sql.GetField_Int(ref dbReader, "SMM");
                        NamescenaNaprava.VrstaNaprave = sql.GetField_String(ref dbReader, "VrstaNaprave").Trim();
                        NamescenaNaprava.TipID = sql.GetField_Int(ref dbReader, "TipID");
                        NamescenaNaprava.TipNaziv = sql.GetField_String(ref dbReader, "TipNaziv").Trim();
                        NamescenaNaprava.TovSt = sql.GetField_String(ref dbReader, "TovSt").Trim();
                        NamescenaNaprava.LetoIzdelave = sql.GetField_IntNull(ref dbReader, "LetoIzdelave");
                        NamescenaNaprava.LetoZiga = sql.GetField_IntNull(ref dbReader, "LetoZiga");
                        NamescenaNaprava.DatumNamestitve = sql.GetField_DateTime(ref dbReader, "DatumNamestitve").Date;
                        NamescenaNaprava.SteviloMestOdbirka = sql.GetField_IntNull(ref dbReader, "SteviloMestOdbirka");
                        NamescenaNaprava.SteviloDecimalnihMest = sql.GetField_IntNull(ref dbReader, "SteviloDecimalnihMest");
                        if (sql.GetField_IntNull(ref dbReader, "ObracunSteviloTarif") == 1)
                        {
                            if ((sql.GetField_Int(ref dbReader, "OznakaMerjeneMoci") == 2) || (sql.GetField_Int(ref dbReader, "OznakaMerjeneMoci") == 3))
                            {
                                NamescenaNaprava.VnosnaPoljaStanj = "DVT;DMT";
                            }
                            else
                            {
                                NamescenaNaprava.VnosnaPoljaStanj = "DET";
                            }
                        }
                        else
                        {
                            NamescenaNaprava.VnosnaPoljaStanj = "DVT;DMT";
                        }
                        _NamesceneNaprave.Add(NamescenaNaprava);
                    }
                }
                else
                {
                    return cFunctionResult.Set(false, (int)efrErrorCodes.NoDataFound, "Za zahtevane omejitve ni rezultatov", "", "");
                }
            }
            finally
            {
                if (dbReader != null) { dbReader.Close(); }
                sql.Conn_Close();
            }
            return fr;
        }
        /// <summary>
        /// Dobimo podatke o nameščeni napravi
        /// </summary>
        /// <param name="_MM_SMM">Številka merilnega mesta</param>
        /// <param name="_TipID">ID tipa naprave</param>
        /// <param name="_TovSt">Tovarniška številka naprave</param> 
        /// <param name="_NamescenaNaprava">out sNamescenaNaprava</param>
        /// <returns></returns>
        public sFunctionResult Get_MountedDevice(Int32 _MM_SMM, Int32 _TipID, String _TovSt, out dcNamescenaNaprava _NamescenaNaprava)
        {
            sFunctionResult fr = cFunctionResult.Init();
            cSQL sql = new cSQL(SqlConnStr);
            SqlDataReader dbReader;

            _NamescenaNaprava = Init_dcNamescenaNaprava();
            sql.Text = sql.Text + "select eOM.id_odjemnega_mesta SMM, VRSTA_MKN VrstaNaprave, smn.TipStevcaId TipID, TIP_OPIS TipNaziv, smn.SIFRA_MERILNE_NAPRAVE TovSt," + cCommon.CR();
            sql.Text = sql.Text + "       year(datum_izdelave) LetoIzdelave, year(datum_atesta) LetoZiga, ns.datumnamestitve DatumNamestitve," + cCommon.CR();
            sql.Text = sql.Text + "       SteviloVnosnihMest SteviloMestOdbirka, SteviloDecimalnihMest SteviloDecimalnihMest," + cCommon.CR();
            sql.Text = sql.Text + "       eOM.dobavitelj Dobavitelj, eOM.nacin_obracuna ObracunSteviloTarif, VRSTA_STEVILCNIKA, ST_FAZ" + cCommon.CR();
            sql.Text = sql.Text + "FROM " + LocalTables.Get_3Tav_bie_odjemna_mesta() + " eOM inner join " + LocalTables.Get_3Tav_bie_omnamescenistevci() + " ns on (eOM.id_odjemnega_mesta=ns.idodjemnegamesta)" + cCommon.CR();
            sql.Text = sql.Text + "                                                          inner join " + LocalTables.Get_3Tav_bis_merilne_naprave() + " smn on (ns.idstevca=smn.id_stevca)" + cCommon.CR();
            sql.Text = sql.Text + "                                                          inner join " + LocalTables.Get_3Tav_bie_merilne_naprave() + " emn on (smn.id_stevca=emn.MerilnaNaprava)" + cCommon.CR();
            sql.Text = sql.Text + "                                                          inner join " + LocalTables.Get_3Tav_bie_tipi_merilnih_naprav() + " on (TIP_SIF=smn.TipStevcaId)" + cCommon.CR();
            sql.Text = sql.Text + "where eOM.id_odjemnega_mesta = @id_odjemnega_mesta" + cCommon.CR();
            sql.Text = sql.Text + "  and ns.DatumOdstranitve is null" + cCommon.CR();
            sql.Text = sql.Text + "  and smn.TipStevcaId = @TipStevcaId" + cCommon.CR();
            sql.Text = sql.Text + "  and smn.SIFRA_MERILNE_NAPRAVE = @SIFRA_MERILNE_NAPRAVE" + cCommon.CR();
            sql.Text = sql.Text + "order by VrstaNaprave, TipID" + cCommon.CR();

            sql.AddSQLParameter("@id_odjemnega_mesta", SqlDbType.Int, ParameterDirection.Input, _MM_SMM);
            sql.AddSQLParameter("@TipStevcaId", SqlDbType.Int, ParameterDirection.Input, _TipID);
            sql.AddSQLParameter("@SIFRA_MERILNE_NAPRAVE", SqlDbType.VarChar, ParameterDirection.Input, _TovSt);
            dbReader = sql.Query_Read("cEGP_BIS.Get_MountedDevice", ref fr);
            if (!fr.resBool)
            {
                return fr;
            }
            try
            {
                if ((dbReader != null) && (dbReader.Read()))
                {
                    _NamescenaNaprava = Init_dcNamescenaNaprava();
                    _NamescenaNaprava.SMM = sql.GetField_Int(ref dbReader, "SMM");
                    _NamescenaNaprava.VrstaNaprave = sql.GetField_String(ref dbReader, "VrstaNaprave").Trim();
                    _NamescenaNaprava.TipID = sql.GetField_Int(ref dbReader, "TipID");
                    _NamescenaNaprava.TipNaziv = sql.GetField_String(ref dbReader, "TipNaziv").Trim();
                    _NamescenaNaprava.TovSt = sql.GetField_String(ref dbReader, "TovSt").Trim();
                    _NamescenaNaprava.LetoIzdelave = sql.GetField_IntNull(ref dbReader, "LetoIzdelave");
                    _NamescenaNaprava.LetoZiga = sql.GetField_IntNull(ref dbReader, "LetoZiga");
                    _NamescenaNaprava.DatumNamestitve = sql.GetField_DateTime(ref dbReader, "DatumNamestitve").Date;
                    _NamescenaNaprava.SteviloMestOdbirka = sql.GetField_IntNull(ref dbReader, "SteviloMestOdbirka");
                    _NamescenaNaprava.SteviloDecimalnihMest = sql.GetField_IntNull(ref dbReader, "SteviloDecimalnihMest");
                    if (sql.GetField_IntNull(ref dbReader, "ObracunSteviloTarif") == 1)
                    {
                        if ((sql.GetField_Int(ref dbReader, "OznakaMerjeneMoci") == 2) || (sql.GetField_Int(ref dbReader, "OznakaMerjeneMoci") == 3))
                        {
                            _NamescenaNaprava.VnosnaPoljaStanj = "DVT;DMT";
                        }
                        else
                        {
                            _NamescenaNaprava.VnosnaPoljaStanj = "DET";
                        }
                    }
                    else
                    {
                        _NamescenaNaprava.VnosnaPoljaStanj = "DVT;DMT";
                    }
                }
                else
                {
                    return cFunctionResult.Set(true, (int)efrErrorCodes.NoDataFound, "Za zahtevane omejitve ni rezultatov", "", "");
                }
            }
            finally
            {
                if (dbReader != null) { dbReader.Close(); }
                sql.Conn_Close();
            }
            return fr;
        }
        /// <summary>
        /// Dobimo podatke o odčitkih
        /// </summary>
        /// <param name="_MM_SMM">Številka merilnega mesta</param>
        /// <param name="_DateFrom">Datum stanj</param> 
        /// <param name="_Odcitek">out sOdcitek</param>
        /// <returns></returns>
        public sFunctionResult Get_SeznamOdcitkov(Int32 _MM_SMM, DateTime? _DateFrom, DateTime? _DateTo, Int32? _SteviloZadnjihOdcitkov, out List<dcOdcitek> _SeznamOdcitkov)
        {
            sFunctionResult fr = cFunctionResult.Init();
            cSQL sql = new cSQL(SqlConnStr);
            SqlDataReader dbReader;
            Int32 SteviloVnosnihMest = 0;
            Int32 SteviloDecimalnihMest = 0;
            dcOdcitek Odcitek;

            _SeznamOdcitkov = new List<dcOdcitek>(0);

            sql.Text = sql.Text + "select ";
            if ((_SteviloZadnjihOdcitkov != null) && (_SteviloZadnjihOdcitkov > 0))
            {
                sql.Text = sql.Text + "TOP " + ((Int32)_SteviloZadnjihOdcitkov).ToString();
            }
            sql.Text = sql.Text + "       o.odjemno_mesto SMM, null TipID, null TovSt, datum_odcitka DatumOdcitka, O.obracun, O.obracunski_paket," + cCommon.CR();
            sql.Text = sql.Text + "       o.odcitek_ET, o.kolicina_ET, o.poraba_prerac_ET, o.odcitek_VT, o.kolicina_VT, o.poraba_prerac_VT, o.odcitek_MT, o.kolicina_MT, o.poraba_prerac_MT, o.odcitek_KT, o.kolicina_KT, o.poraba_prerac_KT, o.odcitek_Moc," + cCommon.CR();
            sql.Text = sql.Text + "       O.vrsta_dokumenta VrstaOdcitka, O.OpisDokumentaId NacinPridobitveOdcitka, null DJA, null SteviloVnosnihMest, null SteviloDecimalnihMest" + cCommon.CR();
            sql.Text = sql.Text + "from " + LocalTables.Get_3Tav_bis_odcitki_v() + " o" + cCommon.CR();
            sql.Text = sql.Text + "where o.odjemno_mesto = @odjemno_mesto" + cCommon.CR();
            sql.Text = sql.Text + "  and vrsta=0" + cCommon.CR();
            if (_DateFrom != null)
                sql.Text = sql.Text + "  and datum_odcitka >=@DateFrom" + cCommon.CR();
            if (_DateTo != null)
                sql.Text = sql.Text + "  and datum_odcitka <=@DateTo" + cCommon.CR();
            sql.Text = sql.Text + "order by O.datum_odcitka desc" + cCommon.CR();

            sql.AddSQLParameter("@odjemno_mesto", SqlDbType.Int, ParameterDirection.Input, _MM_SMM);
            if (_DateFrom != null)
                sql.AddSQLParameter("@DateFrom", SqlDbType.DateTime, ParameterDirection.Input, (DateTime)_DateFrom);
            if (_DateTo != null)
                sql.AddSQLParameter("@DateTo", SqlDbType.DateTime, ParameterDirection.Input, (DateTime)_DateTo);

            dbReader = sql.Query_Read("cEGP_BIS.Get_ZadnjiOdcitek", ref fr);
            if (!fr.resBool)
            {
                return fr;
            }
            try
            {
                if (dbReader != null)
                {
                    while (dbReader.Read())
                    {
                        Odcitek = this.Init_dcOdcitek();

                        SteviloVnosnihMest = sql.GetField_Int(ref dbReader, "SteviloVnosnihMest");
                        if (!sql.GetField_IsNull(ref dbReader, "SteviloDecimalnihMest"))
                        {
                            SteviloDecimalnihMest = sql.GetField_Int(ref dbReader, "SteviloDecimalnihMest");
                        }
                        Odcitek.SMM = sql.GetField_Int(ref dbReader, "SMM");
                        Odcitek.DatumStanja = sql.GetField_DateTime(ref dbReader, "DatumOdcitka").Date;
                        Odcitek.VrstaOdcitka = sql.GetField_Int(ref dbReader, "VrstaOdcitka");
                        Odcitek.NacinPridobitveOdcitka = sql.GetField_Int(ref dbReader, "NacinPridobitveOdcitka");
                        Odcitek.ObracunID = sql.GetField_IntNull(ref dbReader, "obracun");
                        Odcitek.OdcitekObracunan = !(sql.GetField_IntNull(ref dbReader, "obracunski_paket") == 0);

                        Odcitek.TipID_D = 0;
                        Odcitek.TovSt_D = "";

                        Odcitek.VrednostET = sql.GetField_DoubleNull(ref dbReader, "odcitek_ET");
                        if (Odcitek.VrednostET != null)
                        {
                            Odcitek.SlikaET = Convert.ToInt32(Odcitek.VrednostET).ToString();
                            Odcitek.PorabaET = sql.GetField_DoubleNull(ref dbReader, "kolicina_ET");
                            Odcitek.PorabaNaDanET = sql.GetField_DoubleNull(ref dbReader, "poraba_prerac_ET");
                        }

                        Odcitek.VrednostDVT = sql.GetField_DoubleNull(ref dbReader, "odcitek_VT");
                        if (Odcitek.VrednostDVT != null)
                        {
                            Odcitek.SlikaDVT = Convert.ToInt32(Odcitek.VrednostDVT).ToString();
                            Odcitek.PorabaDVT = sql.GetField_DoubleNull(ref dbReader, "kolicina_VT");
                            Odcitek.PorabaNaDanDVT = sql.GetField_DoubleNull(ref dbReader, "poraba_prerac_VT");
                        }

                        Odcitek.VrednostDMT = sql.GetField_DoubleNull(ref dbReader, "odcitek_MT");
                        if (Odcitek.VrednostDMT != null)
                        {
                            Odcitek.SlikaDMT = Convert.ToInt32(Odcitek.VrednostDMT).ToString();
                            Odcitek.PorabaDMT = sql.GetField_DoubleNull(ref dbReader, "kolicina_MT");
                            Odcitek.PorabaNaDanDMT = sql.GetField_DoubleNull(ref dbReader, "poraba_prerac_MT");
                        }

                        Odcitek.VrednostKT = sql.GetField_DoubleNull(ref dbReader, "odcitek_KT");
                        if (Odcitek.VrednostKT != null)
                        {
                            Odcitek.SlikaKT = Convert.ToInt32(Odcitek.VrednostKT).ToString();
                            Odcitek.PorabaKT = sql.GetField_DoubleNull(ref dbReader, "kolicina_KT");
                            Odcitek.PorabaNaDanKT = sql.GetField_DoubleNull(ref dbReader, "poraba_prerac_KT");
                        }
                        Odcitek.Moc = sql.GetField_DoubleNull(ref dbReader, "odcitek_Moc");

                        _SeznamOdcitkov.Add(Odcitek);
                    }
                }
                else
                {
                    return cFunctionResult.Set(true, (int)efrErrorCodes.NoDataFound, "Za zahtevane omejitve ni rezultatov", "", "");
                }
            }
            finally
            {
                if (dbReader != null) { dbReader.Close(); }
                sql.Conn_Close();
            }
            return fr;
        }
        /// <summary>
        /// Dobimo podatke o odčitku za datum
        /// </summary>
        /// <param name="_MM_SMM">Številka merilnega mesta</param>
        /// <param name="_DatumOdcitka">Datum odčitka</param>    
        /// <param name="_Odcitek">out sOdcitek</param>
        /// <returns></returns>
        public sFunctionResult Get_OdcitekZaDatum(Int32 _MM_SMM, DateTime _DatumOdcitka, out dcOdcitek _Odcitek)
        {
            sFunctionResult fr = cFunctionResult.Init();
            cSQL sql = new cSQL(SqlConnStr);
            SqlDataReader dbReader;
            Int32 SteviloVnosnihMest = 0;
            Int32 SteviloDecimalnihMest = 0;

            _Odcitek = Init_dcOdcitek();
            sql.Text = sql.Text + "select o.odjemno_mesto SMM, null TipID, null TovSt, datum_odcitka DatumOdcitka, O.obracun, O.obracunski_paket," + cCommon.CR();
            sql.Text = sql.Text + "       o.odcitek_ET, o.kolicina_ET, o.poraba_prerac_ET, o.odcitek_VT, o.kolicina_VT, o.poraba_prerac_VT, o.odcitek_MT, o.kolicina_MT, o.poraba_prerac_MT, o.odcitek_KT, o.kolicina_KT, o.poraba_prerac_KT, o.odcitek_Moc," + cCommon.CR();
            sql.Text = sql.Text + "       O.vrsta_dokumenta VrstaOdcitka, O.OpisDokumentaId NacinPridobitveOdcitka, null DJA, null SteviloVnosnihMest, null SteviloDecimalnihMest" + cCommon.CR();
            sql.Text = sql.Text + "from " + LocalTables.Get_3Tav_bis_odcitki_v() + " o" + cCommon.CR();
            sql.Text = sql.Text + "where o.odjemno_mesto = @odjemno_mesto" + cCommon.CR();
            sql.Text = sql.Text + "  and vrsta=0" + cCommon.CR();
            sql.Text = sql.Text + "  and O.datum_odcitka = @DatumOdcitka" + cCommon.CR();

            sql.AddSQLParameter("@odjemno_mesto", SqlDbType.Int, ParameterDirection.Input, _MM_SMM);
            sql.AddSQLParameter("@DatumOdcitka", SqlDbType.DateTime, ParameterDirection.Input, _DatumOdcitka);
            dbReader = sql.Query_Read("cEGP_BIS.Get_OdcitekZaDatum", ref fr);
            if (!fr.resBool)
            {
                return fr;
            }
            try
            {
                if (dbReader != null)
                {
                    while (dbReader.Read())
                    {
                        SteviloVnosnihMest = sql.GetField_Int(ref dbReader, "SteviloVnosnihMest");
                        if (!sql.GetField_IsNull(ref dbReader, "SteviloDecimalnihMest"))
                        {
                            SteviloDecimalnihMest = sql.GetField_Int(ref dbReader, "SteviloDecimalnihMest");
                        }
                        _Odcitek.SMM = sql.GetField_Int(ref dbReader, "SMM");
                        _Odcitek.DatumStanja = sql.GetField_DateTime(ref dbReader, "DatumOdcitka").Date;
                        _Odcitek.VrstaOdcitka = sql.GetField_Int(ref dbReader, "VrstaOdcitka");
                        _Odcitek.NacinPridobitveOdcitka = sql.GetField_Int(ref dbReader, "NacinPridobitveOdcitka");
                        _Odcitek.ObracunID = sql.GetField_IntNull(ref dbReader, "obracun");
                        _Odcitek.OdcitekObracunan = !(sql.GetField_IntNull(ref dbReader, "obracunski_paket") == 0);

                        _Odcitek.TipID_D = 0;
                        _Odcitek.TovSt_D = "";

                        _Odcitek.VrednostET = sql.GetField_DoubleNull(ref dbReader, "odcitek_ET");
                        if (_Odcitek.VrednostET != null)
                        {
                            _Odcitek.SlikaET = Convert.ToInt32(_Odcitek.VrednostET).ToString();
                            _Odcitek.PorabaET = sql.GetField_DoubleNull(ref dbReader, "kolicina_ET");
                            _Odcitek.PorabaNaDanET = sql.GetField_DoubleNull(ref dbReader, "poraba_prerac_ET");
                        }

                        _Odcitek.VrednostDVT = sql.GetField_DoubleNull(ref dbReader, "odcitek_VT");
                        if (_Odcitek.VrednostDVT != null)
                        {
                            _Odcitek.SlikaDVT = Convert.ToInt32(_Odcitek.VrednostDVT).ToString();
                            _Odcitek.PorabaDVT = sql.GetField_DoubleNull(ref dbReader, "kolicina_VT");
                            _Odcitek.PorabaNaDanDVT = sql.GetField_DoubleNull(ref dbReader, "poraba_prerac_VT");
                        }

                        _Odcitek.VrednostDMT = sql.GetField_DoubleNull(ref dbReader, "odcitek_MT");
                        if (_Odcitek.VrednostDMT != null)
                        {
                            _Odcitek.SlikaDMT = Convert.ToInt32(_Odcitek.VrednostDMT).ToString();
                            _Odcitek.PorabaDMT = sql.GetField_DoubleNull(ref dbReader, "kolicina_MT");
                            _Odcitek.PorabaNaDanDMT = sql.GetField_DoubleNull(ref dbReader, "poraba_prerac_MT");
                        }

                        _Odcitek.VrednostKT = sql.GetField_DoubleNull(ref dbReader, "odcitek_KT");
                        if (_Odcitek.VrednostKT != null)
                        {
                            _Odcitek.SlikaKT = Convert.ToInt32(_Odcitek.VrednostKT).ToString();
                            _Odcitek.PorabaKT = sql.GetField_DoubleNull(ref dbReader, "kolicina_KT");
                            _Odcitek.PorabaNaDanKT = sql.GetField_DoubleNull(ref dbReader, "poraba_prerac_KT");
                        }
                        _Odcitek.Moc = sql.GetField_DoubleNull(ref dbReader, "odcitek_Moc");
                    }
                }
                else
                {
                    return cFunctionResult.Set(true, (int)efrErrorCodes.NoDataFound, "Za zahtevane omejitve ni rezultatov", "", "");
                }
            }
            finally
            {
                if (dbReader != null) { dbReader.Close(); }
                sql.Conn_Close();
            }
            return fr;
        }
        /// <summary>
        /// Dobimo podatke o zadnjem odčitku
        /// </summary>
        /// <param name="_MM_SMM">Številka merilnega mesta</param>
        /// <param name="_Odcitek">out sOdcitek</param>
        /// <returns></returns>
        public sFunctionResult Get_ZadnjiOdcitek(Int32 _MM_SMM, out dcOdcitek _Odcitek)
        {
            sFunctionResult fr = cFunctionResult.Init();
            cSQL sql = new cSQL(SqlConnStr);
            SqlDataReader dbReader;
            Int32 SteviloVnosnihMest = 0;
            Int32 SteviloDecimalnihMest = 0;

            _Odcitek = Init_dcOdcitek();
            sql.Text = sql.Text + "select o.odjemno_mesto SMM, null TipID, null TovSt, datum_odcitka DatumOdcitka, O.obracun, O.obracunski_paket," + cCommon.CR();
            sql.Text = sql.Text + "       o.odcitek_ET, o.kolicina_ET, o.poraba_prerac_ET, o.odcitek_VT, o.kolicina_VT, o.poraba_prerac_VT, o.odcitek_MT, o.kolicina_MT, o.poraba_prerac_MT, o.odcitek_KT, o.kolicina_KT, o.poraba_prerac_KT, o.odcitek_Moc," + cCommon.CR();
            sql.Text = sql.Text + "       O.vrsta_dokumenta VrstaOdcitka, O.OpisDokumentaId NacinPridobitveOdcitka, null DJA, null SteviloVnosnihMest, null SteviloDecimalnihMest" + cCommon.CR();
            sql.Text = sql.Text + "from " + LocalTables.Get_3Tav_bis_odcitki_v() + " o inner join (select max(datum_odcitka) Max_datum_odcitka, odjemno_mesto from " + LocalTables.Get_3Tav_bis_odcitki_v() + " group by odjemno_mesto) X on ((X.Max_datum_odcitka=O.datum_odcitka) and (X.odjemno_mesto = O.odjemno_mesto))" + cCommon.CR();
            sql.Text = sql.Text + "where o.odjemno_mesto =@odjemno_mesto" + cCommon.CR();
            sql.Text = sql.Text + "  and vrsta=0" + cCommon.CR();
            //sql.Text = sql.Text + "  and ns.DatumOdstranitve is null" + cCommon.CR();
            sql.Text = sql.Text + "order by datum_odcitka desc" + cCommon.CR();

            sql.AddSQLParameter("@odjemno_mesto", SqlDbType.Int, ParameterDirection.Input, _MM_SMM);
            dbReader = sql.Query_Read("cEGP_BIS.Get_ZadnjiOdcitek", ref fr);
            if (!fr.resBool)
            {
                return fr;
            }
            try
            {
                if (dbReader != null)
                {
                    while (dbReader.Read())
                    {
                        SteviloVnosnihMest = sql.GetField_Int(ref dbReader, "SteviloVnosnihMest");
                        if (!sql.GetField_IsNull(ref dbReader, "SteviloDecimalnihMest"))
                        {
                            SteviloDecimalnihMest = sql.GetField_Int(ref dbReader, "SteviloDecimalnihMest");
                        }
                        _Odcitek.SMM = sql.GetField_Int(ref dbReader, "SMM");
                        _Odcitek.DatumStanja = sql.GetField_DateTime(ref dbReader, "DatumOdcitka").Date;
                        _Odcitek.VrstaOdcitka = sql.GetField_Int(ref dbReader, "VrstaOdcitka");
                        _Odcitek.NacinPridobitveOdcitka = sql.GetField_Int(ref dbReader, "NacinPridobitveOdcitka");
                        _Odcitek.ObracunID = sql.GetField_IntNull(ref dbReader, "obracun");
                        _Odcitek.OdcitekObracunan = !(sql.GetField_IntNull(ref dbReader, "obracunski_paket") == 0);

                        _Odcitek.TipID_D = 0;
                        _Odcitek.TovSt_D = "";

                        _Odcitek.VrednostET = sql.GetField_DoubleNull(ref dbReader, "odcitek_ET");
                        if (_Odcitek.VrednostET != null)
                        {
                            _Odcitek.SlikaET = Convert.ToInt32(_Odcitek.VrednostET).ToString();
                            _Odcitek.PorabaET = sql.GetField_DoubleNull(ref dbReader, "kolicina_ET");
                            _Odcitek.PorabaNaDanET = sql.GetField_DoubleNull(ref dbReader, "poraba_prerac_ET");
                        }

                        _Odcitek.VrednostDVT = sql.GetField_DoubleNull(ref dbReader, "odcitek_VT");
                        if (_Odcitek.VrednostDVT != null)
                        {
                            _Odcitek.SlikaDVT = Convert.ToInt32(_Odcitek.VrednostDVT).ToString();
                            _Odcitek.PorabaDVT = sql.GetField_DoubleNull(ref dbReader, "kolicina_VT");
                            _Odcitek.PorabaNaDanDVT = sql.GetField_DoubleNull(ref dbReader, "poraba_prerac_VT");
                        }

                        _Odcitek.VrednostDMT = sql.GetField_DoubleNull(ref dbReader, "odcitek_MT");
                        if (_Odcitek.VrednostDMT != null)
                        {
                            _Odcitek.SlikaDMT = Convert.ToInt32(_Odcitek.VrednostDMT).ToString();
                            _Odcitek.PorabaDMT = sql.GetField_DoubleNull(ref dbReader, "kolicina_MT");
                            _Odcitek.PorabaNaDanDMT = sql.GetField_DoubleNull(ref dbReader, "poraba_prerac_MT");
                        }

                        _Odcitek.VrednostKT = sql.GetField_DoubleNull(ref dbReader, "odcitek_KT");
                        if (_Odcitek.VrednostKT != null)
                        {
                            _Odcitek.SlikaKT = Convert.ToInt32(_Odcitek.VrednostKT).ToString();
                            _Odcitek.PorabaKT = sql.GetField_DoubleNull(ref dbReader, "kolicina_KT");
                            _Odcitek.PorabaNaDanKT = sql.GetField_DoubleNull(ref dbReader, "poraba_prerac_KT");
                        }
                        _Odcitek.Moc = sql.GetField_DoubleNull(ref dbReader, "odcitek_Moc");
                    }
                }
                else
                {
                    return cFunctionResult.Set(true, (int)efrErrorCodes.NoDataFound, "Za zahtevane omejitve ni rezultatov", "", "");
                }
            }
            finally
            {
                if (dbReader != null) { dbReader.Close(); }
                sql.Conn_Close();
            }
            return fr;
        }
        /// <summary>
        /// Dobimo povprečno mesečno porabo za SMM
        /// </summary>
        /// <param name="_MM_SMM">Šifra merilnega mesta</param>
        /// <param name="_PovprecnaPoraba">out povprečna mesečna poraba</param>
        /// <returns></returns>
        public sFunctionResult Get_PovprecnoMesecnoPorabo(Int32 _MM_SMM, out dcPovprecnaMesecnaPoraba _PovprecnaPoraba)
        {
            sFunctionResult fr = cFunctionResult.Init();
            cSQL sql = new cSQL(SqlConnStr);
            SqlDataReader dbReader;

            _PovprecnaPoraba = Init_dcPovprecnaPoraba();
            sql.Text = sql.Text + "select id_odjemnega_mesta, tarifa, PovprecnaKolicina, TipObdobja, Z302LET, Z302ZAP" + cCommon.CR();
            sql.Text = sql.Text + "from " + LocalTables.Get_3Tav_bis_odjemna_mesta_povprecja() + cCommon.CR();
            sql.Text = sql.Text + "where id_odjemnega_mesta =@odjemno_mesto" + cCommon.CR();

            sql.AddSQLParameter("@odjemno_mesto", SqlDbType.Int, ParameterDirection.Input, _MM_SMM);
            dbReader = sql.Query_Read("cEGP_BIS.Get_PovprecnoMesecnoPorabo", ref fr);
            if (!fr.resBool)
            {
                return fr;
            }
            try
            {
                if (dbReader != null)
                {
                    while (dbReader.Read())
                    {
                        if ((_PovprecnaPoraba.SMM == null) || (_PovprecnaPoraba.SMM == 0))
                        {
                            _PovprecnaPoraba.SMM = sql.GetField_Int(ref dbReader, "id_odjemnega_mesta");
                        }
                        if (sql.GetField_Int(ref dbReader, "tarifa") == 6)
                        {
                            _PovprecnaPoraba.PovprecjeET = Convert.ToInt32(sql.GetField_Double(ref dbReader, "PovprecnaKolicina") * 30.5);
                        }
                        if (sql.GetField_Int(ref dbReader, "tarifa") == 4)
                        {
                            _PovprecnaPoraba.PovprecjeDVT = Convert.ToInt32(sql.GetField_Double(ref dbReader, "PovprecnaKolicina") * 30.5);
                        }
                        if (sql.GetField_Int(ref dbReader, "tarifa") == 5)
                        {
                            _PovprecnaPoraba.PovprecjeDMT = Convert.ToInt32(sql.GetField_Double(ref dbReader, "PovprecnaKolicina") * 30.5);
                        }
                    }
                }
                else
                {
                    return cFunctionResult.Set(true, (int)efrErrorCodes.NoDataFound, "Za zahtevane omejitve ni rezultatov", "", "");
                }
            }
            finally
            {
                if (dbReader != null) { dbReader.Close(); }
                sql.Conn_Close();
            }
            return fr;
        }
        /// <summary>
        /// Dobimo seznam količine za obdobje iz realizacije za SMM
        /// </summary>
        /// <param name="_MM_SMM">Šifra merilnega mesta</param>
        /// <param name="_ObdobjeRealizacijeOd">Obdobje realizacije od YYYYMM</param>
        /// <param name="_KolicinePoObdobjuRealizacijeList">out količine iz realizacije list</param>
        /// <returns></returns>
        public sFunctionResult Get_KolicinePoObdobjuRealizacijeList(Int32 _MM_SMM, Int32 Leto, Int32 Mesec, out List<dcKolicinePoObdobjuRealizacije> _KolicinePoObdobjuRealizacijeList)
        {
            sFunctionResult fr = cFunctionResult.Init();
            cSQL sql = new cSQL(SqlConnStr);
            SqlDataReader dbReader;
            dcKolicinePoObdobjuRealizacije KolicinePoObdobjuRealizacije;
            _KolicinePoObdobjuRealizacijeList = new List<dcKolicinePoObdobjuRealizacije>(0);

            Int32 RealizacijaMax;
            Int32 RealizacijaMin;

            RealizacijaMax = Leto * 100 + Mesec;
            RealizacijaMin = (Leto - 1) * 100 + Mesec;
            sql.Text = sql.Text + "select *" + cCommon.CR();
            sql.Text = sql.Text + "from " + LocalTables.Get_3Tav_dwh_RealizacijaKolicine() + cCommon.CR();
            sql.Text = sql.Text + "where id_merilnega_mesta = @odjemno_mesto" + cCommon.CR();
            sql.Text = sql.Text + "  and ObdobjeRealizacije <= @RealizacijaMax" + cCommon.CR();
            sql.Text = sql.Text + "  and ObdobjeRealizacije > @RealizacijaMin" + cCommon.CR();

            sql.AddSQLParameter("@odjemno_mesto", SqlDbType.Int, ParameterDirection.Input, _MM_SMM);
            sql.AddSQLParameter("@RealizacijaMax", SqlDbType.Int, ParameterDirection.Input, RealizacijaMax);
            sql.AddSQLParameter("@RealizacijaMin", SqlDbType.Int, ParameterDirection.Input, RealizacijaMin);
            dbReader = sql.Query_Read("cEGP_BIS.Get_KolicinePoObdobjuRealizacijeList", ref fr);
            if (!fr.resBool)
            {
                return fr;
            }
            try
            {
                if (dbReader != null)
                {
                    while (dbReader.Read())
                    {
                        KolicinePoObdobjuRealizacije = Init_dcKolicinePoObdobjuRealizacije();
                        KolicinePoObdobjuRealizacije.SMM = sql.GetField_IntNull(ref dbReader, "id_merilnega_mesta");
                        KolicinePoObdobjuRealizacije.LetoMesec = sql.GetField_Int(ref dbReader, "Leto").ToString() + '-' + cCommon.Add_LeadingZeros(sql.GetField_Int(ref dbReader, "Mesec").ToString(), 2);
                        KolicinePoObdobjuRealizacije.ObdobjeRealizacije = sql.GetField_IntNull(ref dbReader, "ObdobjeRealizacije");
                        KolicinePoObdobjuRealizacije.Kolicina_Moc = Convert.ToInt32(sql.GetField_Double(ref dbReader, "Moc"));
                        KolicinePoObdobjuRealizacije.Kolicina_DVT = Convert.ToInt32(sql.GetField_Double(ref dbReader, "Energija_DVT"));
                        KolicinePoObdobjuRealizacije.Kolicina_DMT = Convert.ToInt32(sql.GetField_Double(ref dbReader, "Energija_DMT"));
                        KolicinePoObdobjuRealizacije.Kolicina_KT = Convert.ToInt32(sql.GetField_Double(ref dbReader, "Energija_DKT"));
                        KolicinePoObdobjuRealizacije.Kolicina_DET = Convert.ToInt32(sql.GetField_Double(ref dbReader, "Energija_DET"));
                        KolicinePoObdobjuRealizacije.Kolicina_Skupaj_D = Convert.ToInt32(sql.GetField_Double(ref dbReader, "EnergijaDelovna"));
                        _KolicinePoObdobjuRealizacijeList.Add(KolicinePoObdobjuRealizacije);
                    }
                }
                else
                {
                    return cFunctionResult.Set(true, (int)efrErrorCodes.NoDataFound, "Za zahtevane omejitve ni rezultatov", "", "");
                }
            }
            finally
            {
                if (dbReader != null) { dbReader.Close(); }
                sql.Conn_Close();
            }
            return fr;
        }
        /// <summary>
        /// Dobimo sumarno količine za obdobje iz realizacije za SMM
        /// </summary>
        /// <param name="_MM_SMM">Šifra merilnega mesta</param>
        /// <param name="_ObdobjeRealizacijeOd">Obdobje realizacije od YYYYMM</param>
        /// <param name="_SumKolicinePoObdobjuRealizacije">out vsota količine iz realizacije</param>
        /// <returns></returns>
        public sFunctionResult Get_KolicinePoObdobjuRealizacijeSum(Int32 _MM_SMM, Int32 Leto, Int32 Mesec, Boolean NarediLetnoAproksimacijo, out dcKolicinePoObdobjuRealizacije _SumKolicinePoObdobjuRealizacije)
        {
            sFunctionResult fr = cFunctionResult.Init();
            cSQL sql = new cSQL(SqlConnStr);
            SqlDataReader dbReader;
            Int32 RealizacijaMax;
            Int32 RealizacijaMin;

            RealizacijaMax = Leto * 100 + Mesec;
            RealizacijaMin = (Leto - 1) * 100 + Mesec;
            _SumKolicinePoObdobjuRealizacije = Init_dcKolicinePoObdobjuRealizacije();
            sql.Text = sql.Text + "select id_merilnega_mesta, min(ObdobjeRealizacije) MinObdobjeRealizacije, max(ObdobjeRealizacije) MaxObdobjeRealizacije, sum(Moc) S_MOC, sum(Energija_DVT) S_DVT, sum(Energija_DMT) S_DMT, sum(Energija_DET) S_DET, sum(Energija_DKT) S_DKT, sum(EnergijaDelovna) S_ENERGIJA_D, sum(Poraba_JVT) S_JVT, sum(Poraba_JMT) S_JMT, sum(EnergijaJalova) S_JZA" + cCommon.CR();
            sql.Text = sql.Text + "from " + LocalTables.Get_3Tav_dwh_RealizacijaKolicine() + cCommon.CR();
            sql.Text = sql.Text + "where id_merilnega_mesta = @odjemno_mesto" + cCommon.CR();
            sql.Text = sql.Text + "  and ObdobjeRealizacije <= @RealizacijaMax" + cCommon.CR();
            sql.Text = sql.Text + "  and ObdobjeRealizacije > @RealizacijaMin" + cCommon.CR();
            sql.Text = sql.Text + "group by id_merilnega_mesta" + cCommon.CR();

            sql.AddSQLParameter("@odjemno_mesto", SqlDbType.Int, ParameterDirection.Input, _MM_SMM);
            sql.AddSQLParameter("@RealizacijaMax", SqlDbType.Int, ParameterDirection.Input, RealizacijaMax);
            sql.AddSQLParameter("@RealizacijaMin", SqlDbType.Int, ParameterDirection.Input, RealizacijaMin);
            dbReader = sql.Query_Read("cEGP_BIS.Get_KolicinePoObdobjuRealizacijeSum", ref fr);
            if (!fr.resBool)
            {
                return fr;
            }
            try
            {
                if ((dbReader != null) && (dbReader.Read()))
                {
                    if (NarediLetnoAproksimacijo)
                    {
                        String LetoMin = (sql.GetField_Int(ref dbReader, "MinObdobjeRealizacije").ToString()).Substring(0, 4);
                        String MesecMin = (sql.GetField_Int(ref dbReader, "MinObdobjeRealizacije").ToString()).Substring(4, 2);
                        String LetoMax = (sql.GetField_Int(ref dbReader, "MaxObdobjeRealizacije").ToString()).Substring(0, 4);
                        String MesecMax = (sql.GetField_Int(ref dbReader, "MaxObdobjeRealizacije").ToString()).Substring(4, 2);
                        Int32 Mesecev;
                        if (LetoMin == LetoMax)
                        {
                            Mesecev = Convert.ToInt32(MesecMax) - Convert.ToInt32(MesecMin);
                        }
                        else
                        {
                            Mesecev = 12 + Convert.ToInt32(MesecMax) - Convert.ToInt32(MesecMin);
                        }
                        Mesecev = Mesecev + 1; // realizacija 201212 - 201212 = 12 mesecev
                        _SumKolicinePoObdobjuRealizacije.SMM = sql.GetField_IntNull(ref dbReader, "id_merilnega_mesta");
                        _SumKolicinePoObdobjuRealizacije.LetoMesec = "";
                        _SumKolicinePoObdobjuRealizacije.ObdobjeRealizacije = 0;
                        _SumKolicinePoObdobjuRealizacije.Kolicina_Moc = Convert.ToInt32(((sql.GetField_Double(ref dbReader, "S_MOC") / Mesecev) * 12));
                        _SumKolicinePoObdobjuRealizacije.Kolicina_DVT = Convert.ToInt32(((sql.GetField_Double(ref dbReader, "S_DVT") / Mesecev) * 12));
                        _SumKolicinePoObdobjuRealizacije.Kolicina_DMT = Convert.ToInt32(((sql.GetField_Double(ref dbReader, "S_DMT") / Mesecev) * 12));
                        _SumKolicinePoObdobjuRealizacije.Kolicina_DET = Convert.ToInt32(((sql.GetField_Double(ref dbReader, "S_DET") / Mesecev) * 12));
                        _SumKolicinePoObdobjuRealizacije.Kolicina_KT = Convert.ToInt32(((sql.GetField_Double(ref dbReader, "S_DKT") / Mesecev) * 12));
                        _SumKolicinePoObdobjuRealizacije.Kolicina_Skupaj_D = Convert.ToInt32(((sql.GetField_Double(ref dbReader, "S_ENERGIJA_D") / Mesecev) * 12));
                    }
                    else
                    {
                        _SumKolicinePoObdobjuRealizacije.SMM = sql.GetField_IntNull(ref dbReader, "id_merilnega_mesta");
                        _SumKolicinePoObdobjuRealizacije.LetoMesec = "";
                        _SumKolicinePoObdobjuRealizacije.ObdobjeRealizacije = 0;
                        _SumKolicinePoObdobjuRealizacije.Kolicina_Moc = Convert.ToInt32(sql.GetField_Double(ref dbReader, "S_MOC"));
                        _SumKolicinePoObdobjuRealizacije.Kolicina_DVT = Convert.ToInt32(sql.GetField_Double(ref dbReader, "S_DVT"));
                        _SumKolicinePoObdobjuRealizacije.Kolicina_DMT = Convert.ToInt32(sql.GetField_Double(ref dbReader, "S_DMT"));
                        _SumKolicinePoObdobjuRealizacije.Kolicina_DET = Convert.ToInt32(sql.GetField_Double(ref dbReader, "S_DET"));
                        _SumKolicinePoObdobjuRealizacije.Kolicina_KT = Convert.ToInt32(sql.GetField_Double(ref dbReader, "S_DKT"));
                        _SumKolicinePoObdobjuRealizacije.Kolicina_Skupaj_D = Convert.ToInt32(sql.GetField_Double(ref dbReader, "S_ENERGIJA_D"));
                    }
                }
                else
                {
                    return cFunctionResult.Set(true, (int)efrErrorCodes.NoDataFound, "Za zahtevane omejitve ni rezultatov", "", "");
                }
            }
            finally
            {
                if (dbReader != null) { dbReader.Close(); }
                sql.Conn_Close();
            }
            return fr;
        }
        /// <summary>
        /// Dobimo partnerja za merilno mesto
        /// </summary>
        /// <param name="_MM_SMM">Številka merilnega mesta</param>
        /// <param name="_PartnerType">vrsta partnerja</param>
        /// <param name="_PartnerInfo">out struktura podatkov o partnerju dcPartnerInfo</param>
        /// <returns></returns>
        public sFunctionResult Get_PartnerForMMAndPartnerType(Int32 _MM_SMM, ePartnerType _PartnerType, out dcPartnerInfo _PartnerInfo)
        {
            sFunctionResult fr = cFunctionResult.Init();
            cSQL sql = new cSQL(SqlConnStr);
            SqlDataReader dbReader;
            Int32 PartnerID = -1;

            _PartnerInfo = Init_dcPartnerInfo();
            sql.Text = sql.Text + "select PG.kupec LastnikID, PG.placnik PlacnikID, PG.naslovnik NaslovnikID," + cCommon.CR();
            sql.Text = sql.Text + "       eOM.dobavitelj Dobavitelj" + cCommon.CR();
            sql.Text = sql.Text + "from " + LocalTables.Get_3Tav_bis_odjemna_mesta() + " sOM inner join " + LocalTables.Get_3Tav_bie_odjemna_mesta() + " eOM on (sOM.id_odjemnega_mesta=eOM.id_odjemnega_mesta)" + cCommon.CR();
            sql.Text = sql.Text + "                                                         inner join " + LocalTables.Get_3Tav_bis_pogodbe_gl() + " PG on (sOM.pogodba=PG.stevilka_pogodbe)" + cCommon.CR();
            sql.Text = sql.Text + "where sOM.id_odjemnega_mesta = @odjemno_mesto" + cCommon.CR();

            sql.AddSQLParameter("@odjemno_mesto", SqlDbType.Int, ParameterDirection.Input, _MM_SMM);
            dbReader = sql.Query_Read("cEGP_BIS.Get_ZadnjiOdcitek", ref fr);
            if (!fr.resBool)
            {
                return fr;
            }
            try
            {
                if ((dbReader != null) && (dbReader.Read()))
                {
                    switch (_PartnerType)
                    {
                        case ePartnerType.Lastnik:
                            PartnerID = sql.GetField_Int(ref dbReader, "LastnikID");
                            break;
                        case ePartnerType.Placnik:
                            PartnerID = sql.GetField_Int(ref dbReader, "PlacnikID");
                            break;
                        case ePartnerType.Naslovnik:
                            PartnerID = sql.GetField_Int(ref dbReader, "NaslovnikID");
                            if (PartnerID == 0)
                            {
                                PartnerID = sql.GetField_Int(ref dbReader, "PlacnikID");
                            }
                            break;
                    }
                    fr = Get_PartnerForID(PartnerID, "", out _PartnerInfo);
                }
                else
                {
                    return cFunctionResult.Set(true, (int)efrErrorCodes.NoDataFound, "Za zahtevane omejitve ni rezultatov", "", "");
                }
            }
            finally
            {
                if (dbReader != null) { dbReader.Close(); }
                sql.Conn_Close();
            }
            return fr;
        }
        /// <summary>
        /// Dobimo vse partnerje za merilno mesto
        /// </summary>
        /// <param name="_MM_SMM">Številka merilnega mesta</param>
        /// <param name="_PartnerType">vrsta partnerja</param>
        /// <param name="_LastnikInfo">out struktura podatkov o lastniku dcPartnerInfo</param>
        /// <param name="_PlacnikInfo">out struktura podatkov o plačniku dcPartnerInfo</param>
        /// <param name="_NaslovnikInfo">out struktura podatkov o naslovniku dcPartnerInfo</param>     
        /// <returns></returns>
        public sFunctionResult Get_AllPartnersForMM(Int32 _MM_SMM, out dcPartnerInfo _LastnikInfo, out dcPartnerInfo _PlacnikInfo, out dcPartnerInfo _NaslovnikInfo)
        {
            sFunctionResult fr = cFunctionResult.Init();
            cSQL sql = new cSQL(SqlConnStr);
            SqlDataReader dbReader;
            _LastnikInfo = Init_dcPartnerInfo();
            _PlacnikInfo = Init_dcPartnerInfo();
            _NaslovnikInfo = Init_dcPartnerInfo();
            sql.Text = sql.Text + "select PG.kupec LastnikID, PG.placnik PlacnikID, PG.naslovnik NaslovnikID," + cCommon.CR();
            sql.Text = sql.Text + "       eOM.dobavitelj Dobavitelj" + cCommon.CR();
            sql.Text = sql.Text + "from " + LocalTables.Get_3Tav_bis_odjemna_mesta() + " sOM inner join " + LocalTables.Get_3Tav_bie_odjemna_mesta() + " eOM on (sOM.id_odjemnega_mesta=eOM.id_odjemnega_mesta)" + cCommon.CR();
            sql.Text = sql.Text + "                                                         inner join " + LocalTables.Get_3Tav_bis_pogodbe_gl() + " PG on (sOM.pogodba=PG.stevilka_pogodbe)" + cCommon.CR();
            sql.Text = sql.Text + "where sOM.id_odjemnega_mesta = @odjemno_mesto" + cCommon.CR();

            sql.AddSQLParameter("@odjemno_mesto", SqlDbType.Int, ParameterDirection.Input, _MM_SMM);
            dbReader = sql.Query_Read("cEGP_BIS.Get_ZadnjiOdcitek", ref fr);
            if (!fr.resBool)
            {
                return fr;
            }
            try
            {
                if ((dbReader != null) && (dbReader.Read()))
                {
                    // Lastnik
                    fr = Get_PartnerForID(sql.GetField_Int(ref dbReader, "LastnikID"), "", out _LastnikInfo);
                    if (!fr.resBool)
                    {
                        return fr;
                    }
                    // Plačnik
                    if ((sql.GetField_Int(ref dbReader, "PlacnikID") > 0) && (sql.GetField_Int(ref dbReader, "LastnikID") != sql.GetField_Int(ref dbReader, "PlacnikID")))
                    {
                        fr = Get_PartnerForID(sql.GetField_Int(ref dbReader, "PlacnikID"), "", out _LastnikInfo);
                        if (!fr.resBool)
                        {
                            return fr;
                        }
                    }
                    else
                    {
                        _PlacnikInfo = _LastnikInfo;
                    }
                    // Naslovnik
                    if ((sql.GetField_Int(ref dbReader, "NaslovnikID") > 0) && (sql.GetField_Int(ref dbReader, "PlacnikID") != sql.GetField_Int(ref dbReader, "NaslovnikID")))
                    {
                        fr = Get_PartnerForID(sql.GetField_Int(ref dbReader, "NaslovnikID"), "", out _LastnikInfo);
                        if (!fr.resBool)
                        {
                            return fr;
                        }
                    }
                    else
                    {
                        _NaslovnikInfo = _PlacnikInfo;
                    }
                }
                else
                {
                    return cFunctionResult.Set(true, (int)efrErrorCodes.NoDataFound, "Za zahtevane omejitve ni rezultatov", "", "");
                }
            }
            finally
            {
                if (dbReader != null) { dbReader.Close(); }
                sql.Conn_Close();
            }
            return fr;
        }

        public sFunctionResult Check_IsPartnerValidForMP(Int32 _MM_SMM, String SifraPartnerja, ePartnerType _PartnerType, out Boolean _IsPartnerValid)
        {
            sFunctionResult fr;
            dcPartnerInfo PartnerInfo;
            fr = this.Get_PartnerForMMAndPartnerType(_MM_SMM, _PartnerType, out PartnerInfo);
            if (SifraPartnerja == PartnerInfo.ZunanjiPartnerID)
            {
                _IsPartnerValid = true;
            }
            else
            {
                _IsPartnerValid = false;
            }
            return fr;
        }

        public sFunctionResult Get_AliLahkoVpisemoOdcitkeZaMerilnoMesto(Int32 _SMM, DateTime? _Datum, out Boolean _AliLahkoVpisemoOdcitke, out String _OpombaVpisa)
        {
            dcMerilnoMesto MM_Data;
            sFunctionResult fr;

            _AliLahkoVpisemoOdcitke = false;
            _OpombaVpisa = "Nedoločena opomba";
            fr = this.Get_MM(_SMM, out MM_Data);
            if (!fr.resBool)
            {
                return fr;
            }
            if (MM_Data.Status == 1)
            {
                _AliLahkoVpisemoOdcitke = true;
                _OpombaVpisa = "Vpis stanja doboljen.";
            }
            else
            {
                _AliLahkoVpisemoOdcitke = false;
                _OpombaVpisa = "Merilno mesto je v statusu, ki ne dovoljuje vpisa stanja.";
            }
            return fr;
        }

        public sFunctionResult Get_PartnerForID(Int32? _InternalPartnerID, String _ExternalPartnerID, out dcPartnerInfo _PartnerInfo)
        {
            sFunctionResult fr = cFunctionResult.Init();
            cSQL sql = new cSQL(SqlConnStr);
            SqlDataReader dbReader;

            _PartnerInfo = Init_dcPartnerInfo();
            sql.Text = sql.Text + "select PP.POSLOVNI_PARTNER PARTNER_UID, PP.DODATNA_SIFRA_3 ExternalPartnerUID,PP.NAZIV_PP PARTNER_NAZIV," + cCommon.CR();
            sql.Text = sql.Text + "       rtrim(PP.NASLOV_PP) Ulica, rtrim(PP.KRAJ_PP) Kraj, rtrim(PP.his_stevilka) HisnaStevilka, rtrim(PP.dodatek) HisnaStevilkaDodatek, PP.STANOVANJE Stanovanje," + cCommon.CR();
            sql.Text = sql.Text + "       PP.POSTNA_STEVILKA PostaStevilka, PO.kraj PostaNaziv, PP.davcna_stevilka davcna_stevilka" + cCommon.CR();
            sql.Text = sql.Text + "from " + LocalTables.Get_3Tav_Poslovni_partnerji() + " PP left join " + LocalTables.Get_3Tav_Posta() + " po on (PP.postna_stevilka=po.postna_stevilka)" + cCommon.CR();
            if (_InternalPartnerID != null)
                sql.Text = sql.Text + "where PP.POSLOVNI_PARTNER = @POSLOVNI_PARTNER" + cCommon.CR();
            else
                sql.Text = sql.Text + "where PP.DODATNA_SIFRA_3 = @DODATNA_SIFRA_3" + cCommon.CR();

            if (_InternalPartnerID != null)
                sql.AddSQLParameter("@POSLOVNI_PARTNER", SqlDbType.Int, ParameterDirection.Input, (int)_InternalPartnerID);
            else
                sql.AddSQLParameter("@DODATNA_SIFRA_3", SqlDbType.VarChar, ParameterDirection.Input, _ExternalPartnerID);

            dbReader = sql.Query_Read("cEGP_BIS.Get_PartnerForID", ref fr);
            if (!fr.resBool)
            {
                return fr;
            }
            try
            {
                if ((dbReader != null) && (dbReader.Read()))
                {
                    _PartnerInfo.PartnerUID = sql.GetField_Int(ref dbReader, "PARTNER_UID");
                    _PartnerInfo.ZunanjiPartnerID = sql.GetField_String(ref dbReader, "ExternalPartnerUID");
                    if ((_PartnerInfo.ZunanjiPartnerID == null) || (_PartnerInfo.ZunanjiPartnerID == ""))
                    {
                        _PartnerInfo.ZunanjiPartnerID = _PartnerInfo.PartnerUID.ToString();
                    }
                    _PartnerInfo.Naziv = sql.GetField_String(ref dbReader, "PARTNER_NAZIV").Trim();
                    _PartnerInfo.Ulica = sql.GetField_String(ref dbReader, "Ulica").Trim();
                    _PartnerInfo.Kraj = sql.GetField_String(ref dbReader, "Kraj").Trim();
                    _PartnerInfo.HisnaStevilka = sql.GetField_String(ref dbReader, "HisnaStevilka").Trim();
                    _PartnerInfo.HisnaStevilkaDodatek = sql.GetField_String(ref dbReader, "HisnaStevilkaDodatek").Trim();
                    _PartnerInfo.Stanovanje = sql.GetField_String(ref dbReader, "Stanovanje").Trim();
                    _PartnerInfo.Naslov = (_PartnerInfo.Ulica + " " + _PartnerInfo.HisnaStevilka + " " + _PartnerInfo.HisnaStevilkaDodatek).Trim();
                    _PartnerInfo.PostaStevilka = sql.GetField_String(ref dbReader, "PostaStevilka").Trim();
                    _PartnerInfo.PostaNaziv = sql.GetField_String(ref dbReader, "PostaNaziv").Trim();
                    _PartnerInfo.Posta = _PartnerInfo.PostaStevilka + " " + _PartnerInfo.PostaNaziv;
                    _PartnerInfo.DavcnaStevilka = sql.GetField_String(ref dbReader, "davcna_stevilka").Trim();
                }
                else
                {
                    return cFunctionResult.Set(true, (int)efrErrorCodes.NoDataFound, "Za zahtevane omejitve ni rezultatov", "", "");
                }
            }
            finally
            {
                if (dbReader != null) { dbReader.Close(); }
                sql.Conn_Close();
            }
            return fr;
        }

        public sFunctionResult Get_SaldoKontnaKarticaZaPartnerja(Int32 _PlacnikID, Int32? _MerilnoMesto, DateTime? _OdDatumaDalje, out List<dcSaldoKontnaKartica_Postavka> _SaldoKontnaKartica)
        {
            sFunctionResult fr = cFunctionResult.Init();
            cSQL sql = new cSQL(SqlConnStr);
            SqlDataReader dbReader;
            dcSaldoKontnaKartica_Postavka sk_Postavka;

            _SaldoKontnaKartica = new List<dcSaldoKontnaKartica_Postavka>(0);
            sql.Text = sql.Text + "SELECT status, DATUM_DOKUMENTA DatumFakture, VEZA_ZUNANJA StevilkaFakture, OBDOBJE_KNJIZENJA Obdobje, VRSTA_TEMELJNICE, KONTO Konto, breme Breme, dobro Dobro, saldo Saldo, DATUM_ZAPADLOSTI DatumZapadlosti, OPIS_KNJIZBE OpisKnjizbe, dim_1 MerilnoMesto" + cCommon.CR();
            sql.Text = sql.Text + "from bis_saldakontna_kartica" + cCommon.CR();
            sql.Text = sql.Text + "where poslovni_partner = @poslovni_partner" + cCommon.CR();
            if (_MerilnoMesto != null)
                sql.Text = sql.Text + "  and dim_1 = @MerilnoMesto" + cCommon.CR();
            if (_OdDatumaDalje != null)
                sql.Text = sql.Text + "  and DATUM_DOKUMENTA >= @DATUM_DOKUMENTA" + cCommon.CR();
            sql.Text = sql.Text + "ORDER BY poslovni_partner, dim_1,vrstni_red,DATUM_DOKUMENTA,ID_KNJIZBE" + cCommon.CR();

            sql.AddSQLParameter("@poslovni_partner", SqlDbType.Int, ParameterDirection.Input, _PlacnikID);
            if (_MerilnoMesto != null)
                sql.AddSQLParameter("@MerilnoMesto", SqlDbType.Int, ParameterDirection.Input, _MerilnoMesto);
            if (_OdDatumaDalje != null)
                sql.AddSQLParameter("@DATUM_DOKUMENTA", SqlDbType.Date, ParameterDirection.Input, (DateTime)_OdDatumaDalje);
            dbReader = sql.Query_Read("cEGP_BIS.Get_SaldoKontnaKarticaZaPartnerja", ref fr);
            if (!fr.resBool)
            {
                return fr;
            }
            try
            {
                if (dbReader != null)
                {
                    while (dbReader.Read())
                    {
                        sk_Postavka = Init_dcSaldoKontnaKartica_Postavka();
                        sk_Postavka.MerilnoMesto = sql.GetField_Int(ref dbReader, "MerilnoMesto");
                        sk_Postavka.DatumFakture = sql.GetField_DateTime(ref dbReader, "DatumFakture");
                        sk_Postavka.StevilkaFakture = sql.GetField_String(ref dbReader, "StevilkaFakture");
                        sk_Postavka.Obdobje = sql.GetField_String(ref dbReader, "Obdobje");
                        sk_Postavka.VrstaTemeljnice = sql.GetField_String(ref dbReader, "VRSTA_TEMELJNICE");
                        sk_Postavka.Konto = sql.GetField_String(ref dbReader, "Konto");
                        sk_Postavka.Breme = sql.GetField_DoubleNull(ref dbReader, "Breme");
                        sk_Postavka.Dobro = sql.GetField_DoubleNull(ref dbReader, "Dobro");
                        sk_Postavka.Saldo = sql.GetField_DoubleNull(ref dbReader, "Saldo");
                        sk_Postavka.DatumZapadlosti = sql.GetField_DateTimeNull(ref dbReader, "DatumZapadlosti");
                        sk_Postavka.OpisKnjizbe = sql.GetField_String(ref dbReader, "OpisKnjizbe");
                        _SaldoKontnaKartica.Add(sk_Postavka);
                    }
                }
                else
                {
                    return cFunctionResult.Set(false, (int)efrErrorCodes.NoDataFound, "Za zahtevane omejitve ni rezultatov", "", "");
                }
            }
            finally
            {
                if (dbReader != null) { dbReader.Close(); }
                sql.Conn_Close();
            }
            return fr;
        }

        public sFunctionResult Get_IzdaneFaktureZaPartnerja(Int32 _PlacnikID, Int32? _MerilnoMesto, DateTime? _OdDatumaDalje, out List<dcIzdanaFakturaGlava> _IzdaneFaktureGlava)
        {
            sFunctionResult fr = cFunctionResult.Init();
            cSQL sql = new cSQL(SqlConnStr);
            SqlDataReader dbReader;
            dcIzdanaFakturaGlava IzdanaFakturaGlava;

            _IzdaneFaktureGlava = new List<dcIzdanaFakturaGlava>(0);
            sql.Text = sql.Text + "select distinct g.STEVILKA_FAKTURE StevilkaFakture, g.cfVeznaOznaka OznakaFakture, g.sklicna_stevilka StevilkaSklica, g.DATUM_FAKTURE DatumFakture, g.VALUTA_FAKTURE ValutaFakture, g.RACUN_KUPCU PlacnikID, g.naslovnik NaslovnikID, g.namen NamenPlacila, p.odjemno_mesto MerilnoMesto" + cCommon.CR();
            sql.Text = sql.Text + "from dbo.bis_fakture_gl_v g inner join bis_fakture_pp p on (g.STEVILKA_FAKTURE=p.STEVILKA_FAKTURE)" + cCommon.CR();
            sql.Text = sql.Text + "where g.RACUN_KUPCU = @poslovni_partner" + cCommon.CR();
            if (_MerilnoMesto != null)
                sql.Text = sql.Text + "and p.odjemno_mesto = @MerilnoMesto" + cCommon.CR();
            if (_OdDatumaDalje != null)
                sql.Text = sql.Text + "  and g.DATUM_FAKTURE >= @DATUM_DOKUMENTA" + cCommon.CR();
            sql.Text = sql.Text + "order by g.DATUM_FAKTURE, g.STEVILKA_FAKTURE" + cCommon.CR();

            sql.Text = sql.Text + "order by VrstaNaprave, TipID" + cCommon.CR();

            sql.AddSQLParameter("@poslovni_partner", SqlDbType.Int, ParameterDirection.Input, _PlacnikID);
            if (_MerilnoMesto != null)
                sql.AddSQLParameter("@MerilnoMesto", SqlDbType.Int, ParameterDirection.Input, _MerilnoMesto);
            if (_OdDatumaDalje != null)
                sql.AddSQLParameter("@DATUM_DOKUMENTA", SqlDbType.Date, ParameterDirection.Input, (DateTime)_OdDatumaDalje);

            dbReader = sql.Query_Read("cEGP_BIS.Get_IzdaneFaktureZaPartnerja", ref fr);
            if (!fr.resBool)
            {
                return fr;
            }
            try
            {
                if (dbReader != null)
                {
                    while (dbReader.Read())
                    {
                        IzdanaFakturaGlava = Init_dcIzdanaFakturaGlava();

                        IzdanaFakturaGlava.MerilnoMesto = sql.GetField_Int(ref dbReader, "MerilnoMesto");
                        IzdanaFakturaGlava.StevilkaFakture = sql.GetField_Int(ref dbReader, "StevilkaFakture");
                        IzdanaFakturaGlava.OznakaFakture = sql.GetField_String(ref dbReader, "OznakaFakture");
                        IzdanaFakturaGlava.StevilkaSklica = sql.GetField_String(ref dbReader, "StevilkaSklica");
                        IzdanaFakturaGlava.DatumFakture = sql.GetField_DateTime(ref dbReader, "DatumFakture");
                        IzdanaFakturaGlava.ValutaFakture = sql.GetField_DateTime(ref dbReader, "ValutaFakture");
                        IzdanaFakturaGlava.PlacnikID = sql.GetField_Int(ref dbReader, "PlacnikID");
                        IzdanaFakturaGlava.NaslovnikID = sql.GetField_Int(ref dbReader, "NaslovnikID");
                        IzdanaFakturaGlava.NamenPlacila = sql.GetField_String(ref dbReader, "NamenPlacila");
                        _IzdaneFaktureGlava.Add(IzdanaFakturaGlava);
                    }
                }
                else
                {
                    return cFunctionResult.Set(false, (int)efrErrorCodes.NoDataFound, "Za zahtevane omejitve ni rezultatov", "", "");
                }
            }
            finally
            {
                if (dbReader != null) { dbReader.Close(); }
                sql.Conn_Close();
            }
            return fr;
        }

        public sFunctionResult Get_PostavkeFakture(Int32 _FakturaID, out List<dcIzdanaFakturaPostavka> _IzdanaFakturaPostavke)
        {
            sFunctionResult fr = cFunctionResult.Init();
            cSQL sql = new cSQL(SqlConnStr);
            SqlDataReader dbReader;
            dcIzdanaFakturaPostavka IzdanaFakturaPostavka;

            _IzdanaFakturaPostavke = new List<dcIzdanaFakturaPostavka>(0);
            sql.Text = sql.Text + "select p.sifra_artikla SifraArtikla, p.KOLICINA Kolicina, p.ENOTA_MERE EnotaMere, p.DATUM_CENIKA DatumCenika, p.CENA CenaNaEnoto, p.DAVCNA_STOPNJA DavcnaStopnja, NETO ZnesekNeto, DAVEK ZnesekDavek, BRUTO ZnesekBruto, cf_neto ZnesekNetoPN, cf_davek ZnesekDavekPN, cf_bruto ZnesekBrutoPN, sifra NazivArtikla" + cCommon.CR();
            sql.Text = sql.Text + "from dbo.bis_fakture_pp p inner join dbo.artikli a on (p.sifra_artikla = a.preva_id)" + cCommon.CR();
            sql.Text = sql.Text + "where STEVILKA_FAKTURE = @STEVILKA_FAKTURE" + cCommon.CR();
            sql.Text = sql.Text + "order by p.zaporedje" + cCommon.CR();

            sql.AddSQLParameter("@STEVILKA_FAKTURE", SqlDbType.Int, ParameterDirection.Input, _FakturaID);
            dbReader = sql.Query_Read("cEGP_BIS.Get_PostavkeFakture", ref fr);
            if (!fr.resBool)
            {
                return fr;
            }
            try
            {
                if (dbReader != null)
                {
                    while (dbReader.Read())
                    {
                        IzdanaFakturaPostavka = Init_dcIzdanaFakturaPostavke();

                        IzdanaFakturaPostavka.SifraArtikla = sql.GetField_Int(ref dbReader, "SifraArtikla");
                        IzdanaFakturaPostavka.Kolicina = sql.GetField_Double(ref dbReader, "Kolicina");
                        IzdanaFakturaPostavka.EnotaMere = sql.GetField_String(ref dbReader, "EnotaMere");
                        IzdanaFakturaPostavka.DatumCenika = sql.GetField_DateTime(ref dbReader, "DatumCenika");
                        IzdanaFakturaPostavka.CenaNaEnoto = sql.GetField_Double(ref dbReader, "CenaNaEnoto");
                        IzdanaFakturaPostavka.DavcnaStopnja = sql.GetField_Double(ref dbReader, "DavcnaStopnja");
                        IzdanaFakturaPostavka.ZnesekNeto = sql.GetField_Double(ref dbReader, "ZnesekNeto");
                        IzdanaFakturaPostavka.ZnesekDavek = sql.GetField_Double(ref dbReader, "ZnesekDavek");
                        IzdanaFakturaPostavka.ZnesekBruto = sql.GetField_Double(ref dbReader, "ZnesekBruto");
                        IzdanaFakturaPostavka.ZnesekNetoPN = sql.GetField_Double(ref dbReader, "ZnesekNetoPN");
                        IzdanaFakturaPostavka.ZnesekDavekPN = sql.GetField_Double(ref dbReader, "ZnesekDavekPN");
                        IzdanaFakturaPostavka.ZnesekBrutoPN = sql.GetField_Double(ref dbReader, "ZnesekBrutoPN");
                        IzdanaFakturaPostavka.NazivArtikla = sql.GetField_String(ref dbReader, "NazivArtikla");
                        _IzdanaFakturaPostavke.Add(IzdanaFakturaPostavka);
                    }
                }
                else
                {
                    return cFunctionResult.Set(false, (int)efrErrorCodes.NoDataFound, "Za zahtevane omejitve ni rezultatov", "", "");
                }
            }
            finally
            {
                if (dbReader != null) { dbReader.Close(); }
                sql.Conn_Close();
            }
            return fr;
        }

        public sFunctionResult Get_PaketZaOdjemnoMesto(Int32? _MerilnoMesto, DateTime _Datum, out Int32? _PaketID)
        {
            sFunctionResult fr = cFunctionResult.Init();
            cSQL sql = new cSQL(SqlConnStr);
            SqlDataReader dbReader;

            _PaketID = null;

            sql.Text = sql.Text + "select dbo.f_mm_akcija(@MerilnoMesto, @Datum) PaketID" + cCommon.CR();

            sql.AddSQLParameter("@MerilnoMesto", SqlDbType.Int, ParameterDirection.Input, _MerilnoMesto);
            sql.AddSQLParameter("@Datum", SqlDbType.Date, ParameterDirection.Input, _Datum);
            dbReader = sql.Query_Read("cEGP_BIS.Get_PartnerForID", ref fr);
            if (!fr.resBool)
            {
                return fr;
            }
            try
            {
                if ((dbReader != null) && (dbReader.Read()))
                {
                    _PaketID = sql.GetField_IntNull(ref dbReader, "PaketID");
                }
                else
                {
                    return cFunctionResult.Set(true, (int)efrErrorCodes.NoDataFound, "Za zahtevane omejitve ni rezultatov", "", "");
                }
            }
            finally
            {
                if (dbReader != null) { dbReader.Close(); }
                sql.Conn_Close();
            }
            return fr;
        }

        public sFunctionResult Get_CenoZaPaket(Int32? _MerilnoMesto, DateTime _Datum, Int32 _PaketID, eTarifaZaCenik _Tarifa, out Double? _CenaZaEnoto)
        {
            sFunctionResult fr = cFunctionResult.Init();
            cSQL sql = new cSQL(SqlConnStr);
            SqlDataReader dbReader;

            _CenaZaEnoto = null;

            sql.Text = sql.Text + "select dbo.f_mm_artikel_Cena(@MerilnoMesto, @Datum, @PaketID, @Tarifa) CenaZaEnoto" + cCommon.CR();
            sql.AddSQLParameter("@MerilnoMesto", SqlDbType.Int, ParameterDirection.Input, DBNull.Value /* _MerilnoMesto*/);
            sql.AddSQLParameter("@Datum", SqlDbType.Date, ParameterDirection.Input, _Datum);
            sql.AddSQLParameter("@PaketID", SqlDbType.Int, ParameterDirection.Input, _PaketID);
            sql.AddSQLParameter("@Tarifa", SqlDbType.Int, ParameterDirection.Input, (Int32)_Tarifa);
            dbReader = sql.Query_Read("cEGP_BIS.Get_PartnerForID", ref fr);
            if (!fr.resBool)
            {
                return fr;
            }
            try
            {
                if ((dbReader != null) && (dbReader.Read()))
                {
                    _CenaZaEnoto = sql.GetField_DoubleNull(ref dbReader, "CenaZaEnoto");
                }
                else
                {
                    return cFunctionResult.Set(true, (int)efrErrorCodes.NoDataFound, "Za zahtevane omejitve ni rezultatov", "", "");
                }
            }
            finally
            {
                if (dbReader != null) { dbReader.Close(); }
                sql.Conn_Close();
            }
            return fr;
        }

        public sFunctionResult Get_CenoZaP67(out Double? _CenaZaEnoto)
        {
            sFunctionResult fr = cFunctionResult.Init();
            cSQL sql = new cSQL(SqlConnStr);
            SqlDataReader dbReader;

            _CenaZaEnoto = null;

            sql.Text = sql.Text + "select dbo.f_preva_id_cena(12,'P',getdate(),0) CenaZaEnoto" + cCommon.CR();
            dbReader = sql.Query_ReadNoParameters("cEGP_BIS.Get_CenoZaP67", ref fr);
            if (!fr.resBool)
            {
                return fr;
            }
            try
            {
                if ((dbReader != null) && (dbReader.Read()))
                {
                    _CenaZaEnoto = sql.GetField_DoubleNull(ref dbReader, "CenaZaEnoto");
                }
                else
                {
                    return cFunctionResult.Set(true, (int)efrErrorCodes.NoDataFound, "Za zahtevane omejitve ni rezultatov", "", "");
                }
            }
            finally
            {
                if (dbReader != null) { dbReader.Close(); }
                sql.Conn_Close();
            }
            return fr;
        }

        public sFunctionResult Get_CenoZaTrosarina(out Double? _CenaZaEnoto)
        {
            sFunctionResult fr = cFunctionResult.Init();
            cSQL sql = new cSQL(SqlConnStr);
            SqlDataReader dbReader;

            _CenaZaEnoto = null;

            sql.Text = sql.Text + "select dbo.f_preva_id_cena(990,'P',getdate(),0) CenaZaEnoto" + cCommon.CR();
            dbReader = sql.Query_ReadNoParameters("cEGP_BIS.Get_CenoZaTrosarina", ref fr);
            if (!fr.resBool)
            {
                return fr;
            }
            try
            {
                if ((dbReader != null) && (dbReader.Read()))
                {
                    _CenaZaEnoto = sql.GetField_DoubleNull(ref dbReader, "CenaZaEnoto");
                }
                else
                {
                    return cFunctionResult.Set(true, (int)efrErrorCodes.NoDataFound, "Za zahtevane omejitve ni rezultatov", "", "");
                }
            }
            finally
            {
                if (dbReader != null) { dbReader.Close(); }
                sql.Conn_Close();
            }
            return fr;
        }

        public sFunctionResult Get_CenoZaMesecnoNadomestilo(Boolean _Trajnik, out Double? _CenaZaEnoto)
        {
            sFunctionResult fr = cFunctionResult.Init();
            cSQL sql = new cSQL(SqlConnStr);
            SqlDataReader dbReader;

            _CenaZaEnoto = null;

            if (!_Trajnik)
                sql.Text = sql.Text + "select dbo.f_preva_id_cena(4075,'P',getdate(),0) CenaZaEnoto" + cCommon.CR();
            else
                sql.Text = sql.Text + "select dbo.f_preva_id_cena(4076,'P',getdate(),0) CenaZaEnoto" + cCommon.CR();
            dbReader = sql.Query_ReadNoParameters("cEGP_BIS.Get_CenoZaMesecnoNadomestilo", ref fr);
            if (!fr.resBool)
            {
                return fr;
            }
            try
            {
                if ((dbReader != null) && (dbReader.Read()))
                {
                    _CenaZaEnoto = sql.GetField_DoubleNull(ref dbReader, "CenaZaEnoto");
                }
                else
                {
                    return cFunctionResult.Set(true, (int)efrErrorCodes.NoDataFound, "Za zahtevane omejitve ni rezultatov", "", "");
                }
            }
            finally
            {
                if (dbReader != null) { dbReader.Close(); }
                sql.Conn_Close();
            }
            return fr;
        }

        public dcMerilnoMesto Init_dcMerilnoMesto()
        {
            dcMerilnoMesto MerilnoMesto = new dcMerilnoMesto();
            MerilnoMesto.SMM = null;
            MerilnoMesto.SODO = null;
            MerilnoMesto.SODONaziv = "";
            MerilnoMesto.SODO_SMM = null;
            MerilnoMesto.TipOdjemnegaMesta = null;
            MerilnoMesto.Status = null;
            MerilnoMesto.Naziv = "";
            MerilnoMesto.Naslov = "";
            MerilnoMesto.Posta = "";
            MerilnoMesto.StevilkaStanovanja = "";
            MerilnoMesto.NacinObracuna = null;
            MerilnoMesto.NacinObracunaNaziv = "";
            MerilnoMesto.ObracunSteviloTarif = "";
            MerilnoMesto.ObracunSteviloTarifNaziv = "";
            MerilnoMesto.ObracunskeVarovalke = "";
            MerilnoMesto.TarifnaSkupina = null;
            MerilnoMesto.TarifnaSkupinaNaziv = "";
            MerilnoMesto.OdjemnaSkupina = null;
            MerilnoMesto.OdjemnaSkupinaNaziv = "";
            MerilnoMesto.DalinjskoOdcitavanje = null;
            MerilnoMesto.LocenRacun = null;
            MerilnoMesto.StevilkaSoglasja = null;
            MerilnoMesto.DatumZadnjegaObracuna = null;
            MerilnoMesto.DatumZadnjePrijave = null;
            MerilnoMesto.NacinPlacila = "";
            MerilnoMesto.NeAkontacijski = null;
            return MerilnoMesto;
        }

        public dcNamescenaNaprava Init_dcNamescenaNaprava()
        {
            dcNamescenaNaprava NamescenaNaprava = new dcNamescenaNaprava();
            NamescenaNaprava.SMM = null;
            NamescenaNaprava.VrstaNaprave = "";
            NamescenaNaprava.TipID = null;
            NamescenaNaprava.TipNaziv = "";
            NamescenaNaprava.TovSt = "";
            NamescenaNaprava.LetoIzdelave = null;
            NamescenaNaprava.LetoZiga = null;
            NamescenaNaprava.DatumNamestitve = null;
            NamescenaNaprava.SteviloMestOdbirka = null;
            NamescenaNaprava.SteviloDecimalnihMest = null;
            NamescenaNaprava.VnosnaPoljaStanj = "";
            return NamescenaNaprava;
        }

        public dcOdcitek Init_dcOdcitek()
        {
            dcOdcitek Odcitek = new dcOdcitek();
            Odcitek.SMM = null;
            Odcitek.DatumStanja = null;
            Odcitek.VrstaOdcitka = null;
            Odcitek.NacinPridobitveOdcitka = null;
            Odcitek.TipID_D = null;
            Odcitek.TovSt_D = "";
            Odcitek.VrednostET = null;
            Odcitek.SlikaET = "";
            Odcitek.PorabaET = null;
            Odcitek.VrednostDVT = null;
            Odcitek.SlikaDVT = "";
            Odcitek.PorabaDVT = null;
            Odcitek.VrednostDMT = null;
            Odcitek.SlikaDMT = "";
            Odcitek.PorabaDMT = null;
            Odcitek.VrednostKT = null;
            Odcitek.SlikaKT = "";
            Odcitek.PorabaKT = null;
            Odcitek.Moc = null;
            Odcitek.SlikaMoc = "";
            Odcitek.ObracunID = null;
            Odcitek.OdcitekObracunan = false;
            return Odcitek;
        }

        public dcPovprecnaMesecnaPoraba Init_dcPovprecnaPoraba()
        {
            dcPovprecnaMesecnaPoraba PovprecnaPoraba = new dcPovprecnaMesecnaPoraba();
            PovprecnaPoraba.SMM = null;
            PovprecnaPoraba.PovprecjeET = null;
            PovprecnaPoraba.PovprecjeDVT = null;
            PovprecnaPoraba.PovprecjeDMT = null;
            return PovprecnaPoraba;
        }

        public dcPartnerInfo Init_dcPartnerInfo()
        {
            dcPartnerInfo Partner = new dcPartnerInfo();
            Partner.PartnerUID = null;
            Partner.ZunanjiPartnerID = "";
            Partner.Naziv = "";
            Partner.Ulica = "";
            Partner.Kraj = "";
            Partner.HisnaStevilka = "";
            Partner.HisnaStevilkaDodatek = "";
            Partner.Stanovanje = "";
            Partner.Naslov = "";
            Partner.PostaStevilka = "";
            Partner.PostaNaziv = "";
            Partner.Posta = "";
            Partner.DavcnaStevilka = "";
            return Partner;
        }

        public dcKolicinePoObdobjuRealizacije Init_dcKolicinePoObdobjuRealizacije()
        {
            dcKolicinePoObdobjuRealizacije KolicinePoObdobjuRealizacije = new dcKolicinePoObdobjuRealizacije();
            KolicinePoObdobjuRealizacije.SMM = null;
            KolicinePoObdobjuRealizacije.LetoMesec = "";
            KolicinePoObdobjuRealizacije.ObdobjeRealizacije = null;
            KolicinePoObdobjuRealizacije.Kolicina_Moc = 0;
            KolicinePoObdobjuRealizacije.Kolicina_DVT = 0;
            KolicinePoObdobjuRealizacije.Kolicina_DMT = 0;
            KolicinePoObdobjuRealizacije.Kolicina_DET = 0;
            KolicinePoObdobjuRealizacije.Kolicina_Skupaj_D = 0;
            return KolicinePoObdobjuRealizacije;
        }

        public dcSaldoKontnaKartica_Postavka Init_dcSaldoKontnaKartica_Postavka()
        {
            dcSaldoKontnaKartica_Postavka sk_Postavka = new dcSaldoKontnaKartica_Postavka();
            sk_Postavka.MerilnoMesto = null;
            sk_Postavka.DatumFakture = null;
            sk_Postavka.StevilkaFakture = "";
            sk_Postavka.Obdobje = "";
            sk_Postavka.Konto = "";
            sk_Postavka.Breme = null;
            sk_Postavka.Dobro = null;
            sk_Postavka.Saldo = null;
            sk_Postavka.DatumZapadlosti = null;
            sk_Postavka.OpisKnjizbe = "";
            return sk_Postavka;
        }

        public dcIzdanaFakturaGlava Init_dcIzdanaFakturaGlava()
        {
            dcIzdanaFakturaGlava IzdanaFakturaGlava = new dcIzdanaFakturaGlava();
            IzdanaFakturaGlava.MerilnoMesto = null;
            IzdanaFakturaGlava.StevilkaFakture = null;
            IzdanaFakturaGlava.OznakaFakture = null;
            IzdanaFakturaGlava.StevilkaSklica = "";
            IzdanaFakturaGlava.DatumFakture = null;
            IzdanaFakturaGlava.ValutaFakture = null;
            IzdanaFakturaGlava.PlacnikID = null;
            IzdanaFakturaGlava.NaslovnikID = null;
            IzdanaFakturaGlava.NamenPlacila = "";
            return IzdanaFakturaGlava;
        }

        public dcIzdanaFakturaPostavka Init_dcIzdanaFakturaPostavke()
        {
            dcIzdanaFakturaPostavka IzdanaFakturaPostavka = new dcIzdanaFakturaPostavka();
            IzdanaFakturaPostavka.SifraArtikla = null;
            IzdanaFakturaPostavka.Kolicina = null;
            IzdanaFakturaPostavka.EnotaMere = "";
            IzdanaFakturaPostavka.DatumCenika = null;
            IzdanaFakturaPostavka.CenaNaEnoto = null;
            IzdanaFakturaPostavka.DavcnaStopnja = null;
            IzdanaFakturaPostavka.ZnesekNeto = null;
            IzdanaFakturaPostavka.ZnesekDavek = null;
            IzdanaFakturaPostavka.ZnesekBruto = null;
            IzdanaFakturaPostavka.ZnesekNetoPN = null;
            IzdanaFakturaPostavka.ZnesekDavekPN = null;
            IzdanaFakturaPostavka.ZnesekBrutoPN = null;
            IzdanaFakturaPostavka.NazivArtikla = "";
            return IzdanaFakturaPostavka;
        }

    }

    public class cbie_sp
    {
        private String SqlConnStr;

        /// <summary>
        /// Construktor
        /// </summary>
        /// <param name="_dbData">Podatki o podatkovni bazi</param>
        public cbie_sp(String _SqlConnStr)
        {
            SqlConnStr = _SqlConnStr;
        }

        public Decimal? odcitekET;
        public Decimal? odcitekDVT;
        public Decimal? odcitekDMT;
        public String opomba;
        public Int32? uporabnik;
        public Int32? NacinPridobitveOdcitka;

        public sFunctionResult Set_VpisOdcitka_OdcitkiPerun(Int32 _odjemno_mesto, Int32 _SMM_OM, DateTime _DatumOdcitka, Decimal? odcitekET, Decimal? odcitekDVT, Decimal? odcitekDMT, String _KomentarVlagatelja, Int32? uporabnik, Int32? NacinPridobitveOdcitka)
        {
            sFunctionResult fr = cFunctionResult.Init();

            bie_vpisi_odcitek_OdcitkiPerun bie_vpisi_odcitek = new bie_vpisi_odcitek_OdcitkiPerun(this.SqlConnStr);
            bie_vpisi_odcitek_OdcitkiPerun.sInParameters InParameters = new bie_vpisi_odcitek_OdcitkiPerun.sInParameters();
            bie_vpisi_odcitek_OdcitkiPerun.sOutParameters OutParameters = new bie_vpisi_odcitek_OdcitkiPerun.sOutParameters();

            InParameters.odjemno_mesto = _odjemno_mesto;
            InParameters.SMM_OM = _SMM_OM;
            InParameters.DatumOdcitka = _DatumOdcitka;
            InParameters.odcitekET = odcitekET;
            InParameters.odcitekDVT = odcitekDVT;
            InParameters.odcitekDMT = odcitekDMT;
            InParameters.opomba = _KomentarVlagatelja;
            InParameters.uporabnik = uporabnik;
            InParameters.NacinPridobitveOdcitka = NacinPridobitveOdcitka;
            InParameters.Vir = string.Empty;
            InParameters.StatusCakalnica = 0;

            fr = bie_vpisi_odcitek.Execute(InParameters, out OutParameters);

            if (!fr.resBool)
            {
                return fr;
            }

            fr = cFunctionResult.Set(true, (int)OutParameters.RTC, OutParameters.IME_KONTROLE, OutParameters.MSG, "");
            return fr;
        }

        public sFunctionResult Set_VpisOdcitka_OdcitkiPerun(Int32 _odjemno_mesto, Int32 _SMM_OM, DateTime _DatumOdcitka, Decimal? odcitekET, Decimal? odcitekDVT, Decimal? odcitekDMT, String _KomentarVlagatelja, Int32? uporabnik, Int32? nacinPridobitveOdcitka, String vir, StatusiCakalnica statusCakalnica)
        {
            sFunctionResult fr = cFunctionResult.Init();

            bie_vpisi_odcitek_OdcitkiPerun bie_vpisi_odcitek = new bie_vpisi_odcitek_OdcitkiPerun(this.SqlConnStr);
            bie_vpisi_odcitek_OdcitkiPerun.sInParameters InParameters = new bie_vpisi_odcitek_OdcitkiPerun.sInParameters();
            bie_vpisi_odcitek_OdcitkiPerun.sOutParameters OutParameters = new bie_vpisi_odcitek_OdcitkiPerun.sOutParameters();

            InParameters.odjemno_mesto = _odjemno_mesto;
            InParameters.SMM_OM = _SMM_OM;
            InParameters.DatumOdcitka = _DatumOdcitka;
            InParameters.odcitekET = odcitekET;
            InParameters.odcitekDVT = odcitekDVT;
            InParameters.odcitekDMT = odcitekDMT;
            InParameters.opomba = _KomentarVlagatelja;
            InParameters.uporabnik = uporabnik;
            InParameters.NacinPridobitveOdcitka = nacinPridobitveOdcitka;
            InParameters.Vir = vir;
            InParameters.StatusCakalnica = (int)statusCakalnica;

            fr = bie_vpisi_odcitek.Execute(InParameters, out OutParameters);

            if (!fr.resBool)
            {
                return fr;
            }

            fr = cFunctionResult.Set(true, (int)OutParameters.RTC, OutParameters.IME_KONTROLE, OutParameters.MSG, "");
            return fr;
        }

        // **********************************
        // ********* deklaracije sp *********
        // **********************************
        public class bie_vpisi_odcitek_OdcitkiPerun
        {
            public struct sInParameters
            {
                public Int32? odjemno_mesto;
                public Int32? SMM_OM;
                public DateTime? DatumOdcitka;
                public Decimal? odcitekET;
                public Decimal? odcitekDVT;
                public Decimal? odcitekDMT;
                public String opomba;
                public Int32? uporabnik;
                public Int32? NacinPridobitveOdcitka;
                public String Vir;
                public Int32? StatusCakalnica;
            }

            public struct sOutParameters
            {
                public Int32? RTC;
                public String IME_KONTROLE;
                public String MSG;
            }

            private String SqlConnStr;

            /// <summary>
            /// Construktor
            /// </summary>
            /// <param name="_dbData">Podatki o podatkovni bazi</param>
            public bie_vpisi_odcitek_OdcitkiPerun(String _SqlConnStr)
            {
                SqlConnStr = _SqlConnStr;
            }

            public sFunctionResult Execute(sInParameters _InParameters, out sOutParameters _OutParameters)
            {
                sFunctionResult res = cFunctionResult.Init();
                SqlCommand SP = new SqlCommand();

                SP.Connection = new SqlConnection(cSettings.Get_3Tav_ConnString());
                SP.CommandType = CommandType.StoredProcedure;
                SP.CommandText = "bie_vpisi_odcitek_OdcitkiPerun";
                _OutParameters.RTC = (int)efrErrorCodes.UnknownError;
                _OutParameters.IME_KONTROLE = "Neznana napaka";
                _OutParameters.MSG = "Neznana napaka";

                SP.Connection.Open();
                try
                {
                    SqlCommandBuilder.DeriveParameters(SP);
                }
                catch (Exception e)
                {
                    res = cFunctionResult.Set(false, 1, "Napaka: bie_vpisi_odcitek_OdcitkiPerun.Execute - DeriveParameters", e.Message.ToString(), "");
                    return res;
                }

                SP.Parameters["@odjemno_mesto"].Value = _InParameters.odjemno_mesto;
                SP.Parameters["@SMM_OM"].Value = _InParameters.SMM_OM;
                SP.Parameters["@DatumOdcitka"].Value = _InParameters.DatumOdcitka;
                SP.Parameters["@opomba"].Value = _InParameters.opomba;
                SP.Parameters["@uporabnik"].Value = _InParameters.uporabnik;
                SP.Parameters["@NacinPridobitveOdcitka"].Value = _InParameters.NacinPridobitveOdcitka;
                if (_InParameters.odcitekET != null)
                {
                    SP.Parameters["@odcitekET"].Value = _InParameters.odcitekET;
                }
                else
                {
                    SP.Parameters["@odcitekET"].Value = DBNull.Value;
                }
                if (_InParameters.odcitekDVT != null)
                {
                    SP.Parameters["@odcitekDVT"].Value = _InParameters.odcitekDVT;
                }
                else
                {
                    SP.Parameters["@odcitekDVT"].Value = DBNull.Value;
                }
                if (_InParameters.odcitekDMT != null)
                {
                    SP.Parameters["@odcitekDMT"].Value = _InParameters.odcitekDMT;
                }
                else
                {
                    SP.Parameters["@odcitekDMT"].Value = DBNull.Value;
                }

                if (_InParameters.Vir != null)
                {
                    SP.Parameters["@vir"].Value = _InParameters.Vir;
                }
                else
                {
                    SP.Parameters["@vir"].Value = DBNull.Value;
                }

                if (_InParameters.StatusCakalnica != null)
                {
                    SP.Parameters["@job_status"].Value = _InParameters.StatusCakalnica;
                }
                else
                {
                    SP.Parameters["@job_status"].Value = DBNull.Value;
                }

                try
                {
                    SP.ExecuteNonQuery();
                    if (cCommon.IsNumber(SP.Parameters["@RTC"].Value.ToString()))
                    {
                        _OutParameters.RTC = Convert.ToInt32(SP.Parameters["@RTC"].Value.ToString());
                    }
                    else
                    {
                        _OutParameters.RTC = null;
                    }
                    _OutParameters.IME_KONTROLE = SP.Parameters["@IME_KONTROLE"].Value.ToString();
                    _OutParameters.MSG = SP.Parameters["@MSG"].Value.ToString();
                    res = cFunctionResult.Set(true, (int)_OutParameters.RTC, _OutParameters.IME_KONTROLE, _OutParameters.MSG, "");
                }
                catch (Exception e)
                {
                    SP.Dispose();
                    res = cFunctionResult.Set(false, 1, "Napaka: bie_vpisi_odcitek_OdcitkiPerun.Execute ", e.Message.ToString(), "");
                    return res;
                }
                SP.Connection.Close();
                SP.Dispose();
                return res;
            }
        }
    }
}