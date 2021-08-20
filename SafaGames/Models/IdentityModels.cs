using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SafaGames.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("SafaGamesdb", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }

    public class MyContext : DbContext
    {
        public MyContext() : base("SafaGamesdb") { }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    Database.SetInitializer<MyContext>(null);
        //    base.OnModelCreating(modelBuilder);
        //}

        public DbSet<Blogs> Blogs { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Contactus> Contactus { get; set; }
        public DbSet<Countrys> Countrys { get; set; }
        public DbSet<Citys> Citys { get; set; }
        public DbSet<Areas> Areas { get; set; }
        public DbSet<Categorys> Categorys { get; set; }
        public DbSet<AvdTypes> AvdTypes { get; set; }
        public DbSet<Styles> Styles { get; set; }
        public DbSet<Games> Games { get; set; }

        public System.Data.Entity.DbSet<SafaGames.Models.Devices> Devices { get; set; }
    }

    public class Contactus
    {
        [Key]
        public int id { get; set; }
        [Required(ErrorMessage = "نام و نام خانوادگی اجباری است")]
        [Display(Name = "نام و نام خانوادگی")]
        public string Names { get; set; }

        [Required(ErrorMessage = "ایمیل یا تلفن همراه اجباری است")]
        [Display(Name = "ایمیل یا تلفن همراه")]
        public string Emails { get; set; }

        [Required(ErrorMessage = "متن پیام اجباری است")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "پیام")]
        public string Messages { get; set; }
        public string dt { get; set; }
        public string tm { get; set; }
        public string users { get; set; }
    }
    public class News
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "عنوان خبر اجباری است")]
        [Display(Name = "عنوان")]
        public string Title { get; set; }

        [Required(ErrorMessage = "اطلاعات خبر اجباری است")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "توضیح خبر")]
        public string Body { get; set; }

        [Display(Name = "عکس")]
        public string imgpic { get; set; }
        public string dt { get; set; }
        public string tm { get; set; }
        public string users { get; set; }
    }
    public class Blogs
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "عنوان بلاگ اجباری است")]
        [Display(Name = "عنوان")]
        public string Title { get; set; }

        [Required(ErrorMessage = "اطلاعات بلاگ اجباری است")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "توضیح بلاگ")]
        public string Body { get; set; }

        [Display(Name = "عکس")]
        public string imgpic { get; set; }
        public string dt { get; set; }
        public string tm { get; set; }
        public string users { get; set; }
    }
    public class Countrys
    {
        [Key]
        public int CountryID { get; set; }

        [Required(ErrorMessage = "عنوان کشور اجباری است")]
        [Display(Name = "کشور")]
        public string Title { get; set; }

        [Display(Name = "عکس")]
        public string imgpic { get; set; }

        public string dt { get; set; }
        public string tm { get; set; }
        public string users { get; set; }

        public virtual List<Citys> LCitys { get; set; }
    }
    public class Citys
    {
        [Key]
        public int CityID { get; set; }

        [Required(ErrorMessage = "عنوان شهر اجباری است")]
        [Display(Name = "شهر")]
        public string Title { get; set; }

        [Display(Name = "عکس")]
        public string imgpic { get; set; }

        public string dt { get; set; }
        public string tm { get; set; }
        public string users { get; set; }

        [Display(Name = "کشور")]
        public int CountryID { get; set; }
        public virtual Countrys countrys { get; set; }

        public virtual List<Areas> LAreas { get; set; }
    }
    public class Areas
    {
        [Key]
        public int AreaID { get; set; }

        [Required(ErrorMessage = "عنوان منطقه اجباری است")]
        [Display(Name = "مطقه")]
        public string Title { get; set; }

        [Display(Name = "عکس")]
        public string imgpic { get; set; }

        public string dt { get; set; }
        public string tm { get; set; }
        public string users { get; set; }

        [Display(Name = "شهر")]
        public int CityID { get; set; }
        public virtual Citys citys { get; set; }
        public virtual List<Games> LGames { get; set; }
    }
    public class Categorys
    {
        [Key]
        public int CategoryID { get; set; }

        [Required(ErrorMessage = "عنوان دسته بندی اجباری است")]
        [Display(Name = "دسته بندی")]
        public string Title { get; set; }

        [Display(Name = "عکس")]
        public string imgpic { get; set; }

        public string dt { get; set; }
        public string tm { get; set; }
        public string users { get; set; }

        public virtual List<Games> LGames { get; set; }
    }
    public class AvdTypes
    {
        [Key]
        public int AvdTypeID { get; set; }

        [Required(ErrorMessage = "عنوان نوع آگهی اجباری است")]
        [Display(Name = "نوع آگهی")]
        public string Title { get; set; }

        [Display(Name = "عکس")]
        public string imgpic { get; set; }

        public string dt { get; set; }
        public string tm { get; set; }
        public string users { get; set; }

        public virtual List<Games> LGames { get; set; }
    }
    public class Devices
    {
        [Key]
        public int DeviceID { get; set; }

        [Required(ErrorMessage = "عنوان کنسول اجباری است")]
        [Display(Name = "کنسول")]
        public string Title { get; set; }

        [Display(Name = "عکس")]
        public string imgpic { get; set; }

        public string dt { get; set; }
        public string tm { get; set; }
        public string users { get; set; }

        public virtual List<Games> LGames { get; set; }
    }
    public class Styles
    {
        [Key]
        public int StyleID { get; set; }

        [Required(ErrorMessage = "عنوان سبک اجباری است")]
        [Display(Name = "عنوان")]
        public string Title { get; set; }

        [Display(Name = "عکس")]
        public string imgpic { get; set; }

        public string dt { get; set; }
        public string tm { get; set; }
        public string users { get; set; }

        public virtual List<Games> LGames { get; set; }
    }
    public class Games
    {
        [Key]
        public int GameID { get; set; }

        [Display(Name = "عکس")]
        public string imgpic { get; set; }

        [Required(ErrorMessage = "عنوان بازی اجباری است")]
        [Display(Name = "بازی")]
        public string Title { get; set; }

        [Required(ErrorMessage = "درباره بازی اجباری است")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "درباره بازی")]
        public string Body { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "تگ بازی")]
        public string Tags { get; set; }

        [Required(ErrorMessage = "قیمت بازی اجباری است")]
        [DisplayFormat(DataFormatString = "{0:0}", ApplyFormatInEditMode = true)]
        [Display(Name = "قیمت بازی")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "شماره تماس اجباری است")]
        [Display(Name = "شماره تماس")]
        public string Tells { get; set; }

        [Display(Name = "سبک")]
        public int StyleID { get; set; }
        public virtual Styles styles { get; set; }

        [Display(Name = "منطقه")]
        public int AreaID { get; set; }
        public virtual Areas areas { get; set; }

        [Display(Name = "دسته بندی")]
        public int categoryID { get; set; }
        public virtual Categorys categorys { get; set; }

        [Display(Name = "کنسول")]
        public int DeviceID { get; set; }
        public virtual Devices devices { get; set; }

        [Display(Name = "نوع آگهی")]
        public int AvdTypeID { get; set; }
        public virtual AvdTypes avdtypes { get; set; }


        [Display(Name = "فعال")]
        public bool Status { get; set; }

        public string dt { get; set; }
        public string tm { get; set; }
        public string users { get; set; }
    }
}