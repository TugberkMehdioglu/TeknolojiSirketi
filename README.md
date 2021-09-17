# e-commerce-simulation

e-commerce-simulation-libs Repository'deki BankaAPI KargoAPI DepoAPI MagazaWinUi KargoWinUI projeleri ile birlikte çalışır.

DesignPatterns = SingletonPattern, StrategyPattern

ArchitecturePattern = NTier

Bu proje belirli API ve WindowsForms uygulamaları ile birlikte çalışan, elektronik ürün satışı yapan bir web sitesinin simülasyonudur. Initilization ile proje belirli ürünler oluşturulmuş şekilde kurulur, kullanıcı siteye üye olmadan siteye giriş yapıp ürünleri sepete atabilir fakat satın alma kısmında üye olması gerekir. Üyelik gerçekleştiğinde kullanıcının mailine yollanan linke tıklanmadan üyeliğe giriş yapılamaz, üyelik işlemi tamamlandığında kullanıcı profileCRUD ve addressCRUD işlemleri gerçekleştirebilir sepete attığı ürünlerin satın alımını gerçekleştirebilir. Satın almada girilen kart bilgileri BankaAPI'a gider ve buradan successStatuseCode döndüğünde DepoAPI'a gider buradan da olumlu dönüş olduğunda KargoAPI'a adres bilgileri yollanır ve sipariş tamamlanır, kullanıcının mailine sipariş tutarı ve kargo takip numarasını bildiren mail yollanır. Sitenin kargo takip bölümünden bu takip no ile kargo takibi gerçekleştirilir, aynı zamanda KargoWinUi'da kargo şirketi elemanının yaptığı güncellemeler(Dağıtıma çıktı - Kargo teslim edildi vb.) kullanıcı tarafından da görülebilir, kargo elemanı teslim edildi bilgisi girdiğinde kullanıcının mailine bilgilendirme mesajı yollanır. MagazaWinUi sayesinde mağazada yapılan satışlarda kayıt altına alınıp DepoAPI'dan stok düşer. DepoAPI'da stoğu azalan ürünlerin bilgisi depo sorumlusuna mail olarak yollanır.

Admin girişi yapıldığında sadece adminin işlem yapabileceği categoryCRUD productCRUD ve yetkilendirme işlemleri kısmı açılır. Sitedeki bize ulaşın kısmından yollanan destek talepleri teknik destek sorumlusunun mailine yollanır.
