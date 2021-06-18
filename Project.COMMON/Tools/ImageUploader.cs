using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Project.COMMON.Tools
{
    //Class'ımız ve method'umuz static'tir çünkü instance almadan direk kullanmak istiyoruz
    public static class ImageUploader
    {
        //Geriye string deger döndüren metodumuz resmin yolunu döndürecek veya resim yükleme ilgili bir sorun varsa onun kodunu döndürecek "1","2","3","C:/Images/"

        //HttpPostedFileBase => MVC'de eger Post yönteminde bir dosya geliyorsa bu dosya,HttpPostedFileBase tipinde tutulur. Bu tipi kullandığımız için bu class'ımız sadece MVC'ye özgüdür


        public static string UploadImage(string serverPath,HttpPostedFileBase file)
        {


            if (file!=null)
            {
                Guid uniqueName = Guid.NewGuid();

                string[] fileArray = file.FileName.Split('.'); //burada Split metodu sayesinde ilgili yapının uzantısının da iceride bulundugu bir string dizisi almıs olduk...Split metodu belirttiginiz char karakterinden metni bölerek size bir array sunar.

                string extension = fileArray[fileArray.Length - 1].ToLower();//dosya uzantısını yakalayarak kücük harflere cevirdik.

                string fileName = $"{uniqueName}.{extension}"; //normal şartlarda biz burada Guid kullandıgımız icin asla bir dosya ismi aynı olmayacaktır...Lakin GUid kullanmazsak (kullanıcıya yüklemek istedigi dosyanın ismini girdirebiliriz)...Böyle bir durum söz konusu ise ek olarak bir kontrol yapmamız gerekir. Bunu öncelikle extension'ı kontrol edip sonra yapmalıyız.
                if (extension=="jpg"||extension=="gif"||extension=="png"||extension=="jpeg")
                {
                    //Eger dosya ismi zaten varsa
                    if (File.Exists(HttpContext.Current.Server.MapPath(serverPath+fileName)))
                    {
                        return "1"; //Ancak GUid kullandıgımız icin bu acıdan zaten güvendeyiz...(Dosya zaten var kodu)
                    }

                    else
                    {
                        string filePath = HttpContext.Current.Server.MapPath(serverPath + fileName);
                        file.SaveAs(filePath);
                        return serverPath + fileName;

                    }
                }
                else
                {
                    return "2"; //Secilen dosya bir resim degildir.
                }

            }

            else
            {
                return "3"; //Dosya bos kodu
            }

          
        }
    }
}