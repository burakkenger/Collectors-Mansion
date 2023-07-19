# Collectors-Mansion
 <h4>Social media and market platform for collectors</h4>
 <h4>Koleksiyoncular için sosyal medya ve satış platformu</h4>
 <h4>Canlı proje linki : https://collectorsmansion.azurewebsites.net</h4>
 <h4>Demo Hesap : Kullanıcı Adı: demo, Şifre: demo248</h4>
 <p>Projeye ekleme yapmaya devam ettiğimden dosyalarda bazı özellikler eksik gözükebilir</p>
 <p>Kullanıcılar arasında özel mesaj henüz eklenmedi</p>
 <br>
 <h1>Veri Sözlüğü</h1>
 <div style="display: grid; justify-content: start;">
   <img src="Collector/wwwroot/documentation/Veri sözlük part1.png" alt="">
   <img src="Collector/wwwroot/documentation/Veri sözlüğü part 2.png" alt="">
 </div>
 
 <br>
 
 <h1>Use Case Diyagramı</h1>
 <img src="Collector/wwwroot/documentation/Use Case.png" alt="">
 <br>

 <h1>İş Akış Diyagramları</h1>
 <h3>Kullanıcı iş akış diyagramı</h3>
 <img src="Collector/wwwroot/documentation/İş akış diyagramları png/Kayıt olma.png" alt="">
 <br>
 <h3>Oturum açma diyagramı</h3>
 <img src="Collector/wwwroot/documentation/İş akış diyagramları png/Oturum açma.drawio.png" alt="">
 <br>
 <h3>Profil oluşturma diyagramı</h3>
 <img src="Collector/wwwroot/documentation/İş akış diyagramları png/profil oluşturma.drawio.png" alt="">
 <br>
 <h3>Ürün ekleme diyagramı</h3>
 <img src="Collector/wwwroot/documentation/İş akış diyagramları png/Ürün Ekleme.png" alt="">
 <br>
 <h3>Ürün satış diyagramı</h3>
 <img src="Collector/wwwroot/documentation/İş akış diyagramları png/Ürün satış.png" alt="">
 <br>
 <h3>Mesajlaşma ve iletişim</h3>
 <img src="Collector/wwwroot/documentation/İş akış diyagramları png/Mesaj yollama.png" alt="">
 <br>

 <h1>Varlık İlişki (ER) Diyagramı</h1>
 <img src="Collector/wwwroot/documentation/ER Diyagramı.png" alt="">
 <br>

 <h1>Veri Akış Diyagramı</h1>
 <img src="Collector/wwwroot/documentation/veri akış diyagram.png" alt="">
 <br>

 <h1>Girdi İşlem Ve Çıktıların Tasarlanması (IPO/VTOC)</h1>
 <h3>IPO</h3>
 <img src="Collector/wwwroot/documentation/Genel IPO.png" alt="">
 <h3>VTOC</h3>
 <img src="Collector/wwwroot/documentation/VTOC Diyagramı.drawio.png" alt="">
 <h4>VTOC İçerik</h4>
 <p>
 1.0 Kullanıcı : Kullanıcı işlemlerinin yapıldığı kısım. <br>
 1.1 Ürün : Ürün işlemlerinin yapıldığı kısım. <br>
 1.1.1 Ekle : Ürün ekle işleminin gerçekleştiği kısım <br>
 1.1.2 Arşivle : Ürünü arşive kaldırarak liste dışı kalmasını sağlayan kısım. <br>
 <br>
 1.2 Profil Bilgisi : Kullanıcı profil düzenlemelerini yapıldığı kısım <br>
 1.2.1 Ekle : Adres, hakkında ve gibi profil bilgilerinin girildiği kısım <br>
 1.2.2 Güncelle : Profil bilgilerinin güncellenebildiği kısım <br>
 <br>
 1.3 Koleksiyon : Koleksiyon işlemlerinin yapıldığı kısım <br>
 1.3.1 Oluştur : Ürün seçilerek koleksiyonların oluşturulduğu kısım <br>
 1.3.2 Güncelle : Koleksiyon içeriğinin güncellendiği kısım <br>
 1.3.3 Sil : Koleksiyonların silindiği kısım <br>
 <br>
 1.4 Yorumlar : Yorum işlemlerinin yapıldığı kısım <br>
 1.4.1 Ekle : Kullanıcıların ürünlere yorum eklediği kısım <br>
 <br>
 2.0 Yönetici : Yönetici işlemlerinin yapıldığı kısım <br>
 2.1 Kategori : Yöneticinin kategori işlemlerini yaptığı kısım <br>
 2.1.1 Ekle : Kategori ekleme işleminin yapıldığı kısım <br>
 2.1.2 Güncelle : Kategori güncelleme işleminin yapıldığı kısım <br>
 2.1.3 Pasif Hale Getir : Kategorileri liste dışı bırakan kısım <br>
 <br>
 2.2 Durum : Yöneticinin durum işlemlerini yaptığı kısım <br>
 2.2.1 Ekle : Durum ekleme işleminin yapıldığı kısım <br>
 2.2.2 Güncelle : Durum güncelleme işleminin yapıldığı kısım <br>
 2.2.3 Pasif Hale Getir : Durumları liste dışı bırakan kısım <br>
 <br>
 2.3 Yorumlar : Yöneticinin yorum işlemlerini yaptığı kısım <br>
 2.3.1 : Yorum ekleme işleminin yapıldığı kısım <br>
 2.3.2 : Yorum güncelleme işleminin yapıldığı kısım <br>
 2.3.3 : Yorum silme işleminin yapıldığı kısım <br>
 <br>
 2.4 Ürün : Yöneticinin ürün işlemlerini yaptığı kısım <br>
 2.4.1 Ekle : Ürün ekleme işleminin yapıldığı kısım <br>
 2.4.2 Yasakla : Yöneticinin ürünleri yasaklayarak listeden kaldırdığı kısım <br>
</p>
<br>
<h1>Veri Tabanı Tablolarının Tasarımı</h1>
<img src="Collector/wwwroot/documentation/veritabanı diyagramı.png" alt="">


 
 
 
  
  
 

 

 
