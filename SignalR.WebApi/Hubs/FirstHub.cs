using Microsoft.AspNetCore.SignalR;
using SignalR.WebApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalR.WebApi.Hubs
{
    public class FirstHub:Hub
    {
        // Client lar FirstHub a bir talep te bulunduğunda her client için ayrı bir nesne oluşturulduğundan bu içeriği görür. Ancak farklı nesnelerde. Mesajlar kolleksiyonu herkeste ayrı nesneler olduğundan bir clint in eklediğine diğerleri erişemez olur. O nedenle burakaki yapılar static olmalıdır. Bu sayede tüm client lar aynı nesne üzerinde çalışır.

        public static List<Mesaj> Mesajlar { get; set; } = new List<Mesaj>() { 
          new Mesaj(){ Icerik="Merhaba SinalR a Hoşgeldiniz" },
          new Mesaj(){ Icerik="Eğitim Başlöıyor..." },
        };

        public static int BagliKisiSayisi { get; set; }

        public async Task MesajGonder(Mesaj mesaj)
        {
            
            Mesajlar.Add(mesaj);
            await Clients.All.SendAsync("MesajAl", mesaj);
        }
        

        public async Task MesajlariGetir()
        {

            //await Clients.Caller.SendAsync("MesajlariAl", Mesajlar);

            //await Clients.Others.SendAsync("MesajlariAl", Mesajlar); // MesajlariGetir metodunu tetikleyen kimse o hariç diğer tüm clientlardaki MesajlariAl fonskiyonunu çalıştır
           
            await Clients.All.SendAsync("MesajlariAl", Mesajlar);
        }


        //-----------------------------------------------------------------
      
        public override async Task OnConnectedAsync()
        {
            //Burası huba b ağlanan her kişi için otomatik çalışırç

            BagliKisiSayisi++;
            //await Clients.Caller // BU sadece hubda connected olan o an kimse bu metodu kim tetiklediyse onda şu fonksiytonu çaşıtır demek için kullanılırç

            await Clients.All.SendAsync("BagliKisiSayisiGetir", BagliKisiSayisi);
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            //Burası hub bağlantısı iptal olan herkes için otomatik çalışır.
            BagliKisiSayisi--;
            await Clients.All.SendAsync("BagliKisiSayisiGetir", BagliKisiSayisi);


            await base.OnConnectedAsync();
        }



    }
}
