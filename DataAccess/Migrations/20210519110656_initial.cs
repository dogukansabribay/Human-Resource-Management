using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Logins",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mail = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logins", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ResmiTatiller",
                columns: table => new
                {
                    ResmiTatilId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    Aciklama = table.Column<string>(nullable: true),
                    BaslangicTarihi = table.Column<DateTime>(nullable: false),
                    BitisTarihi = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResmiTatiller", x => x.ResmiTatilId);
                });

            migrationBuilder.CreateTable(
                name: "Sirketler",
                columns: table => new
                {
                    SirketId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    Adi = table.Column<string>(nullable: true),
                    Logo = table.Column<string>(nullable: true),
                    SirketUyelikPlaniId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sirketler", x => x.SirketId);
                });

            migrationBuilder.CreateTable(
                name: "TarihSecimleri",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    BaslangicTarihi = table.Column<DateTime>(nullable: false),
                    BitisTarihi = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TarihSecimleri", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UyelikPlanilari",
                columns: table => new
                {
                    UyelikPlaniID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    UyelikPlaniTanimi = table.Column<string>(nullable: true),
                    CalisanSayisi = table.Column<int>(nullable: false),
                    PlanUcreti = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UyelikPlanilari", x => x.UyelikPlaniID);
                });

            migrationBuilder.CreateTable(
                name: "Calisanlar",
                columns: table => new
                {
                    CalisanId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    Adi = table.Column<string>(maxLength: 60, nullable: false),
                    Soyadi = table.Column<string>(maxLength: 60, nullable: false),
                    DogumTarihi = table.Column<DateTime>(nullable: true),
                    TcNo = table.Column<string>(maxLength: 11, nullable: true),
                    ErisimTuru = table.Column<int>(nullable: false),
                    SozlesmeTuru = table.Column<int>(nullable: false),
                    EngelDerecesi = table.Column<int>(nullable: true),
                    Uyruk = table.Column<string>(nullable: true),
                    Cinsiyet = table.Column<int>(nullable: true),
                    KanGrubu = table.Column<int>(nullable: true),
                    EgitimDurumu = table.Column<int>(nullable: true),
                    EgitimSeviyesi = table.Column<int>(nullable: true),
                    SonTamamlananEgitimKurumu = table.Column<string>(nullable: true),
                    CocukSayisi = table.Column<int>(nullable: false),
                    KullandigiYıllıkIzinSayisi = table.Column<int>(nullable: true),
                    KalanYıllıkIzinSayisi = table.Column<int>(nullable: true),
                    ToplamKullanilanIzinSayisi = table.Column<int>(nullable: true),
                    MailIs = table.Column<string>(maxLength: 150, nullable: false),
                    MailKisisel = table.Column<string>(maxLength: 150, nullable: false),
                    TelIs = table.Column<string>(nullable: true),
                    TelKisisel = table.Column<string>(nullable: true),
                    Fotograf = table.Column<string>(nullable: true),
                    AktifMi = table.Column<bool>(nullable: false),
                    IseGirisTarihi = table.Column<DateTime>(nullable: false),
                    SozlesmeBitisTarihi = table.Column<DateTime>(nullable: true),
                    MedeniDurum = table.Column<int>(nullable: true),
                    SonGirişTarihi = table.Column<DateTime>(nullable: true),
                    SirketId = table.Column<int>(nullable: true),
                    AcilDurumBilgiID = table.Column<int>(nullable: true),
                    AdresId = table.Column<int>(nullable: true),
                    BaglantiSosyalMedyaId = table.Column<int>(nullable: true),
                    BankaId = table.Column<int>(nullable: true),
                    CalisanAvansID = table.Column<int>(nullable: true),
                    KullaniciYorumuId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calisanlar", x => x.CalisanId);
                    table.ForeignKey(
                        name: "FK_Calisanlar_Sirketler_SirketId",
                        column: x => x.SirketId,
                        principalTable: "Sirketler",
                        principalColumn: "SirketId");
                });

            migrationBuilder.CreateTable(
                name: "SirketUyelikPlanlari",
                columns: table => new
                {
                    SirketUyelikPlaniID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    UyelikPlaniID = table.Column<int>(nullable: false),
                    SirketID = table.Column<int>(nullable: false),
                    BaslangicTarihi = table.Column<DateTime>(nullable: false),
                    BitisTarihi = table.Column<DateTime>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SirketUyelikPlanlari", x => x.SirketUyelikPlaniID);
                    table.ForeignKey(
                        name: "FK_SirketUyelikPlanlari_Sirketler_SirketID",
                        column: x => x.SirketID,
                        principalTable: "Sirketler",
                        principalColumn: "SirketId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SirketUyelikPlanlari_UyelikPlanilari_UyelikPlaniID",
                        column: x => x.UyelikPlaniID,
                        principalTable: "UyelikPlanilari",
                        principalColumn: "UyelikPlaniID");
                });

            migrationBuilder.CreateTable(
                name: "AcilDurumBilgileri",
                columns: table => new
                {
                    AcilDurumBilgiId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    AranacakKisiAd = table.Column<string>(maxLength: 60, nullable: true),
                    AranacakKisiSoyad = table.Column<string>(maxLength: 60, nullable: true),
                    TelefonNo = table.Column<string>(maxLength: 13, nullable: true),
                    YakinlikDerecesi = table.Column<string>(maxLength: 60, nullable: true),
                    CalisanId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcilDurumBilgileri", x => x.AcilDurumBilgiId);
                    table.ForeignKey(
                        name: "FK_AcilDurumBilgileri_Calisanlar_CalisanId",
                        column: x => x.CalisanId,
                        principalTable: "Calisanlar",
                        principalColumn: "CalisanId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Adresler",
                columns: table => new
                {
                    AdresId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CalisanID = table.Column<int>(nullable: false),
                    AdresAdi = table.Column<string>(nullable: true),
                    AdresDevam = table.Column<string>(nullable: true),
                    EvTelefonu = table.Column<string>(nullable: true),
                    Ulke = table.Column<string>(nullable: true),
                    Sehir = table.Column<string>(nullable: true),
                    PostaKodu = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adresler", x => x.AdresId);
                    table.ForeignKey(
                        name: "FK_Adresler_Calisanlar_CalisanID",
                        column: x => x.CalisanID,
                        principalTable: "Calisanlar",
                        principalColumn: "CalisanId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BaglantiSosyalMedyalar",
                columns: table => new
                {
                    BaglantiSosyalMedyaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    HesapTuru = table.Column<string>(maxLength: 150, nullable: true),
                    BaglantiAdresi = table.Column<string>(maxLength: 150, nullable: true),
                    CalisanID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaglantiSosyalMedyalar", x => x.BaglantiSosyalMedyaID);
                    table.ForeignKey(
                        name: "FK_BaglantiSosyalMedyalar_Calisanlar_CalisanID",
                        column: x => x.CalisanID,
                        principalTable: "Calisanlar",
                        principalColumn: "CalisanId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bankalar",
                columns: table => new
                {
                    BankaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CalisanId = table.Column<int>(nullable: false),
                    BankaAdi = table.Column<string>(maxLength: 150, nullable: true),
                    HesapTipi = table.Column<int>(nullable: false),
                    HesapNo = table.Column<string>(maxLength: 25, nullable: true),
                    IBAN = table.Column<string>(maxLength: 25, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bankalar", x => x.BankaId);
                    table.ForeignKey(
                        name: "FK_Bankalar_Calisanlar_CalisanId",
                        column: x => x.CalisanId,
                        principalTable: "Calisanlar",
                        principalColumn: "CalisanId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bildirimler",
                columns: table => new
                {
                    BildirimId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    Baslik = table.Column<string>(nullable: true),
                    Icerik = table.Column<string>(nullable: true),
                    CalisanId = table.Column<int>(nullable: false),
                    OkunduMu = table.Column<bool>(nullable: false),
                    OkunmaTarihi = table.Column<DateTime>(nullable: true),
                    SablonTanimi = table.Column<int>(nullable: true),
                    GonderilecekMi = table.Column<bool>(nullable: false),
                    GonderilmeTarihi = table.Column<DateTime>(nullable: false),
                    SablonVsBildirim = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bildirimler", x => x.BildirimId);
                    table.ForeignKey(
                        name: "FK_Bildirimler_Calisanlar_CalisanId",
                        column: x => x.CalisanId,
                        principalTable: "Calisanlar",
                        principalColumn: "CalisanId");
                });

            migrationBuilder.CreateTable(
                name: "CalisanAvanslar",
                columns: table => new
                {
                    CalisanAvansID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    TaksitSayısı = table.Column<int>(nullable: true),
                    AvansMiktarı = table.Column<decimal>(type: "decimal", nullable: false),
                    Acıklama = table.Column<string>(nullable: true),
                    AvansTarihi = table.Column<DateTime>(nullable: false),
                    OnayDurumu = table.Column<int>(nullable: false),
                    AvansTalepEdilenTarih = table.Column<DateTime>(nullable: false),
                    AvansOnaylandıgıTarih = table.Column<DateTime>(nullable: false),
                    AvansVerildigiTarih = table.Column<DateTime>(nullable: false),
                    RedAciklama = table.Column<string>(nullable: true),
                    CalisanID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalisanAvanslar", x => x.CalisanAvansID);
                    table.ForeignKey(
                        name: "FK_CalisanAvanslar_Calisanlar_CalisanID",
                        column: x => x.CalisanID,
                        principalTable: "Calisanlar",
                        principalColumn: "CalisanId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CalisanEgitim",
                columns: table => new
                {
                    CalisanId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CalisanId1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalisanEgitim", x => x.CalisanId);
                    table.ForeignKey(
                        name: "FK_CalisanEgitim_Calisanlar_CalisanId1",
                        column: x => x.CalisanId1,
                        principalTable: "Calisanlar",
                        principalColumn: "CalisanId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CalisanHarcamalari",
                columns: table => new
                {
                    CalisanHarcamaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    HarcamaAciklamasi = table.Column<string>(nullable: true),
                    RedAcıklaması = table.Column<string>(nullable: true),
                    HarcamaMiktari = table.Column<double>(nullable: false),
                    HarcamaTarihi = table.Column<DateTime>(nullable: false),
                    HarcamaBelgesiYolu = table.Column<string>(nullable: true),
                    CalisanId = table.Column<int>(nullable: false),
                    OnayDurumu = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalisanHarcamalari", x => x.CalisanHarcamaID);
                    table.ForeignKey(
                        name: "FK_CalisanHarcamalari_Calisanlar_CalisanId",
                        column: x => x.CalisanId,
                        principalTable: "Calisanlar",
                        principalColumn: "CalisanId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CalisanKariyerler",
                columns: table => new
                {
                    CalisanKariyerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    IseGirisTarihi = table.Column<DateTime>(nullable: false),
                    IstenCikisTarihi = table.Column<DateTime>(nullable: false),
                    CalismaSekli = table.Column<string>(maxLength: 100, nullable: true),
                    Sube = table.Column<string>(maxLength: 150, nullable: true),
                    Departman = table.Column<int>(nullable: false),
                    Unvan = table.Column<int>(nullable: false),
                    GorevTanimi = table.Column<string>(nullable: true),
                    YetenekTanimi = table.Column<string>(nullable: true),
                    Maas = table.Column<int>(nullable: false),
                    CalisanId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalisanKariyerler", x => x.CalisanKariyerId);
                    table.ForeignKey(
                        name: "FK_CalisanKariyerler_Calisanlar_CalisanId",
                        column: x => x.CalisanId,
                        principalTable: "Calisanlar",
                        principalColumn: "CalisanId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Izinler",
                columns: table => new
                {
                    IzinID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    IzinTanimi = table.Column<int>(nullable: false),
                    BaslangicTarihi = table.Column<DateTime>(nullable: false),
                    MesaiBaslangicTarihi = table.Column<DateTime>(nullable: false),
                    OnayDurumu = table.Column<int>(nullable: false),
                    IzinDetayTalebi = table.Column<string>(nullable: true),
                    IzinBelgesiYolu = table.Column<string>(nullable: true),
                    RedAcıklaması = table.Column<string>(nullable: true),
                    IzinKullanilanGunSayisi = table.Column<int>(nullable: false),
                    CalisanId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Izinler", x => x.IzinID);
                    table.ForeignKey(
                        name: "FK_Izinler_Calisanlar_CalisanId",
                        column: x => x.CalisanId,
                        principalTable: "Calisanlar",
                        principalColumn: "CalisanId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KullaniciYorumlari",
                columns: table => new
                {
                    KullaniciYorumuId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    YorumDetay = table.Column<string>(nullable: true),
                    YorumTanimi = table.Column<int>(nullable: false),
                    YayinlansinMi = table.Column<bool>(nullable: false),
                    OkunduMu = table.Column<bool>(nullable: false),
                    CalisanId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KullaniciYorumlari", x => x.KullaniciYorumuId);
                    table.ForeignKey(
                        name: "FK_KullaniciYorumlari_Calisanlar_CalisanId",
                        column: x => x.CalisanId,
                        principalTable: "Calisanlar",
                        principalColumn: "CalisanId");
                });

            migrationBuilder.CreateTable(
                name: "Sifreler",
                columns: table => new
                {
                    SifreId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CalisanId = table.Column<int>(nullable: false),
                    Parola = table.Column<string>(nullable: true),
                    OlusturulmaTarihi = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sifreler", x => x.SifreId);
                    table.ForeignKey(
                        name: "FK_Sifreler_Calisanlar_CalisanId",
                        column: x => x.CalisanId,
                        principalTable: "Calisanlar",
                        principalColumn: "CalisanId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EgitimBilgileri",
                columns: table => new
                {
                    EgitimBilgiId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    OkulAdi = table.Column<string>(nullable: true),
                    EgitimSeviyesi = table.Column<int>(nullable: false),
                    MezuniyetTarihi = table.Column<DateTime>(nullable: false),
                    CalisanId = table.Column<int>(nullable: false),
                    CalisanEgitimViewModelCalisanId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EgitimBilgileri", x => x.EgitimBilgiId);
                    table.ForeignKey(
                        name: "FK_EgitimBilgileri_CalisanEgitim_CalisanEgitimViewModelCalisanId",
                        column: x => x.CalisanEgitimViewModelCalisanId,
                        principalTable: "CalisanEgitim",
                        principalColumn: "CalisanId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EgitimBilgileri_Calisanlar_CalisanId",
                        column: x => x.CalisanId,
                        principalTable: "Calisanlar",
                        principalColumn: "CalisanId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AcilDurumBilgileri_CalisanId",
                table: "AcilDurumBilgileri",
                column: "CalisanId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Adresler_CalisanID",
                table: "Adresler",
                column: "CalisanID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BaglantiSosyalMedyalar_CalisanID",
                table: "BaglantiSosyalMedyalar",
                column: "CalisanID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bankalar_CalisanId",
                table: "Bankalar",
                column: "CalisanId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bildirimler_CalisanId",
                table: "Bildirimler",
                column: "CalisanId");

            migrationBuilder.CreateIndex(
                name: "IX_CalisanAvanslar_CalisanID",
                table: "CalisanAvanslar",
                column: "CalisanID");

            migrationBuilder.CreateIndex(
                name: "IX_CalisanEgitim_CalisanId1",
                table: "CalisanEgitim",
                column: "CalisanId1");

            migrationBuilder.CreateIndex(
                name: "IX_CalisanHarcamalari_CalisanId",
                table: "CalisanHarcamalari",
                column: "CalisanId");

            migrationBuilder.CreateIndex(
                name: "IX_CalisanKariyerler_CalisanId",
                table: "CalisanKariyerler",
                column: "CalisanId");

            migrationBuilder.CreateIndex(
                name: "IX_Calisanlar_SirketId",
                table: "Calisanlar",
                column: "SirketId");

            migrationBuilder.CreateIndex(
                name: "IX_EgitimBilgileri_CalisanEgitimViewModelCalisanId",
                table: "EgitimBilgileri",
                column: "CalisanEgitimViewModelCalisanId");

            migrationBuilder.CreateIndex(
                name: "IX_EgitimBilgileri_CalisanId",
                table: "EgitimBilgileri",
                column: "CalisanId");

            migrationBuilder.CreateIndex(
                name: "IX_Izinler_CalisanId",
                table: "Izinler",
                column: "CalisanId");

            migrationBuilder.CreateIndex(
                name: "IX_KullaniciYorumlari_CalisanId",
                table: "KullaniciYorumlari",
                column: "CalisanId");

            migrationBuilder.CreateIndex(
                name: "IX_Sifreler_CalisanId",
                table: "Sifreler",
                column: "CalisanId");

            migrationBuilder.CreateIndex(
                name: "IX_SirketUyelikPlanlari_SirketID",
                table: "SirketUyelikPlanlari",
                column: "SirketID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SirketUyelikPlanlari_UyelikPlaniID",
                table: "SirketUyelikPlanlari",
                column: "UyelikPlaniID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AcilDurumBilgileri");

            migrationBuilder.DropTable(
                name: "Adresler");

            migrationBuilder.DropTable(
                name: "BaglantiSosyalMedyalar");

            migrationBuilder.DropTable(
                name: "Bankalar");

            migrationBuilder.DropTable(
                name: "Bildirimler");

            migrationBuilder.DropTable(
                name: "CalisanAvanslar");

            migrationBuilder.DropTable(
                name: "CalisanHarcamalari");

            migrationBuilder.DropTable(
                name: "CalisanKariyerler");

            migrationBuilder.DropTable(
                name: "EgitimBilgileri");

            migrationBuilder.DropTable(
                name: "Izinler");

            migrationBuilder.DropTable(
                name: "KullaniciYorumlari");

            migrationBuilder.DropTable(
                name: "Logins");

            migrationBuilder.DropTable(
                name: "ResmiTatiller");

            migrationBuilder.DropTable(
                name: "Sifreler");

            migrationBuilder.DropTable(
                name: "SirketUyelikPlanlari");

            migrationBuilder.DropTable(
                name: "TarihSecimleri");

            migrationBuilder.DropTable(
                name: "CalisanEgitim");

            migrationBuilder.DropTable(
                name: "UyelikPlanilari");

            migrationBuilder.DropTable(
                name: "Calisanlar");

            migrationBuilder.DropTable(
                name: "Sirketler");
        }
    }
}
