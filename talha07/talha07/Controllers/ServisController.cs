using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using talha07.Models;
using talha07.ViewModel;

namespace talha07.Controllers
{
    public class ServisController : ApiController
    {

            DB07Entities db = new DB07Entities();
            SonucModel sonuc = new SonucModel();

            #region Odev
            [HttpGet]
            [Route("api/odevliste")]

            public List<OdevModel> OdevListe()
            {
                List<OdevModel> liste = db.Odev.Select(x => new OdevModel()

                {
                    odevId = x.odevId,
                    odevKodu = x.odevKodu,
                    odevAdi = x.odevAdi,
                    odevKredi = x.odevKredi,

                }).ToList();

                return liste;
            }

            [HttpGet]
            [Route("api/odevbyid/{odevId}")]

            public OdevModel OdevById(string odevId)
            {
                OdevModel kayit = db.Odev.Where(s => s.odevId == odevId).Select(x => new OdevModel()

                {

                    odevId = x.odevId,
                    odevKodu = x.odevKodu,
                    odevAdi = x.odevAdi,
                    odevKredi = x.odevKredi,

                }).SingleOrDefault();

                return kayit;
        }

            [HttpPost]
            [Route("api/odevekle")]

            public SonucModel OdevEkle(OdevModel model)
            {
                if (db.Odev.Count(s => s.odevKodu == model.odevKodu) > 0)
                {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen Ödev Kodu Kayıtlıdır!";

                return sonuc;
            }
            Odev yeni = new Odev();
            yeni.odevId = Guid.NewGuid().ToString();
            yeni.odevKodu = model.odevKodu;
            yeni.odevAdi = model.odevAdi;
            yeni.odevKredi = model.odevKredi;
            db.Odev.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Ödev Eklendi";
            return sonuc;
        }

            [HttpPut]
            [Route("api/odevduzenle")]

            public SonucModel OdevDuzenle(OdevModel model)
            {
                Odev kayit = db.Odev.Where(s => s.odevId == model.odevId).SingleOrDefault();

                if (kayit == null)
                {
                    sonuc.islem = false;
                    sonuc.mesaj = "Kayıt Bulunamadı!";
                    return sonuc;
                }
                kayit.odevKodu = model.odevKodu;
                kayit.odevAdi = model.odevAdi;
                kayit.odevKredi = model.odevKredi;
                db.SaveChanges();
                sonuc.islem = true;
                sonuc.mesaj = "Ödev Düzenlendi";
                return sonuc;
        }

        [HttpDelete]
        [Route("api/odevsil/{odevId}")]

        public SonucModel OdevSil(string odevId)
        {
            Odev kayit = db.Odev.Where(s => s.odevId == odevId).SingleOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı!";
                return sonuc;
            }
            if (db.Kayit.Count(s => s.kayitOdevId == odevId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Ödevi Olan Öğrenci Olduğu İçin Silinemez";
                return sonuc;
            }
            db.Odev.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Ödev Silindi";
            return sonuc;
        }
        #endregion

        #region Ogrenci

        [HttpGet]
        [Route("api/ogrenciliste")]

        public List<OgrenciModel> OgrenciListe()
        {
            List<OgrenciModel> liste = db.Ogrenci.Select(x => new OgrenciModel()
            {
                ogrId = x.ogrId,
                ogrNo = x.ogrNo,
                ogrAdsoyad = x.ogrAdsoyad,
                ogrDogTarih = x.ogrDogTarih,

            }).ToList();

            return liste;
        }

        [HttpGet]
        [Route("api/ogrencibyid/{ogrId}")]

        public OgrenciModel OgrenciById(string ogrId)
        {
            OgrenciModel kayit = db.Ogrenci.Where(s => s.ogrId == ogrId).Select(x => new OgrenciModel()
            {
                ogrId = x.ogrId,
                ogrNo = x.ogrNo,
                ogrAdsoyad = x.ogrAdsoyad,
                ogrDogTarih = x.ogrDogTarih,

            }).SingleOrDefault();

            return kayit;
        }

        [HttpPost]
        [Route("api/ogrenciekle")]

