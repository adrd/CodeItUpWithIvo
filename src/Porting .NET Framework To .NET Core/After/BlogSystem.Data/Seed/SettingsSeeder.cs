namespace BlogSystem.Data.Seed
{
    using System.Linq;
    using Contracts;
    using Models;

    public class SettingsSeeder : IDatabaseSeeder
    {
        private readonly ApplicationDbContext data;

        public SettingsSeeder(ApplicationDbContext data)
        {
            this.data = data;
        }

        public void Seed()
        {
            if (this.data.Settings.Any())
            {
                return;
            }

            this.data.Settings.Add(new Setting { Name = "Logo URL", Value = "/img/header-logo.png" });
            this.data.Settings.Add(new Setting { Name = "Home Title", Value = "Home Title" });
            this.data.Settings.Add(new Setting { Name = "Blog Name", Value = "Blog Name" });
            this.data.Settings.Add(new Setting { Name = "Blog Url", Value = "Blog Url" });
            this.data.Settings.Add(new Setting { Name = "Keywords", Value = "Keywords" });
            this.data.Settings.Add(new Setting { Name = "Author", Value = "Author" });
            this.data.Settings.Add(new Setting { Name = "GitHub Profile", Value = "GitHub Profile" });
            this.data.Settings.Add(new Setting { Name = "GitHub Badge HTML Code", Value = "GitHub Badge HTML Code" });
            this.data.Settings.Add(new Setting { Name = "StackOverflow Badge HTML Code", Value = "StackOverflow Badge HTML Code" });
            this.data.Settings.Add(new Setting { Name = "Stack Overflow Profile", Value = "Stack Overflow Profile" });
            this.data.Settings.Add(new Setting { Name = "Linked In Profile", Value = "Linked In Profile" });
            this.data.Settings.Add(new Setting { Name = "Contact Email", Value = "Contact Email" });
            this.data.Settings.Add(new Setting { Name = "Facebook Profile", Value = "Facebook Profile" });
            this.data.Settings.Add(new Setting { Name = "Foursquare Profile", Value = "Foursquare Profile" });
            this.data.Settings.Add(new Setting { Name = "Google Profile", Value = "Google+ Profile" });
            this.data.Settings.Add(new Setting { Name = "RSS Url", Value = "RSS Url" });

            this.data.SaveChanges();
        }
    }
}