        public SonucModel OgrenciEkle(OgrenciModel model)
        {
            if (db.Ogrenci.Count(s => s.ogrNo == model.ogrNo) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen Öğrenci Numarası Kayıtlıdır!";
                return sonuc;
            }
            Ogrenci yeni = new Ogrenci();
            yeni.ogrId = Guid.NewGuid().ToString();
            yeni.ogrNo = model.ogrNo;
            yeni.ogrAdsoyad = model.ogrAdsoyad;
            yeni.ogrDogTarih = model.ogrDogTarih;

            db.Ogrenci.Add(yeni);
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Öğrenci Eklendi";
            
            return sonuc;
        }

        [HttpPut]
        [Route("api/ogrenciduzenle")]

        public SonucModel OgrenciDuzenle(OgrenciModel model)
        {
            Ogrenci kayit = db.Ogrenci.Where(s => s.ogrId == model.ogrId).SingleOrDefault();

            if (kayit == null)

            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı!";

                return sonuc;
            }

            kayit.ogrNo = model.ogrNo;
            kayit.ogrAdsoyad = model.ogrAdsoyad;
            kayit.ogrDogTarih = model.ogrDogTarih;

            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Öğrenci Düzenlendi";

            return sonuc;
        }

        [HttpDelete]
        [Route("api/ogrencisil/{ogrId}")]

        public SonucModel OgrenciSil(string ogrId)

        {
            Ogrenci kayit = db.Ogrenci.Where(s => s.ogrId == ogrId).SingleOrDefault();

            if (kayit == null)

            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı!";

                return sonuc;
            }

            if (db.Kayit.Count(s => s.kayitOgrId == ogrId) > 0)

            {
                sonuc.islem = false;
                sonuc.mesaj = "Öğrencinin Tamamlaması Gereken Ödevi Olduğu İçin Silinemez!";

                return sonuc;
            }

            db.Ogrenci.Remove(kayit);
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Öğrenci Silindi";

            return sonuc;
        }
        #endregion

        #region Kayit

        [HttpGet]
        [Route("api/ogrenciodevliste/{ogrId}")]
        public List<KayitModel> OgrenciOdevListe(string ogrId)
        {
            List<KayitModel> liste = db.Kayit.Where(s => s.kayitOgrId == ogrId).Select(x => new KayitModel()
            {
                kayitId = x.kayitId,
                kayitOdevId = x.kayitOdevId,
                kayitOgrId = x.kayitOgrId,
            }).ToList();
            foreach (var kayit in liste)
            {
                kayit.ogrBilgi = OgrenciById(kayit.kayitOgrId);
                kayit.odevBilgi = OdevById(kayit.kayitOdevId);
            }

            return liste;
        }

        [HttpGet]
        [Route("api/odevogrenciliste/{odevId}")]
        public List<KayitModel> OdevOgrenciListe(string odevId)
        {
            List<KayitModel> liste = db.Kayit.Where(s => s.kayitOdevId == odevId).Select(x => new KayitModel()
            {
                kayitId = x.kayitId,
                kayitOdevId = x.kayitOdevId,
                kayitOgrId = x.kayitOgrId,
            }).ToList();
            foreach (var kayit in liste)
            {
                kayit.ogrBilgi = OgrenciById(kayit.kayitOgrId);
                kayit.odevBilgi = OdevById(kayit.kayitOdevId);
            }

            return liste;
        }

        [HttpPost]
        [Route("api/kayitekle")]
        public SonucModel KayitEkle(KayitModel model)
        {
            if (db.Kayit.Count(s => s.kayitOdevId == model.kayitOdevId && s.kayitOgrId == model.kayitOgrId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "İlgili Öğrenci Ödevden Önceden Kayıtlıdır!";

                return sonuc;
            }

            Kayit yeni = new Kayit();

            yeni.kayitId = Guid.NewGuid().ToString();
            yeni.kayitOgrId = model.kayitOgrId;
            db.Kayit.Add(yeni);
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Öde Kaydı Eklendi";

            return sonuc;
        }

        [HttpDelete]
        [Route("api/kayitsil/{kayitId}")]
        public SonucModel KayitSil(string kayitId)
        {
            Kayit kayit = db.Kayit.Where(s => s.kayitId == kayitId).SingleOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı!";

                return sonuc;
            }

            db.Kayit.Remove(kayit);
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Ödev Kaydı Silindi";

            return sonuc;
        }

        #endregion
    }
}
